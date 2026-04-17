using Sally7.Protocol.S7;

namespace Sally7;

/// <summary>
/// Exception thrown when a read or write operation on a data item returns an error code.
/// </summary>
/// <param name="dataItem">The data item that caused the error.</param>
/// <param name="errorCode">The error code returned by the operation.</param>
public class DataItemReadWriteException(IDataItem dataItem, ReadWriteErrorCode errorCode) :
    DataItemException(dataItem, $"Read/Write of dataItem {dataItem} returned {errorCode}")
{
    /// <summary>
    /// Gets the error code returned by the read or write operation.
    /// </summary>
    public ReadWriteErrorCode ErrorCode { get; } = errorCode;
}