# Repository Guidelines

## Project Structure & Module Organization
The root solution `theproclaimer.sln` composes the libraries in `src/`. `FlowGrep` exposes the CLI entry point for graph generation, while `GraphKit` holds the analyzers and renderers that other projects depend on. The `Sample.*` folders are lightweight reference apps that demonstrate how to embed GraphKit in web, data, and messaging hosts. Documentation lives under `docs/`, and ephemeral build artifacts land in the various `out-*` directories—avoid checking those in. Tests belong in `tests/`, mirroring the source layout (e.g., `src/GraphKit` ↔ `tests/GraphKit.Tests`).

## Build, Test, and Development Commands
Run `dotnet restore theproclaimer.sln` after pulling new dependencies. Build locally with `dotnet build theproclaimer.sln -c Debug`, or swap `Release` when preparing production artifacts. Execute the suite using `dotnet test theproclaimer.sln --logger \"trx\"`, which collects MSTest/xUnit-compatible results under `TestResults/`. To exercise the CLI end-to-end, use `dotnet run --project src/FlowGrep/FlowGrep.csproj -- --workspace <path> --flow <pattern>`.

## Coding Style & Naming Conventions
Target framework is `net8.0` with nullable reference types enabled; prefer idiomatic modern C# features (pattern matching, `Span`, `await foreach`) where they sharpen clarity. Indent with four spaces and keep lines under ~120 characters. Follow `PascalCase` for types, `camelCase` for locals/parameters, and `SCREAMING_SNAKE_CASE` only for constants that are publicly shared. When adding analyzers or formatters, wire them through `Directory.Build.props`; until then, run `dotnet format` before committing to enforce SDK defaults.

## Testing Guidelines
House new test projects under `tests/<Project>.Tests` and align namespaces with the corresponding source module. Favor fast unit coverage of `GraphKit` primitives, and isolate sample-app integration checks behind the CLI where possible. Name test classes after the type under test (e.g., `FlowBuilderTests`) and methods with the `Method_Scenario_ExpectedResult` pattern. Capture edge cases discovered in production as regression tests before merging fixes.

## Commit & Pull Request Guidelines
Adopt conventional commits (`type(scope): summary`) to stay consistent with the current history—for example, `feat(graph): add flow pruning heuristics`. Reference issue numbers in the body when applicable, and include short reproduction steps or CLI invocations that validate the change. Pull requests should describe the problem, the solution, and any follow-up tasks; attach output snippets or screenshots from `FlowGrep` when they materially prove the behavior.

## Configuration Notes
`flow.workspace.json` and `flow.map.json` track generated flow metadata; update or regenerate them via `FlowGrep` when changing pipeline structure. Treat `Directory.Build.targets` and `Directory.Build.props` as the canonical location for shared MSBuild configuration—mirror any project-specific overrides back into those files to avoid drift.
