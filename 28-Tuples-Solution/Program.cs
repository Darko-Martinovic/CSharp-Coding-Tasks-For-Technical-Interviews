// =============================================================================
// SOLUTION 28 — Tuples & Deconstruction
// =============================================================================

using System;

public class Program
{
    static (string Name, int Age) GetPerson() => ("Alice", 30);

    public static void Main()
    {
        // FIX #1: bind by POSITION in the right order (or access by element name:
        //         `var p = GetPerson(); ... p.Name, p.Age`).
        var (name, age) = GetPerson();
        Console.WriteLine($"name={name}, age={age}");

        // FIX #2: value tuples (ValueTuple) overload == and != and compare by
        //         value element-by-element. (System.Tuple<> would need .Equals.)
        var t1 = (1, "a");
        var t2 = (1, "a");
        Console.WriteLine($"tuples equal: {t1 == t2}");
    }
}
