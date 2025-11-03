using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace GraphKit.Workspace;

internal sealed record ProjectFingerprint(string RelativePath, string Hash, int FileCount)
{
    public static async Task<IReadOnlyDictionary<string, ProjectFingerprint>> ComputeAsync(
        IReadOnlyList<ProjectInfo> projects,
        string workspaceRoot,
        CancellationToken cancellationToken)
    {
        if (projects.Count == 0)
        {
            return new Dictionary<string, ProjectFingerprint>(StringComparer.OrdinalIgnoreCase);
        }

        var fingerprints = new ConcurrentDictionary<string, ProjectFingerprint>(StringComparer.OrdinalIgnoreCase);
        var options = new ParallelOptions
        {
            CancellationToken = cancellationToken,
            MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount - 1)
        };

        await Parallel.ForEachAsync(projects, options, async (project, ct) =>
        {
            ct.ThrowIfCancellationRequested();

            var relativePath = NormalizeRelativePath(workspaceRoot, project.ProjectPath);
            var hash = await ComputeHashAsync(project, ct).ConfigureAwait(false);
            var fingerprint = new ProjectFingerprint(relativePath, hash, project.SourceFiles.Count);
            fingerprints[relativePath] = fingerprint;
        }).ConfigureAwait(false);

        return fingerprints;
    }

    private static string NormalizeRelativePath(string workspaceRoot, string projectPath)
    {
        var relative = Path.GetRelativePath(workspaceRoot, projectPath).Replace('\\', '/');
        return string.IsNullOrWhiteSpace(relative) ? Path.GetFileName(projectPath) : relative;
    }

    private static ValueTask<string> ComputeHashAsync(ProjectInfo project, CancellationToken cancellationToken)
    {
        if (project.IsRoslyn)
        {
            return ComputeRoslynHashAsync(project, cancellationToken);
        }

        return ValueTask.FromResult(ComputeLegacyHash(project, cancellationToken));
    }

    private static async ValueTask<string> ComputeRoslynHashAsync(ProjectInfo project, CancellationToken cancellationToken)
    {
        var incremental = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        AppendString(incremental, project.ProjectPath);
        AppendString(incremental, "roslyn");

        if (project.RoslynProjectVersion is { } projectVersion)
        {
            AppendVersionStamp(incremental, projectVersion);
        }

        foreach (var kvp in project.DocumentFilePaths.OrderBy(static kv => kv.Value, StringComparer.OrdinalIgnoreCase))
        {
            cancellationToken.ThrowIfCancellationRequested();

            AppendString(incremental, kvp.Value);

            if (project.DocumentVersions.TryGetValue(kvp.Key, out var version))
            {
                AppendVersionStamp(incremental, version);
            }
            else
            {
                AppendString(incremental, "noversion");
            }
        }

        var hashBytes = incremental.GetHashAndReset();
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    private static string ComputeLegacyHash(ProjectInfo project, CancellationToken cancellationToken)
    {
        var incremental = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        AppendString(incremental, project.ProjectPath);
        AppendString(incremental, "legacy");

        Span<byte> buffer = stackalloc byte[16];

        foreach (var file in project.SourceFiles)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var info = new FileInfo(file);
                if (!info.Exists)
                {
                    AppendString(incremental, file);
                    AppendString(incremental, "missing");
                    continue;
                }

                AppendString(incremental, file);

                BinaryPrimitives.WriteInt64LittleEndian(buffer[..8], info.Length);
                BinaryPrimitives.WriteInt64LittleEndian(buffer[8..], info.LastWriteTimeUtc.Ticks);
                incremental.AppendData(buffer);
            }
            catch (IOException)
            {
                AppendString(incremental, file);
                AppendString(incremental, "ioerror");
            }
            catch (UnauthorizedAccessException)
            {
                AppendString(incremental, file);
                AppendString(incremental, "unauthorized");
            }
        }

        var hashBytes = incremental.GetHashAndReset();
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    private static void AppendString(IncrementalHash incremental, string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        incremental.AppendData(bytes);
    }

    private static void AppendVersionStamp(IncrementalHash incremental, VersionStamp version)
        => AppendString(incremental, version.ToString());
}
