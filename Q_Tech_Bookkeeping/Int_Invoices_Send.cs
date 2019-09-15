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
        private int NUM_OF_CLIENTS;
        private int SELECTED_INVSEND;
        private string CNAME;
        private string NEW_INVOICE;
        private DataTable clientsDT;
        private DataTable dt;

        public Int_Invoices_Send()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // LOAD INVOICES SEND FORM                                                                                                                        //
        //================================================================================================================================================//
        private void Invoices_Send_Load(object sender, EventArgs e)
        {
            dgv_IInvSent.DataSource = bs;

            LoadClients();
            LoadInvSend();

            dgv_IInvSent.Columns[4].DefaultCellStyle.Format = "c";
            dgv_IInvSent.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("en-US");
            dgv_IInvSent.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_IInvSent.Columns[5].DefaultCellStyle.Format = "c";
            dgv_IInvSent.Columns[5].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("en-US");
            dgv_IInvSent.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


        //================================================================================================================================================//
        // LOAD CLIENT DETAILS                                                                                                                            //
        //================================================================================================================================================//
        private void LoadClients()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Int_Clients", conn);
                clientsDT = new DataTable();
                sqlDataAdapter.Fill(clientsDT);
            }

            if (clientsDT.Rows.Count > 0)
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


        //================================================================================================================================================//
        // LOAD INVOICES SEND DETAILS                                                                                                                     //
        //================================================================================================================================================//
        private void LoadInvSend()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Invoices_Send WHERE Client LIKE '" + CNAME + "%'", conn);
                dt = new DataTable();
                da.Fill(dt);
            }
            bs.DataSource = dt;
        }


        //================================================================================================================================================//
        // NEXT CLICKED                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_IIS_Next_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT + 1 < NUM_OF_CLIENTS)
            {
                CUR_CLIENT++;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            else if (CUR_CLIENT + 1 == NUM_OF_CLIENTS)
            {
                btn_IIS_Next.Enabled = false;
                CUR_CLIENT++;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            if (CUR_CLIENT != 0 && !btn_IIS_Prev.Enabled)
                btn_IIS_Prev.Enabled = true;
        }


        //================================================================================================================================================//
        // PREVIOUS CLICKED                                                                                                                               //
        //================================================================================================================================================//
        private void Btn_IIS_Prev_Click(object sender, EventArgs e)
        {
            if (CUR_CLIENT - 1 > 0)
            {
                CUR_CLIENT--;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString().Trim();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString().Trim();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            else if (CUR_CLIENT - 1 == 0)
            {
                btn_IIS_Prev.Enabled = false;
                CUR_CLIENT--;
                txt_IIS_CCode.Text = clientsDT.Rows[CUR_CLIENT]["Code"].ToString();
                CNAME = clientsDT.Rows[CUR_CLIENT]["Name"].ToString();
                txt_IIS_CName.Text = CNAME;
                LoadInvSend();
            }
            if (CUR_CLIENT != NUM_OF_CLIENTS && !btn_IIS_Next.Enabled)
                btn_IIS_Next.Enabled = true;
        }


        //================================================================================================================================================//
        // SELECT CLIENT CLIENT                                                                                                                           //
        //================================================================================================================================================//
        private void Btn_IIS_SelCli_Click(object sender, EventArgs e)
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
            LoadInvSend();

            if (CUR_CLIENT != 0 && !btn_IIS_Prev.Enabled)
                btn_IIS_Prev.Enabled = true;

            if (CUR_CLIENT == 0 && btn_IIS_Prev.Enabled)
                btn_IIS_Prev.Enabled = false;

            if (CUR_CLIENT != NUM_OF_CLIENTS && !btn_IIS_Next.Enabled)
                btn_IIS_Next.Enabled = true;

            if (CUR_CLIENT == NUM_OF_CLIENTS && btn_IIS_Next.Enabled)
                btn_IIS_Next.Enabled = false;
        }


        //================================================================================================================================================//
        // NEW INVOICE SEND CLICK                                                                                                                         //
        //================================================================================================================================================//
        private void Btn_IIS_NewIS_Click(object sender, EventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            using (Inv_Send_Add frmISA = new Inv_Send_Add())
                frmISA.ShowDialog(this);

            LoadInvSend();

            if (send == null)
            {
                foreach (DataGridViewRow row in dgv_IInvSent.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(NEW_INVOICE))
                    {
                        SELECTED_INVSEND = row.Index;
                        break;
                    }
                }
                using (Inv_Send_Edit_DelOld frmISED = new Inv_Send_Edit_DelOld())
                {
                    frmISED.ShowDialog(this);
                }
                LoadInvSend();
            }
            else send = null;
        }


        //================================================================================================================================================//
        // GETTERS                                                                                                                                        //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // SETTERS                                                                                                                                        //
        //================================================================================================================================================//
        public void SetNewInvoice(string invNum)
        {
            NEW_INVOICE = invNum;
        }

        public void SetSender(object sender)
        {
            send = sender;
        }


        //================================================================================================================================================//
        // DATAGRIDVIEW CELL DOUBLECLICK                                                                                                                  //
        //================================================================================================================================================//
        private void Dgv_IInvSent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isFiltered)
                RemoveFilter();

            SELECTED_INVSEND = e.RowIndex;

            using (Inv_Send_Edit_DelOld frmISED = new Inv_Send_Edit_DelOld())
                frmISED.ShowDialog(this);

            LoadInvSend();
        }


        //================================================================================================================================================//
        // FILTERS                                                                                                                                        //
        //================================================================================================================================================//
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

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Invoices_Send WHERE Client LIKE '" + CNAME + "%' AND Date BETWEEN '" + dtp_IIS_From.Value + "' AND '" + dtp_IIS_To.Value + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
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


        //================================================================================================================================================//
        // PREVIOUS BUTTON                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_IIS_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_Prev.Image = Resources.back_white;
        }

        private void Btn_IIS_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_Prev.Image = Resources.back_black;
        }


        //================================================================================================================================================//
        // NEXT BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_IIS_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_Next.Image = Resources.forward_white;
        }

        private void Btn_IIS_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_Next.Image = Resources.forawrd_black;
        }


        //================================================================================================================================================//
        // SELECT CLIENT BUTTON                                                                                                                           //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // NEW INVOICE SEND BUTTON                                                                                                                        //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // FILTER BUTTON                                                                                                                                  //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // CLEAR FILTER BUTTON                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_IIS_ClearFilter_MouseEnter(object sender, EventArgs e)
        {
            btn_IIS_ClearFilter.ForeColor = Color.White;
        }

        private void Btn_IIS_ClearFilter_MouseLeave(object sender, EventArgs e)
        {
            btn_IIS_ClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ENFORCE READ ONLY                                                                                                                              //
        //================================================================================================================================================//
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
