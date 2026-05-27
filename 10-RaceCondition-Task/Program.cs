// =============================================================================
// TASK 10 — Race Conditions & Broken Locking
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// 8 threads each increment a shared counter 1,000,000 times, so both totals
// should be 8,000,000. Both come out LOW (and differently every run) because of
// two synchronization bugs. Fix them so each total is exact, every run.
//
// THINGS TO SCRUTINISE:
//   1. `_counter++` is a read-modify-write: three steps that interleave across
//      threads, so increments are lost. What is the atomic alternative?
//   2. The second loop DOES take a lock — but `lock (new object())` allocates a
//      BRAND NEW lock object on every iteration, so no two threads ever contend
//      on the same monitor. The lock is decorative. Fix what you lock on.
//
// EXPECTED (after fix), reliably:
//   counter       = 8000000 (expected 8000000)
//   lockedCounter = 8000000 (expected 8000000)
// =============================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program
{
    const int Threads = 8;
    const int PerThread = 1_000_000;

    static int _counter = 0;
    static int _lockedCounter = 0;

    public static void Main()
    {
        RunConcurrently(() =>
        {
            for (int i = 0; i < PerThread; i++)
                _counter++;
        });
        Console.WriteLine($"counter       = {_counter} (expected {Threads * PerThread})");

        RunConcurrently(() =>
        {
            for (int i = 0; i < PerThread; i++)
                lock (new object())
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
