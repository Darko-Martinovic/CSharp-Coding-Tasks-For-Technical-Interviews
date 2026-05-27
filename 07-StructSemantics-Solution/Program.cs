// =============================================================================
// SOLUTION 07 — Mutable Struct Value Semantics
// =============================================================================
//
// ROOT CAUSE: a struct is a VALUE. Any time you read it out of something that
// returns it "by value" you get a COPY; mutating the copy is lost. The real-world
// lesson is "avoid mutable structs" — but the mechanics matter, so here is how
// to mutate each container correctly.
// =============================================================================

using System;
using System.Collections.Generic;

struct Counter
{
    public int Count;
    public void Increment() => Count++;
}

public class Program
{
    public static void Main()
    {
        // 1) List<T> indexer GETTER returns a copy. `list[0].Increment()` mutates
        //    that throw-away copy. FIX: copy out, mutate, store back.
        var list = new List<Counter> { new Counter() };
        var tmp = list[0];
        tmp.Increment();
        list[0] = tmp;
        Console.WriteLine($"list:    {list[0].Count}");

        // 2) The foreach variable is a copy (and is read-only). FIX: index into the
        //    array directly — `counters[i]` is a VARIABLE (a managed reference to
        //    the element), so the method mutates the element in place.
        var counters = new Counter[] { new Counter(), new Counter() };
        for (int i = 0; i < counters.Length; i++)
            counters[i].Increment();
        Console.WriteLine($"foreach: {counters[0].Count}, {counters[1].Count}");

        // 3) Already worked, for the same reason as the fix in (2): an array element
        //    access yields a variable, not a copy.
        var arr = new Counter[1];
        arr[0].Increment();
        Console.WriteLine($"array:   {arr[0].Count}");
    }
}
