``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3208/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.302
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-MHLPHK : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Runtime=.NET 7.0  Toolchain=net70  IterationTime=100.0000 ms  
WarmupCount=3  

```
|                               Method |      Value |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Code Size | Allocated | Alloc Ratio |
|------------------------------------- |----------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|------------:|
|                    **SerializeOneByOne** | **UInt16[10]** | **6.6861 ns** | **0.1741 ns** | **0.2658 ns** | **6.7238 ns** |  **1.00** |    **0.00** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned | UInt16[10] | 6.3408 ns | 0.1650 ns | 0.2146 ns | 6.3649 ns |  0.95 |    0.06 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs | UInt16[10] | 3.8173 ns | 0.1109 ns | 0.1660 ns | 3.8626 ns |  0.57 |    0.03 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | UInt16[10] | 6.3303 ns | 0.1613 ns | 0.4655 ns | 6.2670 ns |  0.98 |    0.09 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | UInt16[10] | 2.2504 ns | 0.0782 ns | 0.1195 ns | 2.2341 ns |  0.34 |    0.02 |     246 B |         - |          NA |
|                    **SerializeOneByOne** | **UInt16[11]** | **7.8534 ns** | **0.1899 ns** | **0.2261 ns** | **7.8397 ns** |  **1.16** |    **0.05** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned | UInt16[11] | 7.1007 ns | 0.1833 ns | 0.2799 ns | 7.1111 ns |  1.06 |    0.07 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs | UInt16[11] | 7.9936 ns | 0.2043 ns | 0.3577 ns | 8.0194 ns |  1.19 |    0.08 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | UInt16[11] | 6.3445 ns | 0.1647 ns | 0.1618 ns | 6.3229 ns |  0.94 |    0.04 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | UInt16[11] | 2.6995 ns | 0.0875 ns | 0.1387 ns | 2.6894 ns |  0.40 |    0.03 |     246 B |         - |          NA |
|                    **SerializeOneByOne** | **UInt16[12]** | **7.8701 ns** | **0.1929 ns** | **0.2575 ns** | **7.8173 ns** |  **1.18** |    **0.07** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned | UInt16[12] | 7.8940 ns | 0.1938 ns | 0.2653 ns | 7.8425 ns |  1.18 |    0.07 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs | UInt16[12] | 4.7680 ns | 0.1263 ns | 0.1240 ns | 4.7903 ns |  0.71 |    0.04 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit | UInt16[12] | 4.0652 ns | 0.1133 ns | 0.1551 ns | 4.1185 ns |  0.61 |    0.03 |     294 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned | UInt16[12] | 2.6758 ns | 0.0863 ns | 0.0886 ns | 2.7065 ns |  0.40 |    0.02 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[1]** | **1.9831 ns** | **0.0732 ns** | **0.2124 ns** | **1.9235 ns** |  **0.31** |    **0.04** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[1] | 0.9753 ns | 0.0433 ns | 0.0384 ns | 0.9662 ns |  0.14 |    0.01 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[1] | 5.0231 ns | 0.1323 ns | 0.1980 ns | 4.9652 ns |  0.75 |    0.04 |     475 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[1] | 4.3752 ns | 0.1222 ns | 0.2076 ns | 4.3878 ns |  0.65 |    0.05 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[1] | 1.4597 ns | 0.0624 ns | 0.0583 ns | 1.4340 ns |  0.22 |    0.01 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[2]** | **2.3915 ns** | **0.0811 ns** | **0.1287 ns** | **2.3775 ns** |  **0.36** |    **0.03** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[2] | 1.5340 ns | 0.0624 ns | 0.0767 ns | 1.5335 ns |  0.23 |    0.02 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[2] | 3.2819 ns | 0.0963 ns | 0.1527 ns | 3.2825 ns |  0.49 |    0.03 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[2] | 4.9738 ns | 0.1325 ns | 0.2389 ns | 4.9813 ns |  0.74 |    0.04 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[2] | 1.4603 ns | 0.0622 ns | 0.1022 ns | 1.4546 ns |  0.22 |    0.02 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[3]** | **3.1588 ns** | **0.0954 ns** | **0.0892 ns** | **3.1571 ns** |  **0.47** |    **0.02** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[3] | 2.2118 ns | 0.0790 ns | 0.1907 ns | 2.1467 ns |  0.35 |    0.04 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[3] | 6.9021 ns | 0.1638 ns | 0.2186 ns | 6.8712 ns |  1.03 |    0.05 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[3] | 5.1781 ns | 0.1354 ns | 0.2225 ns | 5.2195 ns |  0.77 |    0.04 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[3] | 1.4714 ns | 0.0581 ns | 0.0869 ns | 1.4597 ns |  0.22 |    0.02 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[4]** | **3.8326 ns** | **0.1088 ns** | **0.1757 ns** | **3.8471 ns** |  **0.57** |    **0.03** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[4] | 2.6588 ns | 0.0850 ns | 0.1372 ns | 2.6938 ns |  0.40 |    0.02 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[4] | 3.1785 ns | 0.0957 ns | 0.1797 ns | 3.2597 ns |  0.48 |    0.03 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[4] | 2.8089 ns | 0.0903 ns | 0.1696 ns | 2.7981 ns |  0.43 |    0.03 |     294 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[4] | 1.5709 ns | 0.0632 ns | 0.0885 ns | 1.5382 ns |  0.23 |    0.02 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[5]** | **4.5989 ns** | **0.1258 ns** | **0.2236 ns** | **4.6197 ns** |  **0.69** |    **0.04** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[5] | 4.3601 ns | 0.1206 ns | 0.3481 ns | 4.3677 ns |  0.67 |    0.06 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[5] | 5.9972 ns | 0.1551 ns | 0.3803 ns | 6.0296 ns |  0.87 |    0.08 |     475 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[5] | 5.5710 ns | 0.1466 ns | 0.2449 ns | 5.5219 ns |  0.84 |    0.06 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[5] | 1.8815 ns | 0.0455 ns | 0.0355 ns | 1.8803 ns |  0.28 |    0.01 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[6]** | **6.1517 ns** | **0.1631 ns** | **0.3908 ns** | **6.1215 ns** |  **0.92** |    **0.07** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[6] | 5.4642 ns | 0.1504 ns | 0.3172 ns | 5.4391 ns |  0.80 |    0.06 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[6] | 3.4538 ns | 0.1180 ns | 0.3385 ns | 3.3771 ns |  0.57 |    0.04 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[6] | 5.4017 ns | 0.1475 ns | 0.2019 ns | 5.3347 ns |  0.81 |    0.04 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[6] | 1.9054 ns | 0.0728 ns | 0.2030 ns | 1.8573 ns |  0.31 |    0.03 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[7]** | **5.5903 ns** | **0.1507 ns** | **0.1850 ns** | **5.6027 ns** |  **0.83** |    **0.04** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[7] | 4.9453 ns | 0.1413 ns | 0.2584 ns | 4.8706 ns |  0.74 |    0.04 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[7] | 8.1934 ns | 0.1986 ns | 0.4233 ns | 8.1728 ns |  1.24 |    0.06 |     519 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[7] | 6.2935 ns | 0.1547 ns | 0.1520 ns | 6.2941 ns |  0.93 |    0.04 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[7] | 2.2472 ns | 0.0789 ns | 0.1558 ns | 2.2215 ns |  0.33 |    0.03 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[8]** | **6.6729 ns** | **0.1749 ns** | **0.4483 ns** | **6.6541 ns** |  **1.00** |    **0.07** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[8] | 5.7579 ns | 0.1316 ns | 0.1408 ns | 5.7533 ns |  0.85 |    0.04 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[8] | 4.1512 ns | 0.1177 ns | 0.1572 ns | 4.1189 ns |  0.62 |    0.02 |     429 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[8] | 3.5086 ns | 0.0985 ns | 0.1011 ns | 3.4762 ns |  0.52 |    0.02 |     294 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[8] | 2.3359 ns | 0.0814 ns | 0.2072 ns | 2.3022 ns |  0.37 |    0.03 |     246 B |         - |          NA |
|                    **SerializeOneByOne** |  **UInt16[9]** | **6.5947 ns** | **0.1645 ns** | **0.2562 ns** | **6.5555 ns** |  **0.99** |    **0.05** |     **146 B** |         **-** |          **NA** |
|             UsingUnsafeReadUnaligned |  UInt16[9] | 6.4567 ns | 0.1588 ns | 0.1485 ns | 6.4424 ns |  0.96 |    0.05 |     143 B |         - |          NA |
|                 CombineUsingUnsafeAs |  UInt16[9] | 5.8589 ns | 0.1554 ns | 0.2419 ns | 5.8869 ns |  0.88 |    0.05 |     475 B |         - |          NA |
| CombineUsingUnsafeAsNoRemainderSplit |  UInt16[9] | 5.6561 ns | 0.1532 ns | 0.2340 ns | 5.6530 ns |  0.85 |    0.05 |     340 B |         - |          NA |
|      CombineUsingUnsafeReadUnaligned |  UInt16[9] | 2.2442 ns | 0.0791 ns | 0.1057 ns | 2.2629 ns |  0.34 |    0.02 |     246 B |         - |          NA |
