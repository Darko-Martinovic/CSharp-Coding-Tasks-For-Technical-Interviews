// =============================================================================
// TASK 03 — Async / Await Done Wrong
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// This program compiles but misbehaves: one expected line never prints, and an
// exception that LOOKS caught actually crashes the process. Fix the async bugs
// so the program runs to completion and the catch block works.
//
// THINGS TO SCRUTINISE:
//   1. LogAsync is `async void`. Why is that almost always wrong, and why does
//      its output go missing here?
//   2. ComputeSync blocks on `.Result`. Two problems: (a) it would DEADLOCK
//      under a UI / classic ASP.NET SynchronizationContext (sync-over-async),
//      and (b) it changes the exception type that propagates. What does the
//      caller actually catch — and why does the `catch (ArgumentException)`
//      below NOT fire?
//   3. Main is `static void`. Make the whole call chain async "all the way".
//
// EXPECTED (after fix):
//   LOG: starting
//   Result: 42
//   Caught: x must be >= 0
//   All done
// =============================================================================

using System;
using System.Threading.Tasks;

public class Program
{
    static async void LogAsync(string msg)
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

    static int ComputeSync(int x)
    {
        return ComputeAsync(x).Result;
    }

    public static void Main()
    {
        LogAsync("starting");

        int r = ComputeSync(21);
        Console.WriteLine($"Result: {r}");

        try
        {
            int bad = ComputeSync(-1);
            Console.WriteLine($"Unexpected: {bad}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Caught: {ex.Message}");
        }

        Console.WriteLine("All done");
    }
}
