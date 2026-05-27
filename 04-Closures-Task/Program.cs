// =============================================================================
// TASK 04 — Closures & Captured Loop Variables
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Both blocks below are SUPPOSED to print 0 1 2 3 4, but neither does. Fix them
// so the captured value is the one from the iteration that created the lambda.
//
// THINGS TO SCRUTINISE:
//   1. The `for` loop captures the SAME variable `i` in every lambda. By the
//      time the lambdas run, what is the value of `i`? How do you capture a
//      per-iteration value instead?
//   2. The Task.Run version has the same capture bug PLUS a race: the loop may
//      finish (and `i` reach its final value) before the tasks even start.
//   3. (Conceptual) Why would rewriting the loop as `foreach (var i in
//      Enumerable.Range(0, 5))` NOT have this bug in modern C#?
//
// Do NOT just hard-code the numbers — fix the capture.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        // ---- Block 1: captured loop variable in stored delegates -----------
        var actions = new List<Action>();
        for (int i = 0; i < 5; i++)
        {
            actions.Add(() => Console.Write(i + " "));
        }

        Console.Write("delegates: ");
        foreach (var a in actions) a();
        Console.WriteLine();

        // ---- Block 2: captured loop variable across parallel tasks ---------
        var tasks = new List<Task>();
        var results = new int[5];
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(Task.Run(() => { results[i] = i; })); // i may be out of range / wrong
        }
        Task.WaitAll(tasks.ToArray());

        Console.Write("tasks:     ");
        foreach (var r in results) Console.Write(r + " ");
        Console.WriteLine();
    }
}
