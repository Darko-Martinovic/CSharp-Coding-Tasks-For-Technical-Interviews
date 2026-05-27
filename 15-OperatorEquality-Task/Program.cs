// =============================================================================
// TASK 15 — Operator Overloading & Equality Consistency
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Temperature overloads == and != but the equality contract is broken, and the
// operator is not null-safe. Make equality fully consistent and safe.
//
// THINGS TO SCRUTINISE:
//   1. == is overloaded but Equals/GetHashCode are NOT (note the build warnings
//      CS0660/CS0661). So `a == b` is True yet `a.Equals(b)` is False, and a
//      HashSet can't find an equal value. Override Equals and GetHashCode to
//      match ==.
//   2. The == operator does `a.Celsius` without checking for null, so `a == null`
//      throws NullReferenceException. Make the operators null-safe.
//
// EXPECTED (after fix):
//   a == b     : True
//   a.Equals(b): True
//   set has b  : True
//   a == null  : False
//   null == a  : False
// =============================================================================

using System;
using System.Collections.Generic;

class Temperature
{
    public double Celsius { get; }
    public Temperature(double c) { Celsius = c; }

    public static bool operator ==(Temperature a, Temperature b) => a.Celsius == b.Celsius;
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
