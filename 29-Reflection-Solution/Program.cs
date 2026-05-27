// =============================================================================
// SOLUTION 29 — Reflection & Attributes
// =============================================================================

using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
class RoleAttribute : Attribute
{
    public string Role { get; }
    public RoleAttribute(string role) { Role = role; }
}

[Role("admin")]
class BaseController { }

class UserController : BaseController { }

class Secret
{
    private int _value = 42;
}

public class Program
{
    public static void Main()
    {
        // FIX #1: inherit:true so an inherited attribute on the base class is found.
        var attr = typeof(UserController).GetCustomAttribute<RoleAttribute>(inherit: true);
        Console.WriteLine($"role: {attr?.Role ?? "<none>"}");

        // FIX #2: a private instance member needs NonPublic | Instance.
        var field = typeof(Secret).GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);

        // FIX #3: pass the instance to read an instance field (null is only for statics).
        var secret = new Secret();
        int value = (int)field.GetValue(secret);
        Console.WriteLine($"value: {value}");
    }
}
