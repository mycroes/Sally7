using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Sally7.Internal
{
    internal static class NetworkStreamExtensions
    {
#if NET5_0_OR_GREATER
        public static ValueTask FrameworkSpecificWriteAsync(this NetworkStream stream, byte[] buffer, int offset, int size,
            CancellationToken cancellationToken)
        {
            return stream.WriteAsync(buffer.AsMemory(offset, size), cancellationToken);
        }
#else
        public static Task FrameworkSpecificWriteAsync(this NetworkStream stream, byte[] buffer, int offset, int size,
            CancellationToken cancellationToken)
        {
#if NETSTANDARD2_1_OR_GREATER
            return stream.WriteAsync(buffer, offset, size, cancellationToken);
#else
            cancellationToken.ThrowIfCancellationRequested();
            return stream.WriteAsync(buffer, offset, size, cancellationToken);
#endif
        }
#endif

#if NET5_0_OR_GREATER
        public static ValueTask<int> FrameworkSpecificReadAsync(this NetworkStream stream, byte[] buffer, int offset, int size,
            CancellationToken cancellationToken)
        {
            return stream.ReadAsync(buffer.AsMemory(offset, size), cancellationToken);
        }
#else
        public static Task<int> FrameworkSpecificReadAsync(this NetworkStream stream, byte[] buffer, int offset, int size,
            CancellationToken cancellationToken)
        {
#if NETSTANDARD2_1_OR_GREATER
            return stream.ReadAsync(buffer, offset, size, cancellationToken);
#else
            cancellationToken.ThrowIfCancellationRequested();
            return stream.ReadAsync(buffer, offset, size, cancellationToken);
#endif
        }
#endif
    }
}