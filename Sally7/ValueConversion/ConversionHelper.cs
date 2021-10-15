using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    internal static class ConversionHelper
    {
        private static readonly MethodInfo sizeOfMethod = typeof(Unsafe).GetMethod(nameof(Unsafe.SizeOf))!;

        public static int SizeOf(Type type)
        {
            return (int) sizeOfMethod.MakeGenericMethod(type).Invoke(null, Array.Empty<object>())!;
        }

        public static int GetElementSize<TValue>()
        {
            if (typeof(TValue).IsValueType) return Unsafe.SizeOf<TValue>();
            if (typeof(TValue).IsArray) return SizeOf(typeof(TValue).GetElementType()!);
            if (typeof(TValue) == typeof(string)) return 1;

            throw new NotImplementedException();
        }
    }
}