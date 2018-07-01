using System;

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
        public byte ErrorClass;
        public byte ErrorCode;

        public void Init(in MessageType messageType, in BigEndianShort paramLength, in BigEndianShort dataLength)
        {
            ProtocolId = 0x32;
            MessageType = messageType;
            Reserved.High = 0;
            Reserved.Low = 0;
            PduRef = 1;
            ParamLength = paramLength;
            DataLength = dataLength;
        }

        public void Assert(in MessageType messageType)
        {
            if (ProtocolId != 0x32) throw new Exception($"Expected protocol ID {0x32}, received {ProtocolId}.");
            if (MessageType != messageType) throw new Exception($"Expected message type {messageType}, received {MessageType}.");
            if (Reserved.High != 0 || Reserved.Low != 0)
                throw new Exception($"Expected reserved 0, received {(int) Reserved}");
            if (ErrorClass != 0 || ErrorCode != 0)
                throw new Exception($"An error was returned. Error class: {ErrorClass}, error code: {ErrorCode}");
        }
    }
}