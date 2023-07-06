using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using Sally7.Internal;

namespace Sally7.Benchmarks.Serialization
{
    public class SerializePrimitives
    {
        private readonly ulong value = 0x0102030405060708;
        private readonly ulong[] arrayValue = { 0x0102030405060708, 0x0807060504030201 };
        private readonly byte[] buffer = new byte[1024];

        [Benchmark]
        public void WriteUInt64()
        {
            Converters.AppendUInt64(buffer, value);
        }

        [Benchmark]
        public void WriteUInt64ArrayIncrementOffset()
        {
            Converters.AppendUInt64ArrayIncrementDestination(buffer, arrayValue);
        }

        [Benchmark]
        public void WriteUInt64ArrayCalculateOffset()
        {
            Converters.AppendUInt64ArrayCalculateOffset(buffer, arrayValue);
        }

        [Benchmark]
        public void WriteUInt64ArrayStoreOffset()
        {
            Converters.AppendUInt64ArrayStoreOffset(buffer, arrayValue);
        }

        public static class Converters
        {
            public static void AppendUInt64(Span<byte> buffer, ulong value)
            {
                ref var destination = ref MemoryMarshal.GetReference(buffer);
                NetworkOrderSerializer.WriteUInt64(ref destination, value);
            }

            public static void AppendUInt64ArrayIncrementDestination(Span<byte> buffer, ulong[] value)
            {
                ref var destination = ref MemoryMarshal.GetReference(buffer);
                for (var i = 0; i < value.Length; i++)
                {
                    NetworkOrderSerializer.WriteUInt64(ref destination, value[i]);
                    destination = ref destination.GetOffset(sizeof(ulong));
                }
            }

            public static void AppendUInt64ArrayCalculateOffset(Span<byte> buffer, ulong[] value)
            {
                ref var destination = ref MemoryMarshal.GetReference(buffer);
                for (uint i = 0; i < value.Length; i++)
                {
                    NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(sizeof(ulong) * i), value[i]);
                }
            }

            public static void AppendUInt64ArrayStoreOffset(Span<byte> buffer, ulong[] value)
            {
                ref var destination = ref MemoryMarshal.GetReference(buffer);
                uint offset = 0;
                for (var i = 0; i < value.Length; i++)
                {
                    NetworkOrderSerializer.WriteUInt64(ref destination.GetOffset(offset), value[i]);
                    offset += sizeof(ulong);
                }
            }
        }
    }
}