// =============================================================================
// SOLUTION 08 — Exception Handling Anti-Patterns
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
        catch (Exception)
        {
            // FIX #1: `throw;` (no operand) RE-throws the current exception while
            //         preserving its original stack trace. `throw ex;` would reset
            //         the trace to this line, erasing Level3 as the origin.
            throw;
        }
    }

    static void Wrap()
    {
        try
        {
            Level2();
        }
        catch (Exception ex)
        {
            // FIX #2: pass the caught exception as the innerException so the root
            //         cause (and its stack trace) is preserved inside the wrapper.
            throw new ApplicationException("operation failed", ex);
        }
    }

    static int SafeLength(string s)
    {
        // FIX #3: don't swallow a NullReferenceException — that masks a real defect.
        //         Guard the precondition explicitly; only "expected" inputs are
        //         handled, genuine bugs are still allowed to surface.
        if (s is null) return 0;
        return s.Trim().Length;
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
