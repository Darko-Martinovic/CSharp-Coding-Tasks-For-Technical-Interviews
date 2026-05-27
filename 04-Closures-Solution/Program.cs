// =============================================================================
// SOLUTION 04 — Closures & Captured Loop Variables
// =============================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        // ---- Block 1 -------------------------------------------------------
        var actions = new List<Action>();
        for (int i = 0; i < 5; i++)
        {
            // FIX: copy the loop variable into a fresh per-iteration local. Each
            //      lambda now closes over its OWN `copy`, not the single shared `i`.
            int copy = i;
            actions.Add(() => Console.Write(copy + " "));
        }

        Console.Write("delegates: ");
        foreach (var a in actions) a();
        Console.WriteLine();

        // ---- Block 2 -------------------------------------------------------
        var tasks = new List<Task>();
        var results = new int[5];
        for (int i = 0; i < 5; i++)
        {
            // FIX: same per-iteration capture. This also removes the race where a
            //      task observed `i == 5` and indexed out of bounds.
            int copy = i;
            tasks.Add(Task.Run(() => { results[copy] = copy; }));
        }
        Task.WaitAll(tasks.ToArray());

        Console.Write("tasks:     ");
        foreach (var r in results) Console.Write(r + " ");
        Console.WriteLine();

        // NOTE: `foreach (var i in Enumerable.Range(0, 5))` would also be correct
        // without a copy — since C# 5, the foreach iteration variable is a fresh
        // binding per iteration. Only the classic `for` variable is shared.
    }
}
