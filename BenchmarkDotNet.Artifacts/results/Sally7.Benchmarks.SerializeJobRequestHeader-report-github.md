``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-XAKHXL : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  IterationTime=120.0000 ms  

```
|              Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|-------------------- |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| WriteFieldsOneByOne | 0.3152 ns | 0.0245 ns | 0.0217 ns |  1.00 |    0.00 |      50 B |         - |          NA |
|  WriteLongThenShort | 0.0194 ns | 0.0167 ns | 0.0156 ns |  0.07 |    0.05 |      48 B |         - |          NA |
|          WriteArray | 7.9353 ns | 0.1871 ns | 0.1837 ns | 25.22 |    2.22 |     197 B |         - |          NA |
|         WriteStruct | 6.0876 ns | 0.1112 ns | 0.1040 ns | 19.42 |    1.43 |     148 B |         - |          NA |
