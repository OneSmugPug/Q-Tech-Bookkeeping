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
        private int NUM_OF_CLIENTS;
        private int SELECTED_ORDER;
        private string CNAME;
        private DataTable clientsDT;
        private DataTable dt;

        public Orders()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // ORDERS FORM LOAD                                                                                                                               //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // LOAD CLIENT DETAILS                                                                                                                            //
        //================================================================================================================================================//
        private void LoadClients()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Clients", conn);
                clientsDT = new DataTable();
                da.Fill(clientsDT);
            }

            if (clientsDT.Rows.Count > 0)
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


        //================================================================================================================================================//
        // LOAD ORDER DETAILS                                                                                                                             //
        //================================================================================================================================================//
        private void LoadOrders()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + CNAME + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
            }
            bs.DataSource = dt;
        }


        //================================================================================================================================================//
        //NEXT CLICKED                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_Order_CNext_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT + 1 < NUM_OF_CLIENTS)
            {
                CUR_CLIENT++;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            else if (CUR_CLIENT + 1 == NUM_OF_CLIENTS)
            {
                btn_LO_Next.Enabled = false;
                CUR_CLIENT++;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            if (CUR_CLIENT != 0 && !btn_LO_Prev.Enabled)
                btn_LO_Prev.Enabled = true;
        }


        //================================================================================================================================================//
        // PREVIOUS CLICKED                                                                                                                               //
        //================================================================================================================================================//
        private void Btn_Order_CPrev_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT - 1 > 0)
            {
                CUR_CLIENT--;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            else if (CUR_CLIENT - 1 == 0)
            {
                btn_LO_Prev.Enabled = false;
                CUR_CLIENT--;
                txt_LO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_LO_CName.Text = CNAME;
                LoadOrders();
            }
            if (CUR_CLIENT != NUM_OF_CLIENTS && !btn_LO_Next.Enabled)
                btn_LO_Next.Enabled = true;
        }


        //================================================================================================================================================//
        // CLIENT SELECT CLICKED                                                                                                                          //
        //================================================================================================================================================//
        private void Btn_Order_CBrowse_Click(object sender, EventArgs e)
        {
            using (ClientList frmCList = new ClientList())
                frmCList.ShowDialog(this);
        }


        //================================================================================================================================================//
        // SET NEW CLIENT                                                                                                                                 //
        //================================================================================================================================================//
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

            if (CUR_CLIENT == NUM_OF_CLIENTS && btn_LO_Next.Enabled)
                btn_LO_Next.Enabled = false;
        }

        //================================================================================================================================================//
        // NEW ORDER ADD CLICKED                                                                                                                          //
        //================================================================================================================================================//
        private void Btn_AddOrder_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            using (O_Add frmOA = new O_Add())
                frmOA.ShowDialog(this);

            LoadOrders();
        }


        //================================================================================================================================================//
        // GETTERS                                                                                                                                        //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // FILTERS                                                                                                                                        //
        //================================================================================================================================================//
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

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + CNAME + "' AND Date BETWEEN '" + dtp_LO_From.Value + "' AND '" + dtp_LO_To.Value + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
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

            using (O_Edit_Del frmOED = new O_Edit_Del())
                frmOED.ShowDialog(this);

            LoadOrders();
        }


        //================================================================================================================================================//
        //  PREVIOUS BUTTON                                                                                                                               //
        //================================================================================================================================================//
        private void Btn_LO_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_Prev.Image = Resources.back_white;
        }

        private void Btn_LO_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_Prev.Image = Resources.back_black;
        }


        //================================================================================================================================================//
        // NEXT BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_LO_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_Next.Image = Resources.forward_white;
        }

        private void Btn_LO_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_Next.Image = Resources.forawrd_black;
        }


        //================================================================================================================================================//
        // SELECT CLIENT BUTTON                                                                                                                           //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // NEW ORDER ADD BUTTON                                                                                                                           //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // FILTER BUTTON                                                                                                                                  //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // CLEAR FILTER BUTTON                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_LO_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_LO_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_LO_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_LO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ENFORCE READONLY                                                                                                                               //
        //================================================================================================================================================//
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
