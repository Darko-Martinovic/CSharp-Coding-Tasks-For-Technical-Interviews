// =============================================================================
// SOLUTION 17 — Task.WhenAll & Exception Aggregation
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
    static async Task<int> WorkAsync(int id)
    {
        await Task.Delay(50);
        if (id % 2 == 0) throw new InvalidOperationException($"task {id} failed");
        return id;
    }

    public static async Task Main()
    {
        var tasks = new List<Task<int>>
        {
            WorkAsync(1), WorkAsync(2), WorkAsync(3), WorkAsync(4)
        };

        // FIX #1: await the combined task so every task reaches a terminal state,
        //         but DON'T rely on the rethrown (single) exception. Swallow it
        //         here and inspect each task individually below.
        try
        {
            await Task.WhenAll(tasks);
        }
        catch
        {
            // intentionally ignored — per-task inspection follows.
        }

        // FIX #2: inspect each task's final state to get ALL failures AND the
        //         results of the tasks that succeeded.
        var failures = tasks
            .Where(t => t.IsFaulted)
            .Select(t => t.Exception.InnerException.Message)
            .ToList();

        var succeeded = tasks
            .Where(t => t.IsCompletedSuccessfully)
            .Select(t => t.Result)
            .ToList();

        Console.WriteLine($"{failures.Count} of {tasks.Count} tasks failed:");
        foreach (var f in failures)
            Console.WriteLine($"  {f}");

        Console.WriteLine($"succeeded: {string.Join(", ", succeeded)}");
    }
}
