// =============================================================================
// TASK 11 — LINQ Operator Correctness
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Each query below throws (or is needlessly wasteful) on this data set. Pick the
// RIGHT operator for each intent so the program runs to completion.
//
// THINGS TO SCRUTINISE:
//   1. First(predicate) THROWS when nothing matches. There is no HR employee.
//   2. Single(predicate) THROWS when MORE than one matches. There are two
//      engineers — what did the caller actually intend?
//   3. Average() THROWS on an empty sequence. No employee is in HR.
//   4. `Count(predicate) > 0` works but enumerates everything. What expresses
//      "does any match?" directly (and short-circuits)?
//
// EXPECTED (after fix):
//   HR lead: none
//   first engineer: Alice
//   HR avg salary: n/a
//   has high earners: True
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
        var hr = Emps.First(e => e.Dept == "HR");
        Console.WriteLine($"HR lead: {hr?.Name ?? "none"}");

        var engineer = Emps.Single(e => e.Dept == "Eng");
        Console.WriteLine($"first engineer: {engineer.Name}");

        var hrAvg = Emps.Where(e => e.Dept == "HR").Average(e => e.Salary);
        Console.WriteLine($"HR avg salary: {hrAvg}");

        if (Emps.Count(e => e.Salary > 110) > 0)
            Console.WriteLine("has high earners: True");
    }
}
