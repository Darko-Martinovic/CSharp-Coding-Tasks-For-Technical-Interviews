// =============================================================================
// TASK 13 — Nullable Reference Types
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// This project has `<Nullable>enable</Nullable>` AND treats nullability
// warnings as ERRORS, so IT DOES NOT COMPILE. Fix every nullability defect
// (do NOT just silence them with the `!` null-forgiving operator — fix the
// actual logic). There are FOUR distinct issues.
//
// THINGS TO SCRUTINISE:
//   1. CS8618: User.Name is a non-nullable string that is never initialised.
//      What keyword forces callers to set it (without a constructor)?
//   2. CS8602: GetDomain dereferences `email` which may be null.
//   3. CS8603: Find can return null (no match) but its return type says non-null.
//   4. CS8602 (again): in Main, the result of Find may be null when you read
//      `.Email`.
//
// EXPECTED (after fix):
//   domain: example.com
//   missing is null: True
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

class User
{
    public string Name { get; set; }
    public string? Email { get; set; }
}

public class Program
{
    static string GetDomain(string? email)
    {
        int at = email.IndexOf('@');
        return email.Substring(at + 1);
    }

    static User Find(List<User> users, string name)
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
        Console.WriteLine($"domain: {GetDomain(alice.Email)}");

        var missing = Find(users, "Bob");
        Console.WriteLine($"missing is null: {missing is null}");
    }
}
