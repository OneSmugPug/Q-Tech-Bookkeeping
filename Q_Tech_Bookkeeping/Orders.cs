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
    public partial class Orders : Form
    {
        private int CUR_CLIENT = 0;
        private BindingSource bs = new BindingSource();
        private bool isFiltered = false;
        //private IContainer components = (IContainer)null;
        private int NUM_OF_CLIENTS;
        private int SELECTED_ORDER;
        private string CNAME;
        private DataTable clientsDT;
        private DataTable dt;
        //private BunifuDatepicker dtp_LO_From;
        //private AdvancedDataGridView dgv_LOrders;
        //private Button btn_LO_NewOrder;
        //private Button btn_LO_Filter;
        //private BunifuDatepicker dtp_LO_To;
        //private BunifuCustomLabel bunifuCustomLabel6;
        //private BunifuCustomLabel bunifuCustomLabel5;
        //private Button btn_LO_SelCli;
        //private BunifuSeparator bunifuSeparator2;
        //private BunifuMaterialTextbox txt_LO_CName;
        //private BunifuMaterialTextbox txt_LO_CCode;
        //private Button btn_LO_Next;
        //private BunifuCustomLabel bunifuCustomLabel3;
        //private BunifuCustomLabel bunifuCustomLabel4;
        //private Button btn_LO_Prev;
        //private Button btn_LO_ClearFilter;

        public Orders()
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
            dgv_LOrders.DataSource = bs;
            LoadClients();
            LoadOrders();
            dgv_LOrders.Columns[4].DefaultCellStyle.Format = "c";
            dgv_LOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_LOrders.Columns[5].DefaultCellStyle.Format = "p0";
            dgv_LOrders.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_LOrders.Columns[6].DefaultCellStyle.Format = "p0";
            dgv_LOrders.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadClients()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Clients", dbConnection);
                clientsDT = new DataTable();
                sqlDataAdapter.Fill(clientsDT);
            }
            if ((uint)clientsDT.Rows.Count > 0U)
            {
                if (!btn_LO_SelCli.Enabled)
                    btn_LO_SelCli.Enabled = true;
                if (!dgv_LOrders.Enabled)
                    dgv_LOrders.Enabled = true;
                if (!btn_LO_NewOrder.Enabled)
                    btn_LO_NewOrder.Enabled = true;
                NUM_OF_CLIENTS = clientsDT.Rows.Count - 1;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_LO_CName.Text = CNAME;
            }
            else
            {
                btn_LO_SelCli.Enabled = false;
                dgv_LOrders.Enabled = false;
                btn_LO_NewOrder.Enabled = false;
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

        private void Btn_Order_CNext_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT + 1 < NUM_OF_CLIENTS)
            {
                ++CUR_CLIENT;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            else if (CUR_CLIENT + 1 == NUM_OF_CLIENTS)
            {
                btn_LO_Next.Enabled = false;
                ++CUR_CLIENT;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            if (CUR_CLIENT == 0 || btn_LO_Prev.Enabled)
                return;
            btn_LO_Prev.Enabled = true;
        }

        private void Btn_Order_CPrev_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT - 1 > 0)
            {
                --CUR_CLIENT;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            else if (CUR_CLIENT - 1 == 0)
            {
                btn_LO_Prev.Enabled = false;
                --CUR_CLIENT;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            if (CUR_CLIENT == NUM_OF_CLIENTS || btn_LO_Next.Enabled)
                return;
            btn_LO_Next.Enabled = true;
        }

        private void Btn_Order_CBrowse_Click(object sender, EventArgs e)
        {
            int num = (int)new ClientList().ShowDialog((IWin32Window)this);
        }

        public void SetNewClient(int rowIdx)
        {
            CUR_CLIENT = rowIdx;
            LoadClients();
            LoadOrders();
            if (CUR_CLIENT != 0 && !btn_LO_Prev.Enabled)
                btn_LO_Prev.Enabled = true;
            if (CUR_CLIENT == 0 && btn_LO_Prev.Enabled)
                btn_LO_Prev.Enabled = false;
            if (CUR_CLIENT != NUM_OF_CLIENTS && !btn_LO_Next.Enabled)
                btn_LO_Next.Enabled = true;
            if (CUR_CLIENT != NUM_OF_CLIENTS || !btn_LO_Next.Enabled)
                return;
            btn_LO_Next.Enabled = false;
        }

        private void Tsb_AddOrder_Click(object sender, EventArgs e)
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
            return txt_LO_CCode.Text;
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

        private void Dgv_Order_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_LOrders.FilterString;
        }

        private void Dgv_Order_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_LOrders.SortString;
        }

        private void Btn_O_FilterD_Click(object sender, EventArgs e)
        {
            bs.Filter = string.Empty;
            bs.Sort = string.Empty;
            isFiltered = true;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + CNAME + "' AND Date BETWEEN '" + dtp_LO_From.Value + "' AND '" + dtp_LO_To.Value + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
            btn_LO_Filter.Visible = false;
            btn_LO_ClearFilter.Visible = true;
        }

        private void Btn_O_ClearF_Click(object sender, EventArgs e)
        {
            RemoveFilter();
        }

        private void RemoveFilter()
        {
            LoadOrders();
            btn_LO_Filter.Visible = true;
            btn_LO_ClearFilter.Visible = false;
        }

        private void Dgv_Order_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void Btn_LO_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_Prev.Image = Resources.back_white;
        }

        private void Btn_LO_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_Prev.Image = Resources.back_black;
        }

        private void Btn_LO_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_Next.Image = Resources.forward_white;
        }

        private void Btn_LO_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_Next.Image = Resources.forawrd_black;
        }

        private void Btn_LO_SelCli_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_SelCli.Image = Resources.client_list_white;
            btn_LO_SelCli.ForeColor = Color.White;
        }

        private void Btn_LO_SelCli_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_SelCli.Image = Resources.user_list;
            btn_LO_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_LO_NewOrder_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_NewOrder.Image = Resources.add_white;
            btn_LO_NewOrder.ForeColor = Color.White;
        }

        private void Btn_LO_NewOrder_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_NewOrder.Image = Resources.add_grey;
            btn_LO_NewOrder.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_LO_Filter_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_Filter.Image = Resources.filter_white;
            btn_LO_Filter.ForeColor = Color.White;
        }

        private void Btn_LO_Filter_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_Filter.Image = Resources.filter_grey;
            btn_LO_Filter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_LO_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_LO_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Txt_LO_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_LO_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
