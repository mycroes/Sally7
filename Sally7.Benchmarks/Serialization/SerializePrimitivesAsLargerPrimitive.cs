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