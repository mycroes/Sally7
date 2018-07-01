using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    internal static class ConversionHelper
    {
        private static readonly MethodInfo sizeOfMethod = typeof(Unsafe).GetMethod(nameof(Unsafe.SizeOf));

        public static int SizeOf(in Type type)
        {
            return (int) sizeOfMethod.MakeGenericMethod(type).Invoke(null, new object[0]);
        }

        public static int GetElementSize<TValue>()
        {
            var type = typeof(TValue);
            if (type.IsValueType) return Unsafe.SizeOf<TValue>();
            if (type.IsArray) return SizeOf(type.GetElementType());
            if (type == typeof(string)) return 1;

            throw new NotImplementedException();
        }
    }
}