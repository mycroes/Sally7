using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sally7.Internal;

namespace Sally7.Benchmarks.Serialization;

public static class SerializeArray
{
    public class SerializeUInt16
    {
        private readonly byte[] buffer = new byte[1024];

        [ParamsSource(nameof(ValueParameters))]
        public ushort[] Value { get; set; } = Array.Empty<ushort>();

        public IEnumerable<ushort[]> ValueParameters =>
            Enumerable.Range(1, 12).Select(l => Enumerable.Range(1, l).Select(x => (ushort)x).ToArray());

        [Benchmark(Baseline = true)]
        public int SerializeOneByOne()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());

            WriteUInt16(ref destination, Value);

            return Value.Length * sizeof(ushort);
        }

        [Benchmark]
        public int UsingUnsafeReadUnaligned()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            ref var source = ref MemoryMarshal.GetReference(MemoryMarshal.Cast<ushort, byte>(Value.AsSpan()));

            var offset = 0u;
            for (var i = 0; i < Value.Length; i++)
            {
                var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(offset), value);

                offset += sizeof(ushort);
            }

            return (int) offset;
        }

        [Benchmark]
        public int UsingUnsafeIsAddressLessThan()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            ref var source = ref MemoryMarshal.GetReference(MemoryMarshal.Cast<ushort, byte>(Value.AsSpan()));

            var size = Value.Length * sizeof(ushort);
            ref var limit = ref source.GetOffset((uint)size);

            while (Unsafe.IsAddressLessThan(ref source, ref limit))
            {
                var value = Unsafe.ReadUnaligned<ushort>(ref source);
                NetworkOrderSerializer.WriteUInt16(ref destination, value);

                source = ref source.GetOffset(sizeof(ushort));
                destination = ref destination.GetOffset(sizeof(ushort));
            }

            return size;
        }

        [Benchmark]
        public int UsingCopyAndAlign()
        {
            return CopyAndAlign16Bit(MemoryMarshal.Cast<ushort, byte>(Value.AsSpan()), buffer.AsSpan(), Value.Length);
        }
    }

    private static int WriteUInt16(ref byte destination, ReadOnlySpan<ushort> values)
    {
        var offset = 0;
        foreach (var value in values)
        {
            NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset((uint)offset), value);
            offset += sizeof(ushort);
        }

        return offset;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CopyAndAlign16Bit(ReadOnlySpan<byte> input, Span<byte> output, int numberOfItems)
    {
        ref var destination = ref MemoryMarshal.GetReference(output);
        ref var source = ref MemoryMarshal.GetReference(input);

        var limit = (uint) numberOfItems * sizeof(ushort);

        var offset = 0u;
        while (offset < limit)
        {
            var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
            NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(offset), value);

            offset += sizeof(ushort);
        }

        return (int)offset;
    }
}