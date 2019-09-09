// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Quotes
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
  public class Quotes : Form
  {
    private int CUR_CLIENT = 0;
    private BindingSource bs = new BindingSource();
    private bool isFiltered = false;
    private IContainer components = (IContainer) null;
    private int NUM_OF_CLIENTS;
    private int SELECTED_QUOTE;
    private string CNAME;
    private DataTable clientsDT;
    private DataTable dt;
    private Button btn_LQ_ClearFilter;
    private BunifuDatepicker dtp_LQ_From;
    private AdvancedDataGridView dgv_Contractors;
    private Button btn_C_NewWW;
    private Button btn_LQ_Filter;
    private BunifuDatepicker dtp_LQ_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_LQ_SelCli;
    private BunifuSeparator bunifuSeparator2;
    private BunifuMaterialTextbox txt_LQ_CName;
    private BunifuMaterialTextbox txt_LQ_CCode;
    private Button btn_LQ_Next;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_LQ_Prev;

    public Quotes()
    {
      this.InitializeComponent();
    }

    private void Quotes_Load(object sender, EventArgs e)
    {
      this.clientsDT = new DataTable();
      this.dgv_Contractors.DataSource = (object) this.bs;
      this.loadClients();
      this.loadQuotes();
    }

    private void loadClients()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Clients", dbConnection);
        this.clientsDT = new DataTable();
        sqlDataAdapter.Fill(this.clientsDT);
      }
      if ((uint) this.clientsDT.Rows.Count > 0U)
      {
        if (!this.btn_LQ_SelCli.Enabled)
          this.btn_LQ_SelCli.Enabled = true;
        if (!this.dgv_Contractors.Enabled)
          this.dgv_Contractors.Enabled = true;
        if (!this.btn_C_NewWW.Enabled)
          this.btn_C_NewWW.Enabled = true;
        if (!this.btn_LQ_Filter.Enabled)
          this.btn_LQ_Filter.Enabled = true;
        this.NUM_OF_CLIENTS = this.clientsDT.Rows.Count - 1;
        this.txt_LQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LQ_CName.Text = this.CNAME;
      }
      else
      {
        this.btn_LQ_SelCli.Enabled = false;
        this.dgv_Contractors.Enabled = false;
        this.btn_C_NewWW.Enabled = false;
        this.btn_LQ_Filter.Enabled = false;
      }
    }

    private void loadQuotes()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Quotes_Send WHERE Client = '" + this.CNAME + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_LQ_Next_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT + 1 < this.NUM_OF_CLIENTS)
      {
        ++this.CUR_CLIENT;
        this.txt_LQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      else if (this.CUR_CLIENT + 1 == this.NUM_OF_CLIENTS)
      {
        this.btn_LQ_Next.Enabled = false;
        ++this.CUR_CLIENT;
        this.txt_LQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_LQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      if (this.CUR_CLIENT == 0 || this.btn_LQ_Prev.Enabled)
        return;
      this.btn_LQ_Prev.Enabled = true;
    }

    private void btn_LQ_Prev_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT - 1 > 0)
      {
        --this.CUR_CLIENT;
        this.txt_LQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      else if (this.CUR_CLIENT - 1 == 0)
      {
        this.btn_LQ_Prev.Enabled = false;
        --this.CUR_CLIENT;
        this.txt_LQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_LQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      if (this.CUR_CLIENT == this.NUM_OF_CLIENTS || this.btn_LQ_Next.Enabled)
        return;
      this.btn_LQ_Next.Enabled = true;
    }

    private void btn_LQ_SelCli_Click(object sender, EventArgs e)
    {
      using (Client_listOld clientList = new Client_listOld())
      {
        int num = (int) clientList.ShowDialog((IWin32Window) this);
      }
    }

    public void setNewClient(int rowIdx)
    {
      this.CUR_CLIENT = rowIdx;
      this.loadClients();
      this.loadQuotes();
      if (this.CUR_CLIENT != 0 && !this.btn_LQ_Prev.Enabled)
        this.btn_LQ_Prev.Enabled = true;
      if (this.CUR_CLIENT == 0 && this.btn_LQ_Prev.Enabled)
        this.btn_LQ_Prev.Enabled = false;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS && !this.btn_LQ_Next.Enabled)
        this.btn_LQ_Next.Enabled = true;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS || this.btn_LQ_Next.Enabled)
        return;
      this.btn_LQ_Next.Enabled = false;
    }

    private void btn_LQ_NewQuote_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (Q_Add qAdd = new Q_Add())
      {
        int num = (int) qAdd.ShowDialog((IWin32Window) this);
      }
      this.loadQuotes();
    }

    public string getCCode()
    {
      return this.txt_LQ_CCode.Text;
    }

    public string getCName()
    {
      return this.CNAME;
    }

    public int getSelectedQuote()
    {
      return this.SELECTED_QUOTE;
    }

    public DataTable getQuotes()
    {
      return this.dt;
    }

    private void dgv_LQuotes_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_Contractors.FilterString;
    }

    private void dgv_LQuotes_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_Contractors.SortString;
    }

    private void btn_LQ_Filter_Click(object sender, EventArgs e)
    {
      this.btn_LQ_Filter.Visible = false;
      this.btn_LQ_ClearFilter.Visible = true;
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Quotes_Send WHERE Client = '" + this.CNAME + "' AND Date_Send BETWEEN '" + (object) this.dtp_LQ_From.Value + "' AND '" + (object) this.dtp_LQ_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_LQ_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadQuotes();
      this.btn_LQ_Filter.Visible = true;
      this.btn_LQ_ClearFilter.Visible = false;
    }

    private void dgv_LQuotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      this.SELECTED_QUOTE = e.RowIndex;
      using (Q_Edit_Del qEditDel = new Q_Edit_Del())
      {
        int num = (int) qEditDel.ShowDialog((IWin32Window) this);
      }
      this.loadQuotes();
    }

    private void btn_LQ_Prev_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LQ_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_LQ_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LQ_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_LQ_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LQ_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_LQ_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LQ_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_LQ_SelCli_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LQ_SelCli.Image = (Image) Resources.client_list_white;
      this.btn_LQ_SelCli.ForeColor = Color.White;
    }

    private void btn_LQ_SelCli_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LQ_SelCli.Image = (Image) Resources.user_list;
      this.btn_LQ_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LQ_NewQuote_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_NewWW.Image = (Image) Resources.add_white;
      this.btn_C_NewWW.ForeColor = Color.White;
    }

    private void btn_LQ_NewQuote_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_NewWW.Image = (Image) Resources.add_grey;
      this.btn_C_NewWW.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LQ_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LQ_Filter.Image = (Image) Resources.filter_white;
      this.btn_LQ_Filter.ForeColor = Color.White;
    }

    private void btn_LQ_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LQ_Filter.Image = (Image) Resources.filter_grey;
      this.btn_LQ_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LQ_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LQ_ClearFilter.ForeColor = Color.White;
    }

    private void btn_LO_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LQ_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_LQ_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_LQ_CName_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Quotes));
      this.btn_LQ_ClearFilter = new Button();
      this.dtp_LQ_From = new BunifuDatepicker();
      this.dgv_Contractors = new AdvancedDataGridView();
      this.btn_C_NewWW = new Button();
      this.btn_LQ_Filter = new Button();
      this.dtp_LQ_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_LQ_SelCli = new Button();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.txt_LQ_CName = new BunifuMaterialTextbox();
      this.txt_LQ_CCode = new BunifuMaterialTextbox();
      this.btn_LQ_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_LQ_Prev = new Button();
      ((ISupportInitialize) this.dgv_Contractors).BeginInit();
      this.SuspendLayout();
      this.btn_LQ_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_LQ_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LQ_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LQ_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_LQ_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LQ_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LQ_ClearFilter.Location = new Point(553, 232);
      this.btn_LQ_ClearFilter.Name = "btn_LQ_ClearFilter";
      this.btn_LQ_ClearFilter.Size = new Size(114, 40);
      this.btn_LQ_ClearFilter.TabIndex = 70;
      this.btn_LQ_ClearFilter.Text = "Clear Filter";
      this.btn_LQ_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_LQ_ClearFilter.Visible = false;
      this.btn_LQ_ClearFilter.Click += new EventHandler(this.btn_LQ_ClearFilter_Click);
      this.btn_LQ_ClearFilter.MouseEnter += new EventHandler(this.btn_LQ_ClearFilter_MouseEnter);
      this.btn_LQ_ClearFilter.MouseLeave += new EventHandler(this.btn_LO_ClearFilter_MouseLeave);
      this.dtp_LQ_From.BackColor = Color.LightGray;
      this.dtp_LQ_From.BorderRadius = 0;
      this.dtp_LQ_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LQ_From.Format = DateTimePickerFormat.Short;
      this.dtp_LQ_From.FormatCustom = (string) null;
      this.dtp_LQ_From.Location = new Point(70, 235);
      this.dtp_LQ_From.Name = "dtp_LQ_From";
      this.dtp_LQ_From.Size = new Size(208, 36);
      this.dtp_LQ_From.TabIndex = 55;
      this.dtp_LQ_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_Contractors.AllowUserToAddRows = false;
      this.dgv_Contractors.AllowUserToDeleteRows = false;
      this.dgv_Contractors.AllowUserToResizeColumns = false;
      this.dgv_Contractors.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_Contractors.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_Contractors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_Contractors.AutoGenerateContextFilters = true;
      this.dgv_Contractors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_Contractors.BorderStyle = BorderStyle.None;
      this.dgv_Contractors.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_Contractors.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_Contractors.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_Contractors.ColumnHeadersHeight = 25;
      this.dgv_Contractors.DateWithTime = false;
      this.dgv_Contractors.EnableHeadersVisualStyles = false;
      this.dgv_Contractors.Location = new Point(-1, 278);
      this.dgv_Contractors.Name = "dgv_Contractors";
      this.dgv_Contractors.ReadOnly = true;
      this.dgv_Contractors.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_Contractors.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_Contractors.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_Contractors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Contractors.Size = new Size(963, 340);
      this.dgv_Contractors.TabIndex = 69;
      this.dgv_Contractors.TimeFilter = false;
      this.dgv_Contractors.SortStringChanged += new EventHandler(this.dgv_LQuotes_SortStringChanged);
      this.dgv_Contractors.FilterStringChanged += new EventHandler(this.dgv_LQuotes_FilterStringChanged);
      this.dgv_Contractors.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_LQuotes_CellDoubleClick);
      this.btn_C_NewWW.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_C_NewWW.FlatAppearance.BorderSize = 0;
      this.btn_C_NewWW.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_NewWW.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_NewWW.FlatStyle = FlatStyle.Flat;
      this.btn_C_NewWW.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_NewWW.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_NewWW.Image = (Image) componentResourceManager.GetObject("btn_C_NewWW.Image");
      this.btn_C_NewWW.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_C_NewWW.Location = new Point(826, 232);
      this.btn_C_NewWW.Name = "btn_C_NewWW";
      this.btn_C_NewWW.Size = new Size(122, 40);
      this.btn_C_NewWW.TabIndex = 68;
      this.btn_C_NewWW.Text = "New Quote";
      this.btn_C_NewWW.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_C_NewWW.UseVisualStyleBackColor = true;
      this.btn_C_NewWW.Click += new EventHandler(this.btn_LQ_NewQuote_Click);
      this.btn_C_NewWW.MouseEnter += new EventHandler(this.btn_LQ_NewQuote_MouseEnter);
      this.btn_C_NewWW.MouseLeave += new EventHandler(this.btn_LQ_NewQuote_MouseLeave);
      this.btn_LQ_Filter.FlatAppearance.BorderSize = 0;
      this.btn_LQ_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LQ_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LQ_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_LQ_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LQ_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LQ_Filter.Image = (Image) componentResourceManager.GetObject("btn_LQ_Filter.Image");
      this.btn_LQ_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LQ_Filter.Location = new Point(553, 232);
      this.btn_LQ_Filter.Name = "btn_LQ_Filter";
      this.btn_LQ_Filter.Size = new Size(114, 40);
      this.btn_LQ_Filter.TabIndex = 67;
      this.btn_LQ_Filter.Text = "Filter";
      this.btn_LQ_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LQ_Filter.UseVisualStyleBackColor = true;
      this.btn_LQ_Filter.Click += new EventHandler(this.btn_LQ_Filter_Click);
      this.btn_LQ_Filter.MouseEnter += new EventHandler(this.btn_LQ_Filter_MouseEnter);
      this.btn_LQ_Filter.MouseLeave += new EventHandler(this.btn_LQ_Filter_MouseLeave);
      this.dtp_LQ_To.BackColor = Color.LightGray;
      this.dtp_LQ_To.BorderRadius = 0;
      this.dtp_LQ_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LQ_To.Format = DateTimePickerFormat.Short;
      this.dtp_LQ_To.FormatCustom = (string) null;
      this.dtp_LQ_To.Location = new Point(324, 235);
      this.dtp_LQ_To.Name = "dtp_LQ_To";
      this.dtp_LQ_To.Size = new Size(208, 36);
      this.dtp_LQ_To.TabIndex = 66;
      this.dtp_LQ_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(284, 242);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 65;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(12, 242);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 64;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_LQ_SelCli.FlatAppearance.BorderSize = 0;
      this.btn_LQ_SelCli.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LQ_SelCli.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LQ_SelCli.FlatStyle = FlatStyle.Flat;
      this.btn_LQ_SelCli.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LQ_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LQ_SelCli.Image = (Image) componentResourceManager.GetObject("btn_LQ_SelCli.Image");
      this.btn_LQ_SelCli.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LQ_SelCli.Location = new Point(518, 161);
      this.btn_LQ_SelCli.Name = "btn_LQ_SelCli";
      this.btn_LQ_SelCli.Size = new Size(114, 40);
      this.btn_LQ_SelCli.TabIndex = 63;
      this.btn_LQ_SelCli.Text = "Client List";
      this.btn_LQ_SelCli.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LQ_SelCli.UseVisualStyleBackColor = true;
      this.btn_LQ_SelCli.Click += new EventHandler(this.btn_LQ_SelCli_Click);
      this.btn_LQ_SelCli.MouseEnter += new EventHandler(this.btn_LQ_SelCli_MouseEnter);
      this.btn_LQ_SelCli.MouseLeave += new EventHandler(this.btn_LQ_SelCli_MouseLeave);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(17, 207);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(934, 35);
      this.bunifuSeparator2.TabIndex = 62;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.txt_LQ_CName.Cursor = Cursors.IBeam;
      this.txt_LQ_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LQ_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LQ_CName.HintForeColor = Color.Empty;
      this.txt_LQ_CName.HintText = "";
      this.txt_LQ_CName.isPassword = false;
      this.txt_LQ_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LQ_CName.LineIdleColor = Color.Gray;
      this.txt_LQ_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LQ_CName.LineThickness = 1;
      this.txt_LQ_CName.Location = new Point(254, 115);
      this.txt_LQ_CName.Margin = new Padding(4);
      this.txt_LQ_CName.Name = "txt_LQ_CName";
      this.txt_LQ_CName.Size = new Size(379, 33);
      this.txt_LQ_CName.TabIndex = 61;
      this.txt_LQ_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_LQ_CName.KeyDown += new KeyEventHandler(this.txt_LQ_CName_KeyDown);
      this.txt_LQ_CCode.Cursor = Cursors.IBeam;
      this.txt_LQ_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LQ_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LQ_CCode.HintForeColor = Color.Empty;
      this.txt_LQ_CCode.HintText = "";
      this.txt_LQ_CCode.isPassword = false;
      this.txt_LQ_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LQ_CCode.LineIdleColor = Color.Gray;
      this.txt_LQ_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LQ_CCode.LineThickness = 1;
      this.txt_LQ_CCode.Location = new Point(253, 53);
      this.txt_LQ_CCode.Margin = new Padding(4);
      this.txt_LQ_CCode.Name = "txt_LQ_CCode";
      this.txt_LQ_CCode.Size = new Size(379, 33);
      this.txt_LQ_CCode.TabIndex = 60;
      this.txt_LQ_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_LQ_CCode.KeyDown += new KeyEventHandler(this.txt_LQ_CCode_KeyDown);
      this.btn_LQ_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_LQ_Next.FlatAppearance.BorderSize = 0;
      this.btn_LQ_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LQ_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LQ_Next.FlatStyle = FlatStyle.Flat;
      this.btn_LQ_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LQ_Next.ForeColor = Color.White;
      this.btn_LQ_Next.Image = (Image) componentResourceManager.GetObject("btn_LQ_Next.Image");
      this.btn_LQ_Next.Location = new Point(897, 21);
      this.btn_LQ_Next.Name = "btn_LQ_Next";
      this.btn_LQ_Next.Size = new Size(49, 149);
      this.btn_LQ_Next.TabIndex = 59;
      this.btn_LQ_Next.UseVisualStyleBackColor = true;
      this.btn_LQ_Next.Click += new EventHandler(this.btn_LQ_Next_Click);
      this.btn_LQ_Next.MouseEnter += new EventHandler(this.btn_LQ_Next_MouseEnter);
      this.btn_LQ_Next.MouseLeave += new EventHandler(this.btn_LQ_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point((int) sbyte.MaxValue, 117);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(120, 26);
      this.bunifuCustomLabel3.TabIndex = 58;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(132, 56);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(114, 26);
      this.bunifuCustomLabel4.TabIndex = 57;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.btn_LQ_Prev.Enabled = false;
      this.btn_LQ_Prev.FlatAppearance.BorderSize = 0;
      this.btn_LQ_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LQ_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LQ_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_LQ_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LQ_Prev.ForeColor = Color.White;
      this.btn_LQ_Prev.Image = (Image) componentResourceManager.GetObject("btn_LQ_Prev.Image");
      this.btn_LQ_Prev.Location = new Point(17, 21);
      this.btn_LQ_Prev.Name = "btn_LQ_Prev";
      this.btn_LQ_Prev.Size = new Size(49, 149);
      this.btn_LQ_Prev.TabIndex = 56;
      this.btn_LQ_Prev.UseVisualStyleBackColor = true;
      this.btn_LQ_Prev.Click += new EventHandler(this.btn_LQ_Prev_Click);
      this.btn_LQ_Prev.MouseEnter += new EventHandler(this.btn_LQ_Prev_MouseEnter);
      this.btn_LQ_Prev.MouseLeave += new EventHandler(this.btn_LQ_Prev_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_LQ_ClearFilter);
      this.Controls.Add((Control) this.dtp_LQ_From);
      this.Controls.Add((Control) this.dgv_Contractors);
      this.Controls.Add((Control) this.btn_C_NewWW);
      this.Controls.Add((Control) this.btn_LQ_Filter);
      this.Controls.Add((Control) this.dtp_LQ_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.btn_LQ_SelCli);
      this.Controls.Add((Control) this.bunifuSeparator2);
      this.Controls.Add((Control) this.txt_LQ_CName);
      this.Controls.Add((Control) this.txt_LQ_CCode);
      this.Controls.Add((Control) this.btn_LQ_Next);
      this.Controls.Add((Control) this.bunifuCustomLabel3);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.btn_LQ_Prev);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(640, 510);
      this.Name = nameof (Quotes);
      this.Text = nameof (Quotes);
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Quotes_Load);
      ((ISupportInitialize) this.dgv_Contractors).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
