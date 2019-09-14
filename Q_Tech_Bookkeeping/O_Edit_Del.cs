// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.O_Edit_Del
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
  public class O_Edit_Del : Form
  {
    private bool isInter = false;
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private DataTable dt;
    private int SELECTED_ORDER;
    private string oldCONum;
    private Decimal pInv;
    private Decimal pRec;
    private Point lastLocation;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_OED_Close;
    private Button btn_OED_Cancel;
    private Button btn_OED_Done;
    private GroupBox gb_OA_ODetails;
    private Panel panel6;
    private TextBox txt_OED_QNum;
    private BunifuSeparator ln_OED_QNum;
    private Panel panel5;
    private TextBox txt_OED_PercRec;
    private BunifuSeparator ln_OED_PercRec;
    private Panel panel4;
    private TextBox txt_OED_PercInv;
    private BunifuSeparator ln_OED_PercInv;
    private Panel panel3;
    private TextBox txt_OED_Amt;
    private BunifuSeparator ln_OED_Amt;
    private Panel panel2;
    private TextBox txt_OED_Desc;
    private BunifuSeparator ln_OED_Desc;
    private Panel panel1;
    private TextBox txt_OED_CONum;
    private BunifuSeparator ln_OED_CONum;
    private BunifuCustomLabel bunifuCustomLabel8;
    private BunifuCustomLabel bunifuCustomLabel9;
    private BunifuCustomLabel bunifuCustomLabel7;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_OED_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;
    private GroupBox gb_OA_CDetails;
    private BunifuMaterialTextbox txt_OED_CName;
    private BunifuCustomLabel bunifuCustomLabel4;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuMaterialTextbox txt_OED_CCode;

    public O_Edit_Del()
    {
      this.InitializeComponent();
    }

    private void O_Edit_Del_Load(object sender, EventArgs e)
    {
      HomeOld owner = (HomeOld) this.Owner;
      if (owner.getCurPanel() == "pnl_L_Orders")
      {
        OrdersOld curForm = (OrdersOld) owner.getCurForm();
        this.dt = curForm.getOrders();
        this.SELECTED_ORDER = curForm.getSelectedOrder();
        this.txt_OED_CCode.Text = curForm.getCCode();
        this.txt_OED_CName.Text = curForm.getCName();
      }
      else
      {
        this.isInter = true;
        Int_OrdersOld curForm = (Int_OrdersOld) owner.getCurForm();
        this.dt = curForm.getOrders();
        this.SELECTED_ORDER = curForm.getSelectedOrder();
        this.txt_OED_CCode.Text = curForm.getCCode();
        this.txt_OED_CName.Text = curForm.getCName();
      }
      this.loadOrder();
      this.oldCONum = this.txt_OED_CONum.Text.Trim();
    }

    private void loadOrder()
    {
      this.txt_OED_CONum.Text = this.dt.Rows[this.SELECTED_ORDER]["Client_Order_Number"].ToString().Trim();
      this.dtp_OED_Date.Value = !(this.dt.Rows[this.SELECTED_ORDER]["Date"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(this.dt.Rows[this.SELECTED_ORDER]["Date"].ToString());
      if (this.isInter)
      {
        if (this.dt.Rows[this.SELECTED_ORDER]["Amount"].ToString() != string.Empty)
          this.txt_OED_Amt.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_ORDER]["Amount"].ToString().Trim()).ToString("c", (IFormatProvider) CultureInfo.GetCultureInfo("en-US"));
        else
          this.txt_OED_Amt.Text = "$0.00";
      }
      else if (this.dt.Rows[this.SELECTED_ORDER]["Amount"].ToString() != string.Empty)
        this.txt_OED_Amt.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_ORDER]["Amount"].ToString().Trim()).ToString("c");
      else
        this.txt_OED_Amt.Text = "R0.00";
      this.txt_OED_Amt.SelectionStart = this.txt_OED_Amt.Text.Length;
      this.txt_OED_Desc.Text = this.dt.Rows[this.SELECTED_ORDER]["Description"].ToString().Trim();
      this.txt_OED_PercInv.Text = (!(this.dt.Rows[this.SELECTED_ORDER]["Percentage_Invoiced"].ToString() != string.Empty) ? 0.0 : Convert.ToDouble(this.dt.Rows[this.SELECTED_ORDER]["Percentage_Invoiced"].ToString().Trim())).ToString("p0");
      this.txt_OED_PercRec.Text = (!(this.dt.Rows[this.SELECTED_ORDER]["Percentage_Received"].ToString() != string.Empty) ? 0.0 : Convert.ToDouble(this.dt.Rows[this.SELECTED_ORDER]["Percentage_Received"].ToString().Trim())).ToString("p0");
      this.txt_OED_QNum.Text = this.dt.Rows[this.SELECTED_ORDER]["Quote_Number"].ToString().Trim();
    }

    private void txt_OED_Amt_TextChanged(object sender, EventArgs e)
    {
      if (this.isInter)
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_OED_Amt.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
        {
          Decimal num = result / new Decimal(100);
          this.txt_OED_Amt.TextChanged -= new EventHandler(this.txt_OED_Amt_TextChanged);
          this.txt_OED_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (object) num);
          this.txt_OED_Amt.TextChanged += new EventHandler(this.txt_OED_Amt_TextChanged);
          this.txt_OED_Amt.Select(this.txt_OED_Amt.Text.Length, 0);
        }
        if (this.TextisValid(this.txt_OED_Amt.Text))
          return;
        this.txt_OED_Amt.Text = "$0.00";
        this.txt_OED_Amt.Select(this.txt_OED_Amt.Text.Length, 0);
      }
      else
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_OED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
        {
          Decimal num = result / new Decimal(100);
          this.txt_OED_Amt.TextChanged -= new EventHandler(this.txt_OED_Amt_TextChanged);
          this.txt_OED_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
          this.txt_OED_Amt.TextChanged += new EventHandler(this.txt_OED_Amt_TextChanged);
          this.txt_OED_Amt.Select(this.txt_OED_Amt.Text.Length, 0);
        }
        if (!this.TextisValid(this.txt_OED_Amt.Text))
        {
          this.txt_OED_Amt.Text = "R0.00";
          this.txt_OED_Amt.Select(this.txt_OED_Amt.Text.Length, 0);
        }
      }
    }

    private bool TextisValid(string text)
    {
      return new Regex("[^0-9]").IsMatch(text);
    }

    private void txt_OED_Perc_Rec_Validating(object sender, CancelEventArgs e)
    {
      double result;
      if (double.TryParse(this.txt_OED_PercRec.Text, out result) && Convert.ToDouble(this.txt_OED_PercRec.Text) >= 0.0 && Convert.ToDouble(this.txt_OED_PercRec.Text) <= 100.0)
      {
        this.pRec = Convert.ToDecimal(this.txt_OED_PercRec.Text.ToString());
        this.txt_OED_PercRec.Text = result.ToString() + "%";
      }
      else if (this.txt_OED_PercRec.Text == string.Empty)
      {
        this.txt_OED_PercRec.Text = "0%";
      }
      else
      {
        e.Cancel = true;
        int num = (int) MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void txt_OED_Perc_Inv_Validating(object sender, CancelEventArgs e)
    {
      double result;
      if (double.TryParse(this.txt_OED_PercInv.Text, out result) && Convert.ToDouble(this.txt_OED_PercInv.Text) >= 0.0 && Convert.ToDouble(this.txt_OED_PercInv.Text) <= 100.0)
      {
        this.pInv = Convert.ToDecimal(this.txt_OED_PercInv.Text.ToString());
        this.txt_OED_PercInv.Text = result.ToString() + "%";
      }
      else if (this.txt_OED_PercInv.Text == string.Empty)
      {
        this.txt_OED_PercInv.Text = "0%";
      }
      else
      {
        e.Cancel = true;
        int num = (int) MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void btn_OED_Done_Click(object sender, EventArgs e)
    {
      if (this.txt_OED_CONum.Text != string.Empty)
      {
        if (MessageBox.Show("Are you sure you want to update order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        if (this.txt_OED_CONum.Text.Trim() == this.oldCONum)
        {
          using (SqlConnection dbConnection = DBUtils.GetDBConnection())
          {
            dbConnection.Open();
            try
            {
              using (SqlCommand sqlCommand = new SqlCommand("UPDATE Orders_Received SET Date = @Date, Description = @Desc, Amount = @Amt, Percentage_Invoiced = @PercInv, Percentage_Received = @PercRec, Quote_Number = @QNum WHERE Client_Order_Number = @CONum", dbConnection))
              {
                Decimal num1 = !this.isInter ? (!this.txt_OED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_OED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_OED_Amt.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2))) : (!this.txt_OED_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_OED_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_OED_Amt.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2)));
                sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_OED_Date.Value);
                sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_OED_Desc.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Amt", (object) num1);
                sqlCommand.Parameters.AddWithValue("@PercInv", (object) this.pInv);
                sqlCommand.Parameters.AddWithValue("@PercRec", (object) this.pRec);
                sqlCommand.Parameters.AddWithValue("@QNum", (object) this.txt_OED_QNum.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@CONum", (object) this.oldCONum);
                sqlCommand.ExecuteNonQuery();
                int num2 = (int) MessageBox.Show("Order successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
              }
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
          }
        }
        else if (this.txt_OED_CONum.Text.Trim() != this.oldCONum)
        {
          using (SqlConnection dbConnection = DBUtils.GetDBConnection())
          {
            dbConnection.Open();
            try
            {
              using (SqlCommand sqlCommand = new SqlCommand("UPDATE Orders_Received SET Client_Order_Number = @CONum, Date = @Date, Description = @Desc, Amount = @Amt, Percentage_Invoiced = @PercInv, Percentage_Received = @PercRec, Quote_Number = @QNum WHERE Client_Order_Number = @oldCONum", dbConnection))
              {
                Decimal num1 = !this.isInter ? (!this.txt_OED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_OED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_OED_Amt.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2))) : (!this.txt_OED_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_OED_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_OED_Amt.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2)));
                sqlCommand.Parameters.AddWithValue("@CONum", (object) this.txt_OED_CONum.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_OED_Date.Value);
                sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_OED_Desc.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Amt", (object) num1);
                sqlCommand.Parameters.AddWithValue("@PercInv", (object) this.pInv);
                sqlCommand.Parameters.AddWithValue("@PercRec", (object) this.pRec);
                sqlCommand.Parameters.AddWithValue("@QNum", (object) this.txt_OED_QNum.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@oldCONum", (object) this.oldCONum);
                sqlCommand.ExecuteNonQuery();
                int num2 = (int) MessageBox.Show("Order successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
              }
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
          }
        }
      }
      else
      {
        int num3 = (int) MessageBox.Show("Please enter a Client Order Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btn_OED_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_OED_Perc_Inv_Enter(object sender, EventArgs e)
    {
      this.txt_OED_PercInv.Clear();
    }

    private void txt_OED_Perc_Rec_Enter(object sender, EventArgs e)
    {
      this.txt_OED_PercRec.Clear();
    }

    private void btn_OED_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_OED_CONum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OED_CONum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OED_CONum_Leave(object sender, EventArgs e)
    {
      this.ln_OED_CONum.LineColor = Color.Gray;
    }

    private void txt_OED_CONum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OED_CONum.Focused)
        return;
      this.ln_OED_CONum.LineColor = Color.Gray;
    }

    private void txt_OED_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_OED_Desc.LineColor = Color.Gray;
    }

    private void txt_OED_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OED_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OED_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OED_Desc.Focused)
        return;
      this.ln_OED_Desc.LineColor = Color.Gray;
    }

    private void txt_OED_Amt_Leave(object sender, EventArgs e)
    {
      this.ln_OED_Amt.LineColor = Color.Gray;
    }

    private void txt_OED_Amt_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OED_Amt.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OED_Amt_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OED_Amt.Focused)
        return;
      this.ln_OED_Amt.LineColor = Color.Gray;
    }

    private void txt_OED_PercInv_Leave(object sender, EventArgs e)
    {
      this.ln_OED_PercInv.LineColor = Color.Gray;
    }

    private void txt_OED_PercInv_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OED_PercInv.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OED_PercInv_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OED_PercInv.Focused)
        return;
      this.ln_OED_PercInv.LineColor = Color.Gray;
    }

    private void txt_OED_PercRec_Leave(object sender, EventArgs e)
    {
      this.ln_OED_PercRec.LineColor = Color.Gray;
    }

    private void txt_OED_PercRec_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OED_PercRec.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OED_PercRec_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OED_PercRec.Focused)
        return;
      this.ln_OED_PercRec.LineColor = Color.Gray;
    }

    private void txt_OED_QNum_Leave(object sender, EventArgs e)
    {
      this.ln_OED_QNum.LineColor = Color.Gray;
    }

    private void txt_OED_QNum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OED_QNum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OED_QNum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OED_QNum.Focused)
        return;
      this.ln_OED_QNum.LineColor = Color.Gray;
    }

    private void btn_OED_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_OED_Close.Image = (Image) Resources.close_white;
    }

    private void btn_OED_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_OED_Close.Image = (Image) Resources.close_black;
    }

    private void btn_OED_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_OED_Done.ForeColor = Color.White;
    }

    private void btn_OED_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_OED_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_OED_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_OED_Cancel.ForeColor = Color.White;
    }

    private void btn_OED_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_OED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_OED_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_OED_CName_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void O_Edit_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void O_Edit_MouseMove(object sender, MouseEventArgs e)
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

    private void O_Edit_MouseUp(object sender, MouseEventArgs e)
    {
      this.mouseDown = false;
    }

    private void txt_OED_PercInv_Enter(object sender, EventArgs e)
    {
      this.txt_OED_PercInv.Clear();
    }

    private void txt_OED_PercRec_Enter(object sender, EventArgs e)
    {
      this.txt_OED_PercRec.Clear();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (O_Edit_Del));
      this.bunifuCustomLabel10 = new BunifuCustomLabel();
      this.btn_OED_Close = new Button();
      this.btn_OED_Cancel = new Button();
      this.btn_OED_Done = new Button();
      this.gb_OA_ODetails = new GroupBox();
      this.panel6 = new Panel();
      this.txt_OED_QNum = new TextBox();
      this.ln_OED_QNum = new BunifuSeparator();
      this.panel5 = new Panel();
      this.txt_OED_PercRec = new TextBox();
      this.ln_OED_PercRec = new BunifuSeparator();
      this.panel4 = new Panel();
      this.txt_OED_PercInv = new TextBox();
      this.ln_OED_PercInv = new BunifuSeparator();
      this.panel3 = new Panel();
      this.txt_OED_Amt = new TextBox();
      this.ln_OED_Amt = new BunifuSeparator();
      this.panel2 = new Panel();
      this.txt_OED_Desc = new TextBox();
      this.ln_OED_Desc = new BunifuSeparator();
      this.panel1 = new Panel();
      this.txt_OED_CONum = new TextBox();
      this.ln_OED_CONum = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.bunifuCustomLabel9 = new BunifuCustomLabel();
      this.bunifuCustomLabel7 = new BunifuCustomLabel();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_OED_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.gb_OA_CDetails = new GroupBox();
      this.txt_OED_CName = new BunifuMaterialTextbox();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.txt_OED_CCode = new BunifuMaterialTextbox();
      this.gb_OA_ODetails.SuspendLayout();
      this.panel6.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.gb_OA_CDetails.SuspendLayout();
      this.SuspendLayout();
      this.bunifuCustomLabel10.AutoSize = true;
      this.bunifuCustomLabel10.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel10.ForeColor = Color.FromArgb(15, 91, 142);
      this.bunifuCustomLabel10.Location = new Point(323, 7);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new Size(119, 22);
      this.bunifuCustomLabel10.TabIndex = 0;
      this.bunifuCustomLabel10.Text = "Update Order";
      this.btn_OED_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_OED_Close.BackColor = Color.Silver;
      this.btn_OED_Close.FlatAppearance.BorderSize = 0;
      this.btn_OED_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_OED_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_OED_Close.FlatStyle = FlatStyle.Flat;
      this.btn_OED_Close.Image = (Image) Resources.close_black;
      this.btn_OED_Close.Location = new Point(724, 6);
      this.btn_OED_Close.Name = "btn_OED_Close";
      this.btn_OED_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_OED_Close.Size = new Size(31, 29);
      this.btn_OED_Close.TabIndex = 56;
      this.btn_OED_Close.TabStop = false;
      this.btn_OED_Close.UseVisualStyleBackColor = false;
      this.btn_OED_Close.Click += new EventHandler(this.btn_OED_Close_Click);
      this.btn_OED_Close.MouseEnter += new EventHandler(this.btn_OED_Close_MouseEnter);
      this.btn_OED_Close.MouseLeave += new EventHandler(this.btn_OED_Close_MouseLeave);
      this.btn_OED_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_OED_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_OED_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_OED_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_OED_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_OED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_OED_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_OED_Cancel.Location = new Point(633, 291);
      this.btn_OED_Cancel.Name = "btn_OED_Cancel";
      this.btn_OED_Cancel.Size = new Size(114, 40);
      this.btn_OED_Cancel.TabIndex = 9;
      this.btn_OED_Cancel.Text = "Cancel";
      this.btn_OED_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_OED_Cancel.UseVisualStyleBackColor = true;
      this.btn_OED_Cancel.Click += new EventHandler(this.btn_OED_Cancel_Click);
      this.btn_OED_Cancel.MouseEnter += new EventHandler(this.btn_OED_Cancel_MouseEnter);
      this.btn_OED_Cancel.MouseLeave += new EventHandler(this.btn_OED_Cancel_MouseLeave);
      this.btn_OED_Done.FlatAppearance.BorderSize = 0;
      this.btn_OED_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_OED_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_OED_Done.FlatStyle = FlatStyle.Flat;
      this.btn_OED_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_OED_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_OED_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_OED_Done.Location = new Point(513, 291);
      this.btn_OED_Done.Name = "btn_OED_Done";
      this.btn_OED_Done.Size = new Size(114, 40);
      this.btn_OED_Done.TabIndex = 8;
      this.btn_OED_Done.Text = "Done";
      this.btn_OED_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_OED_Done.UseVisualStyleBackColor = true;
      this.btn_OED_Done.Click += new EventHandler(this.btn_OED_Done_Click);
      this.btn_OED_Done.MouseEnter += new EventHandler(this.btn_OED_Done_MouseEnter);
      this.btn_OED_Done.MouseLeave += new EventHandler(this.btn_OED_Done_MouseLeave);
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
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_OED_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(12, 120);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(735, 165);
      this.gb_OA_ODetails.TabIndex = 52;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Order Details";
      this.panel6.Controls.Add((Control) this.txt_OED_QNum);
      this.panel6.Controls.Add((Control) this.ln_OED_QNum);
      this.panel6.Location = new Point(165, 118);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(156, 26);
      this.panel6.TabIndex = 64;
      this.txt_OED_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OED_QNum.BackColor = Color.Silver;
      this.txt_OED_QNum.BorderStyle = BorderStyle.None;
      this.txt_OED_QNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_QNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_QNum.Location = new Point(2, 7);
      this.txt_OED_QNum.Name = "txt_OED_QNum";
      this.txt_OED_QNum.Size = new Size(153, 16);
      this.txt_OED_QNum.TabIndex = 7;
      this.txt_OED_QNum.Leave += new EventHandler(this.txt_OED_QNum_Leave);
      this.txt_OED_QNum.MouseEnter += new EventHandler(this.txt_OED_QNum_MouseEnter);
      this.txt_OED_QNum.MouseLeave += new EventHandler(this.txt_OED_QNum_MouseLeave);
      this.ln_OED_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OED_QNum.BackColor = Color.Transparent;
      this.ln_OED_QNum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OED_QNum.LineThickness = 1;
      this.ln_OED_QNum.Location = new Point(-1, 18);
      this.ln_OED_QNum.Name = "ln_OED_QNum";
      this.ln_OED_QNum.Size = new Size(158, 10);
      this.ln_OED_QNum.TabIndex = 52;
      this.ln_OED_QNum.TabStop = false;
      this.ln_OED_QNum.Transparency = (int) byte.MaxValue;
      this.ln_OED_QNum.Vertical = false;
      this.panel5.Controls.Add((Control) this.txt_OED_PercRec);
      this.panel5.Controls.Add((Control) this.ln_OED_PercRec);
      this.panel5.Location = new Point(650, 85);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(61, 26);
      this.panel5.TabIndex = 63;
      this.txt_OED_PercRec.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OED_PercRec.BackColor = Color.Silver;
      this.txt_OED_PercRec.BorderStyle = BorderStyle.None;
      this.txt_OED_PercRec.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_PercRec.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_PercRec.Location = new Point(3, 7);
      this.txt_OED_PercRec.Name = "txt_OED_PercRec";
      this.txt_OED_PercRec.Size = new Size(58, 16);
      this.txt_OED_PercRec.TabIndex = 6;
      this.txt_OED_PercRec.Enter += new EventHandler(this.txt_OED_PercRec_Enter);
      this.txt_OED_PercRec.Leave += new EventHandler(this.txt_OED_PercRec_Leave);
      this.txt_OED_PercRec.MouseEnter += new EventHandler(this.txt_OED_PercRec_MouseEnter);
      this.txt_OED_PercRec.MouseLeave += new EventHandler(this.txt_OED_PercRec_MouseLeave);
      this.txt_OED_PercRec.Validating += new CancelEventHandler(this.txt_OED_Perc_Rec_Validating);
      this.ln_OED_PercRec.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OED_PercRec.BackColor = Color.Transparent;
      this.ln_OED_PercRec.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OED_PercRec.LineThickness = 1;
      this.ln_OED_PercRec.Location = new Point(-1, 18);
      this.ln_OED_PercRec.Name = "ln_OED_PercRec";
      this.ln_OED_PercRec.Size = new Size(63, 10);
      this.ln_OED_PercRec.TabIndex = 52;
      this.ln_OED_PercRec.TabStop = false;
      this.ln_OED_PercRec.Transparency = (int) byte.MaxValue;
      this.ln_OED_PercRec.Vertical = false;
      this.panel4.Controls.Add((Control) this.txt_OED_PercInv);
      this.panel4.Controls.Add((Control) this.ln_OED_PercInv);
      this.panel4.Location = new Point(436, 85);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(61, 26);
      this.panel4.TabIndex = 62;
      this.txt_OED_PercInv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OED_PercInv.BackColor = Color.Silver;
      this.txt_OED_PercInv.BorderStyle = BorderStyle.None;
      this.txt_OED_PercInv.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_PercInv.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_PercInv.Location = new Point(3, 7);
      this.txt_OED_PercInv.Name = "txt_OED_PercInv";
      this.txt_OED_PercInv.Size = new Size(58, 16);
      this.txt_OED_PercInv.TabIndex = 5;
      this.txt_OED_PercInv.Enter += new EventHandler(this.txt_OED_PercInv_Enter);
      this.txt_OED_PercInv.Leave += new EventHandler(this.txt_OED_PercInv_Leave);
      this.txt_OED_PercInv.MouseEnter += new EventHandler(this.txt_OED_PercInv_MouseEnter);
      this.txt_OED_PercInv.MouseLeave += new EventHandler(this.txt_OED_PercInv_MouseLeave);
      this.txt_OED_PercInv.Validating += new CancelEventHandler(this.txt_OED_Perc_Inv_Validating);
      this.ln_OED_PercInv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OED_PercInv.BackColor = Color.Transparent;
      this.ln_OED_PercInv.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OED_PercInv.LineThickness = 1;
      this.ln_OED_PercInv.Location = new Point(-1, 18);
      this.ln_OED_PercInv.Name = "ln_OED_PercInv";
      this.ln_OED_PercInv.Size = new Size(63, 10);
      this.ln_OED_PercInv.TabIndex = 52;
      this.ln_OED_PercInv.TabStop = false;
      this.ln_OED_PercInv.Transparency = (int) byte.MaxValue;
      this.ln_OED_PercInv.Vertical = false;
      this.panel3.Controls.Add((Control) this.txt_OED_Amt);
      this.panel3.Controls.Add((Control) this.ln_OED_Amt);
      this.panel3.Location = new Point(165, 85);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(118, 26);
      this.panel3.TabIndex = 61;
      this.txt_OED_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OED_Amt.BackColor = Color.Silver;
      this.txt_OED_Amt.BorderStyle = BorderStyle.None;
      this.txt_OED_Amt.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_Amt.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_Amt.Location = new Point(2, 5);
      this.txt_OED_Amt.Name = "txt_OED_Amt";
      this.txt_OED_Amt.Size = new Size(115, 16);
      this.txt_OED_Amt.TabIndex = 4;
      this.txt_OED_Amt.TextChanged += new EventHandler(this.txt_OED_Amt_TextChanged);
      this.txt_OED_Amt.Leave += new EventHandler(this.txt_OED_Amt_Leave);
      this.txt_OED_Amt.MouseEnter += new EventHandler(this.txt_OED_Amt_MouseEnter);
      this.txt_OED_Amt.MouseLeave += new EventHandler(this.txt_OED_Amt_MouseLeave);
      this.ln_OED_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OED_Amt.BackColor = Color.Transparent;
      this.ln_OED_Amt.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OED_Amt.LineThickness = 1;
      this.ln_OED_Amt.Location = new Point(-1, 18);
      this.ln_OED_Amt.Name = "ln_OED_Amt";
      this.ln_OED_Amt.Size = new Size(120, 10);
      this.ln_OED_Amt.TabIndex = 52;
      this.ln_OED_Amt.TabStop = false;
      this.ln_OED_Amt.Transparency = (int) byte.MaxValue;
      this.ln_OED_Amt.Vertical = false;
      this.panel2.Controls.Add((Control) this.txt_OED_Desc);
      this.panel2.Controls.Add((Control) this.ln_OED_Desc);
      this.panel2.Location = new Point(165, 53);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(547, 27);
      this.panel2.TabIndex = 60;
      this.txt_OED_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OED_Desc.BackColor = Color.Silver;
      this.txt_OED_Desc.BorderStyle = BorderStyle.None;
      this.txt_OED_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_Desc.Location = new Point(2, 6);
      this.txt_OED_Desc.Name = "txt_OED_Desc";
      this.txt_OED_Desc.Size = new Size(544, 16);
      this.txt_OED_Desc.TabIndex = 3;
      this.txt_OED_Desc.Leave += new EventHandler(this.txt_OED_Desc_Leave);
      this.txt_OED_Desc.MouseEnter += new EventHandler(this.txt_OED_Desc_MouseEnter);
      this.txt_OED_Desc.MouseLeave += new EventHandler(this.txt_OED_Desc_MouseLeave);
      this.ln_OED_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OED_Desc.BackColor = Color.Transparent;
      this.ln_OED_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OED_Desc.LineThickness = 1;
      this.ln_OED_Desc.Location = new Point(-1, 18);
      this.ln_OED_Desc.Name = "ln_OED_Desc";
      this.ln_OED_Desc.Size = new Size(549, 10);
      this.ln_OED_Desc.TabIndex = 0;
      this.ln_OED_Desc.TabStop = false;
      this.ln_OED_Desc.Transparency = (int) byte.MaxValue;
      this.ln_OED_Desc.Vertical = false;
      this.panel1.Controls.Add((Control) this.txt_OED_CONum);
      this.panel1.Controls.Add((Control) this.ln_OED_CONum);
      this.panel1.Location = new Point(165, 19);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(210, 27);
      this.panel1.TabIndex = 52;
      this.txt_OED_CONum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OED_CONum.BackColor = Color.Silver;
      this.txt_OED_CONum.BorderStyle = BorderStyle.None;
      this.txt_OED_CONum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_CONum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_CONum.Location = new Point(2, 6);
      this.txt_OED_CONum.Name = "txt_OED_CONum";
      this.txt_OED_CONum.Size = new Size(208, 16);
      this.txt_OED_CONum.TabIndex = 1;
      this.txt_OED_CONum.Leave += new EventHandler(this.txt_OED_CONum_Leave);
      this.txt_OED_CONum.MouseEnter += new EventHandler(this.txt_OED_CONum_MouseEnter);
      this.txt_OED_CONum.MouseLeave += new EventHandler(this.txt_OED_CONum_MouseLeave);
      this.ln_OED_CONum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OED_CONum.BackColor = Color.Transparent;
      this.ln_OED_CONum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OED_CONum.LineThickness = 1;
      this.ln_OED_CONum.Location = new Point(-1, 18);
      this.ln_OED_CONum.Name = "ln_OED_CONum";
      this.ln_OED_CONum.Size = new Size(212, 10);
      this.ln_OED_CONum.TabIndex = 52;
      this.ln_OED_CONum.TabStop = false;
      this.ln_OED_CONum.Transparency = (int) byte.MaxValue;
      this.ln_OED_CONum.Vertical = false;
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel8.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel8.Location = new Point(54, 123);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new Size(105, 17);
      this.bunifuCustomLabel8.TabIndex = 0;
      this.bunifuCustomLabel8.Text = "Quote Number:";
      this.bunifuCustomLabel9.AutoSize = true;
      this.bunifuCustomLabel9.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel9.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel9.Location = new Point(563, 89);
      this.bunifuCustomLabel9.Name = "bunifuCustomLabel9";
      this.bunifuCustomLabel9.Size = new Size(87, 17);
      this.bunifuCustomLabel9.TabIndex = 0;
      this.bunifuCustomLabel9.Text = "% Recieved:";
      this.bunifuCustomLabel7.AutoSize = true;
      this.bunifuCustomLabel7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel7.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel7.Location = new Point(352, 89);
      this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
      this.bunifuCustomLabel7.Size = new Size(80, 17);
      this.bunifuCustomLabel7.TabIndex = 0;
      this.bunifuCustomLabel7.Text = "% Invoiced:";
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(97, 89);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(60, 17);
      this.bunifuCustomLabel6.TabIndex = 0;
      this.bunifuCustomLabel6.Text = "Amount:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(78, 58);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Description:";
      this.dtp_OED_Date.BackColor = Color.Silver;
      this.dtp_OED_Date.BorderRadius = 0;
      this.dtp_OED_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_OED_Date.Format = DateTimePickerFormat.Short;
      this.dtp_OED_Date.FormatCustom = (string) null;
      this.dtp_OED_Date.Location = new Point(474, 25);
      this.dtp_OED_Date.Name = "dtp_OED_Date";
      this.dtp_OED_Date.Size = new Size(238, 25);
      this.dtp_OED_Date.TabIndex = 2;
      this.dtp_OED_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(427, 26);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(42, 17);
      this.bunifuCustomLabel2.TabIndex = 0;
      this.bunifuCustomLabel2.Text = "Date:";
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(18, 24);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(142, 17);
      this.bunifuCustomLabel1.TabIndex = 0;
      this.bunifuCustomLabel1.Text = "Client Order Number:";
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_OED_CName);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel4);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_OED_CCode);
      this.gb_OA_CDetails.FlatStyle = FlatStyle.Flat;
      this.gb_OA_CDetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_CDetails.Location = new Point(12, 42);
      this.gb_OA_CDetails.Name = "gb_OA_CDetails";
      this.gb_OA_CDetails.Size = new Size(735, 59);
      this.gb_OA_CDetails.TabIndex = 53;
      this.gb_OA_CDetails.TabStop = false;
      this.gb_OA_CDetails.Text = "Client Details";
      this.txt_OED_CName.Cursor = Cursors.IBeam;
      this.txt_OED_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_CName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_CName.HintForeColor = Color.Empty;
      this.txt_OED_CName.HintText = "";
      this.txt_OED_CName.isPassword = false;
      this.txt_OED_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_OED_CName.LineIdleColor = Color.Gray;
      this.txt_OED_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_OED_CName.LineThickness = 1;
      this.txt_OED_CName.Location = new Point(489, 15);
      this.txt_OED_CName.Margin = new Padding(4);
      this.txt_OED_CName.Name = "txt_OED_CName";
      this.txt_OED_CName.Size = new Size(223, 30);
      this.txt_OED_CName.TabIndex = 46;
      this.txt_OED_CName.TabStop = false;
      this.txt_OED_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_OED_CName.KeyDown += new KeyEventHandler(this.txt_OED_CName_KeyDown);
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
      this.bunifuCustomLabel3.Location = new Point(395, 25);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(88, 17);
      this.bunifuCustomLabel3.TabIndex = 0;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.txt_OED_CCode.Cursor = Cursors.IBeam;
      this.txt_OED_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OED_CCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OED_CCode.HintForeColor = Color.Empty;
      this.txt_OED_CCode.HintText = "";
      this.txt_OED_CCode.isPassword = false;
      this.txt_OED_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_OED_CCode.LineIdleColor = Color.Gray;
      this.txt_OED_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_OED_CCode.LineThickness = 1;
      this.txt_OED_CCode.Location = new Point(108, 15);
      this.txt_OED_CCode.Margin = new Padding(4);
      this.txt_OED_CCode.Name = "txt_OED_CCode";
      this.txt_OED_CCode.Size = new Size(223, 30);
      this.txt_OED_CCode.TabIndex = 45;
      this.txt_OED_CCode.TabStop = false;
      this.txt_OED_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_OED_CCode.KeyDown += new KeyEventHandler(this.txt_OED_CCode_KeyDown);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(758, 343);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_OED_Close);
      this.Controls.Add((Control) this.btn_OED_Cancel);
      this.Controls.Add((Control) this.btn_OED_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.Controls.Add((Control) this.gb_OA_CDetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(758, 343);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(758, 343);
      this.Name = nameof (O_Edit_Del);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Order Edit/Delete";
      this.Load += new EventHandler(this.O_Edit_Del_Load);
      this.MouseDown += new MouseEventHandler(this.O_Edit_MouseDown);
      this.MouseMove += new MouseEventHandler(this.O_Edit_MouseMove);
      this.MouseUp += new MouseEventHandler(this.O_Edit_MouseUp);
      this.gb_OA_ODetails.ResumeLayout(false);
      this.gb_OA_ODetails.PerformLayout();
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
      this.gb_OA_CDetails.ResumeLayout(false);
      this.gb_OA_CDetails.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
