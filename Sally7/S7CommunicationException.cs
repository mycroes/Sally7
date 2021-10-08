using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Sally7
{
    /// <summary>
    /// The exception that is thrown when a communication error is detected.
    /// </summary>
    /// <remarks>
    /// When this exception is thrown the connection should no longer be considered usable for further requests.
    /// </remarks>
    [Serializable]
    public class S7CommunicationException : Sally7Exception
    {
        /// <summary>
        /// Gets the data that caused this exception, if known.
        /// </summary>
        public byte[]? ReceivedData { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7CommunicationException"/> class.
        /// </summary>
        public S7CommunicationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7CommunicationException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public S7CommunicationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7CommunicationException"/> class with a specified
        /// error message and the received data that caused this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="receivedData">The received data that caused this exception.</param>
        public S7CommunicationException(string message, byte[] receivedData)
            : base(message)
        {
            ReceivedData = receivedData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7CommunicationException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException"/>
        /// parameter is not a null reference, the current exception is raised in a <langword>catch</langword>
        /// block that handles the inner exception.
        /// </param>
        public S7CommunicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7CommunicationException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException"/>
        /// parameter is not a null reference, the current exception is raised in a <langword>catch</langword>
        /// block that handles the inner exception.
        /// </param>
        /// <param name="receivedData">The received data that caused this exception.</param>
        public S7CommunicationException(string message, Exception innerException, byte[] receivedData)
            : base(message, innerException)
        {
            ReceivedData = receivedData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7CommunicationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected S7CommunicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ReceivedData = (byte[]) info.GetValue(nameof(ReceivedData), typeof(byte[]));
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(ReceivedData), ReceivedData, typeof(byte[]));
        }

        internal static void ThrowFailedToParseResponse(ReadOnlySpan<byte> span, Exception inner)
        {
            byte[] data = span.ToArray();

            throw new S7CommunicationException(
                $"Failed to parse TPKT from response ({string.Join(", ", data.Select(b => b.ToString("X2")))}), " +
                $"see the {nameof(InnerException)} property for details.", inner, data);
        }

        internal static void ThrowInvalidJobID(byte replyJobId, ReadOnlyMemory<byte> message)
            => throw new S7CommunicationException($"Received invalid job ID '{replyJobId}' in response from PLC.", message.ToArray());
    }
}
