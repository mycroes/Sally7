using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sally7.Internal;

namespace Sally7.Benchmarks.Serialization;

public static class SerializePrimitivesAsLargerPrimitive
{
    public class SerializeUInt16
    {
        private readonly ushort[] value = { 1, 2, 3, 4, 5, 6, 7 };
        private readonly byte[] buffer = new byte[1024];

        [Benchmark(Baseline = true)]
        public int SerializeOneByOne()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());

            WriteUInt16(ref destination, value);

            return value.Length * sizeof(ushort);
        }

        [Benchmark]
        public int UsingUnsafeReadUnaligned()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            ref var source = ref MemoryMarshal.GetReference(MemoryMarshal.Cast<ushort, byte>(value.AsSpan()));

            var offset = 0u;
            for (var i = 0; i < value.Length; i++)
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
            var longVal = MemoryMarshal.Cast<ushort, ulong>(value.AsSpan());

            var offset = WriteUInt64(ref destination, longVal);

            var rem = value.Length % 4;
            if (rem > 2)
            {
                var intVal = MemoryMarshal.Cast<ushort, uint>(value.AsSpan().Slice(offset / sizeof(ushort)));
                offset += WriteUInt32(ref destination, intVal);
            }

            if ((rem & 1) == 1)
            {
                offset += WriteUInt16(ref destination, value.AsSpan().Slice(offset / sizeof(ushort)));
            }

            return offset;
        }

        [Benchmark]
        public int CombineUsingUnsafeAsNoRemainderSplit()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            var longVal = MemoryMarshal.Cast<ushort, ulong>(value.AsSpan());

            var offset = WriteUInt64(ref destination, longVal);

            var rem = value.Length % 4;
            if (rem > 0)
            {
                offset += WriteUInt16(ref destination, value.AsSpan().Slice(offset / sizeof(ushort)));
            }

            return offset;
        }

        [Benchmark]
        public int CombineUsingUnsafeReadUnaligned()
        {
            ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
            ref var source = ref MemoryMarshal.GetReference(MemoryMarshal.Cast<ushort, byte>(value.AsSpan()));

            var offset = 0u;
            for (var i = 0; i < value.Length / 4; i++)
            {
                var value = Unsafe.ReadUnaligned<ulong>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), value);

                offset += sizeof(ulong);
            }

            for (var i = offset / 4; i < value.Length / 2; i++)
            {
                var value = Unsafe.ReadUnaligned<uint>(ref source.GetOffset(offset));
                NetworkOrderSerializer.WriteUInt32(ref destination.GetOffset(offset), value);

                offset += sizeof(uint);
            }

            if (offset / 2 < value.Length)
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