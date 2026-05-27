// =============================================================================
// TASK 12 — Generic Type Constraints
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// THIS PROGRAM DOES NOT COMPILE. Repository<T> uses three capabilities that an
// unconstrained T does not have. Add the minimal set of constraints to T so it
// compiles — without changing the method bodies or Main.
//
// THINGS TO SCRUTINISE:
//   1. CreateDefault calls `new T()`. Which constraint allows that?
//   2. Max calls `i.CompareTo(max)`. Which constraint provides CompareTo?
//   3. TryGetFirst assigns `result = null`. Why is that illegal for an
//      unconstrained T (it might be a non-nullable value type), and which
//      constraint makes it legal?
//
// The demo type Money satisfies whatever you add. Constraint order matters:
// `class` (or `struct`) first, then interfaces, then `new()` last.
//
// EXPECTED (after fix):
//   max: $20.00
//   default: $0.00
//   found Bob: True
//   found Zoe: False
// =============================================================================

using System;
using System.Collections.Generic;

class Money : IComparable<Money>
{
    public int Cents { get; set; }
    public string Owner { get; set; }

    public Money() { }
    public Money(int cents, string owner) { Cents = cents; Owner = owner; }

    public int CompareTo(Money other) => Cents.CompareTo(other.Cents);
    public override string ToString() => $"${Cents / 100.0:0.00}";
}

class Repository<T>
{
    private readonly List<T> _items = new();

    public void Add(T item) => _items.Add(item);

    public T CreateDefault() => new T();

    public T Max()
    {
        T max = _items[0];
        foreach (var i in _items)
            if (i.CompareTo(max) > 0) max = i;
        return max;
    }

    public bool TryGetFirst(Func<T, bool> predicate, out T result)
    {
        foreach (var i in _items)
            if (predicate(i)) { result = i; return true; }
        result = null;
        return false;
    }
}

public class Program
{
    public static void Main()
    {
        var repo = new Repository<Money>();
        repo.Add(new Money(150, "Alice"));
        repo.Add(new Money(2000, "Bob"));
        repo.Add(new Money(99, "Carol"));

        bool foundBob = repo.TryGetFirst(m => m.Owner == "Bob", out _);
        bool foundZoe = repo.TryGetFirst(m => m.Owner == "Zoe", out _);

        Console.WriteLine($"max: {repo.Max()}");
        Console.WriteLine($"default: {repo.CreateDefault()}");
        Console.WriteLine($"found Bob: {foundBob}");
        Console.WriteLine($"found Zoe: {foundZoe}");
    }
}
