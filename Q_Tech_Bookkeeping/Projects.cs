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
    public partial class Projects : Form
    {
        private BindingSource bs = new BindingSource();
        private bool isFiltered = false;
        private DataTable dt;
        private int SELECTED_PROJECT;

        public Projects()
        {
            InitializeComponent();
        }

        private void Projects_Load(object sender, EventArgs e)
        {
            dgv_Projects.DataSource = bs;
            LoadProjects();
        }

        private void LoadProjects()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Projects", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
        }

        private void Btn_P_NewProject_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            using (Proj_AddOld projAdd = new Proj_AddOld())
            {
                int num = (int)projAdd.ShowDialog((IWin32Window)this);
            }
            LoadProjects();
        }

        public int GetSelectedProj()
        {
            return SELECTED_PROJECT;
        }

        public DataTable GetProjects()
        {
            return dt;
        }

        public string GetProjID()
        {
            return dgv_Projects[0, SELECTED_PROJECT].Value.ToString();
        }

        private void Dgv_Projects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            SELECTED_PROJECT = e.RowIndex;
            using (Proj_DialogOld projDialog = new Proj_DialogOld())
            {
                int num = (int)projDialog.ShowDialog((IWin32Window)this);
            }
            LoadProjects();
        }

        private void Dgv_Projects_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_Projects.FilterString;
        }

        private void Dgv_Projects_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_Projects.SortString;
        }

        private void Btn_P_Filter_Click(object sender, EventArgs e)
        {
            bs.Filter = string.Empty;
            bs.Sort = string.Empty;
            isFiltered = true;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Projects WHERE Date BETWEEN '" + dtp_P_From.Value + "' AND '" + dtp_P_To.Value + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
            btn_P_Filter.Visible = false;
            btn_P_ClearFilter.Visible = true;
        }

        private void Btn_P_ClearFilter_Click(object sender, EventArgs e)
        {
            RemoveFilter();
        }

        private void RemoveFilter()
        {
            LoadProjects();
            btn_P_Filter.Visible = true;
            btn_P_ClearFilter.Visible = false;
        }

        private void Btn_P_NewProject_MouseEnter(object sender, EventArgs e)
        {
            btn_P_NewProject.Image = Resources.add_white;
            btn_P_NewProject.ForeColor = Color.White;
        }

        private void Btn_P_NewProject_MouseLeave(object sender, EventArgs e)
        {
            btn_P_NewProject.Image = Resources.add_grey;
            btn_P_NewProject.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_P_Filter_MouseEnter(object sender, EventArgs e)
        {
            btn_P_Filter.Image = Resources.filter_white;
            btn_P_Filter.ForeColor = Color.White;
        }

        private void Btn_P_Filter_MouseLeave(object sender, EventArgs e)
        {
            btn_P_Filter.Image = Resources.filter_grey;
            btn_P_Filter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_P_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_P_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_P_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_P_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }
    }
}
