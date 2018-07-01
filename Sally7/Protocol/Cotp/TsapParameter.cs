using System.Runtime.InteropServices;

namespace Sally7.Protocol.Cotp
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TsapParameter
    {
        public ParameterCode Code;
        public byte Length;
        public Tsap Tsap;

        public void Init(in ParameterCode code, in Tsap tsap)
        {
            Code = code;
            Tsap = tsap;
            Length = 2;
        }
    }
}