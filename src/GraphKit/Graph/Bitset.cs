namespace GraphKit.Graph;

/// <summary>
/// Minimal uint[] bitset helpers and SCC reachability DP.
/// </summary>
public static class Bitset
{
    public static uint[][] BuildReach(SccIndex scc)
    {
        int W = (scc.Count + 31) >> 5;
        var reach = new uint[scc.Count][];
        foreach (var v in scc.Topo)
        {
            var rv = reach[v] ??= new uint[W];
            Set(rv, v);
            var outs = scc.Dag[v];
            if (outs is null) continue;
            foreach (var u in outs)
            {
                var ru = reach[u] ??= new uint[W];
                OrInto(rv, ru);
            }
        }
        return reach;
    }

    public static void OrInto(uint[] dst, uint[] src)
    { for (int i=0;i<dst.Length;i++) dst[i] |= src[i]; }

    public static void Set(uint[] a, int bit)
    { a[bit >> 5] |= (1u << (bit & 31)); }

    public static bool Has(uint[] a, int bit)
    { return (a[bit >> 5] & (1u << (bit & 31))) != 0; }
}
