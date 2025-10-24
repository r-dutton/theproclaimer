using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GraphKit.Workspace;

public sealed class FlowWorkspaceIndex
{
    private sealed record ServiceEntry(IReadOnlyCollection<string> Assemblies, IReadOnlyCollection<string> Hosts);

    private static readonly IReadOnlyCollection<string> EmptyAssemblies = Array.Empty<string>();
    private readonly Dictionary<string, ServiceEntry> _services;
    private readonly Dictionary<string, string> _hostLookup;

    public FlowWorkspaceIndex()
        : this(new Dictionary<string, ServiceEntry>(StringComparer.OrdinalIgnoreCase))
    {
    }

    public FlowWorkspaceIndex(IReadOnlyDictionary<string, IEnumerable<string>> services)
        : this(ConvertLegacyAssemblies(services))
    {
    }

    private FlowWorkspaceIndex(IReadOnlyDictionary<string, ServiceEntry> services)
    {
        _services = new Dictionary<string, ServiceEntry>(StringComparer.OrdinalIgnoreCase);
        _hostLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var (serviceName, entry) in services)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                continue;
            }

            var normalizedAssemblies = entry.Assemblies
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .Select(a => a.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            var normalizedHosts = entry.Hosts
                .Where(h => !string.IsNullOrWhiteSpace(h))
                .Select(h => h.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (normalizedAssemblies.Length == 0 && normalizedHosts.Length == 0)
            {
                continue;
            }

            var storedEntry = new ServiceEntry(normalizedAssemblies, normalizedHosts);
            _services[serviceName] = storedEntry;

            foreach (var host in normalizedHosts)
            {
                _hostLookup.TryAdd(host, serviceName);
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

            var services = new Dictionary<string, ServiceEntry>(StringComparer.OrdinalIgnoreCase);
            foreach (var serviceProperty in servicesElement.EnumerateObject())
            {
                var hostSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                if (serviceProperty.Value.TryGetProperty("assembly_names", out var assemblyNames))
                {
                    var assemblies = assemblyNames
                        .EnumerateArray()
                        .Select(element => element.GetString())
                        .Where(value => !string.IsNullOrWhiteSpace(value))
                        .Select(value => value!.Trim())
                        .ToArray();

                    if (serviceProperty.Value.TryGetProperty("base_addresses", out var baseAddresses))
                    {
                        foreach (var addressProperty in baseAddresses.EnumerateObject())
                        {
                            var addressValue = addressProperty.Value.GetString();
                            if (string.IsNullOrWhiteSpace(addressValue))
                            {
                                continue;
                            }

                            if (Uri.TryCreate(addressValue, UriKind.Absolute, out var uri))
                            {
                                hostSet.Add(uri.Host);
                            }
                        }
                    }

                    services[serviceProperty.Name] = new ServiceEntry(assemblies, hostSet);
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

        if (_services.TryGetValue(serviceName, out var entry) && entry.Assemblies.Count > 0)
        {
            assemblies = entry.Assemblies;
            return true;
        }

        assemblies = EmptyAssemblies;
        return false;
    }

    public bool TryResolveServiceByHost(string? host, out string serviceName)
    {
        if (string.IsNullOrWhiteSpace(host))
        {
            serviceName = string.Empty;
            return false;
        }

        if (_hostLookup.TryGetValue(host.Trim(), out var resolved))
        {
            serviceName = resolved;
            return true;
        }

        serviceName = string.Empty;
        return false;
    }

    private static IReadOnlyDictionary<string, ServiceEntry> ConvertLegacyAssemblies(IReadOnlyDictionary<string, IEnumerable<string>>? services)
    {
        if (services is null || services.Count == 0)
        {
            return new Dictionary<string, ServiceEntry>(StringComparer.OrdinalIgnoreCase);
        }

        var converted = new Dictionary<string, ServiceEntry>(StringComparer.OrdinalIgnoreCase);
        foreach (var (serviceName, assemblyList) in services)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                continue;
            }

            var normalizedAssemblies = (assemblyList ?? Array.Empty<string>())
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .Select(a => a!.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            converted[serviceName] = new ServiceEntry(normalizedAssemblies, Array.Empty<string>());
        }

        return converted;
    }
}
