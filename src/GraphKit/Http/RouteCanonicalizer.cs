using System;
using GraphKit.FlowAnalysis.Dependencies;
using Microsoft.CodeAnalysis.Operations;

namespace GraphKit.Http
{
    public static class RouteCanonicalizer
    {
        public static string CanonVerb(string v) => (v ?? "").Trim().ToUpperInvariant();

        public static string CanonRoute(string r)
        {
            if (string.IsNullOrWhiteSpace(r)) return "/";
            var s = r.Trim();
            if (!s.StartsWith("/")) s = "/" + s;
            if (s.Length > 1 && s.EndsWith("/")) s = s.TrimEnd('/');
            return s;
        }

        public static bool TryReconstruct(IInvocationOperation inv, FlowValueContentFacade vcf,
            out string verb, out string route)
        {
            verb = InferVerb(inv);
            route = TryValue(vcf, inv) ?? TryHeuristic(inv);
            if (string.IsNullOrEmpty(verb) || string.IsNullOrEmpty(route)) return false;
            verb = CanonVerb(verb);
            route = CanonRoute(route);
            return true;
        }

        private static string InferVerb(IInvocationOperation inv) =>
            inv.TargetMethod.Name switch
            {
                "GetAsync" => "GET",
                "PostAsync" => "POST",
                "PutAsync" => "PUT",
                "DeleteAsync" => "DELETE",
                _ => "GET"
            };

        private static string? TryValue(FlowValueContentFacade vcf, IInvocationOperation inv)
        {
            if (inv.Arguments.Length == 0) return null;
            return vcf.TryGetStringValue(inv.Arguments[0].Value);
        }

        private static string? TryHeuristic(IInvocationOperation inv)
        {
            // TODO: reuse your existing concatenation/interpolation walkers here
            return null;
        }
    }
}
