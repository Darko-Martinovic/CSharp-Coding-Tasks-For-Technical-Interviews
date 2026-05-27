// =============================================================================
// SOLUTION 09 — Event Subscription Memory Leak
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
        // FIX #1: detach the handler. Until this runs, the publisher's delegate
        //         keeps a strong reference to this Subscriber (the leak) and
        //         OnTick keeps firing for a logically-dead object.
        _pub.Tick -= OnTick;
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

        // FIX #2: keep ONE reference to the handler delegate so `-=` targets the
        //         exact same instance. Two separate lambdas are two different
        //         delegates, so subtracting a fresh lambda removes nothing.
        var pub2 = new Publisher();
        EventHandler<int> handler = (s, n) => Console.WriteLine($"  [lambda] {n}");
        pub2.Tick += handler;
        pub2.Tick -= handler;
        Console.WriteLine($"pub2 handlers after +=/-= lambda: {pub2.HandlerCount}");

        GC.KeepAlive(s1);
    }
}
