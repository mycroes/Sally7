## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitives.WriteUInt64()
       mov       rax,[rcx+10]
       test      rax,rax
       je        short M00_L01
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
M00_L00:
       mov       rcx,[rcx+18]
       movbe     [rdx],rcx
       ret
M00_L01:
       xor       edx,edx
       jmp       short M00_L00
; Total bytes of code 30
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitives.WriteUInt64ArrayIncrementOffset()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       [rsp+50],rcx
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L01
       lea       rsi,[rdx+10]
       mov       edi,[rdx+8]
M00_L00:
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+28]
       mov       rax,[rsp+50]
       mov       rdx,[rax+8]
       call      qword ptr [7FFD79FB1618]; Sally7.Benchmarks.Serialization.SerializePrimitives+Converters.AppendUInt64ArrayIncrementDestination(System.Span`1<Byte>, UInt64[])
       nop
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L01:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 77
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitives+Converters.AppendUInt64ArrayIncrementDestination(System.Span`1<Byte>, UInt64[])
       mov       rax,[rcx]
       xor       ecx,ecx
       mov       r8d,[rdx+8]
       test      r8d,r8d
       jle       short M01_L01
       xchg      ax,ax
M01_L00:
       mov       r9d,ecx
       mov       r9,[rdx+r9*8+10]
       movbe     [rax],r9
       add       rax,8
       inc       ecx
       cmp       r8d,ecx
       jg        short M01_L00
M01_L01:
       ret
; Total bytes of code 41
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitives.WriteUInt64ArrayCalculateOffset()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       [rsp+50],rcx
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L01
       lea       rsi,[rdx+10]
       mov       edi,[rdx+8]
M00_L00:
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+28]
       mov       rax,[rsp+50]
       mov       rdx,[rax+8]
       call      qword ptr [7FFD79FC1330]; Sally7.Benchmarks.Serialization.SerializePrimitives+Converters.AppendUInt64ArrayCalculateOffset(System.Span`1<Byte>, UInt64[])
       nop
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L01:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 77
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitives+Converters.AppendUInt64ArrayCalculateOffset(System.Span`1<Byte>, UInt64[])
       mov       rax,[rcx]
       xor       ecx,ecx
       mov       r8d,[rdx+8]
       test      r8d,r8d
       je        short M01_L01
       xchg      ax,ax
M01_L00:
       mov       r9d,ecx
       mov       r9,[rdx+r9*8+10]
       mov       r10d,ecx
       shl       r10d,3
       movbe     [rax+r10],r9
       inc       ecx
       cmp       r8d,ecx
       ja        short M01_L00
M01_L01:
       ret
; Total bytes of code 45
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitives.WriteUInt64ArrayStoreOffset()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       [rsp+50],rcx
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L01
       lea       rsi,[rdx+10]
       mov       edi,[rdx+8]
M00_L00:
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+28]
       mov       rax,[rsp+50]
       mov       rdx,[rax+8]
       call      qword ptr [7FFD79FC1348]; Sally7.Benchmarks.Serialization.SerializePrimitives+Converters.AppendUInt64ArrayStoreOffset(System.Span`1<Byte>, UInt64[])
       nop
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L01:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 77
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitives+Converters.AppendUInt64ArrayStoreOffset(System.Span`1<Byte>, UInt64[])
       mov       rax,[rcx]
       xor       ecx,ecx
       xor       r8d,r8d
       mov       r9d,[rdx+8]
       test      r9d,r9d
       jle       short M01_L01
       nop       dword ptr [rax]
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r10,[rdx+r10*8+10]
       mov       r11d,ecx
       movbe     [rax+r11],r10
       add       ecx,8
       inc       r8d
       cmp       r9d,r8d
       jg        short M01_L00
M01_L01:
       ret
; Total bytes of code 61
```

