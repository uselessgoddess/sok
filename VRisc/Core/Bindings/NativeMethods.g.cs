using VRisc.Core.Entities;

#pragma warning disable CS8500
#pragma warning disable CS8981

namespace VRisc.Core.Bindings;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static unsafe partial class NativeEmulator
{
    const string __DllName = "vrisc";
    
    [DllImport(__DllName, EntryPoint = "vmalloc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern byte* vmalloc(ulong len);

    [DllImport(__DllName, EntryPoint = "vfree", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void vfree(byte* ptr, ulong len);

    [DllImport(__DllName, EntryPoint = "vempty_emu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void* vempty_emu(ulong ram);

    [DllImport(__DllName, EntryPoint = "vfree_emu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void vfree_emu(void* emu);

    [DllImport(__DllName, EntryPoint = "vmap_emu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void vmap_emu(void* emu, EmuRepr repr);

    [DllImport(__DllName, EntryPoint = "vcycle_emu", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern Trap vcycle_emu(void* emu);

    [DllImport(__DllName, EntryPoint = "vcpu_repr", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern CpuRepr vcpu_repr(void* emu);

}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct Slice<T>
{
    public T* ptr;
    public uint len;
    
    public static unsafe Slice<T> FromArray(T[] bytes)
    {
        var length = (uint)bytes.Length;
        var dst = (T*)NativeEmulator.vmalloc((ulong)(length * sizeof(T)));
        fixed (T* src = &bytes[0])
        {
            Unsafe.CopyBlock(dst, src, length);
        }
        return new Slice<T> { ptr = dst, len = length };
    }
    
    public unsafe T[] IntoArray(bool dealloc = true)
    {
        T[] bytes = new T[len];
        fixed (T* dst = &bytes[0])
        {
            Unsafe.CopyBlock(dst, ptr, (uint)(len * sizeof(T)));
        }

        if (dealloc)
        {
            NativeEmulator.vfree((byte*)ptr, (ulong)(len * sizeof(T)));
        }
        
        return bytes;
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct BusRepr
{
    public Slice<byte> dram;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct CpuRepr
{
    public ulong pc;
    public Mode mode;
    public fixed ulong xregs[32];
    public BusRepr bus;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct EmuRepr
{
    public CpuRepr cpu;
}

public enum Mode : uint
{
    User = 0,
    Supervisor = 1,
    Machine = 3,
}

