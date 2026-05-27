// =============================================================================
// TASK 22 — Culture-Sensitive String Bugs
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// This code runs under the Turkish (tr-TR) culture, where the casing of the
// letter 'i' is special ('i'.ToUpper() == 'İ', 'I'.ToLower() == 'ı'). Both
// helpers below use culture-sensitive casing for what should be culture-
// INVARIANT, programmatic comparisons — and both return the wrong answer.
//
// THINGS TO SCRUTINISE:
//   1. IsImageFile uppercases the file name with the current culture before
//      comparing the extension. Under tr-TR, "image.gif" -> "İMAGE.GİF", which
//      does NOT end with ".GIF". Compare extensions culture-invariantly.
//   2. IsAdmin lowercases the role with the current culture. Under tr-TR,
//      "ADMIN".ToLower() == "admın" (dotless ı) != "admin". Use an ordinal,
//      case-insensitive comparison.
//
// EXPECTED (after fix):
//   icon.png is image: True
//   image.gif is image: True
//   ADMIN is admin: True
// =============================================================================

using System;
using System.Globalization;
using System.Threading;

public class Program
{
    static bool IsImageFile(string name)
    {
        string upper = name.ToUpper();
        return upper.EndsWith(".PNG") || upper.EndsWith(".GIF");
    }

    static bool IsAdmin(string role)
    {
        return role.ToLower() == "admin";
    }

    public static void Main()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

        Console.WriteLine($"icon.png is image: {IsImageFile("icon.png")}");
        Console.WriteLine($"image.gif is image: {IsImageFile("image.gif")}");
        Console.WriteLine($"ADMIN is admin: {IsAdmin("ADMIN")}");
    }
}
