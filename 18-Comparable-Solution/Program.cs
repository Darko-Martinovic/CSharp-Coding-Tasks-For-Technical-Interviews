// =============================================================================
// SOLUTION 18 — IComparable / IComparer Correctness
// =============================================================================

using System;
using System.Collections.Generic;

class Item : IComparable<Item>
{
    public string Name;
    public int Priority;
    public Item(string name, int priority) { Name = name; Priority = priority; }

    // FIX #1: delegate to int.CompareTo, which is overflow-safe. Never implement
    //         comparison via subtraction — `a - b` overflows for extreme values
    //         and silently returns the wrong sign.
    public int CompareTo(Item other) => this.Priority.CompareTo(other.Priority);

    public override string ToString() => Name;
}

class NameLengthComparer : IComparer<Item>
{
    // FIX #2: return a proper three-way result (negative / zero / positive) so the
    //         comparer defines a valid total order. int.CompareTo does exactly that.
    public int Compare(Item a, Item b) => a.Name.Length.CompareTo(b.Name.Length);
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
