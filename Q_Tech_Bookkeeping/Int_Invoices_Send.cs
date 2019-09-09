// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Int_Invoices_Send
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using ADGV;
using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Int_Invoices_Send : Form
  {
    private int CUR_CLIENT = 0;
    private BindingSource bs = new BindingSource();
    private bool isFiltered = false;
    private object send = (object) null;
    private IContainer components = (IContainer) null;
    private int NUM_OF_CLIENTS;
    private int SELECTED_INVSEND;
    private string CNAME;
    private string NEW_INVOICE;
    private DataTable clientsDT;
    private DataTable dt;
    private Button btn_IIS_ClearFilter;
    private BunifuDatepicker dtp_IIS_From;
    private AdvancedDataGridView dgv_IInvSent;
    private Button btn_IIS_NewIS;
    private Button btn_IIS_Filter;
    private BunifuDatepicker dtp_IIS_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_IIS_SelCli;
    private BunifuSeparator bunifuSeparator2;
    private BunifuMaterialTextbox txt_IIS_CName;
    private BunifuMaterialTextbox txt_IIS_CCode;
    private Button btn_IIS_Next;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_IIS_Prev;

    public Int_Invoices_Send()
    {
      this.InitializeComponent();
    }

    private void Invoices_Send_Load(object sender, EventArgs e)
    {
      this.dgv_IInvSent.DataSource = (object) this.bs;
      this.loadClients();
      this.loadInvSend();
      this.dgv_IInvSent.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_IInvSent.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
      this.dgv_IInvSent.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_IInvSent.Columns[5].DefaultCellStyle.Format = "c";
      this.dgv_IInvSent.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
      this.dgv_IInvSent.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
        if (!this.btn_IIS_SelCli.Enabled)
          this.btn_IIS_SelCli.Enabled = true;
        if (!this.dgv_IInvSent.Enabled)
          this.dgv_IInvSent.Enabled = true;
        if (!this.btn_IIS_NewIS.Enabled)
          this.btn_IIS_NewIS.Enabled = true;
        this.NUM_OF_CLIENTS = this.clientsDT.Rows.Count - 1;
        this.txt_IIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IIS_CName.Text = this.CNAME;
      }
      else
      {
        this.btn_IIS_SelCli.Enabled = false;
        this.btn_IIS_Next.Enabled = false;
        this.btn_IIS_Prev.Enabled = false;
        this.dgv_IInvSent.Enabled = false;
        this.btn_IIS_NewIS.Enabled = false;
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
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_IIS_Next_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT + 1 < this.NUM_OF_CLIENTS)
      {
        ++this.CUR_CLIENT;
        this.txt_IIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      else if (this.CUR_CLIENT + 1 == this.NUM_OF_CLIENTS)
      {
        this.btn_IIS_Next.Enabled = false;
        ++this.CUR_CLIENT;
        this.txt_IIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_IIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      if (this.CUR_CLIENT == 0 || this.btn_IIS_Prev.Enabled)
        return;
      this.btn_IIS_Prev.Enabled = true;
    }

    private void btn_IIS_Prev_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT - 1 > 0)
      {
        --this.CUR_CLIENT;
        this.txt_IIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      else if (this.CUR_CLIENT - 1 == 0)
      {
        this.btn_IIS_Prev.Enabled = false;
        --this.CUR_CLIENT;
        this.txt_IIS_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_IIS_CName.Text = this.CNAME;
        this.loadInvSend();
      }
      if (this.CUR_CLIENT == this.NUM_OF_CLIENTS || this.btn_IIS_Next.Enabled)
        return;
      this.btn_IIS_Next.Enabled = true;
    }

    private void btn_IIS_SelCli_Click(object sender, EventArgs e)
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
      this.loadInvSend();
      if (this.CUR_CLIENT != 0 && !this.btn_IIS_Prev.Enabled)
        this.btn_IIS_Prev.Enabled = true;
      if (this.CUR_CLIENT == 0 && this.btn_IIS_Prev.Enabled)
        this.btn_IIS_Prev.Enabled = false;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS && !this.btn_IIS_Next.Enabled)
        this.btn_IIS_Next.Enabled = true;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS || !this.btn_IIS_Next.Enabled)
        return;
      this.btn_IIS_Next.Enabled = false;
    }

    private void btn_IIS_NewIS_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      using (Inv_Send_Add invSendAdd = new Inv_Send_Add())
      {
        int num = (int) invSendAdd.ShowDialog((IWin32Window) this);
      }
      this.loadInvSend();
      if (this.send == null)
      {
        foreach (DataGridViewRow row in (IEnumerable) this.dgv_IInvSent.Rows)
        {
          if (row.Cells[1].Value.ToString().Equals(this.NEW_INVOICE))
          {
            this.SELECTED_INVSEND = row.Index;
            break;
          }
        }
        using (Inv_Send_Edit_Del invSendEditDel = new Inv_Send_Edit_Del())
        {
          int num = (int) invSendEditDel.ShowDialog((IWin32Window) this);
        }
        this.loadInvSend();
      }
      else
        this.send = (object) null;
    }

    public string getCCode()
    {
      return this.txt_IIS_CCode.Text;
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

    public void setSender(object send)
    {
      this.send = send;
    }

    private void dgv_IInvSent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.isFiltered)
        this.removeFilter();
      this.SELECTED_INVSEND = e.RowIndex;
      using (Inv_Send_Edit_Del invSendEditDel = new Inv_Send_Edit_Del())
      {
        int num = (int) invSendEditDel.ShowDialog((IWin32Window) this);
      }
      this.loadInvSend();
    }

    private void dgv_IInvSent_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_IInvSent.FilterString;
    }

    private void dgv_IInvSent_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_IInvSent.SortString;
    }

    private void btn_IIS_Filter_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Send WHERE Client LIKE '" + this.CNAME + "%' AND Date BETWEEN '" + (object) this.dtp_IIS_From.Value + "' AND '" + (object) this.dtp_IIS_To.Value + "' OR Client LIKE '" + this.CNAME + "%' AND Date_Paid BETWEEN '" + (object) this.dtp_IIS_From.Value + "' AND '" + (object) this.dtp_IIS_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_IIS_Filter.Visible = false;
      this.btn_IIS_ClearFilter.Visible = true;
    }

    private void btn_IIS_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadInvSend();
      this.btn_IIS_Filter.Visible = true;
      this.btn_IIS_ClearFilter.Visible = false;
    }

    private void btn_IIS_Prev_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IIS_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_IIS_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IIS_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_IIS_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IIS_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_IIS_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IIS_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_IIS_SelCli_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IIS_SelCli.Image = (Image) Resources.client_list_white;
      this.btn_IIS_SelCli.ForeColor = Color.White;
    }

    private void btn_IIS_SelCli_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IIS_SelCli.Image = (Image) Resources.user_list;
      this.btn_IIS_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IIS_NewIS_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IIS_NewIS.Image = (Image) Resources.add_white;
      this.btn_IIS_NewIS.ForeColor = Color.White;
    }

    private void btn_IIS_NewIS_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IIS_NewIS.Image = (Image) Resources.add_grey;
      this.btn_IIS_NewIS.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IIS_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IIS_Filter.Image = (Image) Resources.filter_white;
      this.btn_IIS_Filter.ForeColor = Color.White;
    }

    private void btn_IIS_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IIS_Filter.Image = (Image) Resources.filter_grey;
      this.btn_IIS_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IIS_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IIS_ClearFilter.ForeColor = Color.White;
    }

    private void btn_IIS_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IIS_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_IIS_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_IIS_CName_KeyDown(object sender, KeyEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Int_Invoices_Send));
      this.btn_IIS_ClearFilter = new Button();
      this.dtp_IIS_From = new BunifuDatepicker();
      this.dgv_IInvSent = new AdvancedDataGridView();
      this.btn_IIS_NewIS = new Button();
      this.btn_IIS_Filter = new Button();
      this.dtp_IIS_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_IIS_SelCli = new Button();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.txt_IIS_CName = new BunifuMaterialTextbox();
      this.txt_IIS_CCode = new BunifuMaterialTextbox();
      this.btn_IIS_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_IIS_Prev = new Button();
      ((ISupportInitialize) this.dgv_IInvSent).BeginInit();
      this.SuspendLayout();
      this.btn_IIS_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_IIS_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IIS_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IIS_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_IIS_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IIS_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IIS_ClearFilter.Location = new Point(553, 232);
      this.btn_IIS_ClearFilter.Name = "btn_IIS_ClearFilter";
      this.btn_IIS_ClearFilter.Size = new Size(114, 40);
      this.btn_IIS_ClearFilter.TabIndex = 102;
      this.btn_IIS_ClearFilter.Text = "Clear Filter";
      this.btn_IIS_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_IIS_ClearFilter.Visible = false;
      this.btn_IIS_ClearFilter.Click += new EventHandler(this.btn_IIS_ClearFilter_Click);
      this.btn_IIS_ClearFilter.MouseEnter += new EventHandler(this.btn_IIS_ClearFilter_MouseEnter);
      this.btn_IIS_ClearFilter.MouseLeave += new EventHandler(this.btn_IIS_ClearFilter_MouseLeave);
      this.dtp_IIS_From.BackColor = Color.LightGray;
      this.dtp_IIS_From.BorderRadius = 0;
      this.dtp_IIS_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_IIS_From.Format = DateTimePickerFormat.Short;
      this.dtp_IIS_From.FormatCustom = (string) null;
      this.dtp_IIS_From.Location = new Point(70, 235);
      this.dtp_IIS_From.Name = "dtp_IIS_From";
      this.dtp_IIS_From.Size = new Size(208, 36);
      this.dtp_IIS_From.TabIndex = 87;
      this.dtp_IIS_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_IInvSent.AllowUserToAddRows = false;
      this.dgv_IInvSent.AllowUserToDeleteRows = false;
      this.dgv_IInvSent.AllowUserToResizeColumns = false;
      this.dgv_IInvSent.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_IInvSent.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_IInvSent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_IInvSent.AutoGenerateContextFilters = true;
      this.dgv_IInvSent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_IInvSent.BorderStyle = BorderStyle.None;
      this.dgv_IInvSent.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_IInvSent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_IInvSent.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_IInvSent.ColumnHeadersHeight = 25;
      this.dgv_IInvSent.DateWithTime = false;
      this.dgv_IInvSent.EnableHeadersVisualStyles = false;
      this.dgv_IInvSent.Location = new Point(0, 279);
      this.dgv_IInvSent.Name = "dgv_IInvSent";
      this.dgv_IInvSent.ReadOnly = true;
      this.dgv_IInvSent.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_IInvSent.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_IInvSent.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_IInvSent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_IInvSent.Size = new Size(963, 340);
      this.dgv_IInvSent.TabIndex = 101;
      this.dgv_IInvSent.TimeFilter = false;
      this.dgv_IInvSent.SortStringChanged += new EventHandler(this.dgv_IInvSent_SortStringChanged);
      this.dgv_IInvSent.FilterStringChanged += new EventHandler(this.dgv_IInvSent_FilterStringChanged);
      this.dgv_IInvSent.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_IInvSent_CellDoubleClick);
      this.btn_IIS_NewIS.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_IIS_NewIS.FlatAppearance.BorderSize = 0;
      this.btn_IIS_NewIS.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IIS_NewIS.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IIS_NewIS.FlatStyle = FlatStyle.Flat;
      this.btn_IIS_NewIS.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IIS_NewIS.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IIS_NewIS.Image = (Image) componentResourceManager.GetObject("btn_IIS_NewIS.Image");
      this.btn_IIS_NewIS.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IIS_NewIS.Location = new Point(825, 232);
      this.btn_IIS_NewIS.Name = "btn_IIS_NewIS";
      this.btn_IIS_NewIS.Size = new Size(122, 40);
      this.btn_IIS_NewIS.TabIndex = 100;
      this.btn_IIS_NewIS.Text = "New Invoice";
      this.btn_IIS_NewIS.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IIS_NewIS.UseVisualStyleBackColor = true;
      this.btn_IIS_NewIS.Click += new EventHandler(this.btn_IIS_NewIS_Click);
      this.btn_IIS_NewIS.MouseEnter += new EventHandler(this.btn_IIS_NewIS_MouseEnter);
      this.btn_IIS_NewIS.MouseLeave += new EventHandler(this.btn_IIS_NewIS_MouseLeave);
      this.btn_IIS_Filter.FlatAppearance.BorderSize = 0;
      this.btn_IIS_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IIS_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IIS_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_IIS_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IIS_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IIS_Filter.Image = (Image) componentResourceManager.GetObject("btn_IIS_Filter.Image");
      this.btn_IIS_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IIS_Filter.Location = new Point(553, 232);
      this.btn_IIS_Filter.Name = "btn_IIS_Filter";
      this.btn_IIS_Filter.Size = new Size(114, 40);
      this.btn_IIS_Filter.TabIndex = 99;
      this.btn_IIS_Filter.Text = "Filter";
      this.btn_IIS_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IIS_Filter.UseVisualStyleBackColor = true;
      this.btn_IIS_Filter.Click += new EventHandler(this.btn_IIS_Filter_Click);
      this.btn_IIS_Filter.MouseEnter += new EventHandler(this.btn_IIS_Filter_MouseEnter);
      this.btn_IIS_Filter.MouseLeave += new EventHandler(this.btn_IIS_Filter_MouseLeave);
      this.dtp_IIS_To.BackColor = Color.LightGray;
      this.dtp_IIS_To.BorderRadius = 0;
      this.dtp_IIS_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_IIS_To.Format = DateTimePickerFormat.Short;
      this.dtp_IIS_To.FormatCustom = (string) null;
      this.dtp_IIS_To.Location = new Point(324, 235);
      this.dtp_IIS_To.Name = "dtp_IIS_To";
      this.dtp_IIS_To.Size = new Size(208, 36);
      this.dtp_IIS_To.TabIndex = 98;
      this.dtp_IIS_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(284, 242);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 97;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(12, 242);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 96;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_IIS_SelCli.FlatAppearance.BorderSize = 0;
      this.btn_IIS_SelCli.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IIS_SelCli.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IIS_SelCli.FlatStyle = FlatStyle.Flat;
      this.btn_IIS_SelCli.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IIS_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IIS_SelCli.Image = (Image) componentResourceManager.GetObject("btn_IIS_SelCli.Image");
      this.btn_IIS_SelCli.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IIS_SelCli.Location = new Point(518, 161);
      this.btn_IIS_SelCli.Name = "btn_IIS_SelCli";
      this.btn_IIS_SelCli.Size = new Size(114, 40);
      this.btn_IIS_SelCli.TabIndex = 95;
      this.btn_IIS_SelCli.Text = "Client List";
      this.btn_IIS_SelCli.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IIS_SelCli.UseVisualStyleBackColor = true;
      this.btn_IIS_SelCli.Click += new EventHandler(this.btn_IIS_SelCli_Click);
      this.btn_IIS_SelCli.MouseEnter += new EventHandler(this.btn_IIS_SelCli_MouseEnter);
      this.btn_IIS_SelCli.MouseLeave += new EventHandler(this.btn_IIS_SelCli_MouseLeave);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(17, 207);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(934, 35);
      this.bunifuSeparator2.TabIndex = 94;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.txt_IIS_CName.Cursor = Cursors.IBeam;
      this.txt_IIS_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IIS_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IIS_CName.HintForeColor = Color.Empty;
      this.txt_IIS_CName.HintText = "";
      this.txt_IIS_CName.isPassword = false;
      this.txt_IIS_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IIS_CName.LineIdleColor = Color.Gray;
      this.txt_IIS_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IIS_CName.LineThickness = 1;
      this.txt_IIS_CName.Location = new Point(254, 115);
      this.txt_IIS_CName.Margin = new Padding(4);
      this.txt_IIS_CName.Name = "txt_IIS_CName";
      this.txt_IIS_CName.Size = new Size(379, 33);
      this.txt_IIS_CName.TabIndex = 93;
      this.txt_IIS_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_IIS_CName.KeyDown += new KeyEventHandler(this.txt_IIS_CName_KeyDown);
      this.txt_IIS_CCode.Cursor = Cursors.IBeam;
      this.txt_IIS_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IIS_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IIS_CCode.HintForeColor = Color.Empty;
      this.txt_IIS_CCode.HintText = "";
      this.txt_IIS_CCode.isPassword = false;
      this.txt_IIS_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IIS_CCode.LineIdleColor = Color.Gray;
      this.txt_IIS_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IIS_CCode.LineThickness = 1;
      this.txt_IIS_CCode.Location = new Point(253, 53);
      this.txt_IIS_CCode.Margin = new Padding(4);
      this.txt_IIS_CCode.Name = "txt_IIS_CCode";
      this.txt_IIS_CCode.Size = new Size(379, 33);
      this.txt_IIS_CCode.TabIndex = 92;
      this.txt_IIS_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_IIS_CCode.KeyDown += new KeyEventHandler(this.txt_IIS_CCode_KeyDown);
      this.btn_IIS_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_IIS_Next.FlatAppearance.BorderSize = 0;
      this.btn_IIS_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IIS_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IIS_Next.FlatStyle = FlatStyle.Flat;
      this.btn_IIS_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IIS_Next.ForeColor = Color.White;
      this.btn_IIS_Next.Image = (Image) componentResourceManager.GetObject("btn_IIS_Next.Image");
      this.btn_IIS_Next.Location = new Point(897, 21);
      this.btn_IIS_Next.Name = "btn_IIS_Next";
      this.btn_IIS_Next.Size = new Size(49, 149);
      this.btn_IIS_Next.TabIndex = 91;
      this.btn_IIS_Next.UseVisualStyleBackColor = true;
      this.btn_IIS_Next.Click += new EventHandler(this.btn_IIS_Next_Click);
      this.btn_IIS_Next.MouseEnter += new EventHandler(this.btn_IIS_Next_MouseEnter);
      this.btn_IIS_Next.MouseLeave += new EventHandler(this.btn_IIS_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point((int) sbyte.MaxValue, 117);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(120, 26);
      this.bunifuCustomLabel3.TabIndex = 90;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(132, 56);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(114, 26);
      this.bunifuCustomLabel4.TabIndex = 89;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.btn_IIS_Prev.Enabled = false;
      this.btn_IIS_Prev.FlatAppearance.BorderSize = 0;
      this.btn_IIS_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IIS_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IIS_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_IIS_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IIS_Prev.ForeColor = Color.White;
      this.btn_IIS_Prev.Image = (Image) componentResourceManager.GetObject("btn_IIS_Prev.Image");
      this.btn_IIS_Prev.Location = new Point(17, 21);
      this.btn_IIS_Prev.Name = "btn_IIS_Prev";
      this.btn_IIS_Prev.Size = new Size(49, 149);
      this.btn_IIS_Prev.TabIndex = 88;
      this.btn_IIS_Prev.UseVisualStyleBackColor = true;
      this.btn_IIS_Prev.Click += new EventHandler(this.btn_IIS_Prev_Click);
      this.btn_IIS_Prev.MouseEnter += new EventHandler(this.btn_IIS_Prev_MouseEnter);
      this.btn_IIS_Prev.MouseLeave += new EventHandler(this.btn_IIS_Prev_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_IIS_ClearFilter);
      this.Controls.Add((Control) this.dtp_IIS_From);
      this.Controls.Add((Control) this.dgv_IInvSent);
      this.Controls.Add((Control) this.btn_IIS_NewIS);
      this.Controls.Add((Control) this.btn_IIS_Filter);
      this.Controls.Add((Control) this.dtp_IIS_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.btn_IIS_SelCli);
      this.Controls.Add((Control) this.bunifuSeparator2);
      this.Controls.Add((Control) this.txt_IIS_CName);
      this.Controls.Add((Control) this.txt_IIS_CCode);
      this.Controls.Add((Control) this.btn_IIS_Next);
      this.Controls.Add((Control) this.bunifuCustomLabel3);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.btn_IIS_Prev);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(963, 618);
      this.Name = nameof (Int_Invoices_Send);
      this.Text = "International Invoices Send";
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Invoices_Send_Load);
      ((ISupportInitialize) this.dgv_IInvSent).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
