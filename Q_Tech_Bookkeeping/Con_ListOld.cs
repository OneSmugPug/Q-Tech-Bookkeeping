// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Con_List
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using ADGV;
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
  public class Con_ListOld : Form
  {
    private BindingSource bs = new BindingSource();
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private SqlDataAdapter da;
    private DataTable dt;
    private Point lastLocation;
    private Panel panel1;
    private AdvancedDataGridView dgv_SelCon;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_SelCon_Close;

    public Con_ListOld()
    {
      this.InitializeComponent();
    }

    private void Con_List_Load(object sender, EventArgs e)
    {
      this.dgv_SelCon.DataSource = (object) this.bs;
      this.loadCon();
    }

    private void loadCon()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        this.da = new SqlDataAdapter("SELECT * FROM Contractors", dbConnection);
        this.dt = new DataTable();
        this.da.Fill(this.dt);
        DataRow row = this.dt.Rows[0];
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void dgv_SelCon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      ((Contractors) ((HomeOld) this.Owner).getCurForm()).setNewCon(e.RowIndex);
      this.Close();
    }

    private void btn_SelCon_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_CL_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_SelCon_Close.Image = (Image) Resources.close_white;
    }

    private void btn_CL_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_SelCon_Close.Image = (Image) Resources.close_black;
    }

    private void dgv_SelCon_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_SelCon.SortString;
    }

    private void dgv_SelCon_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_SelCon.FilterString;
    }

    private void CL_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void CL_MouseMove(object sender, MouseEventArgs e)
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

    private void CL_MouseUp(object sender, MouseEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Con_ListOld));
      this.panel1 = new Panel();
      this.dgv_SelCon = new AdvancedDataGridView();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_SelCon_Close = new Button();
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.dgv_SelCon).BeginInit();
      this.SuspendLayout();
      this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.panel1.Controls.Add((Control) this.dgv_SelCon);
      this.panel1.Location = new Point(12, 50);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(626, 507);
      this.panel1.TabIndex = 0;
      this.dgv_SelCon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_SelCon.AutoGenerateContextFilters = true;
      this.dgv_SelCon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_SelCon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_SelCon.DateWithTime = false;
      this.dgv_SelCon.Location = new Point(0, 0);
      this.dgv_SelCon.Name = "dgv_SelCon";
      this.dgv_SelCon.Size = new Size(626, 507);
      this.dgv_SelCon.TabIndex = 1;
      this.dgv_SelCon.TimeFilter = false;
      this.dgv_SelCon.SortStringChanged += new EventHandler(this.dgv_SelCon_SortStringChanged);
      this.dgv_SelCon.FilterStringChanged += new EventHandler(this.dgv_SelCon_FilterStringChanged);
      this.dgv_SelCon.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_SelCon_CellDoubleClick);
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft NeoGothic", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(19, 118, 188);
      this.bunifuCustomLabel4.Location = new Point(12, 12);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(132, 21);
      this.bunifuCustomLabel4.TabIndex = 37;
      this.bunifuCustomLabel4.Text = "Select Contractor:";
      this.btn_SelCon_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_SelCon_Close.BackColor = Color.Silver;
      this.btn_SelCon_Close.FlatAppearance.BorderSize = 0;
      this.btn_SelCon_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_SelCon_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_SelCon_Close.FlatStyle = FlatStyle.Flat;
      this.btn_SelCon_Close.Image = (Image) componentResourceManager.GetObject("btn_SelCon_Close.Image");
      this.btn_SelCon_Close.Location = new Point(615, 4);
      this.btn_SelCon_Close.Name = "btn_SelCon_Close";
      this.btn_SelCon_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_SelCon_Close.Size = new Size(31, 29);
      this.btn_SelCon_Close.TabIndex = 38;
      this.btn_SelCon_Close.UseVisualStyleBackColor = false;
      this.btn_SelCon_Close.Click += new EventHandler(this.btn_SelCon_Close_Click);
      this.btn_SelCon_Close.MouseEnter += new EventHandler(this.btn_CL_Close_MouseEnter);
      this.btn_SelCon_Close.MouseLeave += new EventHandler(this.btn_CL_Close_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(650, 570);
      this.Controls.Add((Control) this.btn_SelCon_Close);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.panel1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(650, 570);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(650, 570);
      this.Name = nameof (Con_ListOld);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Contractor List";
      this.Load += new EventHandler(this.Con_List_Load);
      this.MouseDown += new MouseEventHandler(this.CL_MouseDown);
      this.MouseMove += new MouseEventHandler(this.CL_MouseMove);
      this.MouseUp += new MouseEventHandler(this.CL_MouseUp);
      this.panel1.ResumeLayout(false);
      ((ISupportInitialize) this.dgv_SelCon).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
