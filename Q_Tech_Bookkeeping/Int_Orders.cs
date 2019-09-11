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
    public partial class Int_Orders : Form
    {
        private int CUR_CLIENT = 0;
        private BindingSource bs = new BindingSource();
        private bool isFiltered = false;
        private IContainer components = (IContainer)null;
        private int NUM_OF_CLIENTS;
        private int SELECTED_ORDER;
        private string CNAME;
        private DataTable clientsDT;
        private DataTable dt;

        public Int_Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            clientsDT = new DataTable();
            dt = new DataTable();
            dt.Columns.Add(string.Empty);
            dt.Rows.Add();
            bs.DataSource = dt;
            dgv_IOrders.DataSource = bs;
            LoadClients();
            LoadOrders();
            dgv_IOrders.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_IOrders.Columns[4].DefaultCellStyle.Format = "c";
            dgv_IOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_IOrders.Columns[5].DefaultCellStyle.Format = "p0";
            dgv_IOrders.Columns[6].DefaultCellStyle.Format = "p0";
        }

        private void LoadClients()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Int_Clients", dbConnection);
                clientsDT = new DataTable();
                sqlDataAdapter.Fill(clientsDT);
            }
            if ((uint)clientsDT.Rows.Count > 0U)
            {
                if (!btn_IO_SelCli.Enabled)
                    btn_IO_SelCli.Enabled = true;
                if (!dgv_IOrders.Enabled)
                    dgv_IOrders.Enabled = true;
                if (!btn_IO_NewOrder.Enabled)
                    btn_IO_NewOrder.Enabled = true;
                NUM_OF_CLIENTS = clientsDT.Rows.Count - 1;
                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IO_CName.Text = CNAME;
            }
            else
            {
                btn_IO_SelCli.Enabled = false;
                dgv_IOrders.Enabled = false;
                btn_IO_NewOrder.Enabled = false;
            }
        }

        private void LoadOrders()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + CNAME + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
        }

        private void Btn_IO_Next_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT + 1 < NUM_OF_CLIENTS)
            {
                ++CUR_CLIENT;
                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IO_CName.Text = CNAME;
                LoadOrders();
            }
            else if (CUR_CLIENT + 1 == NUM_OF_CLIENTS)
            {
                btn_IO_Next.Enabled = false;
                ++CUR_CLIENT;
                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_IO_CName.Text = CNAME;
                LoadOrders();
            }
            if (CUR_CLIENT == 0 || btn_IO_Prev.Enabled)
                return;
            btn_IO_Prev.Enabled = true;
        }

        private void Btn_IO_Prev_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT - 1 > 0)
            {
                --CUR_CLIENT;
                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IO_CName.Text = CNAME;
                LoadOrders();
            }
            else if (CUR_CLIENT - 1 == 0)
            {
                btn_IO_Prev.Enabled = false;
                --CUR_CLIENT;
                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_IO_CName.Text = CNAME;
                LoadOrders();
            }
            if (CUR_CLIENT == NUM_OF_CLIENTS || btn_IO_Next.Enabled)
                return;
            btn_IO_Next.Enabled = true;
        }

        private void Btn_IO_SelCli_Click(object sender, EventArgs e)
        {
            using (Client_listOld clientList = new Client_listOld())
            {
                int num = (int)clientList.ShowDialog((IWin32Window)this);
            }
        }

        public void SetNewClient(int rowIdx)
        {
            CUR_CLIENT = rowIdx;
            LoadClients();
            LoadOrders();
            if (CUR_CLIENT != 0 && !btn_IO_Prev.Enabled)
                btn_IO_Prev.Enabled = true;
            if (CUR_CLIENT == 0 && btn_IO_Prev.Enabled)
                btn_IO_Prev.Enabled = false;
            if (CUR_CLIENT != NUM_OF_CLIENTS && !btn_IO_Next.Enabled)
                btn_IO_Next.Enabled = true;
            if (CUR_CLIENT != NUM_OF_CLIENTS || !btn_IO_Next.Enabled)
                return;
            btn_IO_Next.Enabled = false;
        }

        private void Btn_IO_NewOrder_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            using (O_Add oAdd = new O_Add())
            {
                int num = (int)oAdd.ShowDialog((IWin32Window)this);
            }
            LoadOrders();
        }

        public string GetCCode()
        {
            return txt_IO_CCode.Text;
        }

        public string GetCName()
        {
            return CNAME;
        }

        public int GetSelectedOrder()
        {
            return SELECTED_ORDER;
        }

        public DataTable GetOrders()
        {
            return dt;
        }

        private void Dgv_IOrders_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_IOrders.FilterString;
        }

        private void Dgv_IOrders_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_IOrders.SortString;
        }

        private void Btn_IO_Filter_Click(object sender, EventArgs e)
        {
            bs.Filter = string.Empty;
            bs.Sort = string.Empty;
            isFiltered = true;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + CNAME + "' AND Date BETWEEN '" + btn_IO_ClearFilter.Value + "' AND '" + dtp_IO_To.Value + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
            btn_IO_Filter.Visible = false;
            btn_IO_ClearFilter.Visible = true;
        }

        private void Btn_IO_ClearFilter_Click(object sender, EventArgs e)
        {
            RemoveFilter();
        }

        private void RemoveFilter()
        {
            LoadOrders();
            btn_IO_Filter.Visible = true;
            btn_IO_ClearFilter.Visible = false;
        }

        private void Dgv_IOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            SELECTED_ORDER = e.RowIndex;
            using (O_Edit_Del oEditDel = new O_Edit_Del())
            {
                int num = (int)oEditDel.ShowDialog((IWin32Window)this);
            }
            LoadOrders();
        }

        private void Btn_IO_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_Prev.Image = Resources.back_white;
        }

        private void Btn_IO_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_Prev.Image = Resources.back_black;
        }

        private void Btn_IO_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_Next.Image = Resources.forward_white;
        }

        private void Btn_IO_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_Next.Image = Resources.forawrd_black;
        }

        private void Btn_IO_SelCli_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_SelCli.Image = Resources.client_list_white;
            btn_IO_SelCli.ForeColor = Color.White;
        }

        private void Btn_IO_SelCli_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_SelCli.Image = Resources.user_list;
            btn_IO_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IO_NewOrder_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_NewOrder.Image = Resources.add_white;
            btn_IO_NewOrder.ForeColor = Color.White;
        }

        private void Btn_IO_NewOrder_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_NewOrder.Image = Resources.add_grey;
            btn_IO_NewOrder.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IO_Filter_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_Filter.Image = Resources.filter_white;
            btn_IO_Filter.ForeColor = Color.White;
        }

        private void Btn_IO_Filter_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_Filter.Image = Resources.filter_grey;
            btn_IO_Filter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IO_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_IO_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Txt_IO_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_IO_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
