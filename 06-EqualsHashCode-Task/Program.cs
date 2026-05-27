// =============================================================================
// TASK 06 — Equals & GetHashCode
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Point overrides Equals but the implementation is broken in several ways, and
// hash-based collections silently misbehave as a result.
//
// THINGS TO SCRUTINISE:
//   1. Equals casts `obj` to Point unconditionally. What happens for null, or
//      for an unrelated type? (Run it — the last line crashes.)
//   2. GetHashCode is NOT overridden. Why does that break HashSet.Contains and
//      Dictionary.ContainsKey even though Equals returns true?
//   3. The golden rule: if two objects are Equal they MUST have the same hash
//      code. Make Equals and GetHashCode consistent.
//
// Bonus (idiomatic): implement IEquatable<Point> and the == / != operators so
// equality is complete and allocation-free.
//
// EXPECTED (after fix):
//   a.Equals(b): True
//   set.Contains(b): True
//   dict.ContainsKey(b): True
//   a.Equals(null): False
//   a.Equals("str"): False
// =============================================================================

using System;
using System.Collections.Generic;

class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }

    public override bool Equals(object obj)
    {
        return ((Point)obj).X == X && ((Point)obj).Y == Y;
    }
}

public class Program
{
    public static void Main()
    {
        var a = new Point(1, 2);
        var b = new Point(1, 2);

        Console.WriteLine($"a.Equals(b): {a.Equals(b)}");

        var set = new HashSet<Point> { a };
        Console.WriteLine($"set.Contains(b): {set.Contains(b)}");

        var dict = new Dictionary<Point, string> { [a] = "origin-ish" };
        Console.WriteLine($"dict.ContainsKey(b): {dict.ContainsKey(b)}");

        Console.WriteLine($"a.Equals(null): {a.Equals(null)}");
        Console.WriteLine($"a.Equals(\"str\"): {a.Equals("str")}");
    }
}
