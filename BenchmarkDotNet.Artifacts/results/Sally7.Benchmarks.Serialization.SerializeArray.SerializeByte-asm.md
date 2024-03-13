## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33699F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC359B4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3369A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35D7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33999F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC35CB4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3399A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35D7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33899F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC35BB4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3389A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35E7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33699F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC359B4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3369A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35F7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33799F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC35AB4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3379A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35F7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33799F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC35AB4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3379A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35F7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33699F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC359B4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3369A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35D7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.SpanCopyTo()
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,28
       mov       rsi,rcx
       mov       rcx,[rsi+10]
       test      rcx,rcx
       je        short M00_L02
       lea       rdx,[rcx+10]
       mov       edi,[rcx+8]
M00_L00:
       mov       rcx,[rsi+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rbx,[rcx+10]
       mov       ebp,[rcx+8]
M00_L01:
       mov       rcx,rbx
       cmp       edi,ebp
       ja        short M00_L04
       mov       r8d,edi
       call      qword ptr [7FFEC33999F0]; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       mov       rax,[rsi+10]
       mov       eax,[rax+8]
       add       rsp,28
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       edx,edx
       xor       edi,edi
       jmp       short M00_L00
M00_L03:
       xor       ebx,ebx
       xor       ebp,ebp
       jmp       short M00_L01
M00_L04:
       call      qword ptr [7FFEC35CB4B0]
       int       3
; Total bytes of code 94
```
```assembly
; System.Buffer.Memmove(Byte ByRef, Byte ByRef, UIntPtr)
       vzeroupper
       mov       rax,rcx
       sub       rax,rdx
       cmp       rax,r8
       jae       short M01_L01
M01_L00:
       cmp       rcx,rdx
       je        near ptr M01_L09
       jmp       near ptr M01_L11
M01_L01:
       mov       rax,rdx
       sub       rax,rcx
       cmp       rax,r8
       jb        short M01_L00
       lea       rax,[rdx+r8]
       lea       r9,[rcx+r8]
       cmp       r8,10
       jbe       short M01_L04
       cmp       r8,40
       ja        near ptr M01_L07
M01_L02:
       vmovupd   xmm0,[rdx]
       vmovupd   [rcx],xmm0
       cmp       r8,20
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+10]
       vmovupd   [rcx+10],xmm0
       cmp       r8,30
       jbe       short M01_L03
       vmovupd   xmm0,[rdx+20]
       vmovupd   [rcx+20],xmm0
M01_L03:
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L04:
       test      r8b,18
       je        short M01_L05
       mov       r8,[rdx]
       mov       [rcx],r8
       mov       rdx,[rax-8]
       mov       [r9-8],rdx
       jmp       short M01_L09
M01_L05:
       test      r8b,4
       je        short M01_L06
       mov       r8d,[rdx]
       mov       [rcx],r8d
       mov       edx,[rax-4]
       mov       [r9-4],edx
       jmp       short M01_L09
M01_L06:
       test      r8,r8
       je        short M01_L09
       movzx     edx,byte ptr [rdx]
       mov       [rcx],dl
       test      r8b,2
       je        short M01_L09
       movsx     r8,word ptr [rax-2]
       mov       [r9-2],r8w
       jmp       short M01_L09
M01_L07:
       cmp       r8,800
       ja        short M01_L11
       mov       r10,r8
       shr       r10,6
M01_L08:
       vmovdqu   ymm0,ymmword ptr [rdx]
       vmovdqu   ymmword ptr [rcx],ymm0
       vmovdqu   ymm0,ymmword ptr [rdx+20]
       vmovdqu   ymmword ptr [rcx+20],ymm0
       add       rcx,40
       add       rdx,40
       dec       r10
       je        short M01_L10
       jmp       short M01_L08
M01_L09:
       ret
M01_L10:
       and       r8,3F
       cmp       r8,10
       ja        near ptr M01_L02
       vmovupd   xmm0,[rax-10]
       vmovupd   [r9-10],xmm0
       jmp       short M01_L09
M01_L11:
       jmp       qword ptr [7FFEC3399A08]; System.Buffer._Memmove(Byte ByRef, Byte ByRef, UIntPtr)
; Total bytes of code 270
```

## .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyUsingLargerPrimitives()
       push      rdi
       push      rsi
       sub       rsp,48
       xor       eax,eax
       mov       [rsp+28],rax
       vxorps    xmm4,xmm4,xmm4
       vmovdqa   xmmword ptr [rsp+30],xmm4
       mov       [rsp+40],rax
       mov       rdx,[rcx+10]
       test      rdx,rdx
       je        short M00_L02
       lea       rax,[rdx+10]
       mov       r8d,[rdx+8]
M00_L00:
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L03
       lea       rsi,[rcx+10]
       mov       edi,[rcx+8]
M00_L01:
       mov       [rsp+38],rax
       mov       [rsp+40],r8d
       mov       [rsp+28],rsi
       mov       [rsp+30],edi
       lea       rcx,[rsp+38]
       lea       rdx,[rsp+28]
       call      qword ptr [7FFEC35F7948]; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       nop
       add       rsp,48
       pop       rsi
       pop       rdi
       ret
M00_L02:
       xor       eax,eax
       xor       r8d,r8d
       jmp       short M00_L00
M00_L03:
       xor       esi,esi
       xor       edi,edi
       jmp       short M00_L01
; Total bytes of code 117
```
```assembly
; Sally7.Benchmarks.Serialization.SerializeArray+SerializeByte.CopyBytes(System.ReadOnlySpan`1<Byte>, System.Span`1<Byte>)
       mov       rax,[rcx]
       mov       ecx,[rcx+8]
       mov       rdx,[rdx]
       xor       r8d,r8d
       lea       r9d,[rcx-8]
       movsxd    r9,r9d
       test      r9,r9
       jl        short M01_L01
       nop       dword ptr [rax+rax]
M01_L00:
       mov       r10d,r8d
       mov       r11,[rax+r10]
       mov       [rdx+r10],r11
       add       r8d,8
       mov       r10d,r8d
       cmp       r10,r9
       jle       short M01_L00
M01_L01:
       mov       r9d,r8d
       lea       r10d,[rcx-4]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L02
       mov       r10d,[rax+r9]
       mov       [rdx+r9],r10d
       add       r8d,4
M01_L02:
       mov       r9d,r8d
       lea       r10d,[rcx-2]
       movsxd    r10,r10d
       cmp       r9,r10
       jg        short M01_L03
       movzx     r10d,word ptr [rax+r9]
       mov       [rdx+r9],r10w
       add       r8d,2
M01_L03:
       mov       r9d,r8d
       movsxd    rcx,ecx
       cmp       r9,rcx
       jge       short M01_L04
       movzx     eax,byte ptr [rax+r9]
       mov       [rdx+r9],al
       inc       r8d
M01_L04:
       mov       eax,r8d
       ret
; Total bytes of code 138
```

