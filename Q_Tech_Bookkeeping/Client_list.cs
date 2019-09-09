// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Client_list
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
  public class Client_list : Form
  {
    private BindingSource bs = new BindingSource();
    private bool isInter = false;
    private bool mouseDown = false;
    private IContainer components = (IContainer) null;
    private SqlDataAdapter da;
    private DataTable dt;
    private Point lastLocation;
    private string curVisible;
    private HomeOld frmHome;
    private AdvancedDataGridView dgv_CL;
    private Button btn_SelCon_Close;
    private BunifuCustomLabel lbl_Header;

    public Client_list()
    {
      this.InitializeComponent();
    }

    private void Client_list_Load(object sender, EventArgs e)
    {
      this.frmHome = (HomeOld) this.Owner;
      this.curVisible = this.frmHome.getCurPanel();
      if (this.curVisible == "pnl_I_Orders" || this.curVisible == "pnl_I_Quotes" || this.curVisible == "pnl_I_InvSend")
        this.isInter = true;
      this.dgv_CL.DataSource = (object) this.bs;
      this.loadData();
    }

    private void loadData()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        this.da = !this.isInter ? new SqlDataAdapter("SELECT * FROM Clients", dbConnection) : new SqlDataAdapter("SELECT * FROM Int_Clients", dbConnection);
        this.dt = new DataTable();
        this.da.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void dgv_CL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.curVisible == "pnl_L_Orders")
        ((Orders) this.frmHome.getCurForm()).setNewClient(e.RowIndex);
      if (this.curVisible == "pnl_L_Quotes")
        ((Quotes) this.frmHome.getCurForm()).setNewClient(e.RowIndex);
      this.Close();
    }

    private void btn_CL_Close_Click(object sender, EventArgs e)
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

    private void dgv_CList_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_CL.FilterString;
    }

    private void dgv_CList_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_CL.SortString;
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
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Client_list));
      this.dgv_CL = new AdvancedDataGridView();
      this.btn_SelCon_Close = new Button();
      this.lbl_Header = new BunifuCustomLabel();
      ((ISupportInitialize) this.dgv_CL).BeginInit();
      this.SuspendLayout();
      this.dgv_CL.AllowUserToAddRows = false;
      this.dgv_CL.AllowUserToDeleteRows = false;
      this.dgv_CL.AllowUserToResizeColumns = false;
      this.dgv_CL.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_CL.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_CL.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_CL.AutoGenerateContextFilters = true;
      this.dgv_CL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_CL.BorderStyle = BorderStyle.None;
      this.dgv_CL.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_CL.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_CL.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_CL.ColumnHeadersHeight = 25;
      this.dgv_CL.DateWithTime = false;
      this.dgv_CL.EnableHeadersVisualStyles = false;
      this.dgv_CL.Location = new Point(12, 52);
      this.dgv_CL.Name = "dgv_CL";
      this.dgv_CL.ReadOnly = true;
      this.dgv_CL.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_CL.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_CL.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_CL.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_CL.Size = new Size(366, 616);
      this.dgv_CL.TabIndex = 34;
      this.dgv_CL.TimeFilter = false;
      this.dgv_CL.SortStringChanged += new EventHandler(this.dgv_CList_SortStringChanged);
      this.dgv_CL.FilterStringChanged += new EventHandler(this.dgv_CList_FilterStringChanged);
      this.dgv_CL.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_CL_CellDoubleClick);
      this.btn_SelCon_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_SelCon_Close.BackColor = Color.Silver;
      this.btn_SelCon_Close.FlatAppearance.BorderSize = 0;
      this.btn_SelCon_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_SelCon_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_SelCon_Close.FlatStyle = FlatStyle.Flat;
      this.btn_SelCon_Close.Image = (Image) componentResourceManager.GetObject("btn_SelCon_Close.Image");
      this.btn_SelCon_Close.Location = new Point(353, 6);
      this.btn_SelCon_Close.Name = "btn_SelCon_Close";
      this.btn_SelCon_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_SelCon_Close.Size = new Size(31, 29);
      this.btn_SelCon_Close.TabIndex = 35;
      this.btn_SelCon_Close.UseVisualStyleBackColor = false;
      this.btn_SelCon_Close.Click += new EventHandler(this.btn_CL_Close_Click);
      this.btn_SelCon_Close.MouseEnter += new EventHandler(this.btn_CL_Close_MouseEnter);
      this.btn_SelCon_Close.MouseLeave += new EventHandler(this.btn_CL_Close_MouseLeave);
      this.lbl_Header.AutoSize = true;
      this.lbl_Header.Font = new Font("Microsoft NeoGothic", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lbl_Header.ForeColor = Color.FromArgb(19, 118, 188);
      this.lbl_Header.Location = new Point(12, 14);
      this.lbl_Header.Name = "lbl_Header";
      this.lbl_Header.Size = new Size(98, 21);
      this.lbl_Header.TabIndex = 36;
      this.lbl_Header.Text = "Select Client:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(390, 680);
      this.Controls.Add((Control) this.lbl_Header);
      this.Controls.Add((Control) this.btn_SelCon_Close);
      this.Controls.Add((Control) this.dgv_CL);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MaximumSize = new Size(390, 680);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(390, 680);
      this.Name = nameof (Client_list);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Client List";
      this.Load += new EventHandler(this.Client_list_Load);
      this.MouseDown += new MouseEventHandler(this.CL_MouseDown);
      this.MouseMove += new MouseEventHandler(this.CL_MouseMove);
      this.MouseUp += new MouseEventHandler(this.CL_MouseUp);
      ((ISupportInitialize) this.dgv_CL).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
