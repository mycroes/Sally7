using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Sally7.Protocol.S7;

namespace Sally7;

/// <summary>
/// Helper methods for handling read/write errors on data items, including checking for errors and throwing exceptions with detailed information about the failed items.
/// </summary>
public static class ReadWriteErrorHelpers
{
    /// <summary>
    /// Checks if any of the read/write operations returned an error code other than Success.
    /// </summary>
    /// <param name="results">The results of the read/write operations.</param>
    /// <returns>True if any operation returned an error code other than Success; otherwise, false.</returns>
    public static bool HasErrors(ReadOnlySpan<ReadWriteErrorCode> results)
    {
#if NET7_0_OR_GREATER
        return MemoryMarshal.AsBytes(results).IndexOfAnyExcept((byte)ReadWriteErrorCode.Success) != -1;
#else
        for (var i = 0; i < results.Length; i++)
        {
            if (results[i] != ReadWriteErrorCode.Success) return true;
        }

        return false;
#endif
    }

    /// <summary>
    /// Checks the results of read/write operations and throws an AggregateException if any operation returned an error code other than Success.
    /// </summary>
    /// <param name="message">The error message to include in the exception.</param>
    /// <param name="dataItems">The data items involved in the operations.</param>
    /// <param name="results">The results of the read/write operations.</param>
    public static void ThrowIfHasErrors(
        string message,
        ReadOnlySpan<IDataItem> dataItems,
        ReadOnlySpan<ReadWriteErrorCode> results)
    {
        if (!HasErrors(results)) return;

        ThrowReadWriteException(message, dataItems, results);
    }

    /// <summary>
    /// Throws an AggregateException containing a DataItemReadWriteException for each data item that returned an error code other than Success.
    /// </summary>
    /// <param name="operation">The operation for which the error occurred.</param>
    /// <param name="dataItems">The data items involved in the operations.</param>
    /// <param name="results">The results of the read/write operations.</param>
    /// <exception cref="AggregateException">Thrown with a DataItemReadWriteException for each data item that returned an error code other than Success.</exception>
    [DoesNotReturn]
    public static void ThrowReadWriteException(string operation, ReadOnlySpan<IDataItem> dataItems,
        ReadOnlySpan<ReadWriteErrorCode> results)
    {
        List<Exception> exceptions = [];

        for (var i = 0; i < dataItems.Length; i++)
        {
            if (results[i] == ReadWriteErrorCode.Success) continue;

            exceptions.Add(new DataItemReadWriteException(dataItems[i], results[i], operation));
        }

        throw new AggregateException($"One or more errors occurred during {operation} operation.", exceptions);
    }
}