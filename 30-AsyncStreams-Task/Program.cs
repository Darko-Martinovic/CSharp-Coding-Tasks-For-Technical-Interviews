// =============================================================================
// TASK 30 — IAsyncEnumerable & Cancellation
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// The consumer cancels the stream after receiving 4, expecting it to stop. But
// the producer runs all the way to 9 and the cancellation is never observed.
// Two related defects must BOTH be fixed for cancellation to work.
//
// THINGS TO SCRUTINISE:
//   1. The consumer passes the token via `.WithCancellation(cts.Token)`, but the
//      iterator's CancellationToken parameter lacks the [EnumeratorCancellation]
//      attribute — so the token is never delivered into the method (it stays
//      CancellationToken.None inside GenerateAsync).
//   2. Even with the token wired up, the loop never CHECKS it. Add a
//      cancellation check (ThrowIfCancellationRequested) inside the loop.
//
// EXPECTED (after fix):
//   cancelled
//   produced: 0,1,2,3,4
// =============================================================================

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    static async IAsyncEnumerable<int> GenerateAsync(CancellationToken token = default)
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(10);
            yield return i;
        }
    }

    public static async Task Main()
    {
        using var cts = new CancellationTokenSource();
        var produced = new List<int>();

        try
        {
            await foreach (var n in GenerateAsync().WithCancellation(cts.Token))
            {
                produced.Add(n);
                if (n == 4) cts.Cancel();
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("cancelled");
        }

        Console.WriteLine($"produced: {string.Join(",", produced)}");
    }
}
