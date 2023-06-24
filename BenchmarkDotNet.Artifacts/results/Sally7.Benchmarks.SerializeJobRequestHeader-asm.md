## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.SerializeJobRequestHeader.WriteFieldsOneByOne()
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L01
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
M00_L00:
       mov       dword ptr [rdx],132
       mov       word ptr [rdx+4],0
       mov       word ptr [rdx+6],0A00
       mov       word ptr [rdx+8],1400
       mov       eax,0A
       ret
M00_L01:
       xor       edx,edx
       jmp       short M00_L00
; Total bytes of code 50
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.SerializeJobRequestHeader.WriteLongThenShort()
       mov       rax,[rcx+8]
       test      rax,rax
       je        short M00_L01
       lea       rdx,[rax+10]
       mov       eax,[rax+8]
M00_L00:
       mov       rax,[7FFA03F84A28]
       or        rax,0A
       movbe     [rdx],rax
       mov       word ptr [rdx+8],1400
       mov       eax,0A
       ret
M00_L01:
       xor       edx,edx
       jmp       short M00_L00
; Total bytes of code 48
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.SerializeJobRequestHeader.WriteArray()
       sub       rsp,28
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L01
       lea       rax,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rax
       mov       edx,0A
       mov       r8d,14
       call      qword ptr [7FFA03FB1708]; Sally7.Benchmarks.SerializeJobRequestHeader.WriteArray(Byte ByRef, Int32, Int32)
       nop
       add       rsp,28
       ret
M00_L01:
       xor       eax,eax
       jmp       short M00_L00
; Total bytes of code 50
```
```assembly
; Sally7.Benchmarks.SerializeJobRequestHeader.WriteArray(Byte ByRef, Int32, Int32)
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+20],rax
       mov       [rsp+28],rax
       mov       rax,63E02C046E5C
       mov       [rsp+30],rax
       lea       rax,[rsp+20]
       mov       word ptr [rax],3201
       mov       word ptr [rax+2],0
       mov       word ptr [rax+4],0
       mov       [rax+6],dx
       mov       [rax+8],r8w
       movzx     edx,word ptr [rax]
       movbe     [rax],dx
       movzx     edx,word ptr [rax+6]
       movbe     [rax+6],dx
       movzx     edx,word ptr [rax+8]
       movbe     [rax+8],dx
       mov       rdx,[rax]
       mov       [rcx],rdx
       mov       rdx,[rax+2]
       mov       [rcx+2],rdx
       mov       eax,0A
       mov       rcx,63E02C046E5C
       cmp       [rsp+30],rcx
       je        short M01_L00
       call      CORINFO_HELP_FAIL_FAST
M01_L00:
       nop
       add       rsp,38
       ret
; Total bytes of code 137
```

## .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
```assembly
; Sally7.Benchmarks.SerializeJobRequestHeader.WriteStruct()
       sub       rsp,28
       mov       rcx,[rcx+8]
       test      rcx,rcx
       je        short M00_L01
       lea       rax,[rcx+10]
       mov       ecx,[rcx+8]
M00_L00:
       mov       rcx,rax
       mov       edx,0A
       mov       r8d,14
       call      qword ptr [7FFA03FA1720]; Sally7.Benchmarks.SerializeJobRequestHeader.WriteStruct(Byte ByRef, Int32, Int32)
       nop
       add       rsp,28
       ret
M00_L01:
       xor       eax,eax
       jmp       short M00_L00
; Total bytes of code 50
```
```assembly
; Sally7.Benchmarks.SerializeJobRequestHeader.WriteStruct(Byte ByRef, Int32, Int32)
       sub       rsp,18
       xor       eax,eax
       mov       [rsp+8],rax
       mov       [rsp+0A],rax
       mov       byte ptr [rsp+8],32
       mov       byte ptr [rsp+9],1
       mov       word ptr [rsp+0A],0
       mov       word ptr [rsp+0C],0
       movzx     eax,dx
       ror       ax,8
       movzx     eax,ax
       mov       [rsp+0E],ax
       movzx     eax,r8w
       ror       ax,8
       movzx     eax,ax
       mov       [rsp+10],ax
       mov       rax,[rsp+8]
       mov       [rcx],rax
       mov       rax,[rsp+0A]
       mov       [rcx+2],rax
       mov       eax,0A
       add       rsp,18
       ret
; Total bytes of code 98
```

