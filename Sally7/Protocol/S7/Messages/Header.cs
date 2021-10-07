using Sally7.Infrastructure;

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
                ThrowHelper.ThrowAssertFailProtocolID(ProtocolId);
            }

            if (Reserved.High != 0 || Reserved.Low != 0)
            {
                ThrowHelper.ThrowAssertFailReservedNot0(Reserved);
            }

            if ((MessageType == MessageType.AckData || MessageType == MessageType.Ack) &&
                (ErrorClass != HeaderErrorClass.NoError || ErrorCode != 0))
            {
                ThrowHelper.ThrowAssertFailCommunication(MessageType, ErrorClass, ErrorCode);
            }

            if (MessageType != messageType)
            {
                ThrowHelper.ThrowAssertFailMessageType(messageType, MessageType);
            }
        }
    }
}