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

    public class Home : Form
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
            {
                btn_LC_Edit.Enabled = false;
            }
            else
            {
                if (NUM_OF_LCLIENTS == 0 || btn_LC_Edit.Enabled)
                    return;
                btn_LC_Edit.Enabled = true;
            }
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
            {
                btn_IC_Edit.Enabled = false;
            }
            else
            {
                if (NUM_OF_ICLIENTS == 0 || btn_IC_Edit.Enabled)
                    return;
                btn_IC_Edit.Enabled = true;
            }
        }

        private void btn_Home_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Home_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Close.Image = Resources.close_white;
        }

        private void btn_Home_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Close.Image = Resources.close_black;
        }

        private void btn_Home_Max_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Max.Image = Resources.maximize_white;
        }

        private void btn_Home_Max_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Max.Image = Resources.maximize_black;
        }

        private void btn_Home_Max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btn_Home_Max.Visible = false;
            btn_Home_Nor.Visible = true;
            lblComing.Location = new Point((pnl_Home.Width / 2) - (lblComing.Width / 2), (pnl_Home.Height / 2) - (lblComing.Height / 2));
        }

        private void btn_Home_Nor_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Nor.Image = Resources.restore_white;
        }

        private void btn_Home_Nor_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Nor.Image = Resources.restore_black2;
        }

        private void btn_Home_Nor_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btn_Home_Nor.Visible = false;
            btn_Home_Max.Visible = true;
            lblComing.Location = new Point(416, 297);
        }

        private void btn_Home_Min_MouseEnter(object sender, EventArgs e)
        {
            btn_Home_Min.Image = Resources.minimize_white;
        }

        private void btn_Home_Min_MouseLeave(object sender, EventArgs e)
        {
            btn_Home_Min.Image = Resources.minimize_grey;
        }

        private void btn_Home_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            lblComing.Visible = true;

            resetButtons(selected);
            getSelectedButton(sender);
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
            btn_Home.Image = (Image) Resources.home_white;
        }

        private void btn_Home_MouseEnter(object sender, EventArgs e)
        {
            btn_Home.BackColor = Color.FromArgb(73, 73, 73);
            btn_Home.ForeColor = Color.FromArgb(19, 118, 188);
            btn_Home.Image = (Image) Resources.home_blue;
        }

        private void btn_Home_MouseLeave(object sender, EventArgs e)
        {
            btn_Home.BackColor = Color.FromArgb(64, 64, 64);
            btn_Home.ForeColor = Color.White;
            btn_Home.Image = (Image) Resources.home_white;
        }

        private void getSelectedButton(object sender)
        {
            string name = ((Control) sender).Name;
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
                        this.selected = "Projects";
                        break;
                    }
                case 1572591152:
                    {

                    }
          if (!(name == "btn_I_Clients"))
            break;
          this.selected = "iClients";
          break;
                case 2022576627:
                    {

                    }
          if (!(name == "btn_Local"))
            break;
          this.selected = "Local";
          break;
                case 2071924013:
                    {

                    }
          if (!(name == "btn_I_Orders"))
            break;
          this.selected = "iOrders";
          break;
                case 2105268658:
                    {

                    }
          if (!(name == "btn_L_InvRec"))
            break;
          this.selected = "lInvRec";
          break;
                case 2454825689:
                    {

                    }
          if (!(name == "btn_Int"))
            break;
          this.selected = "Int";
          break;
                case 2595377631:
                    {

                    }
          if (!(name == "btn_Home"))
            break;
          this.selected = nameof (Home);
          break;
                case 2830359379:
                    {

                    }
          if (!(name == "btn_L_Clients"))
            break;
          this.selected = "lClients";
          break;
                case 2999105185:
                    {

                    }
          if (!(name == "btn_I_InvSent"))
            break;
          this.selected = "iInvSent";
          break;
                case 3216239524:
                    {

                    }
          if (!(name == "btn_I_Invoices"))
            break;
          this.selected = "iInvoices";
          break;
                case 3634565802:
                    {

                    }
          if (!(name == "btn_L_InvSent"))
            break;
          this.selected = "lInvSent";
          break;
                case 3663618135:
                    {

                    }
          if (!(name == "btn_C_Timesheets"))
            break;
          this.selected = "cTimesheets";
          break;
                case 3731463956:
                    {

                    }
          if (!(name == "btn_C_NoInv"))
            break;
          this.selected = "cNoInv";
          break;
                case 3848486376:
                    {

                    }
          if (!(name == "btn_L_Orders"))
            break;
          this.selected = "lOrders";
          break;
                case 3907623993:
                    {

                    }
          if (!(name == "btn_I_Quotes"))
            break;
          this.selected = "iQuotes";
          break;
      }
    }

    private void resetButtons(string name)
    {
      string s = name;
      switch (s)
      {
        case "lInvRec" : 
          this.btn_L_InvRec.BackColor = Color.FromArgb(35, 35, 35);
          this.btn_L_InvRec.ForeColor = Color.White;
          break;
        case "lQuotes":
          this.btn_L_Quotes.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_L_Quotes.ForeColor = Color.White;
          break;
        case "Local":
          this.btn_Local.BackColor = Color.FromArgb(64, 64, 64);
          this.btn_Local.ForeColor = Color.White;
          break;
        case "iOrders":
          this.btn_I_Orders.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_I_Orders.ForeColor = Color.White;
          break;
        case "iInvSent":
          this.btn_I_InvSent.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_I_InvSent.ForeColor = Color.White;
          break;
        case "lClients":
          this.btn_L_Clients.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_L_Clients.ForeColor = Color.White;
          break;
        case "Home":
          this.btn_Home.BackColor = Color.FromArgb(64, 64, 64);
          this.btn_Home.ForeColor = Color.White;
          this.lblComing.Visible = false;
          break;
        case "iClients":
          this.btn_I_Clients.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_I_Clients.ForeColor = Color.White;
          break;
        case "Projects":
          this.btn_Projects.BackColor = Color.FromArgb(64, 64, 64);
          this.btn_Projects.ForeColor = Color.White;
          break;
        case "Contractors":
          this.btn_Contractors.BackColor = Color.FromArgb(64, 64, 64);
          this.btn_Contractors.ForeColor = Color.White;
          break;
        case "iQuotes":
          this.btn_I_Quotes.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_I_Quotes.ForeColor = Color.White;
          break;
        case "lInvSent":
          this.btn_L_InvSent.BackColor = Color.FromArgb(35, 35, 35);
          this.btn_L_InvSent.ForeColor = Color.White;
          break;
        case "cNoRem":
          this.btn_C_NoRem.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_C_NoRem.ForeColor = Color.White;
          break;
        case "lPettyCash":
          this.btn_L_PettyCash.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_L_PettyCash.ForeColor = Color.White;
          break;
        case "lOrders":
          this.btn_L_Orders.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_L_Orders.ForeColor = Color.White;
          break;
        case "lInvoices":
          this.btn_L_Invoices.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_L_Invoices.ForeColor = Color.White;
          break;
        case "cTimesheets":
          this.btn_C_Timesheets.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_C_Timesheets.ForeColor = Color.White;
          break;
        case "cNoInv":
          this.btn_C_NoInv.BackColor = Color.FromArgb(50, 50, 50);
          this.btn_C_NoInv.ForeColor = Color.White;
          break;
        case "Int":
          this.btn_Int.BackColor = Color.FromArgb(64, 64, 64);
          this.btn_Int.ForeColor = Color.White;
          break;
      }
    }

    private void CurrentPanel(string name)
    {
      string s = name;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(s))
      {
        case 255025442:
          if (!(s == "pnl_L_Quotes"))
            break;
          this.curVisible = "pnl_L_Quotes";
          break;
        case 394946480:
          if (!(s == "pnl_L_InvSent"))
            break;
          this.curVisible = "pnl_L_InvSent";
          break;
        case 644103399:
          if (!(s == "pnl_I_Orders"))
            break;
          this.curVisible = "pnl_I_Orders";
          break;
        case 1624435748:
          if (!(s == "pnl_L_PettyCash"))
            break;
          this.curVisible = "pnl_L_PettyCash";
          break;
        case 1672930100:
          if (!(s == "pnl_L_InvRec"))
            break;
          this.curVisible = "pnl_L_InvRec";
          break;
        case 1810021675:
          if (!(s == "pnl_I_Quotes"))
            break;
          this.curVisible = "pnl_I_Quotes";
          break;
        case 1959501159:
          if (!(s == "pnl_I_InvSent"))
            break;
          this.curVisible = "pnl_I_InvSent";
          break;
        case 2258667164:
          if (!(s == "pnl_Projects"))
            break;
          this.curVisible = "pnl_Projects";
          break;
        case 2902009809:
          if (!(s == "pnl_C_NoRem"))
            break;
          this.curVisible = "pnl_C_NoRem";
          break;
        case 3021685954:
          if (!(s == "pnl_I_Clients"))
            break;
          this.curVisible = "pnl_I_Clients";
          break;
        case 3239117006:
          if (!(s == "pnl_Contractors"))
            break;
          this.curVisible = "pnl_Contractors";
          break;
        case 3621814393:
          if (!(s == "pnl_Home"))
            break;
          this.curVisible = "pnl_Home";
          break;
        case 3803523437:
          if (!(s == "pnl_L_CDet"))
            break;
          this.curVisible = "pnl_L_CDet";
          break;
        case 3894939078:
          if (!(s == "pnl_C_NoInv"))
            break;
          this.curVisible = "pnl_C_NoInv";
          break;
        case 3918914690:
          if (!(s == "pnl_L_Orders"))
            break;
          this.curVisible = "pnl_L_Orders";
          break;
      }
    }

    private void HidePanel()
    {
      string curVisible = this.curVisible;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(curVisible))
      {
        case 255025442:
          if (!(curVisible == "pnl_L_Quotes"))
            break;
          this.pnl_L_Quotes.Visible = false;
          break;
        case 394946480:
          if (!(curVisible == "pnl_L_InvSent"))
            break;
          this.pnl_L_InvSent.Visible = false;
          break;
        case 644103399:
          if (!(curVisible == "pnl_I_Orders"))
            break;
          this.pnl_I_Orders.Visible = false;
          break;
        case 1624435748:
          if (!(curVisible == "pnl_L_PettyCash"))
            break;
          this.pnl_L_PettyCash.Visible = false;
          break;
        case 1672930100:
          if (!(curVisible == "pnl_L_InvRec"))
            break;
          this.pnl_L_InvRec.Visible = false;
          break;
        case 1810021675:
          if (!(curVisible == "pnl_I_Quotes"))
            break;
          this.pnl_I_Quotes.Visible = false;
          break;
        case 1959501159:
          if (!(curVisible == "pnl_I_InvSent"))
            break;
          this.pnl_I_InvSent.Visible = false;
          break;
        case 2258667164:
          if (!(curVisible == "pnl_Projects"))
            break;
          this.pnl_Projects.Visible = false;
          break;
        case 2902009809:
          if (!(curVisible == "pnl_C_NoRem"))
            break;
          this.pnl_C_NoRem.Visible = false;
          break;
        case 3021685954:
          if (!(curVisible == "pnl_I_Clients"))
            break;
          this.pnl_I_Clients.Visible = false;
          break;
        case 3239117006:
          if (!(curVisible == "pnl_Contractors"))
            break;
          this.pnl_Contractors.Visible = false;
          break;
        case 3621814393:
          if (!(curVisible == "pnl_Home"))
            break;
          this.pnl_Home.Visible = false;
          break;
        case 3803523437:
          if (!(curVisible == "pnl_L_CDet"))
            break;
          this.pnl_L_CDet.Visible = false;
          break;
        case 3894939078:
          if (!(curVisible == "pnl_C_NoInv"))
            break;
          this.pnl_C_NoInv.Visible = false;
          break;
        case 3918914690:
          if (!(curVisible == "pnl_L_Orders"))
            break;
          this.pnl_L_Orders.Visible = false;
          break;
      }
    }

    public string getCurPanel()
    {
      return this.curVisible;
    }

    public object getCurForm()
    {
      return this.curForm;
    }

    private void btn_Local_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "Local"))
        return;
      this.btn_Local.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_Local.ForeColor = Color.FromArgb(19, 118, 188);
      this.btn_Local.Image = (Image) Resources.local_blue;
    }

    private void btn_Local_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "Local"))
        return;
      this.btn_Local.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Local.ForeColor = Color.White;
      this.btn_Local.Image = (Image) Resources.local_white;
    }

    private void btn_Local_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton((object) this.btn_L_Clients);
      this.HidePanel();
      if (this.isIntOpen)
        this.tmr_Int.Start();
      if (this.isConOpen)
        this.tmr_Con.Start();
      this.btn_Local.BackColor = Color.FromArgb(19, 118, 188);
      this.btn_Local.ForeColor = Color.White;
      this.btn_Local.Image = (Image) Resources.local_white;
      if (this.isLInvOpen && this.isLocalOpen)
        this.tmr_L_Inv.Start();
      this.btn_L_Clients.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_L_Clients.ForeColor = Color.White;
      this.pnl_L_CDet.Visible = true;
      this.tmr_Local.Start();
    }

    private void btn_L_Clients_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.btn_L_Clients.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_L_Clients.ForeColor = Color.White;
      this.pnl_L_CDet.Visible = true;
    }

    private void btn_L_Clients_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "lClients"))
        return;
      this.btn_L_Clients.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_L_Clients.ForeColor = Color.FromArgb(19, 118, 188);
    }

    private void btn_L_Clients_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "lClients"))
        return;
      this.btn_L_Clients.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Clients.ForeColor = Color.White;
    }

    private void btn_L_Orders_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "lOrders"))
        return;
      this.btn_L_Orders.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_L_Orders.ForeColor = Color.FromArgb(19, 118, 188);
    }

    private void btn_L_Orders_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "lOrders"))
        return;
      this.btn_L_Orders.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Orders.ForeColor = Color.White;
    }

    private void btn_L_Orders_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_L_Orders.Visible = true;
      this.CurrentPanel("pnl_L_Orders");
      this.btn_L_Orders.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_L_Orders.ForeColor = Color.White;
      this.frmOrder = new Orders();
      this.curForm = (object) this.frmOrder;
      this.frmOrder.TopLevel = false;
      this.frmOrder.TopMost = true;
      this.pnl_L_Orders.Controls.Add((Control) this.frmOrder);
      this.frmOrder.Show();
    }

    private void btn_L_Quotes_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "lQuotes"))
        return;
      this.btn_L_Quotes.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_L_Quotes.ForeColor = Color.FromArgb(19, 118, 188);
    }

    private void btn_L_Quotes_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "lQuotes"))
        return;
      this.btn_L_Quotes.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Quotes.ForeColor = Color.White;
    }

    private void btn_L_Quotes_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_L_Quotes.Visible = true;
      this.CurrentPanel("pnl_L_Quotes");
      this.btn_L_Quotes.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_L_Quotes.ForeColor = Color.White;
      this.frmQuote = new Quotes();
      this.curForm = (object) this.frmQuote;
      this.frmQuote.TopLevel = false;
      this.frmQuote.TopMost = true;
      this.pnl_L_Quotes.Controls.Add((Control) this.frmQuote);
      this.frmQuote.Show();
    }

    private void btn_L_Invoices_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "lInvoices"))
        return;
      this.btn_L_Invoices.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_L_Invoices.ForeColor = Color.FromArgb(19, 118, 188);
    }

    private void btn_L_Invoices_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "lInvoices"))
        return;
      this.btn_L_Invoices.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Invoices.ForeColor = Color.White;
    }

    private void btn_L_Invoices_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.btn_L_Invoices.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_L_Invoices.ForeColor = Color.White;
      this.tmr_L_Inv.Start();
    }

    private void btn_L_InvSent_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_L_InvSent.Visible = true;
      this.CurrentPanel("pnl_L_InvSent");
      this.btn_L_InvSent.BackColor = Color.FromArgb(13, 77, 119);
      this.btn_L_InvSent.ForeColor = Color.White;
      this.frmInvSent = new Invoices_Send();
      this.curForm = (object) this.frmInvSent;
      this.frmInvSent.TopLevel = false;
      this.frmInvSent.TopMost = true;
      this.pnl_L_InvSent.Controls.Add((Control) this.frmInvSent);
      this.frmInvSent.Show();
    }

    private void btn_L_InvSent_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "lInvSent"))
        return;
      this.btn_L_InvSent.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_L_InvSent.ForeColor = Color.FromArgb(19, 118, 188);
    }

    private void btn_L_InvSent_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "lInvSent"))
        return;
      this.btn_L_InvSent.BackColor = Color.FromArgb(35, 35, 35);
      this.btn_L_InvSent.ForeColor = Color.White;
    }

    private void btn_L_InvRec_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_L_InvRec.Visible = true;
      this.CurrentPanel("pnl_L_InvRec");
      this.btn_L_InvRec.BackColor = Color.FromArgb(13, 77, 119);
      this.btn_L_InvRec.ForeColor = Color.White;
      this.frmInvRec = new Inv_Rec();
      this.curForm = (object) this.frmInvRec;
      this.frmInvRec.TopLevel = false;
      this.frmInvRec.TopMost = true;
      this.pnl_L_InvRec.Controls.Add((Control) this.frmInvRec);
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_L_PettyCash.Visible = true;
      this.CurrentPanel("pnl_L_PettyCash");
      this.btn_L_PettyCash.BackColor = Color.FromArgb(13, 77, 119);
      this.btn_L_PettyCash.ForeColor = Color.White;
      this.frmPetty = new PettyCash();
      this.curForm = (object) this.frmPetty;
      this.frmPetty.TopLevel = false;
      this.frmPetty.TopMost = true;
      this.pnl_L_PettyCash.Controls.Add((Control) this.frmPetty);
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
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
      this.btn_Int.Image = (Image) Resources.globe_white;
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
      this.btn_Int.Image = (Image) Resources.globe_blue;
    }

    private void btn_Int_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "Int"))
        return;
      this.btn_Int.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Int.ForeColor = Color.White;
      this.btn_Int.Image = (Image) Resources.globe_white;
    }

    private void btn_I_Clients_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_I_Orders.Visible = true;
      this.CurrentPanel("pnl_I_Orders");
      this.btn_I_Orders.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_I_Orders.ForeColor = Color.White;
      this.frmIntOrders = new Int_Orders();
      this.curForm = (object) this.frmIntOrders;
      this.frmIntOrders.TopLevel = false;
      this.frmIntOrders.TopMost = true;
      this.pnl_I_Orders.Controls.Add((Control) this.frmIntOrders);
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_I_Quotes.Visible = true;
      this.CurrentPanel("pnl_I_Quotes");
      this.btn_I_Quotes.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_I_Quotes.ForeColor = Color.White;
      this.frmIntQuotes = new Int_Quotes();
      this.curForm = (object) this.frmIntQuotes;
      this.frmIntQuotes.TopLevel = false;
      this.frmIntQuotes.TopMost = true;
      this.pnl_I_Quotes.Controls.Add((Control) this.frmIntQuotes);
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_I_InvSent.Visible = true;
      this.CurrentPanel("pnl_I_InvSent");
      this.btn_I_InvSent.BackColor = Color.FromArgb(13, 77, 119);
      this.btn_I_InvSent.ForeColor = Color.White;
      this.frmIntInvSent = new Int_Invoices_Send();
      this.curForm = (object) this.frmIntInvSent;
      this.frmIntInvSent.TopLevel = false;
      this.frmIntInvSent.TopMost = true;
      this.pnl_I_InvSent.Controls.Add((Control) this.frmIntInvSent);
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
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
      this.btn_Contractors.Image = (Image) Resources.contr_white;
      this.tmr_Con.Start();
      this.pnl_Contractors.Visible = true;
      this.CurrentPanel("pnl_Contractors");
      this.btn_C_Timesheets.BackColor = Color.FromArgb(13, 77, 119);
      this.btn_C_Timesheets.ForeColor = Color.White;
      this.frmContr = new Contractors();
      this.curForm = (object) this.frmContr;
      this.frmContr.TopLevel = false;
      this.frmContr.TopMost = true;
      this.pnl_Contractors.Controls.Add((Control) this.frmContr);
      this.frmContr.Show();
    }

    private void btn_Contractors_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "Contractors"))
        return;
      this.btn_Contractors.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_Contractors.ForeColor = Color.FromArgb(19, 118, 188);
      this.btn_Contractors.Image = (Image) Resources.contr_blue;
    }

    private void btn_Contractors_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "Contractors"))
        return;
      this.btn_Contractors.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Contractors.ForeColor = Color.White;
      this.btn_Contractors.Image = (Image) Resources.contr_white;
    }

    private void btn_C_Timesheets_Click(object sender, EventArgs e)
    {
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_Contractors.Visible = true;
      this.CurrentPanel("pnl_Contractors");
      this.btn_C_Timesheets.BackColor = Color.FromArgb(13, 77, 119);
      this.btn_C_Timesheets.ForeColor = Color.White;
      this.frmContr = new Contractors();
      this.curForm = (object) this.frmContr;
      this.frmContr.TopLevel = false;
      this.frmContr.TopMost = true;
      this.pnl_Contractors.Controls.Add((Control) this.frmContr);
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_C_NoRem.Visible = true;
      this.CurrentPanel("pnl_C_NoRem");
      this.btn_C_NoRem.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_NoRem.ForeColor = Color.White;
      this.dgv_NoRem.DataSource = (object) this.conNRBS;
      this.loadNoRemittances();
      this.dgv_NoRem.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
      this.dgv_NoRem.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_NoRem.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_NoRem.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
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
      this.conNRBS.DataSource = (object) dataTable;
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
      this.HidePanel();
      this.pnl_C_NoInv.Visible = true;
      this.CurrentPanel("pnl_C_NoInv");
      this.btn_C_NoInv.BackColor = Color.FromArgb(15, 91, 142);
      this.btn_C_NoInv.ForeColor = Color.White;
      this.dgv_NoInv.DataSource = (object) this.conNIBS;
      this.loadNoInvoices();
      this.dgv_NoInv.Columns[4].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
      this.dgv_NoInv.Columns[4].DefaultCellStyle.Format = "c";
      this.dgv_NoInv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.dgv_NoInv.Columns[5].DefaultCellStyle.FormatProvider = (IFormatProvider) CultureInfo.GetCultureInfo("en-US");
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
      this.conNIBS.DataSource = (object) dataTable;
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
      this.resetButtons(this.selected);
      this.getSelectedButton(sender);
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
      this.btn_Projects.Image = (Image) Resources.project_white;
      this.frmProj = new Projects();
      this.curForm = (object) this.frmProj;
      this.frmProj.TopLevel = false;
      this.frmProj.TopMost = true;
      this.pnl_Projects.Controls.Add((Control) this.frmProj);
      this.frmProj.Show();
    }

    private void btn_Projects_MouseEnter(object sender, EventArgs e)
    {
      if (!(this.selected != "Projects"))
        return;
      this.btn_Projects.BackColor = Color.FromArgb(73, 73, 73);
      this.btn_Projects.ForeColor = Color.FromArgb(19, 118, 188);
      this.btn_Projects.Image = (Image) Resources.project_blue;
    }

    private void btn_Projects_MouseLeave(object sender, EventArgs e)
    {
      if (!(this.selected != "Projects"))
        return;
      this.btn_Projects.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Projects.ForeColor = Color.White;
      this.btn_Projects.Image = (Image) Resources.project_white;
    }

    public void setManageProjects(Manage_Proj frmMP, Home frmHome)
    {
      frmMP.TopLevel = false;
      frmMP.TopMost = true;
      this.pnl_Projects.Controls.Add((Control) frmMP);
      frmMP.setHome(this);
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
      this.btn_LC_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_LC_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LC_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_LC_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LC_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_LC_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LC_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_LC_Add_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LC_Add.ForeColor = Color.White;
      this.btn_LC_Add.Image = (Image) Resources.add_white;
    }

    private void btn_LC_Add_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LC_Add.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LC_Add.Image = (Image) Resources.add_grey;
    }

    private void btn_LC_Edit_MouseEnter(object sender, EventArgs e)
    {
      this.btn_LC_Edit.ForeColor = Color.White;
      this.btn_LC_Edit.Image = (Image) Resources.edit_white;
    }

    private void btn_LC_Edit_MouseLeave(object sender, EventArgs e)
    {
      this.btn_LC_Edit.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_LC_Edit.Image = (Image) Resources.edit_grey;
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
      this.dgv_LClients.DataSource = (object) this.lClientsBS;
      this.LoadLocalClients();
      if ((uint) this.dgv_LClients.Rows.Count > 0U && !string.IsNullOrEmpty(this.dgv_LClients.Rows[0].Cells[0].Value as string))
        this.dgv_CellClick((object) this.dgv_LClients, new DataGridViewCellEventArgs(0, 0));
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
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
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
          this.dgv_CellClick((object) this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
        this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = true;
      }
      else if (this.CUR_LCLIENT + 1 == this.NUM_OF_LCLIENTS - 1)
      {
        this.btn_LC_Next.Enabled = false;
        this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = false;
        ++this.CUR_LCLIENT;
        if (!string.IsNullOrEmpty(this.dgv_LClients.Rows[this.CUR_LCLIENT].Cells[0].Value as string))
          this.dgv_CellClick((object) this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
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
          this.dgv_CellClick((object) this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
        this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = true;
      }
      else if (this.CUR_LCLIENT - 1 == 0)
      {
        this.btn_LC_Prev.Enabled = false;
        this.dgv_LClients.Rows[this.CUR_LCLIENT].Selected = false;
        --this.CUR_LCLIENT;
        if (!string.IsNullOrEmpty(this.dgv_LClients.Rows[this.CUR_LCLIENT].Cells[0].Value as string))
          this.dgv_CellClick((object) this.dgv_LClients, new DataGridViewCellEventArgs(0, this.CUR_LCLIENT));
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
      foreach (DataRow row in (InternalDataCollectionBase) this.lClientDT.Rows)
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
            sqlCommand.Parameters.AddWithValue("@Code", (object) str);
            sqlCommand.Parameters.AddWithValue("@Name", (object) this.txt_LC_CName.Text.Trim());
            sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("New client successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            sqlCommand.Parameters.AddWithValue("@Name", (object) this.txt_LC_CName.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Code", (object) str);
            sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("Client successfully Updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.LoadLocalClients();
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
      this.dgv_CellClick((object) this.dgv_LClients, new DataGridViewCellEventArgs(0, 0));
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
      this.btn_IC_Prev.Image = (Image) Resources.back_white;
    }

    private void btn_IC_Prev_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IC_Prev.Image = (Image) Resources.back_black;
    }

    private void btn_IC_Next_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IC_Next.Image = (Image) Resources.forward_white;
    }

    private void btn_IC_Next_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IC_Next.Image = (Image) Resources.forawrd_black;
    }

    private void btn_IC_Add_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IC_Add.ForeColor = Color.White;
      this.btn_IC_Add.Image = (Image) Resources.add_white;
    }

    private void btn_IC_Add_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IC_Add.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IC_Add.Image = (Image) Resources.add_grey;
    }

    private void btn_IC_Edit_MouseEnter(object sender, EventArgs e)
    {
      this.btn_IC_Edit.ForeColor = Color.White;
      this.btn_IC_Edit.Image = (Image) Resources.edit_white;
    }

    private void btn_IC_Edit_MouseLeave(object sender, EventArgs e)
    {
      this.btn_IC_Edit.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_IC_Edit.Image = (Image) Resources.edit_grey;
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
      this.dgv_IClients.DataSource = (object) this.iClientsBS;
      this.LoadIntClients();
      if ((uint) this.dgv_IClients.Rows.Count > 0U && !string.IsNullOrEmpty(this.dgv_IClients.Rows[0].Cells[0].Value as string))
        this.dgv_I_CellClick((object) this.dgv_IClients, new DataGridViewCellEventArgs(0, 0));
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
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
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
          this.dgv_I_CellClick((object) this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
        this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = true;
      }
      else if (this.CUR_ICLIENT + 1 == this.NUM_OF_ICLIENTS - 1)
      {
        this.btn_IC_Next.Enabled = false;
        this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = false;
        ++this.CUR_ICLIENT;
        if (!string.IsNullOrEmpty(this.dgv_IClients.Rows[this.CUR_ICLIENT].Cells[0].Value as string))
          this.dgv_I_CellClick((object) this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
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
          this.dgv_I_CellClick((object) this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
        this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = true;
      }
      else if (this.CUR_ICLIENT - 1 == 0)
      {
        this.btn_IC_Prev.Enabled = false;
        this.dgv_IClients.Rows[this.CUR_ICLIENT].Selected = false;
        --this.CUR_ICLIENT;
        if (!string.IsNullOrEmpty(this.dgv_IClients.Rows[this.CUR_ICLIENT].Cells[0].Value as string))
          this.dgv_I_CellClick((object) this.dgv_IClients, new DataGridViewCellEventArgs(0, this.CUR_ICLIENT));
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
      foreach (DataRow row in (InternalDataCollectionBase) this.iClientDT.Rows)
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
            sqlCommand.Parameters.AddWithValue("@Code", (object) str);
            sqlCommand.Parameters.AddWithValue("@Name", (object) this.txt_IC_CName.Text.Trim());
            sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("New client successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            sqlCommand.Parameters.AddWithValue("@Name", (object) this.txt_IC_CName.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Code", (object) str);
            sqlCommand.ExecuteNonQuery();
            int num = (int) MessageBox.Show("Client successfully Updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.LoadIntClients();
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
      this.dgv_I_CellClick((object) this.dgv_IClients, new DataGridViewCellEventArgs(0, 0));
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Home));
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle8 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle9 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle10 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle11 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle12 = new DataGridViewCellStyle();
      this.panel1 = new Panel();
      this.btn_Projects = new Button();
      this.pnl_Con = new Panel();
      this.btn_C_Timesheets = new Button();
      this.btn_C_NoInv = new Button();
      this.btn_C_NoRem = new Button();
      this.btn_Contractors = new Button();
      this.pnl_Int = new Panel();
      this.btn_I_InvSent = new Button();
      this.btn_I_Quotes = new Button();
      this.btn_I_Orders = new Button();
      this.btn_I_Clients = new Button();
      this.btn_Int = new Button();
      this.btn_Home = new Button();
      this.pnl_Local = new Panel();
      this.btn_L_PettyCash = new Button();
      this.pnl_L_Inv = new Panel();
      this.btn_L_InvRec = new Button();
      this.btn_L_InvSent = new Button();
      this.btn_L_Invoices = new Button();
      this.btn_L_Clients = new Button();
      this.btn_L_Quotes = new Button();
      this.btn_L_Orders = new Button();
      this.btn_Local = new Button();
      this.pictureBox1 = new PictureBox();
      this.tmr_Local = new Timer(this.components);
      this.tmr_Int = new Timer(this.components);
      this.tmr_L_Inv = new Timer(this.components);
      this.tmr_Con = new Timer(this.components);
      this.pnl_L_CDet = new Panel();
      this.btn_LC_Cancel = new Button();
      this.btn_LC_DoneEdit = new Button();
      this.btn_LC_DoneAdd = new Button();
      this.txt_LC_CName = new BunifuMaterialTextbox();
      this.txt_LC_CCode = new BunifuMaterialTextbox();
      this.bunifuSeparator1 = new BunifuSeparator();
      this.dgv_LClients = new AdvancedDataGridView();
      this.btn_LC_Edit = new Button();
      this.btn_LC_Add = new Button();
      this.btn_LC_Next = new Button();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.btn_LC_Prev = new Button();
      this.pnl_L_Orders = new Panel();
      this.pnl_L_Quotes = new Panel();
      this.pnl_L_InvSent = new Panel();
      this.pnl_L_InvRec = new Panel();
      this.pnl_I_Clients = new Panel();
      this.btn_IC_Cancel = new Button();
      this.btn_IC_DoneAdd = new Button();
      this.btn_IC_DoneEdit = new Button();
      this.txt_IC_CName = new BunifuMaterialTextbox();
      this.txt_IC_CCode = new BunifuMaterialTextbox();
      this.bunifuSeparator2 = new BunifuSeparator();
      this.dgv_IClients = new AdvancedDataGridView();
      this.btn_IC_Edit = new Button();
      this.btn_IC_Add = new Button();
      this.btn_IC_Next = new Button();
      this.bunifuCustomLabel3 = new BunifuCustomLabel();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.btn_IC_Prev = new Button();
      this.pnl_I_Orders = new Panel();
      this.pnl_I_Quotes = new Panel();
      this.pnl_I_InvSent = new Panel();
      this.pnl_Contractors = new Panel();
      this.lblComing = new Label();
      this.pnl_Home = new Panel();
      this.pnl_C_NoRem = new Panel();
      this.dgv_NoRem = new AdvancedDataGridView();
      this.pnl_C_NoInv = new Panel();
      this.dgv_NoInv = new AdvancedDataGridView();
      this.pnl_Projects = new Panel();
      this.pnl_L_PettyCash = new Panel();
      this.btn_Home_Min = new Button();
      this.btn_Home_Nor = new Button();
      this.btn_Home_Max = new Button();
      this.btn_Home_Close = new Button();
      this.clientsBindingSource1 = new BindingSource(this.components);
      this.panel1.SuspendLayout();
      this.pnl_Con.SuspendLayout();
      this.pnl_Int.SuspendLayout();
      this.pnl_Local.SuspendLayout();
      this.pnl_L_Inv.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.pnl_L_CDet.SuspendLayout();
      ((ISupportInitialize) this.dgv_LClients).BeginInit();
      this.pnl_I_Clients.SuspendLayout();
      ((ISupportInitialize) this.dgv_IClients).BeginInit();
      this.pnl_Home.SuspendLayout();
      this.pnl_C_NoRem.SuspendLayout();
      ((ISupportInitialize) this.dgv_NoRem).BeginInit();
      this.pnl_C_NoInv.SuspendLayout();
      ((ISupportInitialize) this.dgv_NoInv).BeginInit();
      ((ISupportInitialize) this.clientsBindingSource1).BeginInit();
      this.SuspendLayout();
      this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.panel1.BackColor = Color.FromArgb(64, 64, 64);
      this.panel1.Controls.Add((Control) this.btn_Projects);
      this.panel1.Controls.Add((Control) this.pnl_Con);
      this.panel1.Controls.Add((Control) this.pnl_Int);
      this.panel1.Controls.Add((Control) this.btn_Home);
      this.panel1.Controls.Add((Control) this.pnl_Local);
      this.panel1.Controls.Add((Control) this.pictureBox1);
      this.panel1.Location = new Point(0, -1);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(230, 645);
      this.panel1.TabIndex = 0;
      this.btn_Projects.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_Projects.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Projects.FlatAppearance.BorderSize = 0;
      this.btn_Projects.FlatStyle = FlatStyle.Flat;
      this.btn_Projects.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_Projects.ForeColor = Color.White;
      this.btn_Projects.Image = (Image) Resources.project_white;
      this.btn_Projects.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_Projects.Location = new Point(0, 192);
      this.btn_Projects.Name = "btn_Projects";
      this.btn_Projects.Padding = new Padding(0, 0, 51, 0);
      this.btn_Projects.Size = new Size(230, 48);
      this.btn_Projects.TabIndex = 10;
      this.btn_Projects.Text = "Projects";
      this.btn_Projects.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_Projects.UseVisualStyleBackColor = false;
      this.btn_Projects.Click += new EventHandler(this.btn_Projects_Click);
      this.btn_Projects.MouseEnter += new EventHandler(this.btn_Projects_MouseEnter);
      this.btn_Projects.MouseLeave += new EventHandler(this.btn_Projects_MouseLeave);
      this.pnl_Con.Controls.Add((Control) this.btn_C_Timesheets);
      this.pnl_Con.Controls.Add((Control) this.btn_C_NoInv);
      this.pnl_Con.Controls.Add((Control) this.btn_C_NoRem);
      this.pnl_Con.Controls.Add((Control) this.btn_Contractors);
      this.pnl_Con.Location = new Point(0, 144);
      this.pnl_Con.Name = "pnl_Con";
      this.pnl_Con.Size = new Size(230, 48);
      this.pnl_Con.TabIndex = 9;
      this.btn_C_Timesheets.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_C_Timesheets.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_C_Timesheets.FlatAppearance.BorderSize = 0;
      this.btn_C_Timesheets.FlatStyle = FlatStyle.Flat;
      this.btn_C_Timesheets.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_Timesheets.ForeColor = Color.White;
      this.btn_C_Timesheets.Location = new Point(0, 48);
      this.btn_C_Timesheets.Name = "btn_C_Timesheets";
      this.btn_C_Timesheets.Size = new Size(230, 48);
      this.btn_C_Timesheets.TabIndex = 11;
      this.btn_C_Timesheets.Text = "Timesheets";
      this.btn_C_Timesheets.UseVisualStyleBackColor = false;
      this.btn_C_Timesheets.Click += new EventHandler(this.btn_C_Timesheets_Click);
      this.btn_C_Timesheets.MouseEnter += new EventHandler(this.btn_C_Timesheets_MouseEnter);
      this.btn_C_Timesheets.MouseLeave += new EventHandler(this.btn_C_Timesheets_MouseLeave);
      this.btn_C_NoInv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_C_NoInv.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_C_NoInv.FlatAppearance.BorderSize = 0;
      this.btn_C_NoInv.FlatStyle = FlatStyle.Flat;
      this.btn_C_NoInv.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_NoInv.ForeColor = Color.White;
      this.btn_C_NoInv.Location = new Point(0, 144);
      this.btn_C_NoInv.Name = "btn_C_NoInv";
      this.btn_C_NoInv.Size = new Size(230, 48);
      this.btn_C_NoInv.TabIndex = 10;
      this.btn_C_NoInv.Text = "No Invoices";
      this.btn_C_NoInv.UseVisualStyleBackColor = false;
      this.btn_C_NoInv.Click += new EventHandler(this.btn_C_NoInv_Click);
      this.btn_C_NoInv.MouseEnter += new EventHandler(this.btn_C_NoInv_MouseEnter);
      this.btn_C_NoInv.MouseLeave += new EventHandler(this.btn_C_NoInv_MouseLeave);
      this.btn_C_NoRem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_C_NoRem.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_C_NoRem.FlatAppearance.BorderSize = 0;
      this.btn_C_NoRem.FlatStyle = FlatStyle.Flat;
      this.btn_C_NoRem.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_C_NoRem.ForeColor = Color.White;
      this.btn_C_NoRem.Location = new Point(0, 96);
      this.btn_C_NoRem.Name = "btn_C_NoRem";
      this.btn_C_NoRem.Size = new Size(230, 48);
      this.btn_C_NoRem.TabIndex = 9;
      this.btn_C_NoRem.Text = "No Remittances";
      this.btn_C_NoRem.UseVisualStyleBackColor = false;
      this.btn_C_NoRem.Click += new EventHandler(this.btn_C_NoRem_Click);
      this.btn_C_NoRem.MouseEnter += new EventHandler(this.btn_C_NoRem_MouseEnter);
      this.btn_C_NoRem.MouseLeave += new EventHandler(this.btn_C_NoRem_MouseLeave);
      this.btn_Contractors.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_Contractors.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Contractors.FlatAppearance.BorderSize = 0;
      this.btn_Contractors.FlatStyle = FlatStyle.Flat;
      this.btn_Contractors.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_Contractors.ForeColor = Color.White;
      this.btn_Contractors.Image = (Image) componentResourceManager.GetObject("btn_Contractors.Image");
      this.btn_Contractors.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_Contractors.Location = new Point(0, 0);
      this.btn_Contractors.Name = "btn_Contractors";
      this.btn_Contractors.Padding = new Padding(0, 0, 22, 0);
      this.btn_Contractors.Size = new Size(230, 48);
      this.btn_Contractors.TabIndex = 8;
      this.btn_Contractors.Text = "Contractors";
      this.btn_Contractors.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_Contractors.UseVisualStyleBackColor = false;
      this.btn_Contractors.Click += new EventHandler(this.btn_Contractors_Click);
      this.btn_Contractors.MouseEnter += new EventHandler(this.btn_Contractors_MouseEnter);
      this.btn_Contractors.MouseLeave += new EventHandler(this.btn_Contractors_MouseLeave);
      this.pnl_Int.Controls.Add((Control) this.btn_I_InvSent);
      this.pnl_Int.Controls.Add((Control) this.btn_I_Quotes);
      this.pnl_Int.Controls.Add((Control) this.btn_I_Orders);
      this.pnl_Int.Controls.Add((Control) this.btn_I_Clients);
      this.pnl_Int.Controls.Add((Control) this.btn_Int);
      this.pnl_Int.Location = new Point(0, 96);
      this.pnl_Int.Name = "pnl_Int";
      this.pnl_Int.Size = new Size(230, 48);
      this.pnl_Int.TabIndex = 7;
      this.btn_I_InvSent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_I_InvSent.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_I_InvSent.FlatAppearance.BorderSize = 0;
      this.btn_I_InvSent.FlatStyle = FlatStyle.Flat;
      this.btn_I_InvSent.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_I_InvSent.ForeColor = Color.White;
      this.btn_I_InvSent.Location = new Point(0, 192);
      this.btn_I_InvSent.Name = "btn_I_InvSent";
      this.btn_I_InvSent.Size = new Size(230, 48);
      this.btn_I_InvSent.TabIndex = 10;
      this.btn_I_InvSent.Text = "Invoices";
      this.btn_I_InvSent.UseVisualStyleBackColor = false;
      this.btn_I_InvSent.Click += new EventHandler(this.btn_I_InvSent_Click);
      this.btn_I_InvSent.MouseEnter += new EventHandler(this.btn_I_InvSent_MouseEnter);
      this.btn_I_InvSent.MouseLeave += new EventHandler(this.btn_I_InvSent_MouseLeave);
      this.btn_I_Quotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_I_Quotes.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_I_Quotes.FlatAppearance.BorderSize = 0;
      this.btn_I_Quotes.FlatStyle = FlatStyle.Flat;
      this.btn_I_Quotes.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_I_Quotes.ForeColor = Color.White;
      this.btn_I_Quotes.Location = new Point(0, 144);
      this.btn_I_Quotes.Name = "btn_I_Quotes";
      this.btn_I_Quotes.Size = new Size(230, 48);
      this.btn_I_Quotes.TabIndex = 9;
      this.btn_I_Quotes.Text = "Quotes";
      this.btn_I_Quotes.UseVisualStyleBackColor = false;
      this.btn_I_Quotes.Click += new EventHandler(this.btn_I_Quotes_Click);
      this.btn_I_Quotes.MouseEnter += new EventHandler(this.btn_I_Quotes_MouseEnter);
      this.btn_I_Quotes.MouseLeave += new EventHandler(this.btn_I_Quotes_MouseLeave);
      this.btn_I_Orders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_I_Orders.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_I_Orders.FlatAppearance.BorderSize = 0;
      this.btn_I_Orders.FlatStyle = FlatStyle.Flat;
      this.btn_I_Orders.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_I_Orders.ForeColor = Color.White;
      this.btn_I_Orders.Location = new Point(0, 96);
      this.btn_I_Orders.Name = "btn_I_Orders";
      this.btn_I_Orders.Size = new Size(230, 48);
      this.btn_I_Orders.TabIndex = 8;
      this.btn_I_Orders.Text = "Orders";
      this.btn_I_Orders.UseVisualStyleBackColor = false;
      this.btn_I_Orders.Click += new EventHandler(this.btn_I_Orders_Click);
      this.btn_I_Orders.MouseEnter += new EventHandler(this.btn_I_Orders_MouseEnter);
      this.btn_I_Orders.MouseLeave += new EventHandler(this.btn_I_Orders_MouseLeave);
      this.btn_I_Clients.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_I_Clients.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_I_Clients.FlatAppearance.BorderSize = 0;
      this.btn_I_Clients.FlatStyle = FlatStyle.Flat;
      this.btn_I_Clients.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_I_Clients.ForeColor = Color.White;
      this.btn_I_Clients.Location = new Point(0, 48);
      this.btn_I_Clients.Name = "btn_I_Clients";
      this.btn_I_Clients.Size = new Size(230, 48);
      this.btn_I_Clients.TabIndex = 7;
      this.btn_I_Clients.Text = "Clients";
      this.btn_I_Clients.UseVisualStyleBackColor = false;
      this.btn_I_Clients.Click += new EventHandler(this.btn_I_Clients_Click);
      this.btn_I_Clients.MouseEnter += new EventHandler(this.btn_I_Clients_MouseEnter);
      this.btn_I_Clients.MouseLeave += new EventHandler(this.btn_I_Clients_MouseLeave);
      this.btn_Int.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_Int.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Int.FlatAppearance.BorderSize = 0;
      this.btn_Int.FlatStyle = FlatStyle.Flat;
      this.btn_Int.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_Int.ForeColor = Color.White;
      this.btn_Int.Image = (Image) componentResourceManager.GetObject("btn_Int.Image");
      this.btn_Int.Location = new Point(0, 0);
      this.btn_Int.Name = "btn_Int";
      this.btn_Int.Padding = new Padding(18, 0, 0, 0);
      this.btn_Int.Size = new Size(230, 48);
      this.btn_Int.TabIndex = 6;
      this.btn_Int.Text = "International";
      this.btn_Int.TextAlign = ContentAlignment.MiddleLeft;
      this.btn_Int.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_Int.UseVisualStyleBackColor = false;
      this.btn_Int.Click += new EventHandler(this.btn_Int_Click);
      this.btn_Int.MouseEnter += new EventHandler(this.btn_Int_MouseEnter);
      this.btn_Int.MouseLeave += new EventHandler(this.btn_Int_MouseLeave);
      this.btn_Home.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_Home.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Home.FlatAppearance.BorderSize = 0;
      this.btn_Home.FlatStyle = FlatStyle.Flat;
      this.btn_Home.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_Home.ForeColor = Color.White;
      this.btn_Home.Image = (Image) componentResourceManager.GetObject("btn_Home.Image");
      this.btn_Home.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_Home.Location = new Point(0, 0);
      this.btn_Home.Name = "btn_Home";
      this.btn_Home.Padding = new Padding(0, 0, 31, 0);
      this.btn_Home.Size = new Size(230, 48);
      this.btn_Home.TabIndex = 4;
      this.btn_Home.Text = "Dashboard";
      this.btn_Home.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_Home.UseVisualStyleBackColor = false;
      this.btn_Home.Click += new EventHandler(this.btn_Home_Click);
      this.btn_Home.MouseEnter += new EventHandler(this.btn_Home_MouseEnter);
      this.btn_Home.MouseLeave += new EventHandler(this.btn_Home_MouseLeave);
      this.pnl_Local.Controls.Add((Control) this.btn_L_PettyCash);
      this.pnl_Local.Controls.Add((Control) this.pnl_L_Inv);
      this.pnl_Local.Controls.Add((Control) this.btn_L_Clients);
      this.pnl_Local.Controls.Add((Control) this.btn_L_Quotes);
      this.pnl_Local.Controls.Add((Control) this.btn_L_Orders);
      this.pnl_Local.Controls.Add((Control) this.btn_Local);
      this.pnl_Local.Location = new Point(0, 48);
      this.pnl_Local.Name = "pnl_Local";
      this.pnl_Local.Size = new Size(230, 48);
      this.pnl_Local.TabIndex = 5;
      this.btn_L_PettyCash.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_L_PettyCash.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_PettyCash.FlatAppearance.BorderSize = 0;
      this.btn_L_PettyCash.FlatStyle = FlatStyle.Flat;
      this.btn_L_PettyCash.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_L_PettyCash.ForeColor = Color.White;
      this.btn_L_PettyCash.Location = new Point(0, 240);
      this.btn_L_PettyCash.Name = "btn_L_PettyCash";
      this.btn_L_PettyCash.Size = new Size(230, 48);
      this.btn_L_PettyCash.TabIndex = 9;
      this.btn_L_PettyCash.Text = "Petty Cash";
      this.btn_L_PettyCash.UseVisualStyleBackColor = false;
      this.btn_L_PettyCash.Click += new EventHandler(this.btn_L_PettyCash_Click);
      this.btn_L_PettyCash.MouseEnter += new EventHandler(this.btn_L_PettyCash_MouseEnter);
      this.btn_L_PettyCash.MouseLeave += new EventHandler(this.btn_L_PettyCash_MouseLeave);
      this.pnl_L_Inv.Controls.Add((Control) this.btn_L_InvRec);
      this.pnl_L_Inv.Controls.Add((Control) this.btn_L_InvSent);
      this.pnl_L_Inv.Controls.Add((Control) this.btn_L_Invoices);
      this.pnl_L_Inv.Location = new Point(0, 192);
      this.pnl_L_Inv.Name = "pnl_L_Inv";
      this.pnl_L_Inv.Size = new Size(230, 48);
      this.pnl_L_Inv.TabIndex = 8;
      this.btn_L_InvRec.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_L_InvRec.BackColor = Color.FromArgb(35, 35, 35);
      this.btn_L_InvRec.FlatAppearance.BorderSize = 0;
      this.btn_L_InvRec.FlatStyle = FlatStyle.Flat;
      this.btn_L_InvRec.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_L_InvRec.ForeColor = Color.White;
      this.btn_L_InvRec.Location = new Point(0, 96);
      this.btn_L_InvRec.Name = "btn_L_InvRec";
      this.btn_L_InvRec.Size = new Size(230, 48);
      this.btn_L_InvRec.TabIndex = 10;
      this.btn_L_InvRec.Text = "Received";
      this.btn_L_InvRec.UseVisualStyleBackColor = false;
      this.btn_L_InvRec.Click += new EventHandler(this.btn_L_InvRec_Click);
      this.btn_L_InvRec.MouseEnter += new EventHandler(this.btn_L_InvRec_MouseEnter);
      this.btn_L_InvRec.MouseLeave += new EventHandler(this.btn_L_InvRec_MouseLeave);
      this.btn_L_InvSent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_L_InvSent.BackColor = Color.FromArgb(35, 35, 35);
      this.btn_L_InvSent.FlatAppearance.BorderSize = 0;
      this.btn_L_InvSent.FlatStyle = FlatStyle.Flat;
      this.btn_L_InvSent.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_L_InvSent.ForeColor = Color.White;
      this.btn_L_InvSent.Location = new Point(0, 48);
      this.btn_L_InvSent.Name = "btn_L_InvSent";
      this.btn_L_InvSent.Size = new Size(230, 48);
      this.btn_L_InvSent.TabIndex = 9;
      this.btn_L_InvSent.Text = "Sent";
      this.btn_L_InvSent.UseVisualStyleBackColor = false;
      this.btn_L_InvSent.Click += new EventHandler(this.btn_L_InvSent_Click);
      this.btn_L_InvSent.MouseEnter += new EventHandler(this.btn_L_InvSent_MouseEnter);
      this.btn_L_InvSent.MouseLeave += new EventHandler(this.btn_L_InvSent_MouseLeave);
      this.btn_L_Invoices.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_L_Invoices.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Invoices.FlatAppearance.BorderSize = 0;
      this.btn_L_Invoices.FlatStyle = FlatStyle.Flat;
      this.btn_L_Invoices.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_L_Invoices.ForeColor = Color.White;
      this.btn_L_Invoices.Location = new Point(0, 0);
      this.btn_L_Invoices.Name = "btn_L_Invoices";
      this.btn_L_Invoices.Size = new Size(230, 48);
      this.btn_L_Invoices.TabIndex = 8;
      this.btn_L_Invoices.Text = "Invoices";
      this.btn_L_Invoices.UseVisualStyleBackColor = false;
      this.btn_L_Invoices.Click += new EventHandler(this.btn_L_Invoices_Click);
      this.btn_L_Invoices.MouseEnter += new EventHandler(this.btn_L_Invoices_MouseEnter);
      this.btn_L_Invoices.MouseLeave += new EventHandler(this.btn_L_Invoices_MouseLeave);
      this.btn_L_Clients.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_L_Clients.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Clients.FlatAppearance.BorderSize = 0;
      this.btn_L_Clients.FlatStyle = FlatStyle.Flat;
      this.btn_L_Clients.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_L_Clients.ForeColor = Color.White;
      this.btn_L_Clients.Location = new Point(0, 48);
      this.btn_L_Clients.Name = "btn_L_Clients";
      this.btn_L_Clients.Size = new Size(230, 48);
      this.btn_L_Clients.TabIndex = 6;
      this.btn_L_Clients.Text = "Clients";
      this.btn_L_Clients.UseVisualStyleBackColor = false;
      this.btn_L_Clients.Click += new EventHandler(this.btn_L_Clients_Click);
      this.btn_L_Clients.MouseEnter += new EventHandler(this.btn_L_Clients_MouseEnter);
      this.btn_L_Clients.MouseLeave += new EventHandler(this.btn_L_Clients_MouseLeave);
      this.btn_L_Quotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_L_Quotes.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Quotes.FlatAppearance.BorderSize = 0;
      this.btn_L_Quotes.FlatStyle = FlatStyle.Flat;
      this.btn_L_Quotes.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_L_Quotes.ForeColor = Color.White;
      this.btn_L_Quotes.Location = new Point(0, 144);
      this.btn_L_Quotes.Name = "btn_L_Quotes";
      this.btn_L_Quotes.Size = new Size(230, 48);
      this.btn_L_Quotes.TabIndex = 7;
      this.btn_L_Quotes.Text = "Quotes";
      this.btn_L_Quotes.UseVisualStyleBackColor = false;
      this.btn_L_Quotes.Click += new EventHandler(this.btn_L_Quotes_Click);
      this.btn_L_Quotes.MouseEnter += new EventHandler(this.btn_L_Quotes_MouseEnter);
      this.btn_L_Quotes.MouseLeave += new EventHandler(this.btn_L_Quotes_MouseLeave);
      this.btn_L_Orders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_L_Orders.BackColor = Color.FromArgb(50, 50, 50);
      this.btn_L_Orders.FlatAppearance.BorderSize = 0;
      this.btn_L_Orders.FlatStyle = FlatStyle.Flat;
      this.btn_L_Orders.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_L_Orders.ForeColor = Color.White;
      this.btn_L_Orders.Location = new Point(0, 96);
      this.btn_L_Orders.Name = "btn_L_Orders";
      this.btn_L_Orders.Size = new Size(230, 48);
      this.btn_L_Orders.TabIndex = 6;
      this.btn_L_Orders.Text = "Orders";
      this.btn_L_Orders.UseVisualStyleBackColor = false;
      this.btn_L_Orders.Click += new EventHandler(this.btn_L_Orders_Click);
      this.btn_L_Orders.MouseEnter += new EventHandler(this.btn_L_Orders_MouseEnter);
      this.btn_L_Orders.MouseLeave += new EventHandler(this.btn_L_Orders_MouseLeave);
      this.btn_Local.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.btn_Local.BackColor = Color.FromArgb(64, 64, 64);
      this.btn_Local.FlatAppearance.BorderSize = 0;
      this.btn_Local.FlatStyle = FlatStyle.Flat;
      this.btn_Local.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_Local.ForeColor = Color.White;
      this.btn_Local.Image = (Image) componentResourceManager.GetObject("btn_Local.Image");
      this.btn_Local.Location = new Point(0, 0);
      this.btn_Local.Name = "btn_Local";
      this.btn_Local.Size = new Size(230, 48);
      this.btn_Local.TabIndex = 5;
      this.btn_Local.Text = "Local";
      this.btn_Local.TextAlign = ContentAlignment.MiddleLeft;
      this.btn_Local.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_Local.UseVisualStyleBackColor = false;
      this.btn_Local.Click += new EventHandler(this.btn_Local_Click);
      this.btn_Local.MouseEnter += new EventHandler(this.btn_Local_MouseEnter);
      this.btn_Local.MouseLeave += new EventHandler(this.btn_Local_MouseLeave);
      this.pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(0, 550);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(230, 96);
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      this.tmr_Local.Interval = 1;
      this.tmr_Local.Tick += new EventHandler(this.timer1_Tick);
      this.tmr_Int.Interval = 1;
      this.tmr_Int.Tick += new EventHandler(this.timer2_Tick);
      this.tmr_L_Inv.Interval = 1;
      this.tmr_L_Inv.Tick += new EventHandler(this.tmr_L_Inv_Tick);
      this.tmr_Con.Interval = 1;
      this.tmr_Con.Tick += new EventHandler(this.tmr_Con_Tick);
      this.pnl_L_CDet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_L_CDet.Controls.Add((Control) this.btn_LC_Cancel);
      this.pnl_L_CDet.Controls.Add((Control) this.btn_LC_DoneEdit);
      this.pnl_L_CDet.Controls.Add((Control) this.btn_LC_DoneAdd);
      this.pnl_L_CDet.Controls.Add((Control) this.txt_LC_CName);
      this.pnl_L_CDet.Controls.Add((Control) this.txt_LC_CCode);
      this.pnl_L_CDet.Controls.Add((Control) this.bunifuSeparator1);
      this.pnl_L_CDet.Controls.Add((Control) this.dgv_LClients);
      this.pnl_L_CDet.Controls.Add((Control) this.btn_LC_Edit);
      this.pnl_L_CDet.Controls.Add((Control) this.btn_LC_Add);
      this.pnl_L_CDet.Controls.Add((Control) this.btn_LC_Next);
      this.pnl_L_CDet.Controls.Add((Control) this.bunifuCustomLabel2);
      this.pnl_L_CDet.Controls.Add((Control) this.bunifuCustomLabel1);
      this.pnl_L_CDet.Controls.Add((Control) this.btn_LC_Prev);
      this.pnl_L_CDet.Location = new Point(236, 48);
      this.pnl_L_CDet.Name = "pnl_L_CDet";
      this.pnl_L_CDet.Size = new Size(1039, 585);
      this.pnl_L_CDet.TabIndex = 5;
      this.pnl_L_CDet.Visible = false;
      this.pnl_L_CDet.VisibleChanged += new EventHandler(this.pnl_L_CDet_VisibleChanged);
      this.btn_LC_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_LC_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LC_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LC_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_LC_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LC_Cancel.Location = new Point(251, 168);
      this.btn_LC_Cancel.Name = "btn_LC_Cancel";
      this.btn_LC_Cancel.Size = new Size(114, 40);
      this.btn_LC_Cancel.TabIndex = 15;
      this.btn_LC_Cancel.Text = "Cancel";
      this.btn_LC_Cancel.UseVisualStyleBackColor = true;
      this.btn_LC_Cancel.Visible = false;
      this.btn_LC_Cancel.Click += new EventHandler(this.btn_LC_Cancel_Click);
      this.btn_LC_Cancel.MouseEnter += new EventHandler(this.btn_LC_Cancel_MouseEnter);
      this.btn_LC_Cancel.MouseLeave += new EventHandler(this.btn_LC_Cancel_MouseLeave);
      this.btn_LC_DoneEdit.FlatAppearance.BorderSize = 0;
      this.btn_LC_DoneEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LC_DoneEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LC_DoneEdit.FlatStyle = FlatStyle.Flat;
      this.btn_LC_DoneEdit.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LC_DoneEdit.Location = new Point(131, 168);
      this.btn_LC_DoneEdit.Name = "btn_LC_DoneEdit";
      this.btn_LC_DoneEdit.Size = new Size(114, 40);
      this.btn_LC_DoneEdit.TabIndex = 14;
      this.btn_LC_DoneEdit.Text = "Done";
      this.btn_LC_DoneEdit.UseVisualStyleBackColor = true;
      this.btn_LC_DoneEdit.Visible = false;
      this.btn_LC_DoneEdit.Click += new EventHandler(this.btn_LC_DoneEdit_Click);
      this.btn_LC_DoneEdit.MouseEnter += new EventHandler(this.btn_LC_DoneEdit_MouseEnter);
      this.btn_LC_DoneEdit.MouseLeave += new EventHandler(this.btn_LC_DoneEdit_MouseLeave);
      this.btn_LC_DoneAdd.FlatAppearance.BorderSize = 0;
      this.btn_LC_DoneAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LC_DoneAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LC_DoneAdd.FlatStyle = FlatStyle.Flat;
      this.btn_LC_DoneAdd.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LC_DoneAdd.Location = new Point(131, 168);
      this.btn_LC_DoneAdd.Name = "btn_LC_DoneAdd";
      this.btn_LC_DoneAdd.Size = new Size(114, 40);
      this.btn_LC_DoneAdd.TabIndex = 13;
      this.btn_LC_DoneAdd.Text = "Done";
      this.btn_LC_DoneAdd.UseVisualStyleBackColor = true;
      this.btn_LC_DoneAdd.Visible = false;
      this.btn_LC_DoneAdd.Click += new EventHandler(this.btn_LC_DoneAdd_Click);
      this.btn_LC_DoneAdd.MouseEnter += new EventHandler(this.btn_LC_DoneAdd_MouseEnter);
      this.btn_LC_DoneAdd.MouseLeave += new EventHandler(this.btn_LC_DoneAdd_MouseLeave);
      this.txt_LC_CName.Cursor = Cursors.IBeam;
      this.txt_LC_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LC_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LC_CName.HintForeColor = Color.Empty;
      this.txt_LC_CName.HintText = "";
      this.txt_LC_CName.isPassword = false;
      this.txt_LC_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LC_CName.LineIdleColor = Color.Gray;
      this.txt_LC_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LC_CName.LineThickness = 1;
      this.txt_LC_CName.Location = new Point(254, 113);
      this.txt_LC_CName.Margin = new Padding(4);
      this.txt_LC_CName.Name = "txt_LC_CName";
      this.txt_LC_CName.Size = new Size(379, 33);
      this.txt_LC_CName.TabIndex = 12;
      this.txt_LC_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_LC_CName.KeyDown += new KeyEventHandler(this.txt_LC_CName_KeyDown);
      this.txt_LC_CCode.Cursor = Cursors.IBeam;
      this.txt_LC_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_LC_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_LC_CCode.HintForeColor = Color.Empty;
      this.txt_LC_CCode.HintText = "";
      this.txt_LC_CCode.isPassword = false;
      this.txt_LC_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_LC_CCode.LineIdleColor = Color.Gray;
      this.txt_LC_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_LC_CCode.LineThickness = 1;
      this.txt_LC_CCode.Location = new Point(253, 51);
      this.txt_LC_CCode.Margin = new Padding(4);
      this.txt_LC_CCode.Name = "txt_LC_CCode";
      this.txt_LC_CCode.Size = new Size(379, 33);
      this.txt_LC_CCode.TabIndex = 11;
      this.txt_LC_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_LC_CCode.KeyDown += new KeyEventHandler(this.txt_LC_CCode_KeyDown);
      this.bunifuSeparator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator1.BackColor = Color.Transparent;
      this.bunifuSeparator1.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator1.LineThickness = 1;
      this.bunifuSeparator1.Location = new Point(18, 205);
      this.bunifuSeparator1.Name = "bunifuSeparator1";
      this.bunifuSeparator1.Size = new Size(1005, 35);
      this.bunifuSeparator1.TabIndex = 10;
      this.bunifuSeparator1.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator1.Vertical = false;
      this.dgv_LClients.AllowUserToAddRows = false;
      this.dgv_LClients.AllowUserToDeleteRows = false;
      this.dgv_LClients.AllowUserToResizeColumns = false;
      this.dgv_LClients.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_LClients.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_LClients.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_LClients.AutoGenerateContextFilters = true;
      this.dgv_LClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_LClients.BorderStyle = BorderStyle.None;
      this.dgv_LClients.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_LClients.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_LClients.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_LClients.ColumnHeadersHeight = 25;
      this.dgv_LClients.DateWithTime = false;
      this.dgv_LClients.EnableHeadersVisualStyles = false;
      this.dgv_LClients.Location = new Point(0, 246);
      this.dgv_LClients.Name = "dgv_LClients";
      this.dgv_LClients.ReadOnly = true;
      this.dgv_LClients.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_LClients.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_LClients.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_LClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_LClients.Size = new Size(1039, 339);
      this.dgv_LClients.TabIndex = 9;
      this.dgv_LClients.TimeFilter = false;
      this.dgv_LClients.SortStringChanged += new EventHandler(this.dgv_LClients_SortStringChanged);
      this.dgv_LClients.FilterStringChanged += new EventHandler(this.dgv_LClients_FilterStringChanged);
      this.dgv_LClients.CellClick += new DataGridViewCellEventHandler(this.dgv_CellClick);
      this.btn_LC_Edit.FlatAppearance.BorderSize = 0;
      this.btn_LC_Edit.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LC_Edit.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LC_Edit.FlatStyle = FlatStyle.Flat;
      this.btn_LC_Edit.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LC_Edit.Image = (Image) componentResourceManager.GetObject("btn_LC_Edit.Image");
      this.btn_LC_Edit.Location = new Point(251, 168);
      this.btn_LC_Edit.Name = "btn_LC_Edit";
      this.btn_LC_Edit.Size = new Size(114, 40);
      this.btn_LC_Edit.TabIndex = 8;
      this.btn_LC_Edit.Text = "Edit";
      this.btn_LC_Edit.TextAlign = ContentAlignment.MiddleRight;
      this.btn_LC_Edit.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LC_Edit.UseVisualStyleBackColor = true;
      this.btn_LC_Edit.Click += new EventHandler(this.btn_LC_Edit_Click);
      this.btn_LC_Edit.MouseEnter += new EventHandler(this.btn_LC_Edit_MouseEnter);
      this.btn_LC_Edit.MouseLeave += new EventHandler(this.btn_LC_Edit_MouseLeave);
      this.btn_LC_Add.FlatAppearance.BorderSize = 0;
      this.btn_LC_Add.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LC_Add.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LC_Add.FlatStyle = FlatStyle.Flat;
      this.btn_LC_Add.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LC_Add.Image = (Image) componentResourceManager.GetObject("btn_LC_Add.Image");
      this.btn_LC_Add.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_LC_Add.Location = new Point(131, 168);
      this.btn_LC_Add.Name = "btn_LC_Add";
      this.btn_LC_Add.Size = new Size(114, 40);
      this.btn_LC_Add.TabIndex = 7;
      this.btn_LC_Add.Text = "Add";
      this.btn_LC_Add.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_LC_Add.UseVisualStyleBackColor = true;
      this.btn_LC_Add.Click += new EventHandler(this.btn_LC_Add_Click);
      this.btn_LC_Add.MouseEnter += new EventHandler(this.btn_LC_Add_MouseEnter);
      this.btn_LC_Add.MouseLeave += new EventHandler(this.btn_LC_Add_MouseLeave);
      this.btn_LC_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_LC_Next.FlatAppearance.BorderSize = 0;
      this.btn_LC_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LC_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LC_Next.FlatStyle = FlatStyle.Flat;
      this.btn_LC_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LC_Next.ForeColor = Color.White;
      this.btn_LC_Next.Image = (Image) componentResourceManager.GetObject("btn_LC_Next.Image");
      this.btn_LC_Next.Location = new Point(974, 19);
      this.btn_LC_Next.Name = "btn_LC_Next";
      this.btn_LC_Next.Size = new Size(49, 149);
      this.btn_LC_Next.TabIndex = 6;
      this.btn_LC_Next.UseVisualStyleBackColor = true;
      this.btn_LC_Next.Click += new EventHandler(this.btn_LC_Next_Click);
      this.btn_LC_Next.MouseEnter += new EventHandler(this.btn_LC_Next_MouseEnter);
      this.btn_LC_Next.MouseLeave += new EventHandler(this.btn_LC_Next_MouseLeave);
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.Location = new Point((int) sbyte.MaxValue, 115);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(118, 24);
      this.bunifuCustomLabel2.TabIndex = 4;
      this.bunifuCustomLabel2.Text = "Client Name:";
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.Location = new Point(132, 54);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(113, 24);
      this.bunifuCustomLabel1.TabIndex = 3;
      this.bunifuCustomLabel1.Text = "Client Code:";
      this.btn_LC_Prev.Enabled = false;
      this.btn_LC_Prev.FlatAppearance.BorderSize = 0;
      this.btn_LC_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_LC_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_LC_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_LC_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_LC_Prev.ForeColor = Color.White;
      this.btn_LC_Prev.Image = (Image) componentResourceManager.GetObject("btn_LC_Prev.Image");
      this.btn_LC_Prev.Location = new Point(18, 19);
      this.btn_LC_Prev.Name = "btn_LC_Prev";
      this.btn_LC_Prev.Size = new Size(49, 149);
      this.btn_LC_Prev.TabIndex = 0;
      this.btn_LC_Prev.UseVisualStyleBackColor = true;
      this.btn_LC_Prev.Click += new EventHandler(this.btn_LC_Prev_Click);
      this.btn_LC_Prev.MouseEnter += new EventHandler(this.btn_LC_Prev_MouseEnter);
      this.btn_LC_Prev.MouseLeave += new EventHandler(this.btn_LC_Prev_MouseLeave);
      this.pnl_L_Orders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_L_Orders.BackColor = Color.LightGray;
      this.pnl_L_Orders.Location = new Point(236, 48);
      this.pnl_L_Orders.Name = "pnl_L_Orders";
      this.pnl_L_Orders.Size = new Size(1039, 585);
      this.pnl_L_Orders.TabIndex = 10;
      this.pnl_L_Orders.Visible = false;
      this.pnl_L_Quotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_L_Quotes.Location = new Point(236, 48);
      this.pnl_L_Quotes.Name = "pnl_L_Quotes";
      this.pnl_L_Quotes.Size = new Size(1039, 585);
      this.pnl_L_Quotes.TabIndex = 9;
      this.pnl_L_Quotes.Visible = false;
      this.pnl_L_InvSent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_L_InvSent.Location = new Point(236, 48);
      this.pnl_L_InvSent.Name = "pnl_L_InvSent";
      this.pnl_L_InvSent.Size = new Size(1039, 585);
      this.pnl_L_InvSent.TabIndex = 9;
      this.pnl_L_InvSent.Visible = false;
      this.pnl_L_InvRec.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_L_InvRec.Location = new Point(236, 48);
      this.pnl_L_InvRec.Name = "pnl_L_InvRec";
      this.pnl_L_InvRec.Size = new Size(1039, 585);
      this.pnl_L_InvRec.TabIndex = 9;
      this.pnl_L_InvRec.Visible = false;
      this.pnl_I_Clients.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_I_Clients.Controls.Add((Control) this.btn_IC_Cancel);
      this.pnl_I_Clients.Controls.Add((Control) this.btn_IC_DoneAdd);
      this.pnl_I_Clients.Controls.Add((Control) this.btn_IC_DoneEdit);
      this.pnl_I_Clients.Controls.Add((Control) this.txt_IC_CName);
      this.pnl_I_Clients.Controls.Add((Control) this.txt_IC_CCode);
      this.pnl_I_Clients.Controls.Add((Control) this.bunifuSeparator2);
      this.pnl_I_Clients.Controls.Add((Control) this.dgv_IClients);
      this.pnl_I_Clients.Controls.Add((Control) this.btn_IC_Edit);
      this.pnl_I_Clients.Controls.Add((Control) this.btn_IC_Add);
      this.pnl_I_Clients.Controls.Add((Control) this.btn_IC_Next);
      this.pnl_I_Clients.Controls.Add((Control) this.bunifuCustomLabel3);
      this.pnl_I_Clients.Controls.Add((Control) this.bunifuCustomLabel4);
      this.pnl_I_Clients.Controls.Add((Control) this.btn_IC_Prev);
      this.pnl_I_Clients.Location = new Point(236, 48);
      this.pnl_I_Clients.Name = "pnl_I_Clients";
      this.pnl_I_Clients.Size = new Size(1039, 585);
      this.pnl_I_Clients.TabIndex = 9;
      this.pnl_I_Clients.Visible = false;
      this.pnl_I_Clients.VisibleChanged += new EventHandler(this.pnl_I_Clients_VisibleChanged);
      this.btn_IC_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_IC_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IC_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IC_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_IC_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IC_Cancel.Location = new Point(254, 168);
      this.btn_IC_Cancel.Name = "btn_IC_Cancel";
      this.btn_IC_Cancel.Size = new Size(114, 40);
      this.btn_IC_Cancel.TabIndex = 28;
      this.btn_IC_Cancel.Text = "Cancel";
      this.btn_IC_Cancel.UseVisualStyleBackColor = true;
      this.btn_IC_Cancel.Visible = false;
      this.btn_IC_Cancel.Click += new EventHandler(this.btn_IC_Cancel_Click);
      this.btn_IC_Cancel.MouseEnter += new EventHandler(this.btn_IC_Cancel_MouseEnter);
      this.btn_IC_Cancel.MouseLeave += new EventHandler(this.btn_IC_Cancel_MouseLeave);
      this.btn_IC_DoneAdd.FlatAppearance.BorderSize = 0;
      this.btn_IC_DoneAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IC_DoneAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IC_DoneAdd.FlatStyle = FlatStyle.Flat;
      this.btn_IC_DoneAdd.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IC_DoneAdd.Location = new Point(131, 168);
      this.btn_IC_DoneAdd.Name = "btn_IC_DoneAdd";
      this.btn_IC_DoneAdd.Size = new Size(114, 40);
      this.btn_IC_DoneAdd.TabIndex = 27;
      this.btn_IC_DoneAdd.Text = "Done";
      this.btn_IC_DoneAdd.UseVisualStyleBackColor = true;
      this.btn_IC_DoneAdd.Visible = false;
      this.btn_IC_DoneAdd.Click += new EventHandler(this.btn_IC_DoneAdd_Click);
      this.btn_IC_DoneAdd.MouseEnter += new EventHandler(this.btn_IC_DoneAdd_MouseEnter);
      this.btn_IC_DoneAdd.MouseLeave += new EventHandler(this.btn_IC_DoneAdd_MouseLeave);
      this.btn_IC_DoneEdit.FlatAppearance.BorderSize = 0;
      this.btn_IC_DoneEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IC_DoneEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IC_DoneEdit.FlatStyle = FlatStyle.Flat;
      this.btn_IC_DoneEdit.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IC_DoneEdit.Location = new Point(131, 168);
      this.btn_IC_DoneEdit.Name = "btn_IC_DoneEdit";
      this.btn_IC_DoneEdit.Size = new Size(114, 40);
      this.btn_IC_DoneEdit.TabIndex = 26;
      this.btn_IC_DoneEdit.Text = "Done";
      this.btn_IC_DoneEdit.UseVisualStyleBackColor = true;
      this.btn_IC_DoneEdit.Visible = false;
      this.btn_IC_DoneEdit.Click += new EventHandler(this.btn_IC_DoneEdit_Click);
      this.btn_IC_DoneEdit.MouseEnter += new EventHandler(this.btn_IC_DoneEdit_MouseEnter);
      this.btn_IC_DoneEdit.MouseLeave += new EventHandler(this.btn_IC_DoneEdit_MouseLeave);
      this.txt_IC_CName.Cursor = Cursors.IBeam;
      this.txt_IC_CName.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IC_CName.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IC_CName.HintForeColor = Color.Empty;
      this.txt_IC_CName.HintText = "";
      this.txt_IC_CName.isPassword = false;
      this.txt_IC_CName.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IC_CName.LineIdleColor = Color.Gray;
      this.txt_IC_CName.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IC_CName.LineThickness = 1;
      this.txt_IC_CName.Location = new Point(254, 113);
      this.txt_IC_CName.Margin = new Padding(4);
      this.txt_IC_CName.Name = "txt_IC_CName";
      this.txt_IC_CName.Size = new Size(379, 33);
      this.txt_IC_CName.TabIndex = 25;
      this.txt_IC_CName.TextAlign = HorizontalAlignment.Left;
      this.txt_IC_CName.KeyDown += new KeyEventHandler(this.txt_IC_CName_KeyDown);
      this.txt_IC_CCode.Cursor = Cursors.IBeam;
      this.txt_IC_CCode.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_IC_CCode.ForeColor = Color.FromArgb(19, 118, 188);
      this.txt_IC_CCode.HintForeColor = Color.Empty;
      this.txt_IC_CCode.HintText = "";
      this.txt_IC_CCode.isPassword = false;
      this.txt_IC_CCode.LineFocusedColor = Color.FromArgb(19, 118, 188);
      this.txt_IC_CCode.LineIdleColor = Color.Gray;
      this.txt_IC_CCode.LineMouseHoverColor = Color.FromArgb(19, 118, 188);
      this.txt_IC_CCode.LineThickness = 1;
      this.txt_IC_CCode.Location = new Point(253, 51);
      this.txt_IC_CCode.Margin = new Padding(4);
      this.txt_IC_CCode.Name = "txt_IC_CCode";
      this.txt_IC_CCode.Size = new Size(379, 33);
      this.txt_IC_CCode.TabIndex = 24;
      this.txt_IC_CCode.TextAlign = HorizontalAlignment.Left;
      this.txt_IC_CCode.KeyDown += new KeyEventHandler(this.txt_IC_CCode_KeyDown);
      this.bunifuSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.bunifuSeparator2.BackColor = Color.Transparent;
      this.bunifuSeparator2.LineColor = Color.FromArgb(105, 105, 105);
      this.bunifuSeparator2.LineThickness = 1;
      this.bunifuSeparator2.Location = new Point(18, 205);
      this.bunifuSeparator2.Name = "bunifuSeparator2";
      this.bunifuSeparator2.Size = new Size(1005, 35);
      this.bunifuSeparator2.TabIndex = 23;
      this.bunifuSeparator2.Transparency = (int) byte.MaxValue;
      this.bunifuSeparator2.Vertical = false;
      this.dgv_IClients.AllowUserToAddRows = false;
      this.dgv_IClients.AllowUserToDeleteRows = false;
      this.dgv_IClients.AllowUserToResizeColumns = false;
      this.dgv_IClients.AllowUserToResizeRows = false;
      gridViewCellStyle4.BackColor = Color.LightGray;
      this.dgv_IClients.AlternatingRowsDefaultCellStyle = gridViewCellStyle4;
      this.dgv_IClients.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_IClients.AutoGenerateContextFilters = true;
      this.dgv_IClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_IClients.BorderStyle = BorderStyle.None;
      this.dgv_IClients.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_IClients.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle5.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle5.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle5.ForeColor = Color.White;
      gridViewCellStyle5.SelectionBackColor = Color.Gray;
      gridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle5.WrapMode = DataGridViewTriState.True;
      this.dgv_IClients.ColumnHeadersDefaultCellStyle = gridViewCellStyle5;
      this.dgv_IClients.ColumnHeadersHeight = 25;
      this.dgv_IClients.DateWithTime = false;
      this.dgv_IClients.EnableHeadersVisualStyles = false;
      this.dgv_IClients.Location = new Point(0, 246);
      this.dgv_IClients.Name = "dgv_IClients";
      this.dgv_IClients.ReadOnly = true;
      this.dgv_IClients.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_IClients.RowHeadersVisible = false;
      gridViewCellStyle6.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_IClients.RowsDefaultCellStyle = gridViewCellStyle6;
      this.dgv_IClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_IClients.Size = new Size(1039, 339);
      this.dgv_IClients.TabIndex = 22;
      this.dgv_IClients.TimeFilter = false;
      this.dgv_IClients.SortStringChanged += new EventHandler(this.dgv_IClients_SortStringChanged);
      this.dgv_IClients.FilterStringChanged += new EventHandler(this.dgv_IClients_FilterStringChanged);
      this.dgv_IClients.CellClick += new DataGridViewCellEventHandler(this.dgv_I_CellClick);
      this.btn_IC_Edit.FlatAppearance.BorderSize = 0;
      this.btn_IC_Edit.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IC_Edit.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IC_Edit.FlatStyle = FlatStyle.Flat;
      this.btn_IC_Edit.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IC_Edit.Image = (Image) componentResourceManager.GetObject("btn_IC_Edit.Image");
      this.btn_IC_Edit.Location = new Point(251, 168);
      this.btn_IC_Edit.Name = "btn_IC_Edit";
      this.btn_IC_Edit.Size = new Size(114, 40);
      this.btn_IC_Edit.TabIndex = 21;
      this.btn_IC_Edit.Text = "Edit";
      this.btn_IC_Edit.TextAlign = ContentAlignment.MiddleRight;
      this.btn_IC_Edit.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IC_Edit.UseVisualStyleBackColor = true;
      this.btn_IC_Edit.Click += new EventHandler(this.btn_IC_Edit_Click);
      this.btn_IC_Edit.MouseEnter += new EventHandler(this.btn_IC_Edit_MouseEnter);
      this.btn_IC_Edit.MouseLeave += new EventHandler(this.btn_IC_Edit_MouseLeave);
      this.btn_IC_Add.FlatAppearance.BorderSize = 0;
      this.btn_IC_Add.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IC_Add.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IC_Add.FlatStyle = FlatStyle.Flat;
      this.btn_IC_Add.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IC_Add.Image = (Image) componentResourceManager.GetObject("btn_IC_Add.Image");
      this.btn_IC_Add.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_IC_Add.Location = new Point(131, 168);
      this.btn_IC_Add.Name = "btn_IC_Add";
      this.btn_IC_Add.Size = new Size(114, 40);
      this.btn_IC_Add.TabIndex = 20;
      this.btn_IC_Add.Text = "Add";
      this.btn_IC_Add.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_IC_Add.UseVisualStyleBackColor = true;
      this.btn_IC_Add.Click += new EventHandler(this.btn_IC_Add_Click);
      this.btn_IC_Add.MouseEnter += new EventHandler(this.btn_IC_Add_MouseEnter);
      this.btn_IC_Add.MouseLeave += new EventHandler(this.btn_IC_Add_MouseLeave);
      this.btn_IC_Next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_IC_Next.FlatAppearance.BorderSize = 0;
      this.btn_IC_Next.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IC_Next.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IC_Next.FlatStyle = FlatStyle.Flat;
      this.btn_IC_Next.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IC_Next.ForeColor = Color.White;
      this.btn_IC_Next.Image = (Image) componentResourceManager.GetObject("btn_IC_Next.Image");
      this.btn_IC_Next.Location = new Point(974, 19);
      this.btn_IC_Next.Name = "btn_IC_Next";
      this.btn_IC_Next.Size = new Size(49, 149);
      this.btn_IC_Next.TabIndex = 19;
      this.btn_IC_Next.UseVisualStyleBackColor = true;
      this.btn_IC_Next.Click += new EventHandler(this.btn_IC_Next_Click);
      this.btn_IC_Next.MouseEnter += new EventHandler(this.btn_IC_Next_MouseEnter);
      this.btn_IC_Next.MouseLeave += new EventHandler(this.btn_IC_Next_MouseLeave);
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel3.Location = new Point((int) sbyte.MaxValue, 115);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new Size(118, 24);
      this.bunifuCustomLabel3.TabIndex = 18;
      this.bunifuCustomLabel3.Text = "Client Name:";
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.Location = new Point(132, 54);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(113, 24);
      this.bunifuCustomLabel4.TabIndex = 17;
      this.bunifuCustomLabel4.Text = "Client Code:";
      this.btn_IC_Prev.Enabled = false;
      this.btn_IC_Prev.FlatAppearance.BorderSize = 0;
      this.btn_IC_Prev.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_IC_Prev.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_IC_Prev.FlatStyle = FlatStyle.Flat;
      this.btn_IC_Prev.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_IC_Prev.ForeColor = Color.White;
      this.btn_IC_Prev.Image = (Image) componentResourceManager.GetObject("btn_IC_Prev.Image");
      this.btn_IC_Prev.Location = new Point(18, 19);
      this.btn_IC_Prev.Name = "btn_IC_Prev";
      this.btn_IC_Prev.Size = new Size(49, 149);
      this.btn_IC_Prev.TabIndex = 16;
      this.btn_IC_Prev.UseVisualStyleBackColor = true;
      this.btn_IC_Prev.Click += new EventHandler(this.btn_IC_Prev_Click);
      this.btn_IC_Prev.MouseEnter += new EventHandler(this.btn_IC_Prev_MouseEnter);
      this.btn_IC_Prev.MouseLeave += new EventHandler(this.btn_IC_Prev_MouseLeave);
      this.pnl_I_Orders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_I_Orders.Location = new Point(236, 48);
      this.pnl_I_Orders.Name = "pnl_I_Orders";
      this.pnl_I_Orders.Size = new Size(1039, 585);
      this.pnl_I_Orders.TabIndex = 9;
      this.pnl_I_Orders.Visible = false;
      this.pnl_I_Quotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_I_Quotes.Location = new Point(236, 48);
      this.pnl_I_Quotes.Name = "pnl_I_Quotes";
      this.pnl_I_Quotes.Size = new Size(1039, 585);
      this.pnl_I_Quotes.TabIndex = 9;
      this.pnl_I_Quotes.Visible = false;
      this.pnl_I_InvSent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_I_InvSent.Location = new Point(236, 48);
      this.pnl_I_InvSent.Name = "pnl_I_InvSent";
      this.pnl_I_InvSent.Size = new Size(1039, 585);
      this.pnl_I_InvSent.TabIndex = 9;
      this.pnl_I_InvSent.Visible = false;
      this.pnl_Contractors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_Contractors.Location = new Point(236, 48);
      this.pnl_Contractors.Name = "pnl_Contractors";
      this.pnl_Contractors.Size = new Size(1039, 585);
      this.pnl_Contractors.TabIndex = 9;
      this.pnl_Contractors.Visible = false;
      this.lblComing.AutoSize = true;
      this.lblComing.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblComing.ForeColor = Color.DarkGray;
      this.lblComing.Location = new Point(416, 297);
      this.lblComing.Name = "lblComing";
      this.lblComing.Size = new Size(138, 25);
      this.lblComing.TabIndex = 12;
      this.lblComing.Text = "Coming Soon!";
      this.pnl_Home.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_Home.Controls.Add((Control) this.lblComing);
      this.pnl_Home.Location = new Point(236, 48);
      this.pnl_Home.Name = "pnl_Home";
      this.pnl_Home.Size = new Size(1039, 585);
      this.pnl_Home.TabIndex = 9;
      this.pnl_Home.Visible = false;
      this.pnl_C_NoRem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_C_NoRem.Controls.Add((Control) this.dgv_NoRem);
      this.pnl_C_NoRem.Location = new Point(236, 48);
      this.pnl_C_NoRem.Name = "pnl_C_NoRem";
      this.pnl_C_NoRem.Size = new Size(1039, 585);
      this.pnl_C_NoRem.TabIndex = 10;
      this.pnl_C_NoRem.Visible = false;
      this.dgv_NoRem.AllowUserToAddRows = false;
      this.dgv_NoRem.AllowUserToDeleteRows = false;
      this.dgv_NoRem.AllowUserToResizeColumns = false;
      this.dgv_NoRem.AllowUserToResizeRows = false;
      gridViewCellStyle7.BackColor = Color.LightGray;
      this.dgv_NoRem.AlternatingRowsDefaultCellStyle = gridViewCellStyle7;
      this.dgv_NoRem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_NoRem.AutoGenerateContextFilters = true;
      this.dgv_NoRem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_NoRem.BorderStyle = BorderStyle.None;
      this.dgv_NoRem.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_NoRem.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle8.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle8.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle8.ForeColor = Color.White;
      gridViewCellStyle8.SelectionBackColor = Color.Gray;
      gridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle8.WrapMode = DataGridViewTriState.True;
      this.dgv_NoRem.ColumnHeadersDefaultCellStyle = gridViewCellStyle8;
      this.dgv_NoRem.ColumnHeadersHeight = 25;
      this.dgv_NoRem.DateWithTime = false;
      this.dgv_NoRem.EnableHeadersVisualStyles = false;
      this.dgv_NoRem.Location = new Point(0, 0);
      this.dgv_NoRem.Name = "dgv_NoRem";
      this.dgv_NoRem.ReadOnly = true;
      this.dgv_NoRem.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_NoRem.RowHeadersVisible = false;
      gridViewCellStyle9.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_NoRem.RowsDefaultCellStyle = gridViewCellStyle9;
      this.dgv_NoRem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_NoRem.Size = new Size(1039, 585);
      this.dgv_NoRem.TabIndex = 2;
      this.dgv_NoRem.TimeFilter = false;
      this.pnl_C_NoInv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_C_NoInv.Controls.Add((Control) this.dgv_NoInv);
      this.pnl_C_NoInv.Location = new Point(236, 48);
      this.pnl_C_NoInv.Name = "pnl_C_NoInv";
      this.pnl_C_NoInv.Size = new Size(1039, 585);
      this.pnl_C_NoInv.TabIndex = 10;
      this.pnl_C_NoInv.Visible = false;
      this.dgv_NoInv.AllowUserToAddRows = false;
      this.dgv_NoInv.AllowUserToDeleteRows = false;
      this.dgv_NoInv.AllowUserToResizeColumns = false;
      this.dgv_NoInv.AllowUserToResizeRows = false;
      gridViewCellStyle10.BackColor = Color.LightGray;
      this.dgv_NoInv.AlternatingRowsDefaultCellStyle = gridViewCellStyle10;
      this.dgv_NoInv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_NoInv.AutoGenerateContextFilters = true;
      this.dgv_NoInv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_NoInv.BorderStyle = BorderStyle.None;
      this.dgv_NoInv.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_NoInv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle11.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle11.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle11.ForeColor = Color.White;
      gridViewCellStyle11.SelectionBackColor = Color.Gray;
      gridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle11.WrapMode = DataGridViewTriState.True;
      this.dgv_NoInv.ColumnHeadersDefaultCellStyle = gridViewCellStyle11;
      this.dgv_NoInv.ColumnHeadersHeight = 25;
      this.dgv_NoInv.DateWithTime = false;
      this.dgv_NoInv.EnableHeadersVisualStyles = false;
      this.dgv_NoInv.Location = new Point(0, 0);
      this.dgv_NoInv.Name = "dgv_NoInv";
      this.dgv_NoInv.ReadOnly = true;
      this.dgv_NoInv.RowHeadersVisible = false;
      gridViewCellStyle12.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_NoInv.RowsDefaultCellStyle = gridViewCellStyle12;
      this.dgv_NoInv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgv_NoInv.Size = new Size(1039, 585);
      this.dgv_NoInv.TabIndex = 3;
      this.dgv_NoInv.TimeFilter = false;
      this.pnl_Projects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_Projects.Location = new Point(236, 48);
      this.pnl_Projects.Name = "pnl_Projects";
      this.pnl_Projects.Size = new Size(1039, 585);
      this.pnl_Projects.TabIndex = 11;
      this.pnl_Projects.Visible = false;
      this.pnl_L_PettyCash.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnl_L_PettyCash.Location = new Point(236, 48);
      this.pnl_L_PettyCash.Name = "pnl_L_PettyCash";
      this.pnl_L_PettyCash.Size = new Size(1039, 585);
      this.pnl_L_PettyCash.TabIndex = 11;
      this.pnl_L_PettyCash.Visible = false;
      this.btn_Home_Min.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_Home_Min.BackColor = Color.LightGray;
      this.btn_Home_Min.FlatAppearance.BorderSize = 0;
      this.btn_Home_Min.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_Home_Min.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_Home_Min.FlatStyle = FlatStyle.Flat;
      this.btn_Home_Min.Image = (Image) componentResourceManager.GetObject("btn_Home_Min.Image");
      this.btn_Home_Min.Location = new Point(1191, -1);
      this.btn_Home_Min.Name = "btn_Home_Min";
      this.btn_Home_Min.Padding = new Padding(0, 0, 1, 0);
      this.btn_Home_Min.Size = new Size(31, 29);
      this.btn_Home_Min.TabIndex = 11;
      this.btn_Home_Min.UseVisualStyleBackColor = false;
      this.btn_Home_Min.Click += new EventHandler(this.btn_Home_Min_Click);
      this.btn_Home_Min.MouseEnter += new EventHandler(this.btn_Home_Min_MouseEnter);
      this.btn_Home_Min.MouseLeave += new EventHandler(this.btn_Home_Min_MouseLeave);
      this.btn_Home_Nor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_Home_Nor.BackColor = Color.LightGray;
      this.btn_Home_Nor.FlatAppearance.BorderSize = 0;
      this.btn_Home_Nor.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_Home_Nor.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_Home_Nor.FlatStyle = FlatStyle.Flat;
      this.btn_Home_Nor.Image = (Image) componentResourceManager.GetObject("btn_Home_Nor.Image");
      this.btn_Home_Nor.Location = new Point(1222, -1);
      this.btn_Home_Nor.Name = "btn_Home_Nor";
      this.btn_Home_Nor.Padding = new Padding(0, 0, 1, 0);
      this.btn_Home_Nor.Size = new Size(31, 29);
      this.btn_Home_Nor.TabIndex = 8;
      this.btn_Home_Nor.UseVisualStyleBackColor = false;
      this.btn_Home_Nor.Visible = false;
      this.btn_Home_Nor.Click += new EventHandler(this.btn_Home_Nor_Click);
      this.btn_Home_Nor.MouseEnter += new EventHandler(this.btn_Home_Nor_MouseEnter);
      this.btn_Home_Nor.MouseLeave += new EventHandler(this.btn_Home_Nor_MouseLeave);
      this.btn_Home_Max.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_Home_Max.BackColor = Color.LightGray;
      this.btn_Home_Max.FlatAppearance.BorderSize = 0;
      this.btn_Home_Max.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_Home_Max.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_Home_Max.FlatStyle = FlatStyle.Flat;
      this.btn_Home_Max.Image = (Image) componentResourceManager.GetObject("btn_Home_Max.Image");
      this.btn_Home_Max.Location = new Point(1224, -1);
      this.btn_Home_Max.Name = "btn_Home_Max";
      this.btn_Home_Max.Padding = new Padding(0, 0, 1, 0);
      this.btn_Home_Max.Size = new Size(31, 29);
      this.btn_Home_Max.TabIndex = 7;
      this.btn_Home_Max.UseVisualStyleBackColor = false;
      this.btn_Home_Max.Click += new EventHandler(this.btn_Home_Max_Click);
      this.btn_Home_Max.MouseEnter += new EventHandler(this.btn_Home_Max_MouseEnter);
      this.btn_Home_Max.MouseLeave += new EventHandler(this.btn_Home_Max_MouseLeave);
      this.btn_Home_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_Home_Close.BackColor = Color.LightGray;
      this.btn_Home_Close.FlatAppearance.BorderSize = 0;
      this.btn_Home_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_Home_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_Home_Close.FlatStyle = FlatStyle.Flat;
      this.btn_Home_Close.Image = (Image) componentResourceManager.GetObject("btn_Home_Close.Image");
      this.btn_Home_Close.Location = new Point(1255, -1);
      this.btn_Home_Close.Name = "btn_Home_Close";
      this.btn_Home_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_Home_Close.Size = new Size(31, 29);
      this.btn_Home_Close.TabIndex = 6;
      this.btn_Home_Close.UseVisualStyleBackColor = false;
      this.btn_Home_Close.Click += new EventHandler(this.btn_Home_Close_Click);
      this.btn_Home_Close.MouseEnter += new EventHandler(this.btn_Home_Close_MouseEnter);
      this.btn_Home_Close.MouseLeave += new EventHandler(this.btn_Home_Close_MouseLeave);
      this.clientsBindingSource1.DataMember = "Clients";
      this.BackColor = Color.LightGray;
      this.BackgroundImageLayout = ImageLayout.Center;
      this.ClientSize = new Size(1286, 644);
      this.ControlBox = false;
      this.Controls.Add((Control) this.pnl_L_PettyCash);
      this.Controls.Add((Control) this.pnl_Projects);
      this.Controls.Add((Control) this.pnl_C_NoInv);
      this.Controls.Add((Control) this.pnl_C_NoRem);
      this.Controls.Add((Control) this.pnl_Home);
      this.Controls.Add((Control) this.pnl_Contractors);
      this.Controls.Add((Control) this.pnl_I_InvSent);
      this.Controls.Add((Control) this.pnl_I_Quotes);
      this.Controls.Add((Control) this.pnl_I_Orders);
      this.Controls.Add((Control) this.pnl_I_Clients);
      this.Controls.Add((Control) this.pnl_L_InvRec);
      this.Controls.Add((Control) this.pnl_L_InvSent);
      this.Controls.Add((Control) this.pnl_L_Quotes);
      this.Controls.Add((Control) this.btn_Home_Min);
      this.Controls.Add((Control) this.pnl_L_Orders);
      this.Controls.Add((Control) this.btn_Home_Nor);
      this.Controls.Add((Control) this.btn_Home_Max);
      this.Controls.Add((Control) this.btn_Home_Close);
      this.Controls.Add((Control) this.pnl_L_CDet);
      this.Controls.Add((Control) this.panel1);
      this.ForeColor = Color.FromArgb(64, 64, 64);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(980, 580);
      this.Name = nameof (Home);
      this.Padding = new Padding(20, 30, 20, 20);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "btn_Home";
      this.Load += new EventHandler(this.Home_Load);
      this.panel1.ResumeLayout(false);
      this.pnl_Con.ResumeLayout(false);
      this.pnl_Int.ResumeLayout(false);
      this.pnl_Local.ResumeLayout(false);
      this.pnl_L_Inv.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.pnl_L_CDet.ResumeLayout(false);
      this.pnl_L_CDet.PerformLayout();
      ((ISupportInitialize) this.dgv_LClients).EndInit();
      this.pnl_I_Clients.ResumeLayout(false);
      this.pnl_I_Clients.PerformLayout();
      ((ISupportInitialize) this.dgv_IClients).EndInit();
      this.pnl_Home.ResumeLayout(false);
      this.pnl_Home.PerformLayout();
      this.pnl_C_NoRem.ResumeLayout(false);
      ((ISupportInitialize) this.dgv_NoRem).EndInit();
      this.pnl_C_NoInv.ResumeLayout(false);
      ((ISupportInitialize) this.dgv_NoInv).EndInit();
      ((ISupportInitialize) this.clientsBindingSource1).EndInit();
      this.ResumeLayout(false);
    }
  }
}
