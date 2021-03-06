﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    [ShortRunJob]
    public class IntToFloatConversion
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct IntFloatUnion
        {
            [FieldOffset(0)] public int IntValue;
            [FieldOffset(0)] public float FloatValue;
        }

        public IntToFloatConversion()
        {
            byteValue = new byte[0];
        }

        [Params(1, 2, 3)]
        public int Value { get; set; }

        private byte[] byteValue;

        [GlobalSetup]
        public void SetByteValue()
        {
            byteValue = BitConverter.GetBytes(Value);
        }

        [Benchmark]
        public float WithUnion()
        {
            return new IntFloatUnion{FloatValue = 0, IntValue = Value}.FloatValue;
        }

        [Benchmark]
        public float WithUnsafe()
        {
            var value = Value;
            return Unsafe.As<int, float>(ref value);
        }

        [Benchmark]
        public float WithBitConverter()
        {
            // This one is here for comparison only. The byte order is based on
            // endianness and as such this doesn't work without additional code
            // to check endianness and possibly reverse the data before passing
            // it to BitConverter.
            return BitConverter.ToSingle(byteValue, 0);
        }

        [Benchmark]
        public unsafe float UnsafeImpl()
        {
            var value = Value;
            return * (float*) &value;
        }
    }
}