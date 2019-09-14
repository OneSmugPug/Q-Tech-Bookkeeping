// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Orders
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
  public class OrdersOld : Form
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
    private BunifuDatepicker dtp_LO_From;
    private AdvancedDataGridView dgv_LOrders;
    private Button btn_LO_NewOrder;
    private Button btn_LO_Filter;
    private BunifuDatepicker dtp_LO_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_LO_SelCli;
    private BunifuSeparator bunifuSeparator2;
    private BunifuMaterialTextbox txt_LO_CName;
    private BunifuMaterialTextbox txt_LO_CCode;
    private Button btn_LO_Next;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_LO_Prev;
    private Button btn_LO_ClearFilter;

    public OrdersOld()
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
      this.dgv_LOrders.DataSource = (object) this.bs;
      this.loadClients();
      this.loadOrders();
      this.dgv_LOrders.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_LOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_LOrders.Columns[5].DefaultCellStyle.Format = "p0";
      this.dgv_LOrders.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_LOrders.Columns[6].DefaultCellStyle.Format = "p0";
      this.dgv_LOrders.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
        if (!this.btn_LO_SelCli.Enabled)
          this.btn_LO_SelCli.Enabled = true;
        if (!this.dgv_LOrders.Enabled)
          this.dgv_LOrders.Enabled = true;
        if (!this.btn_LO_NewOrder.Enabled)
          this.btn_LO_NewOrder.Enabled = true;
        this.NUM_OF_CLIENTS = this.clientsDT.Rows.Count - 1;
        this.txt_LO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LO_CName.Text = this.CNAME;
      }
      else
      {
        this.btn_LO_SelCli.Enabled = false;
        this.dgv_LOrders.Enabled = false;
        this.btn_LO_NewOrder.Enabled = false;
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

    private void btn_Order_CNext_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT + 1 < this.NUM_OF_CLIENTS)
      {
        ++this.CUR_CLIENT;
        this.txt_LO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      else if (this.CUR_CLIENT + 1 == this.NUM_OF_CLIENTS)
      {
        this.btn_LO_Next.Enabled = false;
        ++this.CUR_CLIENT;
        this.txt_LO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_LO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      if (this.CUR_CLIENT == 0 || this.btn_LO_Prev.Enabled)
        return;
      this.btn_LO_Prev.Enabled = true;
    }

    private void btn_Order_CPrev_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT - 1 > 0)
      {
        --this.CUR_CLIENT;
        this.txt_LO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      else if (this.CUR_CLIENT - 1 == 0)
      {
        this.btn_LO_Prev.Enabled = false;
        --this.CUR_CLIENT;
        this.txt_LO_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_LO_CName.Text = this.CNAME;
        this.loadOrders();
      }
      if (this.CUR_CLIENT == this.NUM_OF_CLIENTS || this.btn_LO_Next.Enabled)
        return;
      this.btn_LO_Next.Enabled = true;
    }

    private void btn_Order_CBrowse_Click(object sender, EventArgs e)
    {
      int num = (int) new Client_listOld().ShowDialog((IWin32Window) this);
    }

    public void setNewClient(int rowIdx)
    {
      this.CUR_CLIENT = rowIdx;
      this.loadClients();
      this.loadOrders();
      if (this.CUR_CLIENT != 0 && !this.btn_LO_Prev.Enabled)
        this.btn_LO_Prev.Enabled = true;
      if (this.CUR_CLIENT == 0 && this.btn_LO_Prev.Enabled)
        this.btn_LO_Prev.Enabled = false;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS && !this.btn_LO_Next.Enabled)
        this.btn_LO_Next.Enabled = true;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS || !this.btn_LO_Next.Enabled)
        return;
      this.btn_LO_Next.Enabled = false;
    }

    private void tsb_AddOrder_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (O_AddOld oAdd = new O_AddOld())
      {
        int num = (int) oAdd.ShowDialog((IWin32Window) this);
      }
      this.loadOrders();
    }

    public string getCCode()
    {
      return this.txt_LO_CCode.Text;
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

    private void dgv_Order_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_LOrders.FilterString;
    }

    private void dgv_Order_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_LOrders.SortString;
    }

    private void btn_O_FilterD_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + this.CNAME + "' AND Date BETWEEN '" + (object) this.dtp_LO_From.Value + "' AND '" + (object) this.dtp_LO_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_LO_Filter.Visible = false;
      this.btn_LO_ClearFilter.Visible = true;
    }

    private void btn_O_ClearF_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadOrders();
      this.btn_LO_Filter.Visible = true;
      this.btn_LO_ClearFilter.Visible = false;
    }

    private void dgv_Order_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

    private void btn_LO_Prev_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LO_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_LO_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LO_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_LO_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LO_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_LO_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LO_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_LO_SelCli_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LO_SelCli.Image = (Image) Resources.client_list_white;
      this.btn_LO_SelCli.ForeColor = Color.White;
    }

    private void btn_LO_SelCli_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LO_SelCli.Image = (Image) Resources.user_list;
      this.btn_LO_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LO_NewOrder_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LO_NewOrder.Image = (Image) Resources.add_white;
      this.btn_LO_NewOrder.ForeColor = Color.White;
    }

    private void btn_LO_NewOrder_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LO_NewOrder.Image = (Image) Resources.add_grey;
      this.btn_LO_NewOrder.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LO_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LO_Filter.Image = (Image) Resources.filter_white;
      this.btn_LO_Filter.ForeColor = Color.White;
    }

    private void btn_LO_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LO_Filter.Image = (Image) Resources.filter_grey;
      this.btn_LO_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LO_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LO_ClearFilter.ForeColor = Color.White;
    }

    private void btn_LO_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_LO_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_LO_CName_KeyDown(object sender, KeyEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrdersOld));
      this.dtp_LO_From = new BunifuDatepicker();
      this.dgv_LOrders = new AdvancedDataGridView();
      this.btn_LO_NewOrder = new Button();
      this.btn_LO_Filter = new Button();
      this.dtp_LO_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_LO_SelCli = new Button();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.txt_LO_CName = new BunifuMaterialTextbox();
      this.txt_LO_CCode = new BunifuMaterialTextbox();
      this.btn_LO_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_LO_Prev = new Button();
      this.btn_LO_ClearFilter = new Button();
      ((ISupportInitialize) this.dgv_LOrders).BeginInit();
      this.SuspendLayout();
      this.dtp_LO_From.BackColor = Color.LightGray;
      this.dtp_LO_From.BorderRadius = 0;
      this.dtp_LO_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LO_From.Format = DateTimePickerFormat.Short;
      this.dtp_LO_From.FormatCustom = (string) null;
      this.dtp_LO_From.Location = new Point(70, 235);
      this.dtp_LO_From.Name = "dtp_LO_From";
      this.dtp_LO_From.Size = new Size(208, 36);
      this.dtp_LO_From.TabIndex = 34;
      this.dtp_LO_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_LOrders.AllowUserToAddRows = false;
      this.dgv_LOrders.AllowUserToDeleteRows = false;
      this.dgv_LOrders.AllowUserToResizeColumns = false;
      this.dgv_LOrders.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_LOrders.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_LOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_LOrders.AutoGenerateContextFilters = true;
      this.dgv_LOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_LOrders.BorderStyle = BorderStyle.None;
      this.dgv_LOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_LOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_LOrders.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_LOrders.ColumnHeadersHeight = 25;
      this.dgv_LOrders.DateWithTime = false;
      this.dgv_LOrders.EnableHeadersVisualStyles = false;
      this.dgv_LOrders.Location = new Point(1, 278);
      this.dgv_LOrders.Name = "dgv_LOrders";
      this.dgv_LOrders.ReadOnly = true;
      this.dgv_LOrders.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_LOrders.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_LOrders.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_LOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_LOrders.Size = new Size(963, 342);
      this.dgv_LOrders.TabIndex = 53;
      this.dgv_LOrders.TimeFilter = false;
      this.dgv_LOrders.SortStringChanged += new EventHandler(this.dgv_Order_SortStringChanged);
      this.dgv_LOrders.FilterStringChanged += new EventHandler(this.dgv_Order_FilterStringChanged);
      this.dgv_LOrders.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_Order_CellDoubleClick);
      this.btn_LO_NewOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_LO_NewOrder.FlatAppearance.BorderSize = 0;
      this.btn_LO_NewOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LO_NewOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LO_NewOrder.FlatStyle = FlatStyle.Flat;
      this.btn_LO_NewOrder.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LO_NewOrder.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LO_NewOrder.Image = (Image) componentResourceManager.GetObject("btn_LO_NewOrder.Image");
      this.btn_LO_NewOrder.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LO_NewOrder.Location = new Point(832, 232);
      this.btn_LO_NewOrder.Name = "btn_LO_NewOrder";
      this.btn_LO_NewOrder.Size = new Size(114, 40);
      this.btn_LO_NewOrder.TabIndex = 52;
      this.btn_LO_NewOrder.Text = "New Order";
      this.btn_LO_NewOrder.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LO_NewOrder.UseVisualStyleBackColor = true;
      this.btn_LO_NewOrder.Click += new EventHandler(this.tsb_AddOrder_Click);
      this.btn_LO_NewOrder.MouseEnter += new EventHandler(this.btn_LO_NewOrder_MouseEnter);
      this.btn_LO_NewOrder.MouseLeave += new EventHandler(this.btn_LO_NewOrder_MouseLeave);
      this.btn_LO_Filter.FlatAppearance.BorderSize = 0;
      this.btn_LO_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LO_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LO_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_LO_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LO_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LO_Filter.Image = (Image) componentResourceManager.GetObject("btn_LO_Filter.Image");
      this.btn_LO_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LO_Filter.Location = new Point(556, 232);
      this.btn_LO_Filter.Name = "btn_LO_Filter";
      this.btn_LO_Filter.Size = new Size(114, 40);
      this.btn_LO_Filter.TabIndex = 51;
      this.btn_LO_Filter.Text = "Filter";
      this.btn_LO_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LO_Filter.UseVisualStyleBackColor = true;
      this.btn_LO_Filter.Click += new EventHandler(this.btn_O_FilterD_Click);
      this.btn_LO_Filter.MouseEnter += new EventHandler(this.btn_LO_Filter_MouseEnter);
      this.btn_LO_Filter.MouseLeave += new EventHandler(this.btn_LO_Filter_MouseLeave);
      this.dtp_LO_To.BackColor = Color.LightGray;
      this.dtp_LO_To.BorderRadius = 0;
      this.dtp_LO_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LO_To.Format = DateTimePickerFormat.Short;
      this.dtp_LO_To.FormatCustom = (string) null;
      this.dtp_LO_To.Location = new Point(324, 235);
      this.dtp_LO_To.Name = "dtp_LO_To";
      this.dtp_LO_To.Size = new Size(208, 36);
      this.dtp_LO_To.TabIndex = 50;
      this.dtp_LO_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(284, 242);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 49;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(12, 242);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 48;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_LO_SelCli.FlatAppearance.BorderSize = 0;
      this.btn_LO_SelCli.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LO_SelCli.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LO_SelCli.FlatStyle = FlatStyle.Flat;
      this.btn_LO_SelCli.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LO_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LO_SelCli.Image = (Image) componentResourceManager.GetObject("btn_LO_SelCli.Image");
      this.btn_LO_SelCli.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LO_SelCli.Location = new Point(518, 161);
      this.btn_LO_SelCli.Name = "btn_LO_SelCli";
      this.btn_LO_SelCli.Size = new Size(114, 40);
      this.btn_LO_SelCli.TabIndex = 47;
      this.btn_LO_SelCli.Text = "Client List";
      this.btn_LO_SelCli.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LO_SelCli.UseVisualStyleBackColor = true;
      this.btn_LO_SelCli.Click += new EventHandler(this.btn_Order_CBrowse_Click);
      this.btn_LO_SelCli.MouseEnter += new EventHandler(this.btn_LO_SelCli_MouseEnter);
      this.btn_LO_SelCli.MouseLeave += new EventHandler(this.btn_LO_SelCli_MouseLeave);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(17, 207);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(929, 35);
      this.bunifuSeparator2.TabIndex = 46;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.txt_LO_CName.Cursor = Cursors.IBeam;
      this.txt_LO_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LO_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LO_CName.HintForeColor = Color.Empty;
      this.txt_LO_CName.HintText = "";
      this.txt_LO_CName.isPassword = false;
      this.txt_LO_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LO_CName.LineIdleColor = Color.Gray;
      this.txt_LO_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LO_CName.LineThickness = 1;
      this.txt_LO_CName.Location = new Point(254, 115);
      this.txt_LO_CName.Margin = new Padding(4);
      this.txt_LO_CName.Name = "txt_LO_CName";
      this.txt_LO_CName.Size = new Size(379, 33);
      this.txt_LO_CName.TabIndex = 42;
      this.txt_LO_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_LO_CName.KeyDown += new KeyEventHandler(this.txt_LO_CName_KeyDown);
      this.txt_LO_CCode.Cursor = Cursors.IBeam;
      this.txt_LO_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LO_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LO_CCode.HintForeColor = Color.Empty;
      this.txt_LO_CCode.HintText = "";
      this.txt_LO_CCode.isPassword = false;
      this.txt_LO_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LO_CCode.LineIdleColor = Color.Gray;
      this.txt_LO_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LO_CCode.LineThickness = 1;
      this.txt_LO_CCode.Location = new Point(253, 53);
      this.txt_LO_CCode.Margin = new Padding(4);
      this.txt_LO_CCode.Name = "txt_LO_CCode";
      this.txt_LO_CCode.Size = new Size(379, 33);
      this.txt_LO_CCode.TabIndex = 41;
      this.txt_LO_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_LO_CCode.KeyDown += new KeyEventHandler(this.txt_LO_CCode_KeyDown);
      this.btn_LO_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_LO_Next.FlatAppearance.BorderSize = 0;
      this.btn_LO_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LO_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LO_Next.FlatStyle = FlatStyle.Flat;
      this.btn_LO_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LO_Next.ForeColor = Color.White;
      this.btn_LO_Next.Image = (Image) componentResourceManager.GetObject("btn_LO_Next.Image");
      this.btn_LO_Next.Location = new Point(897, 21);
      this.btn_LO_Next.Name = "btn_LO_Next";
      this.btn_LO_Next.Size = new Size(49, 149);
      this.btn_LO_Next.TabIndex = 38;
      this.btn_LO_Next.UseVisualStyleBackColor = true;
      this.btn_LO_Next.Click += new EventHandler(this.btn_Order_CNext_Click);
      this.btn_LO_Next.MouseEnter += new EventHandler(this.btn_LO_Next_MouseEnter);
      this.btn_LO_Next.MouseLeave += new EventHandler(this.btn_LO_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point((int) sbyte.MaxValue, 117);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(120, 26);
      this.bunifuCustomLabel3.TabIndex = 37;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(132, 56);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(114, 26);
      this.bunifuCustomLabel4.TabIndex = 36;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.btn_LO_Prev.Enabled = false;
      this.btn_LO_Prev.FlatAppearance.BorderSize = 0;
      this.btn_LO_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LO_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LO_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_LO_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LO_Prev.ForeColor = Color.White;
      this.btn_LO_Prev.Image = (Image) componentResourceManager.GetObject("btn_LO_Prev.Image");
      this.btn_LO_Prev.Location = new Point(17, 21);
      this.btn_LO_Prev.Name = "btn_LO_Prev";
      this.btn_LO_Prev.Size = new Size(49, 149);
      this.btn_LO_Prev.TabIndex = 35;
      this.btn_LO_Prev.UseVisualStyleBackColor = true;
      this.btn_LO_Prev.Click += new EventHandler(this.btn_Order_CPrev_Click);
      this.btn_LO_Prev.MouseEnter += new EventHandler(this.btn_LO_Prev_MouseEnter);
      this.btn_LO_Prev.MouseLeave += new EventHandler(this.btn_LO_Prev_MouseLeave);
      this.btn_LO_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_LO_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LO_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LO_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_LO_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LO_ClearFilter.Location = new Point(556, 232);
      this.btn_LO_ClearFilter.Name = "btn_LO_ClearFilter";
      this.btn_LO_ClearFilter.Size = new Size(114, 40);
      this.btn_LO_ClearFilter.TabIndex = 54;
      this.btn_LO_ClearFilter.Text = "Clear Filter";
      this.btn_LO_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_LO_ClearFilter.Visible = false;
      this.btn_LO_ClearFilter.Click += new EventHandler(this.btn_O_ClearF_Click);
      this.btn_LO_ClearFilter.MouseEnter += new EventHandler(this.btn_LO_ClearFilter_MouseEnter);
      this.btn_LO_ClearFilter.MouseLeave += new EventHandler(this.btn_LO_ClearFilter_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_LO_ClearFilter);
      this.Controls.Add((Control) this.dtp_LO_From);
      this.Controls.Add((Control) this.dgv_LOrders);
      this.Controls.Add((Control) this.btn_LO_NewOrder);
      this.Controls.Add((Control) this.btn_LO_Filter);
      this.Controls.Add((Control) this.dtp_LO_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.btn_LO_SelCli);
      this.Controls.Add((Control) this.bunifuSeparator2);
      this.Controls.Add((Control) this.txt_LO_CName);
      this.Controls.Add((Control) this.txt_LO_CCode);
      this.Controls.Add((Control) this.btn_LO_Next);
      this.Controls.Add((Control) this.bunifuCustomLabel3);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.btn_LO_Prev);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(963, 618);
      this.Name = nameof (OrdersOld);
      this.Text = nameof (OrdersOld);
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Orders_Load);
      ((ISupportInitialize) this.dgv_LOrders).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
