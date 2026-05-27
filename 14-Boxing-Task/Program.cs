// =============================================================================
// TASK 14 — Boxing & Unboxing Traps
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Three boxing-related bugs produce wrong output and one crash. Fix each so the
// program prints the expected values.
//
// THINGS TO SCRUTINISE:
//   1. `a == b` on two boxed ints compares REFERENCES (the two boxes), not the
//      values, so it is False. How do you compare the values?
//   2. `IMover m = r` BOXES a copy of the struct. m.Move() mutates the box, but
//      `r` (the original value) is untouched, so r.Position stays 0. Make the
//      two stay in sync. (Hint: should Robot be a value type at all here?)
//   3. Unboxing must use the EXACT boxed type. `(long)o` on a boxed int throws
//      InvalidCastException. Fix the unboxing.
//
// EXPECTED (after fix):
//   values equal: True
//   robot moved to: 2
//   sum: 3
// =============================================================================

using System;
using System.Collections;

interface IMover
{
    void Move();
    int Position { get; }
}

struct Robot : IMover
{
    public int Position { get; private set; }
    public void Move() => Position++;
}

public class Program
{
    public static void Main()
    {
        // 1) Two boxed ints, intended value comparison.
        object a = 42;
        object b = 42;
        Console.WriteLine($"values equal: {a == b}");

        // 2) Move the robot twice via the interface, then read its position.
        Robot r = new Robot();
        IMover m = r;
        m.Move();
        m.Move();
        Console.WriteLine($"robot moved to: {r.Position}");

        // 3) Sum boxed ints pulled out of a non-generic ArrayList.
        ArrayList list = new ArrayList { 1, 2 };
        long sum = 0;
        foreach (object o in list)
            sum += (long)o;
        Console.WriteLine($"sum: {sum}");
    }
}
