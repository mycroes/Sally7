``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-RMFIHR : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  

```
|                                   Method |     Mean |     Error |    StdDev | Code Size | Allocated |
|----------------------------------------- |---------:|----------:|----------:|----------:|----------:|
|                       MemoryMarshal_Cast | 6.517 ns | 0.1357 ns | 0.1270 ns |     395 B |         - |
|                    Unsafe_WriteUnaligned | 4.462 ns | 0.1186 ns | 0.1543 ns |     145 B |         - |
| Unsafe_WriteUnaligned_With_Optimizations | 2.547 ns | 0.0678 ns | 0.0634 ns |     116 B |         - |
|                         Unsafe_As_Struct | 6.453 ns | 0.1559 ns | 0.1382 ns |     220 B |         - |
