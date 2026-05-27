// =============================================================================
// SOLUTION 12 — Generic Type Constraints
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

// FIX: the three constraints, in the required order (class/struct, interfaces, new()):
//   - `class`           -> lets `result = null` compile (T is a reference type).
//   - `IComparable<T>`  -> provides CompareTo for Max().
//   - `new()`           -> lets `new T()` compile in CreateDefault().
//
// MORE FLEXIBLE ALTERNATIVES (if you wanted to drop constraints):
//   - Use Comparer<T>.Default.Compare(i, max) instead of requiring IComparable<T>.
//   - Use `result = default;` instead of null to drop the `class` constraint.
class Repository<T> where T : class, IComparable<T>, new()
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
