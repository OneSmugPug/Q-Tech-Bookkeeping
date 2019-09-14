using ADGV;
using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class Int_Invoices_Send : Form
    {
        private int CUR_CLIENT = 0;
        private BindingSource bs = new BindingSource();
        private bool isFiltered = false;
        private object send = null;
        private IContainer components = (IContainer)null;
        private int NUM_OF_CLIENTS;
        private int SELECTED_INVSEND;
        private string CNAME;
        private string NEW_INVOICE;
        private DataTable clientsDT;
        private DataTable dt;
        //private Button btn_IIS_ClearFilter;
        //private BunifuDatepicker dtp_IIS_From;
        //private AdvancedDataGridView dgv_IInvSent;
        //private Button btn_IIS_NewIS;
        //private Button btn_IIS_Filter;
        //private BunifuDatepicker dtp_IIS_To;
        //private BunifuCustomLabel bunifuCustomLabel6;
        //private BunifuCustomLabel bunifuCustomLabel5;
        //private Button btn_IIS_SelCli;
        //private BunifuSeparator bunifuSeparator2;
        //private BunifuMaterialTextbox txt_IIS_CName;
        //private BunifuMaterialTextbox txt_IIS_CCode;
        //private Button btn_IIS_Next;
        //private BunifuCustomLabel bunifuCustomLabel3;
        //private BunifuCustomLabel bunifuCustomLabel4;
        //private Button btn_IIS_Prev;

        public Int_Invoices_Send()
        {
            InitializeComponent();
        }

        private void Invoices_Send_Load(object sender, EventArgs e)
        {
            dgv_IInvSent.DataSource = bs;
            LoadClients();
            LoadInvSend();
            dgv_IInvSent.Columns[4].DefaultCellStyle.Format = "c";
            dgv_IInvSent.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_IInvSent.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_IInvSent.Columns[5].DefaultCellStyle.Format = "c";
            dgv_IInvSent.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_IInvSent.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                if (!btn_IIS_SelCli.Enabled)
                    btn_IIS_SelCli.Enabled = true;
                if (!dgv_IInvSent.Enabled)
                    dgv_IInvSent.Enabled = true;
                if (!btn_IIS_NewIS.Enabled)
                    btn_IIS_NewIS.Enabled = true;
                NUM_OF_CLIENTS = clientsDT.Rows.Count - 1;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IIS_CName.Text = CNAME;
            }
            else
            {
                btn_IIS_SelCli.Enabled = false;
                btn_IIS_Next.Enabled = false;
                btn_IIS_Prev.Enabled = false;
                dgv_IInvSent.Enabled = false;
                btn_IIS_NewIS.Enabled = false;
            }
        }

        private void LoadInvSend()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Send WHERE Client LIKE '" + CNAME + "%'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
        }

        private void Btn_IIS_Next_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT + 1 < NUM_OF_CLIENTS)
            {
                ++CUR_CLIENT;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            else if (CUR_CLIENT + 1 == NUM_OF_CLIENTS)
            {
                btn_IIS_Next.Enabled = false;
                ++CUR_CLIENT;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            if (CUR_CLIENT == 0 || btn_IIS_Prev.Enabled)
                return;
            btn_IIS_Prev.Enabled = true;
        }

        private void Btn_IIS_Prev_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT - 1 > 0)
            {
                --CUR_CLIENT;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            else if (CUR_CLIENT - 1 == 0)
            {
                btn_IIS_Prev.Enabled = false;
                --CUR_CLIENT;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            if (CUR_CLIENT == NUM_OF_CLIENTS || btn_IIS_Next.Enabled)
                return;
            btn_IIS_Next.Enabled = true;
        }

        private void Btn_IIS_SelCli_Click(object sender, EventArgs e)
        {
            using (ClientList clientList = new ClientList())
            {
                int num = (int)clientList.ShowDialog((IWin32Window)this);
            }
        }

        public void SetNewClient(int rowIdx)
        {
            CUR_CLIENT = rowIdx;
            LoadClients();
            LoadInvSend();
            if (CUR_CLIENT != 0 && !btn_IIS_Prev.Enabled)
                btn_IIS_Prev.Enabled = true;
            if (CUR_CLIENT == 0 && btn_IIS_Prev.Enabled)
                btn_IIS_Prev.Enabled = false;
            if (CUR_CLIENT != NUM_OF_CLIENTS && !btn_IIS_Next.Enabled)
                btn_IIS_Next.Enabled = true;
            if (CUR_CLIENT != NUM_OF_CLIENTS || !btn_IIS_Next.Enabled)
                return;
            btn_IIS_Next.Enabled = false;
        }

        private void Btn_IIS_NewIS_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            using (Inv_Send_Add invSendAdd = new Inv_Send_Add())
            {
                int num = (int)invSendAdd.ShowDialog((IWin32Window)this);
            }
            LoadInvSend();
            if (send == null)
            {
                foreach (DataGridViewRow row in (IEnumerable)dgv_IInvSent.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(NEW_INVOICE))
                    {
                        SELECTED_INVSEND = row.Index;
                        break;
                    }
                }
                using (Inv_Send_Edit_Del invSendEditDel = new Inv_Send_Edit_Del())
                {
                    int num = (int)invSendEditDel.ShowDialog((IWin32Window)this);
                }
                LoadInvSend();
            }
            else
                send = null;
        }

        public string GetCCode()
        {
            return txt_IIS_CCode.Text;
        }

        public string GetCName()
        {
            return CNAME;
        }

        public int GetSelectedInvSend()
        {
            return SELECTED_INVSEND;
        }

        public DataTable GetInvoices()
        {
            return dt;
        }

        public void SetNewInvoice(string invNum)
        {
            NEW_INVOICE = invNum;
        }

        public void SetSender(object send)
        {
            send = send;
        }

        private void Dgv_IInvSent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();
            SELECTED_INVSEND = e.RowIndex;
            using (Inv_Send_Edit_Del invSendEditDel = new Inv_Send_Edit_Del())
            {
                int num = (int)invSendEditDel.ShowDialog((IWin32Window)this);
            }
            LoadInvSend();
        }

        private void Dgv_IInvSent_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_IInvSent.FilterString;
        }

        private void Dgv_IInvSent_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_IInvSent.SortString;
        }

        private void Btn_IIS_Filter_Click(object sender, EventArgs e)
        {
            bs.Filter = string.Empty;
            bs.Sort = string.Empty;
            isFiltered = true;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Invoices_Send WHERE Client LIKE '" + CNAME + "%' AND Date BETWEEN '" + dtp_IIS_From.Value + "' AND '" + dtp_IIS_To.Value + "' OR Client LIKE '" + CNAME + "%' AND Date_Paid BETWEEN '" + dtp_IIS_From.Value + "' AND '" + dtp_IIS_To.Value + "'", dbConnection);
                dt = new DataTable();
                sqlDataAdapter.Fill(dt);
            }
            bs.DataSource = dt;
            btn_IIS_Filter.Visible = false;
            btn_IIS_ClearFilter.Visible = true;
        }

        private void Btn_IIS_ClearFilter_Click(object sender, EventArgs e)
        {
            RemoveFilter();
        }

        private void RemoveFilter()
        {
            LoadInvSend();
            btn_IIS_Filter.Visible = true;
            btn_IIS_ClearFilter.Visible = false;
        }

        private void Btn_IIS_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_Prev.Image = Resources.back_white;
        }

        private void Btn_IIS_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_Prev.Image = Resources.back_black;
        }

        private void Btn_IIS_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_Next.Image = Resources.forward_white;
        }

        private void Btn_IIS_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_Next.Image = Resources.forawrd_black;
        }

        private void Btn_IIS_SelCli_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_SelCli.Image = Resources.client_list_white;
            btn_IIS_SelCli.ForeColor = Color.White;
        }

        private void Btn_IIS_SelCli_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_SelCli.Image = Resources.user_list;
            btn_IIS_SelCli.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IIS_NewIS_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_NewIS.Image = Resources.add_white;
            btn_IIS_NewIS.ForeColor = Color.White;
        }

        private void Btn_IIS_NewIS_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_NewIS.Image = Resources.add_grey;
            btn_IIS_NewIS.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IIS_Filter_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_Filter.Image = Resources.filter_white;
            btn_IIS_Filter.ForeColor = Color.White;
        }

        private void Btn_IIS_Filter_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_Filter.Image = Resources.filter_grey;
            btn_IIS_Filter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IIS_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_IIS_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Txt_IIS_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_IIS_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
