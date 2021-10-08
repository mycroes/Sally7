using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sally7.Infrastructure
{
    [DebuggerNonUserCode]
    internal sealed class Sally7MemoryPool : MemoryPool<byte>
    {
        private readonly int _bufferSize;
        private readonly ConcurrentQueue<MemoryHolder> _memories = new();

        private bool _isDisposed;   // volatile, etc. not needed as the lock in Dispose has release semantics

        public Sally7MemoryPool(int bufferSize) => _bufferSize = bufferSize;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            lock (_memories)
            {
                _isDisposed = true;

                if (disposing)
                {
                    // Empty the ConcurrentQueue to make life for GC easier
                    while (_memories.TryDequeue(out _)) { }
                }
            }
        }

        public override int MaxBufferSize => _bufferSize;

        public override IMemoryOwner<byte> Rent(int size = -1)
        {
            Debug.Assert(!_isDisposed);

            if (size > _bufferSize)
            {
                Sally7Exception.ThrowMemoryRentedTooLarge(_bufferSize);
            }

            return _memories.TryDequeue(out MemoryHolder? memory)
                ? memory
                : new MemoryHolder(this, _bufferSize);
        }

        private void Return(MemoryHolder memoryHolder)
        {
            if (_isDisposed) return;

            _memories.Enqueue(memoryHolder);
        }

        [DebuggerNonUserCode]
        private sealed class MemoryHolder : IMemoryOwner<byte>
        {
            private readonly Sally7MemoryPool _pool;

            public MemoryHolder(Sally7MemoryPool pool, int bufferSize)
            {
                _pool = pool;

#if NET5_0_OR_GREATER
                // Allocate a pinned buffer in the special pinned object heap (POH).
                // This avoids the need to pin this buffer in the socket implementation.
                byte[] arr = GC.AllocateUninitializedArray<byte>(bufferSize, pinned: true);
                Memory = MemoryMarshal.CreateFromPinnedArray(arr, 0, arr.Length);
#else
                Memory = new byte[bufferSize];
#endif
            }

            public Memory<byte> Memory { get; }

            public void Dispose()
            {
#if DEBUG
                Memory.Span.Clear();
#endif
                _pool.Return(this);
            }
        }
    }
}
