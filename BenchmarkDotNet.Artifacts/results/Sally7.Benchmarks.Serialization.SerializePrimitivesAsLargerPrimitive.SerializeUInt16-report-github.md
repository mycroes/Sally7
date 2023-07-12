``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3086/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-BMOLPZ : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  IterationTime=100.0000 ms  
MaxIterationCount=10  MinIterationCount=5  WarmupCount=3  

```
|                               Method |      Value |       Mean |     Error |    StdDev | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|------------------------------------- |----------- |-----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
|                    **SerializeOneByOne** | **UInt16[10]** |  **6.1001 ns** | **0.4143 ns** | **0.2741 ns** |  **1.00** |    **0.00** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned | UInt16[10] |  5.9262 ns | 0.3209 ns | 0.1910 ns |  0.98 |    0.06 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs | UInt16[10] |  3.7665 ns | 0.3051 ns | 0.2018 ns |  0.62 |    0.04 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | UInt16[10] |  5.5031 ns | 0.2968 ns | 0.1766 ns |  0.91 |    0.06 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | UInt16[10] |  3.3318 ns | 0.0932 ns | 0.0242 ns |  0.54 |    0.02 |     265 B |         - |          NA |
|                    **SerializeOneByOne** | **UInt16[11]** |  **6.7363 ns** | **0.2767 ns** | **0.1830 ns** |  **1.11** |    **0.05** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned | UInt16[11] |  6.5175 ns | 0.4580 ns | 0.3030 ns |  1.07 |    0.08 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs | UInt16[11] |  8.5682 ns | 0.5317 ns | 0.3517 ns |  1.41 |    0.10 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | UInt16[11] |  5.9458 ns | 0.5896 ns | 0.3900 ns |  0.98 |    0.06 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | UInt16[11] |  3.8813 ns | 0.3696 ns | 0.2445 ns |  0.64 |    0.03 |     265 B |         - |          NA |
|                    **SerializeOneByOne** | **UInt16[12]** | **11.9643 ns** | **0.2344 ns** | **0.0609 ns** |  **1.96** |    **0.06** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned | UInt16[12] |  7.5153 ns | 1.1554 ns | 0.7642 ns |  1.24 |    0.17 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs | UInt16[12] |  4.4009 ns | 0.1937 ns | 0.1281 ns |  0.72 |    0.04 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | UInt16[12] |  4.2256 ns | 0.7671 ns | 0.5074 ns |  0.69 |    0.09 |     294 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | UInt16[12] |  2.8891 ns | 0.1890 ns | 0.1125 ns |  0.48 |    0.02 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[1]** |  **1.9076 ns** | **0.3031 ns** | **0.2005 ns** |  **0.31** |    **0.03** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[1] |  0.8881 ns | 0.0483 ns | 0.0253 ns |  0.14 |    0.00 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[1] |  4.6820 ns | 0.2685 ns | 0.1598 ns |  0.77 |    0.04 |     475 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[1] |  3.9765 ns | 0.3109 ns | 0.2057 ns |  0.65 |    0.05 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[1] |  1.5137 ns | 0.1248 ns | 0.0826 ns |  0.25 |    0.02 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[2]** |  **2.7004 ns** | **0.1099 ns** | **0.0727 ns** |  **0.44** |    **0.02** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[2] |  1.5259 ns | 0.1191 ns | 0.0788 ns |  0.25 |    0.02 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[2] |  3.0640 ns | 0.4164 ns | 0.2754 ns |  0.50 |    0.05 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[2] |  4.5584 ns | 0.2388 ns | 0.1579 ns |  0.75 |    0.04 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[2] |  1.8350 ns | 0.2141 ns | 0.1416 ns |  0.30 |    0.02 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[3]** |  **3.0301 ns** | **0.3918 ns** | **0.2332 ns** |  **0.50** |    **0.05** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[3] |  2.1433 ns | 0.1600 ns | 0.1058 ns |  0.35 |    0.02 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[3] |  7.8436 ns | 0.5709 ns | 0.3776 ns |  1.29 |    0.09 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[3] |  5.5261 ns | 0.5449 ns | 0.3604 ns |  0.91 |    0.06 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[3] |  2.2785 ns | 0.1648 ns | 0.1090 ns |  0.37 |    0.03 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[4]** |  **3.9149 ns** | **0.3316 ns** | **0.2193 ns** |  **0.64** |    **0.05** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[4] |  2.9716 ns | 0.3050 ns | 0.2018 ns |  0.49 |    0.04 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[4] |  3.3031 ns | 0.3273 ns | 0.2165 ns |  0.54 |    0.04 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[4] |  3.0017 ns | 0.1643 ns | 0.0978 ns |  0.50 |    0.03 |     294 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[4] |  1.7558 ns | 0.1152 ns | 0.0762 ns |  0.29 |    0.02 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[5]** |  **3.8310 ns** | **0.1189 ns** | **0.0786 ns** |  **0.63** |    **0.03** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[5] |  3.6562 ns | 0.1698 ns | 0.1123 ns |  0.60 |    0.03 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[5] |  4.9434 ns | 0.3847 ns | 0.2289 ns |  0.82 |    0.05 |     475 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[5] |  4.7573 ns | 0.5709 ns | 0.3776 ns |  0.78 |    0.09 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[5] |  2.2075 ns | 0.1062 ns | 0.0703 ns |  0.36 |    0.01 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[6]** |  **4.7828 ns** | **0.5381 ns** | **0.3559 ns** |  **0.79** |    **0.07** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[6] |  4.9707 ns | 0.3219 ns | 0.2129 ns |  0.82 |    0.06 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[6] |  3.1889 ns | 0.2114 ns | 0.1399 ns |  0.52 |    0.03 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[6] |  4.8992 ns | 0.4674 ns | 0.3091 ns |  0.80 |    0.05 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[6] |  2.4825 ns | 0.2192 ns | 0.1450 ns |  0.41 |    0.03 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[7]** |  **4.8841 ns** | **0.3840 ns** | **0.2540 ns** |  **0.80** |    **0.05** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[7] |  4.5452 ns | 0.5057 ns | 0.3345 ns |  0.75 |    0.06 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[7] |  8.3064 ns | 0.6411 ns | 0.4241 ns |  1.36 |    0.09 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[7] |  6.0269 ns | 0.3554 ns | 0.2351 ns |  0.99 |    0.07 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[7] |  2.8706 ns | 0.0466 ns | 0.0121 ns |  0.47 |    0.01 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[8]** |  **5.2312 ns** | **0.1485 ns** | **0.0659 ns** |  **0.85** |    **0.03** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[8] |  5.2030 ns | 0.3040 ns | 0.2011 ns |  0.85 |    0.05 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[8] |  4.0067 ns | 0.0876 ns | 0.0136 ns |  0.66 |    0.02 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[8] |  2.9410 ns | 0.0252 ns | 0.0039 ns |  0.48 |    0.02 |     294 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[8] |  2.4924 ns | 0.2051 ns | 0.1357 ns |  0.41 |    0.03 |     265 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[9]** |  **5.4911 ns** | **0.5407 ns** | **0.3218 ns** |  **0.91** |    **0.09** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[9] |  6.0361 ns | 0.1573 ns | 0.0698 ns |  0.98 |    0.03 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[9] |  5.5288 ns | 0.0930 ns | 0.0241 ns |  0.90 |    0.03 |     475 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[9] |  5.5228 ns | 0.3499 ns | 0.2315 ns |  0.91 |    0.05 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[9] |  2.5919 ns | 0.3135 ns | 0.2073 ns |  0.43 |    0.05 |     265 B |         - |          NA |
