// =============================================================================
// TASK 08 — Exception Handling Anti-Patterns
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Three independent exception-handling bugs are destroying diagnostic info.
// Fix them so the root cause survives all the way to Main.
//
// THINGS TO SCRUTINISE:
//   1. RETHROW: Level2 catches and does `throw ex;`. What does that do to the
//      stack trace, and how do you rethrow WITHOUT destroying it?
//   2. WRAPPING: Wrap() throws a new exception but discards the original. How do
//      you wrap an exception while preserving the root cause?
//   3. SWALLOWING: SafeLength catches every Exception and returns 0, hiding a
//      genuine NullReferenceException (a real defect). What should it do instead?
//
// EXPECTED (after fix):
//   caught: operation failed
//   inner : boom at level 3
//   trace shows Level3: True
//   SafeLength(null) = 0
// =============================================================================

using System;

public class Program
{
    static void Level3() => throw new InvalidOperationException("boom at level 3");

    static void Level2()
    {
        try
        {
            Level3();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    static void Wrap()
    {
        try
        {
            Level2();
        }
        catch (Exception)
        {
            throw new ApplicationException("operation failed");
        }
    }

    static int SafeLength(string s)
    {
        try
        {
            return s.Trim().Length;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static void Main()
    {
        try
        {
            Wrap();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"caught: {ex.Message}");
            Console.WriteLine($"inner : {ex.InnerException?.Message ?? "<null - root cause lost>"}");
            Console.WriteLine($"trace shows Level3: {ex.InnerException?.StackTrace?.Contains("Level3") ?? false}");
        }

        Console.WriteLine($"SafeLength(null) = {SafeLength(null)}");
    }
}
