// =============================================================================
// TASK 29 — Reflection & Attributes
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Three reflection mistakes. Fix them so the program prints the role and the
// field value.
//
// THINGS TO SCRUTINISE:
//   1. RoleAttribute is Inherited=true and sits on the BASE class, but the lookup
//      passes inherit:false, so it is not found on UserController.
//   2. GetField uses BindingFlags.Public | Instance, but `_value` is PRIVATE, so
//      it returns null (and the next line then NREs).
//   3. `_value` is an INSTANCE field, so GetValue(null) is wrong — it needs an
//      instance.
//
// EXPECTED (after fix):
//   role: admin
//   value: 42
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
        var attr = typeof(UserController).GetCustomAttribute<RoleAttribute>(inherit: false);
        Console.WriteLine($"role: {attr?.Role ?? "<none>"}");

        var field = typeof(Secret).GetField("_value", BindingFlags.Public | BindingFlags.Instance);

        var secret = new Secret();
        int value = (int)field.GetValue(null);
        Console.WriteLine($"value: {value}");
    }
}
