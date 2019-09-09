// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Projects
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
  public class Projects : Form
  {
    private BindingSource bs = new BindingSource();
    private bool isFiltered = false;
    private IContainer components = (IContainer) null;
    private DataTable dt;
    private int SELECTED_PROJECT;
    private Button btn_P_NewProject;
    private Button btn_P_ClearFilter;
    private BunifuDatepicker dtp_P_From;
    private AdvancedDataGridView dgv_Projects;
    private Button btn_P_Filter;
    private BunifuDatepicker dtp_P_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;

    public Projects()
    {
      this.InitializeComponent();
    }

    private void Projects_Load(object sender, EventArgs e)
    {
      this.dgv_Projects.DataSource = (object) this.bs;
      this.loadProjects();
    }

    private void loadProjects()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Projects", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_P_NewProject_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (Proj_Add projAdd = new Proj_Add())
      {
        int num = (int) projAdd.ShowDialog((IWin32Window) this);
      }
      this.loadProjects();
    }

    public int getSelectedProj()
    {
      return this.SELECTED_PROJECT;
    }

    public DataTable getProjects()
    {
      return this.dt;
    }

    public string getProjID()
    {
      return this.dgv_Projects[0, this.SELECTED_PROJECT].Value.ToString();
    }

    private void dgv_Projects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      this.SELECTED_PROJECT = e.RowIndex;
      using (Proj_Dialog projDialog = new Proj_Dialog())
      {
        int num = (int) projDialog.ShowDialog((IWin32Window) this);
      }
      this.loadProjects();
    }

    private void dgv_Projects_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_Projects.FilterString;
    }

    private void dgv_Projects_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_Projects.SortString;
    }

    private void btn_P_Filter_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Projects WHERE Date BETWEEN '" + (object) this.dtp_P_From.Value + "' AND '" + (object) this.dtp_P_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_P_Filter.Visible = false;
      this.btn_P_ClearFilter.Visible = true;
    }

    private void btn_P_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadProjects();
      this.btn_P_Filter.Visible = true;
      this.btn_P_ClearFilter.Visible = false;
    }

    private void btn_P_NewProject_MouseEnter(object sender, EventArgs e)
    {
      this.btn_P_NewProject.Image = (Image) Resources.add_white;
      this.btn_P_NewProject.ForeColor = Color.White;
    }

    private void btn_P_NewProject_MouseLeave(object sender, EventArgs e)
    {
      this.btn_P_NewProject.Image = (Image) Resources.add_grey;
      this.btn_P_NewProject.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_P_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_P_Filter.Image = (Image) Resources.filter_white;
      this.btn_P_Filter.ForeColor = Color.White;
    }

    private void btn_P_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_P_Filter.Image = (Image) Resources.filter_grey;
      this.btn_P_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_P_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_P_ClearFilter.ForeColor = Color.White;
    }

    private void btn_P_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_P_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Projects));
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      this.btn_P_NewProject = new Button();
      this.btn_P_ClearFilter = new Button();
      this.dtp_P_From = new BunifuDatepicker();
      this.dgv_Projects = new AdvancedDataGridView();
      this.btn_P_Filter = new Button();
      this.dtp_P_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      ((ISupportInitialize) this.dgv_Projects).BeginInit();
      this.SuspendLayout();
      this.btn_P_NewProject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_P_NewProject.FlatAppearance.BorderSize = 0;
      this.btn_P_NewProject.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_P_NewProject.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_P_NewProject.FlatStyle = FlatStyle.Flat;
      this.btn_P_NewProject.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_P_NewProject.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_P_NewProject.Image = (Image) componentResourceManager.GetObject("btn_P_NewProject.Image");
      this.btn_P_NewProject.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_P_NewProject.Location = new Point(829, 10);
      this.btn_P_NewProject.Name = "btn_P_NewProject";
      this.btn_P_NewProject.Size = new Size(122, 40);
      this.btn_P_NewProject.TabIndex = 102;
      this.btn_P_NewProject.Text = "New Project";
      this.btn_P_NewProject.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_P_NewProject.UseVisualStyleBackColor = true;
      this.btn_P_NewProject.Click += new EventHandler(this.btn_P_NewProject_Click);
      this.btn_P_NewProject.MouseEnter += new EventHandler(this.btn_P_NewProject_MouseEnter);
      this.btn_P_NewProject.MouseLeave += new EventHandler(this.btn_P_NewProject_MouseLeave);
      this.btn_P_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_P_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_P_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_P_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_P_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_P_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_P_ClearFilter.Location = new Point(553, 9);
      this.btn_P_ClearFilter.Name = "btn_P_ClearFilter";
      this.btn_P_ClearFilter.Size = new Size(114, 40);
      this.btn_P_ClearFilter.TabIndex = 101;
      this.btn_P_ClearFilter.Text = "Clear Filter";
      this.btn_P_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_P_ClearFilter.Visible = false;
      this.btn_P_ClearFilter.Click += new EventHandler(this.btn_P_ClearFilter_Click);
      this.btn_P_ClearFilter.MouseEnter += new EventHandler(this.btn_P_ClearFilter_MouseEnter);
      this.btn_P_ClearFilter.MouseLeave += new EventHandler(this.btn_P_ClearFilter_MouseLeave);
      this.dtp_P_From.BackColor = Color.LightGray;
      this.dtp_P_From.BorderRadius = 0;
      this.dtp_P_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_P_From.Format = DateTimePickerFormat.Short;
      this.dtp_P_From.FormatCustom = (string) null;
      this.dtp_P_From.Location = new Point(70, 13);
      this.dtp_P_From.Name = "dtp_P_From";
      this.dtp_P_From.Size = new Size(208, 36);
      this.dtp_P_From.TabIndex = 95;
      this.dtp_P_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_Projects.AllowUserToAddRows = false;
      this.dgv_Projects.AllowUserToDeleteRows = false;
      this.dgv_Projects.AllowUserToResizeColumns = false;
      this.dgv_Projects.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_Projects.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_Projects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_Projects.AutoGenerateContextFilters = true;
      this.dgv_Projects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_Projects.BorderStyle = BorderStyle.None;
      this.dgv_Projects.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_Projects.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_Projects.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_Projects.ColumnHeadersHeight = 25;
      this.dgv_Projects.DateWithTime = false;
      this.dgv_Projects.EnableHeadersVisualStyles = false;
      this.dgv_Projects.Location = new Point(0, 57);
      this.dgv_Projects.Name = "dgv_Projects";
      this.dgv_Projects.ReadOnly = true;
      this.dgv_Projects.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_Projects.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_Projects.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_Projects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Projects.Size = new Size(963, 562);
      this.dgv_Projects.TabIndex = 100;
      this.dgv_Projects.TimeFilter = false;
      this.dgv_Projects.SortStringChanged += new EventHandler(this.dgv_Projects_SortStringChanged);
      this.dgv_Projects.FilterStringChanged += new EventHandler(this.dgv_Projects_FilterStringChanged);
      this.dgv_Projects.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_Projects_CellDoubleClick);
      this.btn_P_Filter.FlatAppearance.BorderSize = 0;
      this.btn_P_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_P_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_P_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_P_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_P_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_P_Filter.Image = (Image) componentResourceManager.GetObject("btn_P_Filter.Image");
      this.btn_P_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_P_Filter.Location = new Point(553, 10);
      this.btn_P_Filter.Name = "btn_P_Filter";
      this.btn_P_Filter.Size = new Size(114, 40);
      this.btn_P_Filter.TabIndex = 99;
      this.btn_P_Filter.Text = "Filter";
      this.btn_P_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_P_Filter.UseVisualStyleBackColor = true;
      this.btn_P_Filter.Click += new EventHandler(this.btn_P_Filter_Click);
      this.btn_P_Filter.MouseEnter += new EventHandler(this.btn_P_Filter_MouseEnter);
      this.btn_P_Filter.MouseLeave += new EventHandler(this.btn_P_Filter_MouseLeave);
      this.dtp_P_To.BackColor = Color.LightGray;
      this.dtp_P_To.BorderRadius = 0;
      this.dtp_P_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_P_To.Format = DateTimePickerFormat.Short;
      this.dtp_P_To.FormatCustom = (string) null;
      this.dtp_P_To.Location = new Point(324, 13);
      this.dtp_P_To.Name = "dtp_P_To";
      this.dtp_P_To.Size = new Size(208, 36);
      this.dtp_P_To.TabIndex = 98;
      this.dtp_P_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(284, 20);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 97;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(12, 20);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 96;
      this.bunifuCustomLabel5.Text = "From:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_P_NewProject);
      this.Controls.Add((Control) this.btn_P_ClearFilter);
      this.Controls.Add((Control) this.dtp_P_From);
      this.Controls.Add((Control) this.dgv_Projects);
      this.Controls.Add((Control) this.btn_P_Filter);
      this.Controls.Add((Control) this.dtp_P_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (Projects);
      this.Text = nameof (Projects);
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Projects_Load);
      ((ISupportInitialize) this.dgv_Projects).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
