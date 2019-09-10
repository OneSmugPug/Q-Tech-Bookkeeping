﻿using ADGV;
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
    public partial class Con_List : Form
    {
        private BindingSource bs = new BindingSource();
        private bool mouseDown = false;
        private IContainer components = (IContainer)null;
        private SqlDataAdapter da;
        private DataTable dt;
        private Point lastLocation;
        private Panel panel1;
        private AdvancedDataGridView dgv_SelCon;
        private BunifuCustomLabel bunifuCustomLabel4;
        private Button btn_SelCon_Close;

        public Con_List()
        {
            InitializeComponent();
        }

        private void Con_List_Load(object sender, EventArgs e)
        {
            dgv_SelCon.DataSource = bs;
            LoadCon();
        }

        private void LoadCon()
        {
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                da = new SqlDataAdapter("SELECT * FROM Contractors", dbConnection);
                dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.Rows[0];
            }
            this.bs.DataSource = dt;
        }

        private void Dgv_SelCon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ((ContractorsOld)((Home)this.Owner).GetCurForm()).setNewCon(e.RowIndex);
            this.Close();
        }

        private void Btn_SelCon_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_CL_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_SelCon_Close.Image = Resources.close_white;
        }

        private void Btn_CL_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_SelCon_Close.Image = Resources.close_black;
        }

        private void Dgv_SelCon_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgv_SelCon.SortString;
        }

        private void Dgv_SelCon_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgv_SelCon.FilterString;
        }

        private void CL_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void CL_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
                return;
            Point location = Location;
            int x = location.X - lastLocation.X + e.X;
            location = Location;
            int y = location.Y - lastLocation.Y + e.Y;
            Location = new Point(x, y);
            this.Update();
        }

        private void CL_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}