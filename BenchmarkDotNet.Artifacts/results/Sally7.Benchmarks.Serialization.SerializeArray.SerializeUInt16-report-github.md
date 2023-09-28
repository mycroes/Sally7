``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3448/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
  Job-DPCBSB : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2

IterationTime=150.0000 ms  WarmupCount=2  

```
|                       Method |      Value |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Completed Work Items | Lock Contentions | Code Size | Allocated | Alloc Ratio |
|----------------------------- |----------- |----------:|----------:|----------:|----------:|------:|--------:|---------------------:|-----------------:|----------:|----------:|------------:|
|            **SerializeOneByOne** | **UInt16[10]** | **6.7676 ns** | **0.1555 ns** | **0.1379 ns** | **6.7697 ns** |  **1.00** |    **0.00** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned | UInt16[10] | 6.7781 ns | 0.1524 ns | 0.1426 ns | 6.7463 ns |  1.00 |    0.03 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan | UInt16[10] | 5.3003 ns | 0.1409 ns | 0.3265 ns | 5.2767 ns |  0.79 |    0.06 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign | UInt16[10] | 5.1882 ns | 0.1385 ns | 0.3696 ns | 5.1348 ns |  0.78 |    0.05 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** | **UInt16[11]** | **7.3683 ns** | **0.1758 ns** | **0.1881 ns** | **7.4333 ns** |  **1.09** |    **0.04** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned | UInt16[11] | 7.7785 ns | 0.1900 ns | 0.2901 ns | 7.7991 ns |  1.17 |    0.04 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan | UInt16[11] | 5.3097 ns | 0.1380 ns | 0.1291 ns | 5.3539 ns |  0.78 |    0.03 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign | UInt16[11] | 5.0787 ns | 0.1331 ns | 0.2187 ns | 5.1052 ns |  0.75 |    0.05 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** | **UInt16[12]** | **7.4508 ns** | **0.1809 ns** | **0.1692 ns** | **7.4458 ns** |  **1.10** |    **0.03** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned | UInt16[12] | 7.5599 ns | 0.1877 ns | 0.3238 ns | 7.5065 ns |  1.11 |    0.06 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan | UInt16[12] | 5.6171 ns | 0.1465 ns | 0.2527 ns | 5.6522 ns |  0.83 |    0.03 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign | UInt16[12] | 5.5608 ns | 0.1420 ns | 0.1578 ns | 5.5577 ns |  0.82 |    0.03 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[1]** | **1.8980 ns** | **0.0737 ns** | **0.1232 ns** | **1.8702 ns** |  **0.29** |    **0.02** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[1] | 0.9585 ns | 0.0520 ns | 0.0779 ns | 0.9602 ns |  0.14 |    0.01 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[1] | 0.7194 ns | 0.0490 ns | 0.0638 ns | 0.7136 ns |  0.11 |    0.01 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[1] | 0.8293 ns | 0.0495 ns | 0.1269 ns | 0.7978 ns |  0.12 |    0.02 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[2]** | **2.4908 ns** | **0.0801 ns** | **0.0749 ns** | **2.4860 ns** |  **0.37** |    **0.01** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[2] | 1.4594 ns | 0.0609 ns | 0.0677 ns | 1.4766 ns |  0.22 |    0.01 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[2] | 1.2157 ns | 0.0515 ns | 0.0481 ns | 1.2196 ns |  0.18 |    0.01 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[2] | 1.4421 ns | 0.0613 ns | 0.0797 ns | 1.4363 ns |  0.21 |    0.01 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[3]** | **3.2555 ns** | **0.0968 ns** | **0.1325 ns** | **3.2564 ns** |  **0.48** |    **0.02** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[3] | 2.1776 ns | 0.0761 ns | 0.0876 ns | 2.1652 ns |  0.32 |    0.02 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[3] | 1.4623 ns | 0.0634 ns | 0.0623 ns | 1.4698 ns |  0.22 |    0.01 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[3] | 1.4795 ns | 0.0603 ns | 0.0564 ns | 1.4762 ns |  0.22 |    0.01 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[4]** | **3.6758 ns** | **0.1063 ns** | **0.1455 ns** | **3.6887 ns** |  **0.54** |    **0.02** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[4] | 2.7321 ns | 0.0778 ns | 0.0690 ns | 2.7248 ns |  0.40 |    0.01 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[4] | 1.9155 ns | 0.0722 ns | 0.1059 ns | 1.9169 ns |  0.28 |    0.02 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[4] | 1.9845 ns | 0.0709 ns | 0.0896 ns | 1.9916 ns |  0.29 |    0.01 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[5]** | **4.6423 ns** | **0.1260 ns** | **0.3318 ns** | **4.5721 ns** |  **0.66** |    **0.03** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[5] | 4.1888 ns | 0.0988 ns | 0.1098 ns | 4.1633 ns |  0.62 |    0.02 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[5] | 2.2390 ns | 0.0579 ns | 0.0541 ns | 2.2507 ns |  0.33 |    0.01 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[5] | 2.4711 ns | 0.0823 ns | 0.1256 ns | 2.4921 ns |  0.36 |    0.02 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[6]** | **4.9294 ns** | **0.1281 ns** | **0.1711 ns** | **4.8601 ns** |  **0.72** |    **0.03** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[6] | 4.9451 ns | 0.1270 ns | 0.1462 ns | 4.9462 ns |  0.72 |    0.03 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[6] | 2.9418 ns | 0.0906 ns | 0.1241 ns | 2.9539 ns |  0.43 |    0.02 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[6] | 2.8518 ns | 0.0906 ns | 0.1178 ns | 2.8391 ns |  0.42 |    0.02 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[7]** | **5.6504 ns** | **0.1468 ns** | **0.2328 ns** | **5.6487 ns** |  **0.83** |    **0.05** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[7] | 5.1182 ns | 0.1379 ns | 0.3486 ns | 5.0251 ns |  0.82 |    0.05 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[7] | 3.1176 ns | 0.0946 ns | 0.1127 ns | 3.1385 ns |  0.46 |    0.02 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[7] | 3.0415 ns | 0.0908 ns | 0.1614 ns | 3.0384 ns |  0.45 |    0.03 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[8]** | **5.8796 ns** | **0.1415 ns** | **0.1514 ns** | **5.8546 ns** |  **0.87** |    **0.03** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[8] | 5.8337 ns | 0.1530 ns | 0.1989 ns | 5.8520 ns |  0.87 |    0.03 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[8] | 3.5075 ns | 0.1050 ns | 0.1167 ns | 3.4913 ns |  0.51 |    0.01 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[8] | 3.6621 ns | 0.1054 ns | 0.1477 ns | 3.6645 ns |  0.54 |    0.03 |                    - |                - |     111 B |         - |          NA |
|            **SerializeOneByOne** |  **UInt16[9]** | **6.2735 ns** | **0.1208 ns** | **0.1009 ns** | **6.2891 ns** |  **0.93** |    **0.03** |                    **-** |                **-** |     **146 B** |         **-** |          **NA** |
|     UsingUnsafeReadUnaligned |  UInt16[9] | 6.5691 ns | 0.1677 ns | 0.4051 ns | 6.4135 ns |  0.95 |    0.04 |                    - |                - |     143 B |         - |          NA |
| UsingUnsafeIsAddressLessThan |  UInt16[9] | 4.1082 ns | 0.1027 ns | 0.0961 ns | 4.1170 ns |  0.61 |    0.01 |                    - |                - |     126 B |         - |          NA |
|            UsingCopyAndAlign |  UInt16[9] | 4.3313 ns | 0.1195 ns | 0.1423 ns | 4.3350 ns |  0.63 |    0.02 |                    - |                - |     111 B |         - |          NA |
