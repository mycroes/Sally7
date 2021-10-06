using System;
using System.Text;

namespace Sally7.Protocol.S7.Messages
{
    internal struct Header
    {
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
            Reserved = default;
            PduRef = 1;
            ParamLength = paramLength;
            DataLength = dataLength;
        }

        public readonly void Assert(MessageType messageType)
        {
            if (ProtocolId != 0x32)
            {
                Throw(ProtocolId);
                static void Throw(byte actual) => throw new Exception($"Expected protocol ID {0x32}, received {actual}.");
            }

            if (Reserved.High != 0 || Reserved.Low != 0)
            {
                Throw(Reserved);
                static void Throw(BigEndianShort reserved) => throw new Exception($"Expected reserved 0, received {(int) reserved}");
            }

            if ((MessageType == MessageType.AckData || MessageType == MessageType.Ack) &&
                (ErrorClass != HeaderErrorClass.NoError || ErrorCode != 0))
            {
                Throw(MessageType, ErrorClass, ErrorCode);
                static void Throw(MessageType messageType, HeaderErrorClass errorClass, byte errorCode) => throw new Exception(BuildErrorMessage(messageType, errorClass, errorCode));
            }

            if (MessageType != messageType)
            {
                Throw(messageType, MessageType);
                static void Throw(MessageType expected, MessageType actual) => throw new Exception($"Expected message type {expected}, received {actual}.");
            }
        }

        private static string BuildErrorMessage(MessageType messageType, HeaderErrorClass errorClass, byte errorCode)
        {
            var sb = new StringBuilder("An error was returned during communication:").AppendLine().AppendLine();

            sb.Append("\tMessage type: ").Append(messageType).Append(" / 0x").AppendFormat("{0:X}", messageType).AppendLine();

            sb.Append("\tError class: ");
            if (Enum.IsDefined(typeof(HeaderErrorClass), errorClass)) sb.Append(errorClass).Append(" / ");
            sb.Append("0x").AppendFormat("{0:X}", errorClass).AppendLine();

            sb.Append("\tError code: 0x").AppendFormat("{0:X}", errorCode).AppendLine();

            var combinedErrorCode = (ParameterErrorCode) (((int)errorClass << 8) | errorCode);
            sb.Append("\tCombined error: ");
            if (Enum.IsDefined(typeof(ParameterErrorCode), combinedErrorCode))
                sb.Append(combinedErrorCode).Append(" / ");

            sb.Append("0x").AppendFormat("{0:X}", combinedErrorCode).AppendLine();

            return sb.ToString();
        }
    }
}