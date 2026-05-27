// =============================================================================
// SOLUTION 03 — Async / Await Done Right
// =============================================================================

using System;
using System.Threading.Tasks;

public class Program
{
    // FIX #1: `async Task` instead of `async void`. An async void method cannot
    //         be awaited, so its completion (and any exception) is unobservable —
    //         here Main exited before the 50 ms delay elapsed, swallowing the log.
    static async Task LogAsync(string msg)
    {
        await Task.Delay(50);
        Console.WriteLine($"LOG: {msg}");
    }

    static async Task<int> ComputeAsync(int x)
    {
        await Task.Delay(100);
        if (x < 0) throw new ArgumentException("x must be >= 0");
        return x * 2;
    }

    // FIX #2: No more sync-over-async. We await ComputeAsync directly.
    //         Blocking on `.Result` (a) risks a deadlock under a single-threaded
    //         SynchronizationContext, and (b) re-throws failures wrapped in an
    //         AggregateException — which is why the ArgumentException catch in
    //         the original never matched.

    // FIX #3: async Task Main — async "all the way" from the entry point down.
    public static async Task Main()
    {
        await LogAsync("starting");

        int r = await ComputeAsync(21);
        Console.WriteLine($"Result: {r}");

        try
        {
            int bad = await ComputeAsync(-1); // unwrapped: throws ArgumentException
            Console.WriteLine($"Unexpected: {bad}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Caught: {ex.Message}");
        }

        Console.WriteLine("All done");
    }
}
