// =============================================================================
// SOLUTION 01 — The Dispose Pattern
// =============================================================================
//
// Each fix below is marked with `// FIX:` and references the bug it addresses.
// This is the canonical "full" Dispose pattern for a class that owns BOTH
// managed and unmanaged resources and is NOT sealed (so it must support
// derived classes extending the cleanup).
// =============================================================================

using System;
using System.IO;
using System.Runtime.InteropServices;

class MyClass : IDisposable
{
    private IntPtr _nativeBuffer;
    private SafeHandle _handle;
    private Stream _stream;
    private bool _disposed = false;

    public MyClass()
    {
        _nativeBuffer = Helper.AllocateBuffer();
        _handle = Helper.GetHandle();
        _stream = Helper.OpenStream();
    }

    // FIX #1: A finalizer must pass `false`. During finalization the managed
    //         objects (_handle, _stream) may already have been finalized, so we
    //         must NOT touch them — only the unmanaged buffer is released.
    ~MyClass()
    {
        Dispose(false);
    }

    // FIX #2: Dispose() is non-virtual. Derived classes customise cleanup by
    //         overriding Dispose(bool), never by re-implementing Dispose().
    // FIX #3: Suppress finalization once disposed deterministically — otherwise
    //         the finalizer runs later and frees the native buffer a 2nd time.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // FIX #4: `protected virtual` (was `private`) so a derived class can
    //         override and extend the disposal logic via base.Dispose(bool).
    protected virtual void Dispose(bool disposing)
    {
        // FIX #5: Guard against double dispose. Main disposes explicitly AND via
        //         `using`, so this method is entered twice — the second pass
        //         must be a no-op (and must not free the native buffer twice).
        if (_disposed)
            return;

        if (disposing)
        {
            // FIX #6: Release ALL managed resources. The original code leaked
            //         _stream — only _handle was disposed.
            _handle?.Dispose();
            _stream?.Dispose();
        }

        // Unmanaged cleanup runs on BOTH the dispose and the finalize path.
        Helper.DeallocateBuffer(_nativeBuffer);
        _nativeBuffer = IntPtr.Zero; // defensive: prevents any chance of double-free

        _disposed = true;
    }

    public void DoSomething()
    {
        // FIX #7: Reject use-after-dispose. (.NET 7+ helper.)
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine("MyClass.DoSomething() called.");
    }
}

public class Program
{
    public static void Main()
    {
        using (var myClass = new MyClass())
        {
            myClass.DoSomething();

            // Explicit dispose is now idempotent and safe to combine with `using`.
            myClass.Dispose();

            try
            {
                myClass.DoSomething(); // now correctly throws
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"Correctly rejected use-after-dispose: {ex.GetType().Name}");
            }
        } // `using` calls Dispose() a 2nd time — the _disposed guard makes it a no-op.

        Console.WriteLine("Done — disposed exactly once, no double free, stream released.");
    }
}

static class Helper
{
    public static IntPtr AllocateBuffer() => IntPtr.Zero;
    public static void DeallocateBuffer(IntPtr ptr) { }
    public static SafeHandle GetHandle() =>
        new Microsoft.Win32.SafeHandles.SafeFileHandle(IntPtr.Zero, true);
    public static Stream OpenStream() => Stream.Null;
}
