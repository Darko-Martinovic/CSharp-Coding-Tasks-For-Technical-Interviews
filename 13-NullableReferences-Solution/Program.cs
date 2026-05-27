// =============================================================================
// SOLUTION 13 — Nullable Reference Types
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

class User
{
    // FIX #1 (CS8618): `required` forces every initializer/constructor to set
    //         Name, so it is guaranteed non-null without writing a constructor.
    public required string Name { get; set; }
    public string? Email { get; set; }
}

public class Program
{
    // FIX #2 (CS8602): handle the null/no-'@' case instead of dereferencing.
    static string GetDomain(string? email)
    {
        if (string.IsNullOrEmpty(email)) return "";
        int at = email.IndexOf('@');
        return at < 0 ? "" : email.Substring(at + 1);
    }

    // FIX #3 (CS8603): advertise that the result may be null (no match found).
    static User? Find(List<User> users, string name)
    {
        return users.FirstOrDefault(u => u.Name == name);
    }

    public static void Main()
    {
        var users = new List<User>
        {
            new User { Name = "Alice", Email = "alice@example.com" }
        };

        var alice = Find(users, "Alice");
        // FIX #4 (CS8602): null-conditional access; GetDomain already tolerates null.
        Console.WriteLine($"domain: {GetDomain(alice?.Email)}");

        var missing = Find(users, "Bob");
        Console.WriteLine($"missing is null: {missing is null}");
    }
}
