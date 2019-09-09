// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Int_Quotes
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
  public class Int_Quotes : Form
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
    private Button btn_IQ_ClearFilter;
    private BunifuDatepicker dtp_IQ_From;
    private AdvancedDataGridView dgv_IQuotes;
    private Button btn_IQ_NewQuote;
    private Button btn_IQ_Filter;
    private BunifuDatepicker dtp_IQ_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_IQ_SelCli;
    private BunifuSeparator bunifuSeparator2;
    private BunifuMaterialTextbox txt_IQ_CName;
    private BunifuMaterialTextbox txt_IQ_CCode;
    private Button btn_IQ_Next;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_IQ_Prev;

    public Int_Quotes()
    {
      this.InitializeComponent();
    }

    private void Quotes_Load(object sender, EventArgs e)
    {
      this.clientsDT = new DataTable();
      this.dgv_IQuotes.DataSource = (object) this.bs;
      this.loadClients();
      this.loadQuotes();
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
        if (!this.btn_IQ_SelCli.Enabled)
          this.btn_IQ_SelCli.Enabled = true;
        if (!this.dgv_IQuotes.Enabled)
          this.dgv_IQuotes.Enabled = true;
        if (!this.btn_IQ_NewQuote.Enabled)
          this.btn_IQ_NewQuote.Enabled = true;
        this.NUM_OF_CLIENTS = this.clientsDT.Rows.Count - 1;
        this.txt_IQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IQ_CName.Text = this.CNAME;
      }
      else
      {
        this.btn_IQ_SelCli.Enabled = false;
        this.dgv_IQuotes.Enabled = false;
        this.btn_IQ_NewQuote.Enabled = false;
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

    private void btn_IQ_Next_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT + 1 < this.NUM_OF_CLIENTS)
      {
        ++this.CUR_CLIENT;
        this.txt_IQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      else if (this.CUR_CLIENT + 1 == this.NUM_OF_CLIENTS)
      {
        this.btn_IQ_Next.Enabled = false;
        ++this.CUR_CLIENT;
        this.txt_IQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_IQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      if (this.CUR_CLIENT == 0 || this.btn_IQ_Prev.Enabled)
        return;
      this.btn_IQ_Prev.Enabled = true;
    }

    private void btn_IQ_Prev_Click(object sender, EventArgs e)
    {
      if (this.CUR_CLIENT - 1 > 0)
      {
        --this.CUR_CLIENT;
        this.txt_IQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString().Trim();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString().Trim();
        this.txt_IQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      else if (this.CUR_CLIENT - 1 == 0)
      {
        this.btn_IQ_Prev.Enabled = false;
        --this.CUR_CLIENT;
        this.txt_IQ_CCode.Text = this.clientsDT.Rows[this.CUR_CLIENT]["Code"].ToString();
        this.CNAME = this.clientsDT.Rows[this.CUR_CLIENT]["Name"].ToString();
        this.txt_IQ_CName.Text = this.CNAME;
        this.loadQuotes();
      }
      if (this.CUR_CLIENT == this.NUM_OF_CLIENTS || this.btn_IQ_Next.Enabled)
        return;
      this.btn_IQ_Next.Enabled = true;
    }

    private void btn_IQ_SelCli_Click(object sender, EventArgs e)
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
      if (this.CUR_CLIENT != 0 && !this.btn_IQ_Prev.Enabled)
        this.btn_IQ_Prev.Enabled = true;
      if (this.CUR_CLIENT == 0 && this.btn_IQ_Prev.Enabled)
        this.btn_IQ_Prev.Enabled = false;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS && !this.btn_IQ_Next.Enabled)
        this.btn_IQ_Next.Enabled = true;
      if (this.CUR_CLIENT != this.NUM_OF_CLIENTS || this.btn_IQ_Next.Enabled)
        return;
      this.btn_IQ_Next.Enabled = false;
    }

    private void btn_IQ_NewQuote_Click(object sender, EventArgs e)
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
      return this.txt_IQ_CCode.Text;
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

    private void dgv_IQuotes_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_IQuotes.FilterString;
    }

    private void dgv_IQuotes_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_IQuotes.SortString;
    }

    private void btn_IQ_Filter_Click(object sender, EventArgs e)
    {
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Quotes_Send WHERE Client = '" + this.CNAME + "' AND Date_Send BETWEEN '" + (object) this.dtp_IQ_From.Value + "' AND '" + (object) this.dtp_IQ_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
      this.btn_IQ_Filter.Visible = false;
      this.btn_IQ_ClearFilter.Visible = true;
    }

    private void btn_IQ_ClearFilter_Click(object sender, EventArgs e)
    {
      this.removeFilter();
    }

    private void removeFilter()
    {
      this.loadQuotes();
      this.btn_IQ_Filter.Visible = true;
      this.btn_IQ_ClearFilter.Visible = false;
    }

    private void dgv_IQuotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

    private void btn_IQ_Prev_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IQ_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_IQ_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IQ_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_IQ_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IQ_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_IQ_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IQ_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_IQ_SelCli_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IQ_SelCli.Image = (Image) Resources.client_list_white;
      this.btn_IQ_SelCli.ForeColor = Color.White;
    }

    private void btn_IQ_SelCli_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IQ_SelCli.Image = (Image) Resources.user_list;
      this.btn_IQ_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IQ_NewQuote_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IQ_NewQuote.Image = (Image) Resources.add_white;
      this.btn_IQ_NewQuote.ForeColor = Color.White;
    }

    private void btn_IQ_NewQuote_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IQ_NewQuote.Image = (Image) Resources.add_grey;
      this.btn_IQ_NewQuote.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IQ_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IQ_Filter.Image = (Image) Resources.filter_white;
      this.btn_IQ_Filter.ForeColor = Color.White;
    }

    private void btn_IQ_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IQ_Filter.Image = (Image) Resources.filter_grey;
      this.btn_IQ_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_IQ_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IQ_ClearFilter.ForeColor = Color.White;
    }

    private void btn_IO_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IQ_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void txt_IQ_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = true;
    }

    private void txt_IQ_CName_KeyDown(object sender, KeyEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Int_Quotes));
      this.btn_IQ_ClearFilter = new Button();
      this.dtp_IQ_From = new BunifuDatepicker();
      this.dgv_IQuotes = new AdvancedDataGridView();
      this.btn_IQ_NewQuote = new Button();
      this.btn_IQ_Filter = new Button();
      this.dtp_IQ_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_IQ_SelCli = new Button();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.txt_IQ_CName = new BunifuMaterialTextbox();
      this.txt_IQ_CCode = new BunifuMaterialTextbox();
      this.btn_IQ_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_IQ_Prev = new Button();
      ((ISupportInitialize) this.dgv_IQuotes).BeginInit();
      this.SuspendLayout();
      this.btn_IQ_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_IQ_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IQ_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IQ_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_IQ_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IQ_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IQ_ClearFilter.Location = new Point(554, 233);
      this.btn_IQ_ClearFilter.Name = "btn_IQ_ClearFilter";
      this.btn_IQ_ClearFilter.Size = new Size(114, 40);
      this.btn_IQ_ClearFilter.TabIndex = 86;
      this.btn_IQ_ClearFilter.Text = "Clear Filter";
      this.btn_IQ_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_IQ_ClearFilter.Visible = false;
      this.btn_IQ_ClearFilter.Click += new EventHandler(this.btn_IQ_ClearFilter_Click);
      this.btn_IQ_ClearFilter.MouseEnter += new EventHandler(this.btn_IQ_ClearFilter_MouseEnter);
      this.btn_IQ_ClearFilter.MouseLeave += new EventHandler(this.btn_IO_ClearFilter_MouseLeave);
      this.dtp_IQ_From.BackColor = Color.LightGray;
      this.dtp_IQ_From.BorderRadius = 0;
      this.dtp_IQ_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_IQ_From.Format = DateTimePickerFormat.Short;
      this.dtp_IQ_From.FormatCustom = (string) null;
      this.dtp_IQ_From.Location = new Point(71, 236);
      this.dtp_IQ_From.Name = "dtp_IQ_From";
      this.dtp_IQ_From.Size = new Size(208, 36);
      this.dtp_IQ_From.TabIndex = 71;
      this.dtp_IQ_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.dgv_IQuotes.AllowUserToAddRows = false;
      this.dgv_IQuotes.AllowUserToDeleteRows = false;
      this.dgv_IQuotes.AllowUserToResizeColumns = false;
      this.dgv_IQuotes.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_IQuotes.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_IQuotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_IQuotes.AutoGenerateContextFilters = true;
      this.dgv_IQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_IQuotes.BorderStyle = BorderStyle.None;
      this.dgv_IQuotes.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_IQuotes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_IQuotes.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_IQuotes.ColumnHeadersHeight = 25;
      this.dgv_IQuotes.DateWithTime = false;
      this.dgv_IQuotes.EnableHeadersVisualStyles = false;
      this.dgv_IQuotes.Location = new Point(0, 279);
      this.dgv_IQuotes.Name = "dgv_IQuotes";
      this.dgv_IQuotes.ReadOnly = true;
      this.dgv_IQuotes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_IQuotes.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_IQuotes.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_IQuotes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_IQuotes.Size = new Size(963, 340);
      this.dgv_IQuotes.TabIndex = 85;
      this.dgv_IQuotes.TimeFilter = false;
      this.dgv_IQuotes.SortStringChanged += new EventHandler(this.dgv_IQuotes_SortStringChanged);
      this.dgv_IQuotes.FilterStringChanged += new EventHandler(this.dgv_IQuotes_FilterStringChanged);
      this.dgv_IQuotes.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_IQuotes_CellDoubleClick);
      this.btn_IQ_NewQuote.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_IQ_NewQuote.FlatAppearance.BorderSize = 0;
      this.btn_IQ_NewQuote.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IQ_NewQuote.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IQ_NewQuote.FlatStyle = FlatStyle.Flat;
      this.btn_IQ_NewQuote.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IQ_NewQuote.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IQ_NewQuote.Image = (Image) componentResourceManager.GetObject("btn_IQ_NewQuote.Image");
      this.btn_IQ_NewQuote.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IQ_NewQuote.Location = new Point(827, 233);
      this.btn_IQ_NewQuote.Name = "btn_IQ_NewQuote";
      this.btn_IQ_NewQuote.Size = new Size(122, 40);
      this.btn_IQ_NewQuote.TabIndex = 84;
      this.btn_IQ_NewQuote.Text = "New Quote";
      this.btn_IQ_NewQuote.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IQ_NewQuote.UseVisualStyleBackColor = true;
      this.btn_IQ_NewQuote.Click += new EventHandler(this.btn_IQ_NewQuote_Click);
      this.btn_IQ_NewQuote.MouseEnter += new EventHandler(this.btn_IQ_NewQuote_MouseEnter);
      this.btn_IQ_NewQuote.MouseLeave += new EventHandler(this.btn_IQ_NewQuote_MouseLeave);
      this.btn_IQ_Filter.FlatAppearance.BorderSize = 0;
      this.btn_IQ_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IQ_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IQ_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_IQ_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IQ_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IQ_Filter.Image = (Image) componentResourceManager.GetObject("btn_IQ_Filter.Image");
      this.btn_IQ_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IQ_Filter.Location = new Point(554, 233);
      this.btn_IQ_Filter.Name = "btn_IQ_Filter";
      this.btn_IQ_Filter.Size = new Size(114, 40);
      this.btn_IQ_Filter.TabIndex = 83;
      this.btn_IQ_Filter.Text = "Filter";
      this.btn_IQ_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IQ_Filter.UseVisualStyleBackColor = true;
      this.btn_IQ_Filter.Click += new EventHandler(this.btn_IQ_Filter_Click);
      this.btn_IQ_Filter.MouseEnter += new EventHandler(this.btn_IQ_Filter_MouseEnter);
      this.btn_IQ_Filter.MouseLeave += new EventHandler(this.btn_IQ_Filter_MouseLeave);
      this.dtp_IQ_To.BackColor = Color.LightGray;
      this.dtp_IQ_To.BorderRadius = 0;
      this.dtp_IQ_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_IQ_To.Format = DateTimePickerFormat.Short;
      this.dtp_IQ_To.FormatCustom = (string) null;
      this.dtp_IQ_To.Location = new Point(325, 236);
      this.dtp_IQ_To.Name = "dtp_IQ_To";
      this.dtp_IQ_To.Size = new Size(208, 36);
      this.dtp_IQ_To.TabIndex = 82;
      this.dtp_IQ_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(285, 243);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 81;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(13, 243);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 80;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_IQ_SelCli.FlatAppearance.BorderSize = 0;
      this.btn_IQ_SelCli.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IQ_SelCli.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IQ_SelCli.FlatStyle = FlatStyle.Flat;
      this.btn_IQ_SelCli.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IQ_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IQ_SelCli.Image = (Image) componentResourceManager.GetObject("btn_IQ_SelCli.Image");
      this.btn_IQ_SelCli.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IQ_SelCli.Location = new Point(519, 162);
      this.btn_IQ_SelCli.Name = "btn_IQ_SelCli";
      this.btn_IQ_SelCli.Size = new Size(114, 40);
      this.btn_IQ_SelCli.TabIndex = 79;
      this.btn_IQ_SelCli.Text = "Client List";
      this.btn_IQ_SelCli.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IQ_SelCli.UseVisualStyleBackColor = true;
      this.btn_IQ_SelCli.Click += new EventHandler(this.btn_IQ_SelCli_Click);
      this.btn_IQ_SelCli.MouseEnter += new EventHandler(this.btn_IQ_SelCli_MouseEnter);
      this.btn_IQ_SelCli.MouseLeave += new EventHandler(this.btn_IQ_SelCli_MouseLeave);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(18, 208);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(934, 35);
      this.bunifuSeparator2.TabIndex = 78;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.txt_IQ_CName.Cursor = Cursors.IBeam;
      this.txt_IQ_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IQ_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IQ_CName.HintForeColor = Color.Empty;
      this.txt_IQ_CName.HintText = "";
      this.txt_IQ_CName.isPassword = false;
      this.txt_IQ_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IQ_CName.LineIdleColor = Color.Gray;
      this.txt_IQ_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IQ_CName.LineThickness = 1;
      this.txt_IQ_CName.Location = new Point((int) byte.MaxValue, 116);
      this.txt_IQ_CName.Margin = new Padding(4);
      this.txt_IQ_CName.Name = "txt_IQ_CName";
      this.txt_IQ_CName.Size = new Size(379, 33);
      this.txt_IQ_CName.TabIndex = 77;
      this.txt_IQ_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_IQ_CName.KeyDown += new KeyEventHandler(this.txt_IQ_CName_KeyDown);
      this.txt_IQ_CCode.Cursor = Cursors.IBeam;
      this.txt_IQ_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IQ_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IQ_CCode.HintForeColor = Color.Empty;
      this.txt_IQ_CCode.HintText = "";
      this.txt_IQ_CCode.isPassword = false;
      this.txt_IQ_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IQ_CCode.LineIdleColor = Color.Gray;
      this.txt_IQ_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IQ_CCode.LineThickness = 1;
      this.txt_IQ_CCode.Location = new Point(254, 54);
      this.txt_IQ_CCode.Margin = new Padding(4);
      this.txt_IQ_CCode.Name = "txt_IQ_CCode";
      this.txt_IQ_CCode.Size = new Size(379, 33);
      this.txt_IQ_CCode.TabIndex = 76;
      this.txt_IQ_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_IQ_CCode.KeyDown += new KeyEventHandler(this.txt_IQ_CCode_KeyDown);
      this.btn_IQ_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_IQ_Next.FlatAppearance.BorderSize = 0;
      this.btn_IQ_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IQ_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IQ_Next.FlatStyle = FlatStyle.Flat;
      this.btn_IQ_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IQ_Next.ForeColor = Color.White;
      this.btn_IQ_Next.Image = (Image) componentResourceManager.GetObject("btn_IQ_Next.Image");
      this.btn_IQ_Next.Location = new Point(898, 22);
      this.btn_IQ_Next.Name = "btn_IQ_Next";
      this.btn_IQ_Next.Size = new Size(49, 149);
      this.btn_IQ_Next.TabIndex = 75;
      this.btn_IQ_Next.UseVisualStyleBackColor = true;
      this.btn_IQ_Next.Click += new EventHandler(this.btn_IQ_Next_Click);
      this.btn_IQ_Next.MouseEnter += new EventHandler(this.btn_IQ_Next_MouseEnter);
      this.btn_IQ_Next.MouseLeave += new EventHandler(this.btn_IQ_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point(128, 118);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(120, 26);
      this.bunifuCustomLabel3.TabIndex = 74;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft NeoGothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(133, 57);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(114, 26);
      this.bunifuCustomLabel4.TabIndex = 73;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.btn_IQ_Prev.Enabled = false;
      this.btn_IQ_Prev.FlatAppearance.BorderSize = 0;
      this.btn_IQ_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IQ_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IQ_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_IQ_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IQ_Prev.ForeColor = Color.White;
      this.btn_IQ_Prev.Image = (Image) componentResourceManager.GetObject("btn_IQ_Prev.Image");
      this.btn_IQ_Prev.Location = new Point(18, 22);
      this.btn_IQ_Prev.Name = "btn_IQ_Prev";
      this.btn_IQ_Prev.Size = new Size(49, 149);
      this.btn_IQ_Prev.TabIndex = 72;
      this.btn_IQ_Prev.UseVisualStyleBackColor = true;
      this.btn_IQ_Prev.Click += new EventHandler(this.btn_IQ_Prev_Click);
      this.btn_IQ_Prev.MouseEnter += new EventHandler(this.btn_IQ_Prev_MouseEnter);
      this.btn_IQ_Prev.MouseLeave += new EventHandler(this.btn_IQ_Prev_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_IQ_ClearFilter);
      this.Controls.Add((Control) this.dtp_IQ_From);
      this.Controls.Add((Control) this.dgv_IQuotes);
      this.Controls.Add((Control) this.btn_IQ_NewQuote);
      this.Controls.Add((Control) this.btn_IQ_Filter);
      this.Controls.Add((Control) this.dtp_IQ_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.btn_IQ_SelCli);
      this.Controls.Add((Control) this.bunifuSeparator2);
      this.Controls.Add((Control) this.txt_IQ_CName);
      this.Controls.Add((Control) this.txt_IQ_CCode);
      this.Controls.Add((Control) this.btn_IQ_Next);
      this.Controls.Add((Control) this.bunifuCustomLabel3);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.btn_IQ_Prev);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(963, 618);
      this.Name = nameof (Int_Quotes);
      this.Text = "International Quotes";
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Quotes_Load);
      ((ISupportInitialize) this.dgv_IQuotes).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
