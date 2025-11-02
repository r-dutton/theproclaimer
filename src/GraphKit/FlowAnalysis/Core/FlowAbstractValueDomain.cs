namespace GraphKit.FlowAnalysis.Core
{
    public sealed class FlowAbstractValueDomain
    {
        public FlowAbstractValue Merge(FlowAbstractValue a, FlowAbstractValue b)
            => (a.IsUnknown || b.IsUnknown) ? FlowAbstractValue.Unknown : FlowAbstractValue.None;
        public bool Equals(FlowAbstractValue a, FlowAbstractValue b) => a.Equals(b);
    }
}
