// =============================================================================
// SOLUTION 21 — Records: Value Equality & Shallow Copy
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

// FIX #1: a record only gets STRUCTURAL equality if its members do. List<T> uses
//         reference equality, so we either (a) supply our own Equals/GetHashCode
//         that compares the items by sequence, or (b) use a value-equatable
//         collection. We do (a) here, and expose Items as IReadOnlyList so callers
//         cannot mutate it in place (supporting FIX #2).
record Order(int Id, IReadOnlyList<string> Items)
{
    public virtual bool Equals(Order other) =>
        other is not null && Id == other.Id && Items.SequenceEqual(other.Items);

    public override int GetHashCode()
    {
        var hc = new HashCode();
        hc.Add(Id);
        foreach (var item in Items) hc.Add(item);
        return hc.ToHashCode();
    }
}

public class Program
{
    public static void Main()
    {
        var a = new Order(1, new List<string> { "apple", "pear" });
        var b = new Order(1, new List<string> { "apple", "pear" });

        Console.WriteLine($"a == b: {a == b}");

        // FIX #2: treat the record as immutable. Instead of mutating a shared list,
        //         build a NEW list for the modified copy. `a` is left untouched.
        var c = a with { Id = 2, Items = a.Items.Append("banana").ToList() };

        Console.WriteLine($"a.Items: {string.Join(",", a.Items)}");
        Console.WriteLine($"c.Items: {string.Join(",", c.Items)}");
    }
}
