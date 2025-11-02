## Quick Orientation
- `src/GraphKit` is the Roslyn-based analyzer that walks every .sln listed in `flow.workspace.json`, builds nodes/edges, then hands off to the output layer.
- `src/FlowGrep` is a CLI wrapper that regenerates the graph before applying flow/text/tag filters; always run it from repo root so relative paths resolve.
- `src/Sample.*` projects are the reference domain the analyzer targets (MediatR, EF Core, Service Bus, HttpClient); keep them compiling because regression tests depend on their shapes.
## Workspace & Config
- `flow.workspace.json` limits solution enumeration and maps service assemblies/base addresses; extend it when adding services so FlowBuilder can resolve remote callers.
- `flow.map.json` adds cross-service bindings (e.g., HttpClient to service name) that `ProjectAnalyzer.DependencyInjection` consumes; update both files when introducing new clients.
- `GraphKit.Workspace.WorkspaceLoader` falls back to scanning for .sln files if configs are missing, but we rely on the explicit lists for performance and deterministic ordering.
## Analyzer Mechanics
- `src/GraphKit/GraphGenerator.cs` orchestrates `WorkspaceLoader`, runs the partial `ProjectAnalyzer` passes, and logs `[graph]` progress; keep those Console traces for perf debugging.
- Partial files under `src/GraphKit/Analyzers` each cover a feature slice (Controllers, Http, Services, Mapping, etc.); add new domain logic by introducing another partial rather than bloating existing ones.
- Service discovery lives in `ProjectAnalyzer.DependencyInjection.cs`, capturing `Add*`/Autofac registrations and HttpClient base URLs; respect the helper methods for registering edges so downstream flows stay coherent.
- New node/edge types must go through `StableId.For` and reuse `CreateEvidence` helpers to keep graph IDs stable across runs.
- `ClientLinker.EmitClientUseCallEdges` stitches synthetic cross-solution edges; if you add a new client heuristic, ensure it feeds `_clientTargetServices` via `RegisterClientTargetService`.
## Graph Outputs & Flows
- `src/GraphKit/Outputs/GraphOutputWriter.cs` writes `out/graph.json`, `graph.cypher`, `GRAPH.md`, controller flow markdown, and `VERSION.txt`; add new artifacts there so FlowGrep users do not have to chain extra commands.
- Flow narratives come from `src/GraphKit/Outputs/FlowBuilder/*`; reuse `Append*` helpers and maintain `WriteOperationKinds`/`ReadOperationKinds` when introducing new edge kinds.
- `FlowWorkspaceIndex` (loaded twice: writer + CLI) resolves service hosts, so keep base URLs normalized (lowercase, trailing slash trimmed).
## CLI & Workflows
- Build everything with `dotnet build theproclaimer.sln`; GraphKit targets `net8.0` and the CLI shares that TFM.
- Typical graph run: `dotnet run --project src/FlowGrep/FlowGrep.csproj -- --workspace . --write-out out` (add `--solutions src/Sample.SolutionA.sln` for targeted runs).
- Flow drill-down: `dotnet run --project src/FlowGrep/FlowGrep.csproj -- --workspace . --flow Reports* --max-depth 6` to narrate specific controller paths; use `--text`/`--tags` for ad-hoc searches.
## Sample Domain & Patterns
- `src/Sample.Web/Program.cs` wires MediatR + FluentValidation + EF Core; the analyzer expects DI wiring via `AddMediatR`, `AddValidatorsFromAssemblyContaining`, and typed `AddHttpClient`.
- `Sample.App` handlers rely on IMemoryCache + IOptions (see `Handlers/GetReportHandler.cs`); cache/options edges show up only if those patterns stay intact.
- `Sample.Msg` wraps Azure Service Bus (`Publishing/ReportPublisher.cs`); publishing edges rely on the `PublishAsync` signature, so keep logging + envelope creation idiomatic.
- `Sample.Data` exposes the EF layer (`Infrastructure/ReportsDbContext.cs`); entity/table detection assumes configurations live in `Configurations/`.
## Extending Guidelines
- Prefer enhancing analyzers before post-processing: downstream writers assume node types like `endpoint.controller`, `app.repository`, `app.service` already exist.
- When flows look incomplete, inspect `out/flows/controllers.all.md` first instead of reworking analyzers; it is regenerated every CLI run.
- Keep new code ASCII and thread-safe; most analyzer state uses `ConcurrentDictionary`/`ConcurrentBag` because Roslyn runs in parallel.
