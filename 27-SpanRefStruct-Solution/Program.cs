// =============================================================================
// SOLUTION 27 — Span<T> & ref struct Rules
// =============================================================================
//
// A Span<T> is a stack-only window over memory. It must never escape to the heap
// (class field, generic argument) or outlive the buffer it points at (returning
// a stackalloc). When you need ownership/heap storage, use an array (or
// Memory<T>); pass spans only as parameters/locals.
// =============================================================================

using System;
using System.Collections.Generic;

class Statistics
{
    // FIX #1: store owned data as an array; an array IS heap-safe (and converts
    //         implicitly to Span/ReadOnlySpan when you need windowed access).
    public int[] Samples;
}

public class Program
{
    // FIX #2: return an owned array. (It implicitly converts to ReadOnlySpan when
    //         passed to Sum, so callers keep their span-based API.)
    static int[] BuildRange(int n)
    {
        var buffer = new int[n];
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
        // FIX #3: a List of arrays, not a List of Spans.
        var batches = new List<int[]>();

        // Correct, idiomatic Span usage: stackalloc into a local, pass by ref.
        ReadOnlySpan<int> nums = stackalloc int[] { 1, 2, 3, 4 };
        Console.WriteLine($"sum: {Sum(nums)}");

        int[] range = BuildRange(5);
        batches.Add(range);
        Console.WriteLine($"range sum: {Sum(batches[0])}");
    }
}
