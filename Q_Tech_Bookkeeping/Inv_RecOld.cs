// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Inv_Rec
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
  public class Inv_RecOld : Form
  {
    private BindingSource bs = new BindingSource();
    private bool isFiltered = false;
    private IContainer components = (IContainer) null;
    private int SELECTED_INVOICE;
    private DataTable dt;
    private Button btn_LIR_ClearFilter;
    private BunifuDatepicker dtp_LIR_From;
    private AdvancedDataGridView dgv_LInvRec;
    private Button btn_LIR_Filter;
    private BunifuDatepicker dtp_LIR_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_LIR_NewIR;

    public Inv_RecOld()
    {
      this.InitializeComponent();
    }

    private void Inv_Rec_Load(object sender, EventArgs e)
    {
      this.dgv_LInvRec.DataSource = (object) this.bs;
      this.loadInvRec();
      this.dgv_LInvRec.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_LInvRec.Columns[5].DefaultCellStyle.Format = "c";
      this.dgv_LInvRec.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_LInvRec.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    }

    private void loadInvRec()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Received", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_LIR_NewIR_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (Inv_Rec_Add invRecAdd = new Inv_Rec_Add())
      {
        int num = (int) invRecAdd.ShowDialog((IWin32Window) this);
      }
      this.loadInvRec();
    }

    public int getSelectedInv()
    {
      return this.SELECTED_INVOICE;
    }

    public DataTable getInvRec()
    {
      return this.dt;
    }

    private void dgv_LInvRec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      this.SELECTED_INVOICE = e.RowIndex;
      using (Inv_Rec_Edit_Del invRecEditDel = new Inv_Rec_Edit_Del())
      {
        int num = (int) invRecEditDel.ShowDialog((IWin32Window) this);
      }
      this.loadInvRec();
    }

    private void ddgv_LInvRec_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_LInvRec.FilterString;
    }

    private void dgv_LInvRec_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_LInvRec.SortString;
    }

    private void btn_LIR_Filter_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Received WHERE Date BETWEEN '" + (object) this.dtp_LIR_From.Value + "' AND '" + (object) this.dtp_LIR_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_LIR_Filter.Visible = false;
      this.btn_LIR_ClearFilter.Visible = true;
    }

    private void btn_LIR_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadInvRec();
      this.btn_LIR_Filter.Visible = true;
      this.btn_LIR_ClearFilter.Visible = false;
    }

    private void btn_LIR_NewIR_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIR_NewIR.Image = (Image) Resources.add_white;
      this.btn_LIR_NewIR.ForeColor = Color.White;
    }

    private void btn_LIR_NewIR_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIR_NewIR.Image = (Image) Resources.add_grey;
      this.btn_LIR_NewIR.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LIR_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIR_Filter.Image = (Image) Resources.filter_white;
      this.btn_LIR_Filter.ForeColor = Color.White;
    }

    private void btn_LIR_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIR_Filter.Image = (Image) Resources.filter_grey;
      this.btn_LIR_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LIR_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIR_ClearFilter.ForeColor = Color.White;
    }

    private void btn_LIR_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIR_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Inv_RecOld));
      this.btn_LIR_ClearFilter = new Button();
      this.dtp_LIR_From = new BunifuDatepicker();
      this.dgv_LInvRec = new AdvancedDataGridView();
      this.btn_LIR_Filter = new Button();
      this.dtp_LIR_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_LIR_NewIR = new Button();
      ((ISupportInitialize) this.dgv_LInvRec).BeginInit();
      this.SuspendLayout();
      this.btn_LIR_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_LIR_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIR_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIR_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_LIR_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIR_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LIR_ClearFilter.Location = new Point(553, 9);
      this.btn_LIR_ClearFilter.Name = "btn_LIR_ClearFilter";
      this.btn_LIR_ClearFilter.Size = new Size(114, 40);
      this.btn_LIR_ClearFilter.TabIndex = 93;
      this.btn_LIR_ClearFilter.Text = "Clear Filter";
      this.btn_LIR_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_LIR_ClearFilter.Visible = false;
      this.btn_LIR_ClearFilter.Click += new EventHandler(this.btn_LIR_ClearFilter_Click);
      this.btn_LIR_ClearFilter.MouseEnter += new EventHandler(this.btn_LIR_ClearFilter_MouseEnter);
      this.btn_LIR_ClearFilter.MouseLeave += new EventHandler(this.btn_LIR_ClearFilter_MouseLeave);
      this.dtp_LIR_From.BackColor = Color.LightGray;
      this.dtp_LIR_From.BorderRadius = 0;
      this.dtp_LIR_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LIR_From.Format = DateTimePickerFormat.Short;
      this.dtp_LIR_From.FormatCustom = (string) null;
      this.dtp_LIR_From.Location = new Point(70, 12);
      this.dtp_LIR_From.Name = "dtp_LIR_From";
      this.dtp_LIR_From.Size = new Size(208, 36);
      this.dtp_LIR_From.TabIndex = 87;
      this.dtp_LIR_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_LInvRec.AllowUserToAddRows = false;
      this.dgv_LInvRec.AllowUserToDeleteRows = false;
      this.dgv_LInvRec.AllowUserToResizeColumns = false;
      this.dgv_LInvRec.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_LInvRec.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_LInvRec.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_LInvRec.AutoGenerateContextFilters = true;
      this.dgv_LInvRec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_LInvRec.BorderStyle = BorderStyle.None;
      this.dgv_LInvRec.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_LInvRec.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_LInvRec.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_LInvRec.ColumnHeadersHeight = 25;
      this.dgv_LInvRec.DateWithTime = false;
      this.dgv_LInvRec.EnableHeadersVisualStyles = false;
      this.dgv_LInvRec.Location = new Point(0, 56);
      this.dgv_LInvRec.Name = "dgv_LInvRec";
      this.dgv_LInvRec.ReadOnly = true;
      this.dgv_LInvRec.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_LInvRec.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_LInvRec.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_LInvRec.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_LInvRec.Size = new Size(963, 562);
      this.dgv_LInvRec.TabIndex = 92;
      this.dgv_LInvRec.TimeFilter = false;
      this.dgv_LInvRec.SortStringChanged += new EventHandler(this.dgv_LInvRec_SortStringChanged);
      this.dgv_LInvRec.FilterStringChanged += new EventHandler(this.ddgv_LInvRec_FilterStringChanged);
      this.dgv_LInvRec.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_LInvRec_CellDoubleClick);
      this.btn_LIR_Filter.FlatAppearance.BorderSize = 0;
      this.btn_LIR_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIR_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIR_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_LIR_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIR_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LIR_Filter.Image = (Image) componentResourceManager.GetObject("btn_LIR_Filter.Image");
      this.btn_LIR_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LIR_Filter.Location = new Point(553, 9);
      this.btn_LIR_Filter.Name = "btn_LIR_Filter";
      this.btn_LIR_Filter.Size = new Size(114, 40);
      this.btn_LIR_Filter.TabIndex = 91;
      this.btn_LIR_Filter.Text = "Filter";
      this.btn_LIR_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LIR_Filter.UseVisualStyleBackColor = true;
      this.btn_LIR_Filter.Click += new EventHandler(this.btn_LIR_Filter_Click);
      this.btn_LIR_Filter.MouseEnter += new EventHandler(this.btn_LIR_Filter_MouseEnter);
      this.btn_LIR_Filter.MouseLeave += new EventHandler(this.btn_LIR_Filter_MouseLeave);
      this.dtp_LIR_To.BackColor = Color.LightGray;
      this.dtp_LIR_To.BorderRadius = 0;
      this.dtp_LIR_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LIR_To.Format = DateTimePickerFormat.Short;
      this.dtp_LIR_To.FormatCustom = (string) null;
      this.dtp_LIR_To.Location = new Point(324, 12);
      this.dtp_LIR_To.Name = "dtp_LIR_To";
      this.dtp_LIR_To.Size = new Size(208, 36);
      this.dtp_LIR_To.TabIndex = 90;
      this.dtp_LIR_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(284, 19);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 89;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(12, 19);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 88;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_LIR_NewIR.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_LIR_NewIR.FlatAppearance.BorderSize = 0;
      this.btn_LIR_NewIR.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIR_NewIR.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIR_NewIR.FlatStyle = FlatStyle.Flat;
      this.btn_LIR_NewIR.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIR_NewIR.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LIR_NewIR.Image = (Image) componentResourceManager.GetObject("btn_LIR_NewIR.Image");
      this.btn_LIR_NewIR.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LIR_NewIR.Location = new Point(829, 9);
      this.btn_LIR_NewIR.Name = "btn_LIR_NewIR";
      this.btn_LIR_NewIR.Size = new Size(122, 40);
      this.btn_LIR_NewIR.TabIndex = 94;
      this.btn_LIR_NewIR.Text = "New Invoice";
      this.btn_LIR_NewIR.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LIR_NewIR.UseVisualStyleBackColor = true;
      this.btn_LIR_NewIR.Click += new EventHandler(this.btn_LIR_NewIR_Click);
      this.btn_LIR_NewIR.MouseEnter += new EventHandler(this.btn_LIR_NewIR_MouseEnter);
      this.btn_LIR_NewIR.MouseLeave += new EventHandler(this.btn_LIR_NewIR_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_LIR_NewIR);
      this.Controls.Add((Control) this.btn_LIR_ClearFilter);
      this.Controls.Add((Control) this.dtp_LIR_From);
      this.Controls.Add((Control) this.dgv_LInvRec);
      this.Controls.Add((Control) this.btn_LIR_Filter);
      this.Controls.Add((Control) this.dtp_LIR_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(963, 618);
      this.Name = nameof (Inv_RecOld);
      this.Text = "Invoices Received";
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Inv_Rec_Load);
      ((ISupportInitialize) this.dgv_LInvRec).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
