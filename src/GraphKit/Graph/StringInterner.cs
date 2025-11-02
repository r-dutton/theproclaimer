using System;
using System.Collections.Generic;

namespace GraphKit.Graph
{
    /// <summary>Simple string interner to small integer IDs for hot fields.</summary>
    public sealed class StringInterner
    {
        private readonly Dictionary<string,int> _map;
        public StringInterner(IEqualityComparer<string>? cmp = null) => _map = new Dictionary<string,int>(cmp ?? StringComparer.Ordinal);
        public int Id(string? s)
        {
            s ??= string.Empty;
            if (_map.TryGetValue(s, out var id)) return id;
            id = _map.Count;
            _map[s] = id;
            return id;
        }
    }
}
