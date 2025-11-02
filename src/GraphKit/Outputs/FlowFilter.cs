using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using GraphKit.Outputs.FlowBuilder;

namespace GraphKit.Outputs
{
    public static class FlowFilter
    {
        public static Func<FlowNode, bool> BuildPredicate(IEnumerable<string> patterns)
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

            return node => normalized.Any(pattern => Matches(node, pattern));
        }

        public static bool Matches(FlowNode node, string pattern)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
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

            bool MatchesText(string? text)
                => !string.IsNullOrWhiteSpace(text) && FileSystemName.MatchesSimpleExpression(pattern, text, ignoreCase: true);

            if (MatchesText(node.Id) || MatchesText(node.GetString("name")) || MatchesText(node.GetString("fqdn")))
            {
                return true;
            }

            foreach (var tag in node.GetStringList("tags"))
            {
                if (MatchesText(tag))
                {
                    return true;
                }
            }

            foreach (var value in node.Props.Values)
            {
                if (MatchesText(value?.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsMatchAll(string pattern)
            => string.Equals(pattern, "*", StringComparison.OrdinalIgnoreCase)
               || string.Equals(pattern, "**", StringComparison.OrdinalIgnoreCase)
               || string.Equals(pattern, "all", StringComparison.OrdinalIgnoreCase);
    }
}
