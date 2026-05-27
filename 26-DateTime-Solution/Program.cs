// =============================================================================
// SOLUTION 26 — DateTime, Offsets & Time Zones
// =============================================================================
//
// LESSON: a DateTimeOffset identifies an INSTANT. Reading its `.DateTime` gives
// the local wall-clock with the offset DISCARDED — almost never what you want
// for arithmetic or conversion. Compare/subtract the offsets directly, and use
// `.UtcDateTime` (or ToUniversalTime) to get the real UTC instant.
// =============================================================================

using System;

public class Program
{
    static double HoursBetween(DateTimeOffset a, DateTimeOffset b)
    {
        // FIX #1: subtract the offset-aware values. DateTimeOffset subtraction
        //         compares true instants, so the same moment yields 0.
        return (b - a).TotalHours;
    }

    static DateTime ToUtc(DateTimeOffset dto)
    {
        // FIX #2: UtcDateTime applies the offset to give the actual UTC instant.
        return dto.UtcDateTime;
    }

    public static void Main()
    {
        var inUtc = new DateTimeOffset(2026, 5, 27, 12, 0, 0, TimeSpan.Zero);
        var inPlus2 = new DateTimeOffset(2026, 5, 27, 14, 0, 0, TimeSpan.FromHours(2));

        Console.WriteLine($"same instant? {inUtc == inPlus2}");
        Console.WriteLine($"hours between: {HoursBetween(inUtc, inPlus2)}");
        Console.WriteLine($"plus2 as UTC: {ToUtc(inPlus2):HH:mm}");
    }
}
