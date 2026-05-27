// =============================================================================
// TASK 23 — Extension Method Resolution
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Extension methods are just static method calls in disguise, with two
// consequences that bite here.
//
// THINGS TO SCRUTINISE:
//   1. STATIC DISPATCH: `s.Area()` is resolved against the COMPILE-TIME type of
//      `s` (Shape), not its run-time type (Circle). So Area(Shape) is chosen and
//      returns 0 — extension methods are NOT polymorphic. If you want run-time
//      dispatch, this should be a virtual instance method, not an extension.
//   2. NULL RECEIVER: calling `none.Describe()` does not throw at the call (it is
//      a static call with a null argument), but Describe() then dereferences the
//      shape and throws NullReferenceException. Make Describe null-safe.
//
// EXPECTED (after fix):
//   area: 12.57
//   describe null: <none>
// =============================================================================

using System;

class Shape
{
    public string Name = "shape";
}

class Circle : Shape
{
    public double Radius = 2;
    public Circle() { Name = "circle"; }
}

static class ShapeExtensions
{
    public static double Area(this Shape s) => 0;
    public static double Area(this Circle c) => Math.PI * c.Radius * c.Radius;

    public static string Describe(this Shape s) => $"{s.Name} (area {s.Area():0.##})";
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
