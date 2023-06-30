using System.Runtime.CompilerServices;

namespace Sally7.Internal;

internal static class TypeExtensions
{
    public static ref byte GetOffset(this ref byte destination, uint offset)
    {
#if NET6_0_OR_GREATER
        return ref Unsafe.Add(ref destination, offset);
#else
        return ref Unsafe.Add(ref destination, (int) offset);
#endif
    }
}