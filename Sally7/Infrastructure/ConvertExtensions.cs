using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sally7.Infrastructure
{
    internal static class ConvertExtensions
    {
        public static ref T AsStruct<T>(this ref byte destination) where T : struct =>
            ref Unsafe.As<byte, T>(ref destination);

        public static ref T Struct<T>(this Span<byte> span, int offset) where T : struct =>
            ref MemoryMarshal.Cast<byte, T>(span.Slice(offset))[0];

        public static ref readonly T Struct<T>(this ReadOnlySpan<byte> span, int offset) where T : struct =>
            ref MemoryMarshal.Cast<byte, T>(span.Slice(offset))[0];
    }
}
