``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3448/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
  Job-YLNZRQ : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2

IterationTime=150.0000 ms  WarmupCount=2  

```
|                    Method |    Value |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Completed Work Items | Lock Contentions | Code Size | Allocated | Alloc Ratio |
|-------------------------- |--------- |---------:|----------:|----------:|---------:|------:|--------:|---------------------:|-----------------:|----------:|----------:|------------:|
|                **SpanCopyTo** | **Byte[16]** | **3.201 ns** | **0.1285 ns** | **0.3729 ns** | **3.139 ns** |  **1.00** |    **0.00** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives | Byte[16] | 4.236 ns | 0.1211 ns | 0.1849 ns | 4.232 ns |  1.26 |    0.13 |                    - |                - |     255 B |         - |          NA |
|                **SpanCopyTo** |  **Byte[1]** | **4.482 ns** | **0.1734 ns** | **0.4947 ns** | **4.365 ns** |  **1.42** |    **0.24** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives |  Byte[1] | 3.955 ns | 0.1599 ns | 0.4613 ns | 3.865 ns |  1.25 |    0.19 |                    - |                - |     255 B |         - |          NA |
|                **SpanCopyTo** | **Byte[24]** | **3.049 ns** | **0.1035 ns** | **0.2868 ns** | **2.963 ns** |  **0.96** |    **0.13** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives | Byte[24] | 5.251 ns | 0.1449 ns | 0.3025 ns | 5.172 ns |  1.58 |    0.16 |                    - |                - |     255 B |         - |          NA |
|                **SpanCopyTo** |  **Byte[2]** | **4.087 ns** | **0.1235 ns** | **0.2813 ns** | **4.065 ns** |  **1.23** |    **0.15** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives |  Byte[2] | 3.627 ns | 0.1125 ns | 0.2606 ns | 3.598 ns |  1.10 |    0.14 |                    - |                - |     255 B |         - |          NA |
|                **SpanCopyTo** | **Byte[32]** | **2.588 ns** | **0.0952 ns** | **0.2475 ns** | **2.511 ns** |  **0.80** |    **0.10** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives | Byte[32] | 5.079 ns | 0.1422 ns | 0.3434 ns | 4.968 ns |  1.55 |    0.19 |                    - |                - |     255 B |         - |          NA |
|                **SpanCopyTo** |  **Byte[4]** | **2.896 ns** | **0.0944 ns** | **0.2013 ns** | **2.885 ns** |  **0.87** |    **0.09** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives |  Byte[4] | 3.210 ns | 0.1042 ns | 0.2414 ns | 3.157 ns |  0.97 |    0.11 |                    - |                - |     255 B |         - |          NA |
|                **SpanCopyTo** | **Byte[64]** | **3.387 ns** | **0.1047 ns** | **0.1598 ns** | **3.373 ns** |  **1.01** |    **0.10** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives | Byte[64] | 6.639 ns | 0.1769 ns | 0.2480 ns | 6.567 ns |  1.98 |    0.19 |                    - |                - |     255 B |         - |          NA |
|                **SpanCopyTo** |  **Byte[8]** | **2.715 ns** | **0.0883 ns** | **0.1400 ns** | **2.742 ns** |  **0.81** |    **0.09** |                    **-** |                **-** |     **364 B** |         **-** |          **NA** |
| CopyUsingLargerPrimitives |  Byte[8] | 3.476 ns | 0.1054 ns | 0.2401 ns | 3.465 ns |  1.04 |    0.11 |                    - |                - |     255 B |         - |          NA |
