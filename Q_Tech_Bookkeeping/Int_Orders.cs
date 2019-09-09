// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Int_Orders
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
  public class Int_Orders : Form
  {
    private int CUR_CLIENT = 0;
    private BindingSource bs = new BindingSource();
    private bool isFiltered = false;
    private IContainer components = (IContainer) null;
    private int NUM_OF_CLIENTS;
    private int SELECTED_ORDER;
    private string CNAME;
    private DataTable clientsDT;
    private DataTable dt;
    private Button btn_IO_ClearFilter;
    private BunifuDatepicker dtp_IO_From;
    private AdvancedDataGridView dgv_IOrders;
    private Button btn_IO_NewOrder;
    private Button btn_IO_Filter;
    private BunifuDatepicker dtp_IO_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_IO_SelCli;
    private BunifuSeparator bunifuSeparator2;
    private BunifuMaterialTextbox txt_IO_CName;
    private BunifuMaterialTextbox txt_IO_CCode;
    private Button btn_IO_Next;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_IO_Prev;

    public Int_Orders()
    {
      this.InitializeComponent();
    }

    private void Orders_Load(object sender, EventArgs e)
    {
      this.clientsDT = new DataTable();
      this.dt = new DataTable();
      this.dt.Columns.Add(string.Empty);
      this.dt.Rows.Add();
      this.bs.DataSource = (object) this.dt;
      this.dgv_IOrders.DataSource = (object) this.bs;
      this.loadClients();
      this.loadOrders();
      this.dgv_IOrders.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
      this.dgv_IOrders.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_IOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_IOrders.Columns[5].DefaultCellStyle.Format = "p0";
      this.dgv_IOrders.Columns[6].DefaultCellStyle.Format = "p0";
    }

    private void loadClients()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Int_Clients", dbConnection);
        this.clientsDT = new DataTable();
        sqlDataAdapter.Fill(this.clientsDT);
      }
      if ((uint) this.clientsDT.Rows.Count > 0U)
      {
        if (!this.btn_IO_SelCli.Enabled)
          this.btn_IO_SelCli.Enabled = true;
        if (!this.dgv_IOrders.Enabled)
          this.dgv_IOrders.Enabled = true;
        if (!this.btn_IO_NewOrder.Enabled)
          this.btn_IO_NewOrder.Enabled = true;
        this.NUM_OF_CLIENTS = this.clientsDT.Rows.Count - 1;
        this.txt_IO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IO_CName.Text = this.CNAME;
      }
      else
      {
        this.btn_IO_SelCli.Enabled = false;
        this.dgv_IOrders.Enabled = false;
        this.btn_IO_NewOrder.Enabled = false;
      }
    }

    private void loadOrders()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + this.CNAME + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_IO_Next_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT + 1 < this.NUM_OF_CLIENTS)
      {
        ++this.CUR_CLIENT;
        this.txt_IO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      else if (this.CUR_CLIENT + 1 == this.NUM_OF_CLIENTS)
      {
        this.btn_IO_Next.Enabled = false;
        ++this.CUR_CLIENT;
        this.txt_IO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_IO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      if (this.CUR_CLIENT == 0 || this.btn_IO_Prev.Enabled)
        return;
      this.btn_IO_Prev.Enabled = true;
    }

    private void btn_IO_Prev_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT - 1 > 0)
      {
        --this.CUR_CLIENT;
        this.txt_IO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      else if (this.CUR_CLIENT - 1 == 0)
      {
        this.btn_IO_Prev.Enabled = false;
        --this.CUR_CLIENT;
        this.txt_IO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_IO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      if (this.CUR_CLIENT == this.NUM_OF_CLIENTS || this.btn_IO_Next.Enabled)
        return;
      this.btn_IO_Next.Enabled = true;
    }

    private void btn_IO_SelCli_Click(object sender, EventArgs e)
    {
      using (Client_list clientList = new Client_list())
      {
        int num = (int) clientList.ShowDialog((IWin32Window) this);
      }
    }

    public void setNewClient(int rowIdx)
    {
      this.CUR_CLIENT = rowIdx;
      this.loadClients();
      this.loadOrders();
      if (this.CUR_CLIENT != 0 && !this.btn_IO_Prev.Enabled)
        this.btn_IO_Prev.Enabled = true;
      if (this.CUR_CLIENT == 0 && this.btn_IO_Prev.Enabled)
        this.btn_IO_Prev.Enabled = false;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS && !this.btn_IO_Next.Enabled)
        this.btn_IO_Next.Enabled = true;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS || !this.btn_IO_Next.Enabled)
        return;
      this.btn_IO_Next.Enabled = false;
    }

    private void btn_IO_NewOrder_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (O_Add oAdd = new O_Add())
      {
        int num = (int) oAdd.ShowDialog((IWin32Window) this);
      }
      this.loadOrders();
    }

    public string getCCode()
    {
      return this.txt_IO_CCode.Text;
    }

    public string getCName()
    {
      return this.CNAME;
    }

    public int getSelectedOrder()
    {
      return this.SELECTED_ORDER;
    }

    public DataTable getOrders()
    {
      return this.dt;
    }

    private void dgv_IOrders_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_IOrders.FilterString;
    }

    private void dgv_IOrders_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_IOrders.SortString;
    }

    private void btn_IO_Filter_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + this.CNAME + "' AND Date BETWEEN '" + (object) this.dtp_IO_From.Value + "' AND '" + (object) this.dtp_IO_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_IO_Filter.Visible = false;
      this.btn_IO_ClearFilter.Visible = true;
    }

    private void btn_IO_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadOrders();
      this.btn_IO_Filter.Visible = true;
      this.btn_IO_ClearFilter.Visible = false;
    }

    private void dgv_IOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      this.SELECTED_ORDER = e.RowIndex;
      using (O_Edit_Del oEditDel = new O_Edit_Del())
      {
        int num = (int) oEditDel.ShowDialog((IWin32Window) this);
      }
      this.loadOrders();
    }

    private void btn_IO_Prev_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IO_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_IO_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IO_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_IO_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IO_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_IO_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IO_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_IO_SelCli_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IO_SelCli.Image = (Image) Resources.client_list_white;
      this.btn_IO_SelCli.ForeColor = Color.White;
    }

    private void btn_IO_SelCli_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IO_SelCli.Image = (Image) Resources.user_list;
      this.btn_IO_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IO_NewOrder_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IO_NewOrder.Image = (Image) Resources.add_white;
      this.btn_IO_NewOrder.ForeColor = Color.White;
    }

    private void btn_IO_NewOrder_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IO_NewOrder.Image = (Image) Resources.add_grey;
      this.btn_IO_NewOrder.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IO_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IO_Filter.Image = (Image) Resources.filter_white;
      this.btn_IO_Filter.ForeColor = Color.White;
    }

    private void btn_IO_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IO_Filter.Image = (Image) Resources.filter_grey;
      this.btn_IO_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IO_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IO_ClearFilter.ForeColor = Color.White;
    }

    private void btn_IO_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_IO_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_IO_CName_KeyDown(object sender, KeyEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Int_Orders));
      this.btn_IO_ClearFilter = new Button();
      this.dtp_IO_From = new BunifuDatepicker();
      this.dgv_IOrders = new AdvancedDataGridView();
      this.btn_IO_NewOrder = new Button();
      this.btn_IO_Filter = new Button();
      this.dtp_IO_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_IO_SelCli = new Button();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.txt_IO_CName = new BunifuMaterialTextbox();
      this.txt_IO_CCode = new BunifuMaterialTextbox();
      this.btn_IO_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_IO_Prev = new Button();
      ((ISupportInitialize) this.dgv_IOrders).BeginInit();
      this.SuspendLayout();
      this.btn_IO_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_IO_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IO_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IO_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_IO_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IO_ClearFilter.Location = new Point(555, 230);
      this.btn_IO_ClearFilter.Name = "btn_IO_ClearFilter";
      this.btn_IO_ClearFilter.Size = new Size(114, 40);
      this.btn_IO_ClearFilter.TabIndex = 70;
      this.btn_IO_ClearFilter.Text = "Clear Filter";
      this.btn_IO_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_IO_ClearFilter.Visible = false;
      this.btn_IO_ClearFilter.Click += new EventHandler(this.btn_IO_ClearFilter_Click);
      this.btn_IO_ClearFilter.MouseEnter += new EventHandler(this.btn_IO_ClearFilter_MouseEnter);
      this.btn_IO_ClearFilter.MouseLeave += new EventHandler(this.btn_IO_ClearFilter_MouseLeave);
      this.dtp_IO_From.BackColor = Color.LightGray;
      this.dtp_IO_From.BorderRadius = 0;
      this.dtp_IO_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_IO_From.Format = DateTimePickerFormat.Short;
      this.dtp_IO_From.FormatCustom = (string) null;
      this.dtp_IO_From.Location = new Point(69, 233);
      this.dtp_IO_From.Name = "dtp_IO_From";
      this.dtp_IO_From.Size = new Size(208, 36);
      this.dtp_IO_From.TabIndex = 55;
      this.dtp_IO_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_IOrders.AllowUserToAddRows = false;
      this.dgv_IOrders.AllowUserToDeleteRows = false;
      this.dgv_IOrders.AllowUserToResizeColumns = false;
      this.dgv_IOrders.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_IOrders.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_IOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_IOrders.AutoGenerateContextFilters = true;
      this.dgv_IOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_IOrders.BorderStyle = BorderStyle.None;
      this.dgv_IOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_IOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_IOrders.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_IOrders.ColumnHeadersHeight = 25;
      this.dgv_IOrders.DateWithTime = false;
      this.dgv_IOrders.EnableHeadersVisualStyles = false;
      this.dgv_IOrders.Location = new Point(0, 276);
      this.dgv_IOrders.Name = "dgv_IOrders";
      this.dgv_IOrders.ReadOnly = true;
      this.dgv_IOrders.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_IOrders.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_IOrders.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_IOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_IOrders.Size = new Size(963, 342);
      this.dgv_IOrders.TabIndex = 69;
      this.dgv_IOrders.TimeFilter = false;
      this.dgv_IOrders.SortStringChanged += new EventHandler(this.dgv_IOrders_SortStringChanged);
      this.dgv_IOrders.FilterStringChanged += new EventHandler(this.dgv_IOrders_FilterStringChanged);
      this.dgv_IOrders.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_IOrders_CellDoubleClick);
      this.btn_IO_NewOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_IO_NewOrder.FlatAppearance.BorderSize = 0;
      this.btn_IO_NewOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IO_NewOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IO_NewOrder.FlatStyle = FlatStyle.Flat;
      this.btn_IO_NewOrder.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IO_NewOrder.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IO_NewOrder.Image = (Image) componentResourceManager.GetObject("btn_IO_NewOrder.Image");
      this.btn_IO_NewOrder.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IO_NewOrder.Location = new Point(831, 230);
      this.btn_IO_NewOrder.Name = "btn_IO_NewOrder";
      this.btn_IO_NewOrder.Size = new Size(114, 40);
      this.btn_IO_NewOrder.TabIndex = 68;
      this.btn_IO_NewOrder.Text = "New Order";
      this.btn_IO_NewOrder.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IO_NewOrder.UseVisualStyleBackColor = true;
      this.btn_IO_NewOrder.Click += new EventHandler(this.btn_IO_NewOrder_Click);
      this.btn_IO_NewOrder.MouseEnter += new EventHandler(this.btn_IO_NewOrder_MouseEnter);
      this.btn_IO_NewOrder.MouseLeave += new EventHandler(this.btn_IO_NewOrder_MouseLeave);
      this.btn_IO_Filter.FlatAppearance.BorderSize = 0;
      this.btn_IO_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IO_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IO_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_IO_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IO_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IO_Filter.Image = (Image) componentResourceManager.GetObject("btn_IO_Filter.Image");
      this.btn_IO_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IO_Filter.Location = new Point(555, 230);
      this.btn_IO_Filter.Name = "btn_IO_Filter";
      this.btn_IO_Filter.Size = new Size(114, 40);
      this.btn_IO_Filter.TabIndex = 67;
      this.btn_IO_Filter.Text = "Filter";
      this.btn_IO_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IO_Filter.UseVisualStyleBackColor = true;
      this.btn_IO_Filter.Click += new EventHandler(this.btn_IO_Filter_Click);
      this.btn_IO_Filter.MouseEnter += new EventHandler(this.btn_IO_Filter_MouseEnter);
      this.btn_IO_Filter.MouseLeave += new EventHandler(this.btn_IO_Filter_MouseLeave);
      this.dtp_IO_To.BackColor = Color.LightGray;
      this.dtp_IO_To.BorderRadius = 0;
      this.dtp_IO_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_IO_To.Format = DateTimePickerFormat.Short;
      this.dtp_IO_To.FormatCustom = (string) null;
      this.dtp_IO_To.Location = new Point(323, 233);
      this.dtp_IO_To.Name = "dtp_IO_To";
      this.dtp_IO_To.Size = new Size(208, 36);
      this.dtp_IO_To.TabIndex = 66;
      this.dtp_IO_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(283, 240);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 65;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(11, 240);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 64;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_IO_SelCli.FlatAppearance.BorderSize = 0;
      this.btn_IO_SelCli.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IO_SelCli.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IO_SelCli.FlatStyle = FlatStyle.Flat;
      this.btn_IO_SelCli.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IO_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IO_SelCli.Image = (Image) componentResourceManager.GetObject("btn_IO_SelCli.Image");
      this.btn_IO_SelCli.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IO_SelCli.Location = new Point(517, 159);
      this.btn_IO_SelCli.Name = "btn_IO_SelCli";
      this.btn_IO_SelCli.Size = new Size(114, 40);
      this.btn_IO_SelCli.TabIndex = 63;
      this.btn_IO_SelCli.Text = "Client List";
      this.btn_IO_SelCli.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IO_SelCli.UseVisualStyleBackColor = true;
      this.btn_IO_SelCli.Click += new EventHandler(this.btn_IO_SelCli_Click);
      this.btn_IO_SelCli.MouseEnter += new EventHandler(this.btn_IO_SelCli_MouseEnter);
      this.btn_IO_SelCli.MouseLeave += new EventHandler(this.btn_IO_SelCli_MouseLeave);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(16, 205);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(929, 35);
      this.bunifuSeparator2.TabIndex = 62;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.txt_IO_CName.Cursor = Cursors.IBeam;
      this.txt_IO_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IO_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IO_CName.HintForeColor = Color.Empty;
      this.txt_IO_CName.HintText = "";
      this.txt_IO_CName.isPassword = false;
      this.txt_IO_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IO_CName.LineIdleColor = Color.Gray;
      this.txt_IO_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IO_CName.LineThickness = 1;
      this.txt_IO_CName.Location = new Point(253, 113);
      this.txt_IO_CName.Margin = new Padding(4);
      this.txt_IO_CName.Name = "txt_IO_CName";
      this.txt_IO_CName.Size = new Size(379, 33);
      this.txt_IO_CName.TabIndex = 61;
      this.txt_IO_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_IO_CName.KeyDown += new KeyEventHandler(this.txt_IO_CName_KeyDown);
      this.txt_IO_CCode.Cursor = Cursors.IBeam;
      this.txt_IO_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IO_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IO_CCode.HintForeColor = Color.Empty;
      this.txt_IO_CCode.HintText = "";
      this.txt_IO_CCode.isPassword = false;
      this.txt_IO_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IO_CCode.LineIdleColor = Color.Gray;
      this.txt_IO_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IO_CCode.LineThickness = 1;
      this.txt_IO_CCode.Location = new Point(252, 51);
      this.txt_IO_CCode.Margin = new Padding(4);
      this.txt_IO_CCode.Name = "txt_IO_CCode";
      this.txt_IO_CCode.Size = new Size(379, 33);
      this.txt_IO_CCode.TabIndex = 60;
      this.txt_IO_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_IO_CCode.KeyDown += new KeyEventHandler(this.txt_IO_CCode_KeyDown);
      this.btn_IO_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_IO_Next.FlatAppearance.BorderSize = 0;
      this.btn_IO_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IO_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IO_Next.FlatStyle = FlatStyle.Flat;
      this.btn_IO_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IO_Next.ForeColor = Color.White;
      this.btn_IO_Next.Image = (Image) componentResourceManager.GetObject("btn_IO_Next.Image");
      this.btn_IO_Next.Location = new Point(896, 19);
      this.btn_IO_Next.Name = "btn_IO_Next";
      this.btn_IO_Next.Size = new Size(49, 149);
      this.btn_IO_Next.TabIndex = 59;
      this.btn_IO_Next.UseVisualStyleBackColor = true;
      this.btn_IO_Next.Click += new EventHandler(this.btn_IO_Next_Click);
      this.btn_IO_Next.MouseEnter += new EventHandler(this.btn_IO_Next_MouseEnter);
      this.btn_IO_Next.MouseLeave += new EventHandler(this.btn_IO_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point(126, 115);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(120, 26);
      this.bunifuCustomLabel3.TabIndex = 58;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(131, 54);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(114, 26);
      this.bunifuCustomLabel4.TabIndex = 57;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.btn_IO_Prev.Enabled = false;
      this.btn_IO_Prev.FlatAppearance.BorderSize = 0;
      this.btn_IO_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IO_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IO_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_IO_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IO_Prev.ForeColor = Color.White;
      this.btn_IO_Prev.Image = (Image) componentResourceManager.GetObject("btn_IO_Prev.Image");
      this.btn_IO_Prev.Location = new Point(16, 19);
      this.btn_IO_Prev.Name = "btn_IO_Prev";
      this.btn_IO_Prev.Size = new Size(49, 149);
      this.btn_IO_Prev.TabIndex = 56;
      this.btn_IO_Prev.UseVisualStyleBackColor = true;
      this.btn_IO_Prev.Click += new EventHandler(this.btn_IO_Prev_Click);
      this.btn_IO_Prev.MouseEnter += new EventHandler(this.btn_IO_Prev_MouseEnter);
      this.btn_IO_Prev.MouseLeave += new EventHandler(this.btn_IO_Prev_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_IO_ClearFilter);
      this.Controls.Add((Control) this.dtp_IO_From);
      this.Controls.Add((Control) this.dgv_IOrders);
      this.Controls.Add((Control) this.btn_IO_NewOrder);
      this.Controls.Add((Control) this.btn_IO_Filter);
      this.Controls.Add((Control) this.dtp_IO_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.btn_IO_SelCli);
      this.Controls.Add((Control) this.bunifuSeparator2);
      this.Controls.Add((Control) this.txt_IO_CName);
      this.Controls.Add((Control) this.txt_IO_CCode);
      this.Controls.Add((Control) this.btn_IO_Next);
      this.Controls.Add((Control) this.bunifuCustomLabel3);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.btn_IO_Prev);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(625, 510);
      this.Name = nameof (Int_Orders);
      this.Text = "International Orders";
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Orders_Load);
      ((ISupportInitialize) this.dgv_IOrders).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
