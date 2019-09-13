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
    public partial class Proj_Edit_Del : Form
    {
        private bool mouseDown = false;
        private IContainer components = (IContainer)null;
        private DataTable dt;
        private static int SELECTED_PROJECT;
        private Point lastLocation;

        public Proj_Edit_Del()
        {
            InitializeComponent();
        }

        private void Proj_Edit_Del_Load(object sender, EventArgs e)
        {
            Projects curForm = (Projects)((Home)Owner).GetCurForm();
            dt = curForm.GetProjects();
            Proj_Edit_DelOld.SELECTED_PROJECT = curForm.GetSelectedProj();
            LoadProject();
        }

        private void LoadProject()
        {
            txt_PED_CCode.Text = dt.Rows[Proj_Edit_DelOld.SELECTED_PROJECT]["Client_Code"].ToString().Trim();
            txt_PED_CName.Text = dt.Rows[Proj_Edit_DelOld.SELECTED_PROJECT]["Client_Name"].ToString().Trim();
            txt_PED_ProjCode.Text = dt.Rows[Proj_Edit_DelOld.SELECTED_PROJECT]["Project_ID"].ToString().Trim();
            dtp_PED_Date.Value = !(dt.Rows[Proj_Edit_DelOld.SELECTED_PROJECT]["Date"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[Proj_Edit_DelOld.SELECTED_PROJECT]["Date"].ToString());
            txt_PED_Desc.Text = dt.Rows[Proj_Edit_DelOld.SELECTED_PROJECT]["Description"].ToString().Trim();
            txt_PED_QNum.Text = dt.Rows[Proj_Edit_DelOld.SELECTED_PROJECT]["Quote_Number"].ToString().Trim();
        }

        private void Btn_PED_Done_Click(object sender, EventArgs e)
        {
            string text = this.txt_PED_ProjCode.Text;
            if (MessageBox.Show("Are you sure you want to update project?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UPDATE Projects SET Date = @Date, Description = @Desc WHERE Project_ID = @ProjID", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Date", (object)this.dtp_PED_Date.Value);
                        sqlCommand.Parameters.AddWithValue("@Desc", (object)this.txt_PED_Desc.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@ProjID", (object)text);
                        sqlCommand.ExecuteNonQuery();
                        int num = (int)MessageBox.Show("Project successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Btn_PED_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_PED_ProjCode_MouseEnter(object sender, EventArgs e)
        {
            this.ln_PED_ProjCode.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_PED_ProjCode_Leave(object sender, EventArgs e)
        {
            this.ln_PED_ProjCode.LineColor = Color.Gray;
        }

        private void Txt_PED_ProjCode_MouseLeave(object sender, EventArgs e)
        {
            if (this.txt_PED_ProjCode.Focused)
                return;
            this.ln_PED_ProjCode.LineColor = Color.Gray;
        }

        private void Txt_PED_Desc_Leave(object sender, EventArgs e)
        {
            this.ln_PED_Desc.LineColor = Color.Gray;
        }

        private void Txt_PED_Desc_MouseEnter(object sender, EventArgs e)
        {
            this.ln_PED_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_PED_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (this.txt_PED_Desc.Focused)
                return;
            this.ln_PED_Desc.LineColor = Color.Gray;
        }

        private void Txt_PED_QNum_Leave(object sender, EventArgs e)
        {
            this.ln_PED_QNum.LineColor = Color.Gray;
        }

        private void Txt_PED_QNum_MouseEnter(object sender, EventArgs e)
        {
            this.ln_PED_QNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_PED_QNum_MouseLeave(object sender, EventArgs e)
        {
            if (this.txt_PED_QNum.Focused)
                return;
            this.ln_PED_QNum.LineColor = Color.Gray;
        }

        private void Btn_PED_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_PED_Close_MouseEnter(object sender, EventArgs e)
        {
            this.btn_PED_Close.Image = (Image)Resources.close_white;
        }

        private void Btn_PED_Close_MouseLeave(object sender, EventArgs e)
        {
            this.btn_PED_Close.Image = (Image)Resources.close_black;
        }

        private void Btn_PED_Done_MouseEnter(object sender, EventArgs e)
        {
            this.btn_PED_Done.ForeColor = Color.White;
        }

        private void Btn_PED_Done_MouseLeave(object sender, EventArgs e)
        {
            this.btn_PED_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_PED_Cancel_MouseEnter(object sender, EventArgs e)
        {
            this.btn_PED_Cancel.ForeColor = Color.White;
        }

        private void Btn_PED_Cancel_MouseLeave(object sender, EventArgs e)
        {
            this.btn_PED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Ddb_PED_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_PED_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Proj_Edit_Del_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDown = true;
            this.lastLocation = e.Location;
        }

        private void Proj_Edit_Del_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseDown)
                return;
            Point location = this.Location;
            int x = location.X - this.lastLocation.X + e.X;
            location = this.Location;
            int y = location.Y - this.lastLocation.Y + e.Y;
            this.Location = new Point(x, y);
            this.Update();
        }

        private void Proj_Edit_Del_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;
        }
    }
}
