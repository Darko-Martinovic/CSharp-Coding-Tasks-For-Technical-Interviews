// =============================================================================
// SOLUTION 05 — LINQ Deferred Execution
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
        // ---- Bug 1 fix: materialise once with ToList(), then enumerate the
        //      in-memory list as many times as you like. Select runs 5x total.
        var query = GetNumbers().Where(n => n % 2 == 0).ToList();
        Console.WriteLine($"Count: {query.Count}");
        foreach (var n in query) Console.WriteLine($"Even: {n}");
        Console.WriteLine($"Select invoked {_selectCalls} times");

        // ---- Bug 2 fix: force execution (ToList) while threshold still == 2,
        //      so the later reassignment cannot affect the captured value.
        int threshold = 2;
        var filtered = Enumerable.Range(1, 5).Where(n => n > threshold).ToList();
        threshold = 4; // no longer observed by the (already-executed) query
        Console.WriteLine($"Filtered: {string.Join(",", filtered)}");

        // ---- Bug 3 fix: lifetime now correct (see ReadLines).
        IEnumerable<string> lines = ReadLines();
        Console.WriteLine($"Lines: {string.Join(" | ", lines)}");
    }

    static IEnumerable<string> ReadLines()
    {
        using var reader = new StringReader("a\nb\nc");
        // FIX: materialise INSIDE the `using`, so the reader is alive during the
        //      one and only enumeration. Returning the lazy iterator would defer
        //      reading until after the reader was disposed -> ObjectDisposedException.
        return ReadAll(reader).ToList();
    }

    static IEnumerable<string> ReadAll(TextReader reader)
    {
        string line;
        while ((line = reader.ReadLine()) != null)
            yield return line;
    }
}
