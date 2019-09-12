// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Proj_Edit_Del
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
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Proj_Edit_Del : Form
  {
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private DataTable dt;
    private static int SELECTED_PROJECT;
    private Point lastLocation;
    private BunifuCustomLabel bunifuCustomLabel10;
    private Button btn_PED_Close;
    private Button btn_PED_Cancel;
    private Button btn_PED_Done;
    private GroupBox gb_OA_ODetails;
    private Panel panel6;
    private TextBox txt_PED_QNum;
    private BunifuSeparator ln_PED_QNum;
    private Panel panel2;
    private TextBox txt_PED_Desc;
    private BunifuSeparator ln_PED_Desc;
    private Panel panel1;
    private TextBox txt_PED_ProjCode;
    private BunifuSeparator ln_PED_ProjCode;
    private BunifuCustomLabel bunifuCustomLabel8;
    private BunifuCustomLabel bunifuCustomLabel5;
    private BunifuDatepicker dtp_PED_Date;
    private BunifuCustomLabel bunifuCustomLabel2;
    private BunifuCustomLabel bunifuCustomLabel1;
    private GroupBox gb_OA_CDetails;
    private BunifuMaterialTextbox txt_PED_CName;
    private BunifuCustomLabel bunifuCustomLabel4;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuMaterialTextbox txt_PED_CCode;

    public Proj_Edit_Del()
    {
      this.InitializeComponent();
    }

    private void Proj_Edit_Del_Load(object sender, EventArgs e)
    {
      ProjectsOld curForm = (ProjectsOld) ((HomeOld) this.Owner).getCurForm();
      this.dt = curForm.getProjects();
      Proj_Edit_Del.SELECTED_PROJECT = curForm.getSelectedProj();
      this.loadProject();
    }

    private void loadProject()
    {
      this.txt_PED_CCode.Text = this.dt.Rows[Proj_Edit_Del.SELECTED_PROJECT]["Client_Code"].ToString().Trim();
      this.txt_PED_CName.Text = this.dt.Rows[Proj_Edit_Del.SELECTED_PROJECT]["Client_Name"].ToString().Trim();
      this.txt_PED_ProjCode.Text = this.dt.Rows[Proj_Edit_Del.SELECTED_PROJECT]["Project_ID"].ToString().Trim();
      this.dtp_PED_Date.Value = !(this.dt.Rows[Proj_Edit_Del.SELECTED_PROJECT]["Date"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(this.dt.Rows[Proj_Edit_Del.SELECTED_PROJECT]["Date"].ToString());
      this.txt_PED_Desc.Text = this.dt.Rows[Proj_Edit_Del.SELECTED_PROJECT]["Description"].ToString().Trim();
      this.txt_PED_QNum.Text = this.dt.Rows[Proj_Edit_Del.SELECTED_PROJECT]["Quote_Number"].ToString().Trim();
    }

    private void btn_PED_Done_Click(object sender, EventArgs e)
    {
      string text = this.txt_PED_ProjCode.Text;
      if (MessageBox.Show("Are you sure you want to update project?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        try
        {
          using (SqlCommand sqlCommand = new SqlCommand("UPDATE Projects SET Date = @Date, Description = @Desc WHERE Project_ID = @ProjID", dbConnection))
          {
            sqlCommand.Parameters.AddWithValue("@Date", (object) this.dtp_PED_Date.Value);
            sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_PED_Desc.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@ProjID", (object) text);
            sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("Project successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void btn_PED_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_PED_ProjCode_MouseEnter(object sender, EventArgs e)
    {
      this.ln_PED_ProjCode.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_PED_ProjCode_Leave(object sender, EventArgs e)
    {
      this.ln_PED_ProjCode.LineColor = Color.Gray;
    }

    private void txt_PED_ProjCode_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_PED_ProjCode.Focused)
        return;
      this.ln_PED_ProjCode.LineColor = Color.Gray;
    }

    private void txt_PED_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_PED_Desc.LineColor = Color.Gray;
    }

    private void txt_PED_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_PED_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_PED_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_PED_Desc.Focused)
        return;
      this.ln_PED_Desc.LineColor = Color.Gray;
    }

    private void txt_PED_QNum_Leave(object sender, EventArgs e)
    {
      this.ln_PED_QNum.LineColor = Color.Gray;
    }

    private void txt_PED_QNum_MouseEnter(object sender, EventArgs e)
    {
      this.ln_PED_QNum.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_PED_QNum_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_PED_QNum.Focused)
        return;
      this.ln_PED_QNum.LineColor = Color.Gray;
    }

    private void btn_PED_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_PED_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PED_Close.Image = (Image) Resources.close_white;
    }

    private void btn_PED_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PED_Close.Image = (Image) Resources.close_black;
    }

    private void btn_PED_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PED_Done.ForeColor = Color.White;
    }

    private void btn_PED_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PED_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_PED_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PED_Cancel.ForeColor = Color.White;
    }

    private void btn_PED_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void ddb_PED_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_PED_CName_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void Proj_Edit_Del_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void Proj_Edit_Del_MouseMove(object sender, MouseEventArgs e)
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

    private void Proj_Edit_Del_MouseUp(object sender, MouseEventArgs e)
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
      this.btn_PED_Cancel = new Button();
      this.btn_PED_Done = new Button();
      this.gb_OA_ODetails = new GroupBox();
      this.panel6 = new Panel();
      this.txt_PED_QNum = new TextBox();
      this.ln_PED_QNum = new BunifuSeparator();
      this.panel2 = new Panel();
      this.txt_PED_Desc = new TextBox();
      this.ln_PED_Desc = new BunifuSeparator();
      this.panel1 = new Panel();
      this.txt_PED_ProjCode = new TextBox();
      this.ln_PED_ProjCode = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.dtp_PED_Date = new BunifuDatepicker();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.gb_OA_CDetails = new GroupBox();
      this.txt_PED_CCode = new BunifuMaterialTextbox();
      this.txt_PED_CName = new BunifuMaterialTextbox();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.btn_PED_Close = new Button();
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
      this.bunifuCustomLabel10.Size = new Size(129, 22);
      this.bunifuCustomLabel10.TabIndex = 57;
      this.bunifuCustomLabel10.Text = "Update Project";
      this.btn_PED_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_PED_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_PED_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_PED_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_PED_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_PED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_PED_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_PED_Cancel.Location = new Point(595, (int) byte.MaxValue);
      this.btn_PED_Cancel.Name = "btn_PED_Cancel";
      this.btn_PED_Cancel.Size = new Size(114, 40);
      this.btn_PED_Cancel.TabIndex = 4;
      this.btn_PED_Cancel.Text = "Cancel";
      this.btn_PED_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_PED_Cancel.UseVisualStyleBackColor = true;
      this.btn_PED_Cancel.Click += new EventHandler(this.btn_PED_Cancel_Click);
      this.btn_PED_Cancel.MouseEnter += new EventHandler(this.btn_PED_Cancel_MouseEnter);
      this.btn_PED_Cancel.MouseLeave += new EventHandler(this.btn_PED_Cancel_MouseLeave);
      this.btn_PED_Done.FlatAppearance.BorderSize = 0;
      this.btn_PED_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_PED_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_PED_Done.FlatStyle = FlatStyle.Flat;
      this.btn_PED_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_PED_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_PED_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_PED_Done.Location = new Point(475, (int) byte.MaxValue);
      this.btn_PED_Done.Name = "btn_PED_Done";
      this.btn_PED_Done.Size = new Size(114, 40);
      this.btn_PED_Done.TabIndex = 3;
      this.btn_PED_Done.Text = "Done";
      this.btn_PED_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_PED_Done.UseVisualStyleBackColor = true;
      this.btn_PED_Done.Click += new EventHandler(this.btn_PED_Done_Click);
      this.btn_PED_Done.MouseEnter += new EventHandler(this.btn_PED_Done_MouseEnter);
      this.btn_PED_Done.MouseLeave += new EventHandler(this.btn_PED_Done_MouseLeave);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel6);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.panel1);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel8);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel5);
      this.gb_OA_ODetails.Controls.Add((Control) this.dtp_PED_Date);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel2);
      this.gb_OA_ODetails.Controls.Add((Control) this.bunifuCustomLabel1);
      this.gb_OA_ODetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_ODetails.Location = new Point(11, 120);
      this.gb_OA_ODetails.Name = "gb_OA_ODetails";
      this.gb_OA_ODetails.Size = new Size(698, 129);
      this.gb_OA_ODetails.TabIndex = 58;
      this.gb_OA_ODetails.TabStop = false;
      this.gb_OA_ODetails.Text = "Project Details";
      this.panel6.Controls.Add((Control) this.txt_PED_QNum);
      this.panel6.Controls.Add((Control) this.ln_PED_QNum);
      this.panel6.Location = new Point(129, 90);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(156, 26);
      this.panel6.TabIndex = 64;
      this.txt_PED_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_PED_QNum.BackColor = Color.Silver;
      this.txt_PED_QNum.BorderStyle = BorderStyle.None;
      this.txt_PED_QNum.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PED_QNum.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PED_QNum.Location = new Point(2, 6);
      this.txt_PED_QNum.Name = "txt_PED_QNum";
      this.txt_PED_QNum.ReadOnly = true;
      this.txt_PED_QNum.Size = new Size(153, 16);
      this.txt_PED_QNum.TabIndex = 7;
      this.txt_PED_QNum.TabStop = false;
      this.txt_PED_QNum.Leave += new EventHandler(this.txt_PED_QNum_Leave);
      this.txt_PED_QNum.MouseEnter += new EventHandler(this.txt_PED_QNum_MouseEnter);
      this.txt_PED_QNum.MouseLeave += new EventHandler(this.txt_PED_QNum_MouseLeave);
      this.ln_PED_QNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_PED_QNum.BackColor = Color.Transparent;
      this.ln_PED_QNum.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_PED_QNum.LineThickness = 1;
      this.ln_PED_QNum.Location = new Point(-1, 18);
      this.ln_PED_QNum.Name = "ln_PED_QNum";
      this.ln_PED_QNum.Size = new Size(158, 10);
      this.ln_PED_QNum.TabIndex = 52;
      this.ln_PED_QNum.TabStop = false;
      this.ln_PED_QNum.Transparency = (int) byte.MaxValue;
      this.ln_PED_QNum.Vertical = false;
      this.panel2.Controls.Add((Control) this.txt_PED_Desc);
      this.panel2.Controls.Add((Control) this.ln_PED_Desc);
      this.panel2.Location = new Point(129, 56);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(547, 27);
      this.panel2.TabIndex = 60;
      this.txt_PED_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_PED_Desc.BackColor = Color.Silver;
      this.txt_PED_Desc.BorderStyle = BorderStyle.None;
      this.txt_PED_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PED_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PED_Desc.Location = new Point(2, 7);
      this.txt_PED_Desc.Name = "txt_PED_Desc";
      this.txt_PED_Desc.Size = new Size(544, 16);
      this.txt_PED_Desc.TabIndex = 2;
      this.txt_PED_Desc.Leave += new EventHandler(this.txt_PED_Desc_Leave);
      this.txt_PED_Desc.MouseEnter += new EventHandler(this.txt_PED_Desc_MouseEnter);
      this.txt_PED_Desc.MouseLeave += new EventHandler(this.txt_PED_Desc_MouseLeave);
      this.ln_PED_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_PED_Desc.BackColor = Color.Transparent;
      this.ln_PED_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_PED_Desc.LineThickness = 1;
      this.ln_PED_Desc.Location = new Point(-1, 18);
      this.ln_PED_Desc.Name = "ln_PED_Desc";
      this.ln_PED_Desc.Size = new Size(549, 10);
      this.ln_PED_Desc.TabIndex = 0;
      this.ln_PED_Desc.TabStop = false;
      this.ln_PED_Desc.Transparency = (int) byte.MaxValue;
      this.ln_PED_Desc.Vertical = false;
      this.panel1.Controls.Add((Control) this.txt_PED_ProjCode);
      this.panel1.Controls.Add((Control) this.ln_PED_ProjCode);
      this.panel1.Location = new Point(129, 23);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(210, 27);
      this.panel1.TabIndex = 52;
      this.txt_PED_ProjCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_PED_ProjCode.BackColor = Color.Silver;
      this.txt_PED_ProjCode.BorderStyle = BorderStyle.None;
      this.txt_PED_ProjCode.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PED_ProjCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PED_ProjCode.Location = new Point(2, 6);
      this.txt_PED_ProjCode.Name = "txt_PED_ProjCode";
      this.txt_PED_ProjCode.ReadOnly = true;
      this.txt_PED_ProjCode.Size = new Size(208, 16);
      this.txt_PED_ProjCode.TabIndex = 0;
      this.txt_PED_ProjCode.TabStop = false;
      this.txt_PED_ProjCode.Leave += new EventHandler(this.txt_PED_ProjCode_Leave);
      this.txt_PED_ProjCode.MouseEnter += new EventHandler(this.txt_PED_ProjCode_MouseEnter);
      this.txt_PED_ProjCode.MouseLeave += new EventHandler(this.txt_PED_ProjCode_MouseLeave);
      this.ln_PED_ProjCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_PED_ProjCode.BackColor = Color.Transparent;
      this.ln_PED_ProjCode.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_PED_ProjCode.LineThickness = 1;
      this.ln_PED_ProjCode.Location = new Point(-1, 18);
      this.ln_PED_ProjCode.Name = "ln_PED_ProjCode";
      this.ln_PED_ProjCode.Size = new Size(212, 10);
      this.ln_PED_ProjCode.TabIndex = 52;
      this.ln_PED_ProjCode.TabStop = false;
      this.ln_PED_ProjCode.Transparency = (int) byte.MaxValue;
      this.ln_PED_ProjCode.Vertical = false;
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
      this.dtp_PED_Date.BackColor = Color.Silver;
      this.dtp_PED_Date.BorderRadius = 0;
      this.dtp_PED_Date.ForeColor = Color.FromArgb(15, 91, 142);
      this.dtp_PED_Date.Format = DateTimePickerFormat.Short;
      this.dtp_PED_Date.FormatCustom = (string) null;
      this.dtp_PED_Date.Location = new Point(438, 26);
      this.dtp_PED_Date.Name = "dtp_PED_Date";
      this.dtp_PED_Date.Size = new Size(238, 25);
      this.dtp_PED_Date.TabIndex = 1;
      this.dtp_PED_Date.Value = new DateTime(2018, 12, 27, 9, 43, 4, 245);
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
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_PED_CCode);
      this.gb_OA_CDetails.Controls.Add((Control) this.txt_PED_CName);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel4);
      this.gb_OA_CDetails.Controls.Add((Control) this.bunifuCustomLabel3);
      this.gb_OA_CDetails.FlatStyle = FlatStyle.Flat;
      this.gb_OA_CDetails.ForeColor = Color.FromArgb(15, 91, 142);
      this.gb_OA_CDetails.Location = new Point(11, 42);
      this.gb_OA_CDetails.Name = "gb_OA_CDetails";
      this.gb_OA_CDetails.Size = new Size(698, 59);
      this.gb_OA_CDetails.TabIndex = 59;
      this.gb_OA_CDetails.TabStop = false;
      this.gb_OA_CDetails.Text = "Client Details";
      this.txt_PED_CCode.Cursor = Cursors.IBeam;
      this.txt_PED_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PED_CCode.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PED_CCode.HintForeColor = Color.Empty;
      this.txt_PED_CCode.HintText = "";
      this.txt_PED_CCode.isPassword = false;
      this.txt_PED_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_PED_CCode.LineIdleColor = Color.Gray;
      this.txt_PED_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_PED_CCode.LineThickness = 1;
      this.txt_PED_CCode.Location = new Point(109, 15);
      this.txt_PED_CCode.Margin = new Padding(4);
      this.txt_PED_CCode.Name = "txt_PED_CCode";
      this.txt_PED_CCode.Size = new Size(202, 30);
      this.txt_PED_CCode.TabIndex = 47;
      this.txt_PED_CCode.TabStop = false;
      this.txt_PED_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_PED_CCode.KeyDown += new KeyEventHandler(this.ddb_PED_CCode_KeyDown);
      this.txt_PED_CName.Cursor = Cursors.IBeam;
      this.txt_PED_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_PED_CName.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_PED_CName.HintForeColor = Color.Empty;
      this.txt_PED_CName.HintText = "";
      this.txt_PED_CName.isPassword = false;
      this.txt_PED_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_PED_CName.LineIdleColor = Color.Gray;
      this.txt_PED_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_PED_CName.LineThickness = 1;
      this.txt_PED_CName.Location = new Point(454, 15);
      this.txt_PED_CName.Margin = new Padding(4);
      this.txt_PED_CName.Name = "txt_PED_CName";
      this.txt_PED_CName.Size = new Size(223, 30);
      this.txt_PED_CName.TabIndex = 46;
      this.txt_PED_CName.TabStop = false;
      this.txt_PED_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_PED_CName.KeyDown += new KeyEventHandler(this.txt_PED_CName_KeyDown);
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
      this.btn_PED_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_PED_Close.BackColor = Color.Silver;
      this.btn_PED_Close.FlatAppearance.BorderSize = 0;
      this.btn_PED_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_PED_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_PED_Close.FlatStyle = FlatStyle.Flat;
      this.btn_PED_Close.Image = (Image) Resources.close_black;
      this.btn_PED_Close.Location = new Point(685, 6);
      this.btn_PED_Close.Name = "btn_PED_Close";
      this.btn_PED_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_PED_Close.Size = new Size(31, 29);
      this.btn_PED_Close.TabIndex = 62;
      this.btn_PED_Close.TabStop = false;
      this.btn_PED_Close.UseVisualStyleBackColor = false;
      this.btn_PED_Close.Click += new EventHandler(this.btn_PED_Close_Click);
      this.btn_PED_Close.MouseEnter += new EventHandler(this.btn_PED_Close_MouseEnter);
      this.btn_PED_Close.MouseLeave += new EventHandler(this.btn_PED_Close_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(720, 306);
      this.Controls.Add((Control) this.bunifuCustomLabel10);
      this.Controls.Add((Control) this.btn_PED_Close);
      this.Controls.Add((Control) this.btn_PED_Cancel);
      this.Controls.Add((Control) this.btn_PED_Done);
      this.Controls.Add((Control) this.gb_OA_ODetails);
      this.Controls.Add((Control) this.gb_OA_CDetails);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (Proj_Edit_Del);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (Proj_Edit_Del);
      this.Load += new EventHandler(this.Proj_Edit_Del_Load);
      this.MouseDown += new MouseEventHandler(this.Proj_Edit_Del_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Proj_Edit_Del_MouseMove);
      this.MouseUp += new MouseEventHandler(this.Proj_Edit_Del_MouseUp);
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
