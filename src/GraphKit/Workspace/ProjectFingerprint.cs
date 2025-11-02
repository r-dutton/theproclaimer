using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        await Parallel.ForEachAsync(projects, options, (project, ct) =>
        {
            ct.ThrowIfCancellationRequested();

            var relativePath = NormalizeRelativePath(workspaceRoot, project.ProjectPath);
            var hash = ComputeHash(project, ct);
            var fingerprint = new ProjectFingerprint(relativePath, hash, project.SourceFiles.Count);
            fingerprints[relativePath] = fingerprint;
            return ValueTask.CompletedTask;
        }).ConfigureAwait(false);

        return fingerprints;
    }

    private static string NormalizeRelativePath(string workspaceRoot, string projectPath)
    {
        var relative = Path.GetRelativePath(workspaceRoot, projectPath).Replace('\\', '/');
        return string.IsNullOrWhiteSpace(relative) ? Path.GetFileName(projectPath) : relative;
    }

    private static string ComputeHash(ProjectInfo project, CancellationToken cancellationToken)
    {
        var incremental = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        AppendString(incremental, project.ProjectPath);

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
}
