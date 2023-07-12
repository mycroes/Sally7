## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9CFE1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9CFE1870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9CFE1858]
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CF97498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9D021858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D011870]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt32(Byte ByRef, System.ReadOnlySpan`1<UInt32>)
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt32(Byte ByRef, System.ReadOnlySpan`1<UInt32>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       mov       r10d,[r8+r10*4]
       mov       r11d,eax
       movbe     [rcx+r11],r10d
       add       eax,4
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M03_L01
       nop
M03_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M03_L00
M03_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9CFE1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9CFE1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CF97498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9D021858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9CFE1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9CFE1870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9CFE1858]
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CF97498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9CFE1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9CFE1858]
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CF97498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D011870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D021888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D021870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D021858]
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFD7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9CFF1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFA7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9CFE1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9CFE1870]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt32(Byte ByRef, System.ReadOnlySpan`1<UInt32>)
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9CFE1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CF97498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt32(Byte ByRef, System.ReadOnlySpan`1<UInt32>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       mov       r10d,[r8+r10*4]
       mov       r11d,eax
       movbe     [rcx+r11],r10d
       add       eax,4
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M03_L01
       nop
M03_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M03_L00
M03_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9D021888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9D021858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFD7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9D001858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9CFF1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9CFF1870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9CFF1858]
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFA7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9CFF1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9CFF1858]
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFA7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D011870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D011870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D011858]
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9CFF1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFA7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9D011858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D021888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D021870]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt32(Byte ByRef, System.ReadOnlySpan`1<UInt32>)
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D021858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFD7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt32(Byte ByRef, System.ReadOnlySpan`1<UInt32>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       mov       r10d,[r8+r10*4]
       mov       r11d,eax
       movbe     [rcx+r11],r10d
       add       eax,4
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M03_L01
       nop
M03_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M03_L00
M03_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9CFF1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFA7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9CFF1858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D011888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D011870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D011858]
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFC7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9CFE1888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9CFE1858]
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CF97498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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
       call      qword ptr [7FFD9D021858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
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

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAs()
       push      r15
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,58
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       vmovdqa   xmmword ptr [rsp+40],xmm4
       mov       [rsp+50],rax
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L07
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L08
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        near ptr M00_L06
       mov       [rsp+48],rbp
       mov       [rsp+50],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+48]
       call      qword ptr [7FFD9D001888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ebp,[rcx+8]
       and       ebp,3
       cmp       ebp,2
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        near ptr M00_L09
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        near ptr M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       sub       r15d,ecx
       mov       ecx,r15d
       add       rcx,rcx
       shr       rcx,2
       cmp       rcx,7FFFFFFF
       ja        short M00_L06
       mov       [rsp+38],rdx
       mov       [rsp+40],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+38]
       call      qword ptr [7FFD9D001870]
       add       ebx,eax
M00_L03:
       test      bpl,1
       je        short M00_L05
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L10
       lea       r14,[rcx+10]
       mov       r15d,[rcx+8]
M00_L04:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r15d
       ja        short M00_L11
       mov       edx,ecx
       lea       rdx,[r14+rdx*2]
       mov       eax,r15d
       sub       eax,ecx
       mov       rcx,rdi
       mov       [rsp+28],rdx
       mov       [rsp+30],eax
       lea       rdx,[rsp+28]
       call      qword ptr [7FFD9D001858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       eax,ebx
       mov       ebx,eax
M00_L05:
       mov       eax,ebx
       add       rsp,58
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       pop       r15
       ret
M00_L06:
       call      CORINFO_HELP_OVERFLOW
M00_L07:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L08:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L09:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       near ptr M00_L02
M00_L10:
       xor       r14d,r14d
       xor       r15d,r15d
       jmp       short M00_L04
M00_L11:
       call      qword ptr [7FFD9CFB7498]
       int       3
; Total bytes of code 385
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeAsNoRemainderSplit()
       push      r14
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,40
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+20],xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       rsi,rcx
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        near ptr M00_L05
       lea       rdi,[rcx+10]
       mov       ecx,[rcx+8]
       mov       rbx,[rsi+10]
       test      rbx,rbx
       je        near ptr M00_L06
M00_L00:
       lea       rbp,[rbx+10]
       mov       r14d,[rbx+8]
