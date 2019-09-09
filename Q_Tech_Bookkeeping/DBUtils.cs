// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.DBUtils
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using System.Data.SqlClient;

namespace Q_Tech_Bookkeeping
{
  internal class DBUtils
  {
    public static SqlConnection GetDBConnection()
    {
      return DBConnection.GetDBConnection("SQL-Server\\QTSQLSERVER,1433", "QTech_Bookkeeping", "User01", "12345");
    }
  }
}
