// =============================================================================
// SOLUTION 10 — Race Conditions & Broken Locking
// =============================================================================

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    const int Threads = 8;
    const int PerThread = 1_000_000;

    static int _counter = 0;
    static int _lockedCounter = 0;

    // FIX #2: a single, shared, readonly lock object that every iteration uses.
    static readonly object _gate = new object();

    public static void Main()
    {
        // FIX #1: Interlocked.Increment performs the read-modify-write atomically,
        //         lock-free, with no lost updates. (A lock on _gate would also be
        //         correct, but Interlocked is the right tool for a single counter.)
        RunConcurrently(() =>
        {
            for (int i = 0; i < PerThread; i++)
                Interlocked.Increment(ref _counter);
        });
        Console.WriteLine($"counter       = {_counter} (expected {Threads * PerThread})");

        // FIX #2: lock on the shared _gate so threads actually serialize on the
        //         same monitor. (Locking on a fresh object gave no exclusion.)
        RunConcurrently(() =>
        {
            for (int i = 0; i < PerThread; i++)
                lock (_gate)
                {
                    _lockedCounter++;
                }
        });
        Console.WriteLine($"lockedCounter = {_lockedCounter} (expected {Threads * PerThread})");
    }

    static void RunConcurrently(Action body)
    {
        var tasks = new List<Task>();
        for (int t = 0; t < Threads; t++)
            tasks.Add(Task.Run(body));
        Task.WaitAll(tasks.ToArray());
    }
}
