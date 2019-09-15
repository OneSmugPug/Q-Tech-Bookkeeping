using ADGV;
using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class Home : Form
    {
        private bool mouseDown;
        private bool isLocalOpen = false, isIntOpen = false, isLInvOpen = false, isConOpen = false,
            wasMax = false;
        private string selected = string.Empty;

        private BindingSource lClientsBS = new BindingSource();
        private bool isLCReadOnly = true;
        private int CUR_LCLIENT = 0;

        private BindingSource iClientsBS = new BindingSource();
        private bool isICReadOnly = true;
        private int CUR_ICLIENT = 0;

        private BindingSource conNRBS = new BindingSource();
        private BindingSource conNIBS = new BindingSource();

        private Point lastLocation;
        private string curVisible;
        private object curForm;

        private DataTable lClientDT;
        private int NUM_OF_LCLIENTS;
        private int NUM_OF_ICLIENTS;

        private DataTable iClientDT;

        private OrdersOld frmOrder;
        private Quotes frmQuote;
        private Invoices_SendOld frmInvSent;
        private Inv_Rec frmInvRec;
        private Int_Orders frmIntOrders;
        private Int_Quotes frmIntQuotes;
        private Int_Invoices_SendOld frmIntInvSent;
        private Contractors frmContr;
        private PettyCashOld frmPetty;
        private Projects frmProj;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            btn_Home.BackColor = Color.FromArgb(19, 118, 188);
            btn_Home.ForeColor = Color.White;

            selected = "Home";
            pnl_Home.Visible = true;
            CurrentPanel("pnl_Home");
        }

        private void LoadLocalClients()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                lClientDT = new DataTable();
                da = new SqlDataAdapter("SELECT * FROM Clients", conn);
                da.Fill(lClientDT);
            }

            lClientsBS.DataSource = lClientDT;
            NUM_OF_LCLIENTS = lClientDT.Rows.Count;

            if (NUM_OF_LCLIENTS == 0)
                btn_LC_Edit.Enabled = false;
            else if (NUM_OF_LCLIENTS != 0 && !btn_LC_Edit.Enabled)
                btn_LC_Edit.Enabled = true;
        }

        private void LoadIntClients()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                iClientDT = new DataTable();
                da = new SqlDataAdapter("SELECT * FROM Int_Clients", conn);
                da.Fill(iClientDT);
            }

            iClientsBS.DataSource = iClientDT;
            NUM_OF_ICLIENTS = iClientDT.Rows.Count;

            if (NUM_OF_ICLIENTS == 0)
                btn_IC_Edit.Enabled = false;
            else if (NUM_OF_ICLIENTS != 0 && !btn_IC_Edit.Enabled)
                btn_IC_Edit.Enabled = true;
        }


        //================================================================================================================================================//
        // CLOSE FORM                                                                                                                                     //
        //================================================================================================================================================//
        private void Btn_Home_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Home_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Close.Image = Resources.close_white;
        }

        private void Btn_Home_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Close.Image = Resources.close_black;
        }


        //================================================================================================================================================//
        // MAXIMIZE FORM                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_Home_Max_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Max.Image = Resources.maximize_white;
        }

        private void Btn_Home_Max_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Max.Image = Resources.maximize_black;
        }

        private void Btn_Home_Max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btn_Home_Max.Visible = false;
            btn_Home_Nor.Visible = true;
            lblComing.Location = new Point((pnl_Home.Width / 2) - (lblComing.Width / 2), (pnl_Home.Height / 2) - (lblComing.Height / 2));
        }


        //================================================================================================================================================//
        // NORMALIZE FORM                                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_Home_Nor_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Nor.Image = Resources.restore_white;
        }

        private void Btn_Home_Nor_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Nor.Image = Resources.restore_black2;
        }

        private void Btn_Home_Nor_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btn_Home_Nor.Visible = false;
            btn_Home_Max.Visible = true;
            lblComing.Location = new Point(416, 297);
        }


        //================================================================================================================================================//
        // MINIMIZE FORM                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_Home_Min_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Min.Image = Resources.minimize_white;
        }

        private void Btn_Home_Min_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Min.Image = Resources.minimize_grey;
        }

        private void Btn_Home_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //================================================================================================================================================//
        // DASHBOARD BUTTON                                                                                                                               //
        //================================================================================================================================================//
        private void Btn_Home_Click(object sender, EventArgs e)
        {
            lblComing.Visible = true;

            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_Home.Visible = true;
            CurrentPanel("pnl_Home");

            if (isLocalOpen && !isLInvOpen)
                tmr_Local.Start();
            else if (isLocalOpen && isLInvOpen)
            {
                tmr_L_Inv.Start();
                tmr_Local.Start();
            }

            if (isIntOpen)
                tmr_Int.Start();
            if (isConOpen)
                tmr_Con.Start();

            btn_Home.BackColor = Color.FromArgb(19, 118, 188);
            btn_Home.ForeColor = Color.White;
            btn_Home.Image = Resources.home_white;
        }

        private void Btn_Home_MouseEnter(object sender, EventArgs e)
        {
            btn_Home.BackColor = Color.FromArgb(73, 73, 73);
            btn_Home.ForeColor = Color.FromArgb(19, 118, 188);
            btn_Home.Image = Resources.home_blue;
        }

        private void Btn_Home_MouseLeave(object sender, EventArgs e)
        {
            btn_Home.BackColor = Color.FromArgb(64, 64, 64);
            btn_Home.ForeColor = Color.White;
            btn_Home.Image = Resources.home_white;
        }


        //================================================================================================================================================//
        // SETS NEW SELECTED BUTTON                                                                                                                       //
        //================================================================================================================================================//
        private void GetSelectedButton(object sender)
        {
            Button b = (Button)sender;
            string name = b.Name;

            switch (name)
            {
                case "btn_L_PettyCash":
                    {
                        selected = "lPettyCash";
                        break;
                    }
                case "btn_I_InvRec":
                    {
                        selected = "iInvRec";
                        break;
                    }
                case "btn_Contractors":
                    {
                        selected = "Contractors";
                        break;
                    }
                case "btn_L_Quotes":
                    {
                        selected = "lQuotes";
                        break;
                    }
                case "btn_C_NoRem":
                    {
                        selected = "cNoRem";
                        break;
                    }
                case "btn_Projects":
                    {
                        selected = "Projects";
                        break;
                    }
                case "btn_I_Clients":
                    {
                        selected = "iClients";
                        break;
                    }
                case "btn_Local":
                    {
                        selected = "Local";
                        break;
                    }
                case "btn_I_Orders":
                    {
                        selected = "iOrders";
                        break;
                    }
                case "btn_L_InvRec":
                    {
                        selected = "lInvRec";
                        break;
                    }
                case "btn_Int":
                    {
                        selected = "Int";
                        break;
                    }
                case "btn_Home":
                    {
                        selected = "Home";
                        break;
                    }
                case "btn_L_Clients":
                    {
                        selected = "lClients";
                        break;
                    }
                case "btn_I_InvSent":
                    {
                        selected = "iInvSent";
                        break;
                    }
                case "btn_I_Invoices":
                    {
                        selected = "iInvoices";
                        break;
                    }
                case "btn_L_InvSent":
                    {
                        selected = "lInvSent";
                        break;
                    }
                case "btn_C_Timesheets":
                    {
                        selected = "cTimesheets";
                        break;
                    }
                case "btn_C_NoInv":
                    {
                        selected = "cNoInv";
                        break;
                    }
                case "btn_L_Orders":
                    {
                        selected = "lOrders";
                        break;
                    }
                case "btn_I_Quotes":
                    {
                        selected = "iQuotes";
                        break;
                    }
            }
        }


        //================================================================================================================================================//
        // RESETS PREVIOUS SELECTED BUTTON COLOUR                                                                                                         //
        //================================================================================================================================================//
        private void ResetButtons(string name)
        {
            switch (name)
            {
                case "lInvRec":
                    {
                        btn_L_InvRec.BackColor = Color.FromArgb(35, 35, 35);
                        btn_L_InvRec.ForeColor = Color.White;
                        break;
                    }
                case "lQuotes":
                    {
                        btn_L_Quotes.BackColor = Color.FromArgb(50, 50, 50);
                        btn_L_Quotes.ForeColor = Color.White;
                        break;
                    }
                case "Local":
                    {
                        btn_Local.BackColor = Color.FromArgb(64, 64, 64);
                        btn_Local.ForeColor = Color.White;
                        break;
                    }
                case "iOrders":
                    {
                        btn_I_Orders.BackColor = Color.FromArgb(50, 50, 50);
                        btn_I_Orders.ForeColor = Color.White;
                        break;
                    }
                case "iInvSent":
                    {
                        btn_I_InvSent.BackColor = Color.FromArgb(50, 50, 50);
                        btn_I_InvSent.ForeColor = Color.White;
                        break;
                    }
                case "lClients":
                    {
                        btn_L_Clients.BackColor = Color.FromArgb(50, 50, 50);
                        btn_L_Clients.ForeColor = Color.White;
                        break;
                    }
                case "Home":
                    {
                        btn_Home.BackColor = Color.FromArgb(64, 64, 64);
                        btn_Home.ForeColor = Color.White;
                        lblComing.Visible = false;
                        break;
                    }
                case "iClients":
                    {
                        btn_I_Clients.BackColor = Color.FromArgb(50, 50, 50);
                        btn_I_Clients.ForeColor = Color.White;
                        break;
                    }
                case "Projects":
                    {
                        btn_Projects.BackColor = Color.FromArgb(64, 64, 64);
                        btn_Projects.ForeColor = Color.White;
                        break;
                    }
                case "Contractors":
                    {
                        btn_Contractors.BackColor = Color.FromArgb(64, 64, 64);
                        btn_Contractors.ForeColor = Color.White;
                        break;
                    }
                case "iQuotes":
                    {
                        btn_I_Quotes.BackColor = Color.FromArgb(50, 50, 50);
                        btn_I_Quotes.ForeColor = Color.White;
                        break;
                    }
                case "lInvSent":
                    {
                        btn_L_InvSent.BackColor = Color.FromArgb(35, 35, 35);
                        btn_L_InvSent.ForeColor = Color.White;
                        break;
                    }
                case "cNoRem":
                    {
                        btn_C_NoRem.BackColor = Color.FromArgb(50, 50, 50);
                        btn_C_NoRem.ForeColor = Color.White;
                        break;
                    }
                case "lPettyCash":
                    {
                        btn_L_PettyCash.BackColor = Color.FromArgb(50, 50, 50);
                        btn_L_PettyCash.ForeColor = Color.White;
                        break;
                    }
                case "lOrders":
                    {
                        btn_L_Orders.BackColor = Color.FromArgb(50, 50, 50);
                        btn_L_Orders.ForeColor = Color.White;
                        break;
                    }
                case "lInvoices":
                    {
                        btn_L_Invoices.BackColor = Color.FromArgb(50, 50, 50);
                        btn_L_Invoices.ForeColor = Color.White;
                        break;
                    }
                case "cTimesheets":
                    {
                        btn_C_Timesheets.BackColor = Color.FromArgb(50, 50, 50);
                        btn_C_Timesheets.ForeColor = Color.White;
                        break;
                    }
                case "cNoInv":
                    {
                        btn_C_NoInv.BackColor = Color.FromArgb(50, 50, 50);
                        btn_C_NoInv.ForeColor = Color.White;
                        break;
                    }
                case "Int":
                    {
                        btn_Int.BackColor = Color.FromArgb(64, 64, 64);
                        btn_Int.ForeColor = Color.White;
                        break;
                    }
            }
        }


        //================================================================================================================================================//
        // SETS NEW VISIBLE PANEL                                                                                                                         //
        //================================================================================================================================================//
        private void CurrentPanel(string name)
        {
            switch (name)
            {
                case "pnl_L_Quotes":
                    {
                        curVisible = "pnl_L_Quotes";
                        break;
                    }
                case "pnl_L_InvSent":
                    {
                        curVisible = "pnl_L_InvSent";
                        break;
                    }
                case "pnl_I_Orders":
                    {
                        curVisible = "pnl_I_Orders";
                        break;
                    }
                case "pnl_L_PettyCash":
                    {
                        curVisible = "pnl_L_PettyCash";
                        break;
                    }
                case "pnl_L_InvRec":
                    {
                        curVisible = "pnl_L_InvRec";
                        break;
                    }
                case "pnl_I_Quotes":
                    {
                        curVisible = "pnl_I_Quotes";
                        break;
                    }
                case "pnl_I_InvSent":
                    {
                        curVisible = "pnl_I_InvSent";
                        break;
                    }
                case "pnl_Projects":
                    {
                        curVisible = "pnl_Projects";
                        break;
                    }
                case "pnl_C_NoRem":
                    {
                        curVisible = "pnl_C_NoRem";
                        break;
                    }
                case "pnl_I_Clients":
                    {
                        curVisible = "pnl_I_Clients";
                        break;
                    }
                case "pnl_Contractors":
                    {
                        curVisible = "pnl_Contractors";
                        break;
                    }
                case "pnl_Home":
                    {
                        curVisible = "pnl_Home";
                        break;
                    }
                case "pnl_L_CDet":
                    {
                        curVisible = "pnl_L_CDet";
                        break;
                    }
                case "pnl_C_NoInv":
                    {
                        curVisible = "pnl_C_NoInv";
                        break;
                    }
                case "pnl_L_Orders":
                    {
                        curVisible = "pnl_L_Orders";
                        break;
                    }
            }
        }


        //================================================================================================================================================//
        // HIDE CURRENTLY VISIBLE PANEL                                                                                                                   //
        //================================================================================================================================================//
        private void HidePanel()
        {
            switch (curVisible)
            {
                case "pnl_L_Quotes":
                    {
                        pnl_L_Quotes.Visible = false;
                        break;
                    }
                case "pnl_L_InvSent":
                    {
                        pnl_L_InvSent.Visible = false;
                        break;
                    }
                case "pnl_I_Orders":
                    {
                        pnl_I_Orders.Visible = false;
                        break;
                    }
                case "pnl_L_PettyCash":
                    {
                        pnl_L_PettyCash.Visible = false;
                        break;
                    }
                case "pnl_L_InvRec":
                    {
                        pnl_L_InvRec.Visible = false;
                        break;
                    }
                case "pnl_I_Quotes":
                    {
                        pnl_I_Quotes.Visible = false;
                        break;
                    }
                case "pnl_I_InvSent":
                    {
                        pnl_I_InvSent.Visible = false;
                        break;
                    }
                case "pnl_Projects":
                    {
                        pnl_Projects.Visible = false;
                        break;
                    }
                case "pnl_C_NoRem":
                    {
                        pnl_C_NoRem.Visible = false;
                        break;
                    }
                case "pnl_I_Clients":
                    {
                        pnl_I_Clients.Visible = false;
                        break;
                    }
                case "pnl_Contractors":
                    {
                        pnl_Con.Visible = false;
                        break;
                    }
                case "pnl_Home":
                    {
                        pnl_Home.Visible = false;
                        break;
                    }
                case "pnl_L_CDet":
                    {
                        pnl_L_CDet.Visible = false;
                        break;
                    }
                case "pnl_C_NoInv":
                    {
                        pnl_C_NoInv.Visible = false;
                        break;
                    }
                case "pnl_L_Orders":
                    {
                        pnl_L_Orders.Visible = false;
                        break;
                    }
            }
        }

        public string GetCurPanel() { return curVisible; }

        public object GetCurForm() { return curForm; }


        //================================================================================================================================================//
        // LOCAL BUTTON                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_Local_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "Local")
            {
                btn_Local.BackColor = Color.FromArgb(73, 73, 73);
                btn_Local.ForeColor = Color.FromArgb(19, 118, 188);
                btn_Local.Image = Resources.local_blue;
            }
        }

        private void Btn_Local_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "Local")
            {
                btn_Local.BackColor = Color.FromArgb(64, 64, 64);
                btn_Local.ForeColor = Color.White;
                btn_Local.Image = Resources.local_white;
            }
        }

        private void Btn_Local_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(btn_L_Clients);
            HidePanel();

            if (isIntOpen)
                tmr_Int.Start();
            if (isConOpen)
                tmr_Con.Start();

            btn_Local.BackColor = Color.FromArgb(19, 118, 188);
            btn_Local.ForeColor = Color.White;
            btn_Local.Image = Resources.local_white;

            if (isLInvOpen && isLocalOpen)
                tmr_L_Inv.Start();

            btn_L_Clients.BackColor = Color.FromArgb(15, 91, 142);
            btn_L_Clients.ForeColor = Color.White;
            pnl_L_CDet.Visible = true;
            tmr_Local.Start();
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS BUTTON                                                                                                                           //
        //================================================================================================================================================//
        private void Btn_L_Clients_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            btn_L_Clients.BackColor = Color.FromArgb(15, 91, 142);
            btn_L_Clients.ForeColor = Color.White;
            pnl_L_CDet.Visible = true;
        }

        private void Btn_L_Clients_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "lClients")
            {
                btn_L_Clients.BackColor = Color.FromArgb(73, 73, 73);
                btn_L_Clients.ForeColor = Color.FromArgb(19, 118, 188);
            }
        }

        private void Btn_L_Clients_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "lClients")
            {
                btn_L_Clients.BackColor = Color.FromArgb(50, 50, 50);
                btn_L_Clients.ForeColor = Color.White;
            }
        }

        private void Btn_L_Orders_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "lOrders")
            {
                btn_L_Orders.BackColor = Color.FromArgb(73, 73, 73);
                btn_L_Orders.ForeColor = Color.FromArgb(19, 118, 188);
            }
        }


        //================================================================================================================================================//
        // LOCAL ORDERS BUTTON                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_L_Orders_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "lOrders")
            {
                btn_L_Orders.BackColor = Color.FromArgb(50, 50, 50);
                btn_L_Orders.ForeColor = Color.White;
            }
        }

        private void Btn_L_Orders_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_L_Orders.Visible = true;
            CurrentPanel("pnl_L_Orders");

            btn_L_Orders.BackColor = Color.FromArgb(15, 91, 142);
            btn_L_Orders.ForeColor = Color.White;

            frmOrder = new OrdersOld();
            curForm = frmOrder;
            frmOrder.TopLevel = false;
            frmOrder.TopMost = true;
            pnl_L_Orders.Controls.Add(frmOrder);
            frmOrder.Show();
        }


        //================================================================================================================================================//
        // LOCAL QUOTES BUTTON                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_L_Quotes_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "lQuotes")
            {
                btn_L_Quotes.BackColor = Color.FromArgb(73, 73, 73);
                btn_L_Quotes.ForeColor = Color.FromArgb(19, 118, 188);
            }
        }

        private void Btn_L_Quotes_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "lQuotes")
            {
                btn_L_Quotes.BackColor = Color.FromArgb(50, 50, 50);
                btn_L_Quotes.ForeColor = Color.White;
            }
        }

        private void Btn_L_Quotes_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_L_Quotes.Visible = true;
            CurrentPanel("pnl_L_Quotes");

            btn_L_Quotes.BackColor = Color.FromArgb(15, 91, 142);
            btn_L_Quotes.ForeColor = Color.White;

            frmQuote = new Quotes();
            curForm = frmQuote;
            frmQuote.TopLevel = false;
            frmQuote.TopMost = true;
            pnl_L_Quotes.Controls.Add(frmQuote);
            frmQuote.Show();
        }


        //================================================================================================================================================//
        // LOCAL INVOICES BUTTON                                                                                                                          //
        //================================================================================================================================================//
        private void Btn_L_Invoices_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "lInvoices")
            {
                btn_L_Invoices.BackColor = Color.FromArgb(73, 73, 73);
                btn_L_Invoices.ForeColor = Color.FromArgb(19, 118, 188);
            }
        }

        private void Btn_L_Invoices_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "lInvoices")
            {
                btn_L_Invoices.BackColor = Color.FromArgb(50, 50, 50);
                btn_L_Invoices.ForeColor = Color.White;
            }
        }

        private void Btn_L_Invoices_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);

            btn_L_Invoices.BackColor = Color.FromArgb(15, 91, 142);
            btn_L_Invoices.ForeColor = Color.White;

            tmr_L_Inv.Start();
        }


        //================================================================================================================================================//
        // LOCAL INVOICES SENT BUTTON                                                                                                                     //
        //================================================================================================================================================//
        private void Btn_L_InvSent_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_L_InvSent.Visible = true;
            CurrentPanel("pnl_L_InvSent");

            btn_L_InvSent.BackColor = Color.FromArgb(13, 77, 119);
            btn_L_InvSent.ForeColor = Color.White;

            frmInvSent = new Invoices_SendOld();
            curForm = frmInvSent;
            frmInvSent.TopLevel = false;
            frmInvSent.TopMost = true;
            pnl_L_InvSent.Controls.Add(frmInvSent);
            frmInvSent.Show();
        }

        private void Btn_L_InvSent_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "lInvSent")
            {
                btn_L_InvSent.BackColor = Color.FromArgb(73, 73, 73);
                btn_L_InvSent.ForeColor = Color.FromArgb(19, 118, 188);
            }
        }

        private void Btn_L_InvSent_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "lInvSent")
            {
                btn_L_InvSent.BackColor = Color.FromArgb(35, 35, 35);
                btn_L_InvSent.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // LOCAL INVOICES RECEIVED BUTTON                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_L_InvRec_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_L_InvRec.Visible = true;
            CurrentPanel("pnl_L_InvRec");

            btn_L_InvRec.BackColor = Color.FromArgb(13, 77, 119);
            btn_L_InvRec.ForeColor = Color.White;

            frmInvRec = new Inv_Rec();
            curForm = frmInvRec;
            frmInvRec.TopLevel = false;
            frmInvRec.TopMost = true;
            pnl_L_InvRec.Controls.Add(frmInvRec);
            frmInvRec.Show();
        }

        private void Btn_L_InvRec_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "lInvRec")
            {
                btn_L_InvRec.BackColor = Color.FromArgb(73, 73, 73);
                btn_L_InvRec.ForeColor = Color.FromArgb(19, 118, 188);
            }
        }

        private void Btn_L_InvRec_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "lInvRec")
            {
                btn_L_InvRec.BackColor = Color.FromArgb(35, 35, 35);
                btn_L_InvRec.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // PETTY CASH BUTTON                                                                                                                              //
        //================================================================================================================================================//
        private void Btn_L_PettyCash_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_L_PettyCash.Visible = true;
            CurrentPanel("pnl_L_PettyCash");

            btn_L_PettyCash.BackColor = Color.FromArgb(13, 77, 119);
            btn_L_PettyCash.ForeColor = Color.White;

            frmPetty = new PettyCash();
            curForm = frmPetty;
            frmPetty.TopLevel = false;
            frmPetty.TopMost = true;
            pnl_L_PettyCash.Controls.Add(frmPetty);
            frmPetty.Show();
        }

        private void Btn_L_PettyCash_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "lPettyCash")
            {
                btn_L_PettyCash.BackColor = Color.FromArgb(73, 73, 73);
                btn_L_PettyCash.ForeColor = Color.FromArgb(19, 118, 188);
            }
        }

        private void Btn_L_PettyCash_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "lPettyCash")
            {
                btn_L_PettyCash.BackColor = Color.FromArgb(50, 50, 50);
                btn_L_PettyCash.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // INTERNATIONAL BUTTON                                                                                                                           //
        //================================================================================================================================================//
        private void Btn_Int_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            if (isLocalOpen && !isLInvOpen)
                tmr_Local.Start();
            else if (isLocalOpen && isLInvOpen)
            {
                tmr_L_Inv.Start();
                tmr_Local.Start();
            }

            if (isConOpen)
                tmr_Con.Start();

            btn_Int.BackColor = Color.FromArgb(19, 118, 188);
            btn_Int.ForeColor = Color.White;
            btn_Int.Image = Resources.globe_white;
            btn_I_Clients.BackColor = Color.FromArgb(15, 91, 142);
            btn_I_Clients.ForeColor = Color.White;
            pnl_I_Clients.Visible = true;
            tmr_Int.Start();
        }

        private void Btn_Int_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "Int")
            {
                btn_Int.BackColor = Color.FromArgb(73, 73, 73);
                btn_Int.ForeColor = Color.FromArgb(19, 118, 188);
                btn_Int.Image = Resources.globe_blue;
            }
        }

        private void Btn_Int_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "Int")
            {
                btn_Int.BackColor = Color.FromArgb(64, 64, 64);
                btn_Int.ForeColor = Color.White;
                btn_Int.Image = (Image)Resources.globe_white;
            }
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS BUTTON                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_I_Clients_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            btn_I_Clients.BackColor = Color.FromArgb(15, 91, 142);
            btn_I_Clients.ForeColor = Color.White;
            pnl_I_Clients.Visible = true;
        }

        private void Btn_I_Clients_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "iClients")
            {
                btn_I_Clients.BackColor = Color.FromArgb(56, 56, 56);
                btn_I_Clients.ForeColor = Color.FromArgb(15, 91, 142);
            }
        }

        private void Btn_I_Clients_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "iClients")
            {
                btn_I_Clients.BackColor = Color.FromArgb(50, 50, 50);
                btn_I_Clients.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // INTERNATIONAL ORDERS BUTTON                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_I_Orders_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_I_Orders.Visible = true;
            CurrentPanel("pnl_I_Orders");

            btn_I_Orders.BackColor = Color.FromArgb(15, 91, 142);
            btn_I_Orders.ForeColor = Color.White;

            frmIntOrders = new Int_Orders();
            curForm = frmIntOrders;
            frmIntOrders.TopLevel = false;
            frmIntOrders.TopMost = true;
            pnl_I_Orders.Controls.Add(frmIntOrders);
            frmIntOrders.Show();
        }

        private void Btn_I_Orders_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "iOrders")
            {
                btn_I_Orders.BackColor = Color.FromArgb(56, 56, 56);
                btn_I_Orders.ForeColor = Color.FromArgb(15, 91, 142);
            }
        }

        private void Btn_I_Orders_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "iOrders")
            {
                btn_I_Orders.BackColor = Color.FromArgb(50, 50, 50);
                btn_I_Orders.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // INTERNATIONAL QUOTES BUTTON                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_I_Quotes_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_I_Quotes.Visible = true;
            CurrentPanel("pnl_I_Quotes");

            btn_I_Quotes.BackColor = Color.FromArgb(15, 91, 142);
            btn_I_Quotes.ForeColor = Color.White;

            frmIntQuotes = new Int_Quotes();
            curForm = frmIntQuotes;
            frmIntQuotes.TopLevel = false;
            frmIntQuotes.TopMost = true;
            pnl_I_Quotes.Controls.Add(frmIntQuotes);
            frmIntQuotes.Show();
        }

        private void Btn_I_Quotes_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "iQuotes")
            {
                btn_I_Quotes.BackColor = Color.FromArgb(56, 56, 56);
                btn_I_Quotes.ForeColor = Color.FromArgb(15, 91, 142);
            }
        }

        private void Btn_I_Quotes_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "iQuotes")
            {
                btn_I_Quotes.BackColor = Color.FromArgb(50, 50, 50);
                btn_I_Quotes.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // INTERNATIONAL INVOICES SENT BUTTON                                                                                                             //
        //================================================================================================================================================//
        private void Btn_I_InvSent_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_I_InvSent.Visible = true;
            CurrentPanel("pnl_I_InvSent");

            btn_I_InvSent.BackColor = Color.FromArgb(13, 77, 119);
            btn_I_InvSent.ForeColor = Color.White;

            frmIntInvSent = new Int_Invoices_Send();
            curForm = frmIntInvSent;
            frmIntInvSent.TopLevel = false;
            frmIntInvSent.TopMost = true;
            pnl_I_InvSent.Controls.Add(frmIntInvSent);
            frmIntInvSent.Show();
        }

        private void Btn_I_InvSent_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "iInvSent")
            {
                btn_I_InvSent.BackColor = Color.FromArgb(56, 56, 56);
                btn_I_InvSent.ForeColor = Color.FromArgb(15, 91, 142);
            }
        }

        private void Btn_I_InvSent_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "iInvSent")
            {
                btn_I_InvSent.BackColor = Color.FromArgb(50, 50, 50);
                btn_I_InvSent.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // CONTRACTORS BUTTON                                                                                                                             //
        //================================================================================================================================================//
        private void Btn_Contractors_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();
            if (isLocalOpen && !isLInvOpen)
                tmr_Local.Start();
            else if (isLocalOpen && isLInvOpen)
            {
                tmr_L_Inv.Start();
                tmr_Local.Start();
            }
            if (isIntOpen)
                tmr_Int.Start();

            btn_Contractors.BackColor = Color.FromArgb(19, 118, 188);
            btn_Contractors.ForeColor = Color.White;
            btn_Contractors.Image = Resources.contr_white;
            tmr_Con.Start();

            pnl_Con.Visible = true;
            CurrentPanel("pnl_Contractors");

            btn_C_Timesheets.BackColor = Color.FromArgb(13, 77, 119);
            btn_C_Timesheets.ForeColor = Color.White;

            frmContr = new Contractors();
            curForm = frmContr;
            frmContr.TopLevel = false;
            frmContr.TopMost = true;
            pnl_Con.Controls.Add(frmContr);
            frmContr.Show();
        }

        private void Btn_Contractors_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "Contractors")
            {
                btn_Contractors.BackColor = Color.FromArgb(73, 73, 73);
                btn_Contractors.ForeColor = Color.FromArgb(19, 118, 188);
                btn_Contractors.Image = Resources.contr_blue;
            }
        }

        private void Btn_Contractors_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "Contractors")
            {
                btn_Contractors.BackColor = Color.FromArgb(64, 64, 64);
                btn_Contractors.ForeColor = Color.White;
                btn_Contractors.Image = Resources.contr_white;
            }
        }


        //================================================================================================================================================//
        // CONTRACTORS TIMESHEETS BUTTON                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_C_Timesheets_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_Con.Visible = true;
            CurrentPanel("pnl_Contractors");

            btn_C_Timesheets.BackColor = Color.FromArgb(13, 77, 119);
            btn_C_Timesheets.ForeColor = Color.White;
            frmContr = new Contractors();
            curForm = frmContr;
            frmContr.TopLevel = false;
            frmContr.TopMost = true;
            pnl_Con.Controls.Add(frmContr);
            frmContr.Show();
        }

        private void Btn_C_Timesheets_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "cTimesheets")
            {
                btn_C_Timesheets.BackColor = Color.FromArgb(56, 56, 56);
                btn_C_Timesheets.ForeColor = Color.FromArgb(15, 91, 142);
            }
        }

        private void Btn_C_Timesheets_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "cTimesheets")
            {
                btn_C_Timesheets.BackColor = Color.FromArgb(50, 50, 50);
                btn_C_Timesheets.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // CONTRACTORS OUTSTANDING REMITTANCES BUTTON                                                                                                     //
        //================================================================================================================================================//
        private void Btn_C_NoRem_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_C_NoRem.Visible = true;
            CurrentPanel("pnl_C_NoRem");

            btn_C_NoRem.BackColor = Color.FromArgb(15, 91, 142);
            btn_C_NoRem.ForeColor = Color.White;

            dgv_NoRem.DataSource = conNRBS;
            LoadNoRemittances();

            dgv_NoRem.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_NoRem.Columns[4].DefaultCellStyle.Format = "c";
            dgv_NoRem.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoRem.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_NoRem.Columns[5].DefaultCellStyle.Format = "c";
            dgv_NoRem.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoRem.Columns[6].DefaultCellStyle.Format = "c";
            dgv_NoRem.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoRem.Columns[7].DefaultCellStyle.Format = "c";
            dgv_NoRem.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoRem.Columns[8].DefaultCellStyle.Format = "c";
            dgv_NoRem.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoRem.Columns[9].DefaultCellStyle.Format = "c";
            dgv_NoRem.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadNoRemittances()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                da = new SqlDataAdapter("SELECT * FROM Contractor_Hours WHERE Remittance = 'No'", conn);
                da.Fill(dt);
            }
                
            conNRBS.DataSource = dt;
        }

        private void Btn_C_NoRem_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "cNoRem")
            {
                btn_C_NoRem.BackColor = Color.FromArgb(56, 56, 56);
                btn_C_NoRem.ForeColor = Color.FromArgb(15, 91, 142);
            }
        }

        private void Btn_C_NoRem_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "cNoRem")
            {
                btn_C_NoRem.BackColor = Color.FromArgb(50, 50, 50);
                btn_C_NoRem.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // CONTRACTORS OUTSTANDING INVOICES BUTTON                                                                                                        //
        //================================================================================================================================================//
        private void btn_C_NoInv_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_C_NoInv.Visible = true;
            CurrentPanel("pnl_C_NoInv");

            btn_C_NoInv.BackColor = Color.FromArgb(15, 91, 142);
            btn_C_NoInv.ForeColor = Color.White;

            dgv_NoInv.DataSource = conNIBS;
            LoadNoInvoices();

            dgv_NoInv.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_NoInv.Columns[4].DefaultCellStyle.Format = "c";
            dgv_NoInv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoInv.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            dgv_NoInv.Columns[5].DefaultCellStyle.Format = "c";
            dgv_NoInv.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoInv.Columns[6].DefaultCellStyle.Format = "c";
            dgv_NoInv.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoInv.Columns[7].DefaultCellStyle.Format = "c";
            dgv_NoInv.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoInv.Columns[8].DefaultCellStyle.Format = "c";
            dgv_NoInv.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_NoInv.Columns[9].DefaultCellStyle.Format = "c";
            dgv_NoInv.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadNoInvoices()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                da = new SqlDataAdapter("SELECT * FROM Contractor_Hours WHERE Invoice_Received = 'No'", conn);
                da.Fill(dt);
            }
                
            conNIBS.DataSource = dt;
        }

        private void Btn_C_NoInv_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "cNoInv")
            {
                btn_C_NoInv.BackColor = Color.FromArgb(56, 56, 56);
                btn_C_NoInv.ForeColor = Color.FromArgb(15, 91, 142);
            }
        }

        private void Btn_C_NoInv_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "cNoInv")
            {
                btn_C_NoInv.BackColor = Color.FromArgb(50, 50, 50);
                btn_C_NoInv.ForeColor = Color.White;
            }
        }


        //================================================================================================================================================//
        // PROJECTS BUTTON                                                                                                                                //
        //================================================================================================================================================//
        private void Btn_Projects_Click(object sender, EventArgs e)
        {
            ResetButtons(selected);
            GetSelectedButton(sender);
            HidePanel();

            pnl_Projects.Visible = true;
            CurrentPanel("pnl_Projects");

            if (isLocalOpen && !isLInvOpen)
                tmr_Local.Start();
            else if (isLocalOpen && isLInvOpen)
            {
                tmr_L_Inv.Start();
                tmr_Local.Start();
            }

            if (isIntOpen)
                tmr_Int.Start();

            if (isConOpen)
                tmr_Con.Start();

            btn_Projects.BackColor = Color.FromArgb(19, 118, 188);
            btn_Projects.ForeColor = Color.White;
            btn_Projects.Image = Resources.project_white;

            frmProj = new Projects();
            curForm = frmProj;
            frmProj.TopLevel = false;
            frmProj.TopMost = true;
            pnl_Projects.Controls.Add(frmProj);
            frmProj.Show();
        }

        private void Btn_Projects_MouseEnter(object sender, EventArgs e)
        {
            if (selected != "Projects")
            {
                btn_Projects.BackColor = Color.FromArgb(73, 73, 73);
                btn_Projects.ForeColor = Color.FromArgb(19, 118, 188);
                btn_Projects.Image = Resources.project_blue;
            }
        }

        private void Btn_Projects_MouseLeave(object sender, EventArgs e)
        {
            if (selected != "Projects")
            {
                btn_Projects.BackColor = Color.FromArgb(64, 64, 64);
                btn_Projects.ForeColor = Color.White;
                btn_Projects.Image = Resources.project_white;
            }
        }

        public void SetProjExpForm(Proj_AddExpOld frmAE, Home frmHome)
        {
            frmAE.TopLevel = false;
            frmAE.TopMost = true;
            pnl_Projects.Controls.Add(frmAE);
            frmAE.setHome(frmHome);
            frmAE.Show();
        }


        //================================================================================================================================================//
        // OPENS/CLOSES LOCAL TAB                                                                                                                         //
        //================================================================================================================================================//
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (isLocalOpen)
            {
                if (pnl_Local.Height <= 48)
                {
                    tmr_Local.Stop();
                    isLocalOpen = false;
                }
                else
                {
                    pnl_Local.Height -= 15;

                    pnl_Int.Location = new Point(pnl_Int.Location.X, pnl_Int.Location.Y - 15);
                    pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y - 15);
                    btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y - 15);
                }
            }
            else if (pnl_Local.Height >= 288)
            {
                tmr_Local.Stop();
                isLocalOpen = true;
            }
            else
            {
                pnl_Local.Height += 15;

                pnl_Int.Location = new Point(pnl_Int.Location.X, pnl_Int.Location.Y + 15);
                pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y + 15);
                btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y + 15);
            }
        }


        //================================================================================================================================================//
        // OPENS/CLOSES INTERNATIONAL TAB                                                                                                                 //
        //================================================================================================================================================//
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (isIntOpen)
            {
                if (pnl_Int.Height <= 48)
                {
                    tmr_Int.Stop();
                    isIntOpen = false;
                }
                else if (pnl_Int.Height == 60)
                {
                    pnl_Int.Height -= 12;

                    pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y - 12);
                    btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y - 12);
                }
                else
                {
                    pnl_Int.Height -= 15;

                    pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y - 15);
                    btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y - 15);
                }
            }
            else if (pnl_Int.Height >= 240)
            {
                tmr_Int.Stop();
                isIntOpen = true;
            }
            else if (pnl_Int.Height == 228)
            {
                pnl_Int.Height += 12;

                pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y + 12);
                btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y + 12);
            }
            else
            {
                pnl_Int.Height += 15;

                pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y + 15);
                btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y + 15);
            }
        }


        //================================================================================================================================================//
        // OPENS/CLOSES CONTRACTOR TAB                                                                                                                    //
        //================================================================================================================================================//
        private void Tmr_Con_Tick(object sender, EventArgs e)
        {
            if (isConOpen)
            {
                if (pnl_Con.Height <= 48)
                {
                    tmr_Con.Stop();
                    isConOpen = false;
                }
                else if (pnl_Con.Height == 57)
                {
                    pnl_Con.Height -= 9;
                    btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y - 9);
                }
                else
                {
                    pnl_Con.Height -= 15;
                    btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y - 15);
                }
            }
            else if (pnl_Con.Height >= 192)
            {
                tmr_Con.Stop();
                isConOpen = true;
            }
            else if (pnl_Con.Height == 183)
            {
                pnl_Con.Height += 9;
                btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y + 9);
            }
            else
            {
                pnl_Con.Height += 15;
                btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y + 15);
            }
        }


        //================================================================================================================================================//
        // OPENS/CLOSES LOCAL INVOICES TAB                                                                                                                //
        //================================================================================================================================================//
        private void Tmr_L_Inv_Tick(object sender, EventArgs e)
        {
            if (isLInvOpen)
            {
                if (pnl_L_Inv.Height <= 48)
                {
                    tmr_L_Inv.Stop();
                    isLInvOpen = false;
                }
                else if (pnl_L_Inv.Height == 54)
                {
                    pnl_Local.Height -= 6;
                    pnl_L_Inv.Height -= 6;

                    pnl_Int.Location = new Point(pnl_Int.Location.X, pnl_Int.Location.Y - 6);
                    pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y - 6);
                    btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y - 6);
                    btn_L_PettyCash.Location = new Point(btn_L_PettyCash.Location.X, btn_L_PettyCash.Location.Y - 6);
                }
                else
                {
                    pnl_Local.Height -= 15;
                    pnl_L_Inv.Height -= 15;

                    pnl_Int.Location = new Point(pnl_Int.Location.X, pnl_Int.Location.Y - 15);
                    pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y - 15);
                    btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y - 15);
                    btn_L_PettyCash.Location = new Point(btn_L_PettyCash.Location.X, btn_L_PettyCash.Location.Y - 15);
                }
            }
            else if (pnl_L_Inv.Height >= 144)
            {
                tmr_L_Inv.Stop();
                isLInvOpen = true;
            }
            else if (pnl_L_Inv.Height == 138)
            {
                pnl_Local.Height += 6;
                pnl_L_Inv.Height += 6;

                pnl_Int.Location = new Point(pnl_Int.Location.X, pnl_Int.Location.Y + 6);
                pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y + 6);
                btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y + 6);
                btn_L_PettyCash.Location = new Point(btn_L_PettyCash.Location.X, btn_L_PettyCash.Location.Y + 6);

            }
            else
            {
                pnl_Local.Height += 15;
                pnl_L_Inv.Height += 15;

                pnl_Int.Location = new Point(pnl_Int.Location.X, pnl_Int.Location.Y + 15);
                pnl_Con.Location = new Point(pnl_Con.Location.X, pnl_Con.Location.Y + 15);
                btn_Projects.Location = new Point(btn_Projects.Location.X, btn_Projects.Location.Y + 15);
                btn_L_PettyCash.Location = new Point(btn_L_PettyCash.Location.X, btn_L_PettyCash.Location.Y + 15);
            }
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS PREVIOUS BUTTON                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_LC_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_LC_Prev.Image = Resources.back_white;
        }

        private void Btn_LC_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_LC_Prev.Image = Resources.back_black;
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS NEXT BUTTON                                                                                                                      //
        //================================================================================================================================================//
        private void Btn_LC_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_LC_Next.Image = Resources.forward_white;
        }

        private void Btn_LC_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_LC_Next.Image = Resources.forawrd_black;
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS ADD BUTTON                                                                                                                       //
        //================================================================================================================================================//
        private void Btn_LC_Add_MouseEnter(object sender, EventArgs e)
        {
            btn_LC_Add.ForeColor = Color.White;
            btn_LC_Add.Image = Resources.add_white;
        }

        private void Btn_LC_Add_MouseLeave(object sender, EventArgs e)
        {
            btn_LC_Add.ForeColor = Color.FromArgb(64, 64, 64);
            btn_LC_Add.Image = Resources.add_grey;
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS EDIT BUTTON                                                                                                                      //
        //================================================================================================================================================//
        private void Btn_LC_Edit_MouseEnter(object sender, EventArgs e)
        {
            btn_LC_Edit.ForeColor = Color.White;
            btn_LC_Edit.Image = Resources.edit_white;
        }

        private void Btn_LC_Edit_MouseLeave(object sender, EventArgs e)
        {
            btn_LC_Edit.ForeColor = Color.FromArgb(64, 64, 64);
            btn_LC_Edit.Image = Resources.edit_grey;
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS DONE ADDING BUTTON                                                                                                               //
        //================================================================================================================================================//
        private void Btn_LC_DoneAdd_MouseEnter(object sender, EventArgs e)
        {
            btn_LC_DoneAdd.ForeColor = Color.White;
        }

        private void Btn_LC_DoneAdd_MouseLeave(object sender, EventArgs e)
        {
            btn_LC_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS DONE EDITING BUTTON                                                                                                              //
        //================================================================================================================================================//
        private void Btn_LC_DoneEdit_MouseEnter(object sender, EventArgs e)
        {
            btn_LC_DoneEdit.ForeColor = Color.White;
        }

        private void Btn_LC_DoneEdit_MouseLeave(object sender, EventArgs e)
        {
            btn_LC_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS CANCEL BUTTON                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_LC_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_LC_Cancel.ForeColor = Color.White;
        }

        private void Btn_LC_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_LC_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // LOCAL CLIENTS DATA                                                                                                                             //
        //================================================================================================================================================//
        private void Pnl_L_CDet_VisibleChanged(object sender, EventArgs e)
        {
            if (pnl_L_CDet.Visible)
            {
                CUR_LCLIENT = 0;
                CurrentPanel("pnl_L_CDet");
                dgv_LClients.DataSource = lClientsBS;
                LoadLocalClients();

                if (dgv_LClients.Rows.Count > 0 && !string.IsNullOrEmpty(dgv_LClients.Rows[0].Cells[0].Value as string))
                    DGV_CellClick(dgv_LClients, new DataGridViewCellEventArgs(0, 0));
            }
        }


        //================================================================================================================================================//
        // CELL CLICK IN DATAGRIDVIEW                                                                                                                     //
        //================================================================================================================================================//
        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string str = dgv_LClients.Rows[e.RowIndex].Cells["Code"].Value.ToString();

                DataTable dt;
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Clients WHERE Code = '" + str + "'";
                        dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    txt_LC_CCode.Text = row["Code"].ToString().Trim();
                    txt_LC_CName.Text = row["Name"].ToString().Trim();
                }
            }
        }


        //================================================================================================================================================//
        // LOCAL CLIENT NEXT CLICK                                                                                                                        //
        //================================================================================================================================================//
        private void Btn_LC_Next_Click(object sender, EventArgs e)
        {
            if ((CUR_LCLIENT + 1) < (NUM_OF_LCLIENTS - 1))
            {
                dgv_LClients.Rows[CUR_LCLIENT].Selected = false;
                ++CUR_LCLIENT;

                if (!string.IsNullOrEmpty(dgv_LClients.Rows[CUR_LCLIENT].Cells[0].Value as string))
                    DGV_CellClick(dgv_LClients, new DataGridViewCellEventArgs(0, CUR_LCLIENT));

                dgv_LClients.Rows[CUR_LCLIENT].Selected = true;
            }
            else if ((CUR_LCLIENT + 1) == (NUM_OF_LCLIENTS - 1))
            {
                btn_LC_Next.Enabled = false;
                dgv_LClients.Rows[CUR_LCLIENT].Selected = false;
                ++CUR_LCLIENT;

                if (!string.IsNullOrEmpty(dgv_LClients.Rows[CUR_LCLIENT].Cells[0].Value as string))
                    DGV_CellClick(dgv_LClients, new DataGridViewCellEventArgs(0, CUR_LCLIENT));

                dgv_LClients.Rows[CUR_LCLIENT].Selected = true;
            }
            if (CUR_LCLIENT != 0 && !btn_LC_Prev.Enabled)
                btn_LC_Prev.Enabled = true;
        }


        //================================================================================================================================================//
        // LOCAL CLIENT PREVIOUS CLICK                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_LC_Prev_Click(object sender, EventArgs e)
        {
            if ((CUR_LCLIENT - 1) > 0)
            {
                dgv_LClients.Rows[CUR_LCLIENT].Selected = false;
                --CUR_LCLIENT;

                if (!string.IsNullOrEmpty(dgv_LClients.Rows[CUR_LCLIENT].Cells[0].Value as string))
                    DGV_CellClick(dgv_LClients, new DataGridViewCellEventArgs(0, CUR_LCLIENT));

                dgv_LClients.Rows[CUR_LCLIENT].Selected = true;
            }
            else if ((CUR_LCLIENT - 1) == 0)
            {
                btn_LC_Prev.Enabled = false;
                dgv_LClients.Rows[CUR_LCLIENT].Selected = false;
                --CUR_LCLIENT;

                if (!string.IsNullOrEmpty(dgv_LClients.Rows[CUR_LCLIENT].Cells[0].Value as string))
                    DGV_CellClick((object)dgv_LClients, new DataGridViewCellEventArgs(0, CUR_LCLIENT));

                dgv_LClients.Rows[CUR_LCLIENT].Selected = true;
            }
            if (CUR_LCLIENT != NUM_OF_LCLIENTS && !btn_LC_Next.Enabled)
                btn_LC_Next.Enabled = true;
        }


        //================================================================================================================================================//
        // LOCAL CLIENT ADD CLICK                                                                                                                         //
        //================================================================================================================================================//
        private void Btn_LC_Add_Click(object sender, EventArgs e)
        {
            isLCReadOnly = false;

            btn_LC_Add.Visible = false;
            btn_LC_Edit.Visible = false;
            btn_LC_DoneAdd.Visible = true;
            btn_LC_Cancel.Visible = true;

            txt_LC_CName.Text = string.Empty;
            txt_LC_CName.Focus();

            int newCode = 0;
            foreach (DataRow dr in lClientDT.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    int x = Convert.ToInt32(dr["Code", DataRowVersion.Original].ToString().Trim().Remove(0, 4));
                    if (x > newCode)
                        newCode = x;
                }
                else
                {
                    int x = Convert.ToInt32(dr["Code"].ToString().Trim().Remove(0, 4));
                    if (x > newCode)
                        newCode = x;
                }
            }
            txt_LC_CCode.Text = "QTL" + (newCode++).ToString("000");
        }


        //================================================================================================================================================//
        // LOCAL CLIENT DONE ADDING CLICK                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_LC_DoneAdd_Click(object sender, EventArgs e)
        {
            string newCCode = txt_LC_CCode.Text.Trim();

            StringBuilder sb = new StringBuilder().Append("Are you sure you want to add client with code: ").Append(newCCode).Append("?");

            if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Clients VALUES (@Code, @Name)", conn))
                        {
                            cmd.Parameters.AddWithValue("@Code", newCCode);
                            cmd.Parameters.AddWithValue("@Name", txt_LC_CName.Text.Trim());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("New client successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LoadLocalClients();
                        dgv_LClients.CurrentCell = dgv_LClients.Rows[dgv_LClients.Rows.Count - 1].Cells[0];

                        if (dgv_LClients.Rows.Count != 1)
                        {
                            dgv_LClients.ClearSelection();
                            int index = dgv_LClients.Rows.Count - 1;
                            dgv_LClients.Rows[index].Selected = true;
                            dgv_LClients.FirstDisplayedScrollingRowIndex = index;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btn_LC_Add.Visible = true;
                        btn_LC_Edit.Visible = true;
                        btn_LC_DoneAdd.Visible = false;
                        btn_LC_Cancel.Visible = false;
                        isLCReadOnly = true;
                    }
                }
            }
        }


        //================================================================================================================================================//
        // LOCAL CLIENT EDIT CLICK                                                                                                                        //
        //================================================================================================================================================//
        private void Btn_LC_Edit_Click(object sender, EventArgs e)
        {
            btn_LC_Add.Visible = false;
            btn_LC_Edit.Visible = false;
            btn_LC_DoneEdit.Visible = true;
            btn_LC_Cancel.Visible = true;

            isLCReadOnly = false;

            txt_LC_CName.Focus();
        }


        //================================================================================================================================================//
        // LOCAL CLIENT DONE EDITING CLICK                                                                                                                //
        //================================================================================================================================================//
        private void Btn_LC_DoneEdit_Click(object sender, EventArgs e)
        {
            string cCode = dgv_LClients.CurrentRow.Cells[0].Value.ToString().Trim();

            StringBuilder sb = new StringBuilder().Append("Are you sure you want to edit client with code: ").Append(cCode).Append("?");

            if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Clients SET Name = @Name WHERE Code = @Code", conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", txt_LC_CName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Code", cCode);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Client successfully Updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadLocalClients();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btn_LC_DoneEdit.Visible = false;
                        btn_LC_Cancel.Visible = false;
                        btn_LC_Add.Visible = true;
                        btn_LC_Edit.Visible = true;

                        isLCReadOnly = true;
                    }
                }
            }
        }


        //================================================================================================================================================//
        // LOCAL CLIENT CANCEL CLICK                                                                                                                      //
        //================================================================================================================================================//
        private void Btn_LC_Cancel_Click(object sender, EventArgs e)
        {
            isLCReadOnly = true;

            btn_LC_DoneAdd.Visible = false;
            btn_LC_DoneEdit.Visible = false;
            btn_LC_Cancel.Visible = false;
            btn_LC_Edit.Visible = true;
            btn_LC_Add.Visible = true;

            DGV_CellClick(dgv_LClients, new DataGridViewCellEventArgs(0, 0));
            if (dgv_LClients.Rows.Count != 1)
            {
                dgv_LClients.ClearSelection();
                dgv_LClients.Rows[0].Selected = true;
                dgv_LClients.FirstDisplayedScrollingRowIndex = 0;
            }
        }


        //================================================================================================================================================//
        // READONLY ON TEXTBOXES                                                                                                                          //
        //================================================================================================================================================//
        private void Txt_LC_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (isLCReadOnly)
                e.SuppressKeyPress = true;
        }

        private void Txt_LC_CName_KeyDown(object sender, KeyEventArgs e)
        {
            if (isLCReadOnly)
                e.SuppressKeyPress = true;
        }


        //================================================================================================================================================//
        // DATAGRIDVIEW FILTER AND SORT                                                                                                                   //
        //================================================================================================================================================//
        private void DGV_LClients_FilterStringChanged(object sender, EventArgs e)
        {
            lClientsBS.Filter = dgv_LClients.FilterString;
        }

        private void DGV_LClients_SortStringChanged(object sender, EventArgs e)
        {
            lClientsBS.Sort = dgv_LClients.SortString;
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS PREVIOUS BUTTON                                                                                                          //
        //================================================================================================================================================//
        private void Btn_IC_Prev_MouseEnter(object sender, EventArgs e)
        {
            btn_IC_Prev.Image = Resources.back_white;
        }

        private void Btn_IC_Prev_MouseLeave(object sender, EventArgs e)
        {
            btn_IC_Prev.Image = Resources.back_black;
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS NEXT BUTTON                                                                                                              //
        //================================================================================================================================================//
        private void Btn_IC_Next_MouseEnter(object sender, EventArgs e)
        {
            btn_IC_Next.Image = Resources.forward_white;
        }

        private void Btn_IC_Next_MouseLeave(object sender, EventArgs e)
        {
            btn_IC_Next.Image = Resources.forawrd_black;
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS ADD BUTTON                                                                                                               //
        //================================================================================================================================================//
        private void Btn_IC_Add_MouseEnter(object sender, EventArgs e)
        {
            btn_IC_Add.ForeColor = Color.White;
            btn_IC_Add.Image = Resources.add_white;
        }

        private void Btn_IC_Add_MouseLeave(object sender, EventArgs e)
        {
            btn_IC_Add.ForeColor = Color.FromArgb(64, 64, 64);
            btn_IC_Add.Image = Resources.add_grey;
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS EDIT BUTTON                                                                                                              //
        //================================================================================================================================================//
        private void Btn_IC_Edit_MouseEnter(object sender, EventArgs e)
        {
            btn_IC_Edit.ForeColor = Color.White;
            btn_IC_Edit.Image = Resources.edit_white;
        }

        private void Btn_IC_Edit_MouseLeave(object sender, EventArgs e)
        {
            btn_IC_Edit.ForeColor = Color.FromArgb(64, 64, 64);
            btn_IC_Edit.Image = Resources.edit_grey;
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS DONE ADDING BUTTON                                                                                                       //
        //================================================================================================================================================//
        private void Btn_IC_DoneAdd_MouseEnter(object sender, EventArgs e)
        {
            btn_IC_DoneAdd.ForeColor = Color.White;
        }

        private void Btn_IC_DoneAdd_MouseLeave(object sender, EventArgs e)
        {
            btn_IC_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS DONE EDITING BUTTON                                                                                                      //
        //================================================================================================================================================//
        private void Btn_IC_DoneEdit_MouseEnter(object sender, EventArgs e)
        {
            btn_IC_DoneEdit.ForeColor = Color.White;
        }

        private void Btn_IC_DoneEdit_MouseLeave(object sender, EventArgs e)
        {
            btn_IC_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS CANCEL BUTTON                                                                                                            //
        //================================================================================================================================================//
        private void Btn_IC_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_IC_Cancel.ForeColor = Color.White;
        }

        private void Btn_IC_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_IC_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENTS DATA                                                                                                                     //
        //================================================================================================================================================//
        private void Pnl_I_Clients_VisibleChanged(object sender, EventArgs e)
        {
            if (pnl_I_Clients.Visible)
            {
                CUR_ICLIENT = 0;
                CurrentPanel("pnl_I_Clients");

                dgv_IClients.DataSource = iClientsBS;
                LoadIntClients();

                if (dgv_IClients.Rows.Count > 0 && !string.IsNullOrEmpty(dgv_IClients.Rows[0].Cells[0].Value as string))
                    DGV_I_CellClick(dgv_IClients, new DataGridViewCellEventArgs(0, 0));
            }
        }


        //================================================================================================================================================//
        // DATAGRIDVIEW CELL CLICK                                                                                                                        //
        //================================================================================================================================================//
        private void DGV_I_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string clientCode = dgv_IClients.Rows[e.RowIndex].Cells["Code"].Value.ToString();

                DataTable dt;
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Int_Clients WHERE Code = '" + clientCode + "'";
                        dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    txt_IC_CCode.Text = dr["Code"].ToString().Trim();
                    txt_IC_CName.Text = dr["Name"].ToString().Trim();
                }
            }
           
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENT NEXT CLICK                                                                                                                //
        //================================================================================================================================================//
        private void Btn_IC_Next_Click(object sender, EventArgs e)
        {
            if ((CUR_ICLIENT + 1) < (NUM_OF_ICLIENTS - 1))
            {
                dgv_IClients.Rows[CUR_ICLIENT].Selected = false;
                ++CUR_ICLIENT;

                if (!string.IsNullOrEmpty(dgv_IClients.Rows[CUR_ICLIENT].Cells[0].Value as string))
                    DGV_I_CellClick(dgv_IClients, new DataGridViewCellEventArgs(0, CUR_ICLIENT));

                dgv_IClients.Rows[CUR_ICLIENT].Selected = true;
            }
            else if ((CUR_ICLIENT + 1) == (NUM_OF_ICLIENTS - 1))
            {
                btn_IC_Next.Enabled = false;
                dgv_IClients.Rows[CUR_ICLIENT].Selected = false;
                ++CUR_ICLIENT;

                if (!string.IsNullOrEmpty(dgv_IClients.Rows[CUR_ICLIENT].Cells[0].Value as string))
                    DGV_I_CellClick(dgv_IClients, new DataGridViewCellEventArgs(0, CUR_ICLIENT));

                dgv_IClients.Rows[CUR_ICLIENT].Selected = true;
            }
            if (CUR_ICLIENT != 0 && !btn_IC_Prev.Enabled)
                btn_IC_Prev.Enabled = true;
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENT PREVIOUS CLICK                                                                                                                //
        //================================================================================================================================================//
        private void Btn_IC_Prev_Click(object sender, EventArgs e)
        {
            if ((CUR_ICLIENT - 1) > 0)
            {
                dgv_IClients.Rows[CUR_ICLIENT].Selected = false;
                --CUR_ICLIENT;

                if (!string.IsNullOrEmpty(dgv_IClients.Rows[CUR_ICLIENT].Cells[0].Value as string))
                    DGV_I_CellClick(dgv_IClients, new DataGridViewCellEventArgs(0, CUR_ICLIENT));

                dgv_IClients.Rows[CUR_ICLIENT].Selected = true;
            }
            else if ((CUR_ICLIENT - 1) == 0)
            {
                btn_IC_Prev.Enabled = false;
                dgv_IClients.Rows[CUR_ICLIENT].Selected = false;
                --CUR_ICLIENT;

                if (!string.IsNullOrEmpty(dgv_IClients.Rows[CUR_ICLIENT].Cells[0].Value as string))
                    DGV_I_CellClick(dgv_IClients, new DataGridViewCellEventArgs(0, CUR_ICLIENT));

                dgv_IClients.Rows[CUR_ICLIENT].Selected = true;
            }
            if (CUR_ICLIENT != NUM_OF_ICLIENTS && !btn_IC_Next.Enabled)
                btn_IC_Next.Enabled = true;
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENT ADD CLICK                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_IC_Add_Click(object sender, EventArgs e)
        {
            isICReadOnly = false;

            btn_IC_Add.Visible = false;
            btn_IC_Edit.Visible = false;
            btn_IC_DoneAdd.Visible = true;
            btn_IC_Cancel.Visible = true;

            txt_IC_CName.Text = string.Empty;
            txt_IC_CName.Focus();

            int newCCode = 0;
            foreach (DataRow dr in iClientDT.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    int x = Convert.ToInt32(dr["Code", DataRowVersion.Original].ToString().Trim().Remove(0, 4));
                    if (x > newCCode)
                        newCCode = x;
                }
                else
                {
                    int x = Convert.ToInt32(dr["Code"].ToString().Trim().Remove(0, 4));
                    if (x > newCCode)
                        newCCode = x;
                }
            }

            txt_IC_CCode.Text = "QTI" + (newCCode++).ToString("000");
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENT DONE ADDING CLICK                                                                                                         //
        //================================================================================================================================================//
        private void Btn_IC_DoneAdd_Click(object sender, EventArgs e)
        {
            string newCCode = txt_IC_CCode.Text.Trim();

            StringBuilder sb = new StringBuilder().Append("Are you sure you want to add client with code: ").Append(newCCode).Append("?");

            if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Int_Clients VALUES (@Code, @Name)", conn))
                        {
                            cmd.Parameters.AddWithValue("@Code", newCCode);
                            cmd.Parameters.AddWithValue("@Name", txt_IC_CName.Text.Trim());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("New client successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LoadIntClients();
                        dgv_IClients.CurrentCell = dgv_IClients.Rows[dgv_IClients.Rows.Count - 1].Cells[0];

                        if (dgv_IClients.Rows.Count != 1)
                        {
                            dgv_IClients.ClearSelection();
                            int index = dgv_IClients.Rows.Count - 1;
                            dgv_IClients.Rows[index].Selected = true;
                            dgv_IClients.FirstDisplayedScrollingRowIndex = index;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btn_IC_Add.Visible = true;
                        btn_IC_Edit.Visible = true;
                        btn_IC_DoneAdd.Visible = false;
                        btn_IC_Cancel.Visible = false;

                        isICReadOnly = true;
                    }
                }
            }
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENT EDIT CLICK                                                                                                                //
        //================================================================================================================================================//
        private void Btn_IC_Edit_Click(object sender, EventArgs e)
        {
            btn_IC_Add.Visible = false;
            btn_IC_Edit.Visible = false;
            btn_IC_DoneEdit.Visible = true;
            btn_IC_Cancel.Visible = true;

            isICReadOnly = false;

            txt_IC_CName.Focus();
        }

        //================================================================================================================================================//
        // INTERNATIONAL CLIENT DONE EDITTING CLICK                                                                                                       //
        //================================================================================================================================================//
        private void Btn_IC_DoneEdit_Click(object sender, EventArgs e)
        {
            string cCode = dgv_IClients.CurrentRow.Cells[0].Value.ToString().Trim();

            StringBuilder sb = new StringBuilder().Append("Are you sure you want to edit client with code: ").Append(cCode).Append("?");

            if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Int_Clients SET Name = @Name WHERE Code = @Code", conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", txt_IC_CName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Code", cCode);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Client successfully Updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadIntClients();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btn_IC_DoneEdit.Visible = false;
                        btn_IC_Cancel.Visible = false;
                        btn_IC_Add.Visible = true;
                        btn_IC_Edit.Visible = true;

                        isICReadOnly = true;
                    }
                }
            }
        }


        //================================================================================================================================================//
        // INTERNATIONAL CLIENT CANCEL CLICK                                                                                                              //
        //================================================================================================================================================//
        private void Btn_IC_Cancel_Click(object sender, EventArgs e)
        {
            isICReadOnly = true;

            btn_IC_DoneAdd.Visible = false;
            btn_IC_DoneEdit.Visible = false;
            btn_IC_Cancel.Visible = false;
            btn_IC_Edit.Visible = true;
            btn_IC_Add.Visible = true;

            DGV_I_CellClick(dgv_IClients, new DataGridViewCellEventArgs(0, 0));
            if (dgv_IClients.Rows.Count != 1)
            {
                dgv_IClients.ClearSelection();
                dgv_IClients.Rows[0].Selected = true;
                dgv_IClients.FirstDisplayedScrollingRowIndex = 0;
            }
        }


        //================================================================================================================================================//
        // ENFORCE READONLY ON TEXTBOXES                                                                                                                  //
        //================================================================================================================================================//
        private void Txt_IC_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (isICReadOnly)
                e.SuppressKeyPress = true;
        }

        private void Txt_IC_CName_KeyDown(object sender, KeyEventArgs e)
        {
            if (isICReadOnly)
                e.SuppressKeyPress = true;
        }


        //================================================================================================================================================//
        // DATAGRIDVIEW FILTER AND SORT                                                                                                                   //
        //================================================================================================================================================//
        private void DGV_IClients_FilterStringChanged(object sender, EventArgs e)
        {
            iClientsBS.Filter = dgv_IClients.FilterString;
        }

        private void DGV_IClients_SortStringChanged(object sender, EventArgs e)
        {
            iClientsBS.Sort = dgv_IClients.SortString;
        }
    }
}
