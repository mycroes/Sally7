using System;
using System.Threading;

namespace Sally7.Internal
{
    internal static class CancellationTokenExtensions
    {
        public static CancellationTokenRegistration MaybeUnsafeRegister(this CancellationToken cancellationToken,
            Action<object?> callback, object? state)
        {
#if NET5_0_OR_GREATER
            return cancellationToken.UnsafeRegister(callback, state);
#else
            return cancellationToken.Register(callback, state);
#endif
        }
    }
}
