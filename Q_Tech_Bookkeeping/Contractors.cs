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

            dgv_Contractors.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("en-US");
            dgv_Contractors.Columns[4].DefaultCellStyle.Format = "c";
            dgv_Contractors.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_Contractors.Columns[5].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("en-US");
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


        //================================================================================================================================================//
        // FILL TEXTBOXES                                                                                                                                 //
        //================================================================================================================================================//
        private void FillTextFields()
        {
            if (conDT.Rows.Count > 0)
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


        //================================================================================================================================================//
        // LOAD CONTRACTOR DETAILS                                                                                                                        //
        //================================================================================================================================================//
        private void LoadCon()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter conDA = new SqlDataAdapter("SELECT * FROM Contractors", conn);
                conDA.Fill(conDT);
            }

            NUM_OF_CON = conDT.Rows.Count - 1;

            if (NUM_OF_CON == 0)
                btn_C_Next.Enabled = false;
            else if (NUM_OF_CON != 0 && !btn_C_Next.Enabled)
                btn_C_Next.Enabled = true;

            FillTextFields();
        }

        //================================================================================================================================================//
        // LOAD HOURS OF CONTRACTOR                                                                                                                       //
        //================================================================================================================================================//
        private void LoadHours()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT Code, Date_Start, Date_End, Hours, Rate_Per_Hour, Total_$, Exchange_Rate, Total_R, " 
                    + "QTech_Cut, Final_Total, Remittance, Invoice_Received, Paid, Date_Paid FROM Contractor_Hours WHERE Contractor_Code = '" + CCODE + "'", conn);

                dt = new DataTable();
                da.Fill(dt);
            }

            decimal netTot = new Decimal();
            decimal totHours = new Decimal();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Final_Total"].ToString() != "")
                    netTot += Convert.ToDecimal(dr["Final_Total"].ToString());
                else netTot += Decimal.Zero;
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Hours"].ToString() != "")
                    totHours += Convert.ToDecimal(dr["Hours"].ToString());
                else totHours += Decimal.Zero;
            }

            txt_C_TotPaid.Text = netTot.ToString("c");
            txt_C_TotHours.Text = totHours.ToString();

            bs.DataSource = dt;
        }


        //================================================================================================================================================//
        // NEXT CONTRACTOR                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_C_Next_Click(object sender, EventArgs e)
        {
            if (CUR_CON + 1 < NUM_OF_CON)
            {
                CUR_CON++;

                FillTextFields();

                LoadHours();
            }
            else if (CUR_CON + 1 == NUM_OF_CON)
            {
                btn_C_Next.Enabled = false;

                CUR_CON++;
                FillTextFields();
                LoadHours();
            }
            if (CUR_CON != 0 && !btn_C_Prev.Enabled)
                btn_C_Prev.Enabled = true;
        }


        //================================================================================================================================================//
        // PREVIOUS CONTRACTOR                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_C_Prev_Click(object sender, EventArgs e)
        {
            if (CUR_CON - 1 > 0)
            {
                CUR_CON--;
                FillTextFields();
                LoadHours();
            }
            else if (CUR_CON - 1 == 0)
            {
                btn_C_Prev.Enabled = false;
                CUR_CON--;
                FillTextFields();
                LoadHours();
            }
            if (CUR_CON != NUM_OF_CON && !btn_C_Next.Enabled)
                btn_C_Next.Enabled = true;
        }


        //================================================================================================================================================//
        // BROWSE LIST OF CONTRACTORS                                                                                                                     //
        //================================================================================================================================================//
        private void Btn_C_SelCon_Click(object sender, EventArgs e)
        {
            using (Con_List frmConList = new Con_List())
                frmConList.ShowDialog(this);
        }


        //================================================================================================================================================//
        // SETTERS                                                                                                                                        //
        //================================================================================================================================================//
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

            if (CUR_CON == NUM_OF_CON && btn_C_Next.Enabled)
                btn_C_Next.Enabled = false;
        }

        public void SetNewWW(string code)
        {
            CODE = code;
        }


        //================================================================================================================================================//
        // GETTERS                                                                                                                                        //
        //================================================================================================================================================//
        public string GetCCode() { return CCODE; }

        public string GetCName() { return txt_C_Name.Text; }

        public string GetCSurname() { return txt_C_Surname.Text; }

        public string GetEName() { return txt_C_EName.Text; }

        public int GetSelectedHour() { return SELECTED_HOUR; }

        public DataTable GetHours() { return dt; }

        public object GetSender() { return send; }


        //================================================================================================================================================//
        // SET FIELDS READ ONLY                                                                                                                           //
        //================================================================================================================================================//
        private void SetFieldsReadOnly() { isReadOnly = true; }

        private void SetFieldsNotReadOnly() { isReadOnly = false; }


        //================================================================================================================================================//
        // CLEAR FIELDS                                                                                                                                   //
        //================================================================================================================================================//
        private void ClearFields()
        {
            txt_C_Name.Text = string.Empty;
            txt_C_Surname.Text = string.Empty;
            txt_C_EName.Text = "N/A";
            txt_C_EVN.Text = "N/A";
        }


        //================================================================================================================================================//
        // TOGGLE BUTTONS                                                                                                                                 //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // CANCEL BUTTON CLICKED                                                                                                                          //
        //================================================================================================================================================//
        private void Btn_C_Cancel_Click(object sender, EventArgs e)
        {
            SetFieldsReadOnly();
            ShowButtons();

            btn_C_DoneAdd.Visible = false;
            btn_C_DoneEdit.Visible = false;

            LoadCon();
            LoadHours();
        }


        //================================================================================================================================================//
        // EDIT CONTRACTOR DETAILS                                                                                                                        //
        //================================================================================================================================================//
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
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Contractors SET Contractor_Code = @CCode, Name = @Name, Surname = @Surname, Employer_Name = @EName, Eployer_VAT_Number = @EVN WHERE Contractor_Code = @Code", conn))
                        {
                            cmd.Parameters.AddWithValue("@CCode", txt_C_CCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@Name", txt_C_Name.Text.Trim());
                            cmd.Parameters.AddWithValue("@Surname", txt_C_Surname.Text.Trim());
                            cmd.Parameters.AddWithValue("@EName", txt_C_EName.Text.Trim());
                            cmd.Parameters.AddWithValue("@EVN", txt_C_EVN.Text.Trim());
                            cmd.Parameters.AddWithValue("@Code", txt_C_CCode.Text.Trim());
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Contractor successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LoadCon();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        //================================================================================================================================================//
        // ADD HOURS FOR CONTRACTOR                                                                                                                       //
        //================================================================================================================================================//
        private void Btn_C_NewWW_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            send = sender;

            using (HoursAdd frmHA = new HoursAdd())
                frmHA.ShowDialog(this);

            LoadHours();
        }


        //================================================================================================================================================//
        // EDIT HOURS FOR CONTRACTOR                                                                                                                       //
        //================================================================================================================================================//
        private void Dgv_Contractors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            send = sender;
            SELECTED_HOUR = e.RowIndex;

            using (HoursAdd frmHA = new HoursAdd())
                frmHA.ShowDialog(this);

            LoadHours();
        }


        //================================================================================================================================================//
        // SORT & FILTER                                                                                                                                  //
        //================================================================================================================================================//
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

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Contractor_Hours WHERE Contractor_Code = '" + CCODE + "' AND Date_Start BETWEEN '" 
                    + dtp_C_From.Value + "' AND '" + dtp_C_To.Value + "' OR Date_End BETWEEN '" + dtp_C_From.Value + "' AND '" + dtp_C_To.Value + "'", conn);

                dt = new DataTable();
                da.Fill(dt);
            }

            bs.DataSource = dt;
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


        //================================================================================================================================================//
        // ADD NEW CONTRACTOR                                                                                                                             //
        //================================================================================================================================================//
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
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Contractors VALUES (@CCode, @Name, @Surname, @EName, @EVN)", conn))
                        {
                            sqlCommand.Parameters.AddWithValue("@CCode", txt_C_CCode.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Name", txt_C_Name.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Surname", txt_C_Surname.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@EName", txt_C_EName.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@EVN", txt_C_EVN.Text.Trim());
                            sqlCommand.ExecuteNonQuery();

                            MessageBox.Show("New contractor successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }

                        LoadCon();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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


        //================================================================================================================================================//
        // GENERATE CONTRACTOR CODE                                                                                                                       //
        //================================================================================================================================================//
        private void Txt_C_Name_Leave(object sender, EventArgs e)
        {
            if (txt_C_CCode.Text == string.Empty)
                GenerateCCode();
        }

        private void Txt_C_Surname_Leave(object sender, EventArgs e)
        {
            if (txt_C_CCode.Text == string.Empty)
             GenerateCCode();
        }


        private void GenerateCCode()
        {
            if (txt_C_Name.Text != string.Empty && txt_C_Surname.Text != string.Empty)
            {
                string s = txt_C_Name.Text[0].ToString().ToUpper() + txt_C_Surname.Text[0].ToString().ToUpper()
                    + txt_C_Surname.Text[1].ToString().ToUpper();

                txt_C_CCode.Text = "QTC_" + s;

                foreach (DataRow dr in conDT.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        if (dr["Contractor_Code", DataRowVersion.Original].ToString().Trim() == txt_C_CCode.Text)
                        {
                            s = txt_C_Name.Text[0].ToString().ToUpper() + txt_C_Surname.Text[0].ToString().ToUpper()
                                + txt_C_Surname.Text[1].ToString().ToUpper() + txt_C_Surname.Text[2].ToString().ToUpper();

                            txt_C_CCode.Text = "QTC_" + s;
                            break;
                        }
                    }
                    else if (dr["Contractor_Code"].ToString().Trim() == txt_C_CCode.Text)
                    {
                        s = txt_C_Name.Text[0].ToString().ToUpper() + txt_C_Surname.Text[0].ToString().ToUpper()
                                + txt_C_Surname.Text[1].ToString().ToUpper() + txt_C_Surname.Text[2].ToString().ToUpper();

                        txt_C_CCode.Text = "QTC_" + s;
                        break;
                    }
                }
            }
        }


        //================================================================================================================================================//
        // ENFORCE READ ONLY                                                                                                                              //
        //================================================================================================================================================//
        private void Txt_C_EVN_KeyDown(object sender, KeyEventArgs e)
        {
            if (isReadOnly)
                e.SuppressKeyPress = true;
        }

        private void Txt_C_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (isReadOnly)
                e.SuppressKeyPress = true;
        }

        private void Txt_C_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (isReadOnly)
                e.SuppressKeyPress = true;
        }

        private void Txt_C_Surname_KeyDown(object sender, KeyEventArgs e)
        {
            if (isReadOnly)
                e.SuppressKeyPress = true;
        }

        private void Txt_C_EName_KeyDown(object sender, KeyEventArgs e)
        {
            if (isReadOnly)
                e.SuppressKeyPress = true;
        }


        //================================================================================================================================================//
        // PREVIOUS BUTTON                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_C_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Prev.Image = Resources.back_white;
        }

        private void Btn_C_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Prev.Image = Resources.back_black;
        }


        //================================================================================================================================================//
        // NEXT BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_C_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_C_Next.Image = Resources.forward_white;
        }

        private void Btn_C_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_C_Next.Image = Resources.forawrd_black;
        }


        //================================================================================================================================================//
        // SELECT CODE BUTTON                                                                                                                             //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // NEW WORK WEEK BUTTON                                                                                                                           //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // FILTER BUTTON                                                                                                                                  //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // PREVIOUS BUTTON                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_C_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_C_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_C_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_C_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ADD BUTTON                                                                                                                                     //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // EDIT BUTTON                                                                                                                                    //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // DONE ADD BUTTON                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_C_DoneAdd_MouseEnter(object sender, EventArgs e)
        {
            btn_C_DoneAdd.ForeColor = Color.White;
        }

        private void Btn_C_DoneAdd_MouseLeave(object sender, EventArgs e)
        {
            btn_C_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // DONE EDITING BUTTON                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_C_DoneEdit_MouseEnter(object sender, EventArgs e)
        {
            btn_C_DoneEdit.ForeColor = Color.White;
        }

        private void Btn_C_DoneEdit_MouseLeave(object sender, EventArgs e)
        {
            btn_C_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                                //
        //================================================================================================================================================//
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
