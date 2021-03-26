namespace Sally7
{
    /// <summary>
    /// Defines extension methods for the <see cref="IS7ConnectionParameters"/> interface.
    /// </summary>
    public static class S7ConnectionParameterExtensions
    {
        /// <summary>
        /// Returns the required buffer size to accomodate the maximum PDU size of the given connection parameters.
        /// </summary>
        /// <param name="parameters">The connection parameters defining the maximum PDU size.</param>
        /// <returns>The required buffer size for requests and responses.</returns>
        public static int GetRequiredBufferSize(this IS7ConnectionParameters parameters)
        {
            // TPKT + COTP DT + S7 PDU, assumes TPKT + COTP DT don't count as PDU data
            return parameters.MaximumPduSize + 7;
        }
    }
}