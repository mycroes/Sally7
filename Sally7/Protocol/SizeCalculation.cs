using Sally7.Internal;

namespace Sally7.Protocol
{
    /// <summary>
    /// Provides size declarations and calculations for parts of the protocol messages.
    /// </summary>
    public static class SizeCalculation
    {
        /// <inheritdoc cref="S7.DataItem.Size"/>
        public const int DataItemSize = S7.DataItem.Size;

        /// <inheritdoc cref="S7.RequestItem.Size"/>
        public const int RequestItemSize = S7.RequestItem.Size;

        /// <inheritdoc cref="IsoOverTcp.Tpkt.Size"/>
        public const int TpktSize = IsoOverTcp.Tpkt.Size;

        /// <inheritdoc cref="Cotp.Data.Size"/>
        public const int CotpDataSize = Cotp.Data.Size;

        /// <inheritdoc cref="S7.Messages.Header.Size" />
        public const int S7HeaderSize = S7.Messages.Header.Size;

        /// <summary>
        /// The size of the response header, for reads and writes.
        /// <para>
        /// The response header consists of the S7 <see cref="S7.Messages.Header" />, the
        /// <see cref="S7.FunctionCode" /> and a byte indicating the number of items read or
        /// written. The remaining length of the PDU is available for data item responses.
        /// </para>
        /// </summary>
        public const int ReadWriteResponseHeaderLength = S7HeaderSize
            + sizeof(S7.FunctionCode)
            + sizeof(byte); // The number of items in the response.

        /// <summary>
        /// The size of the request header, for reads and writes.
        /// <para>
        /// The request header consists of the S7 <see cref="S7.Messages.Header" /> without
        /// the error fields, the <see cref="S7.FunctionCode" /> and a byte indicating the number of
        /// items to read or write. The remaining length of the PDU is available for data items.
        /// </para>
        /// </summary>
        public const int ReadWriteRequestHeaderLength = S7HeaderSize - S7.Messages.Header.ErrorSize
            + sizeof(S7.FunctionCode)
            + sizeof(byte); // The number of items in the request.

        /// <summary>
        /// Gets the number of bytes required to transmit the dataItem. This includes the number of bytes required
        /// in the response to indicate the result of the read operation.
        /// </summary>
        /// <param name="dataItem">The data item to calculate the size for.</param>
        /// <returns>The number of bytes required in the read response for this data item.</returns>
        public static int GetDataItemReadTransmissionLength(in IDataItem dataItem)
        {
            return DataItemSize + dataItem.TransmissionLength.CeilToEven();
        }

        /// <summary>
        /// Gets the number of bytes required to transmit the dataItem in a write request. This includes the number
        /// of bytes required for addressing the data item.
        /// </summary>
        /// <param name="dataItem">The data item to calculate the size for.</param>
        /// <returns>The number of bytes required in the write request for this data item.</returns>
        public static int GetDataItemWriteTransmissionLength(in IDataItem dataItem)
        {
            return RequestItemSize + dataItem.TransmissionLength.CeilToEven();
        }
    }
}
