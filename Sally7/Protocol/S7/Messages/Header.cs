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
        public HeaderErrorClass ErrorClass;
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

            if (MessageType == MessageType.AckData)
            {
                if (ErrorClass != HeaderErrorClass.NoError || ErrorCode != 0)
                {
                    var pec = (ParameterErrorCode) (((int) ErrorClass << 8) | ErrorCode);
                    throw new Exception(
                        $"An error occured: Error class: {ErrorClass}, Error code: {ErrorCode}, ParameterErrorCode: {pec}");
                }
            }
        }
    }
}