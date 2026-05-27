// =============================================================================
// TASK 24 — Static Field Initialization Order
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Static field initializers run in TEXTUAL ORDER, top to bottom. Two fields
// below read other static fields that have not been initialised yet, so they
// capture the default value (0 / null) instead of the intended one.
//
// THINGS TO SCRUTINISE:
//   1. Banner is initialised from Subtitle, but Subtitle is declared AFTER it —
//      so Subtitle is still null and Banner becomes "*** ***".
//   2. Doubled is computed from Width before Width's initializer has run, so
//      Doubled is 0 instead of 42.
//
// Fix it so the values are correct REGARDLESS of how someone later reorders the
// declarations. (Hint: a static constructor makes the order explicit and robust;
// merely reordering the lines is fragile.)
//
// EXPECTED (after fix):
//   Banner:  *** v1.0 ***
//   Doubled: 42
//   Width:   21
// =============================================================================

using System;

class Report
{
    public static readonly string Banner = $"*** {Subtitle} ***";
    public static readonly string Subtitle = "v1.0";

    public static readonly int Doubled = Width * 2;
    public static readonly int Width = 21;
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine($"Banner:  {Report.Banner}");
        Console.WriteLine($"Doubled: {Report.Doubled}");
        Console.WriteLine($"Width:   {Report.Width}");
    }
}
