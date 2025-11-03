namespace Microsoft.CodeAnalysis.FlowAnalysis.DataFlow
{
    public enum InterproceduralAnalysisKind
    {
        None = 0,
        ContextSensitive = 1,
        ContextInsensitive = 2
    }

    public sealed class InterproceduralAnalysisConfiguration
    {
        public InterproceduralAnalysisKind InterproceduralAnalysisKind { get; }

        public int MaxInterproceduralCallChainLength { get; }

        public int MaxInterproceduralLambdaOrLocalFunctionCallChainLength { get; }

        private InterproceduralAnalysisConfiguration(
            InterproceduralAnalysisKind kind,
            int callChainLength,
            int lambdaOrLocalFunctionLength)
        {
            InterproceduralAnalysisKind = kind;
            MaxInterproceduralCallChainLength = callChainLength;
            MaxInterproceduralLambdaOrLocalFunctionCallChainLength = lambdaOrLocalFunctionLength;
        }

        public static InterproceduralAnalysisConfiguration Create(
            InterproceduralAnalysisKind interproceduralAnalysisKind,
            int maxInterproceduralCallChainLength = 0,
            int maxInterproceduralLambdaOrLocalFunctionCallChainLength = 0)
            => new(
                interproceduralAnalysisKind,
                maxInterproceduralCallChainLength,
                maxInterproceduralLambdaOrLocalFunctionCallChainLength);
    }
}
