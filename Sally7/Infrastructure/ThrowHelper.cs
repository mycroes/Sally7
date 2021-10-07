using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Sally7.Plc;
using Sally7.Protocol;
using Sally7.Protocol.S7;
using Sally7.Protocol.S7.Messages;

namespace Sally7.Infrastructure
{
    internal static class ThrowHelper
    {
        // exceptions are sorted alphabetically

        public static void ThrowAssertFailCommunication(MessageType messageType, HeaderErrorClass errorClass, byte errorCode)
        {
            throw new Exception(BuildErrorMessage(messageType, errorClass, errorCode));

            static string BuildErrorMessage(MessageType messageType, HeaderErrorClass errorClass, byte errorCode)
            {
                var sb = new StringBuilder("An error was returned during communication:").AppendLine().AppendLine();

                sb.Append("\tMessage type: ").Append(messageType).Append(" / 0x").AppendFormat("{0:X}", messageType).AppendLine();

                sb.Append("\tError class: ");
                if (Enum.IsDefined(typeof(HeaderErrorClass), errorClass)) sb.Append(errorClass).Append(" / ");
                sb.Append("0x").AppendFormat("{0:X}", errorClass).AppendLine();

                sb.Append("\tError code: 0x").AppendFormat("{0:X}", errorCode).AppendLine();

                var combinedErrorCode = (ParameterErrorCode)(((int)errorClass << 8) | errorCode);
                sb.Append("\tCombined error: ");
                if (Enum.IsDefined(typeof(ParameterErrorCode), combinedErrorCode))
                    sb.Append(combinedErrorCode).Append(" / ");

                sb.Append("0x").AppendFormat("{0:X}", combinedErrorCode).AppendLine();

                return sb.ToString();
            }
        }

        public static void ThrowAssertFailLengthReceived(byte length)
            => throw new Exception($"Expected length of 2, received {length}.");

        public static void ThrowAssertFailDTIdentifier(byte dataIdentifier)
            => throw new Exception($"Invalid DT identifier, expected {0b1111_0000}, received {dataIdentifier}.");

        public static void ThrowAssertFailFunctionCode(FunctionCode expected, FunctionCode actual)
            => throw new Exception($"Expected function code {expected}, received {actual}.");

        public static void ThrowAssertFailItemCount(int expected, int actual)
            => throw new Exception($"Expected ItemCount {expected}, received {actual}.");

        public static void ThrowAssertFailMessageType(MessageType expected, MessageType actual)
             => throw new Exception($"Expected message type {expected}, received {actual}.");

        public static void ThrowAssertFailProtocolID(byte actual)
            => throw new Exception($"Expected protocol ID {0x32}, received {actual}.");

        public static void ThrowAssertFailReservedNot0(byte reserved)
            => throw new Exception($"Expected reserved 0, received {reserved}");

        public static void ThrowAssertFailReservedNot0(BigEndianShort reserved)
            => throw new Exception($"Expected reserved 0, received {(int) reserved}");

        public static void ThrowAssertFailSinglePDU()
            => throw new Exception("Expected a single PDU return only.");

        [DoesNotReturn]
        public static void ThrowConnectionParametersNotSet()
            => throw new ArgumentException("The connection must be initialized and it's Parameters property must have a value.");

        public static void ThrowConnectionWasClosedWhileReading()
            => throw new Exception("Connection was closed while reading.");

        public static void ThrowFailedToInitJobChannel()
            => throw new Exception("Failed to initialize the job channel.");

        public static void ThrowFailedToInitReceivingChannel()
            => throw new Exception("Failed to initialize the receiving channel.");

        public static void ThrowFailedToReturnJobIDToPool(byte jobId)
            => throw new Exception($"Couldn't return job ID {jobId} to the pool.");

        public static void ThrowFailedToSignalReceivingChannel()
            => throw new Exception("Couldn't signal receive channel.");

        public static void ThrowFailedToInitSendingChannel()
            => throw new Exception("Failed to initialize the sending channel.");

        public static void ThrowFailedToSignalSendingChannel()
            => throw new Exception("Couldn't signal send channel.");

        public static void ThrowGetDestinationRackIsNull(CpuType cpuType, int? rack)
            => throw new ArgumentNullException(nameof(rack), $"CpuType {cpuType} requires a value for {nameof(rack)}.");

        public static void ThrowGetDestinationSlotIsNull(CpuType cpuType, int? slot)
            => throw new ArgumentNullException(nameof(slot), $"CpuType {cpuType} requires a value for {nameof(slot)}.");

        public static void ThrowMemoryWasNotArrayBased()
            => throw new Exception("Memory was not array based");

        public static void ThrowS7Communication(ReadOnlySpan<byte> span, Exception inner)
        {
            var data = span.ToArray();

            throw new S7CommunicationException(
                $"Failed to parse TPKT from response ({string.Join(", ", data.Select(b => b.ToString("X2")))}), " +
                $"see the {nameof(S7CommunicationException.InnerException)} property for details.", inner,
                data);
        }

        public static void ThrowS7CommunicationInvalidJobID(byte replyJobId, ReadOnlyMemory<byte> message)
            => throw new S7CommunicationException($"Received invalid job ID '{replyJobId}' in response from PLC.", message.ToArray());

        public static void ThrowSpecViolationConnectionConfirmDoesNotMatch()
            => throw new Exception("Spec violation, Connection Confirm doesn't match.");

        public static void ThrowSpecViolationConnectionConfirmFixedPart()
            => throw new Exception("Received data is smaller than Connection Confirm fixed part.");

        public static void ThrowSpecViolationOnlyClass0SupportedForTPKT()
            => throw new Exception("Spec violation: Only class 0 is supported for TPKT over TCP.");

        public static void ThrowSpecViolationReceivedDataSmallerThanCommunicationSetupSize()
            => throw new Exception("Received data is smaller than TPKT + DT PDU + S7 header + S7 communication setup size.");

        public static void ThrowSpecViolationResponseDoesNotMatchAckData(int bufferLength, int paramLength, int dataLength)
            => throw new Exception($"Length of response ({bufferLength}) does not match length of fixed part ({paramLength}) and data ({dataLength}) of S7 Ack Data.");

        public static void ThrowSpecViolationTPKTHeaderIncorrectVersion()
            => throw new Exception("Spec violation: TPKT header has incorrect version.");

        public static void ThrowSpecViolationTPKTLengthSmallerThan7()
            => throw new Exception("Spec violation: TPKT length is smaller than 7.");

        public static void ThrowSpecViolationTPKTReseveredNot0()
            => throw new Exception("Spec violoation: TPKT reserved is not 0.");

        public static void ThrowSpecViolationUnexpectedItemsInWriteResponse(int expected, int actual)
            => throw new Exception($"Expected {expected} items in write response, received {actual}.");

        public static void ThrowSpecViolationUnexpectedParameterLengthForRead(int paramLength)
            => throw new Exception($"Read returned unexpected parameter length {paramLength}");

        public static void ThrowSpecViolationUnexpectedParameterLengthForWrite(int paramLength)
            => throw new Exception($"Write returned unexpected parameter length {paramLength}");

        public static void ThrowTPKTReadingData(int msgLen, int len)
            => throw new Exception($"Error while reading TPKT data, expected {msgLen} bytes but received {len}.");

        public static void ThrowSocketException(SocketError socketError)
            => throw new SocketException((int) socketError);
    }
}
