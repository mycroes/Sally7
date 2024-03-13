using System;

namespace Sally7.Internal;

internal static class DisposableHelper
{
    public static void ThrowIf(bool condition, object instance)
    {
        void Throw() => throw new ObjectDisposedException(instance.GetType().FullName);

        if (condition) Throw();
    }
}