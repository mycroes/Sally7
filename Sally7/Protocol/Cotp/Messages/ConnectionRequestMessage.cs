using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sally7.Protocol.Cotp.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ConnectionRequestMessage
    {
        public static readonly byte Size = (byte) Unsafe.SizeOf<ConnectionRequestMessage>();

        public byte Length;

        public ConnectionRequest ConnectionRequest;
        public PduSizeParameter PduSizeParameter;
        public TsapParameter SourceTsapParameter;
        public TsapParameter DestinationTsapParameter;
        
        public void Init(in PduSizeParameter.PduSize pduSize, in Tsap sourceTsap, in Tsap destinationTsap)
        {
            Length = (byte) (Size - 1);
            ConnectionRequest.Init();
            PduSizeParameter.Init(pduSize);
            SourceTsapParameter.Init(ParameterCode.SourceTsap, sourceTsap);
            DestinationTsapParameter.Init(ParameterCode.DestinationTsap, destinationTsap);
        }
    }
}
