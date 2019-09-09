// Decompiled with JetBrains decompiler
// Type: Microsoft.Office.Interop.Word._Document
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
  [CompilerGenerated]
  [Guid("0002096B-0000-0000-C000-000000000046")]
  [TypeIdentifier]
  [ComImport]
  public interface _Document
  {
    [DispId(0)]
    [IndexerName("Name")]
    string this[] { [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_159();

    [DispId(1105)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Close([MarshalAs(UnmanagedType.Struct), In, Optional] ref object SaveChanges, [MarshalAs(UnmanagedType.Struct), In, Optional] ref object OriginalFormat, [MarshalAs(UnmanagedType.Struct), In, Optional] ref object RouteDocument);

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap2_266();

    [DispId(568)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void SaveAs2(
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object FileName,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object FileFormat,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object LockComments,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Password,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object AddToRecentFiles,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object WritePassword,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object ReadOnlyRecommended,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object EmbedTrueTypeFonts,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object SaveNativePictureFormat,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object SaveFormsData,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object SaveAsAOCELetter,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Encoding,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object InsertLineBreaks,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object AllowSubstitutions,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object LineEnding,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object AddBiDiMarks,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object CompatibilityMode);
  }
}
