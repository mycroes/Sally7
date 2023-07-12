``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-HUNTTZ : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  IterationTime=300.0000 ms  

```
|                               Method |     Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|------------------------------------- |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
|                    SerializeOneByOne | 5.815 ns | 0.1468 ns | 0.3096 ns |  1.00 |    0.00 |     146 B |         - |          NA |
|                 CombineUsingUnsafeAs | 8.105 ns | 0.1936 ns | 0.4856 ns |  1.41 |    0.12 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | 6.417 ns | 0.1550 ns | 0.3592 ns |  1.10 |    0.08 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | 2.730 ns | 0.0844 ns | 0.0972 ns |  0.48 |    0.03 |     265 B |         - |          NA |
