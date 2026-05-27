// =============================================================================
// SOLUTION 14 — Boxing & Unboxing Traps
// =============================================================================

using System;
using System.Collections;

interface IMover
{
    void Move();
    int Position { get; }
}

// FIX #2: make Robot a CLASS (reference type). Now `IMover m = robot` copies a
//         reference (no boxing), so m and the local refer to the SAME object and
//         the mutation is visible. Mutating value types through an interface is a
//         classic trap — prefer reference types (or immutable structs) for this.
class Robot : IMover
{
    public int Position { get; private set; }
    public void Move() => Position++;
}

public class Program
{
    public static void Main()
    {
        // FIX #1: compare the VALUES, not the box references. object.Equals does
        //         a value comparison for boxed value types. (Equivalently, unbox:
        //         `(int)a == (int)b`.)
        object a = 42;
        object b = 42;
        Console.WriteLine($"values equal: {a.Equals(b)}");

        Robot r = new Robot();
        IMover m = r;
        m.Move();
        m.Move();
        Console.WriteLine($"robot moved to: {r.Position}");

        // FIX #3: unbox to the EXACT boxed type (int), then let it widen to long.
        ArrayList list = new ArrayList { 1, 2 };
        long sum = 0;
        foreach (object o in list)
            sum += (int)o;
        Console.WriteLine($"sum: {sum}");
    }
}
