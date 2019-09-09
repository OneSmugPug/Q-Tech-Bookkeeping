// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Hours_Add
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using Bunifu.Framework.UI;
using Microsoft.Office.Interop.Word;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Hours_Add : Form
  {
    private DataTable dt = new DataTable();
    private Microsoft.Office.Interop.Word.Application app = (Microsoft.Office.Interop.Word.Application) null;
    private object missing = (object) null;
    private string error = "";
    private bool isError = false;
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private object send;
    private int SELECTED_WW;
    private Document doc;
    private Point lastLocation;
    private GroupBox groupBox1;
    private BackgroundWorker backgroundWorker1;
    private ProgressBar pb_CreateRem;
    private GroupBox gb_OA_ODetails;
    private BunifuCustomLabel bunifuCustomLabel15;
    private BunifuCheckbox cb_HA_Paid;
    private Panel panel8;
    private TextBox txt_HA_FTotal;
    private BunifuSeparator ln_HA_FTotal;
    private BunifuCustomLabel bunifuCustomLabel14;
    private Panel panel7;
    private TextBox txt_HA_QTCut;
    private BunifuSeparator ln_HA_QTCut;
    private BunifuCustomLabel bunifuCustomLabel13;
    private BunifuDatepicker dtp_HA_To;
    private BunifuCustomLabel bunifuCustomLabel12;
    private BunifuDatepicker dtp_HA_From;
    private BunifuCustomLabel bunifuCustomLabel11;
    private Panel panel6;
    private TextBox txt_HA_TotAE;
    private BunifuSeparator ln_HA_TotAE;
    private Panel panel5;
    private TextBox txt_HA_ExcRate;
    private BunifuSeparator ln_HA_ExcRate;
    private Panel panel4;
    private TextBox txt_HA_TotBE;
    private BunifuSeparator ln_HA_TotBE;
    private Panel panel3;
    private TextBox txt_HA_DolPH;
    private BunifuSeparator ln_HA_DolPH;
    private Panel panel2;
    private TextBox txt_HA_HW;
    private BunifuSeparator ln_HA_HW;
    private Panel panel1;
    private TextBox txt_HA_Code;
    private BunifuSeparator ln_HA_Code;
    private BunifuCustomLabel bunifuCustomLabel8;
    private BunifuCustomLabel bunifuCustomLabel9;
    private BunifuCustomLabel bunifuCustomLabel7;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_HA_DatePaid;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;
    private Panel panel12;
    private TextBox txt_HA_Surname;
    private BunifuSeparator btn_HA_Surname;
    private BunifuCustomLabel bunifuCustomLabel16;
    private Panel panel11;
    private TextBox txt_HA_EName;
    private BunifuSeparator ln_HA_EName;
    private BunifuCustomLabel label;
    private Panel panel10;
    private TextBox txt_HA_Name;
    private BunifuSeparator ln_HA_Name;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Panel panel9;
    private TextBox txt_HA_CCode;
    private BunifuSeparator ln_HA_CCode;
    private BunifuCustomLabel bunifuCustomLabel3;
    private Button btn_HA_Cancel;
    private Button btn_HA_Done;
    private Button btn_HA_CreateRem;
    private BunifuCustomLabel bunifuCustomLabel17;
    private Button btn_HA_Close;

    public Hours_Add()
    {
      this.InitializeComponent();
    }

    private void Hours_Add_Load(object sender, EventArgs e)
    {
      Contractors curForm = (Contractors) ((Home) this.Owner).getCurForm();
      this.txt_HA_CCode.Text = curForm.getCCode();
      this.txt_HA_Name.Text = curForm.getCName();
      this.txt_HA_Surname.Text = curForm.getCSurname();
      this.txt_HA_EName.Text = curForm.getEName();
      this.send = curForm.getSender();
      this.dt = curForm.getHours();
      if (this.send is Button)
      {
        this.btn_HA_CreateRem.Visible = false;
        this.txt_HA_ExcRate.Text = "0.00000";
        this.txt_HA_ExcRate.SelectionStart = this.txt_HA_ExcRate.Text.Length;
        this.txt_HA_DolPH.Text = "$0.00";
        this.txt_HA_DolPH.SelectionStart = this.txt_HA_DolPH.Text.Length;
        this.txt_HA_TotBE.Text = "$0.00";
        this.txt_HA_TotBE.SelectionStart = this.txt_HA_TotBE.Text.Length;
        this.txt_HA_QTCut.Text = "R0.00";
        this.txt_HA_QTCut.SelectionStart = this.txt_HA_QTCut.Text.Length;
        this.txt_HA_TotAE.Text = "R0.00";
        this.txt_HA_TotAE.SelectionStart = this.txt_HA_TotAE.Text.Length;
        this.txt_HA_FTotal.Text = "R0.00";
        this.txt_HA_FTotal.SelectionStart = this.txt_HA_FTotal.Text.Length;
        this.dtp_HA_From.Value = DateTime.Now;
        this.dtp_HA_To.Value = this.dtp_HA_From.Value.AddDays(6.0);
        this.dtp_HA_DatePaid.Value = DateTime.Now;
        int num1 = 0;
        foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
        {
          if (row.RowState == DataRowState.Deleted)
          {
            string str = row["Code", DataRowVersion.Original].ToString().Trim();
            int num2 = str.IndexOf("_");
            int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
            if (int32 > num1)
              num1 = int32;
          }
          else
          {
            string str = row["Code"].ToString().Trim();
            int num2 = str.IndexOf("_");
            int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
            if (int32 > num1)
              num1 = int32;
          }
        }
        this.txt_HA_Code.Text = this.txt_HA_CCode.Text.Split('_')[1] + "_" + (num1 + 1).ToString("0000");
      }
      else
      {
        this.Text = "Edit Work Week";
        this.SELECTED_WW = curForm.getSelectedHour();
        this.loadHours();
        this.btn_HA_CreateRem.Visible = true;
      }
    }

    private void loadHours()
    {
      this.txt_HA_Code.Text = this.dt.Rows[this.SELECTED_WW]["Code"].ToString().Trim();
      this.dtp_HA_From.Value = !(this.dt.Rows[this.SELECTED_WW]["Date_Start"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(this.dt.Rows[this.SELECTED_WW]["Date_Start"].ToString());
      this.dtp_HA_To.Value = !(this.dt.Rows[this.SELECTED_WW]["Date_End"].ToString() != string.Empty) ? this.dtp_HA_From.Value.AddDays(6.0) : Convert.ToDateTime(this.dt.Rows[this.SELECTED_WW]["Date_End"].ToString());
      this.txt_HA_HW.Text = this.dt.Rows[this.SELECTED_WW]["Hours"].ToString().Trim();
      if (this.dt.Rows[this.SELECTED_WW]["Rate_Per_Hour"].ToString() != string.Empty)
        this.txt_HA_DolPH.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_WW]["Rate_Per_Hour"].ToString().Trim()).ToString("c", (IFormatProvider) CultureInfo.GetCultureInfo("en-US"));
      else
        this.txt_HA_DolPH.Text = "$0.00";
      if (this.dt.Rows[this.SELECTED_WW]["Total_$"].ToString() != string.Empty)
        this.txt_HA_TotBE.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_WW]["Total_$"].ToString().Trim()).ToString("c", (IFormatProvider) CultureInfo.GetCultureInfo("en-US"));
      else
        this.txt_HA_TotBE.Text = "$0.00";
      if (this.dt.Rows[this.SELECTED_WW]["Exchange_Rate"].ToString() != string.Empty)
        this.txt_HA_ExcRate.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_WW]["Exchange_Rate"].ToString().Trim()).ToString();
      else
        this.txt_HA_ExcRate.Text = "0.00000";
      if (this.dt.Rows[this.SELECTED_WW]["Total_R"].ToString() != string.Empty)
        this.txt_HA_TotAE.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_WW]["Total_R"].ToString().Trim()).ToString("c");
      else
        this.txt_HA_TotAE.Text = "R0.00";
      if (this.dt.Rows[this.SELECTED_WW]["QTech_Cut"].ToString() != string.Empty)
        this.txt_HA_QTCut.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_WW]["QTech_Cut"].ToString().Trim()).ToString("c");
      else
        this.txt_HA_QTCut.Text = "R0.00";
      if (this.dt.Rows[this.SELECTED_WW]["Final_Total"].ToString() != string.Empty)
        this.txt_HA_FTotal.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_WW]["Final_Total"].ToString().Trim()).ToString("c");
      else
        this.txt_HA_FTotal.Text = "R0.00";
      if (this.dt.Rows[this.SELECTED_WW]["Remittance"].ToString() == "Yes")
        this.btn_HA_CreateRem.Enabled = false;
      if (!(this.dt.Rows[this.SELECTED_WW]["Paid"].ToString() == "Yes"))
        return;
      this.cb_HA_Paid.Checked = true;
      this.dtp_HA_DatePaid.Enabled = true;
      this.dtp_HA_DatePaid.Value = !(this.dt.Rows[this.SELECTED_WW]["Date_Paid"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(this.dt.Rows[this.SELECTED_WW]["Date_Paid"].ToString());
    }

    private void btn_HA_Done_Click(object sender, EventArgs e)
    {
      string text = this.txt_HA_Code.Text;
      if (this.send is ToolStripButton)
      {
        if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add work week with Code: ").Append(text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          dbConnection.Open();
          try
          {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Contractor_Hours VALUES (@Code, @Date_Start, @Date_End, @Hours, @RPHour, @TotBE, @ERate, @TotAE, @QTCut, @FTot, @Rem, @Inv, @Paid, @DPaid, @CCode)", dbConnection))
            {
              Decimal num1 = !(this.txt_HA_ExcRate.Text == "0.00000") ? Decimal.Parse(this.txt_HA_ExcRate.Text) : new Decimal(0, 0, 0, false, (byte) 5);
              Decimal num2 = !this.txt_HA_DolPH.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_HA_DolPH.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_DolPH.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2));
              Decimal num3 = !(this.txt_HA_TotBE.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_TotBE.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2);
              Decimal num4 = !this.txt_HA_QTCut.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_HA_QTCut.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_QTCut.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
              Decimal num5 = !(this.txt_HA_TotAE.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_TotAE.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2);
              Decimal num6 = !(this.txt_HA_FTotal.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_FTotal.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2);
              Decimal num7 = !(this.txt_HA_HW.Text == string.Empty) ? (!this.txt_HA_HW.Text.Contains(".") ? Decimal.Parse(this.txt_HA_HW.Text) : Decimal.Parse(this.txt_HA_HW.Text.Replace(".", ","))) : new Decimal(0, 0, 0, false, (byte) 2);
              sqlCommand.Parameters.AddWithValue("@Code", (object) this.txt_HA_Code.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Date_Start", (object) this.dtp_HA_From.Value);
              sqlCommand.Parameters.AddWithValue("@Date_End", (object) this.dtp_HA_To.Value);
              sqlCommand.Parameters.AddWithValue("@Hours", (object) num7);
              sqlCommand.Parameters.AddWithValue("@RPHour", (object) num2);
              sqlCommand.Parameters.AddWithValue("@TotBE", (object) num3);
              sqlCommand.Parameters.AddWithValue("@ERate", (object) num1);
              sqlCommand.Parameters.AddWithValue("@TotAE", (object) num5);
              sqlCommand.Parameters.AddWithValue("@QTCut", (object) num4);
              sqlCommand.Parameters.AddWithValue("@FTot", (object) num6);
              sqlCommand.Parameters.AddWithValue("@Rem", (object) "No");
              sqlCommand.Parameters.AddWithValue("@Inv", (object) "No");
              if (this.cb_HA_Paid.Checked)
              {
                sqlCommand.Parameters.AddWithValue("@Paid", (object) "Yes");
                sqlCommand.Parameters.AddWithValue("@DPaid", (object) this.dtp_HA_DatePaid.Value);
              }
              else
              {
                sqlCommand.Parameters.AddWithValue("@Paid", (object) "No");
                sqlCommand.Parameters.AddWithValue("@DPaid", (object) DBNull.Value);
              }
              sqlCommand.Parameters.AddWithValue("@CCode", (object) this.txt_HA_CCode.Text.Trim());
              sqlCommand.ExecuteNonQuery();
              int num8 = (int) MessageBox.Show("New work week successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              this.Close();
            }
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
        }
      }
      else if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to update work week with Code: ").Append(text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.doUpdate();
    }

    private void doUpdate()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        try
        {
          using (SqlCommand sqlCommand = new SqlCommand("UPDATE Contractor_Hours SET Date_Start = @DateS, Date_End = @DateE, Hours = @Hours, Rate_Per_Hour = @RPH, Total_$ = @TotBE, Exchange_Rate = @ER, Total_R = @TotAE, QTech_Cut = @QTC, Final_Total = @FTot, Paid = @P, Date_Paid = @DP WHERE Code = @Code", dbConnection))
          {
            Decimal num1 = !(this.txt_HA_ExcRate.Text == "0,00000") && !(this.txt_HA_ExcRate.Text == "0.00000") ? Decimal.Parse(this.txt_HA_ExcRate.Text.Replace(".", ","), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 5);
            Decimal num2 = !this.txt_HA_DolPH.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_HA_DolPH.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_DolPH.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2));
            Decimal num3 = !(this.txt_HA_TotBE.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_TotBE.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2);
            Decimal num4 = !this.txt_HA_QTCut.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_HA_QTCut.Text.Replace("R", string.Empty) == "0,00") && !(this.txt_HA_QTCut.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_QTCut.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
            Decimal num5 = !(this.txt_HA_TotAE.Text.Replace("R", string.Empty) == "0,00") && !(this.txt_HA_TotAE.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_TotAE.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2);
            Decimal num6 = !(this.txt_HA_FTotal.Text.Replace("R", string.Empty) == "0,00") && !(this.txt_HA_FTotal.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_HA_FTotal.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2);
            Decimal num7 = !(this.txt_HA_HW.Text == string.Empty) ? (!this.txt_HA_HW.Text.Contains(".") ? Decimal.Parse(this.txt_HA_HW.Text) : Decimal.Parse(this.txt_HA_HW.Text.Replace(".", ","))) : new Decimal(0, 0, 0, false, (byte) 2);
            sqlCommand.Parameters.AddWithValue("@Code", (object) this.txt_HA_Code.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@DateS", (object) this.dtp_HA_From.Value);
            sqlCommand.Parameters.AddWithValue("@DateE", (object) this.dtp_HA_To.Value);
            sqlCommand.Parameters.AddWithValue("@Hours", (object) num7);
            sqlCommand.Parameters.AddWithValue("@RPH", (object) num2);
            sqlCommand.Parameters.AddWithValue("@TotBE", (object) num3);
            sqlCommand.Parameters.AddWithValue("@ER", (object) num1);
            sqlCommand.Parameters.AddWithValue("@TotAE", (object) num5);
            sqlCommand.Parameters.AddWithValue("@QTC", (object) num4);
            sqlCommand.Parameters.AddWithValue("@FTot", (object) num6);
            if (this.cb_HA_Paid.Checked)
            {
              sqlCommand.Parameters.AddWithValue("@P", (object) "Yes");
              sqlCommand.Parameters.AddWithValue("@DP", (object) this.dtp_HA_DatePaid.Value);
            }
            else
            {
              sqlCommand.Parameters.AddWithValue("@P", (object) "No");
              sqlCommand.Parameters.AddWithValue("@DP", (object) DBNull.Value);
            }
            sqlCommand.ExecuteNonQuery();
            int num8 = (int) MessageBox.Show("Work week successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void btn_HA_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_HA_PerHour_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_HA_DolPH.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_HA_DolPH.TextChanged -= new EventHandler(this.txt_HA_PerHour_TextChanged);
        this.txt_HA_DolPH.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (object) num);
        this.txt_HA_DolPH.TextChanged += new EventHandler(this.txt_HA_PerHour_TextChanged);
        this.txt_HA_DolPH.Select(this.txt_HA_DolPH.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_HA_DolPH.Text))
        return;
      this.txt_HA_DolPH.Text = "$0.00";
      this.txt_HA_DolPH.Select(this.txt_HA_DolPH.Text.Length, 0);
    }

    private void txt_HA_TotalBE_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_HA_TotBE.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_HA_TotBE.TextChanged -= new EventHandler(this.txt_HA_TotalBE_TextChanged);
        this.txt_HA_TotBE.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (object) num);
        this.txt_HA_TotBE.TextChanged += new EventHandler(this.txt_HA_TotalBE_TextChanged);
        this.txt_HA_TotBE.Select(this.txt_HA_TotBE.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_HA_TotBE.Text))
        return;
      this.txt_HA_TotBE.Text = "$0.00";
      this.txt_HA_TotBE.Select(this.txt_HA_TotBE.Text.Length, 0);
    }

    private void txt_HA_QTCut_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_HA_QTCut.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_HA_QTCut.TextChanged -= new EventHandler(this.txt_HA_QTCut_TextChanged);
        this.txt_HA_QTCut.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
        this.txt_HA_QTCut.TextChanged += new EventHandler(this.txt_HA_QTCut_TextChanged);
        this.txt_HA_QTCut.Select(this.txt_HA_QTCut.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_HA_QTCut.Text))
        return;
      this.txt_HA_QTCut.Text = "R0.00";
      this.txt_HA_QTCut.Select(this.txt_HA_QTCut.Text.Length, 0);
    }

    private void txt_HA_TotalAE_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_HA_TotAE.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_HA_TotAE.TextChanged -= new EventHandler(this.txt_HA_TotalAE_TextChanged);
        this.txt_HA_TotAE.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
        this.txt_HA_TotAE.TextChanged += new EventHandler(this.txt_HA_TotalAE_TextChanged);
        this.txt_HA_TotAE.Select(this.txt_HA_TotAE.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_HA_TotAE.Text))
        return;
      this.txt_HA_TotAE.Text = "R0.00";
      this.txt_HA_TotAE.Select(this.txt_HA_TotAE.Text.Length, 0);
    }

    private void txt_HA_FTotal_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_HA_FTotal.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_HA_FTotal.TextChanged -= new EventHandler(this.txt_HA_FTotal_TextChanged);
        this.txt_HA_FTotal.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
        this.txt_HA_FTotal.TextChanged += new EventHandler(this.txt_HA_FTotal_TextChanged);
        this.txt_HA_FTotal.Select(this.txt_HA_FTotal.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_HA_FTotal.Text))
        return;
      this.txt_HA_FTotal.Text = "R0.00";
      this.txt_HA_FTotal.Select(this.txt_HA_FTotal.Text.Length, 0);
    }

    private bool TextisValid(string text)
    {
      return new Regex("[^0-9]").IsMatch(text);
    }

    private void txt_HA_HW_Leave(object sender, EventArgs e)
    {
      this.ln_HA_HW.LineColor = Color.Gray;
      this.CalculateTotBE();
    }

    private void txt_HA_DolPH_Leave(object sender, EventArgs e)
    {
      this.ln_HA_DolPH.LineColor = Color.Gray;
      this.CalculateTotBE();
    }

    private void txt_HA_ExcRate_Leave(object sender, EventArgs e)
    {
      this.ln_HA_ExcRate.LineColor = Color.Gray;
      this.CalculateTotAE();
    }

    private void txt_HA_QTCut_Leave(object sender, EventArgs e)
    {
      this.ln_HA_TotAE.LineColor = Color.Gray;
      this.CalculateFinalTot();
    }

    private void CalculateTotBE()
    {
      if (!(this.txt_HA_HW.Text != string.Empty))
        return;
      this.txt_HA_TotBE.Text = ((!this.txt_HA_HW.Text.Contains(".") ? Decimal.Parse(this.txt_HA_HW.Text) : Decimal.Parse(this.txt_HA_HW.Text.Replace(".", ","), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA"))) * (!this.txt_HA_DolPH.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : Decimal.Parse(this.txt_HA_DolPH.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")))).ToString("c", (IFormatProvider) CultureInfo.GetCultureInfo("en-US"));
    }

    private void CalculateTotAE()
    {
      Decimal num = !this.txt_HA_ExcRate.Text.Contains(".") ? Decimal.Parse(this.txt_HA_ExcRate.Text) : Decimal.Parse(this.txt_HA_ExcRate.Text.Replace(".", ","), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA"));
      this.txt_HA_TotAE.Text = (Decimal.Parse(this.txt_HA_TotBE.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) * num).ToString("c");
    }

    private void CalculateFinalTot()
    {
      this.txt_HA_FTotal.Text = (Decimal.Parse(this.txt_HA_TotAE.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) - (!this.txt_HA_QTCut.Text.Contains(".") ? (!this.txt_HA_QTCut.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : Decimal.Parse(this.txt_HA_QTCut.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA"))) : (!this.txt_HA_QTCut.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : Decimal.Parse(this.txt_HA_QTCut.Text.Replace("R", string.Empty).Replace(".", ","), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA"))))).ToString("c");
    }

    private void cb_HA_Paid_CheckedChanged(object sender, EventArgs e)
    {
      if (this.cb_HA_Paid.Checked)
        this.dtp_HA_DatePaid.Enabled = true;
      else
        this.dtp_HA_DatePaid.Enabled = false;
    }

    private void btn_HA_CRem_Click(object sender, EventArgs e)
    {
      if (this.backgroundWorker1.IsBusy || MessageBox.Show(new StringBuilder().Append("Are you sure you want to create remittance document for work week: ").Append(this.txt_HA_Code.Text.Trim()).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this.btn_HA_Done.Enabled = false;
      this.btn_HA_Cancel.Enabled = false;
      this.btn_HA_CreateRem.Enabled = false;
      this.pb_CreateRem.Visible = true;
      this.backgroundWorker1.RunWorkerAsync();
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        this.app = (Microsoft.Office.Interop.Word.Application) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
        this.doc = (Document) null;
        this.backgroundWorker1.ReportProgress(10);
        object obj1 = (object) "\\\\192.168.8.121\\Contractors\\Remittances\\Remittance_Template.docx";
        this.missing = System.Type.Missing;
        this.backgroundWorker1.ReportProgress(20);
        // ISSUE: variable of a compiler-generated type
        Documents documents = this.app.Documents;
        object obj2 = obj1;
        ref object local1 = ref obj2;
        object missing1 = this.missing;
        ref object local2 = ref missing1;
        object missing2 = this.missing;
        ref object local3 = ref missing2;
        object missing3 = System.Type.Missing;
        ref object local4 = ref missing3;
        object missing4 = System.Type.Missing;
        ref object local5 = ref missing4;
        object missing5 = System.Type.Missing;
        ref object local6 = ref missing5;
        object missing6 = System.Type.Missing;
        ref object local7 = ref missing6;
        object missing7 = System.Type.Missing;
        ref object local8 = ref missing7;
        object missing8 = System.Type.Missing;
        ref object local9 = ref missing8;
        object missing9 = System.Type.Missing;
        ref object local10 = ref missing9;
        object missing10 = System.Type.Missing;
        ref object local11 = ref missing10;
        object missing11 = System.Type.Missing;
        ref object local12 = ref missing11;
        object missing12 = System.Type.Missing;
        ref object local13 = ref missing12;
        object missing13 = System.Type.Missing;
        ref object local14 = ref missing13;
        object missing14 = System.Type.Missing;
        ref object local15 = ref missing14;
        object missing15 = System.Type.Missing;
        ref object local16 = ref missing15;
        // ISSUE: reference to a compiler-generated method
        this.doc = documents.Open(ref local1, ref local2, ref local3, ref local4, ref local5, ref local6, ref local7, ref local8, ref local9, ref local10, ref local11, ref local12, ref local13, ref local14, ref local15, ref local16);
        // ISSUE: reference to a compiler-generated method
        this.app.Selection.Find.ClearFormatting();
        // ISSUE: reference to a compiler-generated method
        this.app.Selection.Find.Replacement.ClearFormatting();
        this.backgroundWorker1.ReportProgress(30);
        // ISSUE: variable of a compiler-generated type
        Find find1 = this.app.Selection.Find;
        object obj3 = (object) "<code>";
        ref object local17 = ref obj3;
        object missing16 = this.missing;
        ref object local18 = ref missing16;
        object missing17 = this.missing;
        ref object local19 = ref missing17;
        object missing18 = this.missing;
        ref object local20 = ref missing18;
        object missing19 = this.missing;
        ref object local21 = ref missing19;
        object missing20 = this.missing;
        ref object local22 = ref missing20;
        object missing21 = this.missing;
        ref object local23 = ref missing21;
        object missing22 = this.missing;
        ref object local24 = ref missing22;
        object missing23 = this.missing;
        ref object local25 = ref missing23;
        object obj4 = (object) this.txt_HA_Code.Text.Trim();
        ref object local26 = ref obj4;
        object obj5 = (object) 2;
        ref object local27 = ref obj5;
        object missing24 = System.Type.Missing;
        ref object local28 = ref missing24;
        object missing25 = System.Type.Missing;
        ref object local29 = ref missing25;
        object missing26 = System.Type.Missing;
        ref object local30 = ref missing26;
        object missing27 = System.Type.Missing;
        ref object local31 = ref missing27;
        // ISSUE: reference to a compiler-generated method
        find1.Execute(ref local17, ref local18, ref local19, ref local20, ref local21, ref local22, ref local23, ref local24, ref local25, ref local26, ref local27, ref local28, ref local29, ref local30, ref local31);
        // ISSUE: variable of a compiler-generated type
        Find find2 = this.app.Selection.Find;
        object obj6 = (object) "<name>";
        ref object local32 = ref obj6;
        object missing28 = this.missing;
        ref object local33 = ref missing28;
        object missing29 = this.missing;
        ref object local34 = ref missing29;
        object missing30 = this.missing;
        ref object local35 = ref missing30;
        object missing31 = this.missing;
        ref object local36 = ref missing31;
        object missing32 = this.missing;
        ref object local37 = ref missing32;
        object missing33 = this.missing;
        ref object local38 = ref missing33;
        object missing34 = this.missing;
        ref object local39 = ref missing34;
        object missing35 = this.missing;
        ref object local40 = ref missing35;
        object obj7 = (object) this.txt_HA_Name.Text.Trim();
        ref object local41 = ref obj7;
        object obj8 = (object) 2;
        ref object local42 = ref obj8;
        object missing36 = System.Type.Missing;
        ref object local43 = ref missing36;
        object missing37 = System.Type.Missing;
        ref object local44 = ref missing37;
        object missing38 = System.Type.Missing;
        ref object local45 = ref missing38;
        object missing39 = System.Type.Missing;
        ref object local46 = ref missing39;
        // ISSUE: reference to a compiler-generated method
        find2.Execute(ref local32, ref local33, ref local34, ref local35, ref local36, ref local37, ref local38, ref local39, ref local40, ref local41, ref local42, ref local43, ref local44, ref local45, ref local46);
        // ISSUE: variable of a compiler-generated type
        Find find3 = this.app.Selection.Find;
        object obj9 = (object) "<surname>";
        ref object local47 = ref obj9;
        object missing40 = this.missing;
        ref object local48 = ref missing40;
        object missing41 = this.missing;
        ref object local49 = ref missing41;
        object missing42 = this.missing;
        ref object local50 = ref missing42;
        object missing43 = this.missing;
        ref object local51 = ref missing43;
        object missing44 = this.missing;
        ref object local52 = ref missing44;
        object missing45 = this.missing;
        ref object local53 = ref missing45;
        object missing46 = this.missing;
        ref object local54 = ref missing46;
        object missing47 = this.missing;
        ref object local55 = ref missing47;
        object obj10 = (object) this.txt_HA_Surname.Text.Trim();
        ref object local56 = ref obj10;
        object obj11 = (object) 2;
        ref object local57 = ref obj11;
        object missing48 = System.Type.Missing;
        ref object local58 = ref missing48;
        object missing49 = System.Type.Missing;
        ref object local59 = ref missing49;
        object missing50 = System.Type.Missing;
        ref object local60 = ref missing50;
        object missing51 = System.Type.Missing;
        ref object local61 = ref missing51;
        // ISSUE: reference to a compiler-generated method
        find3.Execute(ref local47, ref local48, ref local49, ref local50, ref local51, ref local52, ref local53, ref local54, ref local55, ref local56, ref local57, ref local58, ref local59, ref local60, ref local61);
        this.backgroundWorker1.ReportProgress(40);
        // ISSUE: variable of a compiler-generated type
        Find find4 = this.app.Selection.Find;
        object obj12 = (object) "<date>";
        ref object local62 = ref obj12;
        object missing52 = this.missing;
        ref object local63 = ref missing52;
        object missing53 = this.missing;
        ref object local64 = ref missing53;
        object missing54 = this.missing;
        ref object local65 = ref missing54;
        object missing55 = this.missing;
        ref object local66 = ref missing55;
        object missing56 = this.missing;
        ref object local67 = ref missing56;
        object missing57 = this.missing;
        ref object local68 = ref missing57;
        object missing58 = this.missing;
        ref object local69 = ref missing58;
        object missing59 = this.missing;
        ref object local70 = ref missing59;
        object shortDateString = (object) DateTime.Now.ToShortDateString();
        ref object local71 = ref shortDateString;
        object obj13 = (object) 2;
        ref object local72 = ref obj13;
        object missing60 = System.Type.Missing;
        ref object local73 = ref missing60;
        object missing61 = System.Type.Missing;
        ref object local74 = ref missing61;
        object missing62 = System.Type.Missing;
        ref object local75 = ref missing62;
        object missing63 = System.Type.Missing;
        ref object local76 = ref missing63;
        // ISSUE: reference to a compiler-generated method
        find4.Execute(ref local62, ref local63, ref local64, ref local65, ref local66, ref local67, ref local68, ref local69, ref local70, ref local71, ref local72, ref local73, ref local74, ref local75, ref local76);
        // ISSUE: variable of a compiler-generated type
        Find find5 = this.app.Selection.Find;
        object obj14 = (object) "<desc>";
        ref object local77 = ref obj14;
        object missing64 = this.missing;
        ref object local78 = ref missing64;
        object missing65 = this.missing;
        ref object local79 = ref missing65;
        object missing66 = this.missing;
        ref object local80 = ref missing66;
        object missing67 = this.missing;
        ref object local81 = ref missing67;
        object missing68 = this.missing;
        ref object local82 = ref missing68;
        object missing69 = this.missing;
        ref object local83 = ref missing69;
        object missing70 = this.missing;
        ref object local84 = ref missing70;
        object missing71 = this.missing;
        ref object local85 = ref missing71;
        object obj15 = (object) ("Week ending " + this.dtp_HA_To.Value.ToLongDateString());
        ref object local86 = ref obj15;
        object obj16 = (object) 2;
        ref object local87 = ref obj16;
        object missing72 = System.Type.Missing;
        ref object local88 = ref missing72;
        object missing73 = System.Type.Missing;
        ref object local89 = ref missing73;
        object missing74 = System.Type.Missing;
        ref object local90 = ref missing74;
        object missing75 = System.Type.Missing;
        ref object local91 = ref missing75;
        // ISSUE: reference to a compiler-generated method
        find5.Execute(ref local77, ref local78, ref local79, ref local80, ref local81, ref local82, ref local83, ref local84, ref local85, ref local86, ref local87, ref local88, ref local89, ref local90, ref local91);
        // ISSUE: variable of a compiler-generated type
        Find find6 = this.app.Selection.Find;
        object obj17 = (object) "<dolvalue>";
        ref object local92 = ref obj17;
        object missing76 = this.missing;
        ref object local93 = ref missing76;
        object missing77 = this.missing;
        ref object local94 = ref missing77;
        object missing78 = this.missing;
        ref object local95 = ref missing78;
        object missing79 = this.missing;
        ref object local96 = ref missing79;
        object missing80 = this.missing;
        ref object local97 = ref missing80;
        object missing81 = this.missing;
        ref object local98 = ref missing81;
        object missing82 = this.missing;
        ref object local99 = ref missing82;
        object missing83 = this.missing;
        ref object local100 = ref missing83;
        object obj18 = (object) this.txt_HA_TotBE.Text.Trim();
        ref object local101 = ref obj18;
        object obj19 = (object) 2;
        ref object local102 = ref obj19;
        object missing84 = System.Type.Missing;
        ref object local103 = ref missing84;
        object missing85 = System.Type.Missing;
        ref object local104 = ref missing85;
        object missing86 = System.Type.Missing;
        ref object local105 = ref missing86;
        object missing87 = System.Type.Missing;
        ref object local106 = ref missing87;
        // ISSUE: reference to a compiler-generated method
        find6.Execute(ref local92, ref local93, ref local94, ref local95, ref local96, ref local97, ref local98, ref local99, ref local100, ref local101, ref local102, ref local103, ref local104, ref local105, ref local106);
        // ISSUE: variable of a compiler-generated type
        Find find7 = this.app.Selection.Find;
        object obj20 = (object) "<excrate>";
        ref object local107 = ref obj20;
        object missing88 = this.missing;
        ref object local108 = ref missing88;
        object missing89 = this.missing;
        ref object local109 = ref missing89;
        object missing90 = this.missing;
        ref object local110 = ref missing90;
        object missing91 = this.missing;
        ref object local111 = ref missing91;
        object missing92 = this.missing;
        ref object local112 = ref missing92;
        object missing93 = this.missing;
        ref object local113 = ref missing93;
        object missing94 = this.missing;
        ref object local114 = ref missing94;
        object missing95 = this.missing;
        ref object local115 = ref missing95;
        object obj21 = (object) this.txt_HA_ExcRate.Text.Trim();
        ref object local116 = ref obj21;
        object obj22 = (object) 2;
        ref object local117 = ref obj22;
        object missing96 = System.Type.Missing;
        ref object local118 = ref missing96;
        object missing97 = System.Type.Missing;
        ref object local119 = ref missing97;
        object missing98 = System.Type.Missing;
        ref object local120 = ref missing98;
        object missing99 = System.Type.Missing;
        ref object local121 = ref missing99;
        // ISSUE: reference to a compiler-generated method
        find7.Execute(ref local107, ref local108, ref local109, ref local110, ref local111, ref local112, ref local113, ref local114, ref local115, ref local116, ref local117, ref local118, ref local119, ref local120, ref local121);
        this.backgroundWorker1.ReportProgress(50);
        // ISSUE: variable of a compiler-generated type
        Find find8 = this.app.Selection.Find;
        object obj23 = (object) "<total>";
        ref object local122 = ref obj23;
        object missing100 = this.missing;
        ref object local123 = ref missing100;
        object missing101 = this.missing;
        ref object local124 = ref missing101;
        object missing102 = this.missing;
        ref object local125 = ref missing102;
        object missing103 = this.missing;
        ref object local126 = ref missing103;
        object missing104 = this.missing;
        ref object local127 = ref missing104;
        object missing105 = this.missing;
        ref object local128 = ref missing105;
        object missing106 = this.missing;
        ref object local129 = ref missing106;
        object missing107 = this.missing;
        ref object local130 = ref missing107;
        object obj24 = (object) this.txt_HA_TotAE.Text.Trim();
        ref object local131 = ref obj24;
        object obj25 = (object) 2;
        ref object local132 = ref obj25;
        object missing108 = System.Type.Missing;
        ref object local133 = ref missing108;
        object missing109 = System.Type.Missing;
        ref object local134 = ref missing109;
        object missing110 = System.Type.Missing;
        ref object local135 = ref missing110;
        object missing111 = System.Type.Missing;
        ref object local136 = ref missing111;
        // ISSUE: reference to a compiler-generated method
        find8.Execute(ref local122, ref local123, ref local124, ref local125, ref local126, ref local127, ref local128, ref local129, ref local130, ref local131, ref local132, ref local133, ref local134, ref local135, ref local136);
        // ISSUE: variable of a compiler-generated type
        Find find9 = this.app.Selection.Find;
        object obj26 = (object) "<subtotal>";
        ref object local137 = ref obj26;
        object missing112 = this.missing;
        ref object local138 = ref missing112;
        object missing113 = this.missing;
        ref object local139 = ref missing113;
        object missing114 = this.missing;
        ref object local140 = ref missing114;
        object missing115 = this.missing;
        ref object local141 = ref missing115;
        object missing116 = this.missing;
        ref object local142 = ref missing116;
        object missing117 = this.missing;
        ref object local143 = ref missing117;
        object missing118 = this.missing;
        ref object local144 = ref missing118;
        object missing119 = this.missing;
        ref object local145 = ref missing119;
        object obj27 = (object) this.txt_HA_TotAE.Text.Trim();
        ref object local146 = ref obj27;
        object obj28 = (object) 2;
        ref object local147 = ref obj28;
        object missing120 = System.Type.Missing;
        ref object local148 = ref missing120;
        object missing121 = System.Type.Missing;
        ref object local149 = ref missing121;
        object missing122 = System.Type.Missing;
        ref object local150 = ref missing122;
        object missing123 = System.Type.Missing;
        ref object local151 = ref missing123;
        // ISSUE: reference to a compiler-generated method
        find9.Execute(ref local137, ref local138, ref local139, ref local140, ref local141, ref local142, ref local143, ref local144, ref local145, ref local146, ref local147, ref local148, ref local149, ref local150, ref local151);
        // ISSUE: variable of a compiler-generated type
        Find find10 = this.app.Selection.Find;
        object obj29 = (object) "<grandtotal>";
        ref object local152 = ref obj29;
        object missing124 = this.missing;
        ref object local153 = ref missing124;
        object missing125 = this.missing;
        ref object local154 = ref missing125;
        object missing126 = this.missing;
        ref object local155 = ref missing126;
        object missing127 = this.missing;
        ref object local156 = ref missing127;
        object missing128 = this.missing;
        ref object local157 = ref missing128;
        object missing129 = this.missing;
        ref object local158 = ref missing129;
        object missing130 = this.missing;
        ref object local159 = ref missing130;
        object missing131 = this.missing;
        ref object local160 = ref missing131;
        object obj30 = (object) this.txt_HA_TotAE.Text.Trim();
        ref object local161 = ref obj30;
        object obj31 = (object) 2;
        ref object local162 = ref obj31;
        object missing132 = System.Type.Missing;
        ref object local163 = ref missing132;
        object missing133 = System.Type.Missing;
        ref object local164 = ref missing133;
        object missing134 = System.Type.Missing;
        ref object local165 = ref missing134;
        object missing135 = System.Type.Missing;
        ref object local166 = ref missing135;
        // ISSUE: reference to a compiler-generated method
        find10.Execute(ref local152, ref local153, ref local154, ref local155, ref local156, ref local157, ref local158, ref local159, ref local160, ref local161, ref local162, ref local163, ref local164, ref local165, ref local166);
        this.backgroundWorker1.ReportProgress(60);
        object obj32 = (object) ("\\\\192.168.8.121\\Contractors\\Remittances\\Remittance_" + this.txt_HA_Code.Text.Trim() + ".docx");
        // ISSUE: variable of a compiler-generated type
        Document doc = this.doc;
        object obj33 = obj32;
        ref object local167 = ref obj33;
        object missing136 = this.missing;
        ref object local168 = ref missing136;
        object missing137 = this.missing;
        ref object local169 = ref missing137;
        object missing138 = this.missing;
        ref object local170 = ref missing138;
        object missing139 = System.Type.Missing;
        ref object local171 = ref missing139;
        object missing140 = System.Type.Missing;
        ref object local172 = ref missing140;
        object missing141 = System.Type.Missing;
        ref object local173 = ref missing141;
        object missing142 = System.Type.Missing;
        ref object local174 = ref missing142;
        object missing143 = System.Type.Missing;
        ref object local175 = ref missing143;
        object missing144 = System.Type.Missing;
        ref object local176 = ref missing144;
        object missing145 = System.Type.Missing;
        ref object local177 = ref missing145;
        object missing146 = System.Type.Missing;
        ref object local178 = ref missing146;
        object missing147 = System.Type.Missing;
        ref object local179 = ref missing147;
        object missing148 = System.Type.Missing;
        ref object local180 = ref missing148;
        object missing149 = System.Type.Missing;
        ref object local181 = ref missing149;
        object missing150 = System.Type.Missing;
        ref object local182 = ref missing150;
        object missing151 = System.Type.Missing;
        ref object local183 = ref missing151;
        // ISSUE: reference to a compiler-generated method
        doc.SaveAs2(ref local167, ref local168, ref local169, ref local170, ref local171, ref local172, ref local173, ref local174, ref local175, ref local176, ref local177, ref local178, ref local179, ref local180, ref local181, ref local182, ref local183);
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          this.backgroundWorker1.ReportProgress(70);
          dbConnection.Open();
          string cmdText = "UPDATE Contractor_Hours SET Remittance=@Rem WHERE Code=@Code AND Contractor_Code=@CCode";
          this.backgroundWorker1.ReportProgress(80);
          using (SqlCommand sqlCommand = new SqlCommand(cmdText, dbConnection))
          {
            sqlCommand.Parameters.AddWithValue("@Rem", (object) "Yes");
            sqlCommand.Parameters.AddWithValue("@Code", (object) this.txt_HA_Code.Text.Trim());
            this.backgroundWorker1.ReportProgress(90);
            sqlCommand.Parameters.AddWithValue("@CCode", (object) this.txt_HA_CCode.Text.Trim());
            sqlCommand.ExecuteNonQuery();
          }
        }
        this.backgroundWorker1.ReportProgress(100);
      }
      catch (Exception ex)
      {
        this.error = ex.Message;
        this.isError = true;
      }
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.pb_CreateRem.Value = e.ProgressPercentage;
      this.pb_CreateRem.Update();
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!this.isError)
      {
        int num1 = (int) MessageBox.Show("Remittance document successfully created", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        int num2 = (int) MessageBox.Show(this.error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      // ISSUE: variable of a compiler-generated type
      Document doc = this.doc;
      object obj1 = (object) false;
      ref object local1 = ref obj1;
      object missing1 = this.missing;
      ref object local2 = ref missing1;
      object missing2 = this.missing;
      ref object local3 = ref missing2;
      // ISSUE: reference to a compiler-generated method
      doc.Close(ref local1, ref local2, ref local3);
      // ISSUE: variable of a compiler-generated type
      Microsoft.Office.Interop.Word.Application app = this.app;
      object obj2 = (object) false;
      ref object local4 = ref obj2;
      object obj3 = (object) false;
      ref object local5 = ref obj3;
      obj1 = (object) false;
      ref object local6 = ref obj1;
      // ISSUE: reference to a compiler-generated method
      app.Quit(ref local4, ref local5, ref local6);
      Marshal.ReleaseComObject((object) this.app);
      this.doUpdate();
    }

    private void txt_HA_Code_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_Code.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_Code_Leave(object sender, EventArgs e)
    {
      this.ln_HA_Code.LineColor = Color.Gray;
    }

    private void txt_HA_Code_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_Code.Focused)
        return;
      this.ln_HA_Code.LineColor = Color.Gray;
    }

    private void txt_HA_HW_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_HW.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_HW_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_HW.Focused)
        return;
      this.ln_HA_HW.LineColor = Color.Gray;
    }

    private void txt_HA_DolPH_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_DolPH.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_DolPH_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_DolPH.Focused)
        return;
      this.ln_HA_DolPH.LineColor = Color.Gray;
    }

    private void txt_HA_TotBE_Leave(object sender, EventArgs e)
    {
      this.ln_HA_TotBE.LineColor = Color.Gray;
    }

    private void txt_HA_TotBE_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_TotBE.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_TotBE_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_ExcRate.Focused)
        return;
      this.ln_HA_ExcRate.LineColor = Color.Gray;
    }

    private void txt_HA_ExcRate_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_ExcRate.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_ExcRate_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_ExcRate.Focused)
        return;
      this.ln_HA_ExcRate.LineColor = Color.Gray;
    }

    private void txt_HA_TotAE_Leave(object sender, EventArgs e)
    {
      this.ln_HA_TotAE.LineColor = Color.Gray;
    }

    private void txt_HA_TotAE_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_TotAE.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_TotAE_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_TotAE.Focused)
        return;
      this.ln_HA_TotAE.LineColor = Color.Gray;
    }

    private void txt_HA_QTCut_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_QTCut.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_QTCut_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_QTCut.Focused)
        return;
      this.ln_HA_QTCut.LineColor = Color.Gray;
    }

    private void txt_HA_FTotal_Leave(object sender, EventArgs e)
    {
      this.ln_HA_FTotal.LineColor = Color.Gray;
    }

    private void txt_HA_FTotal_MouseEnter(object sender, EventArgs e)
    {
      this.ln_HA_FTotal.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_HA_FTotal_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_HA_FTotal.Focused)
        return;
      this.ln_HA_FTotal.LineColor = Color.Gray;
    }

    private void btn_HA_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_HA_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_HA_Close.Image = (Image) Resources.close_white;
    }

    private void btn_HA_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_HA_Close.Image = (Image) Resources.close_black;
    }

    private void btn_HA_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_HA_Done.ForeColor = Color.White;
    }

    private void btn_HA_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_HA_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_HA_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_HA_Cancel.ForeColor = Color.White;
    }

    private void btn_HA_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_HA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_HA_CreateRem_MouseEnter(object sender, EventArgs e)
    {
      this.btn_HA_CreateRem.ForeColor = Color.White;
    }

    private void btn_HA_CreateRem_MouseLeave(object sender, EventArgs e)
    {
      this.btn_HA_CreateRem.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void O_Add_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void O_Add_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.mouseDown)
        return;
      Point location = this.Location;
      int x = location.X - this.lastLocation.X + e.X;
      location = this.Location;
      int y = location.Y - this.lastLocation.Y + e.Y;
      this.Location = new Point(x, y);
      this.Update();
    }

    private void O_Add_MouseUp(object sender, MouseEventArgs e)
    {
      this.mouseDown = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Hours_Add));
      this.groupBox1 = new GroupBox();
      this.panel12 = new Panel();
      this.txt_HA_Surname = new TextBox();
      this.btn_HA_Surname = new BunifuSeparator();
      this.bunifuCustomLabel16 = new BunifuCustomLabel();
      this.panel11 = new Panel();
      this.txt_HA_EName = new TextBox();
      this.ln_HA_EName = new BunifuSeparator();
      this.label = new BunifuCustomLabel();
      this.panel10 = new Panel();
      this.txt_HA_Name = new TextBox();
      this.ln_HA_Name = new BunifuSeparator();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.panel9 = new Panel();
      this.txt_HA_CCode = new TextBox();
      this.ln_HA_CCode = new BunifuSeparator();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.backgroundWorker1 = new BackgroundWorker();
      this.pb_CreateRem = new ProgressBar();
      this.gb_OA_ODetails = new GroupBox();
      this.bunifuCustomLabel15 = new BunifuCustomLabel();
      this.cb_HA_Paid = new BunifuCheckbox();
      this.panel8 = new Panel();
      this.txt_HA_FTotal = new TextBox();
      this.ln_HA_FTotal = new BunifuSeparator();
      this.bunifuCustomLabel14 = new BunifuCustomLabel();
      this.panel7 = new Panel();
      this.txt_HA_QTCut = new TextBox();
      this.ln_HA_QTCut = new BunifuSeparator();
      this.bunifuCustomLabel13 = new BunifuCustomLabel();
      this.dtp_HA_To = new BunifuDatepicker();
      this.bunifuCustomLabel12 = new BunifuCustomLabel();
      this.dtp_HA_From = new BunifuDatepicker();
      this.bunifuCustomLabel11 = new BunifuCustomLabel();
      this.panel6 = new Panel();
      this.txt_HA_TotAE = new TextBox();
      this.ln_HA_TotAE = new BunifuSeparator();
      this.panel5 = new Panel();
      this.txt_HA_ExcRate = new TextBox();
      this.ln_HA_ExcRate = new BunifuSeparator();
      this.panel4 = new Panel();
      this.txt_HA_TotBE = new TextBox();
      this.ln_HA_TotBE = new BunifuSeparator();
      this.panel3 = new Panel();
      this.txt_HA_DolPH = new TextBox();
      this.ln_HA_DolPH = new BunifuSeparator();
      this.panel2 = new Panel();
      this.txt_HA_HW = new TextBox();
      this.ln_HA_HW = new BunifuSeparator();
      this.panel1 = new Panel();
      this.txt_HA_Code = new TextBox();
      this.ln_HA_Code = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.bunifuCustomLabel9 = new BunifuCustomLabel();
      this.bunifuCustomLabel7 = new BunifuCustomLabel();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_HA_DatePaid = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.btn_HA_Cancel = new Button();
      this.btn_HA_Done = new Button();
      this.btn_HA_CreateRem = new Button();
      this.bunifuCustomLabel17 = new BunifuCustomLabel();
      this.btn_HA_Close = new Button();
      this.groupBox1.SuspendLayout();
      this.panel12.SuspendLayout();
      this.panel11.SuspendLayout();
      this.panel10.SuspendLayout();
      this.panel9.SuspendLayout();
      this.gb_OA_ODetails.SuspendLayout();
      this.panel8.SuspendLayout();
      this.panel7.SuspendLayout();
      this.panel6.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.groupBox1.Controls.Add((Control) this.panel12);
      this.groupBox1.Controls.Add((Control) this.bunifuCustomLabel16);
      this.groupBox1.Controls.Add((Control) this.panel11);
      this.groupBox1.Controls.Add((Control) this.label);
      this.groupBox1.Controls.Add((Control) this.panel10);
      this.groupBox1.Controls.Add((Control) this.bunifuCustomLabel4);
      this.groupBox1.Controls.Add((Control) this.panel9);
      this.groupBox1.Controls.Add((Control) this.bunifuCustomLabel3);
      this.groupBox1.ForeColor = Color.FromArgb(15, 91, 142);
      this.groupBox1.Location = new Point(12, 50);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(580, 104);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Contractor";
      this.panel12.Controls.Add((Control) this.txt_HA_Surname);
      this.panel12.Controls.Add((Control) this.btn_HA_Surname);
      this.panel12.Location = new Point(352, 62);
      this.panel12.Name = "panel12";
      this.panel12.Size = new Size(211, 27);
      this.panel12.TabIndex = 60;
      this.txt_HA_Surname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_Surname.BackColor = Color.Silver;
      this.txt_HA_Surname.BorderStyle = BorderStyle.None;
      this.txt_HA_Surname.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_Surname.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_Surname.Location = new Point(2, 6);
      this.txt_HA_Surname.Name = "txt_HA_Surname";
      this.txt_HA_Surname.ReadOnly = true;
      this.txt_HA_Surname.Size = new Size(209, 16);
      this.txt_HA_Surname.TabIndex = 0;
      this.txt_HA_Surname.TabStop = false;
      this.btn_HA_Surname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_HA_Surname.BackColor = Color.Transparent;
      this.btn_HA_Surname.LineColor = Color.FromArgb(128, 128, 128);
      this.btn_HA_Surname.LineThickness = 1;
      this.btn_HA_Surname.Location = new Point(-1, 18);
      this.btn_HA_Surname.Name = "btn_HA_Surname";
      this.btn_HA_Surname.Size = new Size(213, 10);
      this.btn_HA_Surname.TabIndex = 52;
      this.btn_HA_Surname.TabStop = false;
      this.btn_HA_Surname.Transparency = (int) byte.MaxValue;
      this.btn_HA_Surname.Vertical = false;
      this.bunifuCustomLabel16.AutoSize = true;
      this.bunifuCustomLabel16.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel16.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel16.Location = new Point(283, 65);
      this.bunifuCustomLabel16.Name = "bunifuCustomLabel16";
      this.bunifuCustomLabel16.Size = new Size(65, 17);
      this.bunifuCustomLabel16.TabIndex = 59;
      this.bunifuCustomLabel16.Text = "Surname";
      this.panel11.Controls.Add((Control) this.txt_HA_EName);
      this.panel11.Controls.Add((Control) this.ln_HA_EName);
      this.panel11.Location = new Point(348, 22);
      this.panel11.Name = "panel11";
      this.panel11.Size = new Size(216, 27);
      this.panel11.TabIndex = 58;
      this.txt_HA_EName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_EName.BackColor = Color.Silver;
      this.txt_HA_EName.BorderStyle = BorderStyle.None;
      this.txt_HA_EName.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_EName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_EName.Location = new Point(2, 6);
      this.txt_HA_EName.Name = "txt_HA_EName";
      this.txt_HA_EName.ReadOnly = true;
      this.txt_HA_EName.Size = new Size(214, 16);
      this.txt_HA_EName.TabIndex = 0;
      this.txt_HA_EName.TabStop = false;
      this.ln_HA_EName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_EName.BackColor = Color.Transparent;
      this.ln_HA_EName.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_EName.LineThickness = 1;
      this.ln_HA_EName.Location = new Point(-1, 18);
      this.ln_HA_EName.Name = "ln_HA_EName";
      this.ln_HA_EName.Size = new Size(218, 10);
      this.ln_HA_EName.TabIndex = 52;
      this.ln_HA_EName.TabStop = false;
      this.ln_HA_EName.Transparency = (int) byte.MaxValue;
      this.ln_HA_EName.Vertical = false;
      this.label.AutoSize = true;
      this.label.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label.ForeColor = Color.FromArgb(64, 64, 64);
      this.label.Location = new Point(233, 25);
      this.label.Name = "label";
      this.label.Size = new Size(112, 17);
      this.label.TabIndex = 57;
      this.label.Text = "Employer Name:";
      this.panel10.Controls.Add((Control) this.txt_HA_Name);
      this.panel10.Controls.Add((Control) this.ln_HA_Name);
      this.panel10.Location = new Point(64, 62);
      this.panel10.Name = "panel10";
      this.panel10.Size = new Size(173, 27);
      this.panel10.TabIndex = 56;
      this.txt_HA_Name.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_Name.BackColor = Color.Silver;
      this.txt_HA_Name.BorderStyle = BorderStyle.None;
      this.txt_HA_Name.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_Name.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_Name.Location = new Point(2, 6);
      this.txt_HA_Name.Name = "txt_HA_Name";
      this.txt_HA_Name.ReadOnly = true;
      this.txt_HA_Name.Size = new Size(171, 16);
      this.txt_HA_Name.TabIndex = 0;
      this.txt_HA_Name.TabStop = false;
      this.ln_HA_Name.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_Name.BackColor = Color.Transparent;
      this.ln_HA_Name.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_Name.LineThickness = 1;
      this.ln_HA_Name.Location = new Point(-1, 18);
      this.ln_HA_Name.Name = "ln_HA_Name";
      this.ln_HA_Name.Size = new Size(175, 10);
      this.ln_HA_Name.TabIndex = 52;
      this.ln_HA_Name.TabStop = false;
      this.ln_HA_Name.Transparency = (int) byte.MaxValue;
      this.ln_HA_Name.Vertical = false;
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(10, 65);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(49, 17);
      this.bunifuCustomLabel4.TabIndex = 55;
      this.bunifuCustomLabel4.Text = "Name:";
      this.panel9.Controls.Add((Control) this.txt_HA_CCode);
      this.panel9.Controls.Add((Control) this.ln_HA_CCode);
      this.panel9.Location = new Point(64, 22);
      this.panel9.Name = "panel9";
      this.panel9.Size = new Size(117, 27);
      this.panel9.TabIndex = 54;
      this.txt_HA_CCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_CCode.BackColor = Color.Silver;
      this.txt_HA_CCode.BorderStyle = BorderStyle.None;
      this.txt_HA_CCode.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_CCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_CCode.Location = new Point(2, 6);
      this.txt_HA_CCode.Name = "txt_HA_CCode";
      this.txt_HA_CCode.ReadOnly = true;
      this.txt_HA_CCode.Size = new Size(115, 16);
      this.txt_HA_CCode.TabIndex = 0;
      this.txt_HA_CCode.TabStop = false;
      this.ln_HA_CCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_CCode.BackColor = Color.Transparent;
      this.ln_HA_CCode.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_CCode.LineThickness = 1;
      this.ln_HA_CCode.Location = new Point(-1, 18);
      this.ln_HA_CCode.Name = "ln_HA_CCode";
      this.ln_HA_CCode.Size = new Size(119, 10);
      this.ln_HA_CCode.TabIndex = 52;
      this.ln_HA_CCode.TabStop = false;
      this.ln_HA_CCode.Transparency = (int) byte.MaxValue;
      this.ln_HA_CCode.Vertical = false;
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point(13, 25);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(45, 17);
      this.bunifuCustomLabel3.TabIndex = 53;
      this.bunifuCustomLabel3.Text = "Code:";
      this.backgroundWorker1.WorkerReportsProgress = true;
      this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
      this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
      this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      this.pb_CreateRem.Location = new Point(184, 473);
      this.pb_CreateRem.Name = "pb_CreateRem";
      this.pb_CreateRem.Size = new Size(174, 23);
      this.pb_CreateRem.Style = ProgressBarStyle.Continuous;
      this.pb_CreateRem.TabIndex = 0;
      this.pb_CreateRem.Visible = false;
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel15);
      this.gb_OA_ODetails.Controls.Add((Control) this.cb_HA_Paid);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel8);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel14);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel7);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel13);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_HA_To);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel12);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_HA_From);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel11);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel4);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel3);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel1);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel8);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel9);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel7);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_HA_DatePaid);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(12, 179);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(580, 278);
      this.gb_OA_ODetails.TabIndex = 54;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Work Week";
      this.bunifuCustomLabel15.AutoSize = true;
      this.bunifuCustomLabel15.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel15.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel15.Location = new Point(146, 233);
      this.bunifuCustomLabel15.Name = "bunifuCustomLabel15";
      this.bunifuCustomLabel15.Size = new Size(36, 17);
      this.bunifuCustomLabel15.TabIndex = 74;
      this.bunifuCustomLabel15.Text = "Paid";
      this.cb_HA_Paid.BackColor = Color.FromArgb(132, 135, 140);
      this.cb_HA_Paid.ChechedOffColor = Color.FromArgb(132, 135, 140);
      this.cb_HA_Paid.Checked = false;
      this.cb_HA_Paid.CheckedOnColor = Color.FromArgb(15, 91, 142);
      this.cb_HA_Paid.ForeColor = Color.White;
      this.cb_HA_Paid.Location = new Point(120, 233);
      this.cb_HA_Paid.Name = "cb_HA_Paid";
      this.cb_HA_Paid.Size = new Size(20, 20);
      this.cb_HA_Paid.TabIndex = 9;
      this.cb_HA_Paid.OnChange += new EventHandler(this.cb_HA_Paid_CheckedChanged);
      this.panel8.Controls.Add((Control) this.txt_HA_FTotal);
      this.panel8.Controls.Add((Control) this.ln_HA_FTotal);
      this.panel8.Location = new Point(421, 186);
      this.panel8.Name = "panel8";
      this.panel8.Size = new Size(143, 26);
      this.panel8.TabIndex = 72;
      this.txt_HA_FTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_FTotal.BackColor = Color.Silver;
      this.txt_HA_FTotal.BorderStyle = BorderStyle.None;
      this.txt_HA_FTotal.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_FTotal.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_FTotal.Location = new Point(2, 6);
      this.txt_HA_FTotal.Name = "txt_HA_FTotal";
      this.txt_HA_FTotal.ReadOnly = true;
      this.txt_HA_FTotal.Size = new Size(140, 16);
      this.txt_HA_FTotal.TabIndex = 8;
      this.txt_HA_FTotal.TextChanged += new EventHandler(this.txt_HA_FTotal_TextChanged);
      this.txt_HA_FTotal.Leave += new EventHandler(this.txt_HA_FTotal_Leave);
      this.txt_HA_FTotal.MouseEnter += new EventHandler(this.txt_HA_FTotal_MouseEnter);
      this.txt_HA_FTotal.MouseLeave += new EventHandler(this.txt_HA_FTotal_MouseLeave);
      this.ln_HA_FTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_FTotal.BackColor = Color.Transparent;
      this.ln_HA_FTotal.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_FTotal.LineThickness = 1;
      this.ln_HA_FTotal.Location = new Point(-1, 18);
      this.ln_HA_FTotal.Name = "ln_HA_FTotal";
      this.ln_HA_FTotal.Size = new Size(145, 10);
      this.ln_HA_FTotal.TabIndex = 52;
      this.ln_HA_FTotal.TabStop = false;
      this.ln_HA_FTotal.Transparency = (int) byte.MaxValue;
      this.ln_HA_FTotal.Vertical = false;
      this.bunifuCustomLabel14.AutoSize = true;
      this.bunifuCustomLabel14.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel14.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel14.Location = new Point(340, 191);
      this.bunifuCustomLabel14.Name = "bunifuCustomLabel14";
      this.bunifuCustomLabel14.Size = new Size(78, 17);
      this.bunifuCustomLabel14.TabIndex = 71;
      this.bunifuCustomLabel14.Text = "Final Total:";
      this.panel7.Controls.Add((Control) this.txt_HA_QTCut);
      this.panel7.Controls.Add((Control) this.ln_HA_QTCut);
      this.panel7.Location = new Point(120, 186);
      this.panel7.Name = "panel7";
      this.panel7.Size = new Size(143, 26);
      this.panel7.TabIndex = 70;
      this.txt_HA_QTCut.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_QTCut.BackColor = Color.Silver;
      this.txt_HA_QTCut.BorderStyle = BorderStyle.None;
      this.txt_HA_QTCut.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_QTCut.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_QTCut.Location = new Point(2, 6);
      this.txt_HA_QTCut.Name = "txt_HA_QTCut";
      this.txt_HA_QTCut.Size = new Size(140, 16);
      this.txt_HA_QTCut.TabIndex = 7;
      this.txt_HA_QTCut.TextChanged += new EventHandler(this.txt_HA_QTCut_TextChanged);
      this.txt_HA_QTCut.Leave += new EventHandler(this.txt_HA_QTCut_Leave);
      this.txt_HA_QTCut.MouseEnter += new EventHandler(this.txt_HA_QTCut_MouseEnter);
      this.txt_HA_QTCut.MouseLeave += new EventHandler(this.txt_HA_QTCut_MouseLeave);
      this.ln_HA_QTCut.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_QTCut.BackColor = Color.Transparent;
      this.ln_HA_QTCut.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_QTCut.LineThickness = 1;
      this.ln_HA_QTCut.Location = new Point(-1, 18);
      this.ln_HA_QTCut.Name = "ln_HA_QTCut";
      this.ln_HA_QTCut.Size = new Size(145, 10);
      this.ln_HA_QTCut.TabIndex = 52;
      this.ln_HA_QTCut.TabStop = false;
      this.ln_HA_QTCut.Transparency = (int) byte.MaxValue;
      this.ln_HA_QTCut.Vertical = false;
      this.bunifuCustomLabel13.AutoSize = true;
      this.bunifuCustomLabel13.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel13.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel13.Location = new Point(36, 191);
      this.bunifuCustomLabel13.Name = "bunifuCustomLabel13";
      this.bunifuCustomLabel13.Size = new Size(80, 17);
      this.bunifuCustomLabel13.TabIndex = 69;
      this.bunifuCustomLabel13.Text = "QTech Cut:";
      this.dtp_HA_To.BackColor = Color.Silver;
      this.dtp_HA_To.BorderRadius = 0;
      this.dtp_HA_To.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.dtp_HA_To.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_HA_To.Format = DateTimePickerFormat.Short;
      this.dtp_HA_To.FormatCustom = (string) null;
      this.dtp_HA_To.Location = new Point(352, 28);
      this.dtp_HA_To.Name = "dtp_HA_To";
      this.dtp_HA_To.Size = new Size(212, 25);
      this.dtp_HA_To.TabIndex = 2;
      this.dtp_HA_To.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel12.AutoSize = true;
      this.bunifuCustomLabel12.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel12.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel12.Location = new Point(317, 29);
      this.bunifuCustomLabel12.Name = "bunifuCustomLabel12";
      this.bunifuCustomLabel12.Size = new Size(29, 17);
      this.bunifuCustomLabel12.TabIndex = 67;
      this.bunifuCustomLabel12.Text = "To:";
      this.dtp_HA_From.BackColor = Color.Silver;
      this.dtp_HA_From.BorderRadius = 0;
      this.dtp_HA_From.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.dtp_HA_From.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_HA_From.Format = DateTimePickerFormat.Short;
      this.dtp_HA_From.FormatCustom = (string) null;
      this.dtp_HA_From.Location = new Point(63, 28);
      this.dtp_HA_From.Name = "dtp_HA_From";
      this.dtp_HA_From.Size = new Size(200, 25);
      this.dtp_HA_From.TabIndex = 1;
      this.dtp_HA_From.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel11.AutoSize = true;
      this.bunifuCustomLabel11.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel11.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel11.Location = new Point(14, 29);
      this.bunifuCustomLabel11.Name = "bunifuCustomLabel11";
      this.bunifuCustomLabel11.Size = new Size(44, 17);
      this.bunifuCustomLabel11.TabIndex = 65;
      this.bunifuCustomLabel11.Text = "From:";
      this.panel6.Controls.Add((Control) this.txt_HA_TotAE);
      this.panel6.Controls.Add((Control) this.ln_HA_TotAE);
      this.panel6.Location = new Point(421, 146);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(143, 26);
      this.panel6.TabIndex = 64;
      this.txt_HA_TotAE.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_TotAE.BackColor = Color.Silver;
      this.txt_HA_TotAE.BorderStyle = BorderStyle.None;
      this.txt_HA_TotAE.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_TotAE.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_TotAE.Location = new Point(2, 6);
      this.txt_HA_TotAE.Name = "txt_HA_TotAE";
      this.txt_HA_TotAE.ReadOnly = true;
      this.txt_HA_TotAE.Size = new Size(140, 16);
      this.txt_HA_TotAE.TabIndex = 6;
      this.txt_HA_TotAE.TextChanged += new EventHandler(this.txt_HA_TotalAE_TextChanged);
      this.txt_HA_TotAE.Leave += new EventHandler(this.txt_HA_TotAE_Leave);
      this.txt_HA_TotAE.MouseEnter += new EventHandler(this.txt_HA_TotAE_MouseEnter);
      this.txt_HA_TotAE.MouseLeave += new EventHandler(this.txt_HA_TotAE_MouseLeave);
      this.ln_HA_TotAE.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_TotAE.BackColor = Color.Transparent;
      this.ln_HA_TotAE.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_TotAE.LineThickness = 1;
      this.ln_HA_TotAE.Location = new Point(-1, 18);
      this.ln_HA_TotAE.Name = "ln_HA_TotAE";
      this.ln_HA_TotAE.Size = new Size(145, 10);
      this.ln_HA_TotAE.TabIndex = 52;
      this.ln_HA_TotAE.TabStop = false;
      this.ln_HA_TotAE.Transparency = (int) byte.MaxValue;
      this.ln_HA_TotAE.Vertical = false;
      this.panel5.Controls.Add((Control) this.txt_HA_ExcRate);
      this.panel5.Controls.Add((Control) this.ln_HA_ExcRate);
      this.panel5.Location = new Point(121, 146);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(142, 26);
      this.panel5.TabIndex = 63;
      this.txt_HA_ExcRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_ExcRate.BackColor = Color.Silver;
      this.txt_HA_ExcRate.BorderStyle = BorderStyle.None;
      this.txt_HA_ExcRate.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_ExcRate.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_ExcRate.Location = new Point(3, 7);
      this.txt_HA_ExcRate.Name = "txt_HA_ExcRate";
      this.txt_HA_ExcRate.Size = new Size(139, 16);
      this.txt_HA_ExcRate.TabIndex = 5;
      this.txt_HA_ExcRate.Leave += new EventHandler(this.txt_HA_ExcRate_Leave);
      this.txt_HA_ExcRate.MouseEnter += new EventHandler(this.txt_HA_ExcRate_MouseEnter);
      this.txt_HA_ExcRate.MouseLeave += new EventHandler(this.txt_HA_ExcRate_MouseLeave);
      this.ln_HA_ExcRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_ExcRate.BackColor = Color.Transparent;
      this.ln_HA_ExcRate.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_ExcRate.LineThickness = 1;
      this.ln_HA_ExcRate.Location = new Point(-1, 18);
      this.ln_HA_ExcRate.Name = "ln_HA_ExcRate";
      this.ln_HA_ExcRate.Size = new Size(144, 10);
      this.ln_HA_ExcRate.TabIndex = 52;
      this.ln_HA_ExcRate.TabStop = false;
      this.ln_HA_ExcRate.Transparency = (int) byte.MaxValue;
      this.ln_HA_ExcRate.Vertical = false;
      this.panel4.Controls.Add((Control) this.txt_HA_TotBE);
      this.panel4.Controls.Add((Control) this.ln_HA_TotBE);
      this.panel4.Location = new Point(421, 106);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(143, 26);
      this.panel4.TabIndex = 62;
      this.txt_HA_TotBE.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_TotBE.BackColor = Color.Silver;
      this.txt_HA_TotBE.BorderStyle = BorderStyle.None;
      this.txt_HA_TotBE.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_TotBE.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_TotBE.Location = new Point(3, 7);
      this.txt_HA_TotBE.Name = "txt_HA_TotBE";
      this.txt_HA_TotBE.ReadOnly = true;
      this.txt_HA_TotBE.Size = new Size(140, 16);
      this.txt_HA_TotBE.TabIndex = 4;
      this.txt_HA_TotBE.TextChanged += new EventHandler(this.txt_HA_TotalBE_TextChanged);
      this.txt_HA_TotBE.Leave += new EventHandler(this.txt_HA_TotBE_Leave);
      this.txt_HA_TotBE.MouseEnter += new EventHandler(this.txt_HA_TotBE_MouseEnter);
      this.txt_HA_TotBE.MouseLeave += new EventHandler(this.txt_HA_TotBE_MouseLeave);
      this.ln_HA_TotBE.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_TotBE.BackColor = Color.Transparent;
      this.ln_HA_TotBE.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_TotBE.LineThickness = 1;
      this.ln_HA_TotBE.Location = new Point(-1, 18);
      this.ln_HA_TotBE.Name = "ln_HA_TotBE";
      this.ln_HA_TotBE.Size = new Size(145, 10);
      this.ln_HA_TotBE.TabIndex = 52;
      this.ln_HA_TotBE.TabStop = false;
      this.ln_HA_TotBE.Transparency = (int) byte.MaxValue;
      this.ln_HA_TotBE.Vertical = false;
      this.panel3.Controls.Add((Control) this.txt_HA_DolPH);
      this.panel3.Controls.Add((Control) this.ln_HA_DolPH);
      this.panel3.Location = new Point(121, 106);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(142, 26);
      this.panel3.TabIndex = 61;
      this.txt_HA_DolPH.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_DolPH.BackColor = Color.Silver;
      this.txt_HA_DolPH.BorderStyle = BorderStyle.None;
      this.txt_HA_DolPH.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_DolPH.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_DolPH.Location = new Point(3, 6);
      this.txt_HA_DolPH.Name = "txt_HA_DolPH";
      this.txt_HA_DolPH.Size = new Size(139, 16);
      this.txt_HA_DolPH.TabIndex = 4;
      this.txt_HA_DolPH.TextChanged += new EventHandler(this.txt_HA_PerHour_TextChanged);
      this.txt_HA_DolPH.Leave += new EventHandler(this.txt_HA_DolPH_Leave);
      this.txt_HA_DolPH.MouseEnter += new EventHandler(this.txt_HA_DolPH_MouseEnter);
      this.txt_HA_DolPH.MouseLeave += new EventHandler(this.txt_HA_DolPH_MouseLeave);
      this.ln_HA_DolPH.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_DolPH.BackColor = Color.Transparent;
      this.ln_HA_DolPH.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_DolPH.LineThickness = 1;
      this.ln_HA_DolPH.Location = new Point(-1, 18);
      this.ln_HA_DolPH.Name = "ln_HA_DolPH";
      this.ln_HA_DolPH.Size = new Size(144, 10);
      this.ln_HA_DolPH.TabIndex = 52;
      this.ln_HA_DolPH.TabStop = false;
      this.ln_HA_DolPH.Transparency = (int) byte.MaxValue;
      this.ln_HA_DolPH.Vertical = false;
      this.panel2.Controls.Add((Control) this.txt_HA_HW);
      this.panel2.Controls.Add((Control) this.ln_HA_HW);
      this.panel2.Location = new Point(482, 64);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(82, 27);
      this.panel2.TabIndex = 60;
      this.txt_HA_HW.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_HW.BackColor = Color.Silver;
      this.txt_HA_HW.BorderStyle = BorderStyle.None;
      this.txt_HA_HW.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_HW.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_HW.Location = new Point(2, 7);
      this.txt_HA_HW.Name = "txt_HA_HW";
      this.txt_HA_HW.Size = new Size(79, 16);
      this.txt_HA_HW.TabIndex = 3;
      this.txt_HA_HW.Leave += new EventHandler(this.txt_HA_HW_Leave);
      this.txt_HA_HW.MouseEnter += new EventHandler(this.txt_HA_HW_MouseEnter);
      this.txt_HA_HW.MouseLeave += new EventHandler(this.txt_HA_HW_MouseLeave);
      this.ln_HA_HW.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_HW.BackColor = Color.Transparent;
      this.ln_HA_HW.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_HW.LineThickness = 1;
      this.ln_HA_HW.Location = new Point(-1, 18);
      this.ln_HA_HW.Name = "ln_HA_HW";
      this.ln_HA_HW.Size = new Size(84, 10);
      this.ln_HA_HW.TabIndex = 0;
      this.ln_HA_HW.TabStop = false;
      this.ln_HA_HW.Transparency = (int) byte.MaxValue;
      this.ln_HA_HW.Vertical = false;
      this.panel1.Controls.Add((Control) this.txt_HA_Code);
      this.panel1.Controls.Add((Control) this.ln_HA_Code);
      this.panel1.Location = new Point(120, 64);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(144, 27);
      this.panel1.TabIndex = 52;
      this.txt_HA_Code.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_HA_Code.BackColor = Color.Silver;
      this.txt_HA_Code.BorderStyle = BorderStyle.None;
      this.txt_HA_Code.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_HA_Code.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_HA_Code.Location = new Point(2, 6);
      this.txt_HA_Code.Name = "txt_HA_Code";
      this.txt_HA_Code.Size = new Size(142, 16);
      this.txt_HA_Code.TabIndex = 3;
      this.txt_HA_Code.Leave += new EventHandler(this.txt_HA_Code_Leave);
      this.txt_HA_Code.MouseEnter += new EventHandler(this.txt_HA_Code_MouseEnter);
      this.txt_HA_Code.MouseLeave += new EventHandler(this.txt_HA_Code_MouseLeave);
      this.ln_HA_Code.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_HA_Code.BackColor = Color.Transparent;
      this.ln_HA_Code.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_HA_Code.LineThickness = 1;
      this.ln_HA_Code.Location = new Point(-1, 18);
      this.ln_HA_Code.Name = "ln_HA_Code";
      this.ln_HA_Code.Size = new Size(146, 10);
      this.ln_HA_Code.TabIndex = 52;
      this.ln_HA_Code.TabStop = false;
      this.ln_HA_Code.Transparency = (int) byte.MaxValue;
      this.ln_HA_Code.Vertical = false;
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel8.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel8.Location = new Point(352, 150);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new Size(68, 17);
      this.bunifuCustomLabel8.TabIndex = 0;
      this.bunifuCustomLabel8.Text = "Total (R):";
      this.bunifuCustomLabel9.AutoSize = true;
      this.bunifuCustomLabel9.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel9.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel9.Location = new Point(13, 151);
      this.bunifuCustomLabel9.Name = "bunifuCustomLabel9";
      this.bunifuCustomLabel9.Size = new Size(108, 17);
      this.bunifuCustomLabel9.TabIndex = 0;
      this.bunifuCustomLabel9.Text = "Exchange Rate:";
      this.bunifuCustomLabel7.AutoSize = true;
      this.bunifuCustomLabel7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel7.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel7.Location = new Point(352, 109);
      this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
      this.bunifuCustomLabel7.Size = new Size(66, 17);
      this.bunifuCustomLabel7.TabIndex = 0;
      this.bunifuCustomLabel7.Text = "Total ($):";
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(57, 109);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(55, 17);
      this.bunifuCustomLabel6.TabIndex = 0;
      this.bunifuCustomLabel6.Text = "$/Hour:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(373, 67);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(103, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Hours Worked:";
      this.dtp_HA_DatePaid.BackColor = Color.Silver;
      this.dtp_HA_DatePaid.BorderRadius = 0;
      this.dtp_HA_DatePaid.Enabled = false;
      this.dtp_HA_DatePaid.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_HA_DatePaid.Format = DateTimePickerFormat.Short;
      this.dtp_HA_DatePaid.FormatCustom = (string) null;
      this.dtp_HA_DatePaid.Location = new Point(356, 233);
      this.dtp_HA_DatePaid.Name = "dtp_HA_DatePaid";
      this.dtp_HA_DatePaid.Size = new Size(208, 19);
      this.dtp_HA_DatePaid.TabIndex = 10;
      this.dtp_HA_DatePaid.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(283, 234);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(74, 17);
      this.bunifuCustomLabel2.TabIndex = 0;
      this.bunifuCustomLabel2.Text = "Date Paid:";
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(69, 67);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(45, 17);
      this.bunifuCustomLabel1.TabIndex = 0;
      this.bunifuCustomLabel1.Text = "Code:";
      this.btn_HA_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_HA_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_HA_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_HA_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_HA_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_HA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_HA_Cancel.Location = new Point(486, 463);
      this.btn_HA_Cancel.Name = "btn_HA_Cancel";
      this.btn_HA_Cancel.Size = new Size(114, 40);
      this.btn_HA_Cancel.TabIndex = 12;
      this.btn_HA_Cancel.Text = "Cancel";
      this.btn_HA_Cancel.UseVisualStyleBackColor = true;
      this.btn_HA_Cancel.Click += new EventHandler(this.btn_HA_Cancel_Click);
      this.btn_HA_Cancel.MouseEnter += new EventHandler(this.btn_HA_Cancel_MouseEnter);
      this.btn_HA_Cancel.MouseLeave += new EventHandler(this.btn_HA_Cancel_MouseLeave);
      this.btn_HA_Done.FlatAppearance.BorderSize = 0;
      this.btn_HA_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_HA_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_HA_Done.FlatStyle = FlatStyle.Flat;
      this.btn_HA_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_HA_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_HA_Done.Location = new Point(366, 463);
      this.btn_HA_Done.Name = "btn_HA_Done";
      this.btn_HA_Done.Size = new Size(114, 40);
      this.btn_HA_Done.TabIndex = 11;
      this.btn_HA_Done.Text = "Done";
      this.btn_HA_Done.UseVisualStyleBackColor = true;
      this.btn_HA_Done.Click += new EventHandler(this.btn_HA_Done_Click);
      this.btn_HA_Done.MouseEnter += new EventHandler(this.btn_HA_Done_MouseEnter);
      this.btn_HA_Done.MouseLeave += new EventHandler(this.btn_HA_Done_MouseLeave);
      this.btn_HA_CreateRem.FlatAppearance.BorderSize = 0;
      this.btn_HA_CreateRem.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_HA_CreateRem.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_HA_CreateRem.FlatStyle = FlatStyle.Flat;
      this.btn_HA_CreateRem.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_HA_CreateRem.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_HA_CreateRem.Location = new Point(12, 463);
      this.btn_HA_CreateRem.Name = "btn_HA_CreateRem";
      this.btn_HA_CreateRem.Size = new Size(154, 40);
      this.btn_HA_CreateRem.TabIndex = 98;
      this.btn_HA_CreateRem.Text = "Create Remittance";
      this.btn_HA_CreateRem.UseVisualStyleBackColor = true;
      this.btn_HA_CreateRem.Visible = false;
      this.btn_HA_CreateRem.Click += new EventHandler(this.btn_HA_CRem_Click);
      this.btn_HA_CreateRem.MouseEnter += new EventHandler(this.btn_HA_CreateRem_MouseEnter);
      this.btn_HA_CreateRem.MouseLeave += new EventHandler(this.btn_HA_CreateRem_MouseLeave);
      this.bunifuCustomLabel17.AutoSize = true;
      this.bunifuCustomLabel17.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel17.ForeColor = Color.FromArgb(15, 91, 142);
      this.bunifuCustomLabel17.Location = new Point(211, 10);
      this.bunifuCustomLabel17.Name = "bunifuCustomLabel17";
      this.bunifuCustomLabel17.Size = new Size(181, 22);
      this.bunifuCustomLabel17.TabIndex = 99;
      this.bunifuCustomLabel17.Text = "Add New Work Week";
      this.btn_HA_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_HA_Close.BackColor = Color.Silver;
      this.btn_HA_Close.FlatAppearance.BorderSize = 0;
      this.btn_HA_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_HA_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_HA_Close.FlatStyle = FlatStyle.Flat;
      this.btn_HA_Close.Image = (Image) Resources.close_black;
      this.btn_HA_Close.Location = new Point(570, 5);
      this.btn_HA_Close.Name = "btn_HA_Close";
      this.btn_HA_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_HA_Close.Size = new Size(31, 29);
      this.btn_HA_Close.TabIndex = 100;
      this.btn_HA_Close.TabStop = false;
      this.btn_HA_Close.UseVisualStyleBackColor = false;
      this.btn_HA_Close.Click += new EventHandler(this.btn_HA_Close_Click);
      this.btn_HA_Close.MouseEnter += new EventHandler(this.btn_HA_Close_MouseEnter);
      this.btn_HA_Close.MouseLeave += new EventHandler(this.btn_HA_Close_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(606, 511);
      this.Controls.Add((Control) this.bunifuCustomLabel17);
      this.Controls.Add((Control) this.btn_HA_Close);
      this.Controls.Add((Control) this.btn_HA_CreateRem);
      this.Controls.Add((Control) this.btn_HA_Cancel);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.Controls.Add((Control) this.btn_HA_Done);
      this.Controls.Add((Control) this.pb_CreateRem);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(600, 465);
      this.Name = nameof (Hours_Add);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Add Hours";
      this.Load += new EventHandler(this.Hours_Add_Load);
      this.MouseDown += new MouseEventHandler(this.O_Add_MouseDown);
      this.MouseMove += new MouseEventHandler(this.O_Add_MouseMove);
      this.MouseUp += new MouseEventHandler(this.O_Add_MouseUp);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel12.ResumeLayout(false);
      this.panel12.PerformLayout();
      this.panel11.ResumeLayout(false);
      this.panel11.PerformLayout();
      this.panel10.ResumeLayout(false);
      this.panel10.PerformLayout();
      this.panel9.ResumeLayout(false);
      this.panel9.PerformLayout();
      this.gb_OA_ODetails.ResumeLayout(false);
      this.gb_OA_ODetails.PerformLayout();
      this.panel8.ResumeLayout(false);
      this.panel8.PerformLayout();
      this.panel7.ResumeLayout(false);
      this.panel7.PerformLayout();
      this.panel6.ResumeLayout(false);
      this.panel6.PerformLayout();
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
