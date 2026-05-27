// =============================================================================
// SOLUTION 19 — ref / out / in Parameters
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

        // FIX #1: an `out` parameter must be definitely assigned on EVERY path.
        result = 0;
        return false;
    }

    static double Scale(in double factor, double value)
    {
        // FIX #2: `in` is a readonly reference — copy to a local to compute.
        double scaled = factor * 2;
        return value * scaled;
    }

    static void Swap(ref int a, ref int b)
    {
        int t = a;
        a = b;
        b = t;
    }

    public static void Main()
    {
        // FIX #3: pass the `out` keyword and declare the target inline.
        if (TryParseAge("42", out int age))
            Console.WriteLine($"age: {age}");

        int x = 1, y = 2;
        // FIX #4: `ref` must appear at the call site too, on both arguments.
        Swap(ref x, ref y);
        Console.WriteLine($"x={x}, y={y}");

        Console.WriteLine($"scaled: {Scale(1.5, 10)}");
    }
}
