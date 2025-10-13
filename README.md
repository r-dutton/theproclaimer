# The Proclaimer

## Overview
The Proclaimer repository bundles a Roslyn-based analyzer library (`GraphKit`) and a
small CLI (`FlowGrep`) that turn one or more .NET solutions into a machine-readable
graph and human-friendly flow documentation. The analyzer walks every C# project
listed in the workspace configuration, emits typed graph nodes for controllers,
CQRS requests/handlers, repositories, DTOs, AutoMapper profiles, HTTP clients,
validators, MediatR notifications, background services, Service Bus
publishers, configuration options, and cache dependencies, and links them together with rich edges that capture how
features interact across the codebase.【F:src/GraphKit/Analyzers/ProjectAnalyzer.Core.cs†L10-L111】【F:src/GraphKit/Analyzers/ProjectAnalyzer.Notifications.cs†L1-L230】【F:src/GraphKit/Analyzers/ProjectAnalyzer.BackgroundServices.cs†L1-L122】【F:src/GraphKit/Analyzers/ProjectAnalyzer.Messaging.cs†L1-L120】【F:src/GraphKit/Analyzers/ProjectAnalyzer.Caching.cs†L1-L104】【F:src/GraphKit/Analyzers/ProjectAnalyzer.Options.cs†L1-L147】

`GraphGenerator` orchestrates the workspace load, Roslyn analysis, and final
serialization, so calling code only needs to provide the workspace root and an
output directory.【F:src/GraphKit/GraphGenerator.cs†L8-L29】 The generated
`GraphDocument` can then be post-processed by helpers such as `FlowBuilder` to
produce higher-level narratives for HTTP flows and CQRS pipelines.【F:src/GraphKit/Outputs/FlowBuilder.cs†L9-L200】

## Repository layout
- `src/GraphKit/` – analyzer library plus graph/flow output helpers.
- `src/FlowGrep/` – console app for running the analyzer and querying the
  resulting graph.【F:src/FlowGrep/Program.cs†L8-L89】
- `src/Sample.*` – miniature applications that act as demonstration input for the
  analyzer and exercise the various detectors.【F:theproclaimer.sln†L7-L58】
- `flow.workspace.json` – declares which solutions belong to the workspace and
  optionally describes service endpoints for each logical system.【F:flow.workspace.json†L1-L20】
- `flow.map.json` – maps logical services to base URLs and binds outbound HTTP
  clients to their target services to enrich the generated flows.【F:flow.map.json†L1-L20】

## Configuration
Point the tooling at a workspace root that contains either a
`flow.workspace.json` file or discoverable `.sln` files. The workspace loader will
parse the configuration, resolve absolute project paths, and enumerate all C#
sources for analysis. When the CLI receives explicit `--solution` arguments the
loader prioritizes those paths so you can slice the workspace per run.【F:src/GraphKit/Workspace/WorkspaceLoader.cs†L16-L92】

When `flow.map.json` is present the analyzer also imports service metadata and
pre-configured HTTP client bindings so that downstream flow documentation can note
which logical service a client call targets.【F:src/GraphKit/Analyzers/ProjectAnalyzer.DependencyInjection.cs†L17-L111】

Option classes that expose a `SectionName` constant (for example,
`ReportRetentionOptions`) are automatically matched to `appsettings*.json`
sections so cache expiration policies, retention windows, and other settings show
up in the generated graph and flow documentation.【F:src/GraphKit/Analyzers/ProjectAnalyzer.Options.cs†L1-L147】【F:src/Sample.App/Options/ReportRetentionOptions.cs†L1-L11】

## Running the analyzer
1. Install the .NET 8.0 SDK (GraphKit and FlowGrep both target `net8.0`).【F:src/GraphKit/GraphKit.csproj†L4-L13】【F:src/FlowGrep/FlowGrep.csproj†L4-L12】
2. Restore dependencies: `dotnet restore theproclaimer.sln`.
3. Generate the graph and reports. Solutions are discovered automatically, but
   you can target an explicit set to accelerate large workspaces:
   ```bash
   dotnet run --project src/FlowGrep -- \
     --workspace . \
     --write-out out \
     --solution src/Sample.SolutionA.sln \
     --solution src/Sample.SolutionB.sln
   ```
   FlowGrep accepts optional filters for quick exploration:
   - `--text "Search term"` and/or `--tags tag1,tag2` to filter nodes and dump
     results either as markdown (default) or JSON via `--format json`.
   - `--flow Sample.Web.*` (or comma-separated `--flows`) uses glob matching so
     you can print only the flows you care about, while `--flow "*"` streams
     every discovered controller flow to stdout.【F:src/FlowGrep/Program.cs†L8-L96】【F:src/GraphKit/Outputs/FlowFilter.cs†L8-L64】

## Generated artifacts
Running FlowGrep populates the output directory with several useful assets:
- `graph.json` – serialized `GraphDocument` containing every node and edge with
  evidence metadata for further processing.【F:src/GraphKit/Outputs/GraphOutputWriter.cs†L22-L42】
- `graph.cypher` – Neo4j-friendly script that loads the same nodes and edges into
  a property graph database for ad-hoc querying.【F:src/GraphKit/Outputs/GraphOutputWriter.cs†L44-L61】
- `GRAPH.md` – Markdown summary with node and edge counts grouped by type for a
  fast health check.【F:src/GraphKit/Outputs/GraphOutputWriter.cs†L63-L85】
- `flows/` – aggregated `controllers.all.md` plus one Markdown file per
  controller action so teams can diff individual endpoints or stream all flows in
  bulk. Notification fan-out, background service hops, cache interactions, and
  configuration option dependencies are included alongside HTTP and CQRS steps to
  capture asynchronous workflows.【F:src/GraphKit/Outputs/GraphOutputWriter.cs†L24-L187】【F:src/GraphKit/Outputs/FlowBuilder.cs†L1-L360】
- `evals/` – lightweight evaluation payloads that report controller coverage and
  graph size for observability tooling or regression tests.【F:src/GraphKit/Outputs/GraphOutputWriter.cs†L121-L150】
- `VERSION.txt` – captures the analyzer version and Git commit hash to make
  artifacts reproducible.【F:src/GraphKit/Outputs/GraphOutputWriter.cs†L121-L145】

## Verifying the tooling
The repository already includes an `out/` directory produced from the bundled
sample solutions so you can inspect the expected artifacts without rerunning the
analyzer. Re-running the command above regenerates the files, allowing you to
confirm that graph counts and flow narratives stay in sync with code changes. The
`GraphKit.Tests` project provides fast regression coverage for the new flow
filtering and builder behavior: `dotnet test theproclaimer.sln`.【F:tests/GraphKit.Tests/GraphKit.Tests.csproj†L1-L20】【F:tests/GraphKit.Tests/FlowFilterTests.cs†L1-L52】

## Known gaps and next steps
- Flow files are currently emitted per controller action. Adding support for
  grouping by service or route prefix would make larger solutions easier to
  navigate.【F:src/GraphKit/Outputs/GraphOutputWriter.cs†L84-L187】
- FlowGrep writes results to stdout when `--flow` patterns match. Surfacing the
  same wildcard support in the Markdown outputs (e.g. generating only requested
  files) would keep artifact churn low for incremental runs.【F:src/FlowGrep/Program.cs†L8-L96】
