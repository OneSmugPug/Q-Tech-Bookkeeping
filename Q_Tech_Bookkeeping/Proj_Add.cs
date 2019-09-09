// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Proj_Add
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
  public class Proj_Add : Form
  {
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private DataTable dt;
    private DataTable projDT;
    private Point lastLocation;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_PA_Close;
    private Button btn_PA_Cancel;
    private Button btn_PA_Done;
    private GroupBox gb_OA_ODetails;
    private Panel panel6;
    private TextBox txt_PA_QNum;
    private BunifuSeparator ln_PA_QNum;
    private Panel panel2;
    private TextBox txt_PA_Desc;
    private BunifuSeparator ln_PA_Desc;
    private Panel panel1;
    private TextBox txt_PA_ProjCode;
    private BunifuSeparator ln_PA_ProjCode;
    private BunifuCustomLabel bunifuCustomLabel8;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_PA_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;
    private GroupBox gb_OA_CDetails;
    private BunifuMaterialTextbox txt_PA_CName;
    private BunifuCustomLabel bunifuCustomLabel4;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuDropdown ddb_PA_CCode;

    public Proj_Add()
    {
      this.InitializeComponent();
    }

    private void Proj_Add_Load(object sender, EventArgs e)
    {
      this.dtp_PA_Date.Value = DateTime.Now;
      this.loadClients();
    }

    private void loadClients()
    {
      this.dt = new DataTable();
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter("SELECT * FROM Clients", dbConnection);
        SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("SELECT * FROM Int_Clients", dbConnection);
        sqlDataAdapter1.Fill(this.dt);
        sqlDataAdapter2.Fill(this.dt);
      }
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
        this.ddb_PA_CCode.AddItem(row["Code"].ToString().Trim());
      this.ddb_PA_CCode.selectedIndex = 0;
    }

    private void ddb_PA_CCode_onItemSelected(object sender, EventArgs e)
    {
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row["Code"].ToString().Trim().Equals(this.ddb_PA_CCode.selectedValue))
          this.txt_PA_CName.Text = row["Name"].ToString().Trim();
      }
      this.projDT = ((Projects) ((Home) this.Owner).getCurForm()).getProjects();
      int num1 = 0;
      foreach (DataRow row in (InternalDataCollectionBase) this.projDT.Rows)
      {
        string[] strArray = row["Project_ID"].ToString().Trim().Split('_');
        int num2 = 0;
        if (strArray[1].Equals(this.ddb_PA_CCode.selectedValue))
          num2 = Convert.ToInt32(strArray[0].Remove(0, 1));
        if (num2 > num1)
          num1 = num2;
      }
      this.txt_PA_ProjCode.Text = "P" + (num1 + 1).ToString("000") + "_" + this.ddb_PA_CCode.selectedValue;
      DataTable dataTable;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Quotes_Send WHERE Client = '" + this.txt_PA_CName.Text.Trim() + "'", dbConnection);
        dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
      }
      int num3 = 1;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row.RowState == DataRowState.Deleted)
        {
          string str = row["Quote_Number", DataRowVersion.Original].ToString().Trim();
          int num2 = str.IndexOf("_");
          int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
          if (int32 > num3)
            num3 = int32;
        }
        else
        {
          string str = row["Quote_Number"].ToString().Trim();
          int num2 = str.IndexOf("_");
          int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
          if (int32 > num3)
            num3 = int32;
        }
      }
      this.txt_PA_QNum.Text = this.ddb_PA_CCode.selectedValue + "_Q" + (num3 + 1).ToString("000");
    }

    private void btn_PA_Done_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add project with project code: ").Append(this.txt_PA_ProjCode.Text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        try
        {
          using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Projects VALUES (@ProjID, @Date, @ClientCode, @ClientName, @Desc, @QNum)", dbConnection))
          {
            sqlCommand.Parameters.AddWithValue("@ProjID", (object) this.txt_PA_ProjCode.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_PA_Date.Value);
            sqlCommand.Parameters.AddWithValue("@ClientCode", (object) this.ddb_PA_CCode.selectedValue.Trim());
            sqlCommand.Parameters.AddWithValue("@ClientName", (object) this.txt_PA_CName.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_PA_Desc.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@QNum", (object) this.txt_PA_QNum.Text.Trim());
            sqlCommand.ExecuteNonQuery();
          }
          using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Quotes_Send(Quote_Number, Client) VALUES (@QNum, @Client)", dbConnection))
          {
            sqlCommand.Parameters.AddWithValue("@QNum", (object) this.txt_PA_QNum.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Client", (object) this.txt_PA_CName.Text.Trim());
            sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("New project successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void btn_PA_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_PA_ProjCode_MouseEnter(object sender, EventArgs e)
    {
      this.ln_PA_ProjCode.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_PA_ProjCode_Leave(object sender, EventArgs e)
    {
      this.ln_PA_ProjCode.LineColor = Color.Gray;
    }

    private void txt_PA_ProjCode_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_PA_ProjCode.Focused)
        return;
      this.ln_PA_ProjCode.LineColor = Color.Gray;
    }

    private void txt_PA_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_PA_Desc.LineColor = Color.Gray;
    }

    private void txt_PA_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_PA_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_PA_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_PA_Desc.Focused)
        return;
      this.ln_PA_Desc.LineColor = Color.Gray;
    }

    private void txt_PA_QNum_Leave(object sender, EventArgs e)
    {
      this.ln_PA_QNum.LineColor = Color.Gray;
    }

    private void txt_PA_QNum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_PA_QNum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_PA_QNum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_PA_QNum.Focused)
        return;
      this.ln_PA_QNum.LineColor = Color.Gray;
    }

    private void btn_PA_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_PA_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PA_Close.Image = (Image) Resources.close_white;
    }

    private void btn_PA_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PA_Close.Image = (Image) Resources.close_black;
    }

    private void btn_PA_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PA_Done.ForeColor = Color.White;
    }

    private void btn_PA_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PA_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_PA_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PA_Cancel.ForeColor = Color.White;
    }

    private void btn_PA_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void ddb_PA_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_PA_CName_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void Proj_Add_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void Proj_Add_MouseMove(object sender, MouseEventArgs e)
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

    private void Proj_Add_MouseUp(object sender, MouseEventArgs e)
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
      this.bunifuCustomLabel10 = new BunifuCustomLabel();
      this.btn_PA_Cancel = new Button();
      this.btn_PA_Done = new Button();
      this.gb_OA_ODetails = new GroupBox();
      this.panel6 = new Panel();
      this.txt_PA_QNum = new TextBox();
      this.ln_PA_QNum = new BunifuSeparator();
      this.panel2 = new Panel();
      this.txt_PA_Desc = new TextBox();
      this.ln_PA_Desc = new BunifuSeparator();
      this.panel1 = new Panel();
      this.txt_PA_ProjCode = new TextBox();
      this.ln_PA_ProjCode = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_PA_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.gb_OA_CDetails = new GroupBox();
      this.ddb_PA_CCode = new BunifuDropdown();
      this.txt_PA_CName = new BunifuMaterialTextbox();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.btn_PA_Close = new Button();
      this.gb_OA_ODetails.SuspendLayout();
      this.panel6.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.gb_OA_CDetails.SuspendLayout();
      this.SuspendLayout();
      this.bunifuCustomLabel10.AutoSize = true;
      this.bunifuCustomLabel10.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel10.ForeColor = Color.FromArgb(15, 91, 142);
      this.bunifuCustomLabel10.Location = new Point(294, 9);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new Size(144, 22);
      this.bunifuCustomLabel10.TabIndex = 51;
      this.bunifuCustomLabel10.Text = "Add New Project";
      this.btn_PA_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_PA_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_PA_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_PA_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_PA_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_PA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_PA_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_PA_Cancel.Location = new Point(595, (int) byte.MaxValue);
      this.btn_PA_Cancel.Name = "btn_PA_Cancel";
      this.btn_PA_Cancel.Size = new Size(114, 40);
      this.btn_PA_Cancel.TabIndex = 7;
      this.btn_PA_Cancel.Text = "Cancel";
      this.btn_PA_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_PA_Cancel.UseVisualStyleBackColor = true;
      this.btn_PA_Cancel.Click += new EventHandler(this.btn_PA_Cancel_Click);
      this.btn_PA_Cancel.MouseEnter += new EventHandler(this.btn_PA_Cancel_MouseEnter);
      this.btn_PA_Cancel.MouseLeave += new EventHandler(this.btn_PA_Cancel_MouseLeave);
      this.btn_PA_Done.FlatAppearance.BorderSize = 0;
      this.btn_PA_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_PA_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_PA_Done.FlatStyle = FlatStyle.Flat;
      this.btn_PA_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_PA_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_PA_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_PA_Done.Location = new Point(475, (int) byte.MaxValue);
      this.btn_PA_Done.Name = "btn_PA_Done";
      this.btn_PA_Done.Size = new Size(114, 40);
      this.btn_PA_Done.TabIndex = 6;
      this.btn_PA_Done.Text = "Done";
      this.btn_PA_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_PA_Done.UseVisualStyleBackColor = true;
      this.btn_PA_Done.Click += new EventHandler(this.btn_PA_Done_Click);
      this.btn_PA_Done.MouseEnter += new EventHandler(this.btn_PA_Done_MouseEnter);
      this.btn_PA_Done.MouseLeave += new EventHandler(this.btn_PA_Done_MouseLeave);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel1);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel8);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_PA_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(11, 120);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(698, 129);
      this.gb_OA_ODetails.TabIndex = 52;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Project Details";
      this.panel6.Controls.Add((Control) this.txt_PA_QNum);
      this.panel6.Controls.Add((Control) this.ln_PA_QNum);
      this.panel6.Location = new Point(129, 90);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(156, 26);
      this.panel6.TabIndex = 64;
      this.txt_PA_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_PA_QNum.BackColor = Color.Silver;
      this.txt_PA_QNum.BorderStyle = BorderStyle.None;
      this.txt_PA_QNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PA_QNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PA_QNum.Location = new Point(2, 6);
      this.txt_PA_QNum.Name = "txt_PA_QNum";
      this.txt_PA_QNum.ReadOnly = true;
      this.txt_PA_QNum.Size = new Size(153, 16);
      this.txt_PA_QNum.TabIndex = 5;
      this.txt_PA_QNum.Leave += new EventHandler(this.txt_PA_QNum_Leave);
      this.txt_PA_QNum.MouseEnter += new EventHandler(this.txt_PA_QNum_MouseEnter);
      this.txt_PA_QNum.MouseLeave += new EventHandler(this.txt_PA_QNum_MouseLeave);
      this.ln_PA_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_PA_QNum.BackColor = Color.Transparent;
      this.ln_PA_QNum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_PA_QNum.LineThickness = 1;
      this.ln_PA_QNum.Location = new Point(-1, 18);
      this.ln_PA_QNum.Name = "ln_PA_QNum";
      this.ln_PA_QNum.Size = new Size(158, 10);
      this.ln_PA_QNum.TabIndex = 52;
      this.ln_PA_QNum.TabStop = false;
      this.ln_PA_QNum.Transparency = (int) byte.MaxValue;
      this.ln_PA_QNum.Vertical = false;
      this.panel2.Controls.Add((Control) this.txt_PA_Desc);
      this.panel2.Controls.Add((Control) this.ln_PA_Desc);
      this.panel2.Location = new Point(129, 56);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(547, 27);
      this.panel2.TabIndex = 60;
      this.txt_PA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_PA_Desc.BackColor = Color.Silver;
      this.txt_PA_Desc.BorderStyle = BorderStyle.None;
      this.txt_PA_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PA_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PA_Desc.Location = new Point(2, 7);
      this.txt_PA_Desc.Name = "txt_PA_Desc";
      this.txt_PA_Desc.Size = new Size(544, 16);
      this.txt_PA_Desc.TabIndex = 4;
      this.txt_PA_Desc.Leave += new EventHandler(this.txt_PA_Desc_Leave);
      this.txt_PA_Desc.MouseEnter += new EventHandler(this.txt_PA_Desc_MouseEnter);
      this.txt_PA_Desc.MouseLeave += new EventHandler(this.txt_PA_Desc_MouseLeave);
      this.ln_PA_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_PA_Desc.BackColor = Color.Transparent;
      this.ln_PA_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_PA_Desc.LineThickness = 1;
      this.ln_PA_Desc.Location = new Point(-1, 18);
      this.ln_PA_Desc.Name = "ln_PA_Desc";
      this.ln_PA_Desc.Size = new Size(549, 10);
      this.ln_PA_Desc.TabIndex = 0;
      this.ln_PA_Desc.TabStop = false;
      this.ln_PA_Desc.Transparency = (int) byte.MaxValue;
      this.ln_PA_Desc.Vertical = false;
      this.panel1.Controls.Add((Control) this.txt_PA_ProjCode);
      this.panel1.Controls.Add((Control) this.ln_PA_ProjCode);
      this.panel1.Location = new Point(129, 23);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(210, 27);
      this.panel1.TabIndex = 52;
      this.txt_PA_ProjCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_PA_ProjCode.BackColor = Color.Silver;
      this.txt_PA_ProjCode.BorderStyle = BorderStyle.None;
      this.txt_PA_ProjCode.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PA_ProjCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PA_ProjCode.Location = new Point(2, 6);
      this.txt_PA_ProjCode.Name = "txt_PA_ProjCode";
      this.txt_PA_ProjCode.ReadOnly = true;
      this.txt_PA_ProjCode.Size = new Size(208, 16);
      this.txt_PA_ProjCode.TabIndex = 2;
      this.txt_PA_ProjCode.Leave += new EventHandler(this.txt_PA_ProjCode_Leave);
      this.txt_PA_ProjCode.MouseEnter += new EventHandler(this.txt_PA_ProjCode_MouseEnter);
      this.txt_PA_ProjCode.MouseLeave += new EventHandler(this.txt_PA_ProjCode_MouseLeave);
      this.ln_PA_ProjCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_PA_ProjCode.BackColor = Color.Transparent;
      this.ln_PA_ProjCode.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_PA_ProjCode.LineThickness = 1;
      this.ln_PA_ProjCode.Location = new Point(-1, 18);
      this.ln_PA_ProjCode.Name = "ln_PA_ProjCode";
      this.ln_PA_ProjCode.Size = new Size(212, 10);
      this.ln_PA_ProjCode.TabIndex = 52;
      this.ln_PA_ProjCode.TabStop = false;
      this.ln_PA_ProjCode.Transparency = (int) byte.MaxValue;
      this.ln_PA_ProjCode.Vertical = false;
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel8.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel8.Location = new Point(18, 95);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new Size(105, 17);
      this.bunifuCustomLabel8.TabIndex = 0;
      this.bunifuCustomLabel8.Text = "Quote Number:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(40, 62);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 0;
      this.bunifuCustomLabel5.Text = "Description:";
      this.dtp_PA_Date.BackColor = Color.Silver;
      this.dtp_PA_Date.BorderRadius = 0;
      this.dtp_PA_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_PA_Date.Format = DateTimePickerFormat.Short;
      this.dtp_PA_Date.FormatCustom = (string) null;
      this.dtp_PA_Date.Location = new Point(438, 26);
      this.dtp_PA_Date.Name = "dtp_PA_Date";
      this.dtp_PA_Date.Size = new Size(238, 25);
      this.dtp_PA_Date.TabIndex = 3;
      this.dtp_PA_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(390, 29);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(42, 17);
      this.bunifuCustomLabel2.TabIndex = 0;
      this.bunifuCustomLabel2.Text = "Date:";
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(30, 27);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(93, 17);
      this.bunifuCustomLabel1.TabIndex = 0;
      this.bunifuCustomLabel1.Text = "Project Code:";
      this.gb_OA_CDetails.Controls.Add((Control) this.ddb_PA_CCode);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_PA_CName);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel4);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_CDetails.FlatStyle = FlatStyle.Flat;
      this.gb_OA_CDetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_CDetails.Location = new Point(11, 42);
      this.gb_OA_CDetails.Name = "gb_OA_CDetails";
      this.gb_OA_CDetails.Size = new Size(698, 59);
      this.gb_OA_CDetails.TabIndex = 53;
      this.gb_OA_CDetails.TabStop = false;
      this.gb_OA_CDetails.Text = "Client Details";
      this.ddb_PA_CCode.BackColor = Color.Transparent;
      this.ddb_PA_CCode.BorderRadius = 2;
      this.ddb_PA_CCode.DisabledColor = Color.Gray;
      this.ddb_PA_CCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.ddb_PA_CCode.Items = new string[0];
      this.ddb_PA_CCode.Location = new Point(108, 15);
      this.ddb_PA_CCode.Name = "ddb_PA_CCode";
      this.ddb_PA_CCode.NomalColor = Color.Silver;
      this.ddb_PA_CCode.onHoverColor = Color.DarkGray;
      this.ddb_PA_CCode.selectedIndex = -1;
      this.ddb_PA_CCode.Size = new Size(195, 35);
      this.ddb_PA_CCode.TabIndex = 1;
      this.ddb_PA_CCode.onItemSelected += new EventHandler(this.ddb_PA_CCode_onItemSelected);
      this.ddb_PA_CCode.KeyDown += new KeyEventHandler(this.ddb_PA_CCode_KeyDown);
      this.txt_PA_CName.Cursor = Cursors.IBeam;
      this.txt_PA_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PA_CName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PA_CName.HintForeColor = Color.Empty;
      this.txt_PA_CName.HintText = "";
      this.txt_PA_CName.isPassword = false;
      this.txt_PA_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_PA_CName.LineIdleColor = Color.Gray;
      this.txt_PA_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_PA_CName.LineThickness = 1;
      this.txt_PA_CName.Location = new Point(454, 15);
      this.txt_PA_CName.Margin = new Padding(4);
      this.txt_PA_CName.Name = "txt_PA_CName";
      this.txt_PA_CName.Size = new Size(223, 30);
      this.txt_PA_CName.TabIndex = 46;
      this.txt_PA_CName.TabStop = false;
      this.txt_PA_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_PA_CName.KeyDown += new KeyEventHandler(this.txt_PA_CName_KeyDown);
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
      this.bunifuCustomLabel3.Location = new Point(359, 25);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(88, 17);
      this.bunifuCustomLabel3.TabIndex = 0;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.btn_PA_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_PA_Close.BackColor = Color.Silver;
      this.btn_PA_Close.FlatAppearance.BorderSize = 0;
      this.btn_PA_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_PA_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_PA_Close.FlatStyle = FlatStyle.Flat;
      this.btn_PA_Close.Image = (Image) Resources.close_black;
      this.btn_PA_Close.Location = new Point(685, 6);
      this.btn_PA_Close.Name = "btn_PA_Close";
      this.btn_PA_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_PA_Close.Size = new Size(31, 29);
      this.btn_PA_Close.TabIndex = 56;
      this.btn_PA_Close.TabStop = false;
      this.btn_PA_Close.UseVisualStyleBackColor = false;
      this.btn_PA_Close.Click += new EventHandler(this.btn_PA_Close_Click);
      this.btn_PA_Close.MouseEnter += new EventHandler(this.btn_PA_Close_MouseEnter);
      this.btn_PA_Close.MouseLeave += new EventHandler(this.btn_PA_Close_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(720, 306);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_PA_Close);
      this.Controls.Add((Control) this.btn_PA_Cancel);
      this.Controls.Add((Control) this.btn_PA_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.Controls.Add((Control) this.gb_OA_CDetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (Proj_Add);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (Proj_Add);
      this.Load += new EventHandler(this.Proj_Add_Load);
      this.MouseDown += new MouseEventHandler(this.Proj_Add_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Proj_Add_MouseMove);
      this.MouseUp += new MouseEventHandler(this.Proj_Add_MouseUp);
      this.gb_OA_ODetails.ResumeLayout(false);
      this.gb_OA_ODetails.PerformLayout();
      this.panel6.ResumeLayout(false);
      this.panel6.PerformLayout();
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
