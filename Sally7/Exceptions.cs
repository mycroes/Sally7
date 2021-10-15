using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using System.Text;
using Sally7.Plc;
using Sally7.Protocol;
using Sally7.Protocol.S7;
using Sally7.Protocol.S7.Messages;

namespace Sally7
{
    [Serializable]
    public class Sally7Exception : Exception
    {
        public Sally7Exception() { }
        public Sally7Exception(string message) : base(message) { }
        public Sally7Exception(string message, Exception inner) : base(message, inner) { }
        protected Sally7Exception(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        internal static void ThrowSocketException(SocketError socketError)
            => throw new SocketException((int)socketError);

        internal static void ThrowFailedToInitJobPool()
            => throw new Sally7Exception("Failed to initialize the job pool.");

        internal static void ThrowFailedToInitReceivingSignal()
            => throw new Sally7Exception("Failed to initialize the receiving signal.");

        internal static void ThrowFailedToInitSendingSignal()
            => throw new Sally7Exception("Failed to initialize the sending signal.");

        internal static void ThrowFailedToReturnJobIDToPool(int jobId)
           => throw new Sally7Exception($"Couldn't return job ID {jobId} to the pool.");

        internal static void ThrowFailedToSignalReceiveDone()
           => throw new Sally7Exception("Couldn't signal receive done.");

        internal static void ThrowFailedToSignalSendDone()
            => throw new Sally7Exception("Couldn't signal send done.");

        internal static void ThrowMemoryWasNotArrayBased()
            => throw new Sally7Exception("Memory was not array based");

        internal static void ThrowMemoryRentedTooLarge(int bufferSize)
            => throw new ArgumentOutOfRangeException($"The requested size for the Memory is too large, max. allowed is {bufferSize}.");
    }

    [Serializable]
    internal class Sally7CommunicationSetupException : Sally7Exception
    {
        public Sally7CommunicationSetupException() { }
        public Sally7CommunicationSetupException(string message) : base(message) { }
        public Sally7CommunicationSetupException(string message, Exception inner) : base(message, inner) { }
        protected Sally7CommunicationSetupException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        [DoesNotReturn]
        internal static void ThrowConnectionParametersNotSet()
            => throw new ArgumentException("The connection must be initialized and it's Parameters property must have a value.");

        internal static void ThrowCpuTypeNotSupported(CpuType cpuType)
            => throw new ArgumentOutOfRangeException(nameof(cpuType), $"The cpu-type {cpuType} isn't supported.");

        internal static void ThrowDestinationRackIsNull(CpuType cpuType, int? rack)
            => throw new ArgumentNullException(nameof(rack), $"CpuType {cpuType} requires a value for {nameof(rack)}.");

        internal static void ThrowDestinationSlotIsNull(CpuType cpuType, int? slot)
            => throw new ArgumentNullException(nameof(slot), $"CpuType {cpuType} requires a value for {nameof(slot)}.");
    }

    [Serializable]
    public class CotpProtocolException : Sally7Exception
    {
        public CotpProtocolException() { }
        public CotpProtocolException(string message) : base(message) { }
        public CotpProtocolException(string message, Exception inner) : base(message, inner) { }
        protected CotpProtocolException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        internal static void ThrowSpecConnectionConfirmDoesNotMatch()
            => throw new CotpProtocolException("Spec violation, Connection Confirm doesn't match.");

        internal static void ThrowInvalidDTIdentifier(byte dataIdentifier)
            => throw new CotpProtocolException($"Invalid DT identifier, expected {0b1111_0000}, received {dataIdentifier}.");

        internal static void ThrowInvalidLength(byte length)
            => throw new CotpProtocolException($"Expected length of 2, received {length}.");

        internal static void ThrowNotASinglePDUReturn()
            => throw new CotpProtocolException("Expected a single PDU return only.");

        internal static void ThrowOnlyClass0SupportedForTPKT()
            => throw new CotpProtocolException("Spec violation: Only class 0 is supported for TPKT over TCP.");
    }

    [Serializable]
    public class S7ProtocolException : Sally7Exception
    {
        public S7ProtocolException() { }
        public S7ProtocolException(string message) : base(message) { }
        public S7ProtocolException(string message, Exception inner) : base(message, inner) { }
        protected S7ProtocolException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        internal static void ThrowDataItemCountExceedsParameterCount(int dataItemCount, int parameterCount)
            => throw new S7ProtocolException($"Protocol only allows {parameterCount} data items, given: {dataItemCount}");

        internal static void ThrowIncorrectMessageType(MessageType expected, MessageType actual)
             => throw new S7ProtocolException($"Expected message type {expected}, received {actual}.");

