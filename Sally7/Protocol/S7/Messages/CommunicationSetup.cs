using System;
using System.Runtime.CompilerServices;

namespace Sally7.Protocol.S7.Messages
{
    internal struct CommunicationSetup
    {
        public static readonly byte Size = (byte) Unsafe.SizeOf<CommunicationSetup>();

        public FunctionCode FunctionCode;
        public byte Reserved;
        public BigEndianShort MaxAmqCaller;
        public BigEndianShort MaxAmqCallee;
        public BigEndianShort PduSize;

        public void Init(BigEndianShort maxAmqCaller, BigEndianShort maxAmqCallee, BigEndianShort pduSize)
        {
            FunctionCode = FunctionCode.CommunicationSetup;
            Reserved = 0;
            MaxAmqCaller = maxAmqCaller;
            MaxAmqCallee = maxAmqCallee;
            PduSize = pduSize;
        }

        public void Assert(FunctionCode functionCode)
        {
            if (FunctionCode != functionCode) throw new Exception($"Expected function code {functionCode}, received {FunctionCode}.");
            if (Reserved != 0) throw new Exception($"Expected reserved 0, received {Reserved}");
        }
    }
}