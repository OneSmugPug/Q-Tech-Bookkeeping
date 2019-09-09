// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Manage_Proj
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
using System.Globalization;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Manage_Proj : Form
  {
    private BindingSource bs = new BindingSource();
    private IContainer components = (IContainer) null;
    private DataTable dt;
    private string Proj_ID;
    private HomeOld frmHome;
    private bool isFiltered;
    private Button btn_MP_NewLine;
    private Button btn_MP_ClearFilter;
    private BunifuDatepicker dtp_MP_From;
    private AdvancedDataGridView dgv_ManageProj;
    private Button btn_MP_Filter;
    private BunifuDatepicker dtp_MP_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Label label6;
    private TextBox txt_MP_TotHours;
    private Label label5;
    private TextBox txt_MP_TotRand;
    private Button btn_MP_Close;
    private Label label1;
    private TextBox txt_MP_TotDol;
    private Button btn_MP_RemoveLine;

    public Manage_Proj()
    {
      this.InitializeComponent();
    }

    private void Manage_Proj_Load(object sender, EventArgs e)
    {
      this.dgv_ManageProj.DataSource = (object) this.bs;
      this.Proj_ID = ((Projects) this.frmHome.getCurForm()).getProjID();
      this.loadExpenses();
    }

    private void loadExpenses()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT ID, Description, Travel, Accomodation, Subsistence, Tools, Programming_Hours, Install_Hours, Date, User_Log FROM Project_Expenses WHERE Project_ID = '" + this.Proj_ID + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      Decimal num1 = new Decimal();
      Decimal num2 = new Decimal();
      Decimal num3 = new Decimal();
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row["Travel"].ToString() != "")
        {
          if (row["Travel"].ToString().Contains("R"))
            num1 += Convert.ToDecimal(row["Travel"].ToString().Remove(0, 1));
          else if (row["Travel"].ToString().Contains("$"))
            num2 += Convert.ToDecimal(row["Travel"].ToString().Remove(0, 1));
        }
        if (row["Accomodation"].ToString() != "")
        {
          if (row["Accomodation"].ToString().Contains("R"))
            num1 += Convert.ToDecimal(row["Accomodation"].ToString().Remove(0, 1));
          else if (row["Accomodation"].ToString().Contains("$"))
            num2 += Convert.ToDecimal(row["Accomodation"].ToString().Remove(0, 1));
        }
        if (row["Subsistence"].ToString() != "")
        {
          if (row["Subsistence"].ToString().Contains("R"))
            num1 += Convert.ToDecimal(row["Subsistence"].ToString().Remove(0, 1));
          else if (row["Subsistence"].ToString().Contains("$"))
            num2 += Convert.ToDecimal(row["Subsistence"].ToString().Remove(0, 1));
        }
        if (row["Tools"].ToString() != "")
        {
          if (row["Tools"].ToString().Contains("R"))
            num1 += Convert.ToDecimal(row["Tools"].ToString().Remove(0, 1));
          else if (row["Tools"].ToString().Contains("$"))
            num2 += Convert.ToDecimal(row["Tools"].ToString().Remove(0, 1));
        }
        if (row["Programming_Hours"].ToString() != "")
          num3 += Convert.ToDecimal(row["Programming_Hours"].ToString());
        if (row["Install_Hours"].ToString() != "")
          num3 += Convert.ToDecimal(row["Install_Hours"].ToString());
      }
      this.txt_MP_TotRand.Text = num1.ToString("C");
      this.txt_MP_TotDol.Text = num2.ToString("C", (IFormatProvider) CultureInfo.GetCultureInfo("en-US"));
      this.txt_MP_TotHours.Text = num3.ToString();
      this.bs.DataSource = (object) this.dt;
    }

    public AdvancedDataGridView getLines()
    {
      return this.dgv_ManageProj;
    }

    private void btn_MP_NewLine_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (New_Proj_Line newProjLine = new New_Proj_Line())
      {
        newProjLine.setParent(this);
        int num = (int) newProjLine.ShowDialog();
      }
      this.loadExpenses();
    }

    public string getProjectID()
    {
      return this.Proj_ID;
    }

    public void setHome(Home frmHome)
    {
      this.frmHome = frmHome;
    }

    private void btn_MP_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_MP_RemoveLine_Click(object sender, EventArgs e)
    {
      if (this.dgv_ManageProj.SelectedRows[0].Index > -1)
      {
        int index = this.dgv_ManageProj.SelectedRows[0].Index;
        if (MessageBox.Show("Are you sure you want to remove selected line?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          dbConnection.Open();
          try
          {
            using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM Project_Expenses WHERE ID = '" + this.dt.Rows[index]["ID"].ToString() + "'", dbConnection))
              sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("Line successfully removed.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.loadExpenses();
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show("Please select a line to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void btn_MP_NewLine_MouseEnter(object sender, EventArgs e)
    {
      this.btn_MP_NewLine.Image = (Image) Resources.add_white;
      this.btn_MP_NewLine.ForeColor = Color.White;
    }

    private void btn_MP_NewLine_MouseLeave(object sender, EventArgs e)
    {
      this.btn_MP_NewLine.Image = (Image) Resources.add_grey;
      this.btn_MP_NewLine.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_MP_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_MP_Filter.Image = (Image) Resources.filter_white;
      this.btn_MP_Filter.ForeColor = Color.White;
    }

    private void btn_MP_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_MP_Filter.Image = (Image) Resources.filter_grey;
      this.btn_MP_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_MP_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_MP_ClearFilter.ForeColor = Color.White;
    }

    private void btn_MP_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_MP_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_MP_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_MP_Close.ForeColor = Color.White;
    }

    private void btn_MP_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_MP_Close.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_MP_RemoveLine_MouseEnter(object sender, EventArgs e)
    {
      this.btn_MP_RemoveLine.ForeColor = Color.White;
    }

    private void btn_MP_RemoveLine_MouseLeave(object sender, EventArgs e)
    {
      this.btn_MP_RemoveLine.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void dgv_ManageProj_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_ManageProj.FilterString;
    }

    private void dgv_ManageProj_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_ManageProj.SortString;
    }

    private void btn_MP_Filter_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Projects WHERE Date BETWEEN '" + (object) this.dtp_MP_From.Value + "' AND '" + (object) this.dtp_MP_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_MP_Filter.Visible = false;
      this.btn_MP_ClearFilter.Visible = true;
    }

    private void btn_MP_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadExpenses();
      this.btn_MP_Filter.Visible = true;
      this.btn_MP_ClearFilter.Visible = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Manage_Proj));
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      this.btn_MP_NewLine = new Button();
      this.btn_MP_ClearFilter = new Button();
      this.dtp_MP_From = new BunifuDatepicker();
      this.dgv_ManageProj = new AdvancedDataGridView();
      this.btn_MP_Filter = new Button();
      this.dtp_MP_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.label6 = new Label();
      this.txt_MP_TotHours = new TextBox();
      this.label5 = new Label();
      this.txt_MP_TotRand = new TextBox();
      this.btn_MP_Close = new Button();
      this.label1 = new Label();
      this.txt_MP_TotDol = new TextBox();
      this.btn_MP_RemoveLine = new Button();
      ((ISupportInitialize) this.dgv_ManageProj).BeginInit();
      this.SuspendLayout();
      this.btn_MP_NewLine.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_MP_NewLine.FlatAppearance.BorderSize = 0;
      this.btn_MP_NewLine.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_MP_NewLine.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_MP_NewLine.FlatStyle = FlatStyle.Flat;
      this.btn_MP_NewLine.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_MP_NewLine.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_MP_NewLine.Image = (Image) componentResourceManager.GetObject("btn_MP_NewLine.Image");
      this.btn_MP_NewLine.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_MP_NewLine.Location = new Point(829, 9);
      this.btn_MP_NewLine.Name = "btn_MP_NewLine";
      this.btn_MP_NewLine.Size = new Size(122, 40);
      this.btn_MP_NewLine.TabIndex = 110;
      this.btn_MP_NewLine.Text = "New Line";
      this.btn_MP_NewLine.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_MP_NewLine.UseVisualStyleBackColor = true;
      this.btn_MP_NewLine.Click += new EventHandler(this.btn_MP_NewLine_Click);
      this.btn_MP_NewLine.MouseEnter += new EventHandler(this.btn_MP_NewLine_MouseEnter);
      this.btn_MP_NewLine.MouseLeave += new EventHandler(this.btn_MP_NewLine_MouseLeave);
      this.btn_MP_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_MP_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_MP_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_MP_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_MP_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_MP_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_MP_ClearFilter.Location = new Point(663, 9);
      this.btn_MP_ClearFilter.Name = "btn_MP_ClearFilter";
      this.btn_MP_ClearFilter.Size = new Size(114, 40);
      this.btn_MP_ClearFilter.TabIndex = 109;
      this.btn_MP_ClearFilter.Text = "Clear Filter";
      this.btn_MP_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_MP_ClearFilter.Visible = false;
      this.btn_MP_ClearFilter.Click += new EventHandler(this.btn_MP_ClearFilter_Click);
      this.btn_MP_ClearFilter.MouseEnter += new EventHandler(this.btn_MP_ClearFilter_MouseEnter);
      this.btn_MP_ClearFilter.MouseLeave += new EventHandler(this.btn_MP_ClearFilter_MouseLeave);
      this.dtp_MP_From.BackColor = Color.LightGray;
      this.dtp_MP_From.BorderRadius = 0;
      this.dtp_MP_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_MP_From.Format = DateTimePickerFormat.Short;
      this.dtp_MP_From.FormatCustom = (string) null;
      this.dtp_MP_From.Location = new Point(226, 12);
      this.dtp_MP_From.Name = "dtp_MP_From";
      this.dtp_MP_From.Size = new Size(187, 36);
      this.dtp_MP_From.TabIndex = 103;
      this.dtp_MP_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_ManageProj.AllowUserToAddRows = false;
      this.dgv_ManageProj.AllowUserToDeleteRows = false;
      this.dgv_ManageProj.AllowUserToResizeColumns = false;
      this.dgv_ManageProj.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_ManageProj.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_ManageProj.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_ManageProj.AutoGenerateContextFilters = true;
      this.dgv_ManageProj.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_ManageProj.BorderStyle = BorderStyle.None;
      this.dgv_ManageProj.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_ManageProj.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_ManageProj.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_ManageProj.ColumnHeadersHeight = 25;
      this.dgv_ManageProj.DateWithTime = false;
      this.dgv_ManageProj.EnableHeadersVisualStyles = false;
      this.dgv_ManageProj.Location = new Point(0, 56);
      this.dgv_ManageProj.Name = "dgv_ManageProj";
      this.dgv_ManageProj.ReadOnly = true;
      this.dgv_ManageProj.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_ManageProj.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_ManageProj.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_ManageProj.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_ManageProj.Size = new Size(963, 518);
      this.dgv_ManageProj.TabIndex = 108;
      this.dgv_ManageProj.TimeFilter = false;
      this.dgv_ManageProj.SortStringChanged += new EventHandler(this.dgv_ManageProj_SortStringChanged);
      this.dgv_ManageProj.FilterStringChanged += new EventHandler(this.dgv_ManageProj_FilterStringChanged);
      this.btn_MP_Filter.FlatAppearance.BorderSize = 0;
      this.btn_MP_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_MP_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_MP_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_MP_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_MP_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_MP_Filter.Image = (Image) componentResourceManager.GetObject("btn_MP_Filter.Image");
      this.btn_MP_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_MP_Filter.Location = new Point(663, 10);
      this.btn_MP_Filter.Name = "btn_MP_Filter";
      this.btn_MP_Filter.Size = new Size(114, 40);
      this.btn_MP_Filter.TabIndex = 107;
      this.btn_MP_Filter.Text = "Filter";
      this.btn_MP_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_MP_Filter.UseVisualStyleBackColor = true;
      this.btn_MP_Filter.Click += new EventHandler(this.btn_MP_Filter_Click);
      this.btn_MP_Filter.MouseEnter += new EventHandler(this.btn_MP_Filter_MouseEnter);
      this.btn_MP_Filter.MouseLeave += new EventHandler(this.btn_MP_Filter_MouseLeave);
      this.dtp_MP_To.BackColor = Color.LightGray;
      this.dtp_MP_To.BorderRadius = 0;
      this.dtp_MP_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_MP_To.Format = DateTimePickerFormat.Short;
      this.dtp_MP_To.FormatCustom = (string) null;
      this.dtp_MP_To.Location = new Point(459, 12);
      this.dtp_MP_To.Name = "dtp_MP_To";
      this.dtp_MP_To.Size = new Size(187, 36);
      this.dtp_MP_To.TabIndex = 106;
      this.dtp_MP_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(419, 19);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 105;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(168, 19);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 104;
      this.bunifuCustomLabel5.Text = "From:";
      this.label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6.ForeColor = Color.FromArgb(64, 64, 64);
      this.label6.Location = new Point(299, 587);
      this.label6.Name = "label6";
      this.label6.Size = new Size(86, 17);
      this.label6.TabIndex = 111;
      this.label6.Text = "Total Hours:";
      this.txt_MP_TotHours.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.txt_MP_TotHours.Location = new Point(391, 586);
      this.txt_MP_TotHours.Name = "txt_MP_TotHours";
      this.txt_MP_TotHours.ReadOnly = true;
      this.txt_MP_TotHours.Size = new Size(106, 20);
      this.txt_MP_TotHours.TabIndex = 112;
      this.txt_MP_TotHours.TabStop = false;
      this.label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.ForeColor = Color.FromArgb(64, 64, 64);
      this.label5.Location = new Point(752, 587);
      this.label5.Name = "label5";
      this.label5.Size = new Size(88, 17);
      this.label5.TabIndex = 113;
      this.label5.Text = "Subtotal (R):";
      this.txt_MP_TotRand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.txt_MP_TotRand.Location = new Point(846, 586);
      this.txt_MP_TotRand.Name = "txt_MP_TotRand";
      this.txt_MP_TotRand.ReadOnly = true;
      this.txt_MP_TotRand.Size = new Size(105, 20);
      this.txt_MP_TotRand.TabIndex = 114;
      this.txt_MP_TotRand.TabStop = false;
      this.btn_MP_Close.FlatAppearance.BorderSize = 0;
      this.btn_MP_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_MP_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_MP_Close.FlatStyle = FlatStyle.Flat;
      this.btn_MP_Close.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_MP_Close.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_MP_Close.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_MP_Close.Location = new Point(12, 9);
      this.btn_MP_Close.Name = "btn_MP_Close";
      this.btn_MP_Close.Size = new Size(122, 40);
      this.btn_MP_Close.TabIndex = 115;
      this.btn_MP_Close.Text = "Close";
      this.btn_MP_Close.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_MP_Close.UseVisualStyleBackColor = true;
      this.btn_MP_Close.Click += new EventHandler(this.btn_MP_Close_Click);
      this.btn_MP_Close.MouseEnter += new EventHandler(this.btn_MP_Close_MouseEnter);
      this.btn_MP_Close.MouseLeave += new EventHandler(this.btn_MP_Close_MouseLeave);
      this.label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.ForeColor = Color.FromArgb(64, 64, 64);
      this.label1.Location = new Point(525, 587);
      this.label1.Name = "label1";
      this.label1.Size = new Size(86, 17);
      this.label1.TabIndex = 116;
      this.label1.Text = "Subtotal ($):";
      this.txt_MP_TotDol.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.txt_MP_TotDol.Location = new Point(619, 586);
      this.txt_MP_TotDol.Name = "txt_MP_TotDol";
      this.txt_MP_TotDol.ReadOnly = true;
      this.txt_MP_TotDol.Size = new Size(105, 20);
      this.txt_MP_TotDol.TabIndex = 117;
      this.txt_MP_TotDol.TabStop = false;
      this.btn_MP_RemoveLine.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btn_MP_RemoveLine.FlatAppearance.BorderSize = 0;
      this.btn_MP_RemoveLine.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_MP_RemoveLine.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_MP_RemoveLine.FlatStyle = FlatStyle.Flat;
      this.btn_MP_RemoveLine.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_MP_RemoveLine.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_MP_RemoveLine.Location = new Point(12, 580);
      this.btn_MP_RemoveLine.Name = "btn_MP_RemoveLine";
      this.btn_MP_RemoveLine.Size = new Size(114, 34);
      this.btn_MP_RemoveLine.TabIndex = 118;
      this.btn_MP_RemoveLine.Text = "Remove Line";
      this.btn_MP_RemoveLine.UseVisualStyleBackColor = true;
      this.btn_MP_RemoveLine.Click += new EventHandler(this.btn_MP_RemoveLine_Click);
      this.btn_MP_RemoveLine.MouseEnter += new EventHandler(this.btn_MP_RemoveLine_MouseEnter);
      this.btn_MP_RemoveLine.MouseLeave += new EventHandler(this.btn_MP_RemoveLine_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_MP_RemoveLine);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.txt_MP_TotDol);
      this.Controls.Add((Control) this.btn_MP_Close);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.txt_MP_TotHours);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.txt_MP_TotRand);
      this.Controls.Add((Control) this.btn_MP_NewLine);
      this.Controls.Add((Control) this.btn_MP_ClearFilter);
      this.Controls.Add((Control) this.dtp_MP_From);
      this.Controls.Add((Control) this.dgv_ManageProj);
      this.Controls.Add((Control) this.btn_MP_Filter);
      this.Controls.Add((Control) this.dtp_MP_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (Manage_Proj);
      this.Text = nameof (Manage_Proj);
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Manage_Proj_Load);
      ((ISupportInitialize) this.dgv_ManageProj).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
