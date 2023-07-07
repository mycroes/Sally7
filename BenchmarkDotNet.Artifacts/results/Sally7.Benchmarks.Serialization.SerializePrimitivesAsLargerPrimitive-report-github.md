``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-LQKAUB : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  

```
|            Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|------------------ |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
| SerializeOneByOne | 10.212 ns | 0.2776 ns | 0.3610 ns |  1.00 |    0.00 |      96 B |         - |          NA |
| SerializeAsLarger |  3.291 ns | 0.1528 ns | 0.2467 ns |  0.32 |    0.02 |     123 B |         - |          NA |
