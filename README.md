# Sally7
C# implementation of Siemens S7 connections with a focus on performance



![license](https://img.shields.io/github/license/mycroes/Sally7.svg)
[![.NET](https://github.com/mycroes/Sally7/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mycroes/Sally7/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/Sally7.svg)](https://www.nuget.org/packages/Sally7)


## What is the S7 protocol?
The **S7** protocol is a proprietary protocol for PLC communication with and between Siemens S7 PLC's.
It's making use of **COTP** (*Connection Oriented Transport Protocol*, ISO 8073 / [RFC 905](https://tools.ietf.org/html/rfc905))
 and **TPKT** (*ISO Transport Service on top of the TCP Version 3*, [RFC 1006](https://tools.ietf.org/html/rfc1006)).
The S7 protocol uses **ConnectionRequest** (CC) and **ConnectionConfirm** (CR)
 from COTP for connection management and COTP **DataTransfer** (DT) to wrap S7 protocol functions.
A good description is available at [The Siemens S7 Communication - Part 1 General Structure](http://gmiru.com/article/s7comm/)
 and [The Siemens S7 Communication - Part 2 Job Requests and Ack Data](http://gmiru.com/article/s7comm-part2/).

## The implementation of Sally7
Sally7 supports the most basic read and write actions to the **DataBlock** area.
All protocols are mapped to `struct`s and `enum`s with the intent to create a project that is easy to comprehend and extend.

## How to get started
### Connect to a PLC
Connect to a S7-1500 PLC at adress 192.168.0.15 on Rack 0, Slot 1
```
var connection = Sally7.Plc.ConnectionFactory.GetConnection(host: "192.168.0.15", cpuType: Sally7.Plc.CpuType.S7_1500, rack: 0, slot: 1);
await connection.OpenAsync();
```
### Read a DataBlockDataItem
Read 10 bytes from DataBlock 87 starting at address 54
```
 var dataItem = new DataBlockDataItem<byte[]>(
     dbNumber: 87,
     startByte: 54,
     length: 10
 );
 await connection.ReadAsync(dataItem);
 Console.WriteLine($"Read data: {BitConverter.ToString(dataItem.Value)}");
```
