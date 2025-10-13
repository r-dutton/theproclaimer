using System.Security.Cryptography;
using System.Text;

namespace GraphKit.Graph;

internal static class StableId
{
    public static string For(string type, string fqdn, string assembly, string symbolId)
    {
        using var sha = SHA256.Create();
        var payload = Encoding.UTF8.GetBytes($"{type}|{fqdn}|{assembly}|{symbolId}");
        var hash = sha.ComputeHash(payload);
        var base32 = Convert.ToHexString(hash).ToLowerInvariant();
        return $"node::{base32}";
    }
}
