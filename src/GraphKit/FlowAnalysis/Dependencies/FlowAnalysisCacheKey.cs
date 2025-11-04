using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace GraphKit.FlowAnalysis.Dependencies;

internal readonly struct FlowAnalysisCacheKey : IEquatable<FlowAnalysisCacheKey>
{
    public FlowAnalysisCacheKey(SyntaxTree tree, TextSpan span)
    {
        Tree = tree ?? throw new ArgumentNullException(nameof(tree));
        Span = span;
    }

    public SyntaxTree Tree { get; }

    public TextSpan Span { get; }

    public bool Equals(FlowAnalysisCacheKey other)
        => ReferenceEquals(Tree, other.Tree) && Span.Equals(other.Span);

    public override bool Equals(object? obj)
        => obj is FlowAnalysisCacheKey other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(Tree, Span.Start, Span.Length);
}
