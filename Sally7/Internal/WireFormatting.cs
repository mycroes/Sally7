using Sally7.Protocol.S7.Messages;
using Sally7.RequestExecutor;

namespace Sally7.Internal;

internal static class WireFormatting
{
    private const uint Tpkt = 0x03_00_00_00;

    /// <summary>
    /// Data struct.
    /// </summary>
    /// <remarks>
    /// This struct is only 3 bytes long, final byte is overwritten.
    /// </remarks>
    private const int Data = 2 << 24 // Length
        | 0b1111_0000 << 16 // Identifier
        | 0b1000_0000 << 8; // PDU number and EOT

    /// <summary>
    /// The PDU reference.
    /// </summary>
    /// <remarks>
    /// This value is here for legacy reasons. The <see cref="ConcurrentRequestExecutor"/> modifies the lower
    /// byte to identify responses, the upper byte isn't actually used at the moment.
    /// </remarks>
    private const ushort PduRef = 1 << 8;

    private static ulong JobRequestHeader1 = 0x32L << 56 | (long)MessageType.JobRequest << 48 | PduRef << 16;

    public static uint WriteTpkt(ref byte destination, int length)
    {
        return WriteUInt32(ref destination, (uint) (Tpkt | length));
    }

    public static uint WriteData(ref byte destination)
    {
        // For performance we write an int instead of separate values, the final byte of the int will be overwritten.
        WriteUInt32(ref destination, Data);

        return 3;
    }

    public static uint WriteJobRequestHeader(ref byte destination, int paramLength, int dataLength)
    {
        var header = JobRequestHeader1 | (ushort) paramLength;

        NetworkOrderSerializer.WriteUInt64(ref destination, header);
        NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(8), (ushort)dataLength);

        return 10;
    }

    public static uint WriteUInt16(ref byte destination, ushort value)
    {
        NetworkOrderSerializer.WriteUInt16(ref destination, value);
        return sizeof(ushort);
    }

    public static uint WriteUInt32(ref byte destination, uint value)
    {
        NetworkOrderSerializer.WriteUInt32(ref destination, value);
        return sizeof(uint);
    }
}