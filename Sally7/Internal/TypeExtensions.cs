using System.Runtime.CompilerServices;

namespace Sally7.Internal;

internal static class TypeExtensions
{
    public static ref byte GetOffset(this ref byte destination, int offset)
    {
        return ref Unsafe.Add(ref destination, offset);
    }
}