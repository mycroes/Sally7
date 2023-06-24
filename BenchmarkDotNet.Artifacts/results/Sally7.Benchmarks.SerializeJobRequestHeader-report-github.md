``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-FZJEWY : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  IterationTime=120.0000 ms  

```
|              Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|-------------------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| WriteFieldsOneByOne | 0.3460 ns | 0.0850 ns | 0.2508 ns | 0.2769 ns |     ? |       ? |      50 B |         - |           ? |
|  WriteLongThenShort | 0.0338 ns | 0.0280 ns | 0.0714 ns | 0.0000 ns |     ? |       ? |      48 B |         - |           ? |
|          WriteArray | 8.9878 ns | 0.4043 ns | 1.1795 ns | 8.7688 ns |     ? |       ? |     187 B |         - |           ? |
|         WriteStruct | 8.0472 ns | 0.4705 ns | 1.3347 ns | 7.5046 ns |     ? |       ? |     148 B |         - |           ? |
