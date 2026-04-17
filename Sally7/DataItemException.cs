using System;

namespace Sally7;

/// <summary>
/// Exception thrown for errors related to a specific data item, such as invalid configuration or read/write errors.
/// </summary>
/// <param name="dataItem">The data item that caused the error.</param>
/// <param name="message">The error message.</param>
public class DataItemException(IDataItem dataItem, string message) : Exception(
    message)
{
    /// <summary>
    /// Gets the data item that caused the error.
    /// </summary>
    public IDataItem DataItem { get; } = dataItem;
}