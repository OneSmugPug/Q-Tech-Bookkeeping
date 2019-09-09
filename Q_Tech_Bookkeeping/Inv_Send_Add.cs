// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Inv_Send_Add
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Inv_Send_Add : Form
  {
    private DataTable dt = new DataTable();
    private bool isInter = false;
    private Invoices_Send parent = (Invoices_Send) null;
    private Int_Invoices_Send intParent = (Int_Invoices_Send) null;
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private Point lastLocation;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_ISA_Close;
    private GroupBox gb_OA_CDetails;
    private BunifuMaterialTextbox txt_ISA_CName;
    private BunifuCustomLabel bunifuCustomLabel4;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuMaterialTextbox txt_ISA_CCode;
    private Button btn_ISA_Cancel;
    private Button btn_ISA_Done;
    private GroupBox gb_OA_ODetails;
    private BunifuDatepicker dtp_ISA_DatePaid;
    private BunifuCustomLabel bunifuCustomLabel9;
    private Panel panel4;
    private TextBox txt_ISA_VAT;
    private BunifuSeparator ln_ISA_VAT;
    private BunifuCustomLabel bunifuCustomLabel8;
    private Panel panel3;
    private TextBox txt_ISA_Amt;
    private BunifuSeparator ln_ISA_Amt;
    private BunifuCustomLabel bunifuCustomLabel7;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCheckbox cb_ISA_Paid;
    private Panel panel2;
    private TextBox txt_ISA_Desc;
    private BunifuSeparator ln_ISA_Desc;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_ISA_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;
    private Panel panel5;
    private TextBox txt_ISA_INInst;
    private BunifuSeparator ln_ISA_INInst;
    private BunifuCustomLabel bunifuCustomLabel11;
    private Panel panel1;
    private TextBox txt_ISA_InvNum;
    private BunifuSeparator ln_ISA_InvNum;

    public Inv_Send_Add()
    {
      this.InitializeComponent();
    }

    private void Inv_Send_Add_Load(object sender, EventArgs e)
    {
      HomeOld owner = (HomeOld) this.Owner;
      if (owner.getCurPanel() == "pnl_L_InvSent")
      {
        this.parent = (Invoices_Send) owner.getCurForm();
        this.txt_ISA_CCode.Text = this.parent.getCCode();
        this.txt_ISA_CName.Text = this.parent.getCName();
        this.txt_ISA_Amt.Text = "R0.00";
        this.txt_ISA_Amt.SelectionStart = this.txt_ISA_Amt.Text.Length;
        this.txt_ISA_VAT.Text = "R0.00";
        this.txt_ISA_VAT.SelectionStart = this.txt_ISA_VAT.Text.Length;
        this.dt = this.parent.getInvoices();
      }
      else
      {
        this.isInter = true;
        this.intParent = (Int_Invoices_Send) owner.getCurForm();
        this.txt_ISA_CCode.Text = this.intParent.getCCode();
        this.txt_ISA_CName.Text = this.intParent.getCName();
        this.txt_ISA_Amt.Text = "$0.00";
        this.txt_ISA_Amt.SelectionStart = this.txt_ISA_Amt.Text.Length;
        this.txt_ISA_VAT.Text = "$0.00";
        this.txt_ISA_VAT.SelectionStart = this.txt_ISA_VAT.Text.Length;
        this.dt = this.intParent.getInvoices();
      }
      int num1 = 0;
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row.RowState == DataRowState.Deleted)
        {
          string str1 = row["Invoice_Number", DataRowVersion.Original].ToString().Trim();
          if (!str1.Contains("."))
          {
            int num2 = str1.IndexOf("_");
            int int32 = Convert.ToInt32(str1.Remove(0, num2 + 2));
            if (int32 > num1)
              num1 = int32;
          }
          else
          {
            int num2 = str1.IndexOf("_");
            string str2 = str1.Remove(0, num2 + 2);
            int startIndex = str2.IndexOf(".");
            int int32 = Convert.ToInt32(str2.Remove(startIndex, str2.Length - 3));
            if (int32 > num1)
              num1 = int32;
          }
        }
        else
        {
          string str1 = row["Invoice_Number"].ToString().Trim();
          if (!str1.Contains("."))
          {
            int num2 = str1.IndexOf("_");
            int int32 = Convert.ToInt32(str1.Remove(0, num2 + 2));
            if (int32 > num1)
              num1 = int32;
          }
          else
          {
            int num2 = str1.IndexOf("_");
            string str2 = str1.Remove(0, num2 + 2);
            int startIndex = str2.IndexOf(".");
            int int32 = Convert.ToInt32(str2.Remove(startIndex, str2.Length - 3));
            if (int32 > num1)
              num1 = int32;
          }
        }
      }
      this.txt_ISA_InvNum.Text = this.txt_ISA_CCode.Text + "_I" + (num1 + 1).ToString("000");
      this.dtp_ISA_Date.Value = DateTime.Now;
      this.dtp_ISA_DatePaid.Value = DateTime.Now;
    }

    private void txt_ISA_Amt_TextChanged(object sender, EventArgs e)
    {
      if (!this.isInter)
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_ISA_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
        {
          result /= new Decimal(100);
          this.txt_ISA_Amt.TextChanged -= new EventHandler(this.txt_ISA_Amt_TextChanged);
          this.txt_ISA_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) result);
          this.txt_ISA_Amt.TextChanged += new EventHandler(this.txt_ISA_Amt_TextChanged);
          this.txt_ISA_Amt.Select(this.txt_ISA_Amt.Text.Length, 0);
        }
        if (this.TextisValid(this.txt_ISA_Amt.Text))
          return;
        this.txt_ISA_Amt.Text = "R0.00";
        this.txt_ISA_Amt.Select(this.txt_ISA_Amt.Text.Length, 0);
      }
      else
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_ISA_Amt.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
        {
          result /= new Decimal(100);
          this.txt_ISA_Amt.TextChanged -= new EventHandler(this.txt_ISA_Amt_TextChanged);
          this.txt_ISA_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (object) result);
          this.txt_ISA_Amt.TextChanged += new EventHandler(this.txt_ISA_Amt_TextChanged);
          this.txt_ISA_Amt.Select(this.txt_ISA_Amt.Text.Length, 0);
        }
        if (!this.TextisValid(this.txt_ISA_Amt.Text))
        {
          this.txt_ISA_Amt.Text = "$0.00";
          this.txt_ISA_Amt.Select(this.txt_ISA_Amt.Text.Length, 0);
        }
      }
    }

    private bool TextisValid(string text)
    {
      return new Regex("[^0-9]").IsMatch(text);
    }

    private void txt_ISA_VAT_TextChanged(object sender, EventArgs e)
    {
      if (!this.isInter)
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_ISA_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
        {
          result /= new Decimal(100);
          this.txt_ISA_VAT.TextChanged -= new EventHandler(this.txt_ISA_VAT_TextChanged);
          this.txt_ISA_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) result);
          this.txt_ISA_VAT.TextChanged += new EventHandler(this.txt_ISA_VAT_TextChanged);
          this.txt_ISA_VAT.Select(this.txt_ISA_VAT.Text.Length, 0);
        }
        if (this.TextisValid(this.txt_ISA_VAT.Text))
          return;
        this.txt_ISA_VAT.Text = "R0.00";
        this.txt_ISA_VAT.Select(this.txt_ISA_VAT.Text.Length, 0);
      }
      else
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_ISA_VAT.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
        {
          result /= new Decimal(100);
          this.txt_ISA_VAT.TextChanged -= new EventHandler(this.txt_ISA_VAT_TextChanged);
          this.txt_ISA_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (object) result);
          this.txt_ISA_VAT.TextChanged += new EventHandler(this.txt_ISA_VAT_TextChanged);
          this.txt_ISA_VAT.Select(this.txt_ISA_VAT.Text.Length, 0);
        }
        if (!this.TextisValid(this.txt_ISA_VAT.Text))
        {
          this.txt_ISA_VAT.Text = "$0.00";
          this.txt_ISA_VAT.Select(this.txt_ISA_VAT.Text.Length, 0);
        }
      }
    }

    private void txt_ISA_Amt_Leave(object sender, EventArgs e)
    {
      this.ln_ISA_Amt.LineColor = Color.Gray;
      if (!this.isInter)
      {
        Decimal result;
        if (!Decimal.TryParse(this.txt_ISA_Amt.Text.Replace("R", string.Empty), out result))
          return;
        this.txt_ISA_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) (result - result / new Decimal(115, 0, 0, false, (byte) 2)));
      }
      else
      {
        Decimal num = Decimal.Parse(this.txt_ISA_Amt.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US"));
        this.txt_ISA_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (object) (num - num / new Decimal(115, 0, 0, false, (byte) 2)));
      }
    }

    private void btn_ISA_Done_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to add invoice with Invoice Number: " + this.txt_ISA_InvNum.Text + "?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        try
        {
          using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Invoices_Send VALUES (@Date, @InvNum, @Client, @Desc, @Amt, @VAT, @Paid, @DatePaid)", dbConnection))
          {
            Decimal num1;
            Decimal num2;
            if (!this.isInter)
            {
              num1 = !this.txt_ISA_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_ISA_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_ISA_Amt.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
              num2 = !this.txt_ISA_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_ISA_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_ISA_VAT.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
            }
            else
            {
              num1 = !this.txt_ISA_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_ISA_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_ISA_Amt.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2));
              num2 = !this.txt_ISA_VAT.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_ISA_VAT.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_ISA_VAT.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2));
            }
            sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_ISA_Date.Value);
            sqlCommand.Parameters.AddWithValue("@InvNum", (object) this.txt_ISA_InvNum.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Client", (object) this.txt_ISA_CName.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_ISA_Desc.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Amt", (object) num1);
            sqlCommand.Parameters.AddWithValue("@VAT", (object) num2);
            if (this.cb_ISA_Paid.Checked)
            {
              sqlCommand.Parameters.AddWithValue("@Paid", (object) "Yes");
              sqlCommand.Parameters.AddWithValue("@DatePaid", (object) this.dtp_ISA_DatePaid.Value);
            }
            else
            {
              sqlCommand.Parameters.AddWithValue("@Paid", (object) "No");
              sqlCommand.Parameters.AddWithValue("@DatePaid", (object) DBNull.Value);
            }
            sqlCommand.ExecuteNonQuery();
            int num3 = (int) MessageBox.Show("New invoice successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            if (!this.isInter)
              this.parent.setNewInvoice(this.txt_ISA_InvNum.Text);
            else
              this.intParent.setNewInvoice(this.txt_ISA_InvNum.Text);
            this.Close();
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void btn_ISA_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_ISA_InvNum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_ISA_InvNum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_ISA_InvNum_Leave(object sender, EventArgs e)
    {
      this.ln_ISA_InvNum.LineColor = Color.Gray;
    }

    private void txt_ISA_InvNum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_ISA_InvNum.Focused)
        return;
      this.ln_ISA_InvNum.LineColor = Color.Gray;
    }

    private void txt_ISA_INInst_MouseEnter(object sender, EventArgs e)
    {
      this.ln_ISA_INInst.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_ISA_INInst_Leave(object sender, EventArgs e)
    {
      this.ln_ISA_INInst.LineColor = Color.Gray;
    }

    private void txt_ISA_INInst_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_ISA_INInst.Focused)
        return;
      this.ln_ISA_INInst.LineColor = Color.Gray;
    }

    private void txt_ISA_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_ISA_Desc.LineColor = Color.Gray;
    }

    private void txt_ISA_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_ISA_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_ISA_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_ISA_Desc.Focused)
        return;
      this.ln_ISA_Desc.LineColor = Color.Gray;
    }

    private void txt_ISA_Amt_MouseEnter(object sender, EventArgs e)
    {
      this.ln_ISA_Amt.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_ISA_Amt_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_ISA_Amt.Focused)
        return;
      this.ln_ISA_Amt.LineColor = Color.Gray;
    }

    private void txt_ISA_VAT_Leave(object sender, EventArgs e)
    {
      this.ln_ISA_VAT.LineColor = Color.Gray;
    }

    private void txt_ISA_VAT_MouseEnter(object sender, EventArgs e)
    {
      this.ln_ISA_VAT.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_ISA_VAT_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_ISA_VAT.Focused)
        return;
      this.ln_ISA_VAT.LineColor = Color.Gray;
    }

    private void btn_ISA_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_ISA_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_ISA_Close.Image = (Image) Resources.close_white;
    }

    private void btn_ISA_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_ISA_Close.Image = (Image) Resources.close_black;
    }

    private void btn_ISA_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_ISA_Done.ForeColor = Color.White;
    }

    private void btn_ISA_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_ISA_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_ISA_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_ISA_Cancel.ForeColor = Color.White;
    }

    private void btn_ISA_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_ISA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_ISA_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_ISA_CName_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void Inv_Send_Add_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void Inv_Send_Add_MouseMove(object sender, MouseEventArgs e)
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

    private void Inv_Sent_Add_MouseUp(object sender, MouseEventArgs e)
    {
      this.mouseDown = false;
    }

    private void cb_ISA_Paid_OnChange(object sender, EventArgs e)
    {
      if (this.cb_ISA_Paid.Checked)
        this.dtp_ISA_DatePaid.Enabled = true;
      else
        this.dtp_ISA_DatePaid.Enabled = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Inv_Send_Add));
      this.bunifuCustomLabel10 = new BunifuCustomLabel();
      this.btn_ISA_Close = new Button();
      this.gb_OA_CDetails = new GroupBox();
      this.txt_ISA_CName = new BunifuMaterialTextbox();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.txt_ISA_CCode = new BunifuMaterialTextbox();
      this.btn_ISA_Cancel = new Button();
      this.btn_ISA_Done = new Button();
      this.gb_OA_ODetails = new GroupBox();
      this.panel5 = new Panel();
      this.txt_ISA_INInst = new TextBox();
      this.ln_ISA_INInst = new BunifuSeparator();
      this.bunifuCustomLabel11 = new BunifuCustomLabel();
      this.panel1 = new Panel();
      this.txt_ISA_InvNum = new TextBox();
      this.ln_ISA_InvNum = new BunifuSeparator();
      this.dtp_ISA_DatePaid = new BunifuDatepicker();
      this.bunifuCustomLabel9 = new BunifuCustomLabel();
      this.panel4 = new Panel();
      this.txt_ISA_VAT = new TextBox();
      this.ln_ISA_VAT = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.panel3 = new Panel();
      this.txt_ISA_Amt = new TextBox();
      this.ln_ISA_Amt = new BunifuSeparator();
      this.bunifuCustomLabel7 = new BunifuCustomLabel();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.cb_ISA_Paid = new BunifuCheckbox();
      this.panel2 = new Panel();
      this.txt_ISA_Desc = new TextBox();
      this.ln_ISA_Desc = new BunifuSeparator();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_ISA_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.gb_OA_CDetails.SuspendLayout();
      this.gb_OA_ODetails.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.bunifuCustomLabel10.AutoSize = true;
      this.bunifuCustomLabel10.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel10.ForeColor = Color.FromArgb(15, 91, 142);
      this.bunifuCustomLabel10.Location = new Point(290, 5);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new Size(144, 22);
      this.bunifuCustomLabel10.TabIndex = 54;
      this.bunifuCustomLabel10.Text = "Add New Invoice";
      this.btn_ISA_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_ISA_Close.BackColor = Color.Silver;
      this.btn_ISA_Close.FlatAppearance.BorderSize = 0;
      this.btn_ISA_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_ISA_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_ISA_Close.FlatStyle = FlatStyle.Flat;
      this.btn_ISA_Close.Image = (Image) Resources.close_black;
      this.btn_ISA_Close.Location = new Point(673, 4);
      this.btn_ISA_Close.Name = "btn_ISA_Close";
      this.btn_ISA_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_ISA_Close.Size = new Size(31, 29);
      this.btn_ISA_Close.TabIndex = 56;
      this.btn_ISA_Close.TabStop = false;
      this.btn_ISA_Close.UseVisualStyleBackColor = false;
      this.btn_ISA_Close.Click += new EventHandler(this.btn_ISA_Close_Click);
      this.btn_ISA_Close.MouseEnter += new EventHandler(this.btn_ISA_Close_MouseEnter);
      this.btn_ISA_Close.MouseLeave += new EventHandler(this.btn_ISA_Close_MouseLeave);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_ISA_CName);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel4);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_ISA_CCode);
      this.gb_OA_CDetails.FlatStyle = FlatStyle.Flat;
      this.gb_OA_CDetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_CDetails.Location = new Point(11, 40);
      this.gb_OA_CDetails.Name = "gb_OA_CDetails";
      this.gb_OA_CDetails.Size = new Size(687, 59);
      this.gb_OA_CDetails.TabIndex = 55;
      this.gb_OA_CDetails.TabStop = false;
      this.gb_OA_CDetails.Text = "Client Details";
      this.txt_ISA_CName.Cursor = Cursors.IBeam;
      this.txt_ISA_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_ISA_CName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_ISA_CName.HintForeColor = Color.Empty;
      this.txt_ISA_CName.HintText = "";
      this.txt_ISA_CName.isPassword = false;
      this.txt_ISA_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_ISA_CName.LineIdleColor = Color.Gray;
      this.txt_ISA_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_ISA_CName.LineThickness = 1;
      this.txt_ISA_CName.Location = new Point(469, 15);
      this.txt_ISA_CName.Margin = new Padding(4);
      this.txt_ISA_CName.Name = "txt_ISA_CName";
      this.txt_ISA_CName.Size = new Size(198, 30);
      this.txt_ISA_CName.TabIndex = 46;
      this.txt_ISA_CName.TabStop = false;
      this.txt_ISA_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_ISA_CName.KeyDown += new KeyEventHandler(this.txt_ISA_CName_KeyDown);
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(18, 25);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(84, 17);
      this.bunifuCustomLabel4.TabIndex = 0;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point(375, 25);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(88, 17);
      this.bunifuCustomLabel3.TabIndex = 0;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.txt_ISA_CCode.Cursor = Cursors.IBeam;
      this.txt_ISA_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_ISA_CCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_ISA_CCode.HintForeColor = Color.Empty;
      this.txt_ISA_CCode.HintText = "";
      this.txt_ISA_CCode.isPassword = false;
      this.txt_ISA_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_ISA_CCode.LineIdleColor = Color.Gray;
      this.txt_ISA_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_ISA_CCode.LineThickness = 1;
      this.txt_ISA_CCode.Location = new Point(108, 15);
      this.txt_ISA_CCode.Margin = new Padding(4);
      this.txt_ISA_CCode.Name = "txt_ISA_CCode";
      this.txt_ISA_CCode.Size = new Size(202, 30);
      this.txt_ISA_CCode.TabIndex = 45;
      this.txt_ISA_CCode.TabStop = false;
      this.txt_ISA_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_ISA_CCode.KeyDown += new KeyEventHandler(this.txt_ISA_CCode_KeyDown);
      this.btn_ISA_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_ISA_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_ISA_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_ISA_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_ISA_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_ISA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_ISA_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_ISA_Cancel.Location = new Point(584, 297);
      this.btn_ISA_Cancel.Name = "btn_ISA_Cancel";
      this.btn_ISA_Cancel.Size = new Size(114, 40);
      this.btn_ISA_Cancel.TabIndex = 9;
      this.btn_ISA_Cancel.Text = "Cancel";
      this.btn_ISA_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_ISA_Cancel.UseVisualStyleBackColor = true;
      this.btn_ISA_Cancel.Click += new EventHandler(this.btn_ISA_Cancel_Click);
      this.btn_ISA_Cancel.MouseEnter += new EventHandler(this.btn_ISA_Cancel_MouseEnter);
      this.btn_ISA_Cancel.MouseLeave += new EventHandler(this.btn_ISA_Cancel_MouseLeave);
      this.btn_ISA_Done.FlatAppearance.BorderSize = 0;
      this.btn_ISA_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_ISA_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_ISA_Done.FlatStyle = FlatStyle.Flat;
      this.btn_ISA_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_ISA_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_ISA_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_ISA_Done.Location = new Point(464, 297);
      this.btn_ISA_Done.Name = "btn_ISA_Done";
      this.btn_ISA_Done.Size = new Size(114, 40);
      this.btn_ISA_Done.TabIndex = 8;
      this.btn_ISA_Done.Text = "Done";
      this.btn_ISA_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_ISA_Done.UseVisualStyleBackColor = true;
      this.btn_ISA_Done.Click += new EventHandler(this.btn_ISA_Done_Click);
      this.btn_ISA_Done.MouseEnter += new EventHandler(this.btn_ISA_Done_MouseEnter);
      this.btn_ISA_Done.MouseLeave += new EventHandler(this.btn_ISA_Done_MouseLeave);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel11);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel1);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_ISA_DatePaid);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel9);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel4);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel8);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel3);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel7);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.cb_ISA_Paid);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_ISA_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(11, 120);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(687, 171);
      this.gb_OA_ODetails.TabIndex = 59;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Invoice Details";
      this.panel5.Controls.Add((Control) this.txt_ISA_INInst);
      this.panel5.Controls.Add((Control) this.ln_ISA_INInst);
      this.panel5.Location = new Point(283, 23);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(37, 27);
      this.panel5.TabIndex = 72;
      this.txt_ISA_INInst.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_ISA_INInst.BackColor = Color.Silver;
      this.txt_ISA_INInst.BorderStyle = BorderStyle.None;
      this.txt_ISA_INInst.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_ISA_INInst.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_ISA_INInst.Location = new Point(2, 4);
      this.txt_ISA_INInst.Name = "txt_ISA_INInst";
      this.txt_ISA_INInst.Size = new Size(35, 16);
      this.txt_ISA_INInst.TabIndex = 1;
      this.txt_ISA_INInst.Leave += new EventHandler(this.txt_ISA_INInst_Leave);
      this.txt_ISA_INInst.MouseEnter += new EventHandler(this.txt_ISA_INInst_MouseEnter);
      this.txt_ISA_INInst.MouseLeave += new EventHandler(this.txt_ISA_INInst_MouseLeave);
      this.ln_ISA_INInst.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_ISA_INInst.BackColor = Color.Transparent;
      this.ln_ISA_INInst.Enabled = false;
      this.ln_ISA_INInst.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_ISA_INInst.LineThickness = 1;
      this.ln_ISA_INInst.Location = new Point(-1, 18);
      this.ln_ISA_INInst.Name = "ln_ISA_INInst";
      this.ln_ISA_INInst.Size = new Size(39, 10);
      this.ln_ISA_INInst.TabIndex = 52;
      this.ln_ISA_INInst.TabStop = false;
      this.ln_ISA_INInst.Transparency = (int) byte.MaxValue;
      this.ln_ISA_INInst.Vertical = false;
      this.bunifuCustomLabel11.AutoSize = true;
      this.bunifuCustomLabel11.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel11.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel11.Location = new Point(265, 26);
      this.bunifuCustomLabel11.Name = "bunifuCustomLabel11";
      this.bunifuCustomLabel11.Size = new Size(12, 17);
      this.bunifuCustomLabel11.TabIndex = 71;
      this.bunifuCustomLabel11.Text = ".";
      this.panel1.Controls.Add((Control) this.txt_ISA_InvNum);
      this.panel1.Controls.Add((Control) this.ln_ISA_InvNum);
      this.panel1.Location = new Point(133, 23);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(126, 27);
      this.panel1.TabIndex = 70;
      this.txt_ISA_InvNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_ISA_InvNum.BackColor = Color.Silver;
      this.txt_ISA_InvNum.BorderStyle = BorderStyle.None;
      this.txt_ISA_InvNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_ISA_InvNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_ISA_InvNum.Location = new Point(2, 4);
      this.txt_ISA_InvNum.Name = "txt_ISA_InvNum";
      this.txt_ISA_InvNum.Size = new Size(124, 16);
      this.txt_ISA_InvNum.TabIndex = 1;
      this.txt_ISA_InvNum.Leave += new EventHandler(this.txt_ISA_InvNum_Leave);
      this.txt_ISA_InvNum.MouseEnter += new EventHandler(this.txt_ISA_InvNum_MouseEnter);
      this.txt_ISA_InvNum.MouseLeave += new EventHandler(this.txt_ISA_InvNum_MouseLeave);
      this.ln_ISA_InvNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_ISA_InvNum.BackColor = Color.Transparent;
      this.ln_ISA_InvNum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_ISA_InvNum.LineThickness = 1;
      this.ln_ISA_InvNum.Location = new Point(-1, 18);
      this.ln_ISA_InvNum.Name = "ln_ISA_InvNum";
      this.ln_ISA_InvNum.Size = new Size(128, 10);
      this.ln_ISA_InvNum.TabIndex = 52;
      this.ln_ISA_InvNum.TabStop = false;
      this.ln_ISA_InvNum.Transparency = (int) byte.MaxValue;
      this.ln_ISA_InvNum.Vertical = false;
      this.dtp_ISA_DatePaid.BackColor = Color.Silver;
      this.dtp_ISA_DatePaid.BorderRadius = 0;
      this.dtp_ISA_DatePaid.Enabled = false;
      this.dtp_ISA_DatePaid.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_ISA_DatePaid.Format = DateTimePickerFormat.Short;
      this.dtp_ISA_DatePaid.FormatCustom = (string) null;
      this.dtp_ISA_DatePaid.Location = new Point(430, 132);
      this.dtp_ISA_DatePaid.Name = "dtp_ISA_DatePaid";
      this.dtp_ISA_DatePaid.Size = new Size(237, 20);
      this.dtp_ISA_DatePaid.TabIndex = 7;
      this.dtp_ISA_DatePaid.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel9.AutoSize = true;
      this.bunifuCustomLabel9.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel9.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel9.Location = new Point(353, 132);
      this.bunifuCustomLabel9.Name = "bunifuCustomLabel9";
      this.bunifuCustomLabel9.Size = new Size(74, 17);
      this.bunifuCustomLabel9.TabIndex = 67;
      this.bunifuCustomLabel9.Text = "Date Paid:";
      this.panel4.Controls.Add((Control) this.txt_ISA_VAT);
      this.panel4.Controls.Add((Control) this.ln_ISA_VAT);
      this.panel4.Location = new Point(483, 91);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(184, 27);
      this.panel4.TabIndex = 66;
      this.txt_ISA_VAT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_ISA_VAT.BackColor = Color.Silver;
      this.txt_ISA_VAT.BorderStyle = BorderStyle.None;
      this.txt_ISA_VAT.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_ISA_VAT.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_ISA_VAT.Location = new Point(2, 5);
      this.txt_ISA_VAT.Name = "txt_ISA_VAT";
      this.txt_ISA_VAT.Size = new Size(181, 16);
      this.txt_ISA_VAT.TabIndex = 5;
      this.txt_ISA_VAT.TextChanged += new EventHandler(this.txt_ISA_VAT_TextChanged);
      this.txt_ISA_VAT.Leave += new EventHandler(this.txt_ISA_VAT_Leave);
      this.txt_ISA_VAT.MouseEnter += new EventHandler(this.txt_ISA_VAT_MouseEnter);
      this.txt_ISA_VAT.MouseLeave += new EventHandler(this.txt_ISA_VAT_MouseLeave);
      this.ln_ISA_VAT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_ISA_VAT.BackColor = Color.Transparent;
      this.ln_ISA_VAT.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_ISA_VAT.LineThickness = 1;
      this.ln_ISA_VAT.Location = new Point(-1, 18);
      this.ln_ISA_VAT.Name = "ln_ISA_VAT";
      this.ln_ISA_VAT.Size = new Size(186, 10);
      this.ln_ISA_VAT.TabIndex = 0;
      this.ln_ISA_VAT.TabStop = false;
      this.ln_ISA_VAT.Transparency = (int) byte.MaxValue;
      this.ln_ISA_VAT.Vertical = false;
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel8.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel8.Location = new Point(440, 95);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new Size(39, 17);
      this.bunifuCustomLabel8.TabIndex = 65;
      this.bunifuCustomLabel8.Text = "VAT:";
      this.panel3.Controls.Add((Control) this.txt_ISA_Amt);
      this.panel3.Controls.Add((Control) this.ln_ISA_Amt);
      this.panel3.Location = new Point(133, 91);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(199, 27);
      this.panel3.TabIndex = 64;
      this.txt_ISA_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_ISA_Amt.BackColor = Color.Silver;
      this.txt_ISA_Amt.BorderStyle = BorderStyle.None;
      this.txt_ISA_Amt.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_ISA_Amt.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_ISA_Amt.Location = new Point(2, 5);
      this.txt_ISA_Amt.Name = "txt_ISA_Amt";
      this.txt_ISA_Amt.Size = new Size(196, 16);
      this.txt_ISA_Amt.TabIndex = 4;
      this.txt_ISA_Amt.TextChanged += new EventHandler(this.txt_ISA_Amt_TextChanged);
      this.txt_ISA_Amt.Leave += new EventHandler(this.txt_ISA_Amt_Leave);
      this.txt_ISA_Amt.MouseEnter += new EventHandler(this.txt_ISA_Amt_MouseEnter);
      this.txt_ISA_Amt.MouseLeave += new EventHandler(this.txt_ISA_Amt_MouseLeave);
      this.ln_ISA_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_ISA_Amt.BackColor = Color.Transparent;
      this.ln_ISA_Amt.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_ISA_Amt.LineThickness = 1;
      this.ln_ISA_Amt.Location = new Point(-1, 18);
      this.ln_ISA_Amt.Name = "ln_ISA_Amt";
      this.ln_ISA_Amt.Size = new Size(201, 10);
      this.ln_ISA_Amt.TabIndex = 0;
      this.ln_ISA_Amt.TabStop = false;
      this.ln_ISA_Amt.Transparency = (int) byte.MaxValue;
      this.ln_ISA_Amt.Vertical = false;
      this.bunifuCustomLabel7.AutoSize = true;
      this.bunifuCustomLabel7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel7.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel7.Location = new Point(65, 95);
      this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
      this.bunifuCustomLabel7.Size = new Size(60, 17);
      this.bunifuCustomLabel7.TabIndex = 63;
      this.bunifuCustomLabel7.Text = "Amount:";
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(160, 132);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(36, 17);
      this.bunifuCustomLabel6.TabIndex = 62;
      this.bunifuCustomLabel6.Text = "Paid";
      this.cb_ISA_Paid.BackColor = Color.FromArgb(132, 135, 140);
      this.cb_ISA_Paid.ChechedOffColor = Color.FromArgb(132, 135, 140);
      this.cb_ISA_Paid.Checked = false;
      this.cb_ISA_Paid.CheckedOnColor = Color.FromArgb(15, 91, 142);
      this.cb_ISA_Paid.ForeColor = Color.White;
      this.cb_ISA_Paid.Location = new Point(134, 132);
      this.cb_ISA_Paid.Name = "cb_ISA_Paid";
      this.cb_ISA_Paid.Size = new Size(20, 20);
      this.cb_ISA_Paid.TabIndex = 6;
      this.cb_ISA_Paid.OnChange += new EventHandler(this.cb_ISA_Paid_OnChange);
      this.panel2.Controls.Add((Control) this.txt_ISA_Desc);
      this.panel2.Controls.Add((Control) this.ln_ISA_Desc);
      this.panel2.Location = new Point(133, 58);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(534, 27);
      this.panel2.TabIndex = 60;
      this.txt_ISA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_ISA_Desc.BackColor = Color.Silver;
      this.txt_ISA_Desc.BorderStyle = BorderStyle.None;
      this.txt_ISA_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_ISA_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_ISA_Desc.Location = new Point(2, 5);
      this.txt_ISA_Desc.Name = "txt_ISA_Desc";
      this.txt_ISA_Desc.Size = new Size(531, 16);
      this.txt_ISA_Desc.TabIndex = 3;
      this.txt_ISA_Desc.Leave += new EventHandler(this.txt_ISA_Desc_Leave);
      this.txt_ISA_Desc.MouseEnter += new EventHandler(this.txt_ISA_Desc_MouseEnter);
      this.txt_ISA_Desc.MouseLeave += new EventHandler(this.txt_ISA_Desc_MouseLeave);
      this.ln_ISA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_ISA_Desc.BackColor = Color.Transparent;
      this.ln_ISA_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_ISA_Desc.LineThickness = 1;
      this.ln_ISA_Desc.Location = new Point(-1, 18);
      this.ln_ISA_Desc.Name = "ln_ISA_Desc";
      this.ln_ISA_Desc.Size = new Size(536, 10);
      this.ln_ISA_Desc.TabIndex = 0;
      this.ln_ISA_Desc.TabStop = false;
      this.ln_ISA_Desc.Transparency = (int) byte.MaxValue;
      this.ln_ISA_Desc.Vertical = false;
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(46, 62);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Description:";
      this.dtp_ISA_Date.BackColor = Color.Silver;
      this.dtp_ISA_Date.BorderRadius = 0;
      this.dtp_ISA_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_ISA_Date.Format = DateTimePickerFormat.Short;
      this.dtp_ISA_Date.FormatCustom = (string) null;
      this.dtp_ISA_Date.Location = new Point(428, 23);
      this.dtp_ISA_Date.Name = "dtp_ISA_Date";
      this.dtp_ISA_Date.Size = new Size(238, 25);
      this.dtp_ISA_Date.TabIndex = 2;
      this.dtp_ISA_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(381, 26);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(42, 17);
      this.bunifuCustomLabel2.TabIndex = 0;
      this.bunifuCustomLabel2.Text = "Date:";
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(18, 26);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(110, 17);
      this.bunifuCustomLabel1.TabIndex = 0;
      this.bunifuCustomLabel1.Text = "Invoice Number:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(708, 352);
      this.Controls.Add((Control) this.btn_ISA_Cancel);
      this.Controls.Add((Control) this.btn_ISA_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_ISA_Close);
      this.Controls.Add((Control) this.gb_OA_CDetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(708, 352);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(708, 352);
      this.Name = nameof (Inv_Send_Add);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Add Invoice Sent";
      this.Load += new EventHandler(this.Inv_Send_Add_Load);
      this.MouseDown += new MouseEventHandler(this.Inv_Send_Add_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Inv_Send_Add_MouseMove);
      this.MouseUp += new MouseEventHandler(this.Inv_Sent_Add_MouseUp);
      this.gb_OA_CDetails.ResumeLayout(false);
      this.gb_OA_CDetails.PerformLayout();
      this.gb_OA_ODetails.ResumeLayout(false);
      this.gb_OA_ODetails.PerformLayout();
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
