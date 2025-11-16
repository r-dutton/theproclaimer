using System;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Http
{
    /// <summary>
    /// Central helper for reconstructing HTTP verb and route strings from Roslyn operations
    /// and flow value-content, so that controllers, services, and HttpClient analyzers all
    /// share the same behavior.
    /// </summary>
    public static class RouteCanonicalizer
    {
        private static readonly string[] KnownHttpVerbs = { "GET", "POST", "PUT", "DELETE", "PATCH", "HEAD", "OPTIONS" };

        public static string CanonVerb(string? verb)
        {
            var normalized = NormalizeVerb(verb);
            return string.IsNullOrWhiteSpace(normalized) ? "GET" : normalized!;
        }

        /// <summary>
        /// Canonicalize a route for matching: trim, ensure leading slash, drop trailing slash,
        /// and strip query string. External absolute URLs are left as-is.
        /// </summary>
        public static string CanonRoute(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                return "/";
            }

            var s = route.Trim();

            // Separate path from query; we keep only path for canonical comparison.
            var q = s.IndexOf('?', StringComparison.Ordinal);
            if (q >= 0)
            {
                s = s[..q];
            }

            if (s.Contains("://", StringComparison.Ordinal))
            {
                // For absolute URLs, leave as-is after trimming.
                return s;
            }

            if (!s.StartsWith("/", StringComparison.Ordinal))
            {
                s = "/" + s;
            }

            if (s.Length > 1 && s.EndsWith("/", StringComparison.Ordinal))
            {
                s = s.TrimEnd('/');
            }

            return s;
        }

        /// <summary>
        /// Try to reconstruct verb and route for an HTTP-shaped invocation.
        /// Verb is inferred from the method name; route is obtained from value-content
        /// of the "route" argument (requestUri/url/path/etc) with conservative fallbacks.
        /// </summary>
        public static bool TryReconstruct(
            IInvocationOperation invocation,
            FlowValueContentFacade valueContent,
            out string verb,
            out string route)
        {
            if (invocation is null)
            {
                verb = string.Empty;
                route = string.Empty;
                return false;
            }

            verb = CanonVerb(invocation.TargetMethod?.Name);
            route = TryRouteFromArguments(invocation, valueContent);

            if (string.IsNullOrWhiteSpace(route))
            {
                return false;
            }
            return true;
        }

        private static string? TryRouteFromArguments(IInvocationOperation invocation, FlowValueContentFacade valueContent)
        {
            // Prefer parameters that look like explicit route arguments.
            foreach (var argument in invocation.Arguments)
            {
                if (!IsRouteParameter(argument.Parameter))
                {
                    continue;
                }

                var value = TryGetString(argument.Value, valueContent);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            // Fall back to the first argument when no explicit route parameter is present.
            if (invocation.Arguments.Length > 0)
            {
                var value = TryGetString(invocation.Arguments[0].Value, valueContent);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }

        private static string? TryGetString(IOperation? operation, FlowValueContentFacade valueContent)
        {
            if (operation is null)
            {
                return null;
            }

            if (operation.ConstantValue is { HasValue: true, Value: string literal })
            {
                return literal;
            }

            if (operation is IConversionOperation conversion)
            {
                var converted = TryGetString(conversion.Operand, valueContent);
                if (!string.IsNullOrWhiteSpace(converted))
                {
                    return converted;
                }
            }

            var description = valueContent.DescribeStringValue(operation);
            var literal = description.FirstNonEmptyLiteralOrDefault;
            if (!string.IsNullOrWhiteSpace(literal))
            {
                return literal;
            }

            foreach (var candidate in valueContent.EnumerateContentCandidates(operation))
            {
                var literalCandidate = candidate.TryGetLiteralText();
                if (!string.IsNullOrWhiteSpace(literalCandidate))
                {
                    return literalCandidate;
                }

                var placeholder = candidate.ToDisplayString();
                if (!string.IsNullOrWhiteSpace(placeholder))
                {
                    return placeholder;
                }
            }

            return null;
        }

        private static bool IsRouteParameter(IParameterSymbol? parameter)
        {
            if (parameter is null)
            {
                return false;
            }

            var name = parameter.Name;
            return name.Equals("requestUri", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("uri", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("url", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("endpoint", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("path", StringComparison.OrdinalIgnoreCase);
        }

        private static string? NormalizeVerb(string? methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return null;
            }

            var trimmed = methodName.Trim();
            if (trimmed.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
            {
                trimmed = trimmed[..^5];
            }

            foreach (var verb in KnownHttpVerbs)
            {
                if (trimmed.StartsWith(verb, StringComparison.OrdinalIgnoreCase))
                {
                    return verb;
                }
            }

            return null;
        }
    }
}
