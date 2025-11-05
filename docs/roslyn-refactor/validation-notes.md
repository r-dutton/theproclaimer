# Migration Validation Notes

- 2024-11-03 – Service analyzer migrated to `MethodFlowContext`. Ran `dotnet run --project src/FlowGrep/FlowGrep.csproj --workspace .. --solutions Cirrus/src/Cirrus.sln --flows "DELETE /api/accounting/assets/assets" --quiet` and observed narrative output unchanged for the inspected flow.
- 2024-11-03 – CQRS analyzer now uses `MethodFlowContext`. No dedicated snapshots existed; manual inspection confirms handler graphs still render with identical structure. Follow-up: add targeted snapshot coverage once analyzer migrations complete.
- 2024-11-03 – Controller, notification, and domain-event analyzers switched to the shared context. Re-ran the same targeted flow to verify narratives render as expected; no regressions observed.
- 2024-11-03 – Ran full `FlowGrep` (`dotnet run --project src/FlowGrep/FlowGrep.csproj --workspace .. --quiet`) after migrations. Cache reuse confirmed; full narrative regenerated without errors. Pending: deeper diffing of `out/flow.md` once we intentionally accept structural changes.
- 2024-11-05 – Integrated Roslyn `PointsToAnalysis` and tightened service scoping. Verified new unit coverage (`FlowPointsToFacadeTests`, `ServiceScopeTests`) and spot-checked the `DELETE /api/accounting/assets/assets` flow: cross-solution `Dataverse` services no longer surface while nested local dependencies expand recursively.
