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
    public partial class Proj_AddExp : Form
    {
        private BindingSource bs = new BindingSource();
        private DataTable dt;
        private string Proj_ID;
        private Home frmHome;
        private bool isFiltered;

        public Proj_AddExp()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // PROJECT ADD EXPENSE FORM LOAD                                                                                                                  //
        //================================================================================================================================================//
        private void Proj_AddExp_Load(object sender, EventArgs e)
        {
            dtp_PAE_From.Value = DateTime.Now;
            dtp_PAE_To.Value = DateTime.Now;

            dgv_ProjAddExp.DataSource = bs;
            Proj_ID = ((Projects)frmHome.GetCurForm()).GetProjID();
            LoadExpenses();
        }


        //================================================================================================================================================//
        // LOAD EXPENSES                                                                                                                                  //
        //================================================================================================================================================//
        private void LoadExpenses()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT ID, Description, Travel, Accomodation, Subsistence, Tools, Programming_Hours, Install_Hours, "
                    + "Date, User_Log FROM Project_Expenses WHERE Project_ID = '" + Proj_ID + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            decimal totRand = 0.00m;
            decimal totDol = 0.00m;
            decimal totHours = 0.00m;

            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                if (row["Travel"].ToString() != "")
                {
                    if (row["Travel"].ToString().Contains("R"))
                        totRand += Convert.ToDecimal(row["Travel"].ToString().Remove(0, 1));
                    else if (row["Travel"].ToString().Contains("$"))
                        totDol += Convert.ToDecimal(row["Travel"].ToString().Remove(0, 1));
                }

                if (row["Accomodation"].ToString() != "")
                {
                    if (row["Accomodation"].ToString().Contains("R"))
                        totRand += Convert.ToDecimal(row["Accomodation"].ToString().Remove(0, 1));
                    else if (row["Accomodation"].ToString().Contains("$"))
                        totDol += Convert.ToDecimal(row["Accomodation"].ToString().Remove(0, 1));
                }

                if (row["Subsistence"].ToString() != "")
                {
                    if (row["Subsistence"].ToString().Contains("R"))
                        totRand += Convert.ToDecimal(row["Subsistence"].ToString().Remove(0, 1));
                    else if (row["Subsistence"].ToString().Contains("$"))
                        totDol += Convert.ToDecimal(row["Subsistence"].ToString().Remove(0, 1));
                }

                if (row["Tools"].ToString() != "")
                {
                    if (row["Tools"].ToString().Contains("R"))
                        totRand += Convert.ToDecimal(row["Tools"].ToString().Remove(0, 1));
                    else if (row["Tools"].ToString().Contains("$"))
                        totDol += Convert.ToDecimal(row["Tools"].ToString().Remove(0, 1));
                }

                if (row["Programming_Hours"].ToString() != "")
                    totHours += Convert.ToDecimal(row["Programming_Hours"].ToString());
                if (row["Install_Hours"].ToString() != "")
                    totHours += Convert.ToDecimal(row["Install_Hours"].ToString());
            }

            txt_PAE_TotRand.Text = totRand.ToString("C");
            txt_PAE_TotDol.Text = totDol.ToString("C", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            txt_PAE_TotHours.Text = totHours.ToString();

            bs.DataSource = dt;
        }


        //================================================================================================================================================//
        // GETS ALL EXPENSES                                                                                                                              //
        //================================================================================================================================================//
        public AdvancedDataGridView GetLines()
        {
            return dgv_ProjAddExp;
        }


        //================================================================================================================================================//
        // ADD EXPENSE CLICKED                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_PAE_AddExp_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                removeFilter();

            using (New_Proj_LineOld frmNPL = new New_Proj_LineOld())
            {
                frmNPL.setParent(this);
                frmNPL.ShowDialog();
            }

            LoadExpenses();
        }


        //================================================================================================================================================//
        // GET PROJECT ID                                                                                                                                 //
        //================================================================================================================================================//
        public string GetProjectID()
        {
            return Proj_ID;
        }


        //================================================================================================================================================//
        // SET HOME FORM                                                                                                                                  //
        //================================================================================================================================================//
        public void SetHome(Home frmHome)
        {
            this.frmHome = frmHome;
        }


        //================================================================================================================================================//
        // CLOSE CLICKED                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_PAE_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // REMOVE EXPENSE CLICKED                                                                                                                         //
        //================================================================================================================================================//
        private void Btn_PAE_RemoveExp_Click(object sender, EventArgs e)
        {
            if (dgv_ProjAddExp.SelectedRows[0].Index > -1)
            {
                int index = dgv_ProjAddExp.SelectedRows[0].Index;

                if (MessageBox.Show("Are you sure you want to remove selected line?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection conn = DBUtils.GetDBConnection())
                    {
                        conn.Open();
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM Project_Expenses WHERE ID = '" + dt.Rows[index]["ID"].ToString() + "'", conn))
                                cmd.ExecuteNonQuery();

                            MessageBox.Show("Expense successfully removed.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadExpenses();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }               
            }
            else
            {
                MessageBox.Show("Please select a line to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }


        //================================================================================================================================================//
        // ADD EXPENSE BUTTON                                                                                                                             //
        //================================================================================================================================================//
        private void Btn_PAE_AddExp_MouseEnter(object sender, EventArgs e)
        {
            btn_PAE_AddExp.Image = Resources.add_white;
            btn_PAE_AddExp.ForeColor = Color.White;
        }

        private void Btn_PAE_AddExp_MouseLeave(object sender, EventArgs e)
        {
            btn_PAE_AddExp.Image = Resources.add_grey;
            btn_PAE_AddExp.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // FILTER BUTTON                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_PAE_Filter_MouseEnter(object sender, EventArgs e)
        {
            btn_PAE_Filter.Image = Resources.filter_white;
            btn_PAE_Filter.ForeColor = Color.White;
        }

        private void Btn_PAE_Filter_MouseLeave(object sender, EventArgs e)
        {
            btn_PAE_Filter.Image = Resources.filter_grey;
            btn_PAE_Filter.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CLEAR FILTER BUTTON                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_PAE_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_PAE_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_PAE_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_PAE_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_PAE_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_PAE_Close.ForeColor = Color.White;
        }

        private void Btn_PAE_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_PAE_Close.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // REMOVE LINE BUTTON                                                                                                                             //
        //================================================================================================================================================//
        private void Btn_PAE_RemoveLine_MouseEnter(object sender, EventArgs e)
        {
            btn_PAE_RemoveLine.ForeColor = Color.White;
        }

        private void Btn_PAE_RemoveLine_MouseLeave(object sender, EventArgs e)
        {
            btn_PAE_RemoveLine.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // FILTERS                                                                                                                                        //
        //================================================================================================================================================//
        private void dgv_ProjAddExp_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_ProjAddExp.FilterString;
        }

        private void dgv_ProjAddExp_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_ProjAddExp.SortString;
        }

        private void Btn_PAE_Filter_Click(object sender, EventArgs e)
        {
            bs.Filter = string.Empty;
            bs.Sort = string.Empty;
            isFiltered = true;

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Projects WHERE Date BETWEEN '" + dtp_PAE_From.Value + "' AND '" + dtp_PAE_To.Value + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            bs.DataSource = dt;
            btn_PAE_Filter.Visible = false;
            btn_PAE_ClearFilter.Visible = true;
        }

        private void Btn_PAE_ClearFilter_Click(object sender, EventArgs e)
        {
            removeFilter();
        }

        private void removeFilter()
        {
            LoadExpenses();
            btn_PAE_Filter.Visible = true;
            btn_PAE_ClearFilter.Visible = false;
        }
    }
}
