// =============================================================================
// TASK 19 — ref / out / in Parameters
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// THIS PROGRAM DOES NOT COMPILE. There are four parameter-passing mistakes.
// Fix them all (do not change method signatures except where a body forces it).
//
// THINGS TO SCRUTINISE:
//   1. CS0177: TryParseAge has an `out` parameter that is NOT assigned on the
//      `return false` path. An `out` parameter must be set on EVERY path.
//   2. CS8331: Scale tries to assign to its `in` parameter. `in` means readonly
//      reference — you cannot mutate it. Use a local instead.
//   3. The call `TryParseAge("42", age)` is missing the `out` keyword (and `age`
//      is not declared). Declare it inline with `out`.
//   4. The call `Swap(x, y)` is missing the `ref` keyword on both arguments.
//
// EXPECTED (after fix):
//   age: 42
//   x=2, y=1
//   scaled: 30
// =============================================================================

using System;

public class Program
{
    static bool TryParseAge(string s, out int result)
    {
        if (int.TryParse(s, out int v) && v >= 0)
        {
            result = v;
            return true;
        }
        return false;
    }

    static double Scale(in double factor, double value)
    {
        factor = factor * 2;
        return value * factor;
    }

    static void Swap(ref int a, ref int b)
    {
        int t = a;
        a = b;
        b = t;
    }

    public static void Main()
    {
        if (TryParseAge("42", age))
            Console.WriteLine($"age: {age}");

        int x = 1, y = 2;
        Swap(x, y);
        Console.WriteLine($"x={x}, y={y}");

        Console.WriteLine($"scaled: {Scale(1.5, 10)}");
    }
}
