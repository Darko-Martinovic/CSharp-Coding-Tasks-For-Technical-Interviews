// =============================================================================
// TASK 09 — Event Subscription Memory Leak
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// Events hold STRONG references from publisher to subscriber. Two classic leak
// bugs are present; both are observable through Publisher.HandlerCount.
//
// THINGS TO SCRUTINISE:
//   1. Subscriber implements IDisposable but Dispose() does nothing. After
//      s2.Dispose(), the publisher still references it and OnTick keeps firing.
//      Make Dispose() detach the handler.
//   2. The lambda block subscribes and then tries to unsubscribe with a SECOND
//      lambda. Two lambdas are two different delegate instances, so `-=` removes
//      nothing. Fix it so the handler can actually be removed.
//
// EXPECTED (after fix):
//   RaiseTick(1):
//     S1 received 1
//     S2 received 1
//   handlers after s2.Dispose(): 1
//   RaiseTick(2):
//     S1 received 2
//   pub2 handlers after +=/-= lambda: 0
// =============================================================================

using System;

class Publisher
{
    public event EventHandler<int> Tick;
    public int HandlerCount => Tick?.GetInvocationList().Length ?? 0;
    public void RaiseTick(int n) => Tick?.Invoke(this, n);
}

class Subscriber : IDisposable
{
    private readonly Publisher _pub;
    private readonly string _name;

    public Subscriber(Publisher pub, string name)
    {
        _pub = pub;
        _name = name;
        _pub.Tick += OnTick;
    }

    private void OnTick(object sender, int n) => Console.WriteLine($"  {_name} received {n}");

    public void Dispose()
    {
    }
}

public class Program
{
    public static void Main()
    {
        var pub = new Publisher();
        var s1 = new Subscriber(pub, "S1");
        var s2 = new Subscriber(pub, "S2");

        Console.WriteLine("RaiseTick(1):");
        pub.RaiseTick(1);

        s2.Dispose();
        Console.WriteLine($"handlers after s2.Dispose(): {pub.HandlerCount}");

        Console.WriteLine("RaiseTick(2):");
        pub.RaiseTick(2);

        // Separate publisher to isolate the lambda bug.
        var pub2 = new Publisher();
        pub2.Tick += (s, n) => Console.WriteLine($"  [lambda] {n}");
        pub2.Tick -= (s, n) => Console.WriteLine($"  [lambda] {n}");
        Console.WriteLine($"pub2 handlers after +=/-= lambda: {pub2.HandlerCount}");

        GC.KeepAlive(s1);
    }
}
