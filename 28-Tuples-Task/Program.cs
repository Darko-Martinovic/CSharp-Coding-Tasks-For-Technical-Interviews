// =============================================================================
// TASK 28 — Tuples & Deconstruction
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Two tuple bugs. Fix both.
//
// THINGS TO SCRUTINISE:
//   1. Deconstruction is POSITIONAL, not by name. Even though GetPerson returns
//      (Name, Age), the deconstruction `var (age, name) = ...` binds element 0
//      (the name) to `age` and element 1 (the age) to `name`. The printed values
//      are swapped. Fix the binding.
//   2. System.Tuple<T1,T2> is a REFERENCE type and does NOT overload ==, so the
//      comparison checks reference identity and prints False. Use a value tuple
//      (ValueTuple), which DOES compare by value — or compare with .Equals.
//
// EXPECTED (after fix):
//   name=Alice, age=30
//   tuples equal: True
// =============================================================================

using System;

public class Program
{
    static (string Name, int Age) GetPerson() => ("Alice", 30);

    public static void Main()
    {
        var (age, name) = GetPerson();
        Console.WriteLine($"name={name}, age={age}");

        var t1 = Tuple.Create(1, "a");
        var t2 = Tuple.Create(1, "a");
        Console.WriteLine($"tuples equal: {t1 == t2}");
    }
}
