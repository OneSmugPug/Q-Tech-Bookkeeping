// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Inv_Rec_Edit_Del
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
  public class Inv_Rec_Edit_Del : Form
  {
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private DataTable dt;
    private int SELECTED_INVOICE;
    private string oldINum;
    private Point lastLocation;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_IRED_Close;
    private Button btn_IRED_Cancel;
    private Button btn_IRED_Done;
    private GroupBox gb_OA_ODetails;
    private Panel panel5;
    private TextBox txt_IRED_SuppName;
    private BunifuSeparator ln_IRED_SuppName;
    private BunifuCustomLabel bunifuCustomLabel3;
    private Panel panel1;
    private TextBox txt_IRED_InvNum;
    private BunifuSeparator ln_IRED_InvNum;
    private Panel panel4;
    private TextBox txt_IRED_VAT;
    private BunifuSeparator ln_IRED_VAT;
    private BunifuCustomLabel bunifuCustomLabel8;
    private Panel panel3;
    private TextBox txt_IRED_Amt;
    private BunifuSeparator ln_IRED_Amt;
    private BunifuCustomLabel bunifuCustomLabel7;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCheckbox cb_IRED_Paid;
    private Panel panel2;
    private TextBox txt_IRED_Desc;
    private BunifuSeparator ln_IRED_Desc;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_IRED_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;

    public Inv_Rec_Edit_Del()
    {
      this.InitializeComponent();
    }

    private void Inv_Rec_Edit_Del_Load(object sender, EventArgs e)
    {
      this.txt_IRED_SuppName.Focus();
      Inv_Rec curForm = (Inv_Rec) ((Home) this.Owner).getCurForm();
      this.dt = curForm.getInvRec();
      this.SELECTED_INVOICE = curForm.getSelectedInv();
      this.loadInvRec();
      this.oldINum = this.txt_IRED_InvNum.Text.Trim();
    }

    private void loadInvRec()
    {
      this.txt_IRED_SuppName.Text = this.dt.Rows[this.SELECTED_INVOICE]["Supplier"].ToString().Trim();
      this.txt_IRED_InvNum.Text = this.dt.Rows[this.SELECTED_INVOICE]["Invoice_Number"].ToString().Trim();
      this.dtp_IRED_Date.Value = !(this.dt.Rows[this.SELECTED_INVOICE]["Date"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(this.dt.Rows[this.SELECTED_INVOICE]["Date"].ToString());
      this.txt_IRED_Desc.Text = this.dt.Rows[this.SELECTED_INVOICE]["Description"].ToString().Trim();
      if (this.dt.Rows[this.SELECTED_INVOICE]["Total_Amount"].ToString() != string.Empty)
        this.txt_IRED_Amt.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_INVOICE]["Total_Amount"].ToString().Trim()).ToString("C");
      else
        this.txt_IRED_Amt.Text = "R0.00";
      if (this.dt.Rows[this.SELECTED_INVOICE]["VAT"].ToString() != string.Empty)
        this.txt_IRED_VAT.Text = Convert.ToDecimal(this.dt.Rows[this.SELECTED_INVOICE]["VAT"].ToString().Trim()).ToString("C");
      else
        this.txt_IRED_VAT.Text = "R0.00";
      if (this.dt.Rows[this.SELECTED_INVOICE]["Paid"].ToString() == "Yes")
        this.cb_IRED_Paid.Checked = true;
      else
        this.cb_IRED_Paid.Checked = false;
    }

    private void txt_IRED_Amt_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_IRED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_IRED_Amt.TextChanged -= new EventHandler(this.txt_IRED_Amt_TextChanged);
        this.txt_IRED_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
        this.txt_IRED_Amt.TextChanged += new EventHandler(this.txt_IRED_Amt_TextChanged);
        this.txt_IRED_Amt.Select(this.txt_IRED_Amt.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_IRED_Amt.Text))
        return;
      this.txt_IRED_Amt.Text = "R0.00";
      this.txt_IRED_Amt.Select(this.txt_IRED_Amt.Text.Length, 0);
    }

    private bool TextisValid(string text)
    {
      return new Regex("[^0-9]").IsMatch(text);
    }

    private void txt_IRED_VAT_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_IRED_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_IRED_VAT.TextChanged -= new EventHandler(this.txt_IRED_VAT_TextChanged);
        this.txt_IRED_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
        this.txt_IRED_VAT.TextChanged += new EventHandler(this.txt_IRED_VAT_TextChanged);
        this.txt_IRED_VAT.Select(this.txt_IRED_VAT.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_IRED_VAT.Text))
        return;
      this.txt_IRED_VAT.Text = "R0.00";
      this.txt_IRED_VAT.Select(this.txt_IRED_VAT.Text.Length, 0);
    }

    private void btn_IRED_Done_Click(object sender, EventArgs e)
    {
      if (this.txt_IRED_InvNum.Text != string.Empty)
      {
        if (MessageBox.Show("Are you sure you want to update invoice?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        if (this.txt_IRED_InvNum.Text == this.oldINum)
        {
          using (SqlConnection dbConnection = DBUtils.GetDBConnection())
          {
            dbConnection.Open();
            try
            {
              using (SqlCommand sqlCommand = new SqlCommand("UPDATE Invoices_Received SET Date = @Date, Supplier = @Supp, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid WHERE Invoice_Number = @INum", dbConnection))
              {
                Decimal num1 = !this.txt_IRED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_IRED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_IRED_Amt.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
                Decimal num2 = !this.txt_IRED_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_IRED_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_IRED_VAT.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
                sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_IRED_Date.Value);
                sqlCommand.Parameters.AddWithValue("@Supp", (object) this.txt_IRED_SuppName.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_IRED_Desc.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Amt", (object) num1);
                sqlCommand.Parameters.AddWithValue("@VAT", (object) num2);
                if (this.cb_IRED_Paid.Checked)
                  sqlCommand.Parameters.AddWithValue("@Paid", (object) "Yes");
                else
                  sqlCommand.Parameters.AddWithValue("@Paid", (object) "No");
                sqlCommand.Parameters.AddWithValue("@INum", (object) this.txt_IRED_InvNum.Text.Trim());
                sqlCommand.ExecuteNonQuery();
                int num3 = (int) MessageBox.Show("Invoice successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
              }
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
          }
        }
        else if (this.txt_IRED_InvNum.Text != this.oldINum)
        {
          using (SqlConnection dbConnection = DBUtils.GetDBConnection())
          {
            dbConnection.Open();
            try
            {
              using (SqlCommand sqlCommand = new SqlCommand("UPDATE Invoices_Received SET Date = @Date, Invoice_Number = @oldINum, Supplier = @Supp, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid WHERE Invoice_Number = @INum", dbConnection))
              {
                Decimal num1 = !this.txt_IRED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_IRED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_IRED_Amt.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
                Decimal num2 = !this.txt_IRED_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_IRED_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_IRED_VAT.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
                sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_IRED_Date.Value);
                sqlCommand.Parameters.AddWithValue("@oldINum", (object) this.txt_IRED_InvNum.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Supp", (object) this.txt_IRED_SuppName.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_IRED_Desc.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Amt", (object) num1);
                sqlCommand.Parameters.AddWithValue("@VAT", (object) num2);
                if (this.cb_IRED_Paid.Checked)
                  sqlCommand.Parameters.AddWithValue("@Paid", (object) "Yes");
                else
                  sqlCommand.Parameters.AddWithValue("@Paid", (object) "No");
                sqlCommand.Parameters.AddWithValue("@INum", (object) this.oldINum);
                sqlCommand.ExecuteNonQuery();
                int num3 = (int) MessageBox.Show("Invoice successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        int num4 = (int) MessageBox.Show("Please enter an Invoice Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btn_IRED_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_IRED_Amt_Leave(object sender, EventArgs e)
    {
      Decimal result;
      if (!Decimal.TryParse(this.txt_IRED_Amt.Text.Replace("R", string.Empty), out result))
        return;
      this.txt_IRED_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) (result - result / new Decimal(115, 0, 0, false, (byte) 2)));
    }

    private void txt_IRED_InvNum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRED_InvNum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRA_InvNum_Leave(object sender, EventArgs e)
    {
      this.ln_IRED_InvNum.LineColor = Color.Gray;
    }

    private void txt_IRA_InvNum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRED_InvNum.Focused)
        return;
      this.ln_IRED_InvNum.LineColor = Color.Gray;
    }

    private void txt_IRED_SuppName_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRED_SuppName.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRA_SuppName_Leave(object sender, EventArgs e)
    {
      this.ln_IRED_SuppName.LineColor = Color.Gray;
    }

    private void txt_IRED_SuppName_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRED_SuppName.Focused)
        return;
      this.ln_IRED_SuppName.LineColor = Color.Gray;
    }

    private void txt_IRED_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_IRED_Desc.LineColor = Color.Gray;
    }

    private void txt_IRED_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRED_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRED_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRED_Desc.Focused)
        return;
      this.ln_IRED_Desc.LineColor = Color.Gray;
    }

    private void txt_IRED_Amt_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRED_Amt.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRED_Amt_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRED_Amt.Focused)
        return;
      this.ln_IRED_Amt.LineColor = Color.Gray;
    }

    private void txt_IRED_VAT_Leave(object sender, EventArgs e)
    {
      this.ln_IRED_VAT.LineColor = Color.Gray;
    }

    private void txt_IRED_VAT_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRED_VAT.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRED_VAT_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRED_VAT.Focused)
        return;
      this.ln_IRED_VAT.LineColor = Color.Gray;
    }

    private void btn_IRED_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IRED_Close.Image = (Image) Resources.close_white;
    }

    private void btn_IRED_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IRED_Close.Image = (Image) Resources.close_black;
    }

    private void btn_IRED_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IRED_Done.ForeColor = Color.White;
    }

    private void btn_IRED_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IRED_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IRED_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IRED_Cancel.ForeColor = Color.White;
    }

    private void btn_IRED_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IRED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void Inv_Rec_Edit_Del_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void Inv_Rec_Edit_Del_MouseMove(object sender, MouseEventArgs e)
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

    private void Inv_Rec_Edit_Del_MouseUp(object sender, MouseEventArgs e)
    {
      this.mouseDown = false;
    }

    private void btn_IRED_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Inv_Rec_Edit_Del));
      this.bunifuCustomLabel10 = new BunifuCustomLabel();
      this.btn_IRED_Close = new Button();
      this.btn_IRED_Cancel = new Button();
      this.btn_IRED_Done = new Button();
      this.gb_OA_ODetails = new GroupBox();
      this.panel5 = new Panel();
      this.txt_IRED_SuppName = new TextBox();
      this.ln_IRED_SuppName = new BunifuSeparator();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.panel1 = new Panel();
      this.txt_IRED_InvNum = new TextBox();
      this.ln_IRED_InvNum = new BunifuSeparator();
      this.panel4 = new Panel();
      this.txt_IRED_VAT = new TextBox();
      this.ln_IRED_VAT = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.panel3 = new Panel();
      this.txt_IRED_Amt = new TextBox();
      this.ln_IRED_Amt = new BunifuSeparator();
      this.bunifuCustomLabel7 = new BunifuCustomLabel();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.cb_IRED_Paid = new BunifuCheckbox();
      this.panel2 = new Panel();
      this.txt_IRED_Desc = new TextBox();
      this.ln_IRED_Desc = new BunifuSeparator();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_IRED_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
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
      this.bunifuCustomLabel10.Location = new Point(265, 4);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new Size(129, 22);
      this.bunifuCustomLabel10.TabIndex = 68;
      this.bunifuCustomLabel10.Text = "Update Invoice";
      this.btn_IRED_Close.BackColor = Color.Silver;
      this.btn_IRED_Close.FlatAppearance.BorderSize = 0;
      this.btn_IRED_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_IRED_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_IRED_Close.FlatStyle = FlatStyle.Flat;
      this.btn_IRED_Close.Image = (Image) Resources.close_black;
      this.btn_IRED_Close.Location = new Point(644, 3);
      this.btn_IRED_Close.Name = "btn_IRED_Close";
      this.btn_IRED_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_IRED_Close.Size = new Size(31, 29);
      this.btn_IRED_Close.TabIndex = 69;
      this.btn_IRED_Close.TabStop = false;
      this.btn_IRED_Close.UseVisualStyleBackColor = false;
      this.btn_IRED_Close.Click += new EventHandler(this.btn_IRED_Close_Click);
      this.btn_IRED_Close.MouseEnter += new EventHandler(this.btn_IRED_Close_MouseEnter);
      this.btn_IRED_Close.MouseLeave += new EventHandler(this.btn_IRED_Close_MouseLeave);
      this.btn_IRED_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_IRED_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IRED_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IRED_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_IRED_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IRED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IRED_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IRED_Cancel.Location = new Point(553, 244);
      this.btn_IRED_Cancel.Name = "btn_IRED_Cancel";
      this.btn_IRED_Cancel.Size = new Size(114, 40);
      this.btn_IRED_Cancel.TabIndex = 66;
      this.btn_IRED_Cancel.Text = "Cancel";
      this.btn_IRED_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IRED_Cancel.UseVisualStyleBackColor = true;
      this.btn_IRED_Cancel.Click += new EventHandler(this.btn_IRED_Cancel_Click);
      this.btn_IRED_Cancel.MouseEnter += new EventHandler(this.btn_IRED_Cancel_MouseEnter);
      this.btn_IRED_Cancel.MouseLeave += new EventHandler(this.btn_IRED_Cancel_MouseLeave);
      this.btn_IRED_Done.FlatAppearance.BorderSize = 0;
      this.btn_IRED_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IRED_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IRED_Done.FlatStyle = FlatStyle.Flat;
      this.btn_IRED_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IRED_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IRED_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IRED_Done.Location = new Point(433, 244);
      this.btn_IRED_Done.Name = "btn_IRED_Done";
      this.btn_IRED_Done.Size = new Size(114, 40);
      this.btn_IRED_Done.TabIndex = 65;
      this.btn_IRED_Done.Text = "Done";
      this.btn_IRED_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IRED_Done.UseVisualStyleBackColor = true;
      this.btn_IRED_Done.Click += new EventHandler(this.btn_IRED_Done_Click);
      this.btn_IRED_Done.MouseEnter += new EventHandler(this.btn_IRED_Done_MouseEnter);
      this.btn_IRED_Done.MouseLeave += new EventHandler(this.btn_IRED_Done_MouseLeave);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel1);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel4);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel8);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel3);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel7);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.cb_IRED_Paid);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_IRED_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(12, 38);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(655, 200);
      this.gb_OA_ODetails.TabIndex = 67;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Invoice Details";
      this.panel5.Controls.Add((Control) this.txt_IRED_SuppName);
      this.panel5.Controls.Add((Control) this.ln_IRED_SuppName);
      this.panel5.Location = new Point(133, 57);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(264, 27);
      this.panel5.TabIndex = 72;
      this.txt_IRED_SuppName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRED_SuppName.BackColor = Color.Silver;
      this.txt_IRED_SuppName.BorderStyle = BorderStyle.None;
      this.txt_IRED_SuppName.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRED_SuppName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRED_SuppName.Location = new Point(2, 5);
      this.txt_IRED_SuppName.Name = "txt_IRED_SuppName";
      this.txt_IRED_SuppName.Size = new Size(261, 16);
      this.txt_IRED_SuppName.TabIndex = 3;
      this.txt_IRED_SuppName.Leave += new EventHandler(this.txt_IRA_SuppName_Leave);
      this.txt_IRED_SuppName.MouseEnter += new EventHandler(this.txt_IRED_SuppName_MouseEnter);
      this.txt_IRED_SuppName.MouseLeave += new EventHandler(this.txt_IRED_SuppName_MouseLeave);
      this.ln_IRED_SuppName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRED_SuppName.BackColor = Color.Transparent;
      this.ln_IRED_SuppName.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRED_SuppName.LineThickness = 1;
      this.ln_IRED_SuppName.Location = new Point(-1, 18);
      this.ln_IRED_SuppName.Name = "ln_IRED_SuppName";
      this.ln_IRED_SuppName.Size = new Size(266, 10);
      this.ln_IRED_SuppName.TabIndex = 0;
      this.ln_IRED_SuppName.TabStop = false;
      this.ln_IRED_SuppName.Transparency = (int) byte.MaxValue;
      this.ln_IRED_SuppName.Vertical = false;
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point(26, 61);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(105, 17);
      this.bunifuCustomLabel3.TabIndex = 71;
      this.bunifuCustomLabel3.Text = "Supplier Name:";
      this.panel1.Controls.Add((Control) this.txt_IRED_InvNum);
      this.panel1.Controls.Add((Control) this.ln_IRED_InvNum);
      this.panel1.Location = new Point(133, 23);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(184, 27);
      this.panel1.TabIndex = 70;
      this.txt_IRED_InvNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRED_InvNum.BackColor = Color.Silver;
      this.txt_IRED_InvNum.BorderStyle = BorderStyle.None;
      this.txt_IRED_InvNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRED_InvNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRED_InvNum.Location = new Point(2, 4);
      this.txt_IRED_InvNum.Name = "txt_IRED_InvNum";
      this.txt_IRED_InvNum.Size = new Size(182, 16);
      this.txt_IRED_InvNum.TabIndex = 1;
      this.txt_IRED_InvNum.Leave += new EventHandler(this.txt_IRA_InvNum_Leave);
      this.txt_IRED_InvNum.MouseEnter += new EventHandler(this.txt_IRED_InvNum_MouseEnter);
      this.txt_IRED_InvNum.MouseLeave += new EventHandler(this.txt_IRA_InvNum_MouseLeave);
      this.ln_IRED_InvNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRED_InvNum.BackColor = Color.Transparent;
      this.ln_IRED_InvNum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRED_InvNum.LineThickness = 1;
      this.ln_IRED_InvNum.Location = new Point(-1, 18);
      this.ln_IRED_InvNum.Name = "ln_IRED_InvNum";
      this.ln_IRED_InvNum.Size = new Size(186, 10);
      this.ln_IRED_InvNum.TabIndex = 52;
      this.ln_IRED_InvNum.TabStop = false;
      this.ln_IRED_InvNum.Transparency = (int) byte.MaxValue;
      this.ln_IRED_InvNum.Vertical = false;
      this.panel4.Controls.Add((Control) this.txt_IRED_VAT);
      this.panel4.Controls.Add((Control) this.ln_IRED_VAT);
      this.panel4.Location = new Point(447, 124);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(184, 27);
      this.panel4.TabIndex = 66;
      this.txt_IRED_VAT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRED_VAT.BackColor = Color.Silver;
      this.txt_IRED_VAT.BorderStyle = BorderStyle.None;
      this.txt_IRED_VAT.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRED_VAT.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRED_VAT.Location = new Point(2, 5);
      this.txt_IRED_VAT.Name = "txt_IRED_VAT";
      this.txt_IRED_VAT.Size = new Size(181, 16);
      this.txt_IRED_VAT.TabIndex = 5;
      this.txt_IRED_VAT.TextChanged += new EventHandler(this.txt_IRED_VAT_TextChanged);
      this.txt_IRED_VAT.Leave += new EventHandler(this.txt_IRED_VAT_Leave);
      this.txt_IRED_VAT.MouseEnter += new EventHandler(this.txt_IRED_VAT_MouseEnter);
      this.txt_IRED_VAT.MouseLeave += new EventHandler(this.txt_IRED_VAT_MouseLeave);
      this.ln_IRED_VAT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRED_VAT.BackColor = Color.Transparent;
      this.ln_IRED_VAT.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRED_VAT.LineThickness = 1;
      this.ln_IRED_VAT.Location = new Point(-1, 18);
      this.ln_IRED_VAT.Name = "ln_IRED_VAT";
      this.ln_IRED_VAT.Size = new Size(186, 10);
      this.ln_IRED_VAT.TabIndex = 0;
      this.ln_IRED_VAT.TabStop = false;
      this.ln_IRED_VAT.Transparency = (int) byte.MaxValue;
      this.ln_IRED_VAT.Vertical = false;
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel8.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel8.Location = new Point(404, 128);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new Size(39, 17);
      this.bunifuCustomLabel8.TabIndex = 65;
      this.bunifuCustomLabel8.Text = "VAT:";
      this.panel3.Controls.Add((Control) this.txt_IRED_Amt);
      this.panel3.Controls.Add((Control) this.ln_IRED_Amt);
      this.panel3.Location = new Point(133, 124);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(199, 27);
      this.panel3.TabIndex = 64;
      this.txt_IRED_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRED_Amt.BackColor = Color.Silver;
      this.txt_IRED_Amt.BorderStyle = BorderStyle.None;
      this.txt_IRED_Amt.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRED_Amt.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRED_Amt.Location = new Point(2, 5);
      this.txt_IRED_Amt.Name = "txt_IRED_Amt";
      this.txt_IRED_Amt.Size = new Size(196, 16);
      this.txt_IRED_Amt.TabIndex = 4;
      this.txt_IRED_Amt.TextChanged += new EventHandler(this.txt_IRED_Amt_TextChanged);
      this.txt_IRED_Amt.Leave += new EventHandler(this.txt_IRED_Amt_Leave);
      this.txt_IRED_Amt.MouseEnter += new EventHandler(this.txt_IRED_Amt_MouseEnter);
      this.txt_IRED_Amt.MouseLeave += new EventHandler(this.txt_IRED_Amt_MouseLeave);
      this.ln_IRED_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRED_Amt.BackColor = Color.Transparent;
      this.ln_IRED_Amt.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRED_Amt.LineThickness = 1;
      this.ln_IRED_Amt.Location = new Point(-1, 18);
      this.ln_IRED_Amt.Name = "ln_IRED_Amt";
      this.ln_IRED_Amt.Size = new Size(201, 10);
      this.ln_IRED_Amt.TabIndex = 0;
      this.ln_IRED_Amt.TabStop = false;
      this.ln_IRED_Amt.Transparency = (int) byte.MaxValue;
      this.ln_IRED_Amt.Vertical = false;
      this.bunifuCustomLabel7.AutoSize = true;
      this.bunifuCustomLabel7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel7.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel7.Location = new Point(65, 128);
      this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
      this.bunifuCustomLabel7.Size = new Size(60, 17);
      this.bunifuCustomLabel7.TabIndex = 63;
      this.bunifuCustomLabel7.Text = "Amount:";
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(166, 164);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(36, 17);
      this.bunifuCustomLabel6.TabIndex = 62;
      this.bunifuCustomLabel6.Text = "Paid";
      this.cb_IRED_Paid.BackColor = Color.FromArgb(132, 135, 140);
      this.cb_IRED_Paid.ChechedOffColor = Color.FromArgb(132, 135, 140);
      this.cb_IRED_Paid.Checked = false;
      this.cb_IRED_Paid.CheckedOnColor = Color.FromArgb(15, 91, 142);
      this.cb_IRED_Paid.ForeColor = Color.White;
      this.cb_IRED_Paid.Location = new Point(140, 164);
      this.cb_IRED_Paid.Name = "cb_IRED_Paid";
      this.cb_IRED_Paid.Size = new Size(20, 20);
      this.cb_IRED_Paid.TabIndex = 6;
      this.panel2.Controls.Add((Control) this.txt_IRED_Desc);
      this.panel2.Controls.Add((Control) this.ln_IRED_Desc);
      this.panel2.Location = new Point(133, 91);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(499, 27);
      this.panel2.TabIndex = 60;
      this.txt_IRED_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRED_Desc.BackColor = Color.Silver;
      this.txt_IRED_Desc.BorderStyle = BorderStyle.None;
      this.txt_IRED_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRED_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRED_Desc.Location = new Point(2, 5);
      this.txt_IRED_Desc.Name = "txt_IRED_Desc";
      this.txt_IRED_Desc.Size = new Size(496, 16);
      this.txt_IRED_Desc.TabIndex = 3;
      this.txt_IRED_Desc.Leave += new EventHandler(this.txt_IRED_Desc_Leave);
      this.txt_IRED_Desc.MouseEnter += new EventHandler(this.txt_IRED_Desc_MouseEnter);
      this.txt_IRED_Desc.MouseLeave += new EventHandler(this.txt_IRED_Desc_MouseLeave);
      this.ln_IRED_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRED_Desc.BackColor = Color.Transparent;
      this.ln_IRED_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRED_Desc.LineThickness = 1;
      this.ln_IRED_Desc.Location = new Point(-1, 18);
      this.ln_IRED_Desc.Name = "ln_IRED_Desc";
      this.ln_IRED_Desc.Size = new Size(501, 10);
      this.ln_IRED_Desc.TabIndex = 0;
      this.ln_IRED_Desc.TabStop = false;
      this.ln_IRED_Desc.Transparency = (int) byte.MaxValue;
      this.ln_IRED_Desc.Vertical = false;
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(46, 95);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Description:";
      this.dtp_IRED_Date.BackColor = Color.Silver;
      this.dtp_IRED_Date.BorderRadius = 0;
      this.dtp_IRED_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_IRED_Date.Format = DateTimePickerFormat.Short;
      this.dtp_IRED_Date.FormatCustom = (string) null;
      this.dtp_IRED_Date.Location = new Point(428, 23);
      this.dtp_IRED_Date.Name = "dtp_IRED_Date";
      this.dtp_IRED_Date.Size = new Size(205, 25);
      this.dtp_IRED_Date.TabIndex = 2;
      this.dtp_IRED_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
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
      this.ClientSize = new Size(679, 299);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_IRED_Close);
      this.Controls.Add((Control) this.btn_IRED_Cancel);
      this.Controls.Add((Control) this.btn_IRED_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(679, 299);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(679, 299);
      this.Name = nameof (Inv_Rec_Edit_Del);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Invoice Recieved Edit/Delete";
      this.Load += new EventHandler(this.Inv_Rec_Edit_Del_Load);
      this.MouseDown += new MouseEventHandler(this.Inv_Rec_Edit_Del_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Inv_Rec_Edit_Del_MouseMove);
      this.MouseUp += new MouseEventHandler(this.Inv_Rec_Edit_Del_MouseUp);
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
