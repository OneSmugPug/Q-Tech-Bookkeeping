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
    public partial class Inv_Rec : Form
    {
        private BindingSource bs = new BindingSource();
        private bool isFiltered = false;
        private int SELECTED_INVOICE;
        private DataTable dt;

        public Inv_Rec()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // INVOICES RECIEVED FORM LOAD                                                                                                                    //
        //=============================================================================================================================================
        private void Inv_Rec_Load(object sender, EventArgs e)
        {
            dgv_LInvRec.DataSource = bs;

            LoadInvRec();

            dgv_LInvRec.Columns[4].DefaultCellStyle.Format = "c";
            dgv_LInvRec.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_LInvRec.Columns[5].DefaultCellStyle.Format = "c";            
            dgv_LInvRec.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


        //================================================================================================================================================//
        // LOAD INVOICE DETAILS                                                                                                                           //
        //================================================================================================================================================//
        private void LoadInvRec()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Invoices_Received", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            bs.DataSource = dt;
        }


        //================================================================================================================================================//
        // ADD NEW INVOICE                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_LIR_NewIR_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            using (Inv_Rec_Add frmIRAdd = new Inv_Rec_Add())
                frmIRAdd.ShowDialog(this);

            LoadInvRec();
        }


        //================================================================================================================================================//
        // GETTERS                                                                                                                               //
        //================================================================================================================================================//
        public int GetSelectedInv()
        {
            return SELECTED_INVOICE;
        }

        public DataTable GetInvRec()
        {
            return dt;
        }


        //================================================================================================================================================//
        // EDIT/DELETE INVOICE                                                                                                                            //
        //================================================================================================================================================//
        private void Dgv_LInvRec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            SELECTED_INVOICE = e.RowIndex;

            using (Inv_Rec_Edit_Del frmIRED = new Inv_Rec_Edit_Del())
                frmIRED.ShowDialog(this);

            LoadInvRec();
        }


        //================================================================================================================================================//
        // FILTERS                                                                                                                          //
        //================================================================================================================================================//
        private void Dgv_LInvRec_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_LInvRec.FilterString;
        }

        private void Dgv_LInvRec_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_LInvRec.SortString;
        }

        private void Btn_LIR_Filter_Click(object sender, EventArgs e)
        {
            bs.Filter = string.Empty;
            bs.Sort = string.Empty;

            isFiltered = true;

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Invoices_Received WHERE Date BETWEEN '" + dtp_LIR_From.Value + "' AND '" + dtp_LIR_To.Value + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            bs.DataSource = dt;
            btn_LIR_Filter.Visible = false;
            btn_LIR_ClearFilter.Visible = true;
        }

        private void Btn_LIR_ClearFilter_Click(object sender, EventArgs e)
        {
            RemoveFilter();
        }

        private void RemoveFilter()
        {
            LoadInvRec();
            btn_LIR_Filter.Visible = true;
            btn_LIR_ClearFilter.Visible = false;
        }


        //================================================================================================================================================//
        //  NEW INVOICE RECEIVED BUTTON                                                                                                                                       //
        //================================================================================================================================================//
        private void Btn_LIR_NewIR_MouseEnter(object sender, EventArgs e)
        {
            btn_LIR_NewIR.Image = Resources.add_white;
            btn_LIR_NewIR.ForeColor = Color.White;
        }

        private void Btn_LIR_NewIR_MouseLeave(object sender, EventArgs e)
        {
            btn_LIR_NewIR.Image = Resources.add_grey;
            btn_LIR_NewIR.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // FILTER BUTTON                                                                                                                         //
        //================================================================================================================================================//
        private void Btn_LIR_Filter_MouseEnter(object sender, EventArgs e)
        {
            btn_LIR_Filter.Image = Resources.filter_white;
            btn_LIR_Filter.ForeColor = Color.White;
        }

        private void Btn_LIR_Filter_MouseLeave(object sender, EventArgs e)
        {
            btn_LIR_Filter.Image = Resources.filter_grey;
            btn_LIR_Filter.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CLEAR FILTER BUTTON                                                                                                                          //
        //================================================================================================================================================//
        private void Btn_LIR_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_LIR_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_LIR_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_LIR_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }
    }
}
