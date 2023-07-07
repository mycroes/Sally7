## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.SerializeOneByOne()
       sub       rsp,28
       mov       rax,[rcx+10]
       test      rax,rax
       je        short M00_L03
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
M00_L00:
       xor       eax,eax
       xor       r8d,r8d
       mov       r9,[rcx+8]
       cmp       dword ptr [r9+8],0
       jle       short M00_L02
M00_L01:
       mov       r9,[rcx+8]
       cmp       r8d,[r9+8]
       jae       short M00_L04
       mov       r10d,r8d
       movzx     r9d,word ptr [r9+r10*2+10]
       mov       r10d,eax
       movbe     [rdx+r10],r9w
       add       eax,2
       inc       r8d
       mov       r9,[rcx+8]
       cmp       [r9+8],r8d
       jg        short M00_L01
M00_L02:
       add       rsp,28
       ret
M00_L03:
       xor       edx,edx
       jmp       short M00_L00
M00_L04:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 96
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.Serialization.SerializePrimitivesAsLargerPrimitive.SerializeAsLarger()
       sub       rsp,28
       mov       rax,[rcx+10]
       test      rax,rax
       je        short M00_L05
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L06
M00_L00:
       lea       r8,[rax+10]
       mov       r9d,[rax+8]
M00_L01:
       mov       ecx,r9d
       add       rcx,rcx
       shr       rcx,3
       cmp       rcx,7FFFFFFF
       ja        short M00_L04
       mov       eax,ecx
       xor       ecx,ecx
       xor       r9d,r9d
       test      eax,eax
       jle       short M00_L03
M00_L02:
       mov       r10d,r9d
       mov       r10,[r8+r10*8]
       mov       r11d,ecx
       movbe     [rdx+r11],r10
       add       ecx,8
       inc       r9d
       cmp       r9d,eax
       jl        short M00_L02
M00_L03:
       add       rsp,28
       ret
M00_L04:
       call      CORINFO_HELP_OVERFLOW
M00_L05:
       xor       edx,edx
       mov       rax,[rcx+8]
       test      rax,rax
       jne       short M00_L00
M00_L06:
       xor       r8d,r8d
       xor       r9d,r9d
       jmp       short M00_L01
; Total bytes of code 123
```

