#if NETSTANDARD2_1 || NET5_0_OR_GREATER

using System;
using System.Threading.Tasks;

namespace Sally7.RequestExecutor
{
    /// <summary>
    /// Defines method to execute requests on a <see cref="S7Connection"/>.
    /// </summary>
    public interface IRequestExecutorValueTask : IDisposable
    {
        /// <summary>
        /// The connection on which requests are executed by this executor.
        /// </summary>
        public S7ConnectionValueTask Connection { get; }

        /// <summary>
        /// Performs a request on the <see cref="S7Connection"/> this executor was created for.
        /// </summary>
        /// <remarks>
        /// The <paramref name="response"/> needs to be large enough to accomodate the full response message. The
        /// returned <see cref="Memory{T}"/> is a slice of the provided <paramref name="response"/> parameter.
        /// </remarks>
        /// <param name="request">The <see cref="ReadOnlyMemory{T}"/> that contains the request.</param>
        /// <param name="response">The <see cref="Memory{T}"/> that will be used to store the response.</param>
        /// <returns>The response as a slice of the supplied <paramref name="response"/> parameter.</returns>
        ValueTask<Memory<byte>> PerformRequest(ReadOnlyMemory<byte> request, Memory<byte> response);
    }
}

#endif
