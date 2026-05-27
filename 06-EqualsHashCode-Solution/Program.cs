// =============================================================================
// SOLUTION 06 — Equals & GetHashCode
// =============================================================================

using System;
using System.Collections.Generic;

// FIX: implement IEquatable<Point> for strongly-typed, allocation-free equality.
sealed class Point : IEquatable<Point>
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }

    // FIX #1: null- and type-safe. The `is` pattern returns false for null and
    //         for unrelated types instead of throwing NullReference/InvalidCast.
    public override bool Equals(object obj) => obj is Point p && Equals(p);

    public bool Equals(Point other) =>
        other is not null && X == other.X && Y == other.Y;

    // FIX #2: override GetHashCode consistently with Equals. Equal points now
    //         hash equally, so HashSet/Dictionary lookups work.
    public override int GetHashCode() => HashCode.Combine(X, Y);

    // Bonus: operators kept consistent with Equals.
    public static bool operator ==(Point left, Point right) =>
        left is null ? right is null : left.Equals(right);

    public static bool operator !=(Point left, Point right) => !(left == right);
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
