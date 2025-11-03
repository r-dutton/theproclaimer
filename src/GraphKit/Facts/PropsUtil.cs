using System.Collections.Generic;
using GraphKit.Graph;
using Microsoft.CodeAnalysis;

namespace GraphKit.Facts
{
    public static class PropsUtil
    {
        public static void AddSource(this IDictionary<string, object?> props, ISymbol? symbol)
        {
            if (symbol?.Locations is null)
            {
                return;
            }

            foreach (var loc in symbol.Locations)
            {
                if (!loc.IsInSource)
                {
                    continue;
                }

                var span = loc.GetLineSpan();
                props["file"] = loc.SourceTree?.FilePath;
                props["start_line"] = span.StartLinePosition.Line + 1;
                props["end_line"] = span.EndLinePosition.Line + 1;
                break;
            }
        }

        public static void AddSource(this IDictionary<string, object?> props, string? filePath, int startLine, int? endLine = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            props["file"] = filePath;
            props["start_line"] = startLine;
            props["end_line"] = endLine ?? startLine;
        }

        public static void AddSource(this IDictionary<string, object?> props, string? filePath, GraphSpan? span)
        {
            if (span is null)
            {
                return;
            }

            props.AddSource(filePath, span.StartLine, span.EndLine);
        }
    }
}
