// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.O_Add
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
  public class O_Add : Form
  {
    private bool isInter = false;
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private Decimal pInv;
    private Decimal pRec;
    private StringBuilder sb;
    private Point lastLocation;
    private GroupBox gb_OA_ODetails;
    private GroupBox gb_OA_CDetails;
    private BunifuCustomLabel bunifuCustomLabel8;
    private BunifuCustomLabel bunifuCustomLabel9;
    private BunifuCustomLabel bunifuCustomLabel7;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_OA_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;
    private BunifuMaterialTextbox txt_OA_CName;
    private BunifuCustomLabel bunifuCustomLabel4;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuMaterialTextbox txt_OA_CCode;
    private Button btn_OA_Done;
    private BackgroundWorker backgroundWorker1;
    private Panel panel6;
    private TextBox txt_OA_QNum;
    private BunifuSeparator ln_OA_QNum;
    private Panel panel5;
    private TextBox txt_OA_PercRec;
    private BunifuSeparator ln_OA_PercRec;
    private Panel panel4;
    private TextBox txt_OA_PercInv;
    private BunifuSeparator ln_OA_PercInv;
    private Panel panel3;
    private TextBox txt_OA_Amt;
    private BunifuSeparator ln_OA_Amt;
    private Panel panel2;
    private TextBox txt_OA_Desc;
    private BunifuSeparator ln_OA_Desc;
    private Panel panel1;
    private TextBox txt_OA_CONum;
    private BunifuSeparator ln_OA_CONum;
    private Button btn_OA_Close;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_OA_Cancel;

    public O_Add()
    {
      this.InitializeComponent();
    }

    private void txt_OA_Amt_TextChanged(object sender, EventArgs e)
    {
      if (this.isInter)
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_OA_Amt.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0'), out result))
        {
          Decimal num = result / new Decimal(100);
          this.txt_OA_Amt.TextChanged -= new EventHandler(this.txt_OA_Amt_TextChanged);
          this.txt_OA_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (object) num);
          this.txt_OA_Amt.TextChanged += new EventHandler(this.txt_OA_Amt_TextChanged);
          this.txt_OA_Amt.Select(this.txt_OA_Amt.Text.Length, 0);
        }
        if (this.TextisValid(this.txt_OA_Amt.Text))
          return;
        this.txt_OA_Amt.Text = "$0.00";
        this.txt_OA_Amt.Select(this.txt_OA_Amt.Text.Length, 0);
      }
      else
      {
        Decimal result;
        if (Decimal.TryParse(this.txt_OA_Amt.Text.Replace(",", "").Replace("R", "").Replace(".", "").TrimStart('0'), out result))
        {
          Decimal num = result / new Decimal(100);
          this.txt_OA_Amt.TextChanged -= new EventHandler(this.txt_OA_Amt_TextChanged);
          this.txt_OA_Amt.Text = string.Format((IFormatProvider) CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (object) num);
          this.txt_OA_Amt.TextChanged += new EventHandler(this.txt_OA_Amt_TextChanged);
          this.txt_OA_Amt.Select(this.txt_OA_Amt.Text.Length, 0);
        }
        if (!this.TextisValid(this.txt_OA_Amt.Text))
        {
          this.txt_OA_Amt.Text = "R0.00";
          this.txt_OA_Amt.Select(this.txt_OA_Amt.Text.Length, 0);
        }
      }
    }

    private bool TextisValid(string text)
    {
      return new Regex("[^0-9]").IsMatch(text);
    }

    private void O_Add_Load(object sender, EventArgs e)
    {
      Home owner = (Home) this.Owner;
      this.txt_OA_PercInv.Text = "0%";
      this.txt_OA_PercRec.Text = "0%";
      if (owner.getCurPanel() == "pnl_L_Orders")
      {
        Orders curForm = (Orders) owner.getCurForm();
        this.txt_OA_CCode.Text = curForm.getCCode();
        this.txt_OA_CName.Text = curForm.getCName();
        this.txt_OA_Amt.Text = "R0.00";
      }
      else
      {
        this.isInter = true;
        Int_Orders curForm = (Int_Orders) owner.getCurForm();
        this.txt_OA_CCode.Text = curForm.getCCode();
        this.txt_OA_CName.Text = curForm.getCName();
        this.txt_OA_Amt.Text = "$0.00";
      }
      this.txt_OA_Amt.SelectionStart = this.txt_OA_Amt.Text.Length;
    }

    private void txt_OA_Perc_Inv_Validating(object sender, CancelEventArgs e)
    {
      double result;
      if (double.TryParse(this.txt_OA_PercInv.Text, out result) && Convert.ToDouble(this.txt_OA_PercInv.Text) >= 0.0 && Convert.ToDouble(this.txt_OA_PercInv.Text) <= 100.0)
      {
        this.pInv = Convert.ToDecimal(this.txt_OA_PercInv.Text.ToString());
        this.txt_OA_PercInv.Text = result.ToString() + "%";
      }
      else if (this.txt_OA_PercInv.Text == string.Empty)
      {
        this.txt_OA_PercInv.Text = "0%";
      }
      else
      {
        e.Cancel = true;
        int num = (int) MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void txt_OA_Perc_Rec_Validating(object sender, CancelEventArgs e)
    {
      double result;
      if (double.TryParse(this.txt_OA_PercRec.Text, out result) && Convert.ToDouble(this.txt_OA_PercRec.Text) >= 0.0 && Convert.ToDouble(this.txt_OA_PercRec.Text) <= 100.0)
      {
        this.pRec = Convert.ToDecimal(this.txt_OA_PercRec.Text.ToString());
        this.txt_OA_PercRec.Text = result.ToString() + "%";
      }
      else if (this.txt_OA_PercRec.Text == string.Empty)
      {
        this.txt_OA_PercRec.Text = "0%";
      }
      else
      {
        e.Cancel = true;
        int num = (int) MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void txt_OA_Perc_Inv_Enter(object sender, EventArgs e)
    {
      this.txt_OA_PercInv.Clear();
    }

    private void txt_OA_Perc_Rec_Enter(object sender, EventArgs e)
    {
      this.txt_OA_PercRec.Clear();
    }

    private void btn_OA_Done_Click(object sender, EventArgs e)
    {
      string text = this.txt_OA_CONum.Text;
      this.sb = new StringBuilder().Append("Are you sure you want to add order with Client Order Number: ").Append(text).Append("?");
      if (text != string.Empty)
      {
        if (MessageBox.Show(this.sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          dbConnection.Open();
          try
          {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Orders_Received VALUES (@Date, @Client, @CONum, @Desc, @Amt, @PercInv, @PercRec, @QNum)", dbConnection))
            {
              Decimal num1 = !this.isInter ? (!this.txt_OA_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_OA_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(this.txt_OA_Amt.Text.Replace("R", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte) 2))) : (!this.txt_OA_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte) 2) : (!(this.txt_OA_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(this.txt_OA_Amt.Text.Replace("$", string.Empty), (IFormatProvider) CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte) 2)));
              sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_OA_Date.Value);
              sqlCommand.Parameters.AddWithValue("@Client", (object) this.txt_OA_CName.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@CONum", (object) this.txt_OA_CONum.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_OA_Desc.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Amt", (object) num1);
              sqlCommand.Parameters.AddWithValue("@PercInv", (object) this.pInv);
              sqlCommand.Parameters.AddWithValue("@PercRec", (object) this.pRec);
              sqlCommand.Parameters.AddWithValue("@QNum", (object) this.txt_OA_QNum.Text.Trim());
              sqlCommand.ExecuteNonQuery();
              int num2 = (int) MessageBox.Show("New order successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        int num3 = (int) MessageBox.Show("Please enter a Client Order Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btn_OA_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_OA_CONum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OA_CONum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OA_CONum_Leave(object sender, EventArgs e)
    {
      this.ln_OA_CONum.LineColor = Color.Gray;
    }

    private void txt_OA_CONum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OA_CONum.Focused)
        return;
      this.ln_OA_CONum.LineColor = Color.Gray;
    }

    private void txt_OA_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_OA_Desc.LineColor = Color.Gray;
    }

    private void txt_OA_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OA_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OA_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OA_Desc.Focused)
        return;
      this.ln_OA_Desc.LineColor = Color.Gray;
    }

    private void txt_OA_Amt_Leave(object sender, EventArgs e)
    {
      this.ln_OA_Amt.LineColor = Color.Gray;
    }

    private void txt_OA_Amt_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OA_Amt.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OA_Amt_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OA_Amt.Focused)
        return;
      this.ln_OA_Amt.LineColor = Color.Gray;
    }

    private void txt_OA_PercInv_Leave(object sender, EventArgs e)
    {
      this.ln_OA_PercInv.LineColor = Color.Gray;
    }

    private void txt_OA_PercInv_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OA_PercInv.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OA_PercInv_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OA_PercInv.Focused)
        return;
      this.ln_OA_PercInv.LineColor = Color.Gray;
    }

    private void txt_OA_PercRec_Leave(object sender, EventArgs e)
    {
      this.ln_OA_PercRec.LineColor = Color.Gray;
    }

    private void txt_OA_PercRec_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OA_PercRec.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OA_PercRec_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OA_PercRec.Focused)
        return;
      this.ln_OA_PercRec.LineColor = Color.Gray;
    }

    private void txt_OA_QNum_Leave(object sender, EventArgs e)
    {
      this.ln_OA_QNum.LineColor = Color.Gray;
    }

    private void txt_OA_QNum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_OA_QNum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_OA_QNum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_OA_QNum.Focused)
        return;
      this.ln_OA_QNum.LineColor = Color.Gray;
    }

    private void btn_OA_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_OA_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_OA_Close.Image = (Image) Resources.close_white;
    }

    private void btn_OA_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_OA_Close.Image = (Image) Resources.close_black;
    }

    private void btn_OA_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_OA_Done.ForeColor = Color.White;
    }

    private void btn_OA_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_OA_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_OA_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_OA_Cancel.ForeColor = Color.White;
    }

    private void btn_OA_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_OA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_OA_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_OA_CName_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (O_Add));
      this.gb_OA_ODetails = new GroupBox();
      this.panel6 = new Panel();
      this.txt_OA_QNum = new TextBox();
      this.ln_OA_QNum = new BunifuSeparator();
      this.panel5 = new Panel();
      this.txt_OA_PercRec = new TextBox();
      this.ln_OA_PercRec = new BunifuSeparator();
      this.panel4 = new Panel();
      this.txt_OA_PercInv = new TextBox();
      this.ln_OA_PercInv = new BunifuSeparator();
      this.panel3 = new Panel();
      this.txt_OA_Amt = new TextBox();
      this.ln_OA_Amt = new BunifuSeparator();
      this.panel2 = new Panel();
      this.txt_OA_Desc = new TextBox();
      this.ln_OA_Desc = new BunifuSeparator();
      this.panel1 = new Panel();
      this.txt_OA_CONum = new TextBox();
      this.ln_OA_CONum = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.bunifuCustomLabel9 = new BunifuCustomLabel();
      this.bunifuCustomLabel7 = new BunifuCustomLabel();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_OA_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.gb_OA_CDetails = new GroupBox();
      this.txt_OA_CName = new BunifuMaterialTextbox();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.txt_OA_CCode = new BunifuMaterialTextbox();
      this.btn_OA_Done = new Button();
      this.backgroundWorker1 = new BackgroundWorker();
      this.btn_OA_Close = new Button();
      this.bunifuCustomLabel10 = new BunifuCustomLabel();
      this.btn_OA_Cancel = new Button();
      this.gb_OA_ODetails.SuspendLayout();
      this.panel6.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.gb_OA_CDetails.SuspendLayout();
      this.SuspendLayout();
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
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_OA_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(12, 119);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(735, 165);
      this.gb_OA_ODetails.TabIndex = 0;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Order Details";
      this.panel6.Controls.Add((Control) this.txt_OA_QNum);
      this.panel6.Controls.Add((Control) this.ln_OA_QNum);
      this.panel6.Location = new Point(165, 118);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(156, 26);
      this.panel6.TabIndex = 64;
      this.txt_OA_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OA_QNum.BackColor = Color.Silver;
      this.txt_OA_QNum.BorderStyle = BorderStyle.None;
      this.txt_OA_QNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_QNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_QNum.Location = new Point(2, 6);
      this.txt_OA_QNum.Name = "txt_OA_QNum";
      this.txt_OA_QNum.Size = new Size(153, 16);
      this.txt_OA_QNum.TabIndex = 7;
      this.txt_OA_QNum.Leave += new EventHandler(this.txt_OA_QNum_Leave);
      this.txt_OA_QNum.MouseEnter += new EventHandler(this.txt_OA_QNum_MouseEnter);
      this.txt_OA_QNum.MouseLeave += new EventHandler(this.txt_OA_QNum_MouseLeave);
      this.ln_OA_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OA_QNum.BackColor = Color.Transparent;
      this.ln_OA_QNum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OA_QNum.LineThickness = 1;
      this.ln_OA_QNum.Location = new Point(-1, 18);
      this.ln_OA_QNum.Name = "ln_OA_QNum";
      this.ln_OA_QNum.Size = new Size(158, 10);
      this.ln_OA_QNum.TabIndex = 52;
      this.ln_OA_QNum.TabStop = false;
      this.ln_OA_QNum.Transparency = (int) byte.MaxValue;
      this.ln_OA_QNum.Vertical = false;
      this.panel5.Controls.Add((Control) this.txt_OA_PercRec);
      this.panel5.Controls.Add((Control) this.ln_OA_PercRec);
      this.panel5.Location = new Point(650, 85);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(61, 26);
      this.panel5.TabIndex = 63;
      this.txt_OA_PercRec.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OA_PercRec.BackColor = Color.Silver;
      this.txt_OA_PercRec.BorderStyle = BorderStyle.None;
      this.txt_OA_PercRec.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_PercRec.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_PercRec.Location = new Point(3, 7);
      this.txt_OA_PercRec.Name = "txt_OA_PercRec";
      this.txt_OA_PercRec.Size = new Size(58, 16);
      this.txt_OA_PercRec.TabIndex = 6;
      this.txt_OA_PercRec.Enter += new EventHandler(this.txt_OA_Perc_Rec_Enter);
      this.txt_OA_PercRec.Leave += new EventHandler(this.txt_OA_PercRec_Leave);
      this.txt_OA_PercRec.MouseEnter += new EventHandler(this.txt_OA_PercRec_MouseEnter);
      this.txt_OA_PercRec.MouseLeave += new EventHandler(this.txt_OA_PercRec_MouseLeave);
      this.txt_OA_PercRec.Validating += new CancelEventHandler(this.txt_OA_Perc_Rec_Validating);
      this.ln_OA_PercRec.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OA_PercRec.BackColor = Color.Transparent;
      this.ln_OA_PercRec.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OA_PercRec.LineThickness = 1;
      this.ln_OA_PercRec.Location = new Point(-1, 18);
      this.ln_OA_PercRec.Name = "ln_OA_PercRec";
      this.ln_OA_PercRec.Size = new Size(63, 10);
      this.ln_OA_PercRec.TabIndex = 52;
      this.ln_OA_PercRec.TabStop = false;
      this.ln_OA_PercRec.Transparency = (int) byte.MaxValue;
      this.ln_OA_PercRec.Vertical = false;
      this.panel4.Controls.Add((Control) this.txt_OA_PercInv);
      this.panel4.Controls.Add((Control) this.ln_OA_PercInv);
      this.panel4.Location = new Point(436, 85);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(61, 26);
      this.panel4.TabIndex = 62;
      this.txt_OA_PercInv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OA_PercInv.BackColor = Color.Silver;
      this.txt_OA_PercInv.BorderStyle = BorderStyle.None;
      this.txt_OA_PercInv.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_PercInv.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_PercInv.Location = new Point(3, 7);
      this.txt_OA_PercInv.Name = "txt_OA_PercInv";
      this.txt_OA_PercInv.Size = new Size(58, 16);
      this.txt_OA_PercInv.TabIndex = 5;
      this.txt_OA_PercInv.Enter += new EventHandler(this.txt_OA_Perc_Inv_Enter);
      this.txt_OA_PercInv.Leave += new EventHandler(this.txt_OA_PercInv_Leave);
      this.txt_OA_PercInv.MouseEnter += new EventHandler(this.txt_OA_PercInv_MouseEnter);
      this.txt_OA_PercInv.MouseLeave += new EventHandler(this.txt_OA_PercInv_MouseLeave);
      this.txt_OA_PercInv.Validating += new CancelEventHandler(this.txt_OA_Perc_Inv_Validating);
      this.ln_OA_PercInv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OA_PercInv.BackColor = Color.Transparent;
      this.ln_OA_PercInv.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OA_PercInv.LineThickness = 1;
      this.ln_OA_PercInv.Location = new Point(-1, 18);
      this.ln_OA_PercInv.Name = "ln_OA_PercInv";
      this.ln_OA_PercInv.Size = new Size(63, 10);
      this.ln_OA_PercInv.TabIndex = 52;
      this.ln_OA_PercInv.TabStop = false;
      this.ln_OA_PercInv.Transparency = (int) byte.MaxValue;
      this.ln_OA_PercInv.Vertical = false;
      this.panel3.Controls.Add((Control) this.txt_OA_Amt);
      this.panel3.Controls.Add((Control) this.ln_OA_Amt);
      this.panel3.Location = new Point(165, 85);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(118, 26);
      this.panel3.TabIndex = 61;
      this.txt_OA_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OA_Amt.BackColor = Color.Silver;
      this.txt_OA_Amt.BorderStyle = BorderStyle.None;
      this.txt_OA_Amt.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_Amt.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_Amt.Location = new Point(2, 7);
      this.txt_OA_Amt.Name = "txt_OA_Amt";
      this.txt_OA_Amt.Size = new Size(115, 16);
      this.txt_OA_Amt.TabIndex = 4;
      this.txt_OA_Amt.TextChanged += new EventHandler(this.txt_OA_Amt_TextChanged);
      this.txt_OA_Amt.Leave += new EventHandler(this.txt_OA_Amt_Leave);
      this.txt_OA_Amt.MouseEnter += new EventHandler(this.txt_OA_Amt_MouseEnter);
      this.txt_OA_Amt.MouseLeave += new EventHandler(this.txt_OA_Amt_MouseLeave);
      this.ln_OA_Amt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OA_Amt.BackColor = Color.Transparent;
      this.ln_OA_Amt.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OA_Amt.LineThickness = 1;
      this.ln_OA_Amt.Location = new Point(-1, 18);
      this.ln_OA_Amt.Name = "ln_OA_Amt";
      this.ln_OA_Amt.Size = new Size(120, 10);
      this.ln_OA_Amt.TabIndex = 52;
      this.ln_OA_Amt.TabStop = false;
      this.ln_OA_Amt.Transparency = (int) byte.MaxValue;
      this.ln_OA_Amt.Vertical = false;
      this.panel2.Controls.Add((Control) this.txt_OA_Desc);
      this.panel2.Controls.Add((Control) this.ln_OA_Desc);
      this.panel2.Location = new Point(165, 53);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(547, 27);
      this.panel2.TabIndex = 60;
      this.txt_OA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OA_Desc.BackColor = Color.Silver;
      this.txt_OA_Desc.BorderStyle = BorderStyle.None;
      this.txt_OA_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_Desc.Location = new Point(2, 7);
      this.txt_OA_Desc.Name = "txt_OA_Desc";
      this.txt_OA_Desc.Size = new Size(544, 16);
      this.txt_OA_Desc.TabIndex = 3;
      this.txt_OA_Desc.Leave += new EventHandler(this.txt_OA_Desc_Leave);
      this.txt_OA_Desc.MouseEnter += new EventHandler(this.txt_OA_Desc_MouseEnter);
      this.txt_OA_Desc.MouseLeave += new EventHandler(this.txt_OA_Desc_MouseLeave);
      this.ln_OA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OA_Desc.BackColor = Color.Transparent;
      this.ln_OA_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OA_Desc.LineThickness = 1;
      this.ln_OA_Desc.Location = new Point(-1, 18);
      this.ln_OA_Desc.Name = "ln_OA_Desc";
      this.ln_OA_Desc.Size = new Size(549, 10);
      this.ln_OA_Desc.TabIndex = 0;
      this.ln_OA_Desc.TabStop = false;
      this.ln_OA_Desc.Transparency = (int) byte.MaxValue;
      this.ln_OA_Desc.Vertical = false;
      this.panel1.Controls.Add((Control) this.txt_OA_CONum);
      this.panel1.Controls.Add((Control) this.ln_OA_CONum);
      this.panel1.Location = new Point(165, 19);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(210, 27);
      this.panel1.TabIndex = 52;
      this.txt_OA_CONum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_OA_CONum.BackColor = Color.Silver;
      this.txt_OA_CONum.BorderStyle = BorderStyle.None;
      this.txt_OA_CONum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_CONum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_CONum.Location = new Point(2, 6);
      this.txt_OA_CONum.Name = "txt_OA_CONum";
      this.txt_OA_CONum.Size = new Size(208, 16);
      this.txt_OA_CONum.TabIndex = 1;
      this.txt_OA_CONum.Leave += new EventHandler(this.txt_OA_CONum_Leave);
      this.txt_OA_CONum.MouseEnter += new EventHandler(this.txt_OA_CONum_MouseEnter);
      this.txt_OA_CONum.MouseLeave += new EventHandler(this.txt_OA_CONum_MouseLeave);
      this.ln_OA_CONum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_OA_CONum.BackColor = Color.Transparent;
      this.ln_OA_CONum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_OA_CONum.LineThickness = 1;
      this.ln_OA_CONum.Location = new Point(-1, 18);
      this.ln_OA_CONum.Name = "ln_OA_CONum";
      this.ln_OA_CONum.Size = new Size(212, 10);
      this.ln_OA_CONum.TabIndex = 52;
      this.ln_OA_CONum.TabStop = false;
      this.ln_OA_CONum.Transparency = (int) byte.MaxValue;
      this.ln_OA_CONum.Vertical = false;
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
      this.bunifuCustomLabel9.Location = new Point(563, 91);
      this.bunifuCustomLabel9.Name = "bunifuCustomLabel9";
      this.bunifuCustomLabel9.Size = new Size(87, 17);
      this.bunifuCustomLabel9.TabIndex = 0;
      this.bunifuCustomLabel9.Text = "% Recieved:";
      this.bunifuCustomLabel7.AutoSize = true;
      this.bunifuCustomLabel7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel7.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel7.Location = new Point(352, 91);
      this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
      this.bunifuCustomLabel7.Size = new Size(80, 17);
      this.bunifuCustomLabel7.TabIndex = 0;
      this.bunifuCustomLabel7.Text = "% Invoiced:";
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(97, 91);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(60, 17);
      this.bunifuCustomLabel6.TabIndex = 0;
      this.bunifuCustomLabel6.Text = "Amount:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(78, 59);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Description:";
      this.dtp_OA_Date.BackColor = Color.Silver;
      this.dtp_OA_Date.BorderRadius = 0;
      this.dtp_OA_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_OA_Date.Format = DateTimePickerFormat.Short;
      this.dtp_OA_Date.FormatCustom = (string) null;
      this.dtp_OA_Date.Location = new Point(474, 21);
      this.dtp_OA_Date.Name = "dtp_OA_Date";
      this.dtp_OA_Date.Size = new Size(238, 25);
      this.dtp_OA_Date.TabIndex = 2;
      this.dtp_OA_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(426, 24);
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
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_OA_CName);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel4);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_OA_CCode);
      this.gb_OA_CDetails.FlatStyle = FlatStyle.Flat;
      this.gb_OA_CDetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_CDetails.Location = new Point(12, 41);
      this.gb_OA_CDetails.Name = "gb_OA_CDetails";
      this.gb_OA_CDetails.Size = new Size(735, 59);
      this.gb_OA_CDetails.TabIndex = 0;
      this.gb_OA_CDetails.TabStop = false;
      this.gb_OA_CDetails.Text = "Client Details";
      this.txt_OA_CName.Cursor = Cursors.IBeam;
      this.txt_OA_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_CName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_CName.HintForeColor = Color.Empty;
      this.txt_OA_CName.HintText = "";
      this.txt_OA_CName.isPassword = false;
      this.txt_OA_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_OA_CName.LineIdleColor = Color.Gray;
      this.txt_OA_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_OA_CName.LineThickness = 1;
      this.txt_OA_CName.Location = new Point(489, 15);
      this.txt_OA_CName.Margin = new Padding(4);
      this.txt_OA_CName.Name = "txt_OA_CName";
      this.txt_OA_CName.Size = new Size(223, 30);
      this.txt_OA_CName.TabIndex = 46;
      this.txt_OA_CName.TabStop = false;
      this.txt_OA_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_OA_CName.KeyDown += new KeyEventHandler(this.txt_OA_CName_KeyDown);
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
      this.txt_OA_CCode.Cursor = Cursors.IBeam;
      this.txt_OA_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_OA_CCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_OA_CCode.HintForeColor = Color.Empty;
      this.txt_OA_CCode.HintText = "";
      this.txt_OA_CCode.isPassword = false;
      this.txt_OA_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_OA_CCode.LineIdleColor = Color.Gray;
      this.txt_OA_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_OA_CCode.LineThickness = 1;
      this.txt_OA_CCode.Location = new Point(108, 15);
      this.txt_OA_CCode.Margin = new Padding(4);
      this.txt_OA_CCode.Name = "txt_OA_CCode";
      this.txt_OA_CCode.Size = new Size(223, 30);
      this.txt_OA_CCode.TabIndex = 45;
      this.txt_OA_CCode.TabStop = false;
      this.txt_OA_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_OA_CCode.KeyDown += new KeyEventHandler(this.txt_OA_CCode_KeyDown);
      this.btn_OA_Done.FlatAppearance.BorderSize = 0;
      this.btn_OA_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_OA_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_OA_Done.FlatStyle = FlatStyle.Flat;
      this.btn_OA_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_OA_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_OA_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_OA_Done.Location = new Point(513, 290);
      this.btn_OA_Done.Name = "btn_OA_Done";
      this.btn_OA_Done.Size = new Size(114, 40);
      this.btn_OA_Done.TabIndex = 8;
      this.btn_OA_Done.Text = "Done";
      this.btn_OA_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_OA_Done.UseVisualStyleBackColor = true;
      this.btn_OA_Done.Click += new EventHandler(this.btn_OA_Done_Click);
      this.btn_OA_Done.MouseEnter += new EventHandler(this.btn_OA_Done_MouseEnter);
      this.btn_OA_Done.MouseLeave += new EventHandler(this.btn_OA_Done_MouseLeave);
      this.btn_OA_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_OA_Close.BackColor = Color.Silver;
      this.btn_OA_Close.FlatAppearance.BorderSize = 0;
      this.btn_OA_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_OA_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_OA_Close.FlatStyle = FlatStyle.Flat;
      this.btn_OA_Close.Image = (Image) Resources.close_black;
      this.btn_OA_Close.Location = new Point(724, 5);
      this.btn_OA_Close.Name = "btn_OA_Close";
      this.btn_OA_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_OA_Close.Size = new Size(31, 29);
      this.btn_OA_Close.TabIndex = 50;
      this.btn_OA_Close.TabStop = false;
      this.btn_OA_Close.UseVisualStyleBackColor = false;
      this.btn_OA_Close.Click += new EventHandler(this.btn_OA_Close_Click);
      this.btn_OA_Close.MouseEnter += new EventHandler(this.btn_OA_Close_MouseEnter);
      this.btn_OA_Close.MouseLeave += new EventHandler(this.btn_OA_Close_MouseLeave);
      this.bunifuCustomLabel10.AutoSize = true;
      this.bunifuCustomLabel10.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel10.ForeColor = Color.FromArgb(15, 91, 142);
      this.bunifuCustomLabel10.Location = new Point(307, 10);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new Size(134, 22);
      this.bunifuCustomLabel10.TabIndex = 0;
      this.bunifuCustomLabel10.Text = "Add New Order";
      this.btn_OA_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_OA_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_OA_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_OA_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_OA_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_OA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_OA_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_OA_Cancel.Location = new Point(633, 290);
      this.btn_OA_Cancel.Name = "btn_OA_Cancel";
      this.btn_OA_Cancel.Size = new Size(114, 40);
      this.btn_OA_Cancel.TabIndex = 9;
      this.btn_OA_Cancel.Text = "Cancel";
      this.btn_OA_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_OA_Cancel.UseVisualStyleBackColor = true;
      this.btn_OA_Cancel.Click += new EventHandler(this.btn_OA_Cancel_Click);
      this.btn_OA_Cancel.MouseEnter += new EventHandler(this.btn_OA_Cancel_MouseEnter);
      this.btn_OA_Cancel.MouseLeave += new EventHandler(this.btn_OA_Cancel_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.BackgroundImageLayout = ImageLayout.None;
      this.ClientSize = new Size(758, 343);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_OA_Close);
      this.Controls.Add((Control) this.btn_OA_Cancel);
      this.Controls.Add((Control) this.btn_OA_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.Controls.Add((Control) this.gb_OA_CDetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(758, 343);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(758, 343);
      this.Name = nameof (O_Add);
      this.Padding = new Padding(20, 30, 20, 20);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Date";
      this.Load += new EventHandler(this.O_Add_Load);
      this.MouseDown += new MouseEventHandler(this.O_Add_MouseDown);
      this.MouseMove += new MouseEventHandler(this.O_Add_MouseMove);
      this.MouseUp += new MouseEventHandler(this.O_Add_MouseUp);
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
