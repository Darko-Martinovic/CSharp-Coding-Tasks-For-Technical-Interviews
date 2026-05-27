// =============================================================================
// TASK 25 — Explicit & Default Interface Members
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// THIS PROGRAM DOES NOT COMPILE. Both calls in Main fail because the members are
// not accessible through the concrete `Square` type.
//
// THINGS TO SCRUTINISE:
//   1. Square implements IArea.Area() EXPLICITLY. An explicit implementation is
//      only visible through the interface, never through the implementing type —
//      so `sq.Area()` does not compile.
//   2. Greet() is a DEFAULT INTERFACE METHOD. Default members live on the
//      interface, not on the implementing type, so `sq.Greet()` does not compile
//      either.
//
// Fix Main so both calls work. (You may either access the members through an
// interface reference, or change how the members are exposed. Decide which is
// appropriate for each.)
//
// EXPECTED (after fix):
//   area: 16
//   greet: Hello from sq1
// =============================================================================

using System;

interface IArea
{
    double Area();
}

interface IGreeter
{
    string Name { get; }
    string Greet() => $"Hello from {Name}";   // default interface method
}

class Square : IArea, IGreeter
{
    public double Side;
    public string Name { get; set; }

    double IArea.Area() => Side * Side;        // explicit implementation
}

public class Program
{
    public static void Main()
    {
        var sq = new Square { Side = 4, Name = "sq1" };

        Console.WriteLine($"area: {sq.Area()}");
        Console.WriteLine($"greet: {sq.Greet()}");
    }
}
