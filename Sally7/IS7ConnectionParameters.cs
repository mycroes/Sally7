namespace Sally7
{
    /// <summary>
    /// The connection parameters that were negotiated during connection initialization.
    /// </summary>
    public interface IS7ConnectionParameters
    {
        /// <summary>
        /// The maximum PDU size.
        /// </summary>
        int MaximumPduSize { get; }

        /// <summary>
        /// The maximum number of concurrent requests.
        /// </summary>
        int MaximumNumberOfConcurrentRequests { get; }
    }
}