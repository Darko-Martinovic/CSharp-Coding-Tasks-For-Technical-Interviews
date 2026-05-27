// =============================================================================
// TASK 07 — Mutable Struct Value Semantics
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// `Counter` is a mutable struct. The increments below silently do nothing in
// two of the three cases. Make all three counts come out as expected WITHOUT
// converting Counter to a class.
//
// THINGS TO SCRUTINISE:
//   1. LIST: `list[0].Increment()` compiles and runs, but the count stays 0.
//      Why? (Hint: what does a List<T> indexer GETTER return for a struct?)
//   2. FOREACH: the foreach iteration variable is a COPY of each element.
//      Mutating it cannot affect the array. How do you mutate in place?
//   3. ARRAY-BY-INDEX: `arr[0].Increment()` actually WORKS. Why is array element
//      access different from a List<T> indexer? (Understanding this is the point.)
//
// EXPECTED (after fix):
//   list:    1
//   foreach: 1, 1
//   array:   1
// =============================================================================

using System;
using System.Collections.Generic;

struct Counter
{
    public int Count;
    public void Increment() => Count++;
}

public class Program
{
    public static void Main()
    {
        // 1) struct stored in a List<T>
        var list = new List<Counter> { new Counter() };
        list[0].Increment();
        Console.WriteLine($"list:    {list[0].Count}");

        // 2) struct array mutated via foreach
        var counters = new Counter[] { new Counter(), new Counter() };
        foreach (var c in counters) c.Increment();
        Console.WriteLine($"foreach: {counters[0].Count}, {counters[1].Count}");

        // 3) struct array mutated via index (this one already works — explain why)
        var arr = new Counter[1];
        arr[0].Increment();
        Console.WriteLine($"array:   {arr[0].Count}");
    }
}
