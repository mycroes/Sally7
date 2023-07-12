``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-KSWMMW : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  

```
|                               Method |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|------------------------------------- |---------:|----------:|----------:|---------:|------:|--------:|----------:|----------:|------------:|
|                    SerializeOneByOne | 5.339 ns | 0.1371 ns | 0.3232 ns | 5.227 ns |  1.00 |    0.00 |     146 B |         - |          NA |
|                 CombineUsingUnsafeAs | 7.817 ns | 0.1758 ns | 0.3345 ns | 7.770 ns |  1.45 |    0.10 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | 5.833 ns | 0.1379 ns | 0.2556 ns | 5.796 ns |  1.09 |    0.07 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | 2.742 ns | 0.0823 ns | 0.1011 ns | 2.733 ns |  0.51 |    0.03 |     263 B |         - |          NA |
