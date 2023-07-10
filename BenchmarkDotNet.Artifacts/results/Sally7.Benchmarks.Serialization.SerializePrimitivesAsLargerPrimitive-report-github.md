``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-AZZEDO : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  

```
|               Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
|    SerializeOneByOne | 1.8794 ns | 0.0737 ns | 0.1904 ns | 1.8059 ns |  1.00 |    0.00 |      69 B |         - |          NA |
| CombineUsingUnsafeAs | 0.8329 ns | 0.0508 ns | 0.0915 ns | 0.8185 ns |  0.45 |    0.06 |     123 B |         - |          NA |
|   CombineUsingShifts | 2.5645 ns | 0.1138 ns | 0.3152 ns | 2.5279 ns |  1.37 |    0.22 |     165 B |         - |          NA |
