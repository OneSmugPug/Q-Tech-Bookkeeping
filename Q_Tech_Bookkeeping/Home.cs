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

        private Orders frmOrder;
        private Quotes frmQuote;
        private Invoices_Send frmInvSent;
        private Inv_Rec frmInvRec;
        private Int_Orders frmIntOrders;
        private Int_Quotes frmIntQuotes;
        private Int_Invoices_Send frmIntInvSent;
        private Contractors frmContr;
        private PettyCash frmPetty;
        private Projects frmProj;

        private const int cGrip = 16;
        private const int cCaption = 32;
        private const int SnapDist = 1;

        private Panel panel1;
        private PictureBox pictureBox1;

        private Button btn_Home;
        private Button btn_Local;
        private Button btn_L_Invoices;
        private Button btn_L_Quotes;
        private Button btn_L_Orders;
        private Button btn_L_Clients;
        private Button btn_Int;
        private Button btn_I_Clients;
        private Button btn_I_Quotes;
        private Button btn_I_Orders;
        private Button btn_L_InvSent;
        private Button btn_L_InvRec;
        private Button btn_I_InvSent;
        private Button btn_Contractors;
        private Button btn_LC_Prev;
        private Button btn_LC_Next;
        private Button btn_LC_Edit;
        private Button btn_LC_Add;
        private Button btn_Home_Close;
        private Button btn_Home_Max;
        private Button btn_Home_Nor;
        private Button btn_LC_DoneEdit;
        private Button btn_LC_DoneAdd;
        private Button btn_LC_Cancel;
        private Button btn_Home_Min;
        private Button btn_IC_Cancel;
        private Button btn_IC_DoneAdd;
        private Button btn_IC_DoneEdit;
        private Button btn_IC_Edit;
        private Button btn_IC_Add;
        private Button btn_IC_Next;
        private Button btn_IC_Prev;
        private Button btn_C_NoInv;
        private Button btn_C_NoRem;
        private Button btn_C_Timesheets;
        private Button btn_Projects;
        private Button btn_L_PettyCash;

        private Panel pnl_Local;
        private Panel pnl_Int;
        private Panel pnl_L_Inv;
        private Panel pnl_L_CDet;
        private Panel pnl_L_Orders;
        private Panel pnl_L_Quotes;
        private Panel pnl_L_InvSent;
        private Panel pnl_L_InvRec;
        private Panel pnl_I_Clients;
        private Panel pnl_I_Orders;
        private Panel pnl_I_Quotes;
        private Panel pnl_I_InvSent;
        private Panel pnl_Contractors;
        private Label lblComing;
        private Panel pnl_Home;
        private Panel pnl_Con;
        private Panel pnl_C_NoRem;
        private Panel pnl_Projects;
        private Panel pnl_C_NoInv;
        private Panel pnl_L_PettyCash;

        private Timer tmr_Local;
        private Timer tmr_Int;
        private Timer tmr_L_Inv;
        private Timer tmr_Con;

        private BunifuCustomLabel bunifuCustomLabel2;
        private BunifuCustomLabel bunifuCustomLabel1;
        private BunifuCustomLabel bunifuCustomLabel3;
        private BunifuCustomLabel bunifuCustomLabel4;

        private BunifuSeparator bunifuSeparator1;
        private BunifuSeparator bunifuSeparator2;

        private BunifuMaterialTextbox txt_LC_CName;
        private BunifuMaterialTextbox txt_LC_CCode;
        private BunifuMaterialTextbox txt_IC_CName;
        private BunifuMaterialTextbox txt_IC_CCode;

        private AdvancedDataGridView dgv_LClients;
        private AdvancedDataGridView dgv_IClients;
        private AdvancedDataGridView dgv_NoRem;
        private AdvancedDataGridView dgv_NoInv;

        private BindingSource clientsBindingSource1;

        public Home()
        {
            this.InitializeComponent();
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
                        pnl_Contractors.Visible = false;
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

            frmOrder = new Orders();
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

            frmInvSent = new Invoices_Send();
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
            if (!(this.selected != "lInvSent"))
                return;
            this.btn_L_InvSent.BackColor = Color.FromArgb(35, 35, 35);
            this.btn_L_InvSent.ForeColor = Color.White;
        }

        private void btn_L_InvRec_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_L_InvRec.Visible = true;
            this.CurrentPanel("pnl_L_InvRec");
            this.btn_L_InvRec.BackColor = Color.FromArgb(13, 77, 119);
            this.btn_L_InvRec.ForeColor = Color.White;
            this.frmInvRec = new Inv_Rec();
            this.curForm = (object)this.frmInvRec;
            this.frmInvRec.TopLevel = false;
            this.frmInvRec.TopMost = true;
            this.pnl_L_InvRec.Controls.Add((Control)this.frmInvRec);
            this.frmInvRec.Show();
        }

        private void btn_L_InvRec_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "lInvRec"))
                return;
            this.btn_L_InvRec.BackColor = Color.FromArgb(73, 73, 73);
            this.btn_L_InvRec.ForeColor = Color.FromArgb(19, 118, 188);
        }

        private void btn_L_InvRec_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "lInvRec"))
                return;
            this.btn_L_InvRec.BackColor = Color.FromArgb(35, 35, 35);
            this.btn_L_InvRec.ForeColor = Color.White;
        }

        private void btn_L_PettyCash_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_L_PettyCash.Visible = true;
            this.CurrentPanel("pnl_L_PettyCash");
            this.btn_L_PettyCash.BackColor = Color.FromArgb(13, 77, 119);
            this.btn_L_PettyCash.ForeColor = Color.White;
            this.frmPetty = new PettyCash();
            this.curForm = (object)this.frmPetty;
            this.frmPetty.TopLevel = false;
            this.frmPetty.TopMost = true;
            this.pnl_L_PettyCash.Controls.Add((Control)this.frmPetty);
            this.frmPetty.Show();
        }

        private void btn_L_PettyCash_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "lPettyCash"))
                return;
            this.btn_L_PettyCash.BackColor = Color.FromArgb(73, 73, 73);
            this.btn_L_PettyCash.ForeColor = Color.FromArgb(19, 118, 188);
        }

        private void btn_L_PettyCash_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "lPettyCash"))
                return;
            this.btn_L_PettyCash.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_L_PettyCash.ForeColor = Color.White;
        }

        private void btn_Int_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            if (this.isLocalOpen && !this.isLInvOpen)
                this.tmr_Local.Start();
            else if (this.isLocalOpen && this.isLInvOpen)
            {
                this.tmr_L_Inv.Start();
                this.tmr_Local.Start();
            }
            if (this.isConOpen)
                this.tmr_Con.Start();
            this.btn_Int.BackColor = Color.FromArgb(19, 118, 188);
            this.btn_Int.ForeColor = Color.White;
            this.btn_Int.Image = (Image)Resources.globe_white;
            this.btn_I_Clients.BackColor = Color.FromArgb(15, 91, 142);
            this.btn_I_Clients.ForeColor = Color.White;
            this.pnl_I_Clients.Visible = true;
            this.tmr_Int.Start();
        }

        private void btn_Int_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "Int"))
                return;
            this.btn_Int.BackColor = Color.FromArgb(73, 73, 73);
            this.btn_Int.ForeColor = Color.FromArgb(19, 118, 188);
            this.btn_Int.Image = (Image)Resources.globe_blue;
        }

        private void btn_Int_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "Int"))
                return;
            this.btn_Int.BackColor = Color.FromArgb(64, 64, 64);
            this.btn_Int.ForeColor = Color.White;
            this.btn_Int.Image = (Image)Resources.globe_white;
        }

        private void btn_I_Clients_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.btn_I_Clients.BackColor = Color.FromArgb(15, 91, 142);
            this.btn_I_Clients.ForeColor = Color.White;
            this.pnl_I_Clients.Visible = true;
        }

        private void btn_I_Clients_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "iClients"))
                return;
            this.btn_I_Clients.BackColor = Color.FromArgb(56, 56, 56);
            this.btn_I_Clients.ForeColor = Color.FromArgb(15, 91, 142);
        }

        private void btn_I_Clients_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "iClients"))
                return;
            this.btn_I_Clients.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_I_Clients.ForeColor = Color.White;
        }

        private void btn_I_Orders_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_I_Orders.Visible = true;
            this.CurrentPanel("pnl_I_Orders");
            this.btn_I_Orders.BackColor = Color.FromArgb(15, 91, 142);
            this.btn_I_Orders.ForeColor = Color.White;
            this.frmIntOrders = new Int_Orders();
            this.curForm = (object)this.frmIntOrders;
            this.frmIntOrders.TopLevel = false;
            this.frmIntOrders.TopMost = true;
            this.pnl_I_Orders.Controls.Add((Control)this.frmIntOrders);
            this.frmIntOrders.Show();
        }

        private void btn_I_Orders_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "iOrders"))
                return;
            this.btn_I_Orders.BackColor = Color.FromArgb(56, 56, 56);
            this.btn_I_Orders.ForeColor = Color.FromArgb(15, 91, 142);
        }

        private void btn_I_Orders_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "iOrders"))
                return;
            this.btn_I_Orders.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_I_Orders.ForeColor = Color.White;
        }

        private void btn_I_Quotes_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_I_Quotes.Visible = true;
            this.CurrentPanel("pnl_I_Quotes");
            this.btn_I_Quotes.BackColor = Color.FromArgb(15, 91, 142);
            this.btn_I_Quotes.ForeColor = Color.White;
            this.frmIntQuotes = new Int_Quotes();
            this.curForm = (object)this.frmIntQuotes;
            this.frmIntQuotes.TopLevel = false;
            this.frmIntQuotes.TopMost = true;
            this.pnl_I_Quotes.Controls.Add((Control)this.frmIntQuotes);
            this.frmIntQuotes.Show();
        }

        private void btn_I_Quotes_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "iQuotes"))
                return;
            this.btn_I_Quotes.BackColor = Color.FromArgb(56, 56, 56);
            this.btn_I_Quotes.ForeColor = Color.FromArgb(15, 91, 142);
        }

        private void btn_I_Quotes_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "iQuotes"))
                return;
            this.btn_I_Quotes.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_I_Quotes.ForeColor = Color.White;
        }

        private void btn_I_InvSent_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_I_InvSent.Visible = true;
            this.CurrentPanel("pnl_I_InvSent");
            this.btn_I_InvSent.BackColor = Color.FromArgb(13, 77, 119);
            this.btn_I_InvSent.ForeColor = Color.White;
            this.frmIntInvSent = new Int_Invoices_Send();
            this.curForm = (object)this.frmIntInvSent;
            this.frmIntInvSent.TopLevel = false;
            this.frmIntInvSent.TopMost = true;
            this.pnl_I_InvSent.Controls.Add((Control)this.frmIntInvSent);
            this.frmIntInvSent.Show();
        }

        private void btn_I_InvSent_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "iInvSent"))
                return;
            this.btn_I_InvSent.BackColor = Color.FromArgb(56, 56, 56);
            this.btn_I_InvSent.ForeColor = Color.FromArgb(15, 91, 142);
        }

        private void btn_I_InvSent_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "iInvSent"))
                return;
            this.btn_I_InvSent.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_I_InvSent.ForeColor = Color.White;
        }

        private void btn_Contractors_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            if (this.isLocalOpen && !this.isLInvOpen)
                this.tmr_Local.Start();
            else if (this.isLocalOpen && this.isLInvOpen)
            {
                this.tmr_L_Inv.Start();
                this.tmr_Local.Start();
            }
            if (this.isIntOpen)
                this.tmr_Int.Start();
            this.btn_Contractors.BackColor = Color.FromArgb(19, 118, 188);
            this.btn_Contractors.ForeColor = Color.White;
            this.btn_Contractors.Image = (Image)Resources.contr_white;
            this.tmr_Con.Start();
            this.pnl_Contractors.Visible = true;
            this.CurrentPanel("pnl_Contractors");
            this.btn_C_Timesheets.BackColor = Color.FromArgb(13, 77, 119);
            this.btn_C_Timesheets.ForeColor = Color.White;
            this.frmContr = new Contractors();
            this.curForm = (object)this.frmContr;
            this.frmContr.TopLevel = false;
            this.frmContr.TopMost = true;
            this.pnl_Contractors.Controls.Add((Control)this.frmContr);
            this.frmContr.Show();
        }

        private void btn_Contractors_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "Contractors"))
                return;
            this.btn_Contractors.BackColor = Color.FromArgb(73, 73, 73);
            this.btn_Contractors.ForeColor = Color.FromArgb(19, 118, 188);
            this.btn_Contractors.Image = (Image)Resources.contr_blue;
        }

        private void btn_Contractors_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "Contractors"))
                return;
            this.btn_Contractors.BackColor = Color.FromArgb(64, 64, 64);
            this.btn_Contractors.ForeColor = Color.White;
            this.btn_Contractors.Image = (Image)Resources.contr_white;
        }

        private void btn_C_Timesheets_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_Contractors.Visible = true;
            this.CurrentPanel("pnl_Contractors");
            this.btn_C_Timesheets.BackColor = Color.FromArgb(13, 77, 119);
            this.btn_C_Timesheets.ForeColor = Color.White;
            this.frmContr = new Contractors();
            this.curForm = (object)this.frmContr;
            this.frmContr.TopLevel = false;
            this.frmContr.TopMost = true;
            this.pnl_Contractors.Controls.Add((Control)this.frmContr);
            this.frmContr.Show();
        }

        private void btn_C_Timesheets_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "cTimesheets"))
                return;
            this.btn_C_Timesheets.BackColor = Color.FromArgb(56, 56, 56);
            this.btn_C_Timesheets.ForeColor = Color.FromArgb(15, 91, 142);
        }

        private void btn_C_Timesheets_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "cTimesheets"))
                return;
            this.btn_C_Timesheets.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_C_Timesheets.ForeColor = Color.White;
        }

        private void btn_C_NoRem_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_C_NoRem.Visible = true;
            this.CurrentPanel("pnl_C_NoRem");
            this.btn_C_NoRem.BackColor = Color.FromArgb(15, 91, 142);
            this.btn_C_NoRem.ForeColor = Color.White;
            this.dgv_NoRem.DataSource = (object)this.conNRBS;
            this.loadNoRemittances();
            this.dgv_NoRem.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            this.dgv_NoRem.Columns[4].DefaultCellStyle.Format = "c";
            this.dgv_NoRem.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoRem.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            this.dgv_NoRem.Columns[5].DefaultCellStyle.Format = "c";
            this.dgv_NoRem.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoRem.Columns[6].DefaultCellStyle.Format = "c";
            this.dgv_NoRem.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoRem.Columns[7].DefaultCellStyle.Format = "c";
            this.dgv_NoRem.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoRem.Columns[8].DefaultCellStyle.Format = "c";
            this.dgv_NoRem.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoRem.Columns[9].DefaultCellStyle.Format = "c";
            this.dgv_NoRem.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void loadNoRemittances()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                new SqlDataAdapter("SELECT * FROM Contractor_Hours WHERE Remittance = 'No'", dbConnection).Fill(dataTable);
            this.conNRBS.DataSource = (object)dataTable;
        }

        private void btn_C_NoRem_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "cNoRem"))
                return;
            this.btn_C_NoRem.BackColor = Color.FromArgb(56, 56, 56);
            this.btn_C_NoRem.ForeColor = Color.FromArgb(15, 91, 142);
        }

        private void btn_C_NoRem_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "cNoRem"))
                return;
            this.btn_C_NoRem.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_C_NoRem.ForeColor = Color.White;
        }

        private void btn_C_NoInv_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_C_NoInv.Visible = true;
            this.CurrentPanel("pnl_C_NoInv");
            this.btn_C_NoInv.BackColor = Color.FromArgb(15, 91, 142);
            this.btn_C_NoInv.ForeColor = Color.White;
            this.dgv_NoInv.DataSource = (object)this.conNIBS;
            this.loadNoInvoices();
            this.dgv_NoInv.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            this.dgv_NoInv.Columns[4].DefaultCellStyle.Format = "c";
            this.dgv_NoInv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoInv.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider)CultureInfo.GetCultureInfo("en-US");
            this.dgv_NoInv.Columns[5].DefaultCellStyle.Format = "c";
            this.dgv_NoInv.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoInv.Columns[6].DefaultCellStyle.Format = "c";
            this.dgv_NoInv.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoInv.Columns[7].DefaultCellStyle.Format = "c";
            this.dgv_NoInv.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoInv.Columns[8].DefaultCellStyle.Format = "c";
            this.dgv_NoInv.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_NoInv.Columns[9].DefaultCellStyle.Format = "c";
            this.dgv_NoInv.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void loadNoInvoices()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                new SqlDataAdapter("SELECT * FROM Contractor_Hours WHERE Invoice_Received = 'No'", dbConnection).Fill(dataTable);
            this.conNIBS.DataSource = (object)dataTable;
        }

        private void btn_C_NoInv_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "cNoInv"))
                return;
            this.btn_C_NoInv.BackColor = Color.FromArgb(56, 56, 56);
            this.btn_C_NoInv.ForeColor = Color.FromArgb(15, 91, 142);
        }

        private void btn_C_NoInv_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "cNoInv"))
                return;
            this.btn_C_NoInv.BackColor = Color.FromArgb(50, 50, 50);
            this.btn_C_NoInv.ForeColor = Color.White;
        }

        private void btn_Projects_Click(object sender, EventArgs e)
        {
            ResetButtons(this.selected);
            GetSelectedButton(sender);
            this.HidePanel();
            this.pnl_Projects.Visible = true;
            this.CurrentPanel("pnl_Projects");
            if (this.isLocalOpen && !this.isLInvOpen)
                this.tmr_Local.Start();
            else if (this.isLocalOpen && this.isLInvOpen)
            {
                this.tmr_L_Inv.Start();
                this.tmr_Local.Start();
            }
            if (this.isIntOpen)
                this.tmr_Int.Start();
            if (this.isConOpen)
                this.tmr_Con.Start();
            this.btn_Projects.BackColor = Color.FromArgb(19, 118, 188);
            this.btn_Projects.ForeColor = Color.White;
            this.btn_Projects.Image = (Image)Resources.project_white;
            this.frmProj = new Projects();
            this.curForm = (object)this.frmProj;
            this.frmProj.TopLevel = false;
            this.frmProj.TopMost = true;
            this.pnl_Projects.Controls.Add((Control)this.frmProj);
            this.frmProj.Show();
        }

        private void btn_Projects_MouseEnter(object sender, EventArgs e)
        {
            if (!(this.selected != "Projects"))
                return;
            this.btn_Projects.BackColor = Color.FromArgb(73, 73, 73);
            this.btn_Projects.ForeColor = Color.FromArgb(19, 118, 188);
            this.btn_Projects.Image = (Image)Resources.project_blue;
        }

        private void btn_Projects_MouseLeave(object sender, EventArgs e)
        {
            if (!(this.selected != "Projects"))
                return;
            this.btn_Projects.BackColor = Color.FromArgb(64, 64, 64);
            this.btn_Projects.ForeColor = Color.White;
            this.btn_Projects.Image = (Image)Resources.project_white;
        }

        public void setManageProjects(Manage_Proj frmMP, Home frmHome)
        {
            frmMP.TopLevel = false;
            frmMP.TopMost = true;
            this.pnl_Projects.Controls.Add((Control)frmMP);
            frmMP.setHome(frmHome);
            frmMP.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.isLocalOpen)
            {
                if (this.pnl_Local.Height <= 48)
                {
                    this.tmr_Local.Stop();
                    this.isLocalOpen = false;
                }
                else
                {
                    this.pnl_Local.Height -= 15;
                    Panel pnlInt = this.pnl_Int;
                    int x1 = this.pnl_Int.Location.X;
                    Point location = this.pnl_Int.Location;
                    int y1 = location.Y - 15;
                    Point point1 = new Point(x1, y1);
                    pnlInt.Location = point1;
                    Panel pnlCon = this.pnl_Con;
                    location = this.pnl_Con.Location;
                    int x2 = location.X;
                    location = this.pnl_Con.Location;
                    int y2 = location.Y - 15;
                    Point point2 = new Point(x2, y2);
                    pnlCon.Location = point2;
                    Button btnProjects = this.btn_Projects;
                    location = this.btn_Projects.Location;
                    int x3 = location.X;
                    location = this.btn_Projects.Location;
                    int y3 = location.Y - 15;
                    Point point3 = new Point(x3, y3);
                    btnProjects.Location = point3;
                }
            }
            else if (this.pnl_Local.Height >= 288)
            {
                this.tmr_Local.Stop();
                this.isLocalOpen = true;
            }
            else
            {
                this.pnl_Local.Height += 15;
                Panel pnlInt = this.pnl_Int;
                int x1 = this.pnl_Int.Location.X;
                Point location = this.pnl_Int.Location;
                int y1 = location.Y + 15;
                Point point1 = new Point(x1, y1);
                pnlInt.Location = point1;
                Panel pnlCon = this.pnl_Con;
                location = this.pnl_Con.Location;
                int x2 = location.X;
                location = this.pnl_Con.Location;
                int y2 = location.Y + 15;
                Point point2 = new Point(x2, y2);
                pnlCon.Location = point2;
                Button btnProjects = this.btn_Projects;
                location = this.btn_Projects.Location;
                int x3 = location.X;
                location = this.btn_Projects.Location;
                int y3 = location.Y + 15;
                Point point3 = new Point(x3, y3);
                btnProjects.Location = point3;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.isIntOpen)
            {
                if (this.pnl_Int.Height <= 48)
                {
                    this.tmr_Int.Stop();
                    this.isIntOpen = false;
                }
                else if (this.pnl_Int.Height == 60)
                {
                    this.pnl_Int.Height -= 12;
                    Panel pnlCon = this.pnl_Con;
                    int x1 = this.pnl_Con.Location.X;
                    Point location = this.pnl_Con.Location;
                    int y1 = location.Y - 12;
                    Point point1 = new Point(x1, y1);
                    pnlCon.Location = point1;
                    Button btnProjects = this.btn_Projects;
                    location = this.btn_Projects.Location;
                    int x2 = location.X;
                    location = this.btn_Projects.Location;
                    int y2 = location.Y - 12;
                    Point point2 = new Point(x2, y2);
                    btnProjects.Location = point2;
                }
                else
                {
                    this.pnl_Int.Height -= 15;
                    Panel pnlCon = this.pnl_Con;
                    int x1 = this.pnl_Con.Location.X;
                    Point location = this.pnl_Con.Location;
                    int y1 = location.Y - 15;
                    Point point1 = new Point(x1, y1);
                    pnlCon.Location = point1;
                    Button btnProjects = this.btn_Projects;
                    location = this.btn_Projects.Location;
                    int x2 = location.X;
                    location = this.btn_Projects.Location;
                    int y2 = location.Y - 15;
                    Point point2 = new Point(x2, y2);
                    btnProjects.Location = point2;
                }
            }
            else if (this.pnl_Int.Height >= 240)
            {
                this.tmr_Int.Stop();
                this.isIntOpen = true;
            }
            else if (this.pnl_Int.Height == 228)
            {
                this.pnl_Int.Height += 12;
                Panel pnlCon = this.pnl_Con;
                int x1 = this.pnl_Con.Location.X;
                Point location = this.pnl_Con.Location;
                int y1 = location.Y + 12;
                Point point1 = new Point(x1, y1);
                pnlCon.Location = point1;
                Button btnProjects = this.btn_Projects;
                location = this.btn_Projects.Location;
                int x2 = location.X;
                location = this.btn_Projects.Location;
                int y2 = location.Y + 12;
                Point point2 = new Point(x2, y2);
                btnProjects.Location = point2;
            }
            else
            {
                this.pnl_Int.Height += 15;
                Panel pnlCon = this.pnl_Con;
                int x1 = this.pnl_Con.Location.X;
                Point location = this.pnl_Con.Location;
                int y1 = location.Y + 15;
                Point point1 = new Point(x1, y1);
                pnlCon.Location = point1;
                Button btnProjects = this.btn_Projects;
                location = this.btn_Projects.Location;
                int x2 = location.X;
                location = this.btn_Projects.Location;
                int y2 = location.Y + 15;
                Point point2 = new Point(x2, y2);
                btnProjects.Location = point2;
            }
        }

        private void tmr_Con_Tick(object sender, EventArgs e)
        {
            if (this.isConOpen)
            {
                if (this.pnl_Con.Height <= 48)
                {
                    this.tmr_Con.Stop();
                    this.isConOpen = false;
                }
                else if (this.pnl_Con.Height == 57)
                {
                    this.pnl_Con.Height -= 9;
                    this.btn_Projects.Location = new Point(this.btn_Projects.Location.X, this.btn_Projects.Location.Y - 9);
                }
                else
                {
                    this.pnl_Con.Height -= 15;
                    this.btn_Projects.Location = new Point(this.btn_Projects.Location.X, this.btn_Projects.Location.Y - 15);
                }
            }
            else if (this.pnl_Con.Height >= 192)
            {
                this.tmr_Con.Stop();
                this.isConOpen = true;
            }
            else if (this.pnl_Con.Height == 183)
            {
                this.pnl_Con.Height += 9;
                this.btn_Projects.Location = new Point(this.btn_Projects.Location.X, this.btn_Projects.Location.Y + 9);
            }
            else
            {
                this.pnl_Con.Height += 15;
                this.btn_Projects.Location = new Point(this.btn_Projects.Location.X, this.btn_Projects.Location.Y + 15);
            }
        }

        private void tmr_L_Inv_Tick(object sender, EventArgs e)
        {
            if (this.isLInvOpen)
            {
                if (this.pnl_L_Inv.Height <= 48)
                {
                    this.tmr_L_Inv.Stop();
                    this.isLInvOpen = false;
                }
                else if (this.pnl_L_Inv.Height == 54)
                {
                    this.pnl_Local.Height -= 6;
                    this.pnl_L_Inv.Height -= 6;
                    this.pnl_Int.Location = new Point(this.pnl_Int.Location.X, this.pnl_Int.Location.Y - 6);
                    Panel pnlCon = this.pnl_Con;
                    Point location = this.pnl_Con.Location;
                    int x1 = location.X;
                    location = this.pnl_Con.Location;
                    int y1 = location.Y - 6;
                    Point point1 = new Point(x1, y1);
                    pnlCon.Location = point1;
                    Button btnProjects = this.btn_Projects;
                    location = this.btn_Projects.Location;
                    int x2 = location.X;
                    location = this.btn_Projects.Location;
                    int y2 = location.Y - 6;
                    Point point2 = new Point(x2, y2);
                    btnProjects.Location = point2;
                    Button btnLPettyCash = this.btn_L_PettyCash;
                    location = this.btn_L_PettyCash.Location;
                    int x3 = location.X;
                    location = this.btn_L_PettyCash.Location;
                    int y3 = location.Y - 6;
                    Point point3 = new Point(x3, y3);
                    btnLPettyCash.Location = point3;
                }
                else
                {
                    this.pnl_Local.Height -= 15;
                    this.pnl_L_Inv.Height -= 15;
                    this.pnl_Int.Location = new Point(this.pnl_Int.Location.X, this.pnl_Int.Location.Y - 15);
                    Panel pnlCon = this.pnl_Con;
                    Point location = this.pnl_Con.Location;
                    int x1 = location.X;
                    location = this.pnl_Con.Location;
                    int y1 = location.Y - 15;
                    Point point1 = new Point(x1, y1);
                    pnlCon.Location = point1;
                    Button btnProjects = this.btn_Projects;
                    location = this.btn_Projects.Location;
                    int x2 = location.X;
                    location = this.btn_Projects.Location;
                    int y2 = location.Y - 15;
                    Point point2 = new Point(x2, y2);
                    btnProjects.Location = point2;
                    Button btnLPettyCash = this.btn_L_PettyCash;
                    location = this.btn_L_PettyCash.Location;
                    int x3 = location.X;
                    location = this.btn_L_PettyCash.Location;
                    int y3 = location.Y - 15;
                    Point point3 = new Point(x3, y3);
                    btnLPettyCash.Location = point3;
                }
            }
            else if (this.pnl_L_Inv.Height >= 144)
            {
                this.tmr_L_Inv.Stop();
                this.isLInvOpen = true;
            }
            else if (this.pnl_L_Inv.Height == 138)
            {
                this.pnl_Local.Height += 6;
                this.pnl_L_Inv.Height += 6;
                this.pnl_Int.Location = new Point(this.pnl_Int.Location.X, this.pnl_Int.Location.Y + 6);
                Panel pnlCon = this.pnl_Con;
                Point location = this.pnl_Con.Location;
                int x1 = location.X;
                location = this.pnl_Con.Location;
                int y1 = location.Y + 6;
                Point point1 = new Point(x1, y1);
                pnlCon.Location = point1;
                Button btnProjects = this.btn_Projects;
                location = this.btn_Projects.Location;
                int x2 = location.X;
                location = this.btn_Projects.Location;
                int y2 = location.Y + 6;
                Point point2 = new Point(x2, y2);
                btnProjects.Location = point2;
                Button btnLPettyCash = this.btn_L_PettyCash;
                location = this.btn_L_PettyCash.Location;
                int x3 = location.X;
                location = this.btn_L_PettyCash.Location;
                int y3 = location.Y + 6;
                Point point3 = new Point(x3, y3);
                btnLPettyCash.Location = point3;
            }
            else
            {
                this.pnl_Local.Height += 15;
                this.pnl_L_Inv.Height += 15;
                this.pnl_Int.Location = new Point(this.pnl_Int.Location.X, this.pnl_Int.Location.Y + 15);
                Panel pnlCon = this.pnl_Con;
                Point location = this.pnl_Con.Location;
                int x1 = location.X;
                location = this.pnl_Con.Location;
                int y1 = location.Y + 15;
                Point point1 = new Point(x1, y1);
                pnlCon.Location = point1;
                Button btnProjects = this.btn_Projects;
                location = this.btn_Projects.Location;
                int x2 = location.X;
                location = this.btn_Projects.Location;
                int y2 = location.Y + 15;
                Point point2 = new Point(x2, y2);
                btnProjects.Location = point2;
                Button btnLPettyCash = this.btn_L_PettyCash;
                location = this.btn_L_PettyCash.Location;
                int x3 = location.X;
                location = this.btn_L_PettyCash.Location;
                int y3 = location.Y + 15;
                Point point3 = new Point(x3, y3);
                btnLPettyCash.Location = point3;
            }
        }

        private void btn_LC_Prev_MouseEnter(object sender, EventArgs e)
        {
            this.btn_LC_Prev.Image = (Image)Resources.back_white;
        }

        private void btn_LC_Prev_MouseLeave(object sender, EventArgs e)
        {
            this.btn_LC_Prev.Image = (Image)Resources.back_black;
        }

        private void btn_LC_Next_MouseEnter(object sender, EventArgs e)
        {
            this.btn_LC_Next.Image = (Image)Resources.forward_white;
        }

        private void btn_LC_Next_MouseLeave(object sender, EventArgs e)
        {
            this.btn_LC_Next.Image = (Image)Resources.forawrd_black;
        }

        private void btn_LC_Add_MouseEnter(object sender, EventArgs e)
        {
            this.btn_LC_Add.ForeColor = Color.White;
            this.btn_LC_Add.Image = (Image)Resources.add_white;
        }

        private void btn_LC_Add_MouseLeave(object sender, EventArgs e)
        {
            this.btn_LC_Add.ForeColor = Color.FromArgb(64, 64, 64);
            this.btn_LC_Add.Image = (Image)Resources.add_grey;
        }

        private void btn_LC_Edit_MouseEnter(object sender, EventArgs e)
        {
            this.btn_LC_Edit.ForeColor = Color.White;
            this.btn_LC_Edit.Image = (Image)Resources.edit_white;
        }

        private void btn_LC_Edit_MouseLeave(object sender, EventArgs e)
        {
            this.btn_LC_Edit.ForeColor = Color.FromArgb(64, 64, 64);
            this.btn_LC_Edit.Image = (Image)Resources.edit_grey;
        }

        private void btn_LC_DoneAdd_MouseEnter(object sender, EventArgs e)
        {
            this.btn_LC_DoneAdd.ForeColor = Color.White;
        }

        private void btn_LC_DoneAdd_MouseLeave(object sender, EventArgs e)
        {
            this.btn_LC_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void btn_LC_DoneEdit_MouseEnter(object sender, EventArgs e)
        {
            this.btn_LC_DoneEdit.ForeColor = Color.White;
        }

        private void btn_LC_DoneEdit_MouseLeave(object sender, EventArgs e)
        {
            this.btn_LC_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void btn_LC_Cancel_MouseEnter(object sender, EventArgs e)
        {
            this.btn_LC_Cancel.ForeColor = Color.White;
        }

        private void btn_LC_Cancel_MouseLeave(object sender, EventArgs e)
        {
            this.btn_LC_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void pnl_L_CDet_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.pnl_L_CDet.Visible)
                return;
            this.CUR_LCLIENT = 0;
            this.CurrentPanel("pnl_L_CDet");
            this.dgv_LClients.DataSource = (object)this.lClientsBS;
            this.LoadLocalClients();
            if ((uint)this.dgv_LClients.Rows.Count > 0U && !string.IsNullOrEmpty(this.dgv_LClients.Rows[0].Cells[0].Value as string))
                this.dgv_CellClick((object)this.dgv_LClients, new DataGridViewCellEventArgs(0, 0));
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string str = this.dgv_LClients.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            DataTable dataTable;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                using (SqlCommand command = dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Clients WHERE Code = '" + str + "'";
                    dataTable = new DataTable();
                    new SqlDataAdapter(command).Fill(dataTable);
                }
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.txt_LC_CCode.Text = row["Code"].ToString().Trim();
                this.txt_LC_CName.Text = row["Name"].ToString().Trim();
            }
        }

        private void btn_LC_Next_Click(object sender, EventArgs e)
        {
            if (this.CUR_LCLIENT + 1 < this.NUM_OF_LCLIENTS - 1)
            {
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = false;
                ++this.CUR_LCLIENT;
                if (!string.IsNullOrEmpty(this.dgv_LClients.Rows[this.CUR_LCLIENT].Cells[0].Value as string))
                    this.dgv_CellClick((object)this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = true;
            }
            else if (this.CUR_LCLIENT + 1 == this.NUM_OF_LCLIENTS - 1)
            {
                this.btn_LC_Next.Enabled = false;
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = false;
                ++this.CUR_LCLIENT;
                if (!string.IsNullOrEmpty(this.dgv_LClients.Rows[this.CUR_LCLIENT].Cells[0].Value as string))
                    this.dgv_CellClick((object)this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = true;
            }
            if (this.CUR_LCLIENT == 0 || this.btn_LC_Prev.Enabled)
                return;
            this.btn_LC_Prev.Enabled = true;
        }

        private void btn_LC_Prev_Click(object sender, EventArgs e)
        {
            if (this.CUR_LCLIENT - 1 > 0)
            {
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = false;
                --this.CUR_LCLIENT;
                if (!string.IsNullOrEmpty(this.dgv_LClients.Rows[this.CUR_LCLIENT].Cells[0].Value as string))
                    this.dgv_CellClick((object)this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = true;
            }
            else if (this.CUR_LCLIENT - 1 == 0)
            {
                this.btn_LC_Prev.Enabled = false;
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = false;
                --this.CUR_LCLIENT;
                if (!string.IsNullOrEmpty(this.dgv_LClients.Rows[this.CUR_LCLIENT].Cells[0].Value as string))
                    this.dgv_CellClick((object)this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
                this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = true;
            }
            if (this.CUR_LCLIENT == this.NUM_OF_LCLIENTS || this.btn_LC_Next.Enabled)
                return;
            this.btn_LC_Next.Enabled = true;
        }

        private void btn_LC_Add_Click(object sender, EventArgs e)
        {
            this.isLCReadOnly = false;
            this.btn_LC_Add.Visible = false;
            this.btn_LC_Edit.Visible = false;
            this.btn_LC_DoneAdd.Visible = true;
            this.btn_LC_Cancel.Visible = true;
            this.txt_LC_CName.Text = string.Empty;
            this.txt_LC_CName.Focus();
            int num = 0;
            foreach (DataRow row in (InternalDataCollectionBase)this.lClientDT.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    int int32 = Convert.ToInt32(row["Code", DataRowVersion.Original].ToString().Trim().Remove(0, 4));
                    if (int32 > num)
                        num = int32;
                }
                else
                {
                    int int32 = Convert.ToInt32(row["Code"].ToString().Trim().Remove(0, 4));
                    if (int32 > num)
                        num = int32;
                }
            }
            this.txt_LC_CCode.Text = "QTL" + (num + 1).ToString("000");
        }

        private void btn_LC_DoneAdd_Click(object sender, EventArgs e)
        {
            string str = this.txt_LC_CCode.Text.Trim();
            if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add client with code: ").Append(str).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Clients VALUES (@Code, @Name)", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Code", (object)str);
                        sqlCommand.Parameters.AddWithValue("@Name", (object)this.txt_LC_CName.Text.Trim());
                        sqlCommand.ExecuteNonQuery();
                        int num = (int)MessageBox.Show("New client successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this.LoadLocalClients();
                    this.dgv_LClients.CurrentCell = this.dgv_LClients.Rows[this.dgv_LClients.Rows.Count - 1].Cells[0];
                    if (this.dgv_LClients.Rows.Count != 1)
                    {
                        this.dgv_LClients.ClearSelection();
                        int index = this.dgv_LClients.Rows.Count - 1;
                        this.dgv_LClients.Rows[index].Selected = true;
                        this.dgv_LClients.FirstDisplayedScrollingRowIndex = index;
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    this.btn_LC_Add.Visible = true;
                    this.btn_LC_Edit.Visible = true;
                    this.btn_LC_DoneAdd.Visible = false;
                    this.btn_LC_Cancel.Visible = false;
                    this.isLCReadOnly = true;
                }
            }
        }

        private void btn_LC_Edit_Click(object sender, EventArgs e)
        {
            this.btn_LC_Add.Visible = false;
            this.btn_LC_Edit.Visible = false;
            this.btn_LC_DoneEdit.Visible = true;
            this.btn_LC_Cancel.Visible = true;
            this.isLCReadOnly = false;
            this.txt_LC_CName.Focus();
        }

        private void btn_LC_DoneEdit_Click(object sender, EventArgs e)
        {
            string str = this.dgv_LClients.CurrentRow.Cells[0].Value.ToString().Trim();
            if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to edit client with code: ").Append(str).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UPDATE Clients SET Name = @Name WHERE Code = @Code", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Name", (object)this.txt_LC_CName.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Code", (object)str);
                        sqlCommand.ExecuteNonQuery();
                        int num = (int)MessageBox.Show("Client successfully Updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.LoadLocalClients();
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    this.btn_LC_DoneEdit.Visible = false;
                    this.btn_LC_Cancel.Visible = false;
                    this.btn_LC_Add.Visible = true;
                    this.btn_LC_Edit.Visible = true;
                    this.isLCReadOnly = true;
                }
            }
        }

        private void btn_LC_Cancel_Click(object sender, EventArgs e)
        {
            this.isLCReadOnly = true;
            this.btn_LC_DoneAdd.Visible = false;
            this.btn_LC_DoneEdit.Visible = false;
            this.btn_LC_Cancel.Visible = false;
            this.btn_LC_Edit.Visible = true;
            this.btn_LC_Add.Visible = true;
            this.dgv_CellClick((object)this.dgv_LClients, new DataGridViewCellEventArgs(0, 0));
            if (this.dgv_LClients.Rows.Count == 1)
                return;
            this.dgv_LClients.ClearSelection();
            this.dgv_LClients.Rows[0].Selected = true;
            this.dgv_LClients.FirstDisplayedScrollingRowIndex = 0;
        }

        private void txt_LC_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.isLCReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void txt_LC_CName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.isLCReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void dgv_LClients_FilterStringChanged(object sender, EventArgs e)
        {
            this.lClientsBS.Filter = this.dgv_LClients.FilterString;
        }

        private void dgv_LClients_SortStringChanged(object sender, EventArgs e)
        {
            this.lClientsBS.Sort = this.dgv_LClients.SortString;
        }

        private void btn_IC_Prev_MouseEnter(object sender, EventArgs e)
        {
            this.btn_IC_Prev.Image = (Image)Resources.back_white;
        }

        private void btn_IC_Prev_MouseLeave(object sender, EventArgs e)
        {
            this.btn_IC_Prev.Image = (Image)Resources.back_black;
        }

        private void btn_IC_Next_MouseEnter(object sender, EventArgs e)
        {
            this.btn_IC_Next.Image = (Image)Resources.forward_white;
        }

        private void btn_IC_Next_MouseLeave(object sender, EventArgs e)
        {
            this.btn_IC_Next.Image = (Image)Resources.forawrd_black;
        }

        private void btn_IC_Add_MouseEnter(object sender, EventArgs e)
        {
            this.btn_IC_Add.ForeColor = Color.White;
            this.btn_IC_Add.Image = (Image)Resources.add_white;
        }

        private void btn_IC_Add_MouseLeave(object sender, EventArgs e)
        {
            this.btn_IC_Add.ForeColor = Color.FromArgb(64, 64, 64);
            this.btn_IC_Add.Image = (Image)Resources.add_grey;
        }

        private void btn_IC_Edit_MouseEnter(object sender, EventArgs e)
        {
            this.btn_IC_Edit.ForeColor = Color.White;
            this.btn_IC_Edit.Image = (Image)Resources.edit_white;
        }

        private void btn_IC_Edit_MouseLeave(object sender, EventArgs e)
        {
            this.btn_IC_Edit.ForeColor = Color.FromArgb(64, 64, 64);
            this.btn_IC_Edit.Image = (Image)Resources.edit_grey;
        }

        private void btn_IC_DoneAdd_MouseEnter(object sender, EventArgs e)
        {
            this.btn_IC_DoneAdd.ForeColor = Color.White;
        }

        private void btn_IC_DoneAdd_MouseLeave(object sender, EventArgs e)
        {
            this.btn_IC_DoneAdd.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void btn_IC_DoneEdit_MouseEnter(object sender, EventArgs e)
        {
            this.btn_IC_DoneEdit.ForeColor = Color.White;
        }

        private void btn_IC_DoneEdit_MouseLeave(object sender, EventArgs e)
        {
            this.btn_IC_DoneEdit.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void btn_IC_Cancel_MouseEnter(object sender, EventArgs e)
        {
            this.btn_IC_Cancel.ForeColor = Color.White;
        }

        private void btn_IC_Cancel_MouseLeave(object sender, EventArgs e)
        {
            this.btn_IC_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void pnl_I_Clients_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.pnl_I_Clients.Visible)
                return;
            this.CUR_ICLIENT = 0;
            this.CurrentPanel("pnl_I_Clients");
            this.dgv_IClients.DataSource = (object)this.iClientsBS;
            this.LoadIntClients();
            if ((uint)this.dgv_IClients.Rows.Count > 0U && !string.IsNullOrEmpty(this.dgv_IClients.Rows[0].Cells[0].Value as string))
                this.dgv_I_CellClick((object)this.dgv_IClients, new DataGridViewCellEventArgs(0, 0));
        }

        private void dgv_I_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string str = this.dgv_IClients.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            DataTable dataTable;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                using (SqlCommand command = dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Int_Clients WHERE Code = '" + str + "'";
                    dataTable = new DataTable();
                    new SqlDataAdapter(command).Fill(dataTable);
                }
            }
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                this.txt_IC_CCode.Text = row["Code"].ToString().Trim();
                this.txt_IC_CName.Text = row["Name"].ToString().Trim();
            }
        }

        private void btn_IC_Next_Click(object sender, EventArgs e)
        {
            if (this.CUR_ICLIENT + 1 < this.NUM_OF_ICLIENTS - 1)
            {
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = false;
                ++this.CUR_ICLIENT;
                if (!string.IsNullOrEmpty(this.dgv_IClients.Rows[this.CUR_ICLIENT].Cells[0].Value as string))
                    this.dgv_I_CellClick((object)this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = true;
            }
            else if (this.CUR_ICLIENT + 1 == this.NUM_OF_ICLIENTS - 1)
            {
                this.btn_IC_Next.Enabled = false;
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = false;
                ++this.CUR_ICLIENT;
                if (!string.IsNullOrEmpty(this.dgv_IClients.Rows[this.CUR_ICLIENT].Cells[0].Value as string))
                    this.dgv_I_CellClick((object)this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = true;
            }
            if (this.CUR_ICLIENT == 0 || this.btn_IC_Prev.Enabled)
                return;
            this.btn_IC_Prev.Enabled = true;
        }

        private void btn_IC_Prev_Click(object sender, EventArgs e)
        {
            if (this.CUR_ICLIENT - 1 > 0)
            {
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = false;
                --this.CUR_ICLIENT;
                if (!string.IsNullOrEmpty(this.dgv_IClients.Rows[this.CUR_ICLIENT].Cells[0].Value as string))
                    this.dgv_I_CellClick((object)this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = true;
            }
            else if (this.CUR_ICLIENT - 1 == 0)
            {
                this.btn_IC_Prev.Enabled = false;
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = false;
                --this.CUR_ICLIENT;
                if (!string.IsNullOrEmpty(this.dgv_IClients.Rows[this.CUR_ICLIENT].Cells[0].Value as string))
                    this.dgv_I_CellClick((object)this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
                this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = true;
            }
            if (this.CUR_ICLIENT == this.NUM_OF_ICLIENTS || this.btn_IC_Next.Enabled)
                return;
            this.btn_IC_Next.Enabled = true;
        }

        private void btn_IC_Add_Click(object sender, EventArgs e)
        {
            this.isICReadOnly = false;
            this.btn_IC_Add.Visible = false;
            this.btn_IC_Edit.Visible = false;
            this.btn_IC_DoneAdd.Visible = true;
            this.btn_IC_Cancel.Visible = true;
            this.txt_IC_CName.Text = string.Empty;
            this.txt_IC_CName.Focus();
            int num = 0;
            foreach (DataRow row in (InternalDataCollectionBase)this.iClientDT.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    int int32 = Convert.ToInt32(row["Code", DataRowVersion.Original].ToString().Trim().Remove(0, 4));
                    if (int32 > num)
                        num = int32;
                }
                else
                {
                    int int32 = Convert.ToInt32(row["Code"].ToString().Trim().Remove(0, 4));
                    if (int32 > num)
                        num = int32;
                }
            }
            this.txt_IC_CCode.Text = "QTI" + (num + 1).ToString("000");
        }

        private void btn_IC_DoneAdd_Click(object sender, EventArgs e)
        {
            string str = this.txt_IC_CCode.Text.Trim();
            if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add client with code: ").Append(str).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Int_Clients VALUES (@Code, @Name)", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Code", (object)str);
                        sqlCommand.Parameters.AddWithValue("@Name", (object)this.txt_IC_CName.Text.Trim());
                        sqlCommand.ExecuteNonQuery();
                        int num = (int)MessageBox.Show("New client successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this.LoadIntClients();
                    this.dgv_IClients.CurrentCell = this.dgv_IClients.Rows[this.dgv_IClients.Rows.Count - 1].Cells[0];
                    if (this.dgv_IClients.Rows.Count != 1)
                    {
                        this.dgv_IClients.ClearSelection();
                        int index = this.dgv_IClients.Rows.Count - 1;
                        this.dgv_IClients.Rows[index].Selected = true;
                        this.dgv_IClients.FirstDisplayedScrollingRowIndex = index;
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    this.btn_IC_Add.Visible = true;
                    this.btn_IC_Edit.Visible = true;
                    this.btn_IC_DoneAdd.Visible = false;
                    this.btn_IC_Cancel.Visible = false;
                    this.isICReadOnly = true;
                }
            }
        }

        private void btn_IC_Edit_Click(object sender, EventArgs e)
        {
            this.btn_IC_Add.Visible = false;
            this.btn_IC_Edit.Visible = false;
            this.btn_IC_DoneEdit.Visible = true;
            this.btn_IC_Cancel.Visible = true;
            this.isICReadOnly = false;
            this.txt_IC_CName.Focus();
        }

        private void btn_IC_DoneEdit_Click(object sender, EventArgs e)
        {
            string str = this.dgv_IClients.CurrentRow.Cells[0].Value.ToString().Trim();
            if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to edit client with code: ").Append(str).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UPDATE Int_Clients SET Name = @Name WHERE Code = @Code", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Name", (object)this.txt_IC_CName.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Code", (object)str);
                        sqlCommand.ExecuteNonQuery();
                        int num = (int)MessageBox.Show("Client successfully Updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.LoadIntClients();
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    this.btn_IC_DoneEdit.Visible = false;
                    this.btn_IC_Cancel.Visible = false;
                    this.btn_IC_Add.Visible = true;
                    this.btn_IC_Edit.Visible = true;
                    this.isICReadOnly = true;
                }
            }
        }

        private void btn_IC_Cancel_Click(object sender, EventArgs e)
        {
            this.isICReadOnly = true;
            this.btn_IC_DoneAdd.Visible = false;
            this.btn_IC_DoneEdit.Visible = false;
            this.btn_IC_Cancel.Visible = false;
            this.btn_IC_Edit.Visible = true;
            this.btn_IC_Add.Visible = true;
            this.dgv_I_CellClick((object)this.dgv_IClients, new DataGridViewCellEventArgs(0, 0));
            if (this.dgv_IClients.Rows.Count == 1)
                return;
            this.dgv_IClients.ClearSelection();
            this.dgv_IClients.Rows[0].Selected = true;
            this.dgv_IClients.FirstDisplayedScrollingRowIndex = 0;
        }

        private void txt_IC_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.isICReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void txt_IC_CName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.isICReadOnly)
                return;
            e.SuppressKeyPress = true;
        }

        private void dgv_IClients_FilterStringChanged(object sender, EventArgs e)
        {
            this.iClientsBS.Filter = this.dgv_IClients.FilterString;
        }

        private void dgv_IClients_SortStringChanged(object sender, EventArgs e)
        {
            this.iClientsBS.Sort = this.dgv_IClients.SortString;
        }
    }
}
