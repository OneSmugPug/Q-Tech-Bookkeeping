// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Properties.Settings
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Q_Tech_Bookkeeping.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    [DefaultSettingValue("Data Source=HEINE-LAPTOP\\SQLEXPRESS;Initial Catalog=\"QTech Bookkeeping\";Integrated Security=True")]
    public string QTech_BookkeepingConnectionString
    {
      get
      {
        return (string) this[nameof (QTech_BookkeepingConnectionString)];
      }
    }

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    [DefaultSettingValue("Data Source=SQL-SERVER\\QTSQLSERVER;Initial Catalog=QTech_Bookkeeping;Persist Security Info=True;User ID=User01;Password=12345")]
    public string QTech_BookkeepingConnectionString1
    {
      get
      {
        return (string) this[nameof (QTech_BookkeepingConnectionString1)];
      }
    }
  }
}
