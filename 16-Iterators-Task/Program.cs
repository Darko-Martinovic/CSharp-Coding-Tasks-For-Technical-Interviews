// =============================================================================
// TASK 16 — Iterator (yield) Semantics
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Two iterator pitfalls. Fix both.
//
// THINGS TO SCRUTINISE:
//   1. DEFERRED VALIDATION: TakeFirst validates `count` with a guard clause, but
//      because the method body contains `yield`, the WHOLE body (including the
//      guard) is deferred until the first MoveNext(). So a bad argument is not
//      reported at the call site. Restructure so validation runs EAGERLY (at the
//      call) while iteration stays lazy. (Hint: wrapper method + private iterator
//      / local function.)
//   2. RE-ENUMERATION SIDE EFFECTS: Produce() increments a counter each time it
//      yields. The caller enumerates the SAME sequence twice (two Sum() calls),
//      so the producer runs twice. Make the producer run exactly once for the
//      two reads.
//
// EXPECTED (after fix):
//   validation throws at: call site (correct)
//   sum1=3, sum2=3, producer ran 3 times
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static int _produced = 0;

    static IEnumerable<int> TakeFirst(IEnumerable<int> source, int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        int i = 0;
        foreach (var x in source)
        {
            if (i++ >= count) yield break;
            yield return x;
        }
    }

    static IEnumerable<int> Produce()
    {
        for (int i = 0; i < 3; i++)
        {
            _produced++;
            yield return i;
        }
    }

    public static void Main()
    {
        // ---- Part 1: WHEN does the invalid-argument exception fire? ----
        Console.Write("validation throws at: ");
        try
        {
            var q = TakeFirst(Enumerable.Range(1, 10), -5);
            try
            {
                foreach (var _ in q) { }
                Console.WriteLine("never (bug!)");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("enumeration time (deferred - bug)");
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("call site (correct)");
        }

        // ---- Part 2: how many times does the producer run for two reads? ----
        _produced = 0;
        var produced = Produce();
        int sum1 = produced.Sum();
        int sum2 = produced.Sum();
        Console.WriteLine($"sum1={sum1}, sum2={sum2}, producer ran {_produced} times");
    }
}
