## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDA7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDC7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDD7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDE7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDA7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDB7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDD7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDC7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDD7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDA7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDB7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.SerializeOneByOne()
       push      rdi
       push      rsi
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+28],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rdi
       mov       rdx,[rsi+10]
       test      rdx,rdx
       jne       short M00_L01
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L02
M00_L01:
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L02:
       mov       [rsp+28],rax
       mov       [rsp+30],r8d
       lea       rdx,[rsp+28]
       call      qword ptr [7FFE6DDB7A80]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       eax,eax
       add       rsp,38
       pop       rsi
       pop       rdi
       ret
M00_L03:
       xor       edi,edi
       jmp       short M00_L00
; Total bytes of code 100
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.UsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        short M00_L06
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        short M00_L04
       xor       eax,eax
       xor       r9d,r9d
       cmp       dword ptr [r8+8],0
       jle       short M00_L03
       nop       dword ptr [rax]
       nop       dword ptr [rax]
M00_L02:
       mov       r8d,eax
       movzx     r11d,word ptr [r10+r8]
       movbe     [rdx+r8],r11w
       add       eax,2
       inc       r9d
       mov       r8,[rcx+10]
       cmp       [r8+8],r9d
       jg        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       short M00_L00
M00_L06:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       short M00_L01
; Total bytes of code 143
```

