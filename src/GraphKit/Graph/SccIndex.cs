using System.Collections.Generic;

namespace GraphKit.Graph;

/// <summary>
/// Strongly Connected Components condensation and DAG/topo order.
/// </summary>
public sealed class SccIndex
{
    public readonly int[] SccOf;        // nodeIdx -> sccId
    public readonly List<int>[] Dag;    // sccId  -> [sccId...]
    public readonly List<int>[] Members;// sccId  -> [nodeIdx...]
    public readonly int Count;
    public readonly int[] Topo;         // topo order of DAG

    private SccIndex(int[] sccOf, List<int>[] dag, List<int>[] members, int count, int[] topo)
        => (SccOf, Dag, Members, Count, Topo) = (sccOf, dag, members, count, topo);

    public static SccIndex Build(GraphIndex g)
    {
        var n = g.Nodes.Length;
        var sccOf = Tarjan(g.Adj);
        int count = 0; foreach (var id in sccOf) if (id > count) count = id; count++;

        var members = new List<int>[count];
        for (int i = 0; i < n; i++) (members[sccOf[i]] ??= new()).Add(i);

        var dag = new List<int>[count];
        var seen = new HashSet<long>();
        for (int v = 0; v < n; v++)
        {
            var sv = sccOf[v];
            var outs = g.Adj[v];
            if (outs is null) continue;
            foreach (var u in outs)
            {
                var su = sccOf[u];
                if (sv == su) continue;
                long key = ((long)sv << 32) | (uint)su;
                if (seen.Add(key)) (dag[sv] ??= new()).Add(su);
            }
        }

        var topo = TopoSort(dag, count);
        return new SccIndex(sccOf, dag, members, count, topo);
    }

    private static int[] Tarjan(List<int>[] adj)
    {
        int n = adj?.Length ?? 0, idx = 0, comp = 0;
        var ids = new int[n]; for (int i=0;i<n;i++) ids[i] = -1;
        var low = new int[n];
        var st = new Stack<int>(n);
        var on = new bool[n];
        var scc = new int[n];

        void Dfs(int at)
        {
            st.Push(at); on[at] = true;
            ids[at] = low[at] = idx++;
            var outs = adj[at];
            if (outs != null)
            {
                foreach (var to in outs)
                {
                    if (ids[to] == -1) { Dfs(to); low[at] = low[at] < low[to] ? low[at] : low[to]; }
                    else if (on[to])    { low[at] = low[at] < ids[to] ? low[at] : ids[to]; }
                }
            }
            if (ids[at] == low[at])
            {
                while (true)
                {
                    var v = st.Pop(); on[v] = false;
                    scc[v] = comp;
                    if (v == at) break;
                }
                comp++;
            }
        }

        for (int i=0;i<n;i++) if (ids[i] == -1) Dfs(i);
        return scc;
    }

    private static int[] TopoSort(List<int>[] dag, int count)
    {
        var indeg = new int[count];
        for (int v=0; v<count; v++)
            if (dag[v] != null) foreach (var u in dag[v]) indeg[u]++;

        var q = new Queue<int>();
        for (int v=0; v<count; v++) if (indeg[v]==0) q.Enqueue(v);

        var order = new List<int>(count);
        while (q.Count > 0)
        {
            var v = q.Dequeue(); order.Add(v);
            var outs = dag[v];
            if (outs is null) continue;
            foreach (var u in outs) if (--indeg[u]==0) q.Enqueue(u);
        }
        return order.ToArray();
    }
}
