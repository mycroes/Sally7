``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-XZPIMP : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  

```
|               Method |     Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|--------------------- |---------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
|    SerializeOneByOne | 4.494 ns | 0.1270 ns | 0.1651 ns |  1.00 |    0.00 |      96 B |         - |          NA |
| CombineUsingUnsafeAs | 1.220 ns | 0.0402 ns | 0.0314 ns |  0.27 |    0.01 |     123 B |         - |          NA |
|   CombineUsingShifts | 2.355 ns | 0.0869 ns | 0.1099 ns |  0.52 |    0.03 |     165 B |         - |          NA |
