namespace Sally7.Internal
{
    internal class S7ConnectionParameters : IS7ConnectionParameters
    {
        public int MaximumPduSize { get; }

        public int MaximumNumberOfConcurrentRequests { get; }

        public S7ConnectionParameters(int maximumPduSize, int maximumNumberOfConcurrentRequests)
        {
            MaximumPduSize = maximumPduSize;
            MaximumNumberOfConcurrentRequests = maximumNumberOfConcurrentRequests;
        }
    }
}