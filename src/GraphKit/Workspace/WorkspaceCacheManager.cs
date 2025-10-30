using System.Text.Json;
using System.Threading.Tasks;
using GraphKit.Graph;

namespace GraphKit.Workspace;

internal sealed class WorkspaceCacheManager
{
    private sealed record CacheSnapshot(
        string AnalyzerVersion,
        Dictionary<string, string> ProjectHashes);

    private readonly string _workspaceRoot;
    private readonly string _outputDirectory;
    private readonly string _cachePath;
    private readonly string _documentPath;
    private CacheSnapshot? _snapshot;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public WorkspaceCacheManager(string workspaceRoot, string outputDirectory)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        _outputDirectory = Path.GetFullPath(Path.Combine(_workspaceRoot, outputDirectory));
        _cachePath = Path.Combine(_outputDirectory, "graph.cache.json");
        _documentPath = Path.Combine(_outputDirectory, "graph.document.json");
    }

    public async Task<GraphDocument?> TryLoadCachedDocumentAsync(
        string analyzerVersion,
        IReadOnlyDictionary<string, ProjectFingerprint> currentFingerprints,
        CancellationToken cancellationToken)
    {
        var snapshot = await LoadSnapshotAsync(cancellationToken).ConfigureAwait(false);
        if (snapshot is null)
        {
            return null;
        }

        if (!string.Equals(snapshot.AnalyzerVersion, analyzerVersion, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!FingerprintsMatch(snapshot.ProjectHashes, currentFingerprints))
        {
            return null;
        }

        if (!File.Exists(_documentPath))
        {
            return null;
        }

        await using var stream = File.OpenRead(_documentPath);
        var document = await JsonSerializer.DeserializeAsync<GraphDocument>(stream, cancellationToken: cancellationToken).ConfigureAwait(false);
        return document;
    }

    public async Task SaveAsync(string analyzerVersion, GraphDocument document, IReadOnlyDictionary<string, ProjectFingerprint> fingerprints, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_outputDirectory);

        var payload = new CacheSnapshot(
            analyzerVersion,
            fingerprints.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Hash,
                StringComparer.OrdinalIgnoreCase));

        await using var stream = File.Create(_cachePath);
        await JsonSerializer.SerializeAsync(stream, payload, SerializerOptions, cancellationToken).ConfigureAwait(false);
        await SaveDocumentAsync(document, cancellationToken).ConfigureAwait(false);
        _snapshot = payload;
    }

    private async Task<CacheSnapshot?> LoadSnapshotAsync(CancellationToken cancellationToken)
    {
        if (_snapshot is not null)
        {
            return _snapshot;
        }

        if (!File.Exists(_cachePath))
        {
            return null;
        }

        try
        {
            await using var stream = File.OpenRead(_cachePath);
            _snapshot = await JsonSerializer.DeserializeAsync<CacheSnapshot>(stream, SerializerOptions, cancellationToken).ConfigureAwait(false);
            return _snapshot;
        }
        catch (JsonException)
        {
            return null;
        }
        catch (IOException)
        {
            return null;
        }
    }

    private static bool FingerprintsMatch(
        IReadOnlyDictionary<string, string> cached,
        IReadOnlyDictionary<string, ProjectFingerprint> current)
    {
        if (cached.Count != current.Count)
        {
            return false;
        }

        foreach (var (key, fingerprint) in current)
        {
            if (!cached.TryGetValue(key, out var cachedHash))
            {
                return false;
            }

            if (!string.Equals(cachedHash, fingerprint.Hash, StringComparison.Ordinal))
            {
                return false;
            }
        }

        return true;
    }

    private async Task SaveDocumentAsync(GraphDocument document, CancellationToken cancellationToken)
    {
        await using var docStream = File.Create(_documentPath);
        await JsonSerializer.SerializeAsync(docStream, document, SerializerOptions, cancellationToken).ConfigureAwait(false);
    }
}
