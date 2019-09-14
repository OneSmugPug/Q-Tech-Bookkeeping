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
        private int NUM_OF_CLIENTS;
        private int SELECTED_ORDER;
        private string CNAME;
        private DataTable clientsDT;
        private DataTable dt;

        public Int_Orders()
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

            dgv_IOrders.DataSource = bs;

            LoadClients();
            LoadOrders();

            dgv_IOrders.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("en-US");
            dgv_IOrders.Columns[4].DefaultCellStyle.Format = "c";
            dgv_IOrders.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_IOrders.Columns[5].DefaultCellStyle.Format = "p0";

            dgv_IOrders.Columns[6].DefaultCellStyle.Format = "p0";
        }


        //================================================================================================================================================//
        // LOAD CLIENT DETAILS                                                                                                                            //
        //================================================================================================================================================//
        private void LoadClients()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter clientsDA = new SqlDataAdapter("SELECT * FROM Int_Clients", conn);
                clientsDT = new DataTable();
                clientsDA.Fill(clientsDT);
            }

            if (clientsDT.Rows.Count > 0)
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
        // NEXT CLIENT                                                                                                                                    //
        //================================================================================================================================================//
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

                CUR_CLIENT++;

                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();

                txt_IO_CName.Text = CNAME;

                LoadOrders();
            }
            if (CUR_CLIENT != 0 && !btn_IO_Prev.Enabled)
                btn_IO_Prev.Enabled = true;
        }


        //================================================================================================================================================//
        // PREVIOUS CLIENT                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_IO_Prev_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT - 1 > 0)
            {
                CUR_CLIENT--;

                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();

                txt_IO_CName.Text = CNAME;

                LoadOrders();
            }
            else if (CUR_CLIENT - 1 == 0)
            {
                btn_IO_Prev.Enabled = false;
                CUR_CLIENT--;

                txt_IO_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();

                txt_IO_CName.Text = CNAME;

                LoadOrders();
            }
            if (CUR_CLIENT != NUM_OF_CLIENTS && !btn_IO_Next.Enabled)
                btn_IO_Next.Enabled = true;
        }


        //================================================================================================================================================//
        // LOOKUP CLIENT CLICK                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_IO_SelCli_Click(object sender, EventArgs e)
        {
            using (ClientList frmCList = new ClientList())
                frmCList.ShowDialog(this);
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

            if (CUR_CLIENT == NUM_OF_CLIENTS && btn_IO_Next.Enabled)
                btn_IO_Next.Enabled = false;
        }


        //================================================================================================================================================//
        // ADD NEW ORDER                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_IO_NewOrder_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            using (O_Add frmOAdd = new O_Add())
                frmOAdd.ShowDialog(this);

            LoadOrders();
        }


        //================================================================================================================================================//
        // GETTERS                                                                                                                                        //
        //================================================================================================================================================//
        public string GetCCode() { return txt_IO_CCode.Text; }

        public string GetCName() { return CNAME; }

        public int GetSelectedOrder() { return SELECTED_ORDER; }

        public DataTable GetOrders() { return dt; }


        //================================================================================================================================================//
        // FILTERS                                                                                                                                        //
        //================================================================================================================================================//
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

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Orders_Received WHERE Client = '" + CNAME + "' AND Date BETWEEN '" + dtp_IO_From.Value + "' AND '" + dtp_IO_To.Value + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
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


        //================================================================================================================================================//
        // ADD NEW ORDER                                                                                                                                  //
        //================================================================================================================================================//
        private void Dgv_IOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            SELECTED_ORDER = e.RowIndex;

            using (O_Edit_Del frmOED = new O_Edit_Del())
                frmOED.ShowDialog(this);

            LoadOrders();
        }


        //================================================================================================================================================//
        // PREVIOUS BUTTON                                                                                                                                        //
        //================================================================================================================================================//
        private void Btn_IO_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_Prev.Image = Resources.back_white;
        }

        private void Btn_IO_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_Prev.Image = Resources.back_black;
        }


        //================================================================================================================================================//
        // NEXT BUTTON                                                                                                                                       //
        //================================================================================================================================================//
        private void Btn_IO_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_Next.Image = Resources.forward_white;
        }

        private void Btn_IO_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_Next.Image = Resources.forawrd_black;
        }


        //================================================================================================================================================//
        // SELECT CLIENT BUTTON                                                                                                                                       //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // NEW ORDER BUTTON                                                                                                                                        //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // FILTER BUTTON                                                                                                                                        //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // CLEAR FILTER BUTTON                                                                                                                                       //
        //================================================================================================================================================//
        private void Btn_IO_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_IO_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_IO_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_IO_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ENFORCE READ ONLY                                                                                                                                        //
        //================================================================================================================================================//
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
