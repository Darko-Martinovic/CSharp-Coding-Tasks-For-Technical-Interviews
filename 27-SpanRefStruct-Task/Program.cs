// =============================================================================
// TASK 27 — Span<T> & ref struct Rules
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// THIS PROGRAM DOES NOT COMPILE. `Span<T>` is a `ref struct`: it can live only on
// the stack. Three uses below violate that. Fix each — the Main() usage of Span
// is already correct and should keep working.
//
// THINGS TO SCRUTINISE:
//   1. CS8345: Statistics has a `Span<int>` FIELD. A ref struct cannot be a field
//      of a normal class (it would escape to the heap). Store the data as an
//      ordinary array instead.
//   2. CS8352/CS8347: BuildRange returns a Span over a `stackalloc` local, which
//      would dangle once the method returns. Return owned heap memory instead.
//   3. CS9244: `Span<int>` cannot be used as a generic type argument
//      (List<Span<int>>), because a ref struct may not escape to the heap. Use
//      the array element type instead.
//
// NOTE: the compiler reports the field error (CS8345) first and only surfaces
//       #2 and #3 once #1 is fixed — fix all three.
//
// EXPECTED (after fix):
//   sum: 10
//   range sum: 15
// =============================================================================

using System;
using System.Collections.Generic;

class Statistics
{
    public Span<int> Samples;
}

public class Program
{
    static Span<int> BuildRange(int n)
    {
        Span<int> buffer = stackalloc int[n];
        for (int i = 0; i < n; i++) buffer[i] = i + 1;
        return buffer;
    }

    static int Sum(ReadOnlySpan<int> data)
    {
        int total = 0;
        foreach (var x in data) total += x;
        return total;
    }

    public static void Main()
    {
        var batches = new List<Span<int>>();

        ReadOnlySpan<int> nums = stackalloc int[] { 1, 2, 3, 4 };
        Console.WriteLine($"sum: {Sum(nums)}");

        var range = BuildRange(5);
        batches.Add(range);
        Console.WriteLine($"range sum: {Sum(batches[0])}");
    }
}
