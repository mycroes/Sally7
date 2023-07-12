using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sally7.Internal;

namespace Sally7.Benchmarks.Serialization;

public static class SerializePrimitivesAsLargerPrimitive
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
        public int CombineUsingUnsafeAs()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            var longVal = MemoryMarshal.Cast<ushort, ulong>(Value.AsSpan());

            var offset = WriteUInt64(ref destination, longVal);

            var rem = Value.Length % 4;
            if (rem > 2)
            {
                var intVal = MemoryMarshal.Cast<ushort, uint>(Value.AsSpan().Slice(offset / sizeof(ushort)));
                offset += WriteUInt32(ref destination, intVal);
            }

            if ((rem & 1) == 1)
            {
                offset += WriteUInt16(ref destination, Value.AsSpan().Slice(offset / sizeof(ushort)));
            }

            return offset;
        }

        [Benchmark]
        public int CombineUsingUnsafeAsNoRemainderSplit()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            var longVal = MemoryMarshal.Cast<ushort, ulong>(Value.AsSpan());

            var offset = WriteUInt64(ref destination, longVal);

            var rem = Value.Length % 4;
            if (rem > 0)
            {
                offset += WriteUInt16(ref destination, Value.AsSpan().Slice(offset / sizeof(ushort)));
            }

            return offset;
        }

        [Benchmark]
        public int CombineUsingUnsafeReadUnaligned()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            ref var source = ref MemoryMarshal.GetReference(MemoryMarshal.Cast<ushort, byte>(Value.AsSpan()));

            var offset = 0u;
            for (var i = 0; i < Value.Length / 4; i++)
            {
                var value = Unsafe.ReadUnaligned<ulong>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), value);

                offset += sizeof(ulong);
            }

            for (var i = offset / 4; i < Value.Length / 2; i++)
            {
                var value = Unsafe.ReadUnaligned<uint>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt32(ref destination.GetOffset(offset), value);

                offset += sizeof(uint);
            }

            if (offset / 2 < Value.Length)
            {
                var value = Unsafe.ReadUnaligned<ushort>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(offset), value);

                offset += sizeof(uint);
            }

            return (int) offset;
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

    private static int WriteUInt32(ref byte destination, ReadOnlySpan<uint> values)
    {
        var offset = 0;
        foreach (var value in values)
        {
            NetworkOrderSerializer.WriteUInt32(ref destination.GetOffset((uint) offset), value);
            offset += sizeof(uint);
        }

        return offset;
    }

    private static int WriteUInt64(ref byte destination, ReadOnlySpan<ulong> values)
    {
        var offset = 0;
        foreach (var value in values)
        {
            NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset((uint) offset), value);
            offset += sizeof(ulong);
        }

        return offset;
    }
}