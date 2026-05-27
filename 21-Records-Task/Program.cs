// =============================================================================
// TASK 21 — Records: Value Equality & Shallow Copy
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Records give you value equality and non-destructive `with` — but only as deep
// as the MEMBERS' own semantics. This Order record holds a mutable List, and
// both behaviours surprise here.
//
// THINGS TO SCRUTINISE:
//   1. EQUALITY: `a == b` is False even though both orders look identical. The
//      synthesized record equality compares the List member by REFERENCE
//      (List<T> has no value equality), not by contents. Make equal-content
//      orders compare equal.
//   2. SHALLOW COPY: `a with { Id = 2 }` copies the LIST REFERENCE, so `c.Items`
//      and `a.Items` are the SAME list — mutating one mutates the other. Treat
//      the record as immutable so producing `c` does not corrupt `a`.
//
// EXPECTED (after fix):
//   a == b: True
//   a.Items: apple,pear
//   c.Items: apple,pear,banana
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

record Order(int Id, List<string> Items);

public class Program
{
    public static void Main()
    {
        var a = new Order(1, new List<string> { "apple", "pear" });
        var b = new Order(1, new List<string> { "apple", "pear" });

        Console.WriteLine($"a == b: {a == b}");

        var c = a with { Id = 2 };
        c.Items.Add("banana");

        Console.WriteLine($"a.Items: {string.Join(",", a.Items)}");
        Console.WriteLine($"c.Items: {string.Join(",", c.Items)}");
    }
}
