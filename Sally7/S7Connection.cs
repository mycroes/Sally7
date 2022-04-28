using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Sally7.Infrastructure;
using Sally7.Internal;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.IsoOverTcp;
using Sally7.RequestExecutor;

namespace Sally7
{
    public sealed class S7Connection : IDisposable
    {
        private const int IsoOverTcpPort = 102;

        private readonly string host;
        private readonly Tsap sourceTsap;
        private readonly Tsap destinationTsap;
        private readonly int tcpPort;
        private readonly RequestExecutorFactory executorFactory;

        private int bufferSize;
        private MemoryPool<byte>? memoryPool;
        private IRequestExecutor? requestExecutor;

        public static RequestExecutorFactory DefaultRequestExecutorFactory { get; set; } =
            conn => new ConcurrentRequestExecutor(conn, conn.memoryPool);

        public TcpClient TcpClient { get; } = new() {NoDelay = true};

        public IS7ConnectionParameters? Parameters { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7Connection"/> class with a specified host, source TSAP
        /// and destination TSAP.
        ///
        /// Use the <see cref="Plc.ConnectionFactory"/> to create a connection using default TSAP values.
        /// </summary>
        /// <param name="host">The PLC host, specified as IP address or hostname.</param>
        /// <param name="sourceTsap">The local TSAP for the connection.</param>
        /// <param name="destinationTsap">The remote TSAP for the connection.</param>
        /// <param name="tcpPort"></param>
        /// <param name="memoryPool">The memory pool used to allocate buffers.</param>
        /// <param name="executorFactory">
        /// The factory used to create an executor after the connection is initialized.
        /// </param>
        public S7Connection(string host, Tsap sourceTsap, Tsap destinationTsap, int? tcpPort, MemoryPool<byte>? memoryPool = default, RequestExecutorFactory? executorFactory = default)
        {
            this.host = host;
            this.sourceTsap = sourceTsap;
            this.destinationTsap = destinationTsap;
            this.tcpPort = tcpPort ?? IsoOverTcpPort;
            this.memoryPool = memoryPool;
            this.executorFactory = executorFactory ?? DefaultRequestExecutorFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7Connection"/> class with a specified host, source TSAP
        /// and destination TSAP.
        ///
        /// Use the <see cref="Plc.ConnectionFactory"/> to create a connection using default TSAP values.
        /// </summary>
        /// <param name="host">The PLC host, specified as IP address or hostname.</param>
        /// <param name="sourceTsap">The local TSAP for the connection.</param>
        /// <param name="destinationTsap">The remote TSAP for the connection.</param>
        public S7Connection(string host, Tsap sourceTsap, Tsap destinationTsap) : this(host, sourceTsap,
            destinationTsap, default)
        {
        }

        /// <summary>
        /// Gets or sets the ReceiveTimeout of the underlying <see cref="TcpClient"/>.
        /// </summary>
        public int ReceiveTimeout
        {
            get => TcpClient.ReceiveTimeout;
            set => TcpClient.ReceiveTimeout = value;
        }

        /// <summary>
        /// Gets or sets the SendTimeout of the underlying <see cref="TcpClient"/>.
        /// </summary>
        public int SendTimeout
        {
            get => TcpClient.SendTimeout;
            set => TcpClient.SendTimeout = value;
        }

        public void Close()
        {
            TcpClient.Close();
        }

        public void Dispose()
        {
            TcpClient.Dispose();
            requestExecutor?.Dispose();

            if (memoryPool is Sally7MemoryPool mp)
            {
                mp.Dispose();
            }
        }

        public async Task OpenAsync(CancellationToken cancellationToken = default)
        {
#if NET5_0_OR_GREATER
            await TcpClient.ConnectAsync(host, tcpPort, cancellationToken).ConfigureAwait(false);
#else
            cancellationToken.ThrowIfCancellationRequested();
            await TcpClient.ConnectAsync(host, tcpPort).ConfigureAwait(false);
#endif
            var stream = TcpClient.GetStream();

            var buffer = ArrayPool<byte>.Shared.Rent(100);

#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildConnectRequest(buffer, sourceTsap, destinationTsap), cancellationToken).ConfigureAwait(false);
#else
            cancellationToken.ThrowIfCancellationRequested();
            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildConnectRequest(buffer, sourceTsap, destinationTsap)).ConfigureAwait(false);
#endif
            var length = await ReadTpktAsync(stream, buffer, cancellationToken).ConfigureAwait(false);
            S7ConnectionHelpers.ParseConnectionConfirm(buffer.AsSpan().Slice(0, length));

#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildCommunicationSetup(buffer), cancellationToken).ConfigureAwait(false);
#else
            cancellationToken.ThrowIfCancellationRequested();
            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildCommunicationSetup(buffer)).ConfigureAwait(false);
#endif
            length = await ReadTpktAsync(stream, buffer, cancellationToken).ConfigureAwait(false);
            S7ConnectionHelpers.ParseCommunicationSetup(buffer.AsSpan().Slice(0, length), out var pduSize, out var maxRequests);

            Parameters = new S7ConnectionParameters(pduSize, maxRequests);
            bufferSize = Parameters.GetRequiredBufferSize();

            memoryPool ??= new Sally7MemoryPool(bufferSize);

            requestExecutor = executorFactory.Invoke(this);

            ArrayPool<byte>.Shared.Return(buffer);
        }

