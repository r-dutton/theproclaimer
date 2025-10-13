using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GraphKit.Workspace;

public sealed class FlowWorkspaceIndex
{
    private static readonly IReadOnlyCollection<string> EmptyAssemblies = Array.Empty<string>();
    private readonly Dictionary<string, IReadOnlyCollection<string>> _serviceAssemblies;

    public FlowWorkspaceIndex()
        : this(new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase))
    {
    }

    public FlowWorkspaceIndex(IReadOnlyDictionary<string, IEnumerable<string>> services)
    {
        _serviceAssemblies = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var kvp in services)
        {
            if (string.IsNullOrWhiteSpace(kvp.Key))
            {
                continue;
            }

            var assemblies = kvp.Value?
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .Select(a => a!.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (assemblies is { Length: > 0 })
            {
                _serviceAssemblies[kvp.Key] = assemblies;
            }
        }
    }

    public static FlowWorkspaceIndex Load(string workspaceRoot)
    {
        var root = Path.GetFullPath(workspaceRoot ?? string.Empty);
        var path = Path.Combine(root, "flow.workspace.json");
        if (!File.Exists(path))
        {
            return new FlowWorkspaceIndex();
        }

        try
        {
            using var stream = File.OpenRead(path);
            using var document = JsonDocument.Parse(stream);
            if (!document.RootElement.TryGetProperty("services", out var servicesElement))
            {
                return new FlowWorkspaceIndex();
            }

            var services = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var serviceProperty in servicesElement.EnumerateObject())
            {
                if (serviceProperty.Value.TryGetProperty("assembly_names", out var assemblyNames))
                {
                    var assemblies = assemblyNames
                        .EnumerateArray()
                        .Select(element => element.GetString())
                        .Where(value => !string.IsNullOrWhiteSpace(value))
                        .Select(value => value!.Trim())
                        .ToArray();

                    if (assemblies.Length > 0)
                    {
                        services[serviceProperty.Name] = assemblies;
                    }
                }
            }

            return services.Count > 0 ? new FlowWorkspaceIndex(services) : new FlowWorkspaceIndex();
        }
        catch (JsonException)
        {
            return new FlowWorkspaceIndex();
        }
        catch (IOException)
        {
            return new FlowWorkspaceIndex();
        }
    }

    public bool TryGetAssemblies(string serviceName, out IReadOnlyCollection<string> assemblies)
    {
        if (string.IsNullOrWhiteSpace(serviceName))
        {
            assemblies = EmptyAssemblies;
            return false;
        }

        if (_serviceAssemblies.TryGetValue(serviceName, out var result))
        {
            assemblies = result;
            return assemblies.Count > 0;
        }

        assemblies = EmptyAssemblies;
        return false;
    }
}
