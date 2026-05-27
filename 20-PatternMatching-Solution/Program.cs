// =============================================================================
// SOLUTION 20 — Pattern Matching & switch Expressions
// =============================================================================

using System;

abstract record Shape;
record Circle(double Radius) : Shape;
record Rectangle(double Width, double Height) : Shape;
record Square(double Side) : Shape;

public class Program
{
    static double Area(Shape s) => s switch
    {
        Circle c => Math.PI * c.Radius * c.Radius,
        Rectangle r => r.Width * r.Height,
        // FIX #1: handle Square. A `_` throw arm also documents the "shouldn't
        //         happen" case explicitly instead of an opaque runtime exception.
        Square sq => sq.Side * sq.Side,
        _ => throw new ArgumentException($"unsupported shape: {s.GetType().Name}"),
    };

    static string Name(Shape s) => s switch
    {
        // FIX #2: match null FIRST (before the `_` catch-all) so "no shape" is not
        //         mislabelled as "unknown shape".
        null => "no shape",
        Circle => "circle",
        Rectangle => "rectangle",
        Square => "square",
        _ => "unknown shape",
    };

    public static void Main()
    {
        Shape[] shapes = { new Circle(2), new Rectangle(3, 4), new Square(5) };
        foreach (var sh in shapes)
            Console.WriteLine($"{Name(sh)} area={Area(sh):0.##}");

        Shape none = null;
        Console.WriteLine($"null -> {Name(none)}");
    }
}
