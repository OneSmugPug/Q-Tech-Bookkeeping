// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Q_Add
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
using System.Text;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Q_Add : Form
  {
    private DataTable dt = (DataTable) null;
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private Point lastLocation;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_QA_Close;
    private GroupBox gb_OA_CDetails;
    private BunifuMaterialTextbox txt_QA_CName;
    private BunifuCustomLabel bunifuCustomLabel4;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuMaterialTextbox txt_QA_CCode;
    private Button btn_QA_Cancel;
    private Button btn_QA_Done;
    private GroupBox gb_OA_ODetails;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCheckbox cb_QA_OrderPlaced;
    private Panel panel2;
    private TextBox txt_QA_Desc;
    private BunifuSeparator ln_QA_Desc;
    private Panel panel1;
    private TextBox txt_QA_QNum;
    private BunifuSeparator ln_QA_CONum;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_QA_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;

    public Q_Add()
    {
      this.InitializeComponent();
    }

    private void Q_Add_Load(object sender, EventArgs e)
    {
      Home owner = (Home) this.Owner;
      if (owner.getCurPanel() == "pnl_L_Quotes")
      {
        Quotes curForm = (Quotes) owner.getCurForm();
        this.txt_QA_CCode.Text = curForm.getCCode();
        this.txt_QA_CName.Text = curForm.getCName();
        this.dt = curForm.getQuotes();
      }
      else
      {
        Int_Quotes curForm = (Int_Quotes) owner.getCurForm();
        this.txt_QA_CCode.Text = curForm.getCCode();
        this.txt_QA_CName.Text = curForm.getCName();
        this.dt = curForm.getQuotes();
      }
      int num1 = 0;
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row.RowState == DataRowState.Deleted)
        {
          string str = row["Quote_Number", DataRowVersion.Original].ToString().Trim();
          int num2 = str.IndexOf("_");
          int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
          if (int32 > num1)
            num1 = int32;
        }
        else
        {
          string str = row["Quote_Number"].ToString().Trim();
          int num2 = str.IndexOf("_");
          int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
          if (int32 > num1)
            num1 = int32;
        }
      }
      this.txt_QA_QNum.Text = this.txt_QA_CCode.Text + "_Q" + (num1 + 1).ToString("000");
      this.txt_QA_Desc.Focus();
    }

    private void btn_QA_Done_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add quote with Quote Number: ").Append(this.txt_QA_QNum.Text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        try
        {
          using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Quotes_Send VALUES (@QNum, @Date, @Client, @Desc, @OPlaced)", dbConnection))
          {
            sqlCommand.Parameters.AddWithValue("@QNum", (object) this.txt_QA_QNum.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_QA_Date.Value);
            sqlCommand.Parameters.AddWithValue("@Client", (object) this.txt_QA_CName.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_QA_Desc.Text.Trim());
            if (this.cb_QA_OrderPlaced.Checked)
              sqlCommand.Parameters.AddWithValue("@OPlaced", (object) "Yes");
            else
              sqlCommand.Parameters.AddWithValue("@OPlaced", (object) "No");
            sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("New quote successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void btn_QA_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_QA_ONum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_QA_CONum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_QA_ONum_Leave(object sender, EventArgs e)
    {
      this.ln_QA_CONum.LineColor = Color.Gray;
    }

    private void txt_QA_ONum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_QA_QNum.Focused)
        return;
      this.ln_QA_CONum.LineColor = Color.Gray;
    }

    private void txt_QA_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_QA_Desc.LineColor = Color.Gray;
    }

    private void txt_QA_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_QA_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_QA_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_QA_Desc.Focused)
        return;
      this.ln_QA_Desc.LineColor = Color.Gray;
    }

    private void btn_QA_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_QA_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_QA_Close.Image = (Image) Resources.close_white;
    }

    private void btn_QA_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_QA_Close.Image = (Image) Resources.close_black;
    }

    private void btn_QA_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_QA_Done.ForeColor = Color.White;
    }

    private void btn_QA_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_QA_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_QA_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_QA_Cancel.ForeColor = Color.White;
    }

    private void btn_QA_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_QA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_QA_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_QA_CName_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void Q_Add_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void Q_Add_MouseMove(object sender, MouseEventArgs e)
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

    private void Q_Add_MouseUp(object sender, MouseEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Q_Add));
      this.bunifuCustomLabel10 = new BunifuCustomLabel();
      this.btn_QA_Close = new Button();
      this.gb_OA_CDetails = new GroupBox();
      this.txt_QA_CName = new BunifuMaterialTextbox();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.txt_QA_CCode = new BunifuMaterialTextbox();
      this.btn_QA_Cancel = new Button();
      this.btn_QA_Done = new Button();
      this.gb_OA_ODetails = new GroupBox();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.cb_QA_OrderPlaced = new BunifuCheckbox();
      this.panel2 = new Panel();
      this.txt_QA_Desc = new TextBox();
      this.ln_QA_Desc = new BunifuSeparator();
      this.panel1 = new Panel();
      this.txt_QA_QNum = new TextBox();
      this.ln_QA_CONum = new BunifuSeparator();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_QA_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.gb_OA_CDetails.SuspendLayout();
      this.gb_OA_ODetails.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.bunifuCustomLabel10.AutoSize = true;
      this.bunifuCustomLabel10.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel10.ForeColor = Color.FromArgb(15, 91, 142);
      this.bunifuCustomLabel10.Location = new Point(317, 6);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new Size(137, 22);
      this.bunifuCustomLabel10.TabIndex = 51;
      this.bunifuCustomLabel10.Text = "Add New Quote";
      this.btn_QA_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_QA_Close.BackColor = Color.Silver;
      this.btn_QA_Close.FlatAppearance.BorderSize = 0;
      this.btn_QA_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_QA_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_QA_Close.FlatStyle = FlatStyle.Flat;
      this.btn_QA_Close.Image = (Image) Resources.close_black;
      this.btn_QA_Close.Location = new Point(723, 5);
      this.btn_QA_Close.Name = "btn_QA_Close";
      this.btn_QA_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_QA_Close.Size = new Size(31, 29);
      this.btn_QA_Close.TabIndex = 53;
      this.btn_QA_Close.TabStop = false;
      this.btn_QA_Close.UseVisualStyleBackColor = false;
      this.btn_QA_Close.Click += new EventHandler(this.btn_QA_Close_Click);
      this.btn_QA_Close.MouseEnter += new EventHandler(this.btn_QA_Close_MouseEnter);
      this.btn_QA_Close.MouseLeave += new EventHandler(this.btn_QA_Close_MouseLeave);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_QA_CName);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel4);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_QA_CCode);
      this.gb_OA_CDetails.FlatStyle = FlatStyle.Flat;
      this.gb_OA_CDetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_CDetails.Location = new Point(11, 41);
      this.gb_OA_CDetails.Name = "gb_OA_CDetails";
      this.gb_OA_CDetails.Size = new Size(735, 59);
      this.gb_OA_CDetails.TabIndex = 52;
      this.gb_OA_CDetails.TabStop = false;
      this.gb_OA_CDetails.Text = "Client Details";
      this.txt_QA_CName.Cursor = Cursors.IBeam;
      this.txt_QA_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_QA_CName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_QA_CName.HintForeColor = Color.Empty;
      this.txt_QA_CName.HintText = "";
      this.txt_QA_CName.isPassword = false;
      this.txt_QA_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_QA_CName.LineIdleColor = Color.Gray;
      this.txt_QA_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_QA_CName.LineThickness = 1;
      this.txt_QA_CName.Location = new Point(489, 15);
      this.txt_QA_CName.Margin = new Padding(4);
      this.txt_QA_CName.Name = "txt_QA_CName";
      this.txt_QA_CName.Size = new Size(223, 30);
      this.txt_QA_CName.TabIndex = 46;
      this.txt_QA_CName.TabStop = false;
      this.txt_QA_CName.TextAlign = HorizontalAlignment.Left;
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
      this.txt_QA_CCode.Cursor = Cursors.IBeam;
      this.txt_QA_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_QA_CCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_QA_CCode.HintForeColor = Color.Empty;
      this.txt_QA_CCode.HintText = "";
      this.txt_QA_CCode.isPassword = false;
      this.txt_QA_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_QA_CCode.LineIdleColor = Color.Gray;
      this.txt_QA_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_QA_CCode.LineThickness = 1;
      this.txt_QA_CCode.Location = new Point(108, 15);
      this.txt_QA_CCode.Margin = new Padding(4);
      this.txt_QA_CCode.Name = "txt_QA_CCode";
      this.txt_QA_CCode.Size = new Size(223, 30);
      this.txt_QA_CCode.TabIndex = 45;
      this.txt_QA_CCode.TabStop = false;
      this.txt_QA_CCode.TextAlign = HorizontalAlignment.Left;
      this.btn_QA_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_QA_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_QA_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_QA_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_QA_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_QA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_QA_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_QA_Cancel.Location = new Point(632, 251);
      this.btn_QA_Cancel.Name = "btn_QA_Cancel";
      this.btn_QA_Cancel.Size = new Size(114, 40);
      this.btn_QA_Cancel.TabIndex = 6;
      this.btn_QA_Cancel.Text = "Cancel";
      this.btn_QA_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_QA_Cancel.UseVisualStyleBackColor = true;
      this.btn_QA_Cancel.Click += new EventHandler(this.btn_QA_Cancel_Click);
      this.btn_QA_Cancel.MouseEnter += new EventHandler(this.btn_QA_Cancel_MouseEnter);
      this.btn_QA_Cancel.MouseLeave += new EventHandler(this.btn_QA_Cancel_MouseLeave);
      this.btn_QA_Done.FlatAppearance.BorderSize = 0;
      this.btn_QA_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_QA_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_QA_Done.FlatStyle = FlatStyle.Flat;
      this.btn_QA_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_QA_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_QA_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_QA_Done.Location = new Point(512, 251);
      this.btn_QA_Done.Name = "btn_QA_Done";
      this.btn_QA_Done.Size = new Size(114, 40);
      this.btn_QA_Done.TabIndex = 5;
      this.btn_QA_Done.Text = "Done";
      this.btn_QA_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_QA_Done.UseVisualStyleBackColor = true;
      this.btn_QA_Done.Click += new EventHandler(this.btn_QA_Done_Click);
      this.btn_QA_Done.MouseEnter += new EventHandler(this.btn_QA_Done_MouseEnter);
      this.btn_QA_Done.MouseLeave += new EventHandler(this.btn_QA_Done_MouseLeave);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.cb_QA_OrderPlaced);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel1);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_QA_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(12, 119);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(735, 126);
      this.gb_OA_ODetails.TabIndex = 54;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Quote Details";
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(337, 88);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(92, 17);
      this.bunifuCustomLabel6.TabIndex = 62;
      this.bunifuCustomLabel6.Text = "Order Placed";
      this.cb_QA_OrderPlaced.BackColor = Color.FromArgb(132, 135, 140);
      this.cb_QA_OrderPlaced.ChechedOffColor = Color.FromArgb(132, 135, 140);
      this.cb_QA_OrderPlaced.Checked = false;
      this.cb_QA_OrderPlaced.CheckedOnColor = Color.FromArgb(15, 91, 142);
      this.cb_QA_OrderPlaced.ForeColor = Color.White;
      this.cb_QA_OrderPlaced.Location = new Point(311, 88);
      this.cb_QA_OrderPlaced.Name = "cb_QA_OrderPlaced";
      this.cb_QA_OrderPlaced.Size = new Size(20, 20);
      this.cb_QA_OrderPlaced.TabIndex = 4;
      this.panel2.Controls.Add((Control) this.txt_QA_Desc);
      this.panel2.Controls.Add((Control) this.ln_QA_Desc);
      this.panel2.Location = new Point(125, 57);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(586, 27);
      this.panel2.TabIndex = 60;
      this.txt_QA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_QA_Desc.BackColor = Color.Silver;
      this.txt_QA_Desc.BorderStyle = BorderStyle.None;
      this.txt_QA_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_QA_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_QA_Desc.Location = new Point(2, 5);
      this.txt_QA_Desc.Name = "txt_QA_Desc";
      this.txt_QA_Desc.Size = new Size(583, 16);
      this.txt_QA_Desc.TabIndex = 3;
      this.txt_QA_Desc.Leave += new EventHandler(this.txt_QA_Desc_Leave);
      this.txt_QA_Desc.MouseEnter += new EventHandler(this.txt_QA_Desc_MouseEnter);
      this.txt_QA_Desc.MouseLeave += new EventHandler(this.txt_QA_Desc_MouseLeave);
      this.ln_QA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_QA_Desc.BackColor = Color.Transparent;
      this.ln_QA_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_QA_Desc.LineThickness = 1;
      this.ln_QA_Desc.Location = new Point(-1, 18);
      this.ln_QA_Desc.Name = "ln_QA_Desc";
      this.ln_QA_Desc.Size = new Size(588, 10);
      this.ln_QA_Desc.TabIndex = 0;
      this.ln_QA_Desc.TabStop = false;
      this.ln_QA_Desc.Transparency = (int) byte.MaxValue;
      this.ln_QA_Desc.Vertical = false;
      this.panel1.Controls.Add((Control) this.txt_QA_QNum);
      this.panel1.Controls.Add((Control) this.ln_QA_CONum);
      this.panel1.Location = new Point(125, 23);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(177, 27);
      this.panel1.TabIndex = 52;
      this.txt_QA_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_QA_QNum.BackColor = Color.Silver;
      this.txt_QA_QNum.BorderStyle = BorderStyle.None;
      this.txt_QA_QNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_QA_QNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_QA_QNum.Location = new Point(2, 4);
      this.txt_QA_QNum.Name = "txt_QA_QNum";
      this.txt_QA_QNum.ReadOnly = true;
      this.txt_QA_QNum.Size = new Size(175, 16);
      this.txt_QA_QNum.TabIndex = 1;
      this.txt_QA_QNum.Leave += new EventHandler(this.txt_QA_ONum_Leave);
      this.txt_QA_QNum.MouseEnter += new EventHandler(this.txt_QA_ONum_MouseEnter);
      this.txt_QA_QNum.MouseLeave += new EventHandler(this.txt_QA_ONum_MouseLeave);
      this.ln_QA_CONum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_QA_CONum.BackColor = Color.Transparent;
      this.ln_QA_CONum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_QA_CONum.LineThickness = 1;
      this.ln_QA_CONum.Location = new Point(-1, 18);
      this.ln_QA_CONum.Name = "ln_QA_CONum";
      this.ln_QA_CONum.Size = new Size(179, 10);
      this.ln_QA_CONum.TabIndex = 52;
      this.ln_QA_CONum.TabStop = false;
      this.ln_QA_CONum.Transparency = (int) byte.MaxValue;
      this.ln_QA_CONum.Vertical = false;
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(38, 61);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Description:";
      this.dtp_QA_Date.BackColor = Color.Silver;
      this.dtp_QA_Date.BorderRadius = 0;
      this.dtp_QA_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_QA_Date.Format = DateTimePickerFormat.Short;
      this.dtp_QA_Date.FormatCustom = (string) null;
      this.dtp_QA_Date.Location = new Point(462, 25);
      this.dtp_QA_Date.Name = "dtp_QA_Date";
      this.dtp_QA_Date.Size = new Size(238, 25);
      this.dtp_QA_Date.TabIndex = 2;
      this.dtp_QA_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(381, 26);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(79, 17);
      this.bunifuCustomLabel2.TabIndex = 0;
      this.bunifuCustomLabel2.Text = "Date Send:";
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(16, 25);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(105, 17);
      this.bunifuCustomLabel1.TabIndex = 0;
      this.bunifuCustomLabel1.Text = "Quote Number:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(758, 303);
      this.Controls.Add((Control) this.btn_QA_Cancel);
      this.Controls.Add((Control) this.btn_QA_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_QA_Close);
      this.Controls.Add((Control) this.gb_OA_CDetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(758, 343);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(758, 303);
      this.Name = nameof (Q_Add);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Add Quote Sent";
      this.Load += new EventHandler(this.Q_Add_Load);
      this.MouseDown += new MouseEventHandler(this.Q_Add_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Q_Add_MouseMove);
      this.MouseUp += new MouseEventHandler(this.Q_Add_MouseUp);
      this.gb_OA_CDetails.ResumeLayout(false);
      this.gb_OA_CDetails.PerformLayout();
      this.gb_OA_ODetails.ResumeLayout(false);
      this.gb_OA_ODetails.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
