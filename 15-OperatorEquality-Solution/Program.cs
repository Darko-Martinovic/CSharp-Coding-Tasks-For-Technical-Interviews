// =============================================================================
// SOLUTION 15 — Operator Overloading & Equality Consistency
// =============================================================================

using System;
using System.Collections.Generic;

// FIX: implement IEquatable<T> and override Equals + GetHashCode so ALL of
//      ==, Equals and hashing agree. The operators are now null-safe via
//      ReferenceEquals checks.
sealed class Temperature : IEquatable<Temperature>
{
    public double Celsius { get; }
    public Temperature(double c) { Celsius = c; }

    public bool Equals(Temperature other) =>
        other is not null && Celsius == other.Celsius;

    public override bool Equals(object obj) => Equals(obj as Temperature);

    // FIX #1: hash code consistent with equality (equal => same hash).
    public override int GetHashCode() => Celsius.GetHashCode();

    // FIX #2: null-safe operators. If left is null the result depends only on
    //         whether right is null too — no dereference of a null operand.
    public static bool operator ==(Temperature a, Temperature b) =>
        a is null ? b is null : a.Equals(b);

    public static bool operator !=(Temperature a, Temperature b) => !(a == b);

    public override string ToString() => $"{Celsius}°C";
}

public class Program
{
    public static void Main()
    {
        var a = new Temperature(20);
        var b = new Temperature(20);

        Console.WriteLine($"a == b     : {a == b}");
        Console.WriteLine($"a.Equals(b): {a.Equals(b)}");

        var set = new HashSet<Temperature> { a };
        Console.WriteLine($"set has b  : {set.Contains(b)}");

        Temperature n = null;
        Console.WriteLine($"a == null  : {a == n}");
        Console.WriteLine($"null == a  : {n == a}");
    }
}
