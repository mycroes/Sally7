namespace Sally7.Internal
{
    internal static class IntExtensions
    {
        public static int CeilToEven(this in int input)
        {
            if ((input & 1) == 1) return input + 1;

            return input;
        }
    }
}
