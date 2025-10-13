using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using GraphKit.Graph;

namespace GraphKit.Outputs;

public static class FlowFilter
{
    public static Func<GraphNode, bool> BuildPredicate(IEnumerable<string> patterns)
    {
        var normalized = patterns
            ?.SelectMany(p => (p ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Select(p => p.Trim())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray() ?? Array.Empty<string>();

        if (normalized.Length == 0)
        {
            return _ => false;
        }

        if (normalized.Any(IsMatchAll))
        {
            return _ => true;
        }

        return controller => normalized.Any(pattern => Matches(controller, pattern));
    }

    public static bool Matches(GraphNode controller, string pattern)
    {
        if (controller is null)
        {
            throw new ArgumentNullException(nameof(controller));
        }

        if (string.IsNullOrWhiteSpace(pattern))
        {
            return false;
        }

        pattern = pattern.Trim();
        if (IsMatchAll(pattern))
        {
            return true;
        }

        bool MatchesText(string? text) =>
            !string.IsNullOrWhiteSpace(text) &&
            FileSystemName.MatchesSimpleExpression(pattern, text, ignoreCase: true);

        if (MatchesText(controller.Id) || MatchesText(controller.Name) || MatchesText(controller.Fqdn))
        {
            return true;
        }

        if (controller.Tags.Any(MatchesText))
        {
            return true;
        }

        if (controller.Props is { Count: > 0 })
        {
            foreach (var value in controller.Props.Values)
            {
                if (MatchesText(value?.ToString()))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsMatchAll(string pattern)
        => string.Equals(pattern, "*", StringComparison.OrdinalIgnoreCase)
           || string.Equals(pattern, "**", StringComparison.OrdinalIgnoreCase)
           || string.Equals(pattern, "all", StringComparison.OrdinalIgnoreCase);
}
