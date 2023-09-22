``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3448/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2


```
|                          Method |      Mean |     Error |    StdDev | Code Size | Completed Work Items | Lock Contentions | Allocated |
|-------------------------------- |----------:|----------:|----------:|----------:|---------------------:|-----------------:|----------:|
|                     WriteUInt64 | 0.2361 ns | 0.0352 ns | 0.0376 ns |      30 B |                    - |                - |         - |
| WriteUInt64ArrayIncrementOffset | 2.2491 ns | 0.0758 ns | 0.1203 ns |     118 B |                    - |                - |         - |
| WriteUInt64ArrayCalculateOffset | 2.2776 ns | 0.0768 ns | 0.0944 ns |     122 B |                    - |                - |         - |
|     WriteUInt64ArrayStoreOffset | 2.2588 ns | 0.0736 ns | 0.1079 ns |     138 B |                    - |                - |         - |
