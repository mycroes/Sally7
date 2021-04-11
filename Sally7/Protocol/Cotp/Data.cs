using System;
using System.Runtime.InteropServices;

namespace Sally7.Protocol.Cotp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Data
    {
        /// <summary>
        /// The size of the <see cref="Data"/> struct.
        /// <para>
        /// The data struct is not part of the PDU data and it's length should not be considered for the maximum PDU
        /// size negotiated with the PLC.
        /// </para>
        /// </summary>
        public const int Size = sizeof(byte) + // Length
            sizeof(byte) + // DataIdentifier
            sizeof(byte); // PduNumberAndEot

        /// <summary>
        /// The length of the data segment.
        /// </summary>
        public byte Length;

        /// <summary>
        /// The identifier for a data PDU.
        /// </summary>
        public byte DataIdentifier;

        /// <summary>
        /// The PDU number and end of transmission indication.
        /// <para>
        /// The PDU number is stored in the lower 7 bits. The first bit is used to indicate end of transmission.
        /// S7 PLC's only support single segment PDU's, so the end of transmission bit should always be set.
        /// </para>
        /// </summary>
        public byte PduNumberAndEot;

        public void Init()
        {
            Length = 2;
            DataIdentifier = 0b1111_0000;
            PduNumberAndEot = 0b1_000_0000;
        }

        public readonly void Assert()
        {
            if (Length != 2) throw new Exception($"Expected length of 2, received {Length}.");
            if (DataIdentifier != 0b1111_0000) throw new Exception($"Invalid DT identifier, expected {0b1111_0000}, received {DataIdentifier}.");
            if ((PduNumberAndEot & 0b1_000_0000) == 0) throw new Exception("Expected a single PDU return only.");
        }
    }
}