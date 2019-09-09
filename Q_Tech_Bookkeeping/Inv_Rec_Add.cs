// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Inv_Rec_Add
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Inv_Rec_Add : Form
  {
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private StringBuilder sb;
    private Point lastLocation;
    private Button btn_IRA_Cancel;
    private Button btn_IRA_Done;
    private GroupBox gb_OA_ODetails;
    private Panel panel1;
    private TextBox txt_IRA_InvNum;
    private BunifuSeparator ln_IRA_InvNum;
    private Panel panel4;
    private TextBox txt_IRA_VAT;
    private BunifuSeparator ln_IRA_VAT;
    private BunifuCustomLabel bunifuCustomLabel8;
    private Panel panel3;
    private TextBox txt_IRA_Amt;
    private BunifuSeparator ln_IRA_Amt;
    private BunifuCustomLabel bunifuCustomLabel7;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCheckbox cb_IRA_Paid;
    private Panel panel2;
    private TextBox txt_IRA_Desc;
    private BunifuSeparator ln_IRA_Desc;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_IRA_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;
    private Panel panel5;
    private TextBox txt_IRA_SuppName;
    private BunifuSeparator ln_IRA_SuppName;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_IRA_Close;

    public Inv_Rec_Add()
    {
      this.InitializeComponent();
    }

    private void Inv_Rec_Add_Load(object sender, EventArgs e)
    {
      this.txt_IRA_Amt.Text = "R0.00";
      this.txt_IRA_Amt.SelectionStart = this.txt_IRA_Amt.Text.Length;
      this.txt_IRA_VAT.Text = "R0.00";
      this.txt_IRA_VAT.SelectionStart = this.txt_IRA_VAT.Text.Length;
      this.dtp_IRA_Date.Value = DateTime.Now;
    }

    private void txt_IRA_Amt_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_IRA_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_IRA_Amt.TextChanged -= new EventHandler(this.txt_IRA_Amt_TextChanged);
        this.txt_IRA_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
        this.txt_IRA_Amt.TextChanged += new EventHandler(this.txt_IRA_Amt_TextChanged);
        this.txt_IRA_Amt.Select(this.txt_IRA_Amt.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_IRA_Amt.Text))
        return;
      this.txt_IRA_Amt.Text = "R0.00";
      this.txt_IRA_Amt.Select(this.txt_IRA_Amt.Text.Length, 0);
    }

    private bool TextisValid(string text)
    {
      return new Regex("[^0-9]").IsMatch(text);
    }

    private void txt_IRA_VAT_TextChanged(object sender, EventArgs e)
    {
      Decimal result;
      if (Decimal.TryParse(this.txt_IRA_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
      {
        Decimal num = result / new Decimal(100);
        this.txt_IRA_VAT.TextChanged -= new EventHandler(this.txt_IRA_VAT_TextChanged);
        this.txt_IRA_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
        this.txt_IRA_VAT.TextChanged += new EventHandler(this.txt_IRA_VAT_TextChanged);
        this.txt_IRA_VAT.Select(this.txt_IRA_VAT.Text.Length, 0);
      }
      if (this.TextisValid(this.txt_IRA_VAT.Text))
        return;
      this.txt_IRA_VAT.Text = "R0.00";
      this.txt_IRA_VAT.Select(this.txt_IRA_VAT.Text.Length, 0);
    }

    private void txt_IRA_Amt_Leave(object sender, EventArgs e)
    {
      Decimal result;
      if (!Decimal.TryParse(this.txt_IRA_Amt.Text.Replace("R", string.Empty), out result))
        return;
      this.txt_IRA_VAT.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) (result - result / new Decimal(115, 0, 0, false, (byte) 2)));
    }

    private void btn_IRA_Done_Click(object sender, EventArgs e)
    {
      string text = this.txt_IRA_InvNum.Text;
      this.sb = new StringBuilder().Append("Are you sure you want to add invoice with Invoice Number: ").Append(text).Append("?");
      if (text != string.Empty)
      {
        if (MessageBox.Show(this.sb.ToString(), "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          dbConnection.Open();
          try
          {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Invoices_Received VALUES (@Date, @InvNum, @Supp, @Desc, @Amt, @VAT, @Paid)", dbConnection))
            {
              Decimal num1 = !this.txt_IRA_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_IRA_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_IRA_Amt.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
              Decimal num2 = !this.txt_IRA_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_IRA_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_IRA_VAT.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2));
              sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_IRA_Date.Value);
              sqlCommand.Parameters.AddWithValue("@InvNum", (object) this.txt_IRA_InvNum.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Supp", (object) this.txt_IRA_SuppName.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_IRA_Desc.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Amt", (object) num1);
              sqlCommand.Parameters.AddWithValue("@VAT", (object) num2);
              if (this.cb_IRA_Paid.Checked)
                sqlCommand.Parameters.AddWithValue("@Paid", (object) "Yes");
              else
                sqlCommand.Parameters.AddWithValue("@Paid", (object) "No");
              sqlCommand.ExecuteNonQuery();
              int num3 = (int) MessageBox.Show("New invoice successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              this.Close();
            }
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
        }
      }
      else
      {
        int num4 = (int) MessageBox.Show("Please enter an Invoice Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btn_IRA_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_IRA_InvNum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRA_InvNum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRA_InvNum_Leave(object sender, EventArgs e)
    {
      this.ln_IRA_InvNum.LineColor = Color.Gray;
    }

    private void txt_IRA_InvNum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRA_InvNum.Focused)
        return;
      this.ln_IRA_InvNum.LineColor = Color.Gray;
    }

    private void txt_IRA_SuppName_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRA_SuppName.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRA_SuppName_Leave(object sender, EventArgs e)
    {
      this.ln_IRA_SuppName.LineColor = Color.Gray;
    }

    private void txt_IRA_SuppName_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRA_SuppName.Focused)
        return;
      this.ln_IRA_SuppName.LineColor = Color.Gray;
    }

    private void txt_IRA_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_IRA_Desc.LineColor = Color.Gray;
    }

    private void txt_IRA_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRA_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRA_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRA_Desc.Focused)
        return;
      this.ln_IRA_Desc.LineColor = Color.Gray;
    }

    private void txt_IRA_Amt_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRA_Amt.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRA_Amt_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRA_Amt.Focused)
        return;
      this.ln_IRA_Amt.LineColor = Color.Gray;
    }

    private void txt_IRA_VAT_Leave(object sender, EventArgs e)
    {
      this.ln_IRA_VAT.LineColor = Color.Gray;
    }

    private void txt_IRA_VAT_MouseEnter(object sender, EventArgs e)
    {
      this.ln_IRA_VAT.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_IRA_VAT_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_IRA_VAT.Focused)
        return;
      this.ln_IRA_VAT.LineColor = Color.Gray;
    }

    private void btn_IRA_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_IRA_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IRA_Close.Image = (Image) Resources.close_white;
    }

    private void btn_IRA_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IRA_Close.Image = (Image) Resources.close_black;
    }

    private void btn_IRA_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IRA_Done.ForeColor = Color.White;
    }

    private void btn_IRA_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IRA_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IRA_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IRA_Cancel.ForeColor = Color.White;
    }

    private void btn_IRA_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IRA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void Inv_Rec_Add_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void Inv_Rec_Add_MouseMove(object sender, MouseEventArgs e)
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

    private void Inv_REc_Add_MouseUp(object sender, MouseEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Inv_Rec_Add));
      this.btn_IRA_Cancel = new Button();
      this.btn_IRA_Done = new Button();
      this.gb_OA_ODetails = new GroupBox();
      this.panel5 = new Panel();
      this.txt_IRA_SuppName = new TextBox();
      this.ln_IRA_SuppName = new BunifuSeparator();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.panel1 = new Panel();
      this.txt_IRA_InvNum = new TextBox();
      this.ln_IRA_InvNum = new BunifuSeparator();
      this.panel4 = new Panel();
      this.txt_IRA_VAT = new TextBox();
      this.ln_IRA_VAT = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.panel3 = new Panel();
      this.txt_IRA_Amt = new TextBox();
      this.ln_IRA_Amt = new BunifuSeparator();
      this.bunifuCustomLabel7 = new BunifuCustomLabel();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.cb_IRA_Paid = new BunifuCheckbox();
      this.panel2 = new Panel();
      this.txt_IRA_Desc = new TextBox();
      this.ln_IRA_Desc = new BunifuSeparator();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_IRA_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.bunifuCustomLabel10 = new BunifuCustomLabel();
      this.btn_IRA_Close = new Button();
      this.gb_OA_ODetails.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.btn_IRA_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_IRA_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IRA_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IRA_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_IRA_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IRA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IRA_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IRA_Cancel.Location = new Point(553, 245);
      this.btn_IRA_Cancel.Name = "btn_IRA_Cancel";
      this.btn_IRA_Cancel.Size = new Size(114, 40);
      this.btn_IRA_Cancel.TabIndex = 61;
      this.btn_IRA_Cancel.Text = "Cancel";
      this.btn_IRA_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IRA_Cancel.UseVisualStyleBackColor = true;
      this.btn_IRA_Cancel.Click += new EventHandler(this.btn_IRA_Cancel_Click);
      this.btn_IRA_Cancel.MouseEnter += new EventHandler(this.btn_IRA_Cancel_MouseEnter);
      this.btn_IRA_Cancel.MouseLeave += new EventHandler(this.btn_IRA_Cancel_MouseLeave);
      this.btn_IRA_Done.FlatAppearance.BorderSize = 0;
      this.btn_IRA_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IRA_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IRA_Done.FlatStyle = FlatStyle.Flat;
      this.btn_IRA_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IRA_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IRA_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IRA_Done.Location = new Point(433, 245);
      this.btn_IRA_Done.Name = "btn_IRA_Done";
      this.btn_IRA_Done.Size = new Size(114, 40);
      this.btn_IRA_Done.TabIndex = 60;
      this.btn_IRA_Done.Text = "Done";
      this.btn_IRA_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IRA_Done.UseVisualStyleBackColor = true;
      this.btn_IRA_Done.Click += new EventHandler(this.btn_IRA_Done_Click);
      this.btn_IRA_Done.MouseEnter += new EventHandler(this.btn_IRA_Done_MouseEnter);
      this.btn_IRA_Done.MouseLeave += new EventHandler(this.btn_IRA_Done_MouseLeave);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel1);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel4);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel8);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel3);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel7);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.cb_IRA_Paid);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_IRA_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(12, 39);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(655, 200);
      this.gb_OA_ODetails.TabIndex = 62;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Invoice Details";
      this.panel5.Controls.Add((Control) this.txt_IRA_SuppName);
      this.panel5.Controls.Add((Control) this.ln_IRA_SuppName);
      this.panel5.Location = new Point(133, 57);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(264, 27);
      this.panel5.TabIndex = 72;
      this.txt_IRA_SuppName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRA_SuppName.BackColor = Color.Silver;
      this.txt_IRA_SuppName.BorderStyle = BorderStyle.None;
      this.txt_IRA_SuppName.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRA_SuppName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRA_SuppName.Location = new Point(2, 5);
      this.txt_IRA_SuppName.Name = "txt_IRA_SuppName";
      this.txt_IRA_SuppName.Size = new Size(261, 16);
      this.txt_IRA_SuppName.TabIndex = 3;
      this.txt_IRA_SuppName.Leave += new EventHandler(this.txt_IRA_SuppName_Leave);
      this.txt_IRA_SuppName.MouseEnter += new EventHandler(this.txt_IRA_SuppName_MouseEnter);
      this.txt_IRA_SuppName.MouseLeave += new EventHandler(this.txt_IRA_SuppName_MouseLeave);
      this.ln_IRA_SuppName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRA_SuppName.BackColor = Color.Transparent;
      this.ln_IRA_SuppName.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRA_SuppName.LineThickness = 1;
      this.ln_IRA_SuppName.Location = new Point(-1, 18);
      this.ln_IRA_SuppName.Name = "ln_IRA_SuppName";
      this.ln_IRA_SuppName.Size = new Size(266, 10);
      this.ln_IRA_SuppName.TabIndex = 0;
      this.ln_IRA_SuppName.TabStop = false;
      this.ln_IRA_SuppName.Transparency = (int) byte.MaxValue;
      this.ln_IRA_SuppName.Vertical = false;
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point(26, 61);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(105, 17);
      this.bunifuCustomLabel3.TabIndex = 71;
      this.bunifuCustomLabel3.Text = "Supplier Name:";
      this.panel1.Controls.Add((Control) this.txt_IRA_InvNum);
      this.panel1.Controls.Add((Control) this.ln_IRA_InvNum);
      this.panel1.Location = new Point(133, 23);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(184, 27);
      this.panel1.TabIndex = 70;
      this.txt_IRA_InvNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRA_InvNum.BackColor = Color.Silver;
      this.txt_IRA_InvNum.BorderStyle = BorderStyle.None;
      this.txt_IRA_InvNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRA_InvNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRA_InvNum.Location = new Point(2, 4);
      this.txt_IRA_InvNum.Name = "txt_IRA_InvNum";
      this.txt_IRA_InvNum.Size = new Size(182, 16);
      this.txt_IRA_InvNum.TabIndex = 1;
      this.txt_IRA_InvNum.Leave += new EventHandler(this.txt_IRA_InvNum_Leave);
      this.txt_IRA_InvNum.MouseEnter += new EventHandler(this.txt_IRA_InvNum_MouseEnter);
      this.txt_IRA_InvNum.MouseLeave += new EventHandler(this.txt_IRA_InvNum_MouseLeave);
      this.ln_IRA_InvNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRA_InvNum.BackColor = Color.Transparent;
      this.ln_IRA_InvNum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRA_InvNum.LineThickness = 1;
      this.ln_IRA_InvNum.Location = new Point(-1, 18);
      this.ln_IRA_InvNum.Name = "ln_IRA_InvNum";
      this.ln_IRA_InvNum.Size = new Size(186, 10);
      this.ln_IRA_InvNum.TabIndex = 52;
      this.ln_IRA_InvNum.TabStop = false;
      this.ln_IRA_InvNum.Transparency = (int) byte.MaxValue;
      this.ln_IRA_InvNum.Vertical = false;
      this.panel4.Controls.Add((Control) this.txt_IRA_VAT);
      this.panel4.Controls.Add((Control) this.ln_IRA_VAT);
      this.panel4.Location = new Point(447, 124);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(184, 27);
      this.panel4.TabIndex = 66;
      this.txt_IRA_VAT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRA_VAT.BackColor = Color.Silver;
      this.txt_IRA_VAT.BorderStyle = BorderStyle.None;
      this.txt_IRA_VAT.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRA_VAT.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRA_VAT.Location = new Point(2, 5);
      this.txt_IRA_VAT.Name = "txt_IRA_VAT";
      this.txt_IRA_VAT.Size = new Size(181, 16);
      this.txt_IRA_VAT.TabIndex = 5;
      this.txt_IRA_VAT.TextChanged += new EventHandler(this.txt_IRA_VAT_TextChanged);
      this.txt_IRA_VAT.Leave += new EventHandler(this.txt_IRA_VAT_Leave);
      this.txt_IRA_VAT.MouseEnter += new EventHandler(this.txt_IRA_VAT_MouseEnter);
      this.txt_IRA_VAT.MouseLeave += new EventHandler(this.txt_IRA_VAT_MouseLeave);
      this.ln_IRA_VAT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRA_VAT.BackColor = Color.Transparent;
      this.ln_IRA_VAT.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRA_VAT.LineThickness = 1;
      this.ln_IRA_VAT.Location = new Point(-1, 18);
      this.ln_IRA_VAT.Name = "ln_IRA_VAT";
      this.ln_IRA_VAT.Size = new Size(186, 10);
      this.ln_IRA_VAT.TabIndex = 0;
      this.ln_IRA_VAT.TabStop = false;
      this.ln_IRA_VAT.Transparency = (int) byte.MaxValue;
      this.ln_IRA_VAT.Vertical = false;
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel8.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel8.Location = new Point(404, 128);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new Size(39, 17);
      this.bunifuCustomLabel8.TabIndex = 65;
      this.bunifuCustomLabel8.Text = "VAT:";
      this.panel3.Controls.Add((Control) this.txt_IRA_Amt);
      this.panel3.Controls.Add((Control) this.ln_IRA_Amt);
      this.panel3.Location = new Point(133, 124);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(199, 27);
      this.panel3.TabIndex = 64;
      this.txt_IRA_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRA_Amt.BackColor = Color.Silver;
      this.txt_IRA_Amt.BorderStyle = BorderStyle.None;
      this.txt_IRA_Amt.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRA_Amt.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRA_Amt.Location = new Point(2, 5);
      this.txt_IRA_Amt.Name = "txt_IRA_Amt";
      this.txt_IRA_Amt.Size = new Size(196, 16);
      this.txt_IRA_Amt.TabIndex = 4;
      this.txt_IRA_Amt.TextChanged += new EventHandler(this.txt_IRA_Amt_TextChanged);
      this.txt_IRA_Amt.Leave += new EventHandler(this.txt_IRA_Amt_Leave);
      this.txt_IRA_Amt.MouseEnter += new EventHandler(this.txt_IRA_Amt_MouseEnter);
      this.txt_IRA_Amt.MouseLeave += new EventHandler(this.txt_IRA_Amt_MouseLeave);
      this.ln_IRA_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRA_Amt.BackColor = Color.Transparent;
      this.ln_IRA_Amt.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRA_Amt.LineThickness = 1;
      this.ln_IRA_Amt.Location = new Point(-1, 18);
      this.ln_IRA_Amt.Name = "ln_IRA_Amt";
      this.ln_IRA_Amt.Size = new Size(201, 10);
      this.ln_IRA_Amt.TabIndex = 0;
      this.ln_IRA_Amt.TabStop = false;
      this.ln_IRA_Amt.Transparency = (int) byte.MaxValue;
      this.ln_IRA_Amt.Vertical = false;
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
      this.cb_IRA_Paid.BackColor = Color.FromArgb(132, 135, 140);
      this.cb_IRA_Paid.ChechedOffColor = Color.FromArgb(132, 135, 140);
      this.cb_IRA_Paid.Checked = false;
      this.cb_IRA_Paid.CheckedOnColor = Color.FromArgb(15, 91, 142);
      this.cb_IRA_Paid.ForeColor = Color.White;
      this.cb_IRA_Paid.Location = new Point(140, 164);
      this.cb_IRA_Paid.Name = "cb_IRA_Paid";
      this.cb_IRA_Paid.Size = new Size(20, 20);
      this.cb_IRA_Paid.TabIndex = 6;
      this.panel2.Controls.Add((Control) this.txt_IRA_Desc);
      this.panel2.Controls.Add((Control) this.ln_IRA_Desc);
      this.panel2.Location = new Point(133, 91);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(499, 27);
      this.panel2.TabIndex = 60;
      this.txt_IRA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_IRA_Desc.BackColor = Color.Silver;
      this.txt_IRA_Desc.BorderStyle = BorderStyle.None;
      this.txt_IRA_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IRA_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_IRA_Desc.Location = new Point(2, 5);
      this.txt_IRA_Desc.Name = "txt_IRA_Desc";
      this.txt_IRA_Desc.Size = new Size(496, 16);
      this.txt_IRA_Desc.TabIndex = 3;
      this.txt_IRA_Desc.Leave += new EventHandler(this.txt_IRA_Desc_Leave);
      this.txt_IRA_Desc.MouseEnter += new EventHandler(this.txt_IRA_Desc_MouseEnter);
      this.txt_IRA_Desc.MouseLeave += new EventHandler(this.txt_IRA_Desc_MouseLeave);
      this.ln_IRA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_IRA_Desc.BackColor = Color.Transparent;
      this.ln_IRA_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_IRA_Desc.LineThickness = 1;
      this.ln_IRA_Desc.Location = new Point(-1, 18);
      this.ln_IRA_Desc.Name = "ln_IRA_Desc";
      this.ln_IRA_Desc.Size = new Size(501, 10);
      this.ln_IRA_Desc.TabIndex = 0;
      this.ln_IRA_Desc.TabStop = false;
      this.ln_IRA_Desc.Transparency = (int) byte.MaxValue;
      this.ln_IRA_Desc.Vertical = false;
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(46, 95);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Description:";
      this.dtp_IRA_Date.BackColor = Color.Silver;
      this.dtp_IRA_Date.BorderRadius = 0;
      this.dtp_IRA_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_IRA_Date.Format = DateTimePickerFormat.Short;
      this.dtp_IRA_Date.FormatCustom = (string) null;
      this.dtp_IRA_Date.Location = new Point(428, 23);
      this.dtp_IRA_Date.Name = "dtp_IRA_Date";
      this.dtp_IRA_Date.Size = new Size(205, 25);
      this.dtp_IRA_Date.TabIndex = 2;
      this.dtp_IRA_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
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
      this.bunifuCustomLabel10.AutoSize = true;
      this.bunifuCustomLabel10.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel10.ForeColor = Color.FromArgb(15, 91, 142);
      this.bunifuCustomLabel10.Location = new Point(265, 5);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new Size(144, 22);
      this.bunifuCustomLabel10.TabIndex = 63;
      this.bunifuCustomLabel10.Text = "Add New Invoice";
      this.btn_IRA_Close.BackColor = Color.Silver;
      this.btn_IRA_Close.FlatAppearance.BorderSize = 0;
      this.btn_IRA_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_IRA_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_IRA_Close.FlatStyle = FlatStyle.Flat;
      this.btn_IRA_Close.Image = (Image) Resources.close_black;
      this.btn_IRA_Close.Location = new Point(644, 4);
      this.btn_IRA_Close.Name = "btn_IRA_Close";
      this.btn_IRA_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_IRA_Close.Size = new Size(31, 29);
      this.btn_IRA_Close.TabIndex = 64;
      this.btn_IRA_Close.TabStop = false;
      this.btn_IRA_Close.UseVisualStyleBackColor = false;
      this.btn_IRA_Close.Click += new EventHandler(this.btn_IRA_Close_Click);
      this.btn_IRA_Close.MouseEnter += new EventHandler(this.btn_IRA_Close_MouseEnter);
      this.btn_IRA_Close.MouseLeave += new EventHandler(this.btn_IRA_Cancel_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(679, 299);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_IRA_Close);
      this.Controls.Add((Control) this.btn_IRA_Cancel);
      this.Controls.Add((Control) this.btn_IRA_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(679, 299);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(679, 299);
      this.Name = nameof (Inv_Rec_Add);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Add Recieved Invoice";
      this.Load += new EventHandler(this.Inv_Rec_Add_Load);
      this.MouseDown += new MouseEventHandler(this.Inv_Rec_Add_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Inv_Rec_Add_MouseMove);
      this.MouseUp += new MouseEventHandler(this.Inv_REc_Add_MouseUp);
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
