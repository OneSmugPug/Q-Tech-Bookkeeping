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
    public partial class Contractors : Form
    {
        private BindingSource bs = new BindingSource();
        private int CUR_CON = 0;
        private string CODE = string.Empty;
        private bool isFiltered = false;
        private bool isReadOnly = true;
        //private IContainer components = (IContainer)null;            <------- Gee 'n error, delete as dit nie hier moet wees nie
        private DataTable conDT;
        private DataTable dt;
        private int NUM_OF_CON;
        private int SELECTED_HOUR;
        private string CCODE;
        private object send;
        

        public Contractors()
        {
            InitializeComponent();
        }

        private void Contractors_Load(object sender, EventArgs e)
        {
            dgv_Contractors.DataSource = bs;
            dtp_C_To.Value = DateTime.Now;
            dtp_C_From.Value = dtp_C_From.Value.AddDays(-21.0);
            LoadCon();
            LoadHours();
            dgv_Contractors.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_Contractors.Columns[4].DefaultCellStyle.Format = "c";
            dgv_Contractors.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_Contractors.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_Contractors.Columns[5].DefaultCellStyle.Format = "c";
            dgv_Contractors.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_Contractors.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_Contractors.Columns[7].DefaultCellStyle.Format = "c";
            dgv_Contractors.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_Contractors.Columns[8].DefaultCellStyle.Format = "c";
            dgv_Contractors.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_Contractors.Columns[9].DefaultCellStyle.Format = "c";
            dgv_Contractors.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void FillTextFields()
        {
            if ((uint)conDT.Rows.Count > 0U)
            {
                if (!btn_C_Edit.Enabled && !dgv_Contractors.Enabled && !btn_C_SelCon.Enabled && !btn_C_NewWW.Enabled)
                {
                    btn_C_SelCon.Enabled = true;
                    btn_C_Edit.Enabled = true;
                    dgv_Contractors.Enabled = true;
                    btn_C_NewWW.Enabled = true;
                }
                CCODE = conDT.Rows[CUR_CON]["Contractor_Code"].ToString().Trim();
                txt_C_CCode.Text = CCODE;
                txt_C_Name.Text = conDT.Rows[CUR_CON]["Name"].ToString().Trim();
                txt_C_Surname.Text = conDT.Rows[CUR_CON]["Surname"].ToString().Trim();
                txt_C_EName.Text = conDT.Rows[CUR_CON]["Employer_Name"].ToString().Trim();
                txt_C_EVN.Text = conDT.Rows[CUR_CON]["Employer_VAT_Number"].ToString().Trim();
            }
            else
            {
                btn_C_SelCon.Enabled = false;
                btn_C_Edit.Enabled = false;
                dgv_Contractors.Enabled = false;
                btn_C_NewWW.Enabled = false;
            }
        }

        private void LoadCon()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Contractors", dbConnection);
                conDT = new DataTable();
                sqlDataAdapter.Fill(conDT);
            }
            NUM_OF_CON = conDT.Rows.Count - 1;
            if (NUM_OF_CON == 0)
                btn_C_Next.Enabled = false;
            else if (NUM_OF_CON != 0 && !btn_C_Next.Enabled)
                btn_C_Next.Enabled = true;
            FillTextFields();
        }

        private void LoadHours()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT Code, Date_Start, Date_End, Hours, Rate_Per_Hour, Total_$, Exchange_Rate, Total_R, QTech_Cut, Final_Total, Remittance, Invoice_Received, Paid, Date_Paid FROM Contractor_Hours WHERE Contractor_Code = '" + CCODE + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            Decimal num1 = new Decimal();
            Decimal num2 = new Decimal();
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                if (row["Final_Total"].ToString() != "")
                    num1 += Convert.ToDecimal(row["Final_Total"].ToString());
                else
                    num1 += Decimal.Zero;
            }
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                if (row["Hours"].ToString() != "")
                    num2 += Convert.ToDecimal(row["Hours"].ToString());
                else
                    num2 += Decimal.Zero;
            }
            txt_C_TotPaid.Text = num1.ToString("c");
            txt_C_TotHours.Text = num2.ToString();
            bs.DataSource = (object)dt;
        }

        private void Btn_C_Next_Click(object sender, EventArgs e)
        {
            if (CUR_CON + 1 < NUM_OF_CON)
            {
                ++CUR_CON;
                FillTextFields();
                LoadHours();
            }
            else if (CUR_CON + 1 == NUM_OF_CON)
            {
                btn_C_Next.Enabled = false;
                ++CUR_CON;
                FillTextFields();
                LoadHours();
            }
            if (CUR_CON == 0 || btn_C_Prev.Enabled)
                return;
            btn_C_Prev.Enabled = true;
        }

        private void Btn_C_Prev_Click(object sender, EventArgs e)
        {
            if (CUR_CON - 1 > 0)
            {
                --CUR_CON;
                FillTextFields();
                LoadHours();
            }
            else if (CUR_CON - 1 == 0)
            {
                btn_C_Prev.Enabled = false;
                --CUR_CON;
                FillTextFields();
                LoadHours();
            }
            if (CUR_CON == NUM_OF_CON || btn_C_Next.Enabled)
                return;
            btn_C_Next.Enabled = true;
        }

        private void Btn_C_SelCon_Click(object sender, EventArgs e)
        {
            using (Con_List conList = new Con_List())
            {
                int num = (int)conList.ShowDialog((IWin32Window)this);
            }
        }

        public void SetNewCon(int rowIdx)
        {
            CUR_CON = rowIdx;
            LoadCon();
            LoadHours();
            if (CUR_CON != 0 && !btn_C_Prev.Enabled)
                btn_C_Prev.Enabled = true;
            if (CUR_CON == 0 && btn_C_Prev.Enabled)
                btn_C_Prev.Enabled = false;
            if (CUR_CON != NUM_OF_CON && !btn_C_Next.Enabled)
                btn_C_Next.Enabled = true;
            if (CUR_CON != NUM_OF_CON || !btn_C_Next.Enabled)
                return;
            btn_C_Next.Enabled = false;
        }

        public void SetNewWW(string code)
        {
            CODE = code;
        }

        public string GetCCode()
        {
            return CCODE;
        }

        public string GetCName()
        {
            return txt_C_Name.Text;
        }

        public string GetCSurname()
        {
            return txt_C_Surname.Text;
        }

        public string GetEName()
        {
            return txt_C_EName.Text;
        }

        public int GetSelectedHour()
        {
            return SELECTED_HOUR;
        }

        public DataTable GetHours()
        {
            return dt;
        }

        public object GetSender()
        {
            return send;
        }

        private void SetFieldsReadOnly()
        {
            isReadOnly = true;
        }

        private void SetFieldsNotReadOnly()
        {
            isReadOnly = false;
        }

        private void ClearFields()
        {
            txt_C_Name.Text = string.Empty;
            txt_C_Surname.Text = string.Empty;
            txt_C_EName.Text = "N/A";
            txt_C_EVN.Text = "N/A";
        }

        private void HideButtons()
        {
            btn_C_Add.Visible = false;
            btn_C_Edit.Visible = false;
            btn_C_Cancel.Visible = true;
        }

        private void ShowButtons()
        {
            btn_C_Add.Visible = true;
            btn_C_Edit.Visible = true;
            btn_C_Cancel.Visible = false;
        }

        private void Btn_C_Cancel_Click(object sender, EventArgs e)
        {
            SetFieldsReadOnly();
            ShowButtons();
            btn_C_DoneAdd.Visible = false;
            btn_C_DoneEdit.Visible = false;
            LoadCon();
            LoadHours();
        }

        private void Btn_C_Edit_Click(object sender, EventArgs e)
        {
            SetFieldsNotReadOnly();
            HideButtons();
            btn_C_DoneEdit.Visible = true;
            txt_C_Name.Focus();
        }

        private void Btn_C_DoneEdit_Click(object sender, EventArgs e)
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
                            sqlCommand.Parameters.AddWithValue("@CCode", txt_C_CCode.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Name", txt_C_Name.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Surname", txt_C_Surname.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@EName", txt_C_EName.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@EVN", txt_C_EVN.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Code", txt_C_CCode.Text.Trim());
                            sqlCommand.ExecuteNonQuery();
                            int num = (int)MessageBox.Show("Contractor successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        LoadCon();
                    }
                    catch (Exception ex)
                    {
                        int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    finally
                    {
                        SetFieldsReadOnly();
                        ShowButtons();
                        btn_C_DoneEdit.Visible = false;
                    }
                }
            }
            else
            {
                SetFieldsReadOnly();
                ShowButtons();
                btn_C_DoneEdit.Visible = false;
            }
        }

        private void Btn_C_NewWW_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            send = sender;
            using (HoursAdd hoursAdd = new HoursAdd())
            {
                int num = (int)hoursAdd.ShowDialog((IWin32Window)this);
            }
            LoadHours();
        }

        private void Dgv_Contractors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            send = sender;
            SELECTED_HOUR = e.RowIndex;
            using (HoursAdd hoursAdd = new HoursAdd())
            {
                int num = (int)hoursAdd.ShowDialog((IWin32Window)this);
            }
            LoadHours();
        }

        private void Dgv_Contractors_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_Contractors.SortString;
        }

        private void Dgv_Contractors_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_Contractors.FilterString;
        }

        private void Btn_C_Filter_Click(object sender, EventArgs e)
        {
            btn_C_Filter.Visible = false;
            btn_C_ClearFilter.Visible = true;
            bs.Filter = string.Empty;
            bs.Sort = string.Empty;
            isFiltered = true;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Contractor_Hours WHERE Contractor_Code = '" + CCODE + "' AND Date_Start BETWEEN '" + (object)dtp_C_From.Value + "' AND '" + (object)dtp_C_To.Value + "' OR Date_End BETWEEN '" + (object)dtp_C_From.Value + "' AND '" + (object)dtp_C_To.Value + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = (object)dt;
        }

        private void Btn_C_ClearF_Click(object sender, EventArgs e)
        {
            RemoveFilter();
        }

        private void RemoveFilter()
        {
            LoadHours();
            btn_C_Filter.Visible = true;
            btn_C_ClearFilter.Visible = false;
        }

        private void Btn_C_Add_Click(object sender, EventArgs e)
        {
            SetFieldsNotReadOnly();
            txt_C_CCode.Text = string.Empty;
            ClearFields();
            HideButtons();
            btn_C_DoneAdd.Visible = true;
            txt_C_Name.Focus();
        }

        private void Btn_C_DoneAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add contractor with Contractor Code: ").Append(txt_C_CCode.Text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                {
                    dbConnection.Open();
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Contractors VALUES (@CCode, @Name, @Surname, @EName, @EVN)", dbConnection))
                        {
                            sqlCommand.Parameters.AddWithValue("@CCode", (object)txt_C_CCode.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Name", (object)txt_C_Name.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Surname", (object)txt_C_Surname.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@EName", (object)txt_C_EName.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@EVN", (object)txt_C_EVN.Text.Trim());
                            sqlCommand.ExecuteNonQuery();
                            int num = (int)MessageBox.Show("New contractor successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        LoadCon();
                    }
                    catch (Exception ex)
                    {
                        int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    finally
                    {
                        SetFieldsReadOnly();
                        ShowButtons();
                        btn_C_DoneAdd.Visible = false;
                    }
                }
            }
            else
            {
                SetFieldsReadOnly();
                ShowButtons();
                btn_C_DoneAdd.Visible = false;
            }
        }

        private void Txt_C_Name_Leave(object sender, EventArgs e)
        {
            if (!(txt_C_CCode.Text == string.Empty))
                return;
            GenerateCCode();
        }

        private void Txt_C_Surname_Leave(object sender, EventArgs e)
        {
            if (!(txt_C_CCode.Text == string.Empty))
                return;
            GenerateCCode();
        }

        private void GenerateCCode()
        {
            if (!(txt_C_Name.Text != string.Empty) || !(txt_C_Surname.Text != string.Empty))
                return;
            char ch = txt_C_Name.Text[0];
            string upper1 = ch.ToString().ToUpper();
            ch = txt_C_Surname.Text[0];
            string upper2 = ch.ToString().ToUpper();
            ch = txt_C_Surname.Text[1];
            string upper3 = ch.ToString().ToUpper();
            txt_C_CCode.Text = "QTC_" + (upper1 + upper2 + upper3);
            foreach (DataRow row in (InternalDataCollectionBase)conDT.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    if (row["Contractor_Code", DataRowVersion.Original].ToString().Trim() == txt_C_CCode.Text)
                    {
                        ch = txt_C_Name.Text[0];
                        string upper4 = ch.ToString().ToUpper();
                        ch = txt_C_Surname.Text[0];
                        string upper5 = ch.ToString().ToUpper();
                        ch = txt_C_Surname.Text[1];
                        string upper6 = ch.ToString().ToUpper();
                        ch = txt_C_Surname.Text[2];
                        string upper7 = ch.ToString().ToUpper();
                        txt_C_CCode.Text = "QTC_" + (upper4 + upper5 + upper6 + upper7);
                        break;
                    }
                }
                else if (row["Contractor_Code"].ToString().Trim() == txt_C_CCode.Text)
                {
                    ch = txt_C_Name.Text[0];
                    string upper4 = ch.ToString().ToUpper();
                    ch = txt_C_Surname.Text[0];
                    string upper5 = ch.ToString().ToUpper();
                    ch = txt_C_Surname.Text[1];
                    string upper6 = ch.ToString().ToUpper();
                    ch = txt_C_Surname.Text[2];
                    string upper7 = ch.ToString().ToUpper();
                    txt_C_CCode.Text = "QTC_" + (upper4 + upper5 + upper6 + upper7);
                    break;
                }
            }
        }

        private void Txt_C_EVN_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void Txt_C_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void Txt_C_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void Txt_C_Surname_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void Txt_C_EName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void Btn_C_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Prev.Image = Resources.back_white;
        }

        private void Btn_C_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Prev.Image = Resources.back_black;
        }

        private void Btn_C_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Next.Image = Resources.forward_white;
        }

        private void Btn_C_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Next.Image = Resources.forawrd_black;
        }

        private void Btn_C_SelCon_MouseEnter(object sender, EventArgs e)
        {
            btn_C_SelCon.Image = Resources.client_list_white;
            btn_C_SelCon.ForeColor = Color.White;
        }

        private void Btn_C_SelCon_MouseLeave(object sender, EventArgs e)
        {
            btn_C_SelCon.Image = Resources.user_list;
            btn_C_SelCon.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_C_NewWW_MouseEnter(object sender, EventArgs e)
        {
            btn_C_NewWW.Image = Resources.add_white;
            btn_C_NewWW.ForeColor = Color.White;
        }

        private void Btn_C_NewWW_MouseLeave(object sender, EventArgs e)
        {
            btn_C_NewWW.Image = Resources.add_grey;
            btn_C_NewWW.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_C_Filter_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Filter.Image = Resources.filter_white;
            btn_C_Filter.ForeColor = Color.White;
        }

        private void Btn_C_Filter_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Filter.Image = Resources.filter_grey;
            btn_C_Filter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_C_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_C_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_C_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_C_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_C_Add_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Add.ForeColor = Color.White;
            btn_C_Add.Image = Resources.add_white;
        }

        private void Btn_C_Add_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Add.ForeColor = Color.FromArgb(64, 64, 64);
            btn_C_Add.Image = Resources.add_grey;
        }

        private void Btn_C_Edit_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Edit.ForeColor = Color.White;
            btn_C_Edit.Image = Resources.edit_white;
        }

        private void Btn_C_Edit_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Edit.ForeColor = Color.FromArgb(64, 64, 64);
            btn_C_Edit.Image = Resources.edit_grey;
        }

        private void Btn_C_DoneAdd_MouseEnter(object sender, EventArgs e)
        {
            btn_C_DoneAdd.ForeColor = Color.White;
        }

        private void Btn_C_DoneAdd_MouseLeave(object sender, EventArgs e)
        {
            btn_C_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_C_DoneEdit_MouseEnter(object sender, EventArgs e)
        {
            btn_C_DoneEdit.ForeColor = Color.White;
        }

        private void Btn_C_DoneEdit_MouseLeave(object sender, EventArgs e)
        {
            btn_C_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_C_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Cancel.ForeColor = Color.White;
        }

        private void Btn_C_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }
    }
}
