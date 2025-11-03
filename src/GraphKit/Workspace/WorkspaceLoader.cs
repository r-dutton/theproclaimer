using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using System.Xml.Linq;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;

namespace GraphKit.Workspace;

public sealed class WorkspaceLoader
{
    private readonly string _workspaceRoot;
    private readonly IReadOnlyList<string>? _explicitSolutions;
    private readonly bool _useRoslyn;
    private readonly MSBuildWorkspace? _roslynWorkspace;
    private static readonly ImmutableArray<MetadataReference> DefaultMetadataReferences = CreateDefaultMetadataReferences();

    public WorkspaceLoader(
        string workspaceRoot,
        IReadOnlyList<string>? explicitSolutions = null,
        bool useRoslyn = false,
        MSBuildWorkspace? roslynWorkspace = null)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        _explicitSolutions = explicitSolutions;
        _useRoslyn = useRoslyn;
        _roslynWorkspace = roslynWorkspace;

        if (_useRoslyn && _roslynWorkspace is null)
        {
            throw new ArgumentException("A Roslyn workspace instance must be provided when Roslyn loading is enabled.", nameof(roslynWorkspace));
        }
    }

    public async Task<IReadOnlyList<ProjectInfo>> LoadAsync(CancellationToken cancellationToken = default)
    {
        var solutionPaths = await ResolveSolutionPathsAsync(cancellationToken).ConfigureAwait(false);

        if (_useRoslyn)
        {
            var roslynLoader = new RoslynWorkspaceLoader(_workspaceRoot, _roslynWorkspace!, solutionPaths);
            return await roslynLoader.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        return await LoadLegacyAsync(solutionPaths, cancellationToken).ConfigureAwait(false);
    }

    private async Task<List<string>> ResolveSolutionPathsAsync(CancellationToken cancellationToken)
    {
        var configPath = Path.Combine(_workspaceRoot, "flow.workspace.json");
        var solutionPaths = new List<string>();

        if (_explicitSolutions is { Count: > 0 })
        {
            foreach (var candidate in _explicitSolutions)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    continue;
                }

                var path = Path.GetFullPath(Path.IsPathRooted(candidate)
                    ? candidate
                    : Path.Combine(_workspaceRoot, candidate));

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Solution '{candidate}' could not be resolved relative to '{_workspaceRoot}'.", path);
                }

                solutionPaths.Add(path);
            }
        }
        else if (File.Exists(configPath))
        {
            using var stream = File.OpenRead(configPath);
            var doc = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (doc.RootElement.TryGetProperty("solutions", out var solutionsElement))
            {
                foreach (var element in solutionsElement.EnumerateArray())
                {
                    var rel = element.GetString();
                    if (!string.IsNullOrWhiteSpace(rel))
                    {
                        solutionPaths.Add(Path.GetFullPath(Path.Combine(_workspaceRoot, rel)));
                    }
                }
            }
        }
        else
        {
            solutionPaths.AddRange(Directory
                .EnumerateFiles(_workspaceRoot, "*.sln", SearchOption.AllDirectories)
                .Where(path => !path.Contains("/bin/") && !path.Contains("/obj/") && !path.Contains("/.git/")));
        }

        return solutionPaths.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    private async Task<IReadOnlyList<ProjectInfo>> LoadLegacyAsync(IReadOnlyList<string> solutionPaths, CancellationToken cancellationToken)
    {
        var projectPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var solutionPath in solutionPaths)
        {
            foreach (var line in await File.ReadAllLinesAsync(solutionPath, cancellationToken).ConfigureAwait(false))
            {
                if (!line.StartsWith("Project", StringComparison.Ordinal))
                {
                    continue;
                }

                var parts = line.Split('"');
                if (parts.Length >= 6)
                {
                    var projectRelative = parts[5].Replace('\\', Path.DirectorySeparatorChar);
                    var candidate = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(solutionPath)!, projectRelative));
                    if (candidate.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
                    {
                        projectPaths.Add(candidate);
                    }
                }
            }
        }

        if (projectPaths.Count == 0)
        {
            return Array.Empty<ProjectInfo>();
        }

        var bag = new ConcurrentBag<ProjectInfo>();
        var parallelOptions = new ParallelOptions
        {
            CancellationToken = cancellationToken,
            MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount - 1)
        };

        Parallel.ForEach(projectPaths, parallelOptions, projectPath =>
        {
            parallelOptions.CancellationToken.ThrowIfCancellationRequested();

            var projectDir = Path.GetDirectoryName(projectPath)!;
            var relativeDir = Path.GetRelativePath(_workspaceRoot, projectDir).Replace('\\', '/');
            using var stream = File.OpenRead(projectPath);
            var projectXml = XDocument.Load(stream);
            var ns = projectXml.Root?.Name.Namespace ?? XNamespace.None;
            var assemblyName = projectXml.Root?
                .Elements(ns + "PropertyGroup")
                .Elements(ns + "AssemblyName")
                .Select(e => e.Value)
                .FirstOrDefault() ?? Path.GetFileNameWithoutExtension(projectPath);

            var rootNamespace = projectXml.Root?
                .Elements(ns + "PropertyGroup")
                .Elements(ns + "RootNamespace")
                .Select(e => e.Value)
                .FirstOrDefault() ?? assemblyName;

            var files = EnumerateSourceFiles(projectDir)
                .Select(Path.GetFullPath)
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var langVersionValue = GetProjectProperty(projectXml, ns, "LangVersion");
            var defineConstants = GetProjectProperty(projectXml, ns, "DefineConstants");
            var nullableValue = GetProjectProperty(projectXml, ns, "Nullable");
            var parseOptions = CreateParseOptions(langVersionValue, defineConstants);
            var outputType = GetProjectProperty(projectXml, ns, "OutputType");
            var compilationOptions = CreateCompilationOptions(nullableValue, outputType);

            bag.Add(new ProjectInfo(
                projectPath,
                assemblyName,
                rootNamespace,
                relativeDir,
                files,
                CreateCompilationFactory(assemblyName, files, parseOptions, compilationOptions)));
        });

        return bag
            .OrderBy(p => p.ProjectPath, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static Func<Compilation> CreateCompilationFactory(
        string assemblyName,
        IReadOnlyList<string> files,
        CSharpParseOptions parseOptions,
        CSharpCompilationOptions compilationOptions)
        => () =>
        {
            var trees = new List<SyntaxTree>(files.Count);
            foreach (var file in files)
            {
                try
                {
                    var text = File.ReadAllText(file);
                    var tree = CSharpSyntaxTree.ParseText(text, options: parseOptions, path: file);
                    trees.Add(tree);
                }
                catch (IOException)
                {
                    continue;
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
            }

            return CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: trees,
                references: DefaultMetadataReferences,
                options: compilationOptions);
        };

    private static CSharpParseOptions CreateParseOptions(string? langVersion, string? defineConstants)
    {
        var options = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);

        if (!string.IsNullOrWhiteSpace(langVersion) &&
            LanguageVersionFacts.TryParse(langVersion, out var version))
        {
            options = options.WithLanguageVersion(version);
        }

        if (!string.IsNullOrWhiteSpace(defineConstants))
        {
            var symbols = defineConstants
                .Split(new[] { ';', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            if (symbols.Length > 0)
            {
                options = options.WithPreprocessorSymbols(symbols);
            }
        }

        return options;
    }

    private static CSharpCompilationOptions CreateCompilationOptions(string? nullableValue, string? outputType)
    {
        var nullable = MapNullableOption(nullableValue);
        var outputKind = MapOutputKind(outputType);

        return new CSharpCompilationOptions(outputKind)
            .WithNullableContextOptions(nullable)
            .WithOptimizationLevel(OptimizationLevel.Debug)
            .WithMetadataImportOptions(MetadataImportOptions.Public);
    }

    private static OutputKind MapOutputKind(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return OutputKind.DynamicallyLinkedLibrary;
        }

        return value.Trim().ToLowerInvariant() switch
        {
            "exe" => OutputKind.ConsoleApplication,
            "winexe" => OutputKind.WindowsApplication,
            "appcontainerexe" => OutputKind.WindowsRuntimeApplication,
            "library" => OutputKind.DynamicallyLinkedLibrary,
            "netmodule" => OutputKind.NetModule,
            "winmdobj" => OutputKind.WindowsRuntimeMetadata,
            _ => OutputKind.DynamicallyLinkedLibrary
        };
    }

    private static NullableContextOptions MapNullableOption(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return NullableContextOptions.Disable;
        }

        return value.Trim().ToLowerInvariant() switch
        {
            "enable" => NullableContextOptions.Enable,
            "disable" => NullableContextOptions.Disable,
            "warnings" => NullableContextOptions.Warnings,
            "annotations" => NullableContextOptions.Annotations,
            "safeonly" => NullableContextOptions.Enable,
            _ => NullableContextOptions.Disable
        };
    }

    private static string? GetProjectProperty(XDocument projectXml, XNamespace ns, string propertyName)
    {
        return projectXml.Root?
            .Elements(ns + "PropertyGroup")
            .Elements(ns + propertyName)
            .Select(e => e.Value)
            .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
    }

    private static ImmutableArray<MetadataReference> CreateDefaultMetadataReferences()
    {
        var trustedAssemblies = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") as string;
        if (string.IsNullOrWhiteSpace(trustedAssemblies))
        {
            return ImmutableArray<MetadataReference>.Empty;
        }

        var builder = ImmutableArray.CreateBuilder<MetadataReference>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var path in trustedAssemblies.Split(Path.PathSeparator))
        {
            if (string.IsNullOrWhiteSpace(path) || !seen.Add(path))
            {
                continue;
            }

            try
            {
                builder.Add(MetadataReference.CreateFromFile(path));
            }
            catch (IOException)
            {
                continue;
            }
            catch (PlatformNotSupportedException)
            {
                continue;
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }
        }

        return builder.ToImmutable();
    }

    private static IEnumerable<string> EnumerateSourceFiles(string projectDirectory)
    {
        var stack = new Stack<string>();
        stack.Push(projectDirectory);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(current, "*.cs", SearchOption.TopDirectoryOnly);
            }
            catch (IOException)
            {
                continue;
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            foreach (var file in files)
            {
                yield return file;
            }

            IEnumerable<string> directories;
            try
            {
                directories = Directory.EnumerateDirectories(current);
            }
            catch (IOException)
            {
                continue;
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            foreach (var directory in directories)
            {
                var name = Path.GetFileName(directory);
                if (IsIgnoredDirectory(name))
                {
                    continue;
                }

                stack.Push(directory);
            }
        }
    }

    private static bool IsIgnoredDirectory(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return name.Equals("bin", StringComparison.OrdinalIgnoreCase) ||
               name.Equals("obj", StringComparison.OrdinalIgnoreCase) ||
               name.Equals(".git", StringComparison.OrdinalIgnoreCase) ||
               name.Equals(".vs", StringComparison.OrdinalIgnoreCase) ||
               name.Equals("node_modules", StringComparison.OrdinalIgnoreCase);
    }
}
