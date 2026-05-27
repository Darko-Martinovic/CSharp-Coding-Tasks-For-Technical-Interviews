# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

An interview question bank: **30 self-contained C# debugging/fixing tasks** aimed at L4/advanced
developers. Each task is designed to be solved in **under 10 minutes** and deliberately contains
**multiple distinct bugs or steps** (not a single fix). This is teaching material, not an application —
there is no shared library, no cross-project references, and each project's `Program.cs` stands alone.

## Structure

Each of the 30 topics ships as **two runnable console projects** (60 projects total), aggregated by
`CSharpCodingTasks.sln`:

- `NN-Topic-Task` — a `// TASK NN` header comment block (difficulty, target time, goal, "things to
  scrutinise" hints, expected output) above starting code that either **runs and misbehaves** or
  **fails to compile by design**.
- `NN-Topic-Solution` — the worked fix, with **every change annotated `// FIX #n`** referencing the
  specific bug it addresses.

`README.md` indexes all 30 (topic / compile-vs-behavioral / focus) — keep it in sync when adding tasks.

## Build & run

All projects target **`net10.0`**. Run a single project (the normal workflow):

```pwsh
dotnet run --project 01-Disposing-Task       # observe the bug / compile failure
dotnet run --project 01-Disposing-Solution   # see the corrected output
```

**Do not rely on building the whole solution.** Six tasks are **compile-by-design** (02, 12, 13, 19,
25, 27) — making them compile *is* the exercise, so `dotnet build CSharpCodingTasks.sln` will report
failures for those six `-Task` projects. That is expected, not a regression. Build and run projects
**individually**.

## Conventions when adding or editing tasks

- Project settings: console `Exe`, `net10.0`, with `ImplicitUsings` and `Nullable` **disabled** — except
  task 13, which sets `<Nullable>enable</Nullable>` + `<WarningsAsErrors>nullable</WarningsAsErrors>` so
  nullability issues are hard compile errors. Each project sets its own `RootNamespace`/`AssemblyName`.
- Behavioral tasks must **misbehave deterministically** — verify by running. Avoid flaky timing/concurrency
  (e.g. task 10 uses 8 threads × 1M increments rather than `Parallel.For` to reliably lose updates).
- The master solution is a **classic `.sln`** by explicit choice. Note the .NET 10 SDK defaults
  `dotnet new sln` to `.slnx`; use `--format sln` to keep the classic format.
- Solutions annotate every change with `// FIX #n`; tasks lead with the `// TASK NN` header block.
