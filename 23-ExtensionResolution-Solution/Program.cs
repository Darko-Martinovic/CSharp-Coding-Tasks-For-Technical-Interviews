// =============================================================================
// SOLUTION 23 — Extension Method Resolution
// =============================================================================

using System;

class Shape
{
    public string Name = "shape";

    // FIX #1: make Area a VIRTUAL INSTANCE method. Now `s.Area()` dispatches on
    //         the run-time type (Circle) — true polymorphism, which extension
    //         methods can never provide (they bind to the static type).
    public virtual double Area() => 0;
}

class Circle : Shape
{
    public double Radius = 2;
    public Circle() { Name = "circle"; }

    public override double Area() => Math.PI * Radius * Radius;
}

static class ShapeExtensions
{
    // Describe stays an extension (a convenience over Shape) but is now null-safe.
    public static string Describe(this Shape s)
    {
        // FIX #2: guard the null receiver. An extension call on null does not throw
        //         on its own, so the method must check before dereferencing.
        if (s is null) return "<none>";
        return $"{s.Name} (area {s.Area():0.##})";
    }
}

public class Program
{
    public static void Main()
    {
        Shape s = new Circle();
        Console.WriteLine($"area: {s.Area():0.##}");

        Shape none = null;
        Console.WriteLine($"describe null: {none.Describe()}");
    }
}