        public Task ReadAsync(params IDataItem[] dataItems) => ReadAsync(dataItems, CancellationToken.None);
        public async Task ReadAsync(IDataItem[] dataItems, CancellationToken cancellationToken = default)
        {
            IRequestExecutor executor = GetExecutorOrThrow();

            using IMemoryOwner<byte> mo = memoryPool!.Rent(bufferSize);
            Memory<byte> mem = mo.Memory;
            int length = S7ConnectionHelpers.BuildReadRequest(mem.Span, dataItems);

            // The response will only be written after the request has been sent. At that point we no longer
            // care about the request contents, so we use a single buffer only.
            Memory<byte> response = await executor.PerformRequest(mem.Slice(0, length), mem, cancellationToken).ConfigureAwait(false);

            S7ConnectionHelpers.ParseReadResponse(response.Span, dataItems);
        }

        public Task WriteAsync(params IDataItem[] dataItems) => WriteAsync(dataItems, CancellationToken.None);
        public async Task WriteAsync(IDataItem[] dataItems, CancellationToken cancellationToken = default)
        {
            IRequestExecutor executor = GetExecutorOrThrow();

            using IMemoryOwner<byte> mo = memoryPool!.Rent(bufferSize);
            Memory<byte> mem = mo.Memory;
            int length = S7ConnectionHelpers.BuildWriteRequest(mem.Span, dataItems);

            // The response will only be written after the request has been sent. At that point we no longer
            // care about the request contents, so we use a single buffer only.
            Memory<byte> response = await executor.PerformRequest(mem.Slice(0, length), mem, cancellationToken).ConfigureAwait(false);

            S7ConnectionHelpers.ParseWriteResponse(response.Span, dataItems);
        }

        private IRequestExecutor GetExecutorOrThrow()
        {
            if (requestExecutor is null)
            {
                Throw();
                [DoesNotReturn]
                static void Throw() => throw new InvalidOperationException("Can't perform read when the connection is not yet open.");
            }

            return requestExecutor;
        }

        private static async Task<int> ReadTpktAsync(NetworkStream stream, byte[] buffer, CancellationToken cancellationToken)
        {
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            var len = await stream.ReadAsync(buffer, 0, 4, cancellationToken).ConfigureAwait(false);
#else
            cancellationToken.ThrowIfCancellationRequested();
            var len = await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false);
#endif
            if (len < 4)
            {
                Throw(len);
                static void Throw(int len) => throw new Exception($"Error while reading TPKT header, expected 4 bytes but received {len}.");
            }

            var tpkt = buffer.AsSpan().Struct<Tpkt>(0);
            tpkt.Assert();
            var msgLen = tpkt.MessageLength();

#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            len = await stream.ReadAsync(buffer, 4, msgLen, cancellationToken).ConfigureAwait(false);
#else
            cancellationToken.ThrowIfCancellationRequested();
            len = await stream.ReadAsync(buffer, 4, msgLen).ConfigureAwait(false);
#endif
            if (len != msgLen)
            {
                TpktException.ThrowReadUnexptectedByteCount(msgLen, len);
            }

            return tpkt.Length;
        }
    }
}
