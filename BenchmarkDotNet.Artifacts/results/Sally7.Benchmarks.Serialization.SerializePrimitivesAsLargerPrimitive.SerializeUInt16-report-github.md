``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-NVQZPK : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  IterationTime=300.0000 ms  

```
|                               Method |     Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|------------------------------------- |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
|                    SerializeOneByOne | 5.887 ns | 0.1498 ns | 0.3704 ns |  1.00 |    0.00 |     146 B |         - |          NA |
|             UsingUnsafeReadUnaligned | 4.736 ns | 0.1297 ns | 0.2764 ns |  0.80 |    0.07 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs | 7.891 ns | 0.1867 ns | 0.2223 ns |  1.31 |    0.08 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | 6.169 ns | 0.1564 ns | 0.3159 ns |  1.04 |    0.09 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | 2.971 ns | 0.0870 ns | 0.2167 ns |  0.51 |    0.04 |     265 B |         - |          NA |
