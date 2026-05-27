# C# Coding Tasks (L4 / advanced)

30 self-contained C# debugging/fixing tasks for advanced (L4) developers. Each
task is designed to be solved in **under 10 minutes** and contains **multiple
bugs or steps**.

Each task ships as **two runnable projects**:

- `NN-Topic-Task` — the task definition (a header comment) **plus** the starting
  code. It either runs and misbehaves, or fails to compile **by design**.
- `NN-Topic-Solution` — a worked solution with every change annotated `// FIX`.

All 60 projects target **`net10.0`** and are aggregated by `CSharpCodingTasks.sln`.

## Running

```pwsh
# Run a task (to observe the bug) or its solution (to see correct output):
dotnet run --project 01-Disposing-Task
dotnet run --project 01-Disposing-Solution
```

> The six **compile-by-design** tasks (02, 12, 13, 19, 25, 27) do **not** build —
> making them compile *is* the exercise. Their `-Solution` projects build and run.
> Building the whole `.sln` will therefore report failures for those six tasks;
> build/run projects individually.

## Task index

| # | Topic | Type | Focus |
|---|-------|------|-------|
| 01 | Disposing | behavioral | Dispose pattern: finalizer arg, SuppressFinalize, virtuality, leaked stream, double-dispose, use-after-dispose |
| 02 | Variance | **compile** | `out`/`in` covariance & contravariance; array-covariance runtime trap |
| 03 | Async Deadlock | behavioral | sync-over-async `.Result`, `async void`, AggregateException wrapping, async-all-the-way |
| 04 | Closures | behavioral | captured loop variable in delegates and parallel tasks |
| 05 | Deferred Execution | behavioral | multiple enumeration, captured query variable, enumerate-after-dispose |
| 06 | Equals & GetHashCode | behavioral | override both, null/type safety, hash-collection consistency |
| 07 | Struct Semantics | behavioral | mutable struct copies in List/foreach vs array element |
| 08 | Exception Handling | behavioral | `throw ex` vs `throw`, dropped inner exception, swallowed NRE |
| 09 | Event Leak | behavioral | missing unsubscribe in Dispose, un-removable lambda handler |
| 10 | Race Condition | behavioral | non-atomic `++`, `lock (new object())` |
| 11 | LINQ Correctness | behavioral | First/Single/Average throwing, Count>0 vs Any |
| 12 | Generic Constraints | **compile** | `class` / `IComparable<T>` / `new()` constraints |
| 13 | Nullable References | **compile** | CS8618/8602/8603 (warnings-as-errors), `required`, null handling |
| 14 | Boxing | behavioral | boxed-value `==`, mutate-via-interface, wrong unbox type |
| 15 | Operator Equality | behavioral | `==` vs Equals/GetHashCode consistency, null-safe operators |
| 16 | Iterators | behavioral | deferred argument validation, re-enumeration side effects |
| 17 | Task.WhenAll | behavioral | observe all failures + successful results, not just the first |
| 18 | IComparable / IComparer | behavioral | subtraction overflow, comparer that never returns negative |
| 19 | ref / out / in | **compile** | unassigned `out`, write to `in`, missing `out`/`ref` at call site |
| 20 | Pattern Matching | behavioral | non-exhaustive switch, null arm ordering |
| 21 | Records | behavioral | value equality with a List member, shallow `with` copy |
| 22 | String Pitfalls | behavioral | culture-sensitive casing (Turkish-I) vs ordinal |
| 23 | Extension Resolution | behavioral | static (non-polymorphic) dispatch, null receiver |
| 24 | Static Init Order | behavioral | field-initializer order; static constructor fix |
| 25 | Interface Members | **compile** | explicit implementation & default interface methods need an interface ref |
| 26 | DateTime / TimeZone | behavioral | DateTimeOffset vs stripped `.DateTime`, `.UtcDateTime` |
| 27 | Span / ref struct | **compile** | ref struct as field / generic arg / returned stackalloc |
| 28 | Tuples | behavioral | positional deconstruction, ValueTuple vs Tuple equality |
| 29 | Reflection | behavioral | `inherit:true`, `NonPublic\|Instance` flags, instance vs static GetValue |
| 30 | Async Streams | behavioral | `[EnumeratorCancellation]` + honoring the cancellation token |
