namespace GraphKit.FlowAnalysis.Interprocedural
{
    public sealed record FlowInterproceduralConfig(int MaxMethodDepth = 4, int MaxLambdaDepth = 2);
}
