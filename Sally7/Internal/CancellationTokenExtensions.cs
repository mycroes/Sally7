using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sally7.Internal
{
    internal static class CancellationTokenExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationTokenRegistration MaybeUnsafeRegister(this CancellationToken cancellationToken,
            Action callback)
        {
#if NET5_0_OR_GREATER
            return cancellationToken.UnsafeRegister(_ => callback.Invoke(), null);
#else
            return cancellationToken.Register(callback);
#endif
        }
    }
}
