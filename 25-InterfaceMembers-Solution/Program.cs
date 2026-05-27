// =============================================================================
// SOLUTION 25 — Explicit & Default Interface Members
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

        // FIX: both members are only reachable through an interface reference.
        //   - Area is explicitly implemented, so a cast to IArea is REQUIRED
        //     (or you could switch it to a public implicit implementation).
        //   - Greet is a default interface method that lives on IGreeter, so a
        //     cast to IGreeter is REQUIRED unless Square provides its own Greet().
        Console.WriteLine($"area: {((IArea)sq).Area()}");
        Console.WriteLine($"greet: {((IGreeter)sq).Greet()}");
    }
}
