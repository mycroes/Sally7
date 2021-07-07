using System;
using System.Buffers;
using System.Net;
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
    public class S7Connection : IDisposable
    {
        private const int IsoOverTcpPort = 102;

        private readonly string host;
        private readonly Tsap sourceTsap;
        private readonly Tsap destinationTsap;
        private readonly RequestExecutorFactory executorFactory;
        private readonly MemoryPool<byte> memoryPool;

        private IRequestExecutor? requestExecutor;
        private int bufferSize;

        public static RequestExecutorFactory DefaultRequestExecutorFactory { get; set; } =
            conn => new ConcurrentRequestExecutor(conn);

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
        /// <param name="memoryPool">The memory pool used to allocate buffers.</param>
        /// <param name="executorFactory">
        /// The factory used to create an executor after the connection is initialized.
        /// </param>
        public S7Connection(in string host, in Tsap sourceTsap, in Tsap destinationTsap, in MemoryPool<byte>? memoryPool = default, in RequestExecutorFactory? executorFactory = default)
        {
            this.host = host;
            this.sourceTsap = sourceTsap;
            this.destinationTsap = destinationTsap;
            this.memoryPool = memoryPool ?? MemoryPool<byte>.Shared;
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
        public S7Connection(in string host, in Tsap sourceTsap, in Tsap destinationTsap) : this(host, sourceTsap,
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
            (requestExecutor as IDisposable)?.Dispose();
            ((IDisposable) TcpClient).Dispose();
        }

        public async Task OpenAsync()
        {
            await TcpClient.ConnectAsync(host, IsoOverTcpPort).ConfigureAwait(false);
            await openAsync();
        }

        public async Task OpenAsync(double connectionTimeout)
        {
            var cancelTask = Task.Delay(TimeSpan.FromMilliseconds(connectionTimeout));
            var connectTask = TcpClient.ConnectAsync(host, IsoOverTcpPort);

            if (await Task.WhenAny(connectTask, cancelTask).ConfigureAwait(false) == cancelTask)
            {
                throw new TimeoutException($"Failed to connect within '{connectionTimeout}' milliseconds");
            }

            await openAsync();
        }

        async Task openAsync()
        {
            var stream = TcpClient.GetStream();

            // TODO: use memory from the pool
            var buffer = new byte[100];

            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildConnectRequest(buffer, sourceTsap, destinationTsap))
                .ConfigureAwait(false);
            var length = await ReadTpktAsync(stream, buffer).ConfigureAwait(false);
            S7ConnectionHelpers.ParseConnectionConfirm(buffer.AsSpan().Slice(0, length));

            await stream.WriteAsync(buffer, 0, S7ConnectionHelpers.BuildCommunicationSetup(buffer)).ConfigureAwait(false);
            length = await ReadTpktAsync(stream, buffer).ConfigureAwait(false);
            S7ConnectionHelpers.ParseCommunicationSetup(buffer.AsSpan().Slice(0, length), out var pduSize, out var maxRequests);

            Parameters = new S7ConnectionParameters(pduSize, maxRequests);
            bufferSize = Parameters.GetRequiredBufferSize();

            requestExecutor = executorFactory.Invoke(this);
        }

        public async Task ReadAsync(params IDataItem[] dataItems)
        {
            var executor = GetExecutorOrThrow();

            using var mo = memoryPool.Rent(bufferSize);
            var mem = mo.Memory;
            var length = S7ConnectionHelpers.BuildReadRequest(mem.Span, dataItems);

            // The response will only be written after the request has been sent. At that point we no longer
            // care about the request contents, so we use a single buffer only.
            var response = await executor.PerformRequest(mem.Slice(0, length), mem).ConfigureAwait(false);

            S7ConnectionHelpers.ParseReadResponse(response.Span, dataItems);
        }

        public async Task WriteAsync(params IDataItem[] dataItems)
        {
            var executor = GetExecutorOrThrow();

            using var mo = memoryPool.Rent(bufferSize);
            var mem = mo.Memory;
            var length = S7ConnectionHelpers.BuildWriteRequest(mem.Span, dataItems);

            // The response will only be written after the request has been sent. At that point we no longer
            // care about the request contents, so we use a single buffer only.
            var response = await executor.PerformRequest(mem.Slice(0, length), mem).ConfigureAwait(false);

            S7ConnectionHelpers.ParseWriteResponse(response.Span, dataItems);
        }

        private IRequestExecutor GetExecutorOrThrow()
        {
            return requestExecutor ??
                throw new InvalidOperationException("Can't perform read when the connection is not yet open.");
        }

        private static async Task<int> ReadTpktAsync(NetworkStream stream, byte[] buffer)
        {
            var len = await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false);
            if (len < 4)
                throw new Exception($"Error while reading TPKT header, expected 4 bytes but received {len}.");

            var tpkt = buffer.AsSpan().Struct<Tpkt>(0);
            tpkt.Assert();
            var msgLen = tpkt.MessageLength();
            len = await stream.ReadAsync(buffer, 4, msgLen).ConfigureAwait(false);
            if (len != msgLen)
            {
                throw new Exception($"Error while reading TPKT data, expected {msgLen} bytes but received {len}.");
            }

            return tpkt.Length;
        }
    }
}
