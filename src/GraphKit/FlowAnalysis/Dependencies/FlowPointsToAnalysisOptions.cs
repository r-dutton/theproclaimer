using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;

namespace GraphKit.FlowAnalysis.Dependencies;

public enum FlowPointsToPrecision
{
    Fast,
    HighPrecision
}

public readonly record struct FlowPointsToAnalysisOptions(
    PointsToAnalysisKind PointsToAnalysisKind,
    bool PerformCopyAnalysis,
    bool PessimisticAnalysis,
    bool ExceptionPathsAnalysis)
{
    public static FlowPointsToAnalysisOptions Fast { get; } = new(
        PointsToAnalysisKind.PartialWithoutTrackingFieldsAndProperties,
        performCopyAnalysis: false,
        pessimisticAnalysis: false,
        exceptionPathsAnalysis: false);

    public static FlowPointsToAnalysisOptions HighPrecision { get; } = new(
        PointsToAnalysisKind.PartialWithTrackingFieldsAndProperties,
        performCopyAnalysis: true,
        pessimisticAnalysis: true,
        exceptionPathsAnalysis: true);
}
