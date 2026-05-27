// =============================================================================
// TASK 26 — DateTime, Offsets & Time Zones
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// `inUtc` and `inPlus2` represent the SAME instant (12:00Z == 14:00+02:00).
// The DateTimeOffset == comparison already knows this. But the two helpers below
// throw away the offset by reading `.DateTime` (the wall-clock part), producing
// wrong answers. (Output is independent of the machine's local time zone.)
//
// THINGS TO SCRUTINISE:
//   1. HoursBetween subtracts the `.DateTime` (wall-clock) values, so two
//      representations of the same instant look 2 hours apart. Subtract the
//      DateTimeOffsets themselves (offset-aware) instead.
//   2. ToUtc returns `.DateTime` (the local wall-clock, offset discarded) rather
//      than the actual UTC instant. Return the true UTC time.
//
// EXPECTED (after fix):
//   same instant? True
//   hours between: 0
//   plus2 as UTC: 12:00
// =============================================================================

using System;

public class Program
{
    static double HoursBetween(DateTimeOffset a, DateTimeOffset b)
    {
        return (b.DateTime - a.DateTime).TotalHours;
    }

    static DateTime ToUtc(DateTimeOffset dto)
    {
        return dto.DateTime;
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
