// =============================================================================
// SOLUTION 24 — Static Field Initialization Order
// =============================================================================

using System;

// FIX: assign all statics in a STATIC CONSTRUCTOR, where the order is explicit
//      and dependencies are computed only after their inputs exist. This is
//      robust against later reordering of declarations (the bug-prone part of
//      relying on textual field-initializer order). The simple inputs are set
//      first, then the values derived from them.
class Report
{
    public static readonly string Banner;
    public static readonly string Subtitle;
    public static readonly int Doubled;
    public static readonly int Width;

    static Report()
    {
        Subtitle = "v1.0";
        Width = 21;

        Banner = $"*** {Subtitle} ***";
        Doubled = Width * 2;
    }
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
