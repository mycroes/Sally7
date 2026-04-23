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
using Sally7.Protocol.S7;
using Sally7.RequestExecutor;

namespace Sally7
{
    public sealed class S7Connection : IDisposable
    {
        /// <summary>
        /// The default timeout (5 seconds) for performing requests.
        /// Set the actual value using <see cref="RequestTimeout"/>.
        /// </summary>
        public static TimeSpan DefaultRequestTimeout => TimeSpan.FromSeconds(5);

        /// <summary>
        /// The default port number used for S7 communication.
        /// </summary>
        public const int DefaultPort = 102;

        private readonly string _host;
        private readonly int _port = DefaultPort;
        private readonly Tsap _sourceTsap;
        private readonly Tsap _destinationTsap;
        private readonly RequestExecutorFactory _executorFactory;

        private int _bufferSize;
        private MemoryPool<byte>? _memoryPool;
        private IRequestExecutor? _requestExecutor;

        public static RequestExecutorFactory DefaultRequestExecutorFactory { get; set; } =
            conn => new ConcurrentRequestExecutor(conn, conn._memoryPool);

        public TcpClient TcpClient { get; } = new() {NoDelay = true};

        public IS7ConnectionParameters? Parameters { get; private set; }

        private TimeSpan _requestTimeout = DefaultRequestTimeout;

