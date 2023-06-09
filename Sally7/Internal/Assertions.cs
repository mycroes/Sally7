using System;

namespace Sally7.Internal
{
    internal static class Assertions
    {
        public static void AssertTimeoutIsValid(TimeSpan value)
        {
            var totalMilliseconds = (long)value.TotalMilliseconds;
            if (totalMilliseconds is < -1 or > int.MaxValue)
            {
                ThrowTimeoutIsInvalid(value);
            }
        }

        private static void ThrowTimeoutIsInvalid(TimeSpan value) =>
            throw new ArgumentOutOfRangeException(nameof(value),
                $"The timeout {value.TotalMilliseconds}ms is less than -1 or greater than Int32.MaxValue.");

        public static void AssertDataItemLengthIsValidForType(int length, Type type)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    $"Length must be greater or equal to 1. Value provided was: {length}.");
            }

            if (type.IsValueType && length != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    $"Length can't be set for value of type {type} because it is of constant size. Value provided was: {length}.");
            }

            if (type == typeof(string) && length > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    $"Length of a string can't be greater than 255. Value provided was: {length}.");
            }
        }

        public static void AssertBitIsValidForType(int bit, Type type)
        {
            if (bit != 0 && type != typeof(bool))
            {
                throw new ArgumentException("Bit can only be set when value type is boolean.", nameof(bit));
            }

            if (bit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bit),
                    $"Bit value can't be less than 0. Value provided was: {bit}.");
            }

            if (bit > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bit),
                    $"Bit value can't be greater than 7 (increment {nameof(DataBlockDataItem<bool>.StartByte)} instead). Value provided was: {bit}.");
            }
        }
    }
}
