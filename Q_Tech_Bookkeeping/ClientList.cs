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
    public partial class ClientList : Form
    {
        private BindingSource bs = new BindingSource();
        private bool isInter = false, mouseDown = false;
        private SqlDataAdapter da;
        private DataTable dt;
        private Point lastLocation;
        private string curVisible;
        private Home frmHome;

        public ClientList()
        {
            InitializeComponent();
        }

        private void ClientList_Load(object sender, EventArgs e)
        {
            frmHome = (Home)this.Owner;
            curVisible = frmHome.GetCurPanel();

            if (curVisible == "pnl_I_Orders" || curVisible == "pnl_I_Quotes" || curVisible == "pnl_I_InvSend")
                isInter = true;

            dgv_CL.DataSource = bs;
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                da = !isInter ? da = new SqlDataAdapter("SELECT * FROM Clients", conn) : da = new SqlDataAdapter("SELECT * FROM Int_Clients", conn);
                dt = new DataTable();
                da.Fill(dt);
            }
            bs.DataSource = dt;
        }
         
        private void DGV_CL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (curVisible == "pnl_L_Orders")
                ((OrdersOld)frmHome.GetCurForm()).setNewClient(e.RowIndex);

            if (curVisible == "pnl_L_Quotes")
                ((QuotesOld)frmHome.GetCurForm()).setNewClient(e.RowIndex);

            this.Close();
        }

        private void Btn_CL_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_CL_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_CL_Close.Image = Resources.close_white;
        }

        private void Btn_CL_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_CL_Close.Image = Resources.close_black;
        }

        private void DGV_CList_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_CL.FilterString;
        }

        private void DGV_CList_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_CL.SortString;
        }

        private void CL_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void CL_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                //Moves the form to a new location as long as user has mouse click down
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                //Updates the main form with the new position
                this.Update();
            }
        }

        private void CL_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
