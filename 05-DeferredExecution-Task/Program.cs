// =============================================================================
// TASK 05 — LINQ Deferred Execution
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Three independent bugs, all rooted in the fact that LINQ queries are LAZY —
// they execute when enumerated, not when defined.
//
// THINGS TO SCRUTINISE:
//   1. MULTIPLE ENUMERATION: `query` is enumerated twice (Count() then foreach).
//      The side-effecting Select therefore runs 10 times, not 5. Make the source
//      run exactly once.
//   2. CAPTURED VARIABLE: `filtered` is defined while threshold == 2, but
//      threshold is changed to 4 BEFORE the query is enumerated. Which value
//      wins, and how do you lock in the intended (threshold == 2) result?
//   3. ENUMERATION AFTER DISPOSE: ReadLines() returns a lazy iterator over a
//      StringReader that is already disposed by the time the caller enumerates
//      it -> ObjectDisposedException. Fix the lifetime.
//
// EXPECTED (after fix):
//   Count: 2
//   Even: 2
//   Even: 4
//   Select invoked 5 times
//   Filtered: 3,4,5
//   Lines: a | b | c
// =============================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    static int _selectCalls = 0;

    static IEnumerable<int> GetNumbers()
    {
        return Enumerable.Range(1, 5).Select(n =>
        {
            _selectCalls++;
            return n;
        });
    }

    public static void Main()
    {
        // ---- Bug 1: multiple enumeration ----------------------------------
        var query = GetNumbers().Where(n => n % 2 == 0);
        Console.WriteLine($"Count: {query.Count()}");
        foreach (var n in query) Console.WriteLine($"Even: {n}");
        Console.WriteLine($"Select invoked {_selectCalls} times");

        // ---- Bug 2: captured variable in deferred query -------------------
        int threshold = 2;
        var filtered = Enumerable.Range(1, 5).Where(n => n > threshold);
        threshold = 4;
        Console.WriteLine($"Filtered: {string.Join(",", filtered)}");

        // ---- Bug 3: enumeration after the source is disposed --------------
        IEnumerable<string> lines = ReadLines();
        Console.WriteLine($"Lines: {string.Join(" | ", lines)}");
    }

    static IEnumerable<string> ReadLines()
    {
        using var reader = new StringReader("a\nb\nc");
        return ReadAll(reader);
    }

    static IEnumerable<string> ReadAll(TextReader reader)
    {
        string line;
        while ((line = reader.ReadLine()) != null)
            yield return line;
    }
}
