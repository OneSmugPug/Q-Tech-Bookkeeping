﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Office.Interop.Excel._Worksheet
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
  [CompilerGenerated]
  [Guid("000208D8-0000-0000-C000-000000000046")]
  [TypeIdentifier]
  [ComImport]
  public interface _Worksheet
  {
    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_45();

    [DispId(238)]
    Range Cells { [DispId(238), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