        internal static void ThrowIncorrectReserved(byte reserved)
           => throw new S7ProtocolException($"Expected reserved 0, received {reserved}");

        internal static void ThrowProtocolIDDoesNotMatch(byte actual)
           => throw new S7ProtocolException($"Expected protocol ID {0x32}, received {actual}.");

        internal static void ThrowReservedNot0(BigEndianShort reserved)
            => throw new S7ProtocolException($"Expected reserved 0, received {(int)reserved}");

        internal static void ThrowResponseDoesNotMatchAckData(int bufferLength, int paramLength, int dataLength)
            => throw new S7ProtocolException($"Length of response ({bufferLength}) does not match length of fixed part ({paramLength}) and data ({dataLength}) of S7 Ack Data.");

        internal static void ThrowReceivedDataSmallerThanCommunicationSetupSize()
            => throw new S7ProtocolException("Received data is smaller than TPKT + DT PDU + S7 header + S7 communication setup size.");

        internal static void ThrowReceivedDataSmallerThanConnectionConfirmFixedPart()
            => throw new S7ProtocolException("Received data is smaller than Connection Confirm fixed part.");

        internal static void ThrowUnexpectedFunctionCode(FunctionCode expected, FunctionCode actual)
            => throw new S7ProtocolException($"Expected function code {expected}, received {actual}.");

        internal static void ThrowUnexpectedItemCount(int expected, int actual)
            => throw new S7ProtocolException($"Expected ItemCount {expected}, received {actual}.");

        internal static void ThrowUnexpectedItemsInWriteResponse(int expected, int actual)
            => throw new S7ProtocolException($"Expected {expected} items in write response, received {actual}.");

        internal static void ThrowUnexpectedParameterLengthForRead(int paramLength)
           => throw new S7ProtocolException($"Read returned unexpected parameter length {paramLength}");

        internal static void ThrowUnexpectedParameterLengthForWrite(int paramLength)
            => throw new S7ProtocolException($"Write returned unexpected parameter length {paramLength}");

        internal static void ThrowCommunicationFailure(MessageType messageType, HeaderErrorClass errorClass, byte errorCode)
        {
            throw new S7ProtocolException(BuildErrorMessage(messageType, errorClass, errorCode));

            static string BuildErrorMessage(MessageType messageType, HeaderErrorClass errorClass, byte errorCode)
            {
                var sb = new StringBuilder("An error was returned during communication:").AppendLine().AppendLine();

                sb.Append("\tMessage type: ").Append(messageType).Append(" / 0x").AppendFormat("{0:X}", messageType).AppendLine();

                sb.Append("\tError class: ");
                if (Enum.IsDefined(typeof(HeaderErrorClass), errorClass))
                {
                    sb.Append(errorClass).Append(" / ");
                }
                sb.Append("0x").AppendFormat("{0:X}", errorClass).AppendLine();

                sb.Append("\tError code: 0x").AppendFormat("{0:X}", errorCode).AppendLine();

                var combinedErrorCode = (ParameterErrorCode)(((int)errorClass << 8) | errorCode);
                sb.Append("\tCombined error: ");
                if (Enum.IsDefined(typeof(ParameterErrorCode), combinedErrorCode))
                {
                    sb.Append(combinedErrorCode).Append(" / ");
                }
                sb.Append("0x").AppendFormat("{0:X}", combinedErrorCode).AppendLine();

                return sb.ToString();
            }
        }
    }

    [Serializable]
    public class TpktException : Sally7Exception
    {
        public TpktException() { }
        public TpktException(string message) : base(message) { }
        public TpktException(string message, Exception inner) : base(message, inner) { }
        protected TpktException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        internal static void ThrowIncorrectHeaderVersion()
            => throw new TpktException("Spec violation: TPKT header has incorrect version.");

        internal static void ThrowLengthSmallerThan7()
            => throw new TpktException("Spec violation: TPKT length is smaller than 7.");

        internal static void ThrowReadUnexptectedByteCount(int msgLen, int len)
            => throw new TpktException($"Error while reading TPKT data, expected {msgLen} bytes but received {len}.");

        internal static void ThrowReseveredNot0()
            => throw new TpktException("Spec violation: TPKT reserved is not 0.");

        internal static void ThrowConnectionWasClosedWhileReading()
           => throw new TpktException("Connection was closed while reading.");
    }
}

#if !NETSTANDARD2_1_OR_GREATER && !NET5_0_OR_GREATER
namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class DoesNotReturnAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class MemberNotNullAttribute : Attribute
    {
        public MemberNotNullAttribute(string member) : this(new[] { member }) { }
        public MemberNotNullAttribute(params string[] members) => Members = members;
        public string[] Members { get; }
    }
}
#endif
