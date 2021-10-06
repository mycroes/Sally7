using System;
using System.Text;

namespace Sally7.Protocol.S7.Messages
{
    internal struct Header
    {
        /// <summary>
        /// The size of the <see cref="Header"/> struct.
        /// </summary>
        public const int Size = sizeof(byte) + // ProtocolId
            sizeof(MessageType) + // MessageType
            BigEndianShort.Size + // Reserved
            sizeof(short) + // PduRef
            BigEndianShort.Size + // ParamLength
            BigEndianShort.Size + // DataLength
            sizeof(HeaderErrorClass) + // ErrorClass
            sizeof(byte); // ErrorCode

        /// <summary>
        /// The size of the error data.
        /// <para>
        /// The S7 header in requests does not contain error data, subtract the <see cref="ErrorSize"/> to
        /// get the actual size of the request header.
        /// </para>
        /// </summary>
        public const int ErrorSize = sizeof(HeaderErrorClass) // ErrorClass
            + sizeof(byte); // ErrorCode

        public byte ProtocolId;
        public MessageType MessageType;
        public BigEndianShort Reserved;
        public short PduRef;
        public BigEndianShort ParamLength;
        public BigEndianShort DataLength;
        public HeaderErrorClass ErrorClass;
        public byte ErrorCode;

        public void Init(MessageType messageType, BigEndianShort paramLength, BigEndianShort dataLength)
        {
            ProtocolId = 0x32;
            MessageType = messageType;
            Reserved.High = 0;
            Reserved.Low = 0;
            PduRef = 1;
            ParamLength = paramLength;
            DataLength = dataLength;
        }

        public readonly void Assert(MessageType messageType)
        {
            if (ProtocolId != 0x32) throw new Exception($"Expected protocol ID {0x32}, received {ProtocolId}.");
            if (Reserved.High != 0 || Reserved.Low != 0)
                throw new Exception($"Expected reserved 0, received {(int) Reserved}");

            if ((MessageType == MessageType.AckData || MessageType == MessageType.Ack) &&
                (ErrorClass != HeaderErrorClass.NoError || ErrorCode != 0)) throw new Exception(BuildErrorMessage());

            if (MessageType != messageType)
                throw new Exception($"Expected message type {messageType}, received {MessageType}.");
        }

        private readonly string BuildErrorMessage()
        {
            var sb = new StringBuilder("An error was returned during communication:").AppendLine().AppendLine();

            sb.AppendLine($"\tMessage type: {MessageType} / 0x{MessageType:X}");

            sb.Append("\tError class: ");
            if (Enum.IsDefined(typeof(HeaderErrorClass), ErrorClass)) sb.Append(ErrorClass).Append(" / ");
            sb.AppendLine($"0x{ErrorClass:X}");

            sb.AppendLine($"\tError code: 0x{ErrorCode:X}");

            var combinedErrorCode = (ParameterErrorCode) (((int) ErrorClass << 8) | ErrorCode);
            sb.Append("\tCombined error: ");
            if (Enum.IsDefined(typeof(ParameterErrorCode), combinedErrorCode))
                sb.Append(combinedErrorCode).Append(" / ");

            sb.AppendLine($"0x{combinedErrorCode:X}");

            return sb.ToString();
        }
    }
}