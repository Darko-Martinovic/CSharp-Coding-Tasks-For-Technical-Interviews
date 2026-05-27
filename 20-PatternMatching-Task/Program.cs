// =============================================================================
// TASK 20 — Pattern Matching & switch Expressions
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Two defects in the switch expressions below (note the compiler's CS8509
// non-exhaustive warning as a hint for the first).
//
// THINGS TO SCRUTINISE:
//   1. Area() handles Circle and Rectangle but NOT Square. A switch expression
//      with no matching arm throws SwitchExpressionException at run time. Add the
//      missing arm (and ideally make the switch exhaustive).
//   2. Name() lumps a null Shape into the `_ => "unknown shape"` arm, silently
//      mislabelling "nothing" as "an unknown kind of shape". Distinguish null
//      explicitly. (Pattern order matters: the `null` arm must precede `_`.)
//
// EXPECTED (after fix):
//   circle area=12.57
//   rectangle area=12
//   square area=25
//   null -> no shape
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
    };

    static string Name(Shape s) => s switch
    {
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
