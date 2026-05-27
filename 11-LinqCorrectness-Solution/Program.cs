// =============================================================================
// SOLUTION 11 — LINQ Operator Correctness
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

record Employee(string Name, string Dept, int Salary);

public class Program
{
    static readonly List<Employee> Emps = new()
    {
        new("Alice", "Eng",   120),
        new("Bob",   "Eng",   100),
        new("Carol", "Sales",  90),
    };

    public static void Main()
    {
        // FIX #1: FirstOrDefault returns null (not throw) when nothing matches.
        var hr = Emps.FirstOrDefault(e => e.Dept == "HR");
        Console.WriteLine($"HR lead: {hr?.Name ?? "none"}");

        // FIX #2: the intent is "the FIRST engineer", and several exist — use
        //         First, not Single (Single asserts exactly one match).
        var engineer = Emps.First(e => e.Dept == "Eng");
        Console.WriteLine($"first engineer: {engineer.Name}");

        // FIX #3: guard the empty case. DefaultIfEmpty + a sentinel, or just
        //         materialise and check Count before averaging.
        var hrSalaries = Emps.Where(e => e.Dept == "HR").Select(e => e.Salary).ToList();
        Console.WriteLine($"HR avg salary: {(hrSalaries.Count == 0 ? "n/a" : hrSalaries.Average().ToString())}");

        // FIX #4: Any(predicate) expresses existence directly and short-circuits
        //         on the first match instead of counting every element.
        if (Emps.Any(e => e.Salary > 110))
            Console.WriteLine("has high earners: True");
    }
}
