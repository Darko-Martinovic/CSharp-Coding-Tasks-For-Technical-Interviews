// =============================================================================
// SOLUTION 22 — Culture-Sensitive String Bugs
// =============================================================================
//
// RULE OF THUMB: for programmatic strings (file names, keys, tokens, protocol
// values) use Ordinal / OrdinalIgnoreCase. Reserve culture-sensitive comparison
// for text shown to and ordered for humans.
// =============================================================================

using System;
using System.Globalization;
using System.Threading;

public class Program
{
    static bool IsImageFile(string name)
    {
        // FIX #1: compare the extension with an ordinal, case-insensitive check.
        //         No casing transform, so the Turkish 'i' rule cannot interfere.
        return name.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            || name.EndsWith(".gif", StringComparison.OrdinalIgnoreCase);
    }

    static bool IsAdmin(string role)
    {
        // FIX #2: ordinal, case-insensitive equality — culture cannot change it.
        return string.Equals(role, "admin", StringComparison.OrdinalIgnoreCase);
    }

    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

        Console.WriteLine($"icon.png is image: {IsImageFile("icon.png")}");
        Console.WriteLine($"image.gif is image: {IsImageFile("image.gif")}");
        Console.WriteLine($"ADMIN is admin: {IsAdmin("ADMIN")}");
    }
}
