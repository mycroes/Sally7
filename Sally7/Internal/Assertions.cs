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
    }
}
