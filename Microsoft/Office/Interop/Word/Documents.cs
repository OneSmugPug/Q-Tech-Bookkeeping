// Decompiled with JetBrains decompiler
// Type: Microsoft.Office.Interop.Word.Documents
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Word
{
  [CompilerGenerated]
  [Guid("0002096C-0000-0000-C000-000000000046")]
  [TypeIdentifier]
  [ComImport]
  public interface Documents : IEnumerable
  {
    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_15();

    [DispId(19)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    Document Open(
      [MarshalAs(UnmanagedType.Struct), In] ref object FileName,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object ConfirmConversions,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object ReadOnly,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object AddToRecentFiles,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object PasswordDocument,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object PasswordTemplate,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Revert,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object WritePasswordDocument,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object WritePasswordTemplate,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Format,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Encoding,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object Visible,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object OpenAndRepair,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object DocumentDirection,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object NoEncodingDialog,
      [MarshalAs(UnmanagedType.Struct), In, Optional] ref object XMLTransform);
  }
}
