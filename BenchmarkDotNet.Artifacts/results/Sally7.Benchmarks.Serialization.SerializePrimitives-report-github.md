``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-JESTFH : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  

```
|                          Method |      Mean |     Error |    StdDev |    Median | Code Size | Allocated |
|-------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                     WriteUInt64 | 0.0008 ns | 0.0032 ns | 0.0028 ns | 0.0000 ns |      30 B |         - |
| WriteUInt64ArrayIncrementOffset | 2.8744 ns | 0.0897 ns | 0.1068 ns | 2.8971 ns |     118 B |         - |
| WriteUInt64ArrayCalculateOffset | 2.5779 ns | 0.0854 ns | 0.0949 ns | 2.5608 ns |     122 B |         - |
|     WriteUInt64ArrayStoreOffset | 2.5464 ns | 0.0579 ns | 0.0542 ns | 2.5382 ns |     138 B |         - |
