``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2965/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-HLOCPM : .NET 6.0.16 (6.0.1623.17311), X64 RyuJIT AVX2
  Job-PEQFBQ : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-EAOMPI : .NET Framework 4.8 (4.8.4614.0), X64 RyuJIT VectorSize=256
  Job-YCATYC : .NET Framework 4.8 (4.8.4614.0), X64 RyuJIT VectorSize=256


```
|                Method |              Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|---------------------- |--------------------- |----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
|    MemoryMarshal_Cast |             .NET 6.0 |  6.350 ns | 0.1560 ns | 0.1602 ns |  0.15 |    0.01 |     390 B |         - |          NA |
|    MemoryMarshal_Cast |             .NET 7.0 |  5.903 ns | 0.1543 ns | 0.2212 ns |  0.13 |    0.01 |     395 B |         - |          NA |
|    MemoryMarshal_Cast | .NET Framework 4.6.2 | 43.833 ns | 0.9021 ns | 1.2646 ns |  1.00 |    0.00 |   1,445 B |         - |          NA |
|    MemoryMarshal_Cast |   .NET Framework 4.8 | 49.520 ns | 1.0227 ns | 0.9566 ns |  1.13 |    0.04 |   1,445 B |         - |          NA |
|                       |                      |           |           |           |       |         |           |           |             |
| Unsafe_WriteUnaligned |             .NET 6.0 |  4.171 ns | 0.0901 ns | 0.0752 ns |  0.33 |    0.01 |     145 B |         - |          NA |
| Unsafe_WriteUnaligned |             .NET 7.0 |  4.840 ns | 0.1249 ns | 0.1336 ns |  0.38 |    0.02 |     145 B |         - |          NA |
| Unsafe_WriteUnaligned | .NET Framework 4.6.2 | 12.745 ns | 0.2834 ns | 0.3481 ns |  1.00 |    0.00 |     345 B |         - |          NA |
| Unsafe_WriteUnaligned |   .NET Framework 4.8 | 13.594 ns | 0.2918 ns | 0.2866 ns |  1.07 |    0.04 |     345 B |         - |          NA |
|                       |                      |           |           |           |       |         |           |           |             |
|      Unsafe_As_Struct |             .NET 6.0 |  6.327 ns | 0.1561 ns | 0.2650 ns |  0.23 |    0.01 |     224 B |         - |          NA |
|      Unsafe_As_Struct |             .NET 7.0 |  6.047 ns | 0.1521 ns | 0.1868 ns |  0.22 |    0.01 |     220 B |         - |          NA |
|      Unsafe_As_Struct | .NET Framework 4.6.2 | 27.930 ns | 0.5846 ns | 1.0980 ns |  1.00 |    0.00 |     348 B |         - |          NA |
|      Unsafe_As_Struct |   .NET Framework 4.8 | 27.753 ns | 0.5820 ns | 0.6702 ns |  1.01 |    0.05 |     348 B |         - |          NA |