        /// <summary>
        /// Gets or sets the timeout for performing requests.
        /// </summary>
        /// <remarks>
        /// The default value is <see cref="DefaultRequestTimeout"/>.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/>.TotalMilliseconds is less than -1 or greater than maximum allowed timer duration.
        /// </exception>
        public TimeSpan RequestTimeout
        {
            get => _requestTimeout;
            set
            {
                Assertions.AssertTimeoutIsValid(value);

                _requestTimeout = value;
            }
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
        /// <param name="memoryPool">The memory pool used to allocate buffers.</param>
        /// <param name="executorFactory">
        /// The factory used to create an executor after the connection is initialized.
        /// </param>
        public S7Connection(string host, Tsap sourceTsap, Tsap destinationTsap, MemoryPool<byte>? memoryPool = default, RequestExecutorFactory? executorFactory = default)
        {
            _host = host;
            _sourceTsap = sourceTsap;
            _destinationTsap = destinationTsap;
            _memoryPool = memoryPool;
            _executorFactory = executorFactory ?? DefaultRequestExecutorFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="S7Connection"/> class with a specified host, port,
        /// source TSAP and destination TSAP.
        ///
        /// Use the <see cref="Plc.ConnectionFactory"/> to create a connection using default TSAP values.
        /// </summary>
        /// <param name="host">The PLC host, specified as IP address or hostname.</param>
        /// <param name="port">The TCP port to connect to.</param>
        /// <param name="sourceTsap">The local TSAP for the connection.</param>
        /// <param name="destinationTsap">The remote TSAP for the connection.</param>
        /// <param name="memoryPool">The memory pool used to allocate buffers.</param>
        /// <param name="executorFactory">
        /// The factory used to create an executor after the connection is initialized.
        /// </param>
        public S7Connection(string host, int port, Tsap sourceTsap, Tsap destinationTsap, MemoryPool<byte>? memoryPool = default,
            RequestExecutorFactory? executorFactory = default)
        {
            _host = host;
            _port = port;
            _sourceTsap = sourceTsap;
            _destinationTsap = destinationTsap;
            _memoryPool = memoryPool;
            _executorFactory = executorFactory ?? DefaultRequestExecutorFactory;
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

        public void Close()
        {
            TcpClient.Close();
        }

        public void Dispose()
        {
            TcpClient.Dispose();
            _requestExecutor?.Dispose();

            if (_memoryPool is Sally7MemoryPool mp)
            {
                mp.Dispose();
            }
        }

        public async Task OpenAsync(CancellationToken cancellationToken = default)
        {
            using var linkedCts = CreateRequestTimeoutCancellationTokenSource(cancellationToken);
            var linkedToken = linkedCts.Token;

            try
            {
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                await
#endif
                using var closeOnCancel =
                    linkedToken.MaybeUnsafeRegister(SocketHelper.CloseSocketCallback, TcpClient.Client);

#if NET5_0_OR_GREATER
                await TcpClient.ConnectAsync(_host, _port, linkedToken).ConfigureAwait(false);
#else
                linkedToken.ThrowIfCancellationRequested();
                await TcpClient.ConnectAsync(_host, _port).ConfigureAwait(false);
#endif

                var stream = TcpClient.GetStream();

                var buffer = ArrayPool<byte>.Shared.Rent(100);
                try
                {
                    await stream.FrameworkSpecificWriteAsync(buffer, 0,
                            S7ConnectionHelpers.BuildConnectRequest(buffer, _sourceTsap, _destinationTsap),
                            cancellationToken)
                        .ConfigureAwait(false);

                    var length = await ReadTpktAsync(stream, buffer, linkedToken).ConfigureAwait(false);
                    S7ConnectionHelpers.ParseConnectionConfirm(buffer.AsSpan().Slice(0, length));

                    await stream
                        .FrameworkSpecificWriteAsync(buffer, 0, S7ConnectionHelpers.BuildCommunicationSetup(buffer), linkedToken)
                        .ConfigureAwait(false);

                    length = await ReadTpktAsync(stream, buffer, linkedToken).ConfigureAwait(false);
                    S7ConnectionHelpers.ParseCommunicationSetup(buffer.AsSpan().Slice(0, length), out var pduSize,
                        out var maxRequests);

                    Parameters = new S7ConnectionParameters(pduSize, maxRequests);
                    _bufferSize = Parameters.GetRequiredBufferSize();

                    _memoryPool ??= new Sally7MemoryPool(_bufferSize);

                    _requestExecutor = _executorFactory.Invoke(this);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                }

            }
            catch (Exception) when (linkedToken.IsCancellationRequested)
            {
                // The exception handling is quite generic, but exceptions thrown differ across target frameworks.
                // (See https://stackoverflow.com/a/66656805/1085457)
                // This is probably not something to worry about, since apparently cancellation was requested anyway.
                // Ensure user cancel gets an exception with their own token
                cancellationToken.ThrowIfCancellationRequested();

                // Not user requested, request timed out.
                Sally7Exception.ThrowTimeoutException();
            }
        }

        /// <summary>
        /// Performs read operations for the specified data items. If any operation returns an error code other than Success,
        /// an AggregateException is thrown containing a DataItemReadWriteException for each failed item with detailed information about the error.
        /// </summary>
        /// <param name="dataItems">The data items to read.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task ReadAsync(params IDataItem[] dataItems) => ReadAsync(dataItems, CancellationToken.None);

        /// <summary>
        /// Performs read operations for the specified data items. If any operation returns an error code other than Success,
        /// an AggregateException is thrown containing a DataItemReadWriteException for each failed item with detailed information about the error.
        /// </summary>
        /// <param name="dataItems">The data items to read.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ReadAsync(IDataItem[] dataItems, CancellationToken cancellationToken = default)
        {
            var results = ArrayPool<ReadWriteErrorCode>.Shared.Rent(dataItems.Length);
            await ReadAsync(dataItems, results, cancellationToken).ConfigureAwait(false);

            ReadWriteErrorHelpers.ThrowIfHasErrors("Read", dataItems, results.AsSpan(0, dataItems.Length));
        }

        /// <summary>
        /// Performs read operations for the specified data items and returns the results of each operation in the provided results array.
        /// Inspect the results array to check for errors, this method doesn't throw on individual item errors to allow for partial success.
        /// Use <see cref="ReadWriteErrorHelpers"/> to check the results and throw exceptions with detailed information if needed.
        /// The length of the results array needs to be at least the same as the length of the data items array.
        /// </summary>
        /// <param name="dataItems">The data items to read.</param>
        /// <param name="results">The array to store the results of each read operation.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ReadAsync(IDataItem[] dataItems, Memory<ReadWriteErrorCode> results,
            CancellationToken cancellationToken = default)
        {
            ThrowIfResultsTooShort(results.Length, dataItems.Length);

            using var linkedCts = CreateRequestTimeoutCancellationTokenSource(cancellationToken);
            var linkedToken = linkedCts.Token;

            IRequestExecutor executor = GetExecutorOrThrow();

            using IMemoryOwner<byte> mo = _memoryPool!.Rent(_bufferSize);
            Memory<byte> mem = mo.Memory;
            int length = S7ConnectionHelpers.BuildReadRequest(mem.Span, dataItems);

            try
            {
                Memory<byte> response = await executor.PerformRequest(mem.Slice(0, length), mem, linkedToken).ConfigureAwait(false);

                S7ConnectionHelpers.ParseReadResponse(response.Span, dataItems, results.Span);
            }
            catch (OperationCanceledException) when (linkedToken.IsCancellationRequested)
            {
                // Ensure user cancel gets an exception with their own token
                cancellationToken.ThrowIfCancellationRequested();

                // Not user requested, request timed out.
                Sally7Exception.ThrowTimeoutException();
            }
        }

        /// <summary>
        /// Performs write operations for the specified data items. If any operation returns an error code other than Success,
        /// an AggregateException is thrown containing a DataItemReadWriteException for each failed item with detailed information about the error.
        /// </summary>
        /// <param name="dataItems">The data items to write.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task WriteAsync(params IDataItem[] dataItems) => WriteAsync(dataItems, CancellationToken.None);

        /// <summary>
        /// Performs write operations for the specified data items. If any operation returns an error code other than Success,
        /// an AggregateException is thrown containing a DataItemReadWriteException for each failed item with detailed information about the error.
        /// </summary>
        /// <param name="dataItems">The data items to write.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task WriteAsync(IDataItem[] dataItems, CancellationToken cancellationToken = default)
        {
            var results = ArrayPool<ReadWriteErrorCode>.Shared.Rent(dataItems.Length);
            await WriteAsync(dataItems, results, cancellationToken).ConfigureAwait(false);

            ReadWriteErrorHelpers.ThrowIfHasErrors("Write", dataItems, results.AsSpan(0, dataItems.Length));
        }

        /// <summary>
        /// Performs write operations for the specified data items and returns the results of each operation in the provided results array.
        /// Inspect the results array to check for errors, this method doesn't throw on individual item errors to allow for partial success.
        /// Use <see cref="ReadWriteErrorHelpers"/> to check the results and throw exceptions with detailed information if needed.
        /// The length of the results array needs to be at least the same as the length of the data items array.
        /// </summary>
        /// <param name="dataItems">The data items to write.</param>
        /// <param name="results">The array to store the results of each write operation.</param>
        /// <param name="cancellationToken">The cancellation token to observe.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task WriteAsync(IDataItem[] dataItems, Memory<ReadWriteErrorCode> results,
            CancellationToken cancellationToken = default)
        {
            ThrowIfResultsTooShort(results.Length, dataItems.Length);

            IRequestExecutor executor = GetExecutorOrThrow();

            using IMemoryOwner<byte> mo = _memoryPool!.Rent(_bufferSize);
            Memory<byte> mem = mo.Memory;
            int length = S7ConnectionHelpers.BuildWriteRequest(mem.Span, dataItems);

            using var linkedCts = CreateRequestTimeoutCancellationTokenSource(cancellationToken);
            var linkedToken = linkedCts.Token;

            try
            {
                Memory<byte> response = await executor.PerformRequest(mem.Slice(0, length), mem, linkedToken)
                    .ConfigureAwait(false);

                S7ConnectionHelpers.ParseWriteResponse(response.Span, dataItems, results.Span);
            }
            catch (OperationCanceledException) when (linkedToken.IsCancellationRequested)
            {
                // Ensure user cancel gets an exception with their own token
                cancellationToken.ThrowIfCancellationRequested();

                // Not user requested, request timed out.
                Sally7Exception.ThrowTimeoutException();
            }
        }

        private static void ThrowIfResultsTooShort(int resultsLength, int dataItemsLength)
        {
            if (resultsLength >= dataItemsLength) return;

            Throw();
            [DoesNotReturn]
            static void Throw() => throw new ArgumentException("Results array length needs to be at least the same as the data items length.", "results");
        }

        private IRequestExecutor GetExecutorOrThrow()
        {
            if (_requestExecutor is null)
            {
                Throw();
                [DoesNotReturn]
                static void Throw() => throw new InvalidOperationException("Can't perform read when the connection is not yet open.");
            }

            return _requestExecutor;
        }

        private CancellationTokenSource CreateRequestTimeoutCancellationTokenSource(CancellationToken userToken)
        {
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(userToken);
            linkedCts.CancelAfter(RequestTimeout);

            return linkedCts;
        }

        private static async Task<int> ReadTpktAsync(NetworkStream stream, byte[] buffer, CancellationToken cancellationToken)
        {
            var len = await stream.FrameworkSpecificReadAsync(buffer, 0, 4, cancellationToken).ConfigureAwait(false);
            if (len < 4)
            {
                Throw(len);
                static void Throw(int len) => throw new Exception($"Error while reading TPKT header, expected 4 bytes but received {len}.");
            }

            var tpkt = buffer.AsSpan().Struct<Tpkt>(0);
            tpkt.Assert();
            var msgLen = tpkt.MessageLength();

            len = await stream.FrameworkSpecificReadAsync(buffer, 4, msgLen, cancellationToken).ConfigureAwait(false);
            if (len != msgLen)
            {
                TpktException.ThrowReadUnexptectedByteCount(msgLen, len);
            }

            return tpkt.Length;
        }
    }
}
