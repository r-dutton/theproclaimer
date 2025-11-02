using System;

namespace GraphKit.FlowAnalysis.Core
{
    public readonly struct FlowAbstractValue : IEquatable<FlowAbstractValue>
    {
        public static readonly FlowAbstractValue Unknown = new(true);
        public static readonly FlowAbstractValue None = new(false);
        public bool IsUnknown { get; }
        private FlowAbstractValue(bool isUnknown) => IsUnknown = isUnknown;
        public bool Equals(FlowAbstractValue other) => IsUnknown == other.IsUnknown;
        public override bool Equals(object? obj) => obj is FlowAbstractValue v && Equals(v);
        public override int GetHashCode() => IsUnknown ? 1 : 0;
    }
}
