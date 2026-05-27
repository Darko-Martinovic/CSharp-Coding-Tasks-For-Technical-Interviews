// =============================================================================
// SOLUTION 30 — IAsyncEnumerable & Cancellation
// =============================================================================

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    // FIX #1: [EnumeratorCancellation] tells the compiler to feed the token from
    //         the consumer's `.WithCancellation(...)` into THIS parameter. Without
    //         it, `token` is always CancellationToken.None here.
    static async IAsyncEnumerable<int> GenerateAsync(
        [EnumeratorCancellation] CancellationToken token = default)
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(10);
            // FIX #2: actually observe cancellation. (Task.Delay(ms, token) would
            //         also honor it; here we check explicitly before yielding.)
            token.ThrowIfCancellationRequested();
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
