``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3448/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2


```
|                   Method |      Value |      Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Completed Work Items | Lock Contentions | Allocated | Alloc Ratio |
|------------------------- |----------- |----------:|----------:|----------:|------:|--------:|----------:|---------------------:|-----------------:|----------:|------------:|
|        **SerializeOneByOne** | **UInt16[10]** | **7.7525 ns** | **0.1857 ns** | **0.4115 ns** |  **1.00** |    **0.00** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned | UInt16[10] | 6.7415 ns | 0.1673 ns | 0.3143 ns |  0.87 |    0.05 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** | **UInt16[11]** | **6.8764 ns** | **0.1586 ns** | **0.2274 ns** |  **0.87** |    **0.05** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned | UInt16[11] | 7.3268 ns | 0.1770 ns | 0.2649 ns |  0.93 |    0.05 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** | **UInt16[12]** | **7.7209 ns** | **0.1857 ns** | **0.3253 ns** |  **0.99** |    **0.06** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned | UInt16[12] | 7.5350 ns | 0.1818 ns | 0.2364 ns |  0.95 |    0.05 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[1]** | **2.0574 ns** | **0.0732 ns** | **0.2042 ns** |  **0.27** |    **0.03** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[1] | 0.9390 ns | 0.0458 ns | 0.0860 ns |  0.12 |    0.01 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[2]** | **2.4496 ns** | **0.0801 ns** | **0.0923 ns** |  **0.30** |    **0.01** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[2] | 1.5399 ns | 0.0575 ns | 0.0945 ns |  0.20 |    0.01 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[3]** | **2.9694 ns** | **0.0899 ns** | **0.1754 ns** |  **0.38** |    **0.03** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[3] | 2.0440 ns | 0.0701 ns | 0.0960 ns |  0.26 |    0.02 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[4]** | **3.3845 ns** | **0.0953 ns** | **0.1020 ns** |  **0.42** |    **0.02** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[4] | 2.6555 ns | 0.0832 ns | 0.1167 ns |  0.34 |    0.02 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[5]** | **4.2239 ns** | **0.1157 ns** | **0.1900 ns** |  **0.54** |    **0.03** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[5] | 3.8004 ns | 0.1033 ns | 0.1414 ns |  0.48 |    0.03 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[6]** | **4.6494 ns** | **0.1194 ns** | **0.1466 ns** |  **0.58** |    **0.03** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[6] | 4.4927 ns | 0.1143 ns | 0.2387 ns |  0.58 |    0.04 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[7]** | **5.2495 ns** | **0.1316 ns** | **0.1801 ns** |  **0.66** |    **0.04** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[7] | 4.5302 ns | 0.1196 ns | 0.1378 ns |  0.56 |    0.02 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[8]** | **5.6404 ns** | **0.1372 ns** | **0.1468 ns** |  **0.69** |    **0.02** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[8] | 5.4526 ns | 0.1399 ns | 0.2375 ns |  0.70 |    0.04 |     143 B |                    - |                - |         - |          NA |
|        **SerializeOneByOne** |  **UInt16[9]** | **6.1025 ns** | **0.1325 ns** | **0.1361 ns** |  **0.75** |    **0.02** |     **146 B** |                    **-** |                **-** |         **-** |          **NA** |
| UsingUnsafeReadUnaligned |  UInt16[9] | 6.2095 ns | 0.1548 ns | 0.3128 ns |  0.80 |    0.06 |     143 B |                    - |                - |         - |          NA |
