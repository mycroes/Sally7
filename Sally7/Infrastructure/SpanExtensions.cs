using System;
using System.Runtime.InteropServices;

namespace Sally7.Infrastructure
{
    internal static class SpanExtensions
    {
        public static ref T Struct<T>(this byte[] buffer, int offset) where T : struct =>
            ref buffer.AsSpan().Struct<T>(offset);

        public static ref T Struct<T>(this Span<byte> span, int offset) where T : struct =>
            ref MemoryMarshal.Cast<byte, T>(span.Slice(offset))[0];
    }
}
