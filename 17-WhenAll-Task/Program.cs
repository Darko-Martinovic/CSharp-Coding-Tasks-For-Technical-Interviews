// =============================================================================
// TASK 17 — Task.WhenAll & Exception Aggregation
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Four tasks run concurrently; the even-numbered ones fail. The current handler
// reports only ONE failure and throws away the successful results entirely.
//
// THINGS TO SCRUTINISE:
//   1. `await Task.WhenAll(tasks)` rethrows only the FIRST exception, even though
//      the returned Task aggregates them all. How do you observe EVERY failure?
//   2. When WhenAll faults, you lose the successful results too. How do you still
//      collect the values from the tasks that DID succeed? (Hint: inspect each
//      task's final state — IsFaulted / IsCompletedSuccessfully / Result.)
//
// EXPECTED (after fix):
//   2 of 4 tasks failed:
//     task 2 failed
//     task 4 failed
//   succeeded: 1, 3
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

        try
        {
            int[] results = await Task.WhenAll(tasks);
            Console.WriteLine($"all succeeded: {string.Join(", ", results)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"caught: {ex.Message}");
        }
    }
}
