using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sally7.Internal;

namespace Sally7.Benchmarks.Serialization;

public class SerializePrimitivesAsLargerPrimitive
{
    private readonly ushort[] value = { 1, 2, 3, 4 };
    private readonly byte[] buffer = new byte[1024];

    [Benchmark(Baseline = true)]
    public void SerializeOneByOne()
    {
        ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
        uint offset = 0;
        for (var i = 0; i < value.Length; i++)
        {
            NetworkOrderSerializer.WriteUInt16(ref destination.GetOffset(offset), value[i]);
            offset += sizeof(ushort);
        }
    }

    [Benchmark]
    public void CombineUsingUnsafeAs()
    {
        ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());
        var longVal = MemoryMarshal.Cast<ushort, ulong>(value.AsSpan());

        uint offset = 0;
        for (var i = 0; i < longVal.Length; i++)
        {
            NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), longVal[i]);
            offset += sizeof(ulong);
        }
    }

    [Benchmark]
    public void CombineUsingShifts()
    {
        ref var destination = ref MemoryMarshal.GetReference(buffer.AsSpan());

        uint offset = 0;
        for (var i = 0; i < value.Length; i+= 4)
        {
            var longVal = (ulong)value[0] << 48 | (ulong)value[1] << 32 | (ulong)value[2] << 16 | value[3];

            NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), longVal);
            offset += sizeof(ushort);
        }
    }
}