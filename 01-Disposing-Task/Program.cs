// =============================================================================
// TASK 01 — The Dispose Pattern
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// The class below owns THREE resources: an unmanaged native buffer (IntPtr),
// a SafeHandle, and a managed Stream. It attempts to follow the IDisposable
// pattern, but the implementation is wrong in several distinct ways.
//
// GOAL: Fix ALL the bugs so the class correctly implements the canonical
//       Dispose pattern. There are at least 6 separate problems, spanning the
//       finalizer, Dispose(), Dispose(bool), the resource cleanup itself,
//       double-dispose safety, and use-after-dispose protection.
//
// THINGS TO SCRUTINISE:
//   1. What argument should a finalizer pass to Dispose(bool)?
//   2. After Dispose() runs, should the finalizer still run?
//   3. Can a DERIVED class override the right method? (accessibility + virtual)
//   4. Are ALL three resources actually released?
//   5. Is it safe to call Dispose() twice? (Main calls it twice.)
//   6. What should happen if a method is used AFTER the object is disposed?
//
// NOTE: The code compiles and runs as-is — the bugs are behavioural, not
//       syntactic. As written, DoSomething() prints twice, including after
//       the object has already been disposed.
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

    ~MyClass()
    {
        Dispose(true);
    }

    public virtual void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        Helper.DeallocateBuffer(_nativeBuffer);
        if (disposing)
        {
            _handle?.Dispose();
        }
        _disposed = true;
    }

    public void DoSomething()
    {
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
            myClass.Dispose();
            myClass.DoSomething();
        }
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
