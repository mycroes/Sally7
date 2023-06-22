using Sally7.Protocol.S7.Messages;
using Sally7.RequestExecutor;

namespace Sally7.Internal;

internal static class WireFormatting
{
    private const int Tpkt = 0x03_00_00_00;

    /// <summary>
    /// Data struct.
    /// </summary>
    /// <remarks>
    /// This struct is only 3 bytes long, final byte is overwritten.
    /// </remarks>
    private const int Data = 2 << 24 // Length
        | 0b1111_0000 << 16 // Identifier
        | 0b1000_0000 << 8; // PDU number and EOT

    private const int JobRequestHeader1 = 0x32 << 24 // Protocol ID
        | (byte)MessageType.JobRequest << 16; // Message type

    /// <summary>
    /// The PDU reference.
    /// </summary>
    /// <remarks>
    /// This value is here for legacy reasons. The <see cref="ConcurrentRequestExecutor"/> modifies the lower
    /// byte to identify responses, the upper byte isn't actually used at the moment.
    /// </remarks>
    private const short PduRef = 1 << 8;

    public static int WriteTpkt(ref byte destination, int length)
    {
        return WriteInt32(ref destination, Tpkt | length);
    }

    public static int WriteData(ref byte destination)
    {
        // For performance we write an int instead of separate values, the final byte of the int will be overwritten.
        WriteInt32(ref destination, Data);

        return 3;
    }

    public static int WriteJobRequestHeader(ref byte destination, int paramLength, int dataLength)
    {
        WriteInt32(ref destination, JobRequestHeader1);
        // Legacy, the
        WriteInt16(ref destination.GetOffset(4), 1 << 8); // PDU ref
        WriteInt16(ref destination.GetOffset(6), (short)paramLength);
        WriteInt16(ref destination.GetOffset(8), (short)dataLength);

        return 10;
    }

    public static int WriteInt16(ref byte destination, short value)
    {
        NetworkOrderSerializer.WriteInt16(ref destination, value);
        return sizeof(short);
    }

    public static int WriteInt32(ref byte destination, int value)
    {
        NetworkOrderSerializer.WriteInt32(ref destination, value);
        return sizeof(int);
    }
}