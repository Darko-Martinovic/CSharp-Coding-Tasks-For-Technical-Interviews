// =============================================================================
// SOLUTION 16 — Iterator (yield) Semantics
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static int _produced = 0;

    // FIX #1: a NON-iterator outer method validates eagerly, then returns the
    //         lazy iterator (a local function here). Because the outer method has
    //         no `yield`, its body — including the guard — runs at call time.
    static IEnumerable<int> TakeFirst(IEnumerable<int> source, int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        return Iterator(source, count);

        static IEnumerable<int> Iterator(IEnumerable<int> source, int count)
        {
            int i = 0;
            foreach (var x in source)
            {
                if (i++ >= count) yield break;
                yield return x;
            }
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
        // ---- Part 1 ----
        Console.Write("validation throws at: ");
        try
        {
            var q = TakeFirst(Enumerable.Range(1, 10), -5); // now throws HERE
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

        // ---- Part 2 ----
        // FIX #2: materialise once at the consumer. The lazy Produce() can't cache
        //         itself, so the caller decides: enumerate once into a list, then
        //         read the list as many times as needed.
        _produced = 0;
        var produced = Produce().ToList();
        int sum1 = produced.Sum();
        int sum2 = produced.Sum();
        Console.WriteLine($"sum1={sum1}, sum2={sum2}, producer ran {_produced} times");
    }
}
