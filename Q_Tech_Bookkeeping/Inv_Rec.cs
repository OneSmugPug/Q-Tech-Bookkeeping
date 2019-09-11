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
        private IContainer components = (IContainer)null;
        private int SELECTED_INVOICE;
        private DataTable dt;

        public Inv_Rec()
        {
            InitializeComponent();
        }

        private void Inv_Rec_Load(object sender, EventArgs e)
        {
            dgv_LInvRec.DataSource = bs;
            LoadInvRec();
            dgv_LInvRec.Columns[4].DefaultCellStyle.Format = "c";
            dgv_LInvRec.Columns[5].DefaultCellStyle.Format = "c";
            dgv_LInvRec.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_LInvRec.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadInvRec()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Received", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
        }

        private void Btn_LIR_NewIR_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            using (Inv_Rec_Add invRecAdd = new Inv_Rec_Add())
            {
                int num = (int)invRecAdd.ShowDialog((IWin32Window)this);
            }
            LoadInvRec();
        }

        public int GetSelectedInv()
        {
            return SELECTED_INVOICE;
        }

        public DataTable GetInvRec()
        {
            return dt;
        }

        private void Dgv_LInvRec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            SELECTED_INVOICE = e.RowIndex;
            using (Inv_Rec_Edit_Del invRecEditDel = new Inv_Rec_Edit_Del())
            {
                int num = (int)invRecEditDel.ShowDialog((IWin32Window)this);
            }
            LoadInvRec();
        }

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
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Received WHERE Date BETWEEN '" + dtp_LIR_From.Value + "' AND '" + dtp_LIR_To.Value + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
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
