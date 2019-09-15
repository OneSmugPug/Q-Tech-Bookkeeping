// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Invoices_Send
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
  public class Invoices_Send : Form
  {
    private int CUR_CLIENT = 0;
    private BindingSource bs = new BindingSource();
    private bool isFiltered = false;
    private IContainer components = (IContainer) null;
    private int NUM_OF_CLIENTS;
    private int SELECTED_INVSEND;
    private string CNAME;
    private string NEW_INVOICE;
    private DataTable clientsDT;
    private DataTable dt;
    private Button btn_LIS_ClearFilter;
    private BunifuDatepicker dtp_LIS_From;
    private AdvancedDataGridView dgv_LInvSent;
    private Button btn_LIS_NewIS;
    private Button btn_LIS_Filter;
    private BunifuDatepicker dtp_LIS_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_LIS_SelCli;
    private BunifuSeparator bunifuSeparator2;
    private BunifuMaterialTextbox txt_LIS_CName;
    private BunifuMaterialTextbox txt_LIS_CCode;
    private Button btn_LIS_Next;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_LIS_Prev;

    public Invoices_Send()
    {
      this.InitializeComponent();
    }

    private void Invoices_Send_Load(object sender, EventArgs e)
    {
      this.clientsDT = new DataTable();
      this.dgv_LInvSent.DataSource = (object) this.bs;
      this.loadClients();
      this.loadInvSend();
      this.dgv_LInvSent.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_LInvSent.Columns[5].DefaultCellStyle.Format = "c";
      this.dgv_LInvSent.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_LInvSent.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
        if (!this.btn_LIS_SelCli.Enabled)
          this.btn_LIS_SelCli.Enabled = true;
        if (!this.dgv_LInvSent.Enabled)
          this.dgv_LInvSent.Enabled = true;
        if (!this.btn_LIS_NewIS.Enabled)
          this.btn_LIS_NewIS.Enabled = true;
        if (!this.btn_LIS_Filter.Enabled)
          this.btn_LIS_Filter.Enabled = true;
        this.NUM_OF_CLIENTS = this.clientsDT.Rows.Count - 1;
        this.txt_LIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LIS_CName.Text = this.CNAME;
      }
      else
      {
        this.btn_LIS_SelCli.Enabled = false;
        this.dgv_LInvSent.Enabled = false;
        this.btn_LIS_NewIS.Enabled = false;
        this.btn_LIS_Filter.Enabled = false;
      }
    }

    private void loadInvSend()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Send WHERE Client LIKE '" + this.CNAME + "%'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      Decimal num = new Decimal();
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row["Total_Amount"].ToString() != string.Empty)
          num += Convert.ToDecimal(row["Total_Amount"].ToString());
        else
          num += Decimal.Zero;
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_LIS_Next_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT + 1 < this.NUM_OF_CLIENTS)
      {
        ++this.CUR_CLIENT;
        this.txt_LIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      else if (this.CUR_CLIENT + 1 == this.NUM_OF_CLIENTS)
      {
        this.btn_LIS_Next.Enabled = false;
        ++this.CUR_CLIENT;
        this.txt_LIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_LIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      if (this.CUR_CLIENT == 0 || this.btn_LIS_Prev.Enabled)
        return;
      this.btn_LIS_Prev.Enabled = true;
    }

    private void btn_LIS_Prev_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT - 1 > 0)
      {
        --this.CUR_CLIENT;
        this.txt_LIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_LIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      else if (this.CUR_CLIENT - 1 == 0)
      {
        this.btn_LIS_Prev.Enabled = false;
        --this.CUR_CLIENT;
        this.txt_LIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_LIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      if (this.CUR_CLIENT == this.NUM_OF_CLIENTS || this.btn_LIS_Next.Enabled)
        return;
      this.btn_LIS_Next.Enabled = true;
    }

    private void btn_LIS_SelCli_Click(object sender, EventArgs e)
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
      this.loadInvSend();
      if (this.CUR_CLIENT != 0 && !this.btn_LIS_Prev.Enabled)
        this.btn_LIS_Prev.Enabled = true;
      if (this.CUR_CLIENT == 0 && this.btn_LIS_Prev.Enabled)
        this.btn_LIS_Prev.Enabled = false;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS && !this.btn_LIS_Next.Enabled)
        this.btn_LIS_Next.Enabled = true;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS || !this.btn_LIS_Next.Enabled)
        return;
      this.btn_LIS_Next.Enabled = false;
    }

    private void btn_LIS_NewIS_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (Inv_Send_Add invSendAdd = new Inv_Send_Add())
      {
        int num = (int) invSendAdd.ShowDialog((IWin32Window) this);
      }
      this.loadInvSend();
    }

    public string getCCode()
    {
      return this.txt_LIS_CCode.Text;
    }

    public string getCName()
    {
      return this.CNAME;
    }

    public int getSelectedInvSend()
    {
      return this.SELECTED_INVSEND;
    }

    public DataTable getInvoices()
    {
      return this.dt;
    }

    public void setNewInvoice(string invNum)
    {
      this.NEW_INVOICE = invNum;
    }

    private void dgv_LInvSent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      this.SELECTED_INVSEND = e.RowIndex;
      using (Inv_Send_Edit_DelOld invSendEditDel = new Inv_Send_Edit_DelOld())
      {
        int num = (int) invSendEditDel.ShowDialog((IWin32Window) this);
      }
      this.loadInvSend();
    }

    private void dgv_LInvSent_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_LInvSent.FilterString;
    }

    private void dgv_LInvSent_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_LInvSent.SortString;
    }

    private void btn_LIS_Filter_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Send WHERE Client LIKE '" + this.CNAME + "%' AND Date BETWEEN '" + (object) this.dtp_LIS_From.Value + "' AND '" + (object) this.dtp_LIS_To.Value + "' OR Client LIKE '" + this.CNAME + "%' AND Date_Paid BETWEEN '" + (object) this.dtp_LIS_From.Value + "' AND '" + (object) this.dtp_LIS_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      Decimal num = new Decimal();
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row["Total_Amount"].ToString() != string.Empty)
          num += Convert.ToDecimal(row["Total_Amount"].ToString());
        else
          num += Decimal.Zero;
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_LIS_Filter.Visible = false;
      this.btn_LIS_ClearFilter.Visible = true;
    }

    private void btn_LIS_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadInvSend();
      this.btn_LIS_Filter.Visible = true;
      this.btn_LIS_ClearFilter.Visible = false;
    }

    private void btn_LIS_Prev_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIS_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_LIS_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIS_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_LIS_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIS_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_LIS_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIS_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_LIS_SelCli_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIS_SelCli.Image = (Image) Resources.client_list_white;
      this.btn_LIS_SelCli.ForeColor = Color.White;
    }

    private void btn_LIS_SelCli_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIS_SelCli.Image = (Image) Resources.user_list;
      this.btn_LIS_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LIS_NewIS_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIS_NewIS.Image = (Image) Resources.add_white;
      this.btn_LIS_NewIS.ForeColor = Color.White;
    }

    private void btn_LIS_NewIS_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIS_NewIS.Image = (Image) Resources.add_grey;
      this.btn_LIS_NewIS.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LIS_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIS_Filter.Image = (Image) Resources.filter_white;
      this.btn_LIS_Filter.ForeColor = Color.White;
    }

    private void btn_LIS_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIS_Filter.Image = (Image) Resources.filter_grey;
      this.btn_LIS_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_LIS_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LIS_ClearFilter.ForeColor = Color.White;
    }

    private void btn_LIS_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LIS_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_LIS_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_LIS_CName_KeyDown(object sender, KeyEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Invoices_Send));
      this.btn_LIS_ClearFilter = new Button();
      this.dtp_LIS_From = new BunifuDatepicker();
      this.dgv_LInvSent = new AdvancedDataGridView();
      this.btn_LIS_NewIS = new Button();
      this.btn_LIS_Filter = new Button();
      this.dtp_LIS_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_LIS_SelCli = new Button();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.txt_LIS_CName = new BunifuMaterialTextbox();
      this.txt_LIS_CCode = new BunifuMaterialTextbox();
      this.btn_LIS_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_LIS_Prev = new Button();
      ((ISupportInitialize) this.dgv_LInvSent).BeginInit();
      this.SuspendLayout();
      this.btn_LIS_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_LIS_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIS_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIS_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_LIS_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIS_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LIS_ClearFilter.Location = new Point(553, 232);
      this.btn_LIS_ClearFilter.Name = "btn_LIS_ClearFilter";
      this.btn_LIS_ClearFilter.Size = new Size(114, 40);
      this.btn_LIS_ClearFilter.TabIndex = 86;
      this.btn_LIS_ClearFilter.Text = "Clear Filter";
      this.btn_LIS_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_LIS_ClearFilter.Visible = false;
      this.btn_LIS_ClearFilter.Click += new EventHandler(this.btn_LIS_ClearFilter_Click);
      this.btn_LIS_ClearFilter.MouseEnter += new EventHandler(this.btn_LIS_ClearFilter_MouseEnter);
      this.btn_LIS_ClearFilter.MouseLeave += new EventHandler(this.btn_LIS_ClearFilter_MouseLeave);
      this.dtp_LIS_From.BackColor = Color.LightGray;
      this.dtp_LIS_From.BorderRadius = 0;
      this.dtp_LIS_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LIS_From.Format = DateTimePickerFormat.Short;
      this.dtp_LIS_From.FormatCustom = (string) null;
      this.dtp_LIS_From.Location = new Point(70, 235);
      this.dtp_LIS_From.Name = "dtp_LIS_From";
      this.dtp_LIS_From.Size = new Size(208, 36);
      this.dtp_LIS_From.TabIndex = 71;
      this.dtp_LIS_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_LInvSent.AllowUserToAddRows = false;
      this.dgv_LInvSent.AllowUserToDeleteRows = false;
      this.dgv_LInvSent.AllowUserToResizeColumns = false;
      this.dgv_LInvSent.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_LInvSent.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_LInvSent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_LInvSent.AutoGenerateContextFilters = true;
      this.dgv_LInvSent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_LInvSent.BorderStyle = BorderStyle.None;
      this.dgv_LInvSent.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_LInvSent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_LInvSent.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_LInvSent.ColumnHeadersHeight = 25;
      this.dgv_LInvSent.DateWithTime = false;
      this.dgv_LInvSent.EnableHeadersVisualStyles = false;
      this.dgv_LInvSent.Location = new Point(0, 279);
      this.dgv_LInvSent.Name = "dgv_LInvSent";
      this.dgv_LInvSent.ReadOnly = true;
      this.dgv_LInvSent.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_LInvSent.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_LInvSent.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_LInvSent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_LInvSent.Size = new Size(963, 340);
      this.dgv_LInvSent.TabIndex = 85;
      this.dgv_LInvSent.TimeFilter = false;
      this.dgv_LInvSent.SortStringChanged += new EventHandler(this.dgv_LInvSent_SortStringChanged);
      this.dgv_LInvSent.FilterStringChanged += new EventHandler(this.dgv_LInvSent_FilterStringChanged);
      this.dgv_LInvSent.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_LInvSent_CellDoubleClick);
      this.btn_LIS_NewIS.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_LIS_NewIS.FlatAppearance.BorderSize = 0;
      this.btn_LIS_NewIS.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIS_NewIS.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIS_NewIS.FlatStyle = FlatStyle.Flat;
      this.btn_LIS_NewIS.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIS_NewIS.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LIS_NewIS.Image = (Image) componentResourceManager.GetObject("btn_LIS_NewIS.Image");
      this.btn_LIS_NewIS.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LIS_NewIS.Location = new Point(825, 232);
      this.btn_LIS_NewIS.Name = "btn_LIS_NewIS";
      this.btn_LIS_NewIS.Size = new Size(122, 40);
      this.btn_LIS_NewIS.TabIndex = 84;
      this.btn_LIS_NewIS.Text = "New Invoice";
      this.btn_LIS_NewIS.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LIS_NewIS.UseVisualStyleBackColor = true;
      this.btn_LIS_NewIS.Click += new EventHandler(this.btn_LIS_NewIS_Click);
      this.btn_LIS_NewIS.MouseEnter += new EventHandler(this.btn_LIS_NewIS_MouseEnter);
      this.btn_LIS_NewIS.MouseLeave += new EventHandler(this.btn_LIS_NewIS_MouseLeave);
      this.btn_LIS_Filter.FlatAppearance.BorderSize = 0;
      this.btn_LIS_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIS_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIS_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_LIS_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIS_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LIS_Filter.Image = (Image) componentResourceManager.GetObject("btn_LIS_Filter.Image");
      this.btn_LIS_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LIS_Filter.Location = new Point(553, 232);
      this.btn_LIS_Filter.Name = "btn_LIS_Filter";
      this.btn_LIS_Filter.Size = new Size(114, 40);
      this.btn_LIS_Filter.TabIndex = 83;
      this.btn_LIS_Filter.Text = "Filter";
      this.btn_LIS_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LIS_Filter.UseVisualStyleBackColor = true;
      this.btn_LIS_Filter.Click += new EventHandler(this.btn_LIS_Filter_Click);
      this.btn_LIS_Filter.MouseEnter += new EventHandler(this.btn_LIS_Filter_MouseEnter);
      this.btn_LIS_Filter.MouseLeave += new EventHandler(this.btn_LIS_Filter_MouseLeave);
      this.dtp_LIS_To.BackColor = Color.LightGray;
      this.dtp_LIS_To.BorderRadius = 0;
      this.dtp_LIS_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_LIS_To.Format = DateTimePickerFormat.Short;
      this.dtp_LIS_To.FormatCustom = (string) null;
      this.dtp_LIS_To.Location = new Point(324, 235);
      this.dtp_LIS_To.Name = "dtp_LIS_To";
      this.dtp_LIS_To.Size = new Size(208, 36);
      this.dtp_LIS_To.TabIndex = 82;
      this.dtp_LIS_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(284, 242);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 81;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(12, 242);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 80;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_LIS_SelCli.FlatAppearance.BorderSize = 0;
      this.btn_LIS_SelCli.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIS_SelCli.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIS_SelCli.FlatStyle = FlatStyle.Flat;
      this.btn_LIS_SelCli.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIS_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LIS_SelCli.Image = (Image) componentResourceManager.GetObject("btn_LIS_SelCli.Image");
      this.btn_LIS_SelCli.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LIS_SelCli.Location = new Point(518, 161);
      this.btn_LIS_SelCli.Name = "btn_LIS_SelCli";
      this.btn_LIS_SelCli.Size = new Size(114, 40);
      this.btn_LIS_SelCli.TabIndex = 79;
      this.btn_LIS_SelCli.Text = "Client List";
      this.btn_LIS_SelCli.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LIS_SelCli.UseVisualStyleBackColor = true;
      this.btn_LIS_SelCli.Click += new EventHandler(this.btn_LIS_SelCli_Click);
      this.btn_LIS_SelCli.MouseEnter += new EventHandler(this.btn_LIS_SelCli_MouseEnter);
      this.btn_LIS_SelCli.MouseLeave += new EventHandler(this.btn_LIS_SelCli_MouseLeave);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(17, 207);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(934, 35);
      this.bunifuSeparator2.TabIndex = 78;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.txt_LIS_CName.Cursor = Cursors.IBeam;
      this.txt_LIS_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LIS_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LIS_CName.HintForeColor = Color.Empty;
      this.txt_LIS_CName.HintText = "";
      this.txt_LIS_CName.isPassword = false;
      this.txt_LIS_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LIS_CName.LineIdleColor = Color.Gray;
      this.txt_LIS_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LIS_CName.LineThickness = 1;
      this.txt_LIS_CName.Location = new Point(254, 115);
      this.txt_LIS_CName.Margin = new Padding(4);
      this.txt_LIS_CName.Name = "txt_LIS_CName";
      this.txt_LIS_CName.Size = new Size(379, 33);
      this.txt_LIS_CName.TabIndex = 77;
      this.txt_LIS_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_LIS_CName.KeyDown += new KeyEventHandler(this.txt_LIS_CName_KeyDown);
      this.txt_LIS_CCode.Cursor = Cursors.IBeam;
      this.txt_LIS_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LIS_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LIS_CCode.HintForeColor = Color.Empty;
      this.txt_LIS_CCode.HintText = "";
      this.txt_LIS_CCode.isPassword = false;
      this.txt_LIS_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LIS_CCode.LineIdleColor = Color.Gray;
      this.txt_LIS_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LIS_CCode.LineThickness = 1;
      this.txt_LIS_CCode.Location = new Point(253, 53);
      this.txt_LIS_CCode.Margin = new Padding(4);
      this.txt_LIS_CCode.Name = "txt_LIS_CCode";
      this.txt_LIS_CCode.Size = new Size(379, 33);
      this.txt_LIS_CCode.TabIndex = 76;
      this.txt_LIS_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_LIS_CCode.KeyDown += new KeyEventHandler(this.txt_LIS_CCode_KeyDown);
      this.btn_LIS_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_LIS_Next.FlatAppearance.BorderSize = 0;
      this.btn_LIS_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIS_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIS_Next.FlatStyle = FlatStyle.Flat;
      this.btn_LIS_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIS_Next.ForeColor = Color.White;
      this.btn_LIS_Next.Image = (Image) componentResourceManager.GetObject("btn_LIS_Next.Image");
      this.btn_LIS_Next.Location = new Point(897, 21);
      this.btn_LIS_Next.Name = "btn_LIS_Next";
      this.btn_LIS_Next.Size = new Size(49, 149);
      this.btn_LIS_Next.TabIndex = 75;
      this.btn_LIS_Next.UseVisualStyleBackColor = true;
      this.btn_LIS_Next.Click += new EventHandler(this.btn_LIS_Next_Click);
      this.btn_LIS_Next.MouseEnter += new EventHandler(this.btn_LIS_Next_MouseEnter);
      this.btn_LIS_Next.MouseLeave += new EventHandler(this.btn_LIS_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point((int) sbyte.MaxValue, 117);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(120, 26);
      this.bunifuCustomLabel3.TabIndex = 74;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(132, 56);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(114, 26);
      this.bunifuCustomLabel4.TabIndex = 73;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.btn_LIS_Prev.Enabled = false;
      this.btn_LIS_Prev.FlatAppearance.BorderSize = 0;
      this.btn_LIS_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LIS_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LIS_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_LIS_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LIS_Prev.ForeColor = Color.White;
      this.btn_LIS_Prev.Image = (Image) componentResourceManager.GetObject("btn_LIS_Prev.Image");
      this.btn_LIS_Prev.Location = new Point(17, 21);
      this.btn_LIS_Prev.Name = "btn_LIS_Prev";
      this.btn_LIS_Prev.Size = new Size(49, 149);
      this.btn_LIS_Prev.TabIndex = 72;
      this.btn_LIS_Prev.UseVisualStyleBackColor = true;
      this.btn_LIS_Prev.Click += new EventHandler(this.btn_LIS_Prev_Click);
      this.btn_LIS_Prev.MouseEnter += new EventHandler(this.btn_LIS_Prev_MouseEnter);
      this.btn_LIS_Prev.MouseLeave += new EventHandler(this.btn_LIS_Prev_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_LIS_ClearFilter);
      this.Controls.Add((Control) this.dtp_LIS_From);
      this.Controls.Add((Control) this.dgv_LInvSent);
      this.Controls.Add((Control) this.btn_LIS_NewIS);
      this.Controls.Add((Control) this.btn_LIS_Filter);
      this.Controls.Add((Control) this.dtp_LIS_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.btn_LIS_SelCli);
      this.Controls.Add((Control) this.bunifuSeparator2);
      this.Controls.Add((Control) this.txt_LIS_CName);
      this.Controls.Add((Control) this.txt_LIS_CCode);
      this.Controls.Add((Control) this.btn_LIS_Next);
      this.Controls.Add((Control) this.bunifuCustomLabel3);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.btn_LIS_Prev);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(850, 510);
      this.Name = nameof (Invoices_Send);
      this.Text = "Invoices Send";
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Invoices_Send_Load);
      ((ISupportInitialize) this.dgv_LInvSent).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
