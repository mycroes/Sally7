using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Sally7.Benchmarks
{
    [ShortRunJob]
    public class ConverterLookup
    {
        private readonly DataItemWithConverter<int> _withConverter =
            new DataItemWithConverter<int>(b => b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]);

        private readonly DataItemWithValue<int> _withValue = new DataItemWithValue<int>();

        private readonly Dictionary<Type, IConverter> _typeToConverterLookup =
            new Dictionary<Type, IConverter>
            {
                {typeof(bool), new Int32Converter()},
                {typeof(short), new Int32Converter()},
                {typeof(int), new Int32Converter()},
                {typeof(float), new Int32Converter()},
                {typeof(string), new Int32Converter()}
            };

        private readonly Dictionary<int, IConverter> _metadataTokenToConverterLookup =
            new Dictionary<int, IConverter>
            {
                {typeof(bool).MetadataToken, new Int32Converter()},
                {typeof(short).MetadataToken, new Int32Converter()},
                {typeof(int).MetadataToken, new Int32Converter()},
                {typeof(float).MetadataToken, new Int32Converter()},
                {typeof(string).MetadataToken, new Int32Converter()}
            };

        // Type converters
        private readonly IConverter _boolConverter = new Int32Converter();
        private readonly IConverter _shortConverter = new Int32Converter();
        private readonly IConverter _floatConverter = new Int32Converter();
        private readonly IConverter _stringConverter = new Int32Converter();

        private readonly Int32Converter _intConverter = new Int32Converter();

        public ConverterLookup()
        {
            Message = new byte[0];
        }

        [Params(1, 1 << 9)]
        public int Value { get; set; }

        public byte[] Message { get; private set; }

        [GlobalSetup]
        public void SetupMessage()
        {
            Message = new[]
            {
                (byte) (Value >> 24),
                (byte) (Value >> 16),
                (byte) (Value >> 8),
                (byte) Value
            };
        }

        [Benchmark]
        public int UsingDataItemWithConverter()
        {
            _withConverter.ApplyValue(Message);
            return _withConverter.Value;
        }

        [Benchmark]
        public int WithTypeLookup()
        {
            _typeToConverterLookup[_withValue.ValueType].Apply(Message, _withValue);
            return _withValue.Value;
        }

        [Benchmark]
        public int WithMetadataTokenLookup()
        {
            _metadataTokenToConverterLookup[_withValue.ValueType.MetadataToken].Apply(Message, _withValue);
            return _withValue.Value;
        }

        [Benchmark]
        public int WithIfElseLookup()
        {
            IfElseLookup(_withValue.ValueType).Apply(Message, _withValue);
            return _withValue.Value;
        }

        [Benchmark]
        public int WithConvertUsingIfElseLookup()
        {
            ConvertUsingIfElseLookup(Message, _withValue);
            return _withValue.Value;
        }

        [Benchmark]
        public int WithTypeSwitchConvert()
        {
            TypeSwitchConvert(Message, _withValue);
            return _withValue.Value;
        }

        private IConverter IfElseLookup(Type type)
        {
            if (type == typeof(bool))
                return _boolConverter;
            if (type == typeof(short))
                return _shortConverter;
            if (type == typeof(int))
                return _intConverter;
            if (type == typeof(float))
                return _floatConverter;
            if (type == typeof(string))
                return _stringConverter;

            throw new ArgumentException($"Type {type} not supported.");
        }

        private void ConvertUsingIfElseLookup(byte[] message, IDataItem dataItem)
        {
            var type = dataItem.ValueType;

            if (type == typeof(bool))
                _boolConverter.Apply(message, dataItem);
            else if (type == typeof(short))
                _shortConverter.Apply(message, dataItem);
            else if (type == typeof(int))
                _intConverter.Apply(message, dataItem);
            else if (type == typeof(float))
                _floatConverter.Apply(message, dataItem);
            else if (type == typeof(string))
                _stringConverter.Apply(message, dataItem);
            else
                throw new ArgumentException($"Type {type} not supported.");
        }

        private void TypeSwitchConvert(byte[] message, IDataItem dataItem)
        {
            switch (dataItem)
            {
                case IDataItem<bool> di:
                    di.Value = message[0] == 1;
                    break;
                case IDataItem<short> di:
                    di.Value = (short) (message[0] << 8 | message[1]);
                    break;
                case IDataItem<int> di:
                    di.Value = message[0] << 24 | message[1] << 16 | message[2] << 8 | message[3];
                    break;
                case IDataItem<float> di:
                    di.Value = 0;
                    break;
                case IDataItem<string> di:
                    di.Value = Encoding.ASCII.GetString(message);
                    break;
                default:
                    throw new ArgumentException($"Type {dataItem.GetType()} not supported.");
            }
        }

        private interface IDataItem
        {
            Type ValueType { get; }
        }

        private interface IDataItem<T> : IDataItem
        {
            T? Value { get; set; }
        }

        private interface IConverter
        {
            void Apply(byte[] message, IDataItem dataItem);
        }

        private class Int32Converter : IConverter
        {
            public void Apply(byte[] message, IDataItem dataItem)
            {
                ((DataItemWithValue<int>) dataItem).Value =
                    message[0] << 24 | message[1] << 16 | message[2] << 8 | message[3];
            }
        }

        private class DataItemWithValue<T> : IDataItem<T>
        {
            public Type ValueType => typeof(T);
            public T? Value { get; set; }
        }

        private class DataItemWithConverter<T>
        {
            private readonly Func<byte[], T> _converter;

            public DataItemWithConverter(Func<byte[], T> converter)
            {
                _converter = converter;
            }

            public T? Value { get; set; }

            public void ApplyValue(byte[] message)
            {
                Value = _converter(message);
            }
        }
    }
}
