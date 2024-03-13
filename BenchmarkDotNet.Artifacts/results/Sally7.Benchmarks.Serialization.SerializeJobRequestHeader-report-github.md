``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.3448/22H2/2022Update)
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2


```
|              Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Completed Work Items | Lock Contentions | Code Size | Allocated | Alloc Ratio |
|-------------------- |----------:|----------:|----------:|----------:|------:|--------:|---------------------:|-----------------:|----------:|----------:|------------:|
| WriteFieldsOneByOne | 0.2237 ns | 0.0365 ns | 0.0929 ns | 0.2225 ns |  1.00 |    0.00 |                    - |                - |      50 B |         - |          NA |
|  WriteLongThenShort | 0.2529 ns | 0.0352 ns | 0.0329 ns | 0.2551 ns |  1.00 |    0.69 |                    - |                - |      48 B |         - |          NA |
|      WriteVector128 | 0.0072 ns | 0.0105 ns | 0.0166 ns | 0.0000 ns |  0.03 |    0.07 |                    - |                - |      41 B |         - |          NA |
|          WriteArray | 6.7946 ns | 0.1702 ns | 0.2442 ns | 6.8168 ns | 35.15 |   16.45 |                    - |                - |     197 B |         - |          NA |
|         WriteStruct | 5.7721 ns | 0.1242 ns | 0.1275 ns | 5.7772 ns | 24.70 |   14.39 |                    - |                - |     148 B |         - |          NA |
