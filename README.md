# The Proclaimer

The Proclaimer is a documentation generator for large .NET codebases. It walks C# solutions with a Roslyn-based analyzer, turns the results into a rich dependency graph, and then builds readable flow summaries that explain how HTTP endpoints, CQRS handlers, background services, caches, and configuration settings interact. The tool exists to close the gap between sprawling enterprise code and the humans (or agents) that need to understand it quickly.

## Why it exists

Teams working on multi-solution .NET systems often rely on tribal knowledge or outdated wiki pages to reason about data flow. The Proclaimer keeps that knowledge current by inspecting the source directly. Every run produces:

- A machine-friendly graph describing controllers, handlers, repositories, options, messaging clients, and all their relationships.
- Human-friendly flow narratives that explain the full journey of a request, including asynchronous fan-out and configuration dependencies.
- Lightweight evaluation artifacts that track coverage so you can spot missing documentation.

Because the analyzer is code-first, the outputs stay in sync with the repository and can be regenerated as part of CI pipelines or quality gates.

## What makes it different

- **Full workspace analysis**: Point it at a workspace file or a root directory, and it will ingest every referenced solution without you needing to maintain per-project scripts.
- **Typed graph model**: The graph captures evidence for each relationship, so downstream tools (agents, visualizers, graph databases) can trust the links.
- **Narrative flows**: Instead of dumping raw edges, The Proclaimer assembles end-to-end stories that connect HTTP, CQRS, background jobs, caches, and configuration in a single document.
- **Agent-ready outputs**: Markdown summaries and JSON payloads are easy to ingest in autonomous workflows or conversational assistants.

## Getting started

1. Install the .NET 8 SDK.
2. Restore the workspace dependencies:
   ```bash
   dotnet restore theproclaimer.sln
   ```
3. Run the analyzer with the FlowGrep CLI:
   ```bash
   dotnet run --project src/FlowGrep -- \
     --workspace . \
     --write-out out \
     --solution src/Sample.SolutionA.sln \
     --solution src/Sample.SolutionB.sln
   ```
4. Explore the generated `out/` folder. You'll find the graph data (`graph.json`, `graph.cypher`), Markdown flow reports, evaluation summaries, and a version stamp for reproducibility.

You can scope the run by passing additional flags:

- `--flow Pattern` (supports wildcards) to emit only matching flows.
- `--text "Search term"` or `--tags a,b,c` to filter node dumps.
- `--format json` to receive JSON instead of Markdown for the flow output.

## Example output

Below is a condensed excerpt from four flows generated across two sample solutions. Each entry shows how the narrative stitches together HTTP calls, queue messages, background processing, and cache usage:

```
# Sample.Web.ReportsController.Get
1. HTTP GET /reports/{id} (Sample.Web)
2. Loads ReportByIdQuery -> ReportByIdHandler (Sample.App)
3. Handler queries ReportRepository.GetById
4. Maps result via ReportProfile to ReportDto
5. Publishes ReportViewedNotification -> ReportViewedHandler
6. Handler enqueues message on bus: reporting.events
7. Flow ends with HTTP 200 and ReportDto payload

# Sample.Web.ReportsController.Post
1. HTTP POST /reports (Sample.Web)
2. Validates payload with CreateReportValidator
3. Sends CreateReportCommand -> CreateReportHandler
4. Handler writes via ReportRepository.Create
5. Invalidates cache key reports:list
6. Returns HTTP 201 with location header

# Sample.Tasks.ReportDigestWorker.Execute
1. Background service starts on 5-minute timer
2. Calls DigestQuery -> DigestHandler
3. Handler reads cache key reports:list (hit)
4. Sends SendDigestEmailCommand -> EmailHandler
5. EmailHandler calls SendGridClient.Post
6. Logs outcome via AuditLogger

# Sample.Web.AnalyticsController.Get
1. HTTP GET /analytics/daily (Sample.Web)
2. Reads cache key analytics:daily (miss)
3. Falls back to AnalyticsQuery -> AnalyticsHandler
4. Handler calls AnalyticsApiClient.GetDailyStats (Sample.Infra)
5. Response cached for 15 minutes
6. Returns HTTP 200 with AnalyticsDto
```

The same run also emits the raw graph data, so a Neo4j instance or another agent can traverse the nodes programmatically while humans keep the readable Markdown for quick reference.

## Using The Proclaimer inside agent workflows

Because the CLI is deterministic and scriptable, it fits naturally into agent toolboxes. Here are a few ways to describe it when registering tools for an agent framework:

- **Name**: `proclaimer.run`
- **Description**: "Analyze .NET solutions in the current workspace and emit flow documentation plus a dependency graph."
- **Arguments**: `workspace` (path), optional `solutions` (list), `output` (directory), optional filters (`flows`, `text`, `tags`, `format`).

Example tool declaration in JSON:

```json
{
  "name": "proclaimer.run",
  "description": "Generate graph and flow documentation for .NET solutions using FlowGrep.",
  "parameters": {
    "type": "object",
    "properties": {
      "workspace": { "type": "string", "description": "Workspace root" },
      "output": { "type": "string", "description": "Directory for generated artifacts" },
      "solutions": {
        "type": "array",
        "items": { "type": "string" },
        "description": "Optional list of .sln paths to restrict analysis"
      },
      "flows": {
        "type": "array",
        "items": { "type": "string" },
        "description": "Glob patterns for flow selection"
      }
    },
    "required": ["workspace", "output"]
  }
}
```

An agent can call the tool after cloning a repository to regenerate documentation, inspect `flows/controllers.all.md` to reason about a feature, or pipe `graph.json` into its own reasoning modules. Because the analyzer returns consistent artifacts for every run, agents can safely diff results over time to detect regressions or missing coverage.

## Next steps

The current sample output ships in the `out/` directory if you want to explore without running the tool yourself. To expand coverage, point the workspace configuration at additional solutions or enrich `flow.map.json` with more service metadata. Contributions that add new analyzers (for example, gRPC or SignalR support) or alternative renderers are welcome.
