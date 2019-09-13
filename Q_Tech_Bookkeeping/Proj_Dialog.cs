using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class Proj_Dialog : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        public Proj_Dialog()
        {
            InitializeComponent();
        }

        private void Btn_PD_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_PD_EditProj_Click(object sender, EventArgs e)
        {
            new Proj_Edit_DelOld().Show((IWin32Window)Owner);
            this.Close();
        }

        private void Btn_PD_ManProject_Click(object sender, EventArgs e)
        {
            Home owner = (Home)Owner;
            Manage_Proj frmMP = new Manage_Proj();
            owner.SetManageProjects(frmMP, owner);
            this.Close();
        }

        private void Btn_PD_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_PD_Close.Image = Resources.close_white;
        }

        private void Btn_PD_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_PD_Close.Image = Resources.close_black;
        }

        private void Btn_PD_ManProject_MouseEnter(object sender, EventArgs e)
        {
            btn_PD_ManProject.ForeColor = Color.White;
        }

        private void Btn_PD_ManProject_MouseLeave(object sender, EventArgs e)
        {
            btn_PD_ManProject.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_PD_EditProj_MouseEnter(object sender, EventArgs e)
        {
            btn_PD_EditProj.ForeColor = Color.White;
        }

        private void Btn_PD_EditProj_MouseLeave(object sender, EventArgs e)
        {
            btn_PD_EditProj.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Proj_Dialog_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Proj_Dialog_MouseMove(object sender, MouseEventArgs e)
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

        private void Proj_Dialog_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
