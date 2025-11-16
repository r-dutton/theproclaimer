using System;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.FlowAnalysis.Interprocedural;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Xunit;

namespace GraphKit.Tests.FlowAnalysis;

public sealed class FlowValueContentFacadeTests
{
    [Fact]
    public void Constructor_FromPointsToFacade_ReusesConfigurationAndPredicate()
    {
        var settings = new InterproceduralSettings(
            InterproceduralAnalysisKind.ContextSensitive,
            maxCallChainLength: 2,
            maxLambdaOrLocalFunctionDepth: 1);
        var pointsTo = new FlowPointsToFacade(settings, static _ => true);

        var valueContent = new FlowValueContentFacade(pointsTo);

        Assert.Equal(settings, valueContent.Settings);
        Assert.Same(pointsTo.InterproceduralPredicate, valueContent.InterproceduralPredicate);
    }

    [Fact]
    public void Constructor_FromPointsToFacade_ThrowsForNullFacade()
    {
        Assert.Throws<ArgumentNullException>(() => new FlowValueContentFacade(pointsToFacade: null!));
    }
}
