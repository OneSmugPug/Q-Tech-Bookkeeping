// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Contractors
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
using System.Text;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class ContractorsOld : Form
  {
    private BindingSource bs = new BindingSource();
    private int CUR_CON = 0;
    private string CODE = string.Empty;
    private bool isFiltered = false;
    private bool isReadOnly = true;
    private IContainer components = (IContainer) null;
    private DataTable conDT;
    private DataTable dt;
    private int NUM_OF_CON;
    private int SELECTED_HOUR;
    private string CCODE;
    private object send;
    private TextBox txt_C_TotPaid;
    private Label label5;
    private TextBox txt_C_TotHours;
    private Label label6;
    private Button btn_C_ClearFilter;
    private BunifuDatepicker dtp_C_From;
    private Button btn_C_NewWW;
    private Button btn_C_Filter;
    private BunifuDatepicker dtp_C_To;
    private BunifuCustomLabel bunifuCustomLabel6;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Button btn_C_SelCon;
    private BunifuSeparator bunifuSeparator2;
    private BunifuMaterialTextbox txt_C_Surname;
    private BunifuMaterialTextbox txt_C_Name;
    private Button btn_C_Next;
    private BunifuCustomLabel bunifuCustomLabel3;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Button btn_C_Prev;
    private Button btn_C_Cancel;
    private Button btn_C_DoneAdd;
    private Button btn_C_DoneEdit;
    private Button btn_C_Edit;
    private Button btn_C_Add;
    private BunifuMaterialTextbox txt_C_EName;
    private BunifuCustomLabel bunifuCustomLabel1;
    private BunifuMaterialTextbox txt_C_EVN;
    private BunifuCustomLabel bunifuCustomLabel2;
    private AdvancedDataGridView dgv_Contractors;
    private BunifuMaterialTextbox txt_C_CCode;
    private BunifuCustomLabel bunifuCustomLabel7;

    public ContractorsOld()
    {
      this.InitializeComponent();
    }

    private void Contractors_Load(object sender, EventArgs e)
    {
      this.dgv_Contractors.DataSource = (object) this.bs;
      this.dtp_C_To.Value = DateTime.Now;
      this.dtp_C_From.Value = this.dtp_C_From.Value.AddDays(-21.0);
      this.loadCon();
      this.loadHours();
      this.dgv_Contractors.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
      this.dgv_Contractors.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_Contractors.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_Contractors.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
      this.dgv_Contractors.Columns[5].DefaultCellStyle.Format = "c";
      this.dgv_Contractors.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_Contractors.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_Contractors.Columns[7].DefaultCellStyle.Format = "c";
      this.dgv_Contractors.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_Contractors.Columns[8].DefaultCellStyle.Format = "c";
      this.dgv_Contractors.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_Contractors.Columns[9].DefaultCellStyle.Format = "c";
      this.dgv_Contractors.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    }

    private void fillTextFields()
    {
      if ((uint) this.conDT.Rows.Count > 0U)
      {
        if (!this.btn_C_Edit.Enabled && !this.dgv_Contractors.Enabled && !this.btn_C_SelCon.Enabled && !this.btn_C_NewWW.Enabled)
        {
          this.btn_C_SelCon.Enabled = true;
          this.btn_C_Edit.Enabled = true;
          this.dgv_Contractors.Enabled = true;
          this.btn_C_NewWW.Enabled = true;
        }
        this.CCODE = this.conDT.Rows[this.CUR_CON]["Contractor_Code"].ToString().Trim();
        this.txt_C_CCode.Text = this.CCODE;
        this.txt_C_Name.Text = this.conDT.Rows[this.CUR_CON]["Name"].ToString().Trim();
        this.txt_C_Surname.Text = this.conDT.Rows[this.CUR_CON]["Surname"].ToString().Trim();
        this.txt_C_EName.Text = this.conDT.Rows[this.CUR_CON]["Employer_Name"].ToString().Trim();
        this.txt_C_EVN.Text = this.conDT.Rows[this.CUR_CON]["Employer_VAT_Number"].ToString().Trim();
      }
      else
      {
        this.btn_C_SelCon.Enabled = false;
        this.btn_C_Edit.Enabled = false;
        this.dgv_Contractors.Enabled = false;
        this.btn_C_NewWW.Enabled = false;
      }
    }

    private void loadCon()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Contractors", dbConnection);
        this.conDT = new DataTable();
        sqlDataAdapter.Fill(this.conDT);
      }
      this.NUM_OF_CON = this.conDT.Rows.Count - 1;
      if (this.NUM_OF_CON == 0)
        this.btn_C_Next.Enabled = false;
      else if (this.NUM_OF_CON != 0 && !this.btn_C_Next.Enabled)
        this.btn_C_Next.Enabled = true;
      this.fillTextFields();
    }

    private void loadHours()
    {
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT Code, Date_Start, Date_End, Hours, Rate_Per_Hour, Total_$, Exchange_Rate, Total_R, QTech_Cut, Final_Total, Remittance, Invoice_Received, Paid, Date_Paid FROM Contractor_Hours WHERE Contractor_Code = '" + this.CCODE + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      Decimal num1 = new Decimal();
      Decimal num2 = new Decimal();
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row["Final_Total"].ToString() != "")
          num1 += Convert.ToDecimal(row["Final_Total"].ToString());
        else
          num1 += Decimal.Zero;
      }
      foreach (DataRow row in (InternalDataCollectionBase) this.dt.Rows)
      {
        if (row["Hours"].ToString() != "")
          num2 += Convert.ToDecimal(row["Hours"].ToString());
        else
          num2 += Decimal.Zero;
      }
      this.txt_C_TotPaid.Text = num1.ToString("c");
      this.txt_C_TotHours.Text = num2.ToString();
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_C_Next_Click(object sender, EventArgs e)
    {
      if (this.CUR_CON + 1 < this.NUM_OF_CON)
      {
        ++this.CUR_CON;
        this.fillTextFields();
        this.loadHours();
      }
      else if (this.CUR_CON + 1 == this.NUM_OF_CON)
      {
        this.btn_C_Next.Enabled = false;
        ++this.CUR_CON;
        this.fillTextFields();
        this.loadHours();
      }
      if (this.CUR_CON == 0 || this.btn_C_Prev.Enabled)
        return;
      this.btn_C_Prev.Enabled = true;
    }

    private void btn_C_Prev_Click(object sender, EventArgs e)
    {
      if (this.CUR_CON - 1 > 0)
      {
        --this.CUR_CON;
        this.fillTextFields();
        this.loadHours();
      }
      else if (this.CUR_CON - 1 == 0)
      {
        this.btn_C_Prev.Enabled = false;
        --this.CUR_CON;
        this.fillTextFields();
        this.loadHours();
      }
      if (this.CUR_CON == this.NUM_OF_CON || this.btn_C_Next.Enabled)
        return;
      this.btn_C_Next.Enabled = true;
    }

    private void btn_C_SelCon_Click(object sender, EventArgs e)
    {
      using (Con_ListOld conList = new Con_ListOld())
      {
        int num = (int) conList.ShowDialog((IWin32Window) this);
      }
    }

    public void setNewCon(int rowIdx)
    {
      this.CUR_CON = rowIdx;
      this.loadCon();
      this.loadHours();
      if (this.CUR_CON != 0 && !this.btn_C_Prev.Enabled)
        this.btn_C_Prev.Enabled = true;
      if (this.CUR_CON == 0 && this.btn_C_Prev.Enabled)
        this.btn_C_Prev.Enabled = false;
      if (this.CUR_CON != this.NUM_OF_CON && !this.btn_C_Next.Enabled)
        this.btn_C_Next.Enabled = true;
      if (this.CUR_CON != this.NUM_OF_CON || !this.btn_C_Next.Enabled)
        return;
      this.btn_C_Next.Enabled = false;
    }

    public void setNewWW(string code)
    {
      this.CODE = code;
    }

    public string getCCode()
    {
      return this.CCODE;
    }

    public string getCName()
    {
      return this.txt_C_Name.Text;
    }

    public string getCSurname()
    {
      return this.txt_C_Surname.Text;
    }

    public string getEName()
    {
      return this.txt_C_EName.Text;
    }

    public int getSelectedHour()
    {
      return this.SELECTED_HOUR;
    }

    public DataTable getHours()
    {
      return this.dt;
    }

    public object getSender()
    {
      return this.send;
    }

    private void setFieldsReadOnly()
    {
      this.isReadOnly = true;
    }

    private void setFieldsNotReadOnly()
    {
      this.isReadOnly = false;
    }

    private void clearFields()
    {
      this.txt_C_Name.Text = string.Empty;
      this.txt_C_Surname.Text = string.Empty;
      this.txt_C_EName.Text = "N/A";
      this.txt_C_EVN.Text = "N/A";
    }

    private void hideButtons()
    {
      this.btn_C_Add.Visible = false;
      this.btn_C_Edit.Visible = false;
      this.btn_C_Cancel.Visible = true;
    }

    private void showButtons()
    {
      this.btn_C_Add.Visible = true;
      this.btn_C_Edit.Visible = true;
      this.btn_C_Cancel.Visible = false;
    }

    private void btn_C_Cancel_Click(object sender, EventArgs e)
    {
      this.setFieldsReadOnly();
      this.showButtons();
      this.btn_C_DoneAdd.Visible = false;
      this.btn_C_DoneEdit.Visible = false;
      this.loadCon();
      this.loadHours();
    }

    private void btn_C_Edit_Click(object sender, EventArgs e)
    {
      this.setFieldsNotReadOnly();
      this.hideButtons();
      this.btn_C_DoneEdit.Visible = true;
      this.txt_C_Name.Focus();
    }

    private void btn_C_DoneEdit_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to update contractor?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          dbConnection.Open();
          try
          {
            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Contractors SET Contractor_Code = @CCode, Name = @Name, Surname = @Surname, Employer_Name = @EName, Eployer_VAT_Number = @EVN WHERE Contractor_Code = @Code", dbConnection))
            {
              sqlCommand.Parameters.AddWithValue("@CCode", (object) this.txt_C_CCode.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Name", (object) this.txt_C_Name.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Surname", (object) this.txt_C_Surname.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@EName", (object) this.txt_C_EName.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@EVN", (object) this.txt_C_EVN.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Code", (object) this.txt_C_CCode.Text.Trim());
              sqlCommand.ExecuteNonQuery();
              int num = (int) MessageBox.Show("Contractor successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.loadCon();
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          finally
          {
            this.setFieldsReadOnly();
            this.showButtons();
            this.btn_C_DoneEdit.Visible = false;
          }
        }
      }
      else
      {
        this.setFieldsReadOnly();
        this.showButtons();
        this.btn_C_DoneEdit.Visible = false;
      }
    }

    private void btn_C_NewWW_Click(object sender, EventArgs e)
    {
      if (this.isFiltered)
        this.RemoveFilter();
      this.send = sender;
      using (Hours_AddOld hoursAdd = new Hours_AddOld())
      {
        int num = (int) hoursAdd.ShowDialog((IWin32Window) this);
      }
      this.loadHours();
    }

    private void dgv_Contractors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.isFiltered)
        this.RemoveFilter();
      this.send = sender;
      this.SELECTED_HOUR = e.RowIndex;
      using (Hours_AddOld hoursAdd = new Hours_AddOld())
      {
        int num = (int) hoursAdd.ShowDialog((IWin32Window) this);
      }
      this.loadHours();
    }

    private void dgv_Contractors_SortStringChanged(object sender, EventArgs e)
    {
      this.bs.Sort = this.dgv_Contractors.SortString;
    }

    private void dgv_Contractors_FilterStringChanged(object sender, EventArgs e)
    {
      this.bs.Filter = this.dgv_Contractors.FilterString;
    }

    private void btn_C_Filter_Click(object sender, EventArgs e)
    {
      this.btn_C_Filter.Visible = false;
      this.btn_C_ClearFilter.Visible = true;
      this.bs.Filter = string.Empty;
      this.bs.Sort = string.Empty;
      this.isFiltered = true;
      using (SqlConnection dbConnection = DBUtils.GetDBConnection())
      {
        dbConnection.Open();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Contractor_Hours WHERE Contractor_Code = '" + this.CCODE + "' AND Date_Start BETWEEN '" + (object) this.dtp_C_From.Value + "' AND '" + (object) this.dtp_C_To.Value + "' OR Date_End BETWEEN '" + (object) this.dtp_C_From.Value + "' AND '" + (object) this.dtp_C_To.Value + "'", dbConnection);
        this.dt = new DataTable();
        sqlDataAdapter.Fill(this.dt);
      }
      this.bs.DataSource = (object) this.dt;
    }

    private void btn_C_ClearF_Click(object sender, EventArgs e)
    {
      this.RemoveFilter();
    }

    private void RemoveFilter()
    {
      this.loadHours();
      this.btn_C_Filter.Visible = true;
      this.btn_C_ClearFilter.Visible = false;
    }

    private void btn_C_Add_Click(object sender, EventArgs e)
    {
      this.setFieldsNotReadOnly();
      this.txt_C_CCode.Text = string.Empty;
      this.clearFields();
      this.hideButtons();
      this.btn_C_DoneAdd.Visible = true;
      this.txt_C_Name.Focus();
    }

    private void btn_C_DoneAdd_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add contractor with Contractor Code: ").Append(this.txt_C_CCode.Text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          dbConnection.Open();
          try
          {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Contractors VALUES (@CCode, @Name, @Surname, @EName, @EVN)", dbConnection))
            {
              sqlCommand.Parameters.AddWithValue("@CCode", (object) this.txt_C_CCode.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Name", (object) this.txt_C_Name.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@Surname", (object) this.txt_C_Surname.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@EName", (object) this.txt_C_EName.Text.Trim());
              sqlCommand.Parameters.AddWithValue("@EVN", (object) this.txt_C_EVN.Text.Trim());
              sqlCommand.ExecuteNonQuery();
              int num = (int) MessageBox.Show("New contractor successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.loadCon();
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          finally
          {
            this.setFieldsReadOnly();
            this.showButtons();
            this.btn_C_DoneAdd.Visible = false;
          }
        }
      }
      else
      {
        this.setFieldsReadOnly();
        this.showButtons();
        this.btn_C_DoneAdd.Visible = false;
      }
    }

    private void txt_C_Name_Leave(object sender, EventArgs e)
    {
      if (!(this.txt_C_CCode.Text == string.Empty))
        return;
      this.generateCCode();
    }

    private void txt_C_Surname_Leave(object sender, EventArgs e)
    {
      if (!(this.txt_C_CCode.Text == string.Empty))
        return;
      this.generateCCode();
    }

    private void generateCCode()
    {
      if (!(this.txt_C_Name.Text != string.Empty) || !(this.txt_C_Surname.Text != string.Empty))
        return;
      char ch = this.txt_C_Name.Text[0];
      string upper1 = ch.ToString().ToUpper();
      ch = this.txt_C_Surname.Text[0];
      string upper2 = ch.ToString().ToUpper();
      ch = this.txt_C_Surname.Text[1];
      string upper3 = ch.ToString().ToUpper();
      this.txt_C_CCode.Text = "QTC_" + (upper1 + upper2 + upper3);
      foreach (DataRow row in (InternalDataCollectionBase) this.conDT.Rows)
      {
        if (row.RowState == DataRowState.Deleted)
        {
          if (row["Contractor_Code", DataRowVersion.Original].ToString().Trim() == this.txt_C_CCode.Text)
          {
            ch = this.txt_C_Name.Text[0];
            string upper4 = ch.ToString().ToUpper();
            ch = this.txt_C_Surname.Text[0];
            string upper5 = ch.ToString().ToUpper();
            ch = this.txt_C_Surname.Text[1];
            string upper6 = ch.ToString().ToUpper();
            ch = this.txt_C_Surname.Text[2];
            string upper7 = ch.ToString().ToUpper();
            this.txt_C_CCode.Text = "QTC_" + (upper4 + upper5 + upper6 + upper7);
            break;
          }
        }
        else if (row["Contractor_Code"].ToString().Trim() == this.txt_C_CCode.Text)
        {
          ch = this.txt_C_Name.Text[0];
          string upper4 = ch.ToString().ToUpper();
          ch = this.txt_C_Surname.Text[0];
          string upper5 = ch.ToString().ToUpper();
          ch = this.txt_C_Surname.Text[1];
          string upper6 = ch.ToString().ToUpper();
          ch = this.txt_C_Surname.Text[2];
          string upper7 = ch.ToString().ToUpper();
          this.txt_C_CCode.Text = "QTC_" + (upper4 + upper5 + upper6 + upper7);
          break;
        }
      }
    }

    private void txt_C_EVN_KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.isReadOnly)
        return;
      e.SuppressKeyPress = true;
    }

    private void txt_C_CCode_KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.isReadOnly)
        return;
      e.SuppressKeyPress = true;
    }

    private void txt_C_Name_KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.isReadOnly)
        return;
      e.SuppressKeyPress = true;
    }

    private void txt_C_Surname_KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.isReadOnly)
        return;
      e.SuppressKeyPress = true;
    }

    private void txt_C_EName_KeyDown(object sender, KeyEventArgs e)
    {
      if (!this.isReadOnly)
        return;
      e.SuppressKeyPress = true;
    }

    private void btn_C_Prev_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_C_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_C_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_C_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_C_SelCon_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_SelCon.Image = (Image) Resources.client_list_white;
      this.btn_C_SelCon.ForeColor = Color.White;
    }

    private void btn_C_SelCon_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_SelCon.Image = (Image) Resources.user_list;
      this.btn_C_SelCon.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_C_NewWW_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_NewWW.Image = (Image) Resources.add_white;
      this.btn_C_NewWW.ForeColor = Color.White;
    }

    private void btn_C_NewWW_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_NewWW.Image = (Image) Resources.add_grey;
      this.btn_C_NewWW.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_C_Filter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_Filter.Image = (Image) Resources.filter_white;
      this.btn_C_Filter.ForeColor = Color.White;
    }

    private void btn_C_Filter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_Filter.Image = (Image) Resources.filter_grey;
      this.btn_C_Filter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_C_ClearFilter_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_ClearFilter.ForeColor = Color.White;
    }

    private void btn_C_ClearFilter_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_C_Add_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_Add.ForeColor = Color.White;
      this.btn_C_Add.Image = (Image) Resources.add_white;
    }

    private void btn_C_Add_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_Add.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_Add.Image = (Image) Resources.add_grey;
    }

    private void btn_C_Edit_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_Edit.ForeColor = Color.White;
      this.btn_C_Edit.Image = (Image) Resources.edit_white;
    }

    private void btn_C_Edit_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_Edit.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_Edit.Image = (Image) Resources.edit_grey;
    }

    private void btn_C_DoneAdd_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_DoneAdd.ForeColor = Color.White;
    }

    private void btn_C_DoneAdd_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_C_DoneEdit_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_DoneEdit.ForeColor = Color.White;
    }

    private void btn_C_DoneEdit_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_C_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_C_Cancel.ForeColor = Color.White;
    }

    private void btn_C_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_C_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContractorsOld));
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      this.txt_C_TotPaid = new TextBox();
      this.label5 = new Label();
      this.txt_C_TotHours = new TextBox();
      this.label6 = new Label();
      this.btn_C_ClearFilter = new Button();
      this.dtp_C_From = new BunifuDatepicker();
      this.btn_C_NewWW = new Button();
      this.btn_C_Filter = new Button();
      this.dtp_C_To = new BunifuDatepicker();
      this.bunifuCustomLabel6 = new BunifuCustomLabel();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.btn_C_SelCon = new Button();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.txt_C_Surname = new BunifuMaterialTextbox();
      this.txt_C_Name = new BunifuMaterialTextbox();
      this.btn_C_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_C_Prev = new Button();
      this.btn_C_Cancel = new Button();
      this.btn_C_DoneAdd = new Button();
      this.btn_C_DoneEdit = new Button();
      this.btn_C_Edit = new Button();
      this.btn_C_Add = new Button();
      this.txt_C_EName = new BunifuMaterialTextbox();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.txt_C_EVN = new BunifuMaterialTextbox();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.dgv_Contractors = new AdvancedDataGridView();
      this.txt_C_CCode = new BunifuMaterialTextbox();
      this.bunifuCustomLabel7 = new BunifuCustomLabel();
      ((ISupportInitialize) this.dgv_Contractors).BeginInit();
      this.SuspendLayout();
      this.txt_C_TotPaid.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.txt_C_TotPaid.Location = new Point(847, 583);
      this.txt_C_TotPaid.Name = "txt_C_TotPaid";
      this.txt_C_TotPaid.ReadOnly = true;
      this.txt_C_TotPaid.Size = new Size(105, 20);
      this.txt_C_TotPaid.TabIndex = 0;
      this.txt_C_TotPaid.TabStop = false;
      this.label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.ForeColor = Color.FromArgb(64, 64, 64);
      this.label5.Location = new Point(765, 584);
      this.label5.Name = "label5";
      this.label5.Size = new Size(76, 17);
      this.label5.TabIndex = 0;
      this.label5.Text = "Total Paid:";
      this.txt_C_TotHours.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.txt_C_TotHours.Location = new Point(628, 583);
      this.txt_C_TotHours.Name = "txt_C_TotHours";
      this.txt_C_TotHours.ReadOnly = true;
      this.txt_C_TotHours.Size = new Size(106, 20);
      this.txt_C_TotHours.TabIndex = 0;
      this.txt_C_TotHours.TabStop = false;
      this.label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6.ForeColor = Color.FromArgb(64, 64, 64);
      this.label6.Location = new Point(536, 584);
      this.label6.Name = "label6";
      this.label6.Size = new Size(86, 17);
      this.label6.TabIndex = 0;
      this.label6.Text = "Total Hours:";
      this.btn_C_ClearFilter.FlatAppearance.BorderSize = 0;
      this.btn_C_ClearFilter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_ClearFilter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_ClearFilter.FlatStyle = FlatStyle.Flat;
      this.btn_C_ClearFilter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_ClearFilter.Location = new Point(554, 232);
      this.btn_C_ClearFilter.Name = "btn_C_ClearFilter";
      this.btn_C_ClearFilter.Size = new Size(114, 40);
      this.btn_C_ClearFilter.TabIndex = 86;
      this.btn_C_ClearFilter.Text = "Clear Filter";
      this.btn_C_ClearFilter.UseVisualStyleBackColor = true;
      this.btn_C_ClearFilter.Visible = false;
      this.btn_C_ClearFilter.Click += new EventHandler(this.btn_C_ClearF_Click);
      this.btn_C_ClearFilter.MouseEnter += new EventHandler(this.btn_C_ClearFilter_MouseEnter);
      this.btn_C_ClearFilter.MouseLeave += new EventHandler(this.btn_C_ClearFilter_MouseLeave);
      this.dtp_C_From.BackColor = Color.LightGray;
      this.dtp_C_From.BorderRadius = 0;
      this.dtp_C_From.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_C_From.Format = DateTimePickerFormat.Short;
      this.dtp_C_From.FormatCustom = (string) null;
      this.dtp_C_From.Location = new Point(71, 235);
      this.dtp_C_From.Name = "dtp_C_From";
      this.dtp_C_From.Size = new Size(208, 36);
      this.dtp_C_From.TabIndex = 71;
      this.dtp_C_From.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.btn_C_NewWW.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_C_NewWW.FlatAppearance.BorderSize = 0;
      this.btn_C_NewWW.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_NewWW.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_NewWW.FlatStyle = FlatStyle.Flat;
      this.btn_C_NewWW.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_NewWW.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_NewWW.Image = (Image) componentResourceManager.GetObject("btn_C_NewWW.Image");
      this.btn_C_NewWW.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_C_NewWW.Location = new Point(790, 232);
      this.btn_C_NewWW.Name = "btn_C_NewWW";
      this.btn_C_NewWW.Size = new Size(159, 40);
      this.btn_C_NewWW.TabIndex = 84;
      this.btn_C_NewWW.Text = "New Work Week";
      this.btn_C_NewWW.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_C_NewWW.UseVisualStyleBackColor = true;
      this.btn_C_NewWW.Click += new EventHandler(this.btn_C_NewWW_Click);
      this.btn_C_NewWW.MouseEnter += new EventHandler(this.btn_C_NewWW_MouseEnter);
      this.btn_C_NewWW.MouseLeave += new EventHandler(this.btn_C_NewWW_MouseLeave);
      this.btn_C_Filter.FlatAppearance.BorderSize = 0;
      this.btn_C_Filter.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_Filter.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_Filter.FlatStyle = FlatStyle.Flat;
      this.btn_C_Filter.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_Filter.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_Filter.Image = (Image) componentResourceManager.GetObject("btn_C_Filter.Image");
      this.btn_C_Filter.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_C_Filter.Location = new Point(554, 232);
      this.btn_C_Filter.Name = "btn_C_Filter";
      this.btn_C_Filter.Size = new Size(114, 40);
      this.btn_C_Filter.TabIndex = 83;
      this.btn_C_Filter.Text = "Filter";
      this.btn_C_Filter.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_C_Filter.UseVisualStyleBackColor = true;
      this.btn_C_Filter.Click += new EventHandler(this.btn_C_Filter_Click);
      this.btn_C_Filter.MouseEnter += new EventHandler(this.btn_C_Filter_MouseEnter);
      this.btn_C_Filter.MouseLeave += new EventHandler(this.btn_C_Filter_MouseLeave);
      this.dtp_C_To.BackColor = Color.LightGray;
      this.dtp_C_To.BorderRadius = 0;
      this.dtp_C_To.ForeColor = Color.FromArgb(19, 118, 188);
      this.dtp_C_To.Format = DateTimePickerFormat.Short;
      this.dtp_C_To.FormatCustom = (string) null;
      this.dtp_C_To.Location = new Point(325, 235);
      this.dtp_C_To.Name = "dtp_C_To";
      this.dtp_C_To.Size = new Size(208, 36);
      this.dtp_C_To.TabIndex = 82;
      this.dtp_C_To.Value = new DateTime(2018, 12, 19, 0, 0, 0, 0);
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel6.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel6.Location = new Point(285, 242);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new Size(34, 19);
      this.bunifuCustomLabel6.TabIndex = 81;
      this.bunifuCustomLabel6.Text = "To:";
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(13, 242);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(52, 19);
      this.bunifuCustomLabel5.TabIndex = 80;
      this.bunifuCustomLabel5.Text = "From:";
      this.btn_C_SelCon.FlatAppearance.BorderSize = 0;
      this.btn_C_SelCon.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_SelCon.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_SelCon.FlatStyle = FlatStyle.Flat;
      this.btn_C_SelCon.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_SelCon.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_SelCon.Image = (Image) componentResourceManager.GetObject("btn_C_SelCon.Image");
      this.btn_C_SelCon.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_C_SelCon.Location = new Point(722, 161);
      this.btn_C_SelCon.Name = "btn_C_SelCon";
      this.btn_C_SelCon.Size = new Size(147, 40);
      this.btn_C_SelCon.TabIndex = 79;
      this.btn_C_SelCon.Text = "Contractor List";
      this.btn_C_SelCon.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_C_SelCon.UseVisualStyleBackColor = true;
      this.btn_C_SelCon.Click += new EventHandler(this.btn_C_SelCon_Click);
      this.btn_C_SelCon.MouseEnter += new EventHandler(this.btn_C_SelCon_MouseEnter);
      this.btn_C_SelCon.MouseLeave += new EventHandler(this.btn_C_SelCon_MouseLeave);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(18, 207);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(934, 35);
      this.bunifuSeparator2.TabIndex = 78;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.txt_C_Surname.Cursor = Cursors.IBeam;
      this.txt_C_Surname.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_C_Surname.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_C_Surname.HintForeColor = Color.Empty;
      this.txt_C_Surname.HintText = "";
      this.txt_C_Surname.isPassword = false;
      this.txt_C_Surname.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_C_Surname.LineIdleColor = Color.Gray;
      this.txt_C_Surname.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_C_Surname.LineThickness = 1;
      this.txt_C_Surname.Location = new Point(678, 44);
      this.txt_C_Surname.Margin = new Padding(4);
      this.txt_C_Surname.Name = "txt_C_Surname";
      this.txt_C_Surname.Size = new Size(191, 33);
      this.txt_C_Surname.TabIndex = 77;
      this.txt_C_Surname.TextAlign = HorizontalAlignment.Left;
      this.txt_C_Surname.KeyDown += new KeyEventHandler(this.txt_C_Surname_KeyDown);
      this.txt_C_Surname.Leave += new EventHandler(this.txt_C_Surname_Leave);
      this.txt_C_Name.Cursor = Cursors.IBeam;
      this.txt_C_Name.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_C_Name.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_C_Name.HintForeColor = Color.Empty;
      this.txt_C_Name.HintText = "";
      this.txt_C_Name.isPassword = false;
      this.txt_C_Name.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_C_Name.LineIdleColor = Color.Gray;
      this.txt_C_Name.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_C_Name.LineThickness = 1;
      this.txt_C_Name.Location = new Point(349, 44);
      this.txt_C_Name.Margin = new Padding(4);
      this.txt_C_Name.Name = "txt_C_Name";
      this.txt_C_Name.Size = new Size(194, 33);
      this.txt_C_Name.TabIndex = 76;
      this.txt_C_Name.TextAlign = HorizontalAlignment.Left;
      this.txt_C_Name.KeyDown += new KeyEventHandler(this.txt_C_Name_KeyDown);
      this.txt_C_Name.Leave += new EventHandler(this.txt_C_Name_Leave);
      this.btn_C_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_C_Next.FlatAppearance.BorderSize = 0;
      this.btn_C_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_Next.FlatStyle = FlatStyle.Flat;
      this.btn_C_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_Next.ForeColor = Color.White;
      this.btn_C_Next.Image = (Image) componentResourceManager.GetObject("btn_C_Next.Image");
      this.btn_C_Next.Location = new Point(898, 21);
      this.btn_C_Next.Name = "btn_C_Next";
      this.btn_C_Next.Size = new Size(49, 149);
      this.btn_C_Next.TabIndex = 75;
      this.btn_C_Next.UseVisualStyleBackColor = true;
      this.btn_C_Next.Click += new EventHandler(this.btn_C_Next_Click);
      this.btn_C_Next.MouseEnter += new EventHandler(this.btn_C_Next_MouseEnter);
      this.btn_C_Next.MouseLeave += new EventHandler(this.btn_C_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel3.Location = new Point(604, 54);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(69, 20);
      this.bunifuCustomLabel3.TabIndex = 74;
      this.bunifuCustomLabel3.Text = "Surame:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(287, 54);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(55, 20);
      this.bunifuCustomLabel4.TabIndex = 73;
      this.bunifuCustomLabel4.Text = "Name:";
      this.btn_C_Prev.Enabled = false;
      this.btn_C_Prev.FlatAppearance.BorderSize = 0;
      this.btn_C_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_C_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_Prev.ForeColor = Color.White;
      this.btn_C_Prev.Image = (Image) componentResourceManager.GetObject("btn_C_Prev.Image");
      this.btn_C_Prev.Location = new Point(18, 21);
      this.btn_C_Prev.Name = "btn_C_Prev";
      this.btn_C_Prev.Size = new Size(49, 149);
      this.btn_C_Prev.TabIndex = 72;
      this.btn_C_Prev.UseVisualStyleBackColor = true;
      this.btn_C_Prev.Click += new EventHandler(this.btn_C_Prev_Click);
      this.btn_C_Prev.MouseEnter += new EventHandler(this.btn_C_Prev_MouseEnter);
      this.btn_C_Prev.MouseLeave += new EventHandler(this.btn_C_Prev_MouseLeave);
      this.btn_C_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_C_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_C_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_Cancel.Location = new Point(234, 161);
      this.btn_C_Cancel.Name = "btn_C_Cancel";
      this.btn_C_Cancel.Size = new Size(114, 40);
      this.btn_C_Cancel.TabIndex = 91;
      this.btn_C_Cancel.Text = "Cancel";
      this.btn_C_Cancel.UseVisualStyleBackColor = true;
      this.btn_C_Cancel.Visible = false;
      this.btn_C_Cancel.Click += new EventHandler(this.btn_C_Cancel_Click);
      this.btn_C_Cancel.MouseEnter += new EventHandler(this.btn_C_Cancel_MouseEnter);
      this.btn_C_Cancel.MouseLeave += new EventHandler(this.btn_C_Cancel_MouseLeave);
      this.btn_C_DoneAdd.FlatAppearance.BorderSize = 0;
      this.btn_C_DoneAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_DoneAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_DoneAdd.FlatStyle = FlatStyle.Flat;
      this.btn_C_DoneAdd.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_DoneAdd.Location = new Point(114, 161);
      this.btn_C_DoneAdd.Name = "btn_C_DoneAdd";
      this.btn_C_DoneAdd.Size = new Size(114, 40);
      this.btn_C_DoneAdd.TabIndex = 90;
      this.btn_C_DoneAdd.Text = "Done";
      this.btn_C_DoneAdd.UseVisualStyleBackColor = true;
      this.btn_C_DoneAdd.Visible = false;
      this.btn_C_DoneAdd.Click += new EventHandler(this.btn_C_DoneAdd_Click);
      this.btn_C_DoneAdd.MouseEnter += new EventHandler(this.btn_C_DoneAdd_MouseEnter);
      this.btn_C_DoneAdd.MouseLeave += new EventHandler(this.btn_C_DoneAdd_MouseLeave);
      this.btn_C_DoneEdit.FlatAppearance.BorderSize = 0;
      this.btn_C_DoneEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_DoneEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_DoneEdit.FlatStyle = FlatStyle.Flat;
      this.btn_C_DoneEdit.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_DoneEdit.Location = new Point(114, 161);
      this.btn_C_DoneEdit.Name = "btn_C_DoneEdit";
      this.btn_C_DoneEdit.Size = new Size(114, 40);
      this.btn_C_DoneEdit.TabIndex = 89;
      this.btn_C_DoneEdit.Text = "Done";
      this.btn_C_DoneEdit.UseVisualStyleBackColor = true;
      this.btn_C_DoneEdit.Visible = false;
      this.btn_C_DoneEdit.Click += new EventHandler(this.btn_C_DoneEdit_Click);
      this.btn_C_DoneEdit.MouseEnter += new EventHandler(this.btn_C_DoneEdit_MouseEnter);
      this.btn_C_DoneEdit.MouseLeave += new EventHandler(this.btn_C_DoneEdit_MouseLeave);
      this.btn_C_Edit.FlatAppearance.BorderSize = 0;
      this.btn_C_Edit.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_Edit.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_Edit.FlatStyle = FlatStyle.Flat;
      this.btn_C_Edit.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_Edit.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_Edit.Image = (Image) componentResourceManager.GetObject("btn_C_Edit.Image");
      this.btn_C_Edit.Location = new Point(261, 161);
      this.btn_C_Edit.Name = "btn_C_Edit";
      this.btn_C_Edit.Size = new Size(149, 40);
      this.btn_C_Edit.TabIndex = 88;
      this.btn_C_Edit.Text = "Edit Contractor";
      this.btn_C_Edit.TextAlign = ContentAlignment.MiddleRight;
      this.btn_C_Edit.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_C_Edit.UseVisualStyleBackColor = true;
      this.btn_C_Edit.Click += new EventHandler(this.btn_C_Edit_Click);
      this.btn_C_Edit.MouseEnter += new EventHandler(this.btn_C_Edit_MouseEnter);
      this.btn_C_Edit.MouseLeave += new EventHandler(this.btn_C_Edit_MouseLeave);
      this.btn_C_Add.FlatAppearance.BorderSize = 0;
      this.btn_C_Add.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_Add.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_C_Add.FlatStyle = FlatStyle.Flat;
      this.btn_C_Add.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_Add.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_C_Add.Image = (Image) componentResourceManager.GetObject("btn_C_Add.Image");
      this.btn_C_Add.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_C_Add.Location = new Point(94, 161);
      this.btn_C_Add.Name = "btn_C_Add";
      this.btn_C_Add.Size = new Size(161, 40);
      this.btn_C_Add.TabIndex = 87;
      this.btn_C_Add.Text = "Add Contractor";
      this.btn_C_Add.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_C_Add.UseVisualStyleBackColor = true;
      this.btn_C_Add.Click += new EventHandler(this.btn_C_Add_Click);
      this.btn_C_Add.MouseEnter += new EventHandler(this.btn_C_Add_MouseEnter);
      this.btn_C_Add.MouseLeave += new EventHandler(this.btn_C_Add_MouseLeave);
      this.txt_C_EName.Cursor = Cursors.IBeam;
      this.txt_C_EName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_C_EName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_C_EName.HintForeColor = Color.Empty;
      this.txt_C_EName.HintText = "";
      this.txt_C_EName.isPassword = false;
      this.txt_C_EName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_C_EName.LineIdleColor = Color.Gray;
      this.txt_C_EName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_C_EName.LineThickness = 1;
      this.txt_C_EName.Location = new Point(218, 100);
      this.txt_C_EName.Margin = new Padding(4);
      this.txt_C_EName.Name = "txt_C_EName";
      this.txt_C_EName.Size = new Size(214, 33);
      this.txt_C_EName.TabIndex = 93;
      this.txt_C_EName.TextAlign = HorizontalAlignment.Left;
      this.txt_C_EName.KeyDown += new KeyEventHandler(this.txt_C_EName_KeyDown);
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(86, 112);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(125, 20);
      this.bunifuCustomLabel1.TabIndex = 92;
      this.bunifuCustomLabel1.Text = "Employer Name:";
      this.txt_C_EVN.Cursor = Cursors.IBeam;
      this.txt_C_EVN.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_C_EVN.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_C_EVN.HintForeColor = Color.Empty;
      this.txt_C_EVN.HintText = "";
      this.txt_C_EVN.isPassword = false;
      this.txt_C_EVN.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_C_EVN.LineIdleColor = Color.Gray;
      this.txt_C_EVN.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_C_EVN.LineThickness = 1;
      this.txt_C_EVN.Location = new Point(668, 100);
      this.txt_C_EVN.Margin = new Padding(4);
      this.txt_C_EVN.Name = "txt_C_EVN";
      this.txt_C_EVN.Size = new Size(201, 33);
      this.txt_C_EVN.TabIndex = 95;
      this.txt_C_EVN.TextAlign = HorizontalAlignment.Left;
      this.txt_C_EVN.KeyDown += new KeyEventHandler(this.txt_C_EVN_KeyDown);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(488, 112);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(174, 20);
      this.bunifuCustomLabel2.TabIndex = 94;
      this.bunifuCustomLabel2.Text = "Employer VAT Number:";
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
      this.dgv_Contractors.Location = new Point(1, 277);
      this.dgv_Contractors.Name = "dgv_Contractors";
      this.dgv_Contractors.ReadOnly = true;
      this.dgv_Contractors.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_Contractors.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_Contractors.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_Contractors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Contractors.Size = new Size(963, 286);
      this.dgv_Contractors.TabIndex = 96;
      this.dgv_Contractors.TimeFilter = false;
      this.dgv_Contractors.SortStringChanged += new EventHandler(this.dgv_Contractors_SortStringChanged);
      this.dgv_Contractors.FilterStringChanged += new EventHandler(this.dgv_Contractors_FilterStringChanged);
      this.dgv_Contractors.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_Contractors_CellDoubleClick);
      this.txt_C_CCode.Cursor = Cursors.IBeam;
      this.txt_C_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_C_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_C_CCode.HintForeColor = Color.Empty;
      this.txt_C_CCode.HintText = "";
      this.txt_C_CCode.isPassword = false;
      this.txt_C_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_C_CCode.LineIdleColor = Color.Gray;
      this.txt_C_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_C_CCode.LineThickness = 1;
      this.txt_C_CCode.Location = new Point(148, 44);
      this.txt_C_CCode.Margin = new Padding(4);
      this.txt_C_CCode.Name = "txt_C_CCode";
      this.txt_C_CCode.Size = new Size(88, 33);
      this.txt_C_CCode.TabIndex = 99;
      this.txt_C_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_C_CCode.KeyDown += new KeyEventHandler(this.txt_C_CCode_KeyDown);
      this.bunifuCustomLabel7.AutoSize = true;
      this.bunifuCustomLabel7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel7.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel7.Location = new Point(86, 54);
      this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
      this.bunifuCustomLabel7.Size = new Size(51, 20);
      this.bunifuCustomLabel7.TabIndex = 98;
      this.bunifuCustomLabel7.Text = "Code:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.txt_C_CCode);
      this.Controls.Add((Control) this.bunifuCustomLabel7);
      this.Controls.Add((Control) this.dgv_Contractors);
      this.Controls.Add((Control) this.txt_C_EVN);
      this.Controls.Add((Control) this.bunifuCustomLabel2);
      this.Controls.Add((Control) this.txt_C_EName);
      this.Controls.Add((Control) this.bunifuCustomLabel1);
      this.Controls.Add((Control) this.btn_C_Cancel);
      this.Controls.Add((Control) this.btn_C_DoneAdd);
      this.Controls.Add((Control) this.btn_C_DoneEdit);
      this.Controls.Add((Control) this.btn_C_Edit);
      this.Controls.Add((Control) this.btn_C_Add);
      this.Controls.Add((Control) this.btn_C_ClearFilter);
      this.Controls.Add((Control) this.dtp_C_From);
      this.Controls.Add((Control) this.btn_C_NewWW);
      this.Controls.Add((Control) this.btn_C_Filter);
      this.Controls.Add((Control) this.dtp_C_To);
      this.Controls.Add((Control) this.bunifuCustomLabel6);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.btn_C_SelCon);
      this.Controls.Add((Control) this.bunifuSeparator2);
      this.Controls.Add((Control) this.txt_C_Surname);
      this.Controls.Add((Control) this.txt_C_Name);
      this.Controls.Add((Control) this.btn_C_Next);
      this.Controls.Add((Control) this.bunifuCustomLabel3);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.btn_C_Prev);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.txt_C_TotHours);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.txt_C_TotPaid);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(963, 618);
      this.Name = nameof (ContractorsOld);
      this.Text = nameof (ContractorsOld);
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.Contractors_Load);
      ((ISupportInitialize) this.dgv_Contractors).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