M00_L01:
       mov       ecx,r14d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       [rsp+30],rbp
       mov       [rsp+38],ecx
       mov       rcx,rdi
       lea       rdx,[rsp+30]
       call      qword ptr [7FFD9D001888]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       mov       ebx,eax
       mov       rcx,[rsi+10]
       mov       ecx,[rcx+8]
       and       ecx,3
       jle       short M00_L03
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L07
       lea       rbp,[rcx+10]
       mov       r14d,[rcx+8]
M00_L02:
       mov       ecx,ebx
       shr       ecx,1F
       add       ecx,ebx
       sar       ecx,1
       cmp       ecx,r14d
       ja        short M00_L08
       mov       edx,ecx
       lea       rdx,[rbp+rdx*2]
       sub       r14d,ecx
       mov       rcx,rdi
       mov       [rsp+20],rdx
       mov       [rsp+28],r14d
       lea       rdx,[rsp+20]
       call      qword ptr [7FFD9D001858]; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       add       ebx,eax
M00_L03:
       mov       eax,ebx
       add       rsp,40
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       pop       r14
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edi,edi
       mov       rbx,[rsi+10]
       test      rbx,rbx
       jne       near ptr M00_L00
M00_L06:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       near ptr M00_L01
M00_L07:
       xor       ebp,ebp
       xor       r14d,r14d
       jmp       short M00_L02
M00_L08:
       call      qword ptr [7FFD9CFB7498]
       int       3
; Total bytes of code 250
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt64(Byte ByRef, System.ReadOnlySpan`1<UInt64>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M01_L01
       nop
M01_L00:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,eax
       movbe     [rcx+r11],r10
       add       eax,8
       inc       r9d
       cmp       r9d,edx
       jl        short M01_L00
M01_L01:
       ret
; Total bytes of code 44
```
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.WriteUInt16(Byte ByRef, System.ReadOnlySpan`1<UInt16>)
       xor       eax,eax
       mov       r8,[rdx]
       mov       edx,[rdx+8]
       xor       r9d,r9d
       test      edx,edx
       jle       short M02_L01
       nop
M02_L00:
       mov       r10d,r9d
       movzx     r10d,word ptr [r8+r10*2]
       mov       r11d,eax
       movbe     [rcx+r11],r10w
       add       eax,2
       inc       r9d
       cmp       r9d,edx
       jl        short M02_L00
M02_L01:
       ret
; Total bytes of code 46
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive+SerializeUInt16.CombineUsingUnsafeReadUnaligned()
       sub       rsp,28
       mov       rax,[rcx+8]
       test      rax,rax
       je        near ptr M00_L08
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       je        near ptr M00_L09
M00_L00:
       lea       r10,[r9+10]
       mov       r11d,[r9+8]
M00_L01:
       mov       eax,r11d
       add       rax,rax
       cmp       rax,7FFFFFFF
       ja        near ptr M00_L07
       xor       eax,eax
       xor       r9d,r9d
       mov       r8d,[r8+8]
       shr       r8d,2
       test      r8d,r8d
       jle       short M00_L03
M00_L02:
       mov       r8d,eax
       mov       r11,[r10+r8]
       movbe     [rdx+r8],r11
       add       eax,8
       inc       r9d
       mov       r8,[rcx+10]
       mov       r8d,[r8+8]
       shr       r8d,2
       cmp       r8d,r9d
       jg        short M00_L02
M00_L03:
       mov       r8d,eax
       shr       r8d,2
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jle       short M00_L05
M00_L04:
       mov       r9d,eax
       mov       r11d,[r10+r9]
       movbe     [rdx+r9],r11d
       add       eax,4
       inc       r8d
       mov       r9,[rcx+10]
       mov       r9d,[r9+8]
       shr       r9d,1
       movsxd    r9,r9d
       mov       r11d,r8d
       cmp       r9,r11
       jg        short M00_L04
M00_L05:
       mov       r8d,eax
       shr       r8d,1
       mov       rcx,[rcx+10]
       cmp       r8d,[rcx+8]
       jae       short M00_L06
       mov       ecx,eax
       movzx     ecx,word ptr [r10+rcx]
       mov       r8d,eax
       movbe     [rdx+r8],cx
       add       eax,4
M00_L06:
       add       rsp,28
       ret
M00_L07:
       call      CORINFO_HELP_OVERFLOW
M00_L08:
       xor       edx,edx
       mov       r8,[rcx+10]
       mov       r9,r8
       test      r9,r9
       jne       near ptr M00_L00
M00_L09:
       xor       r10d,r10d
       xor       r11d,r11d
       jmp       near ptr M00_L01
; Total bytes of code 265
```

