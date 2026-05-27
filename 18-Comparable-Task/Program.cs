// =============================================================================
// TASK 18 — IComparable / IComparer Correctness
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Two comparison implementations are subtly wrong. Both produce incorrect signs
// (and a List.Sort built on them can mis-order or even throw). Fix both.
//
// THINGS TO SCRUTINISE:
//   1. Item.CompareTo uses `Priority - other.Priority`. For values near int.Min/
//      int.Max this SUBTRACTION OVERFLOWS and flips sign, so min compares as
//      GREATER than max. Compare without subtracting.
//   2. NameLengthComparer.Compare returns 1 or 0 only — it never returns a
//      negative value, so "a < b" is impossible. That violates the contract of a
//      total order. Make it return negative / zero / positive correctly.
//
// EXPECTED (after fix):
//   min.CompareTo(max) sign: -1 (expected -1)
//   sorted by priority: min, low, zero, max
//   compare(len2, len4) sign: -1 (expected -1)
// =============================================================================

using System;
using System.Collections.Generic;

class Item : IComparable<Item>
{
    public string Name;
    public int Priority;
    public Item(string name, int priority) { Name = name; Priority = priority; }

    public int CompareTo(Item other) => this.Priority - other.Priority;

    public override string ToString() => Name;
}

class NameLengthComparer : IComparer<Item>
{
    public int Compare(Item a, Item b) => a.Name.Length > b.Name.Length ? 1 : 0;
}

public class Program
{
    public static void Main()
    {
        var min = new Item("min", int.MinValue);
        var max = new Item("max", int.MaxValue);
        Console.WriteLine($"min.CompareTo(max) sign: {Math.Sign(min.CompareTo(max))} (expected -1)");

        var items = new List<Item>
        {
            max,
            new Item("zero", 0),
            min,
            new Item("low", -1),
        };
        items.Sort();
        Console.WriteLine($"sorted by priority: {string.Join(", ", items)}");

        var cmp = new NameLengthComparer();
        int sign = Math.Sign(cmp.Compare(new Item("ab", 0), new Item("abcd", 0)));
        Console.WriteLine($"compare(len2, len4) sign: {sign} (expected -1)");
    }
}
