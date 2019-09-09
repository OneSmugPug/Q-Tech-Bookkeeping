// Decompiled with JetBrains decompiler
// Type: Microsoft.Office.Interop.Word.Find
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
  [CompilerGenerated]
  [Guid("000209B0-0000-0000-C000-000000000046")]
  [TypeIdentifier]
  [ComImport]
  public interface Find
  {
    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_32();

    [DispId(25)]
    Replacement Replacement { [DispId(25), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap2_12();

    [DispId(31)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void ClearFormatting();

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap3_2();

    [DispId(444)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    bool Execute(
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object FindText,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchCase,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchWholeWord,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchWildcards,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchSoundsLike,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchAllWordForms,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Forward,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Wrap,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Format,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object ReplaceWith,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Replace,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchKashida,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchDiacritics,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchAlefHamza,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object MatchControl);
  }
}
