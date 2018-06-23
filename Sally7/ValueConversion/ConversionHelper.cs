using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sally7.ValueConversion
{
    internal static class ConversionHelper
    {
        private static readonly MethodInfo sizeOfMethod = typeof(Unsafe).GetMethod(nameof(Unsafe.SizeOf));

        public static int SizeOf(Type type)
        {
            return (int) sizeOfMethod.MakeGenericMethod(type).Invoke(null, Array.Empty<object>());
        }
    }
}