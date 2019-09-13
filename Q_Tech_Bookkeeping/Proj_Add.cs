using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class Proj_Add : Form
    {
        private bool mouseDown = false;
        private IContainer components = (IContainer)null;
        private DataTable dt;
        private DataTable projDT;
        private Point lastLocation;

        public Proj_Add()
        {
            InitializeComponent();
        }

        private void Proj_Add_Load(object sender, EventArgs e)
        {
            dtp_PA_Date.Value = DateTime.Now;
            LoadClients();
        }

        private void LoadClients()
        {
            dt = new DataTable();
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter("SELECT * FROM Clients", dbConnection);
                SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("SELECT * FROM Int_Clients", dbConnection);
                sqlDataAdapter1.Fill(dt);
                sqlDataAdapter2.Fill(dt);
            }
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
                ddb_PA_CCode.AddItem(row["Code"].ToString().Trim());
            ddb_PA_CCode.selectedIndex = 0;
        }

        private void Ddb_PA_CCode_onItemSelected(object sender, EventArgs e)
        {
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                if (row["Code"].ToString().Trim().Equals(ddb_PA_CCode.selectedValue))
                    txt_PA_CName.Text = row["Name"].ToString().Trim();
            }
            projDT = ((Projects)((Home)Owner).GetCurForm()).GetProjects();
            int num1 = 0;
            foreach (DataRow row in (InternalDataCollectionBase)projDT.Rows)
            {
                string[] strArray = row["Project_ID"].ToString().Trim().Split('_');
                int num2 = 0;
                if (strArray[1].Equals(ddb_PA_CCode.selectedValue))
                    num2 = Convert.ToInt32(strArray[0].Remove(0, 1));
                if (num2 > num1)
                    num1 = num2;
            }
            txt_PA_ProjCode.Text = "P" + (num1 + 1).ToString("000") + "_" + ddb_PA_CCode.selectedValue;
            DataTable dataTable;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Quotes_Send WHERE Client = '" + txt_PA_CName.Text.Trim() + "'", dbConnection);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
            }
            int num3 = 1;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    string str = row["Quote_Number", DataRowVersion.Original].ToString().Trim();
                    int num2 = str.IndexOf("_");
                    int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
                    if (int32 > num3)
                        num3 = int32;
                }
                else
                {
                    string str = row["Quote_Number"].ToString().Trim();
                    int num2 = str.IndexOf("_");
                    int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
                    if (int32 > num3)
                        num3 = int32;
                }
            }
            txt_PA_QNum.Text = ddb_PA_CCode.selectedValue + "_Q" + (num3 + 1).ToString("000");
        }

        private void Btn_PA_Done_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add project with project code: ").Append(txt_PA_ProjCode.Text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Projects VALUES (@ProjID, @Date, @ClientCode, @ClientName, @Desc, @QNum)", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@ProjID", txt_PA_ProjCode.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Date", dtp_PA_Date.Value);
                        sqlCommand.Parameters.AddWithValue("@ClientCode", ddb_PA_CCode.selectedValue.Trim());
                        sqlCommand.Parameters.AddWithValue("@ClientName", txt_PA_CName.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Desc", txt_PA_Desc.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@QNum", txt_PA_QNum.Text.Trim());
                        sqlCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Quotes_Send(Quote_Number, Client) VALUES (@QNum, @Client)", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@QNum", txt_PA_QNum.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Client", txt_PA_CName.Text.Trim());
                        sqlCommand.ExecuteNonQuery();
                        int num = (int)MessageBox.Show("New project successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Btn_PA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_PA_ProjCode_MouseEnter(object sender, EventArgs e)
        {
            ln_PA_ProjCode.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_PA_ProjCode_Leave(object sender, EventArgs e)
        {
            ln_PA_ProjCode.LineColor = Color.Gray;
        }

        private void Txt_PA_ProjCode_MouseLeave(object sender, EventArgs e)
        {
            if (txt_PA_ProjCode.Focused)
                return;
            ln_PA_ProjCode.LineColor = Color.Gray;
        }

        private void Txt_PA_Desc_Leave(object sender, EventArgs e)
        {
            ln_PA_Desc.LineColor = Color.Gray;
        }

        private void Txt_PA_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_PA_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_PA_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (txt_PA_Desc.Focused)
                return;
            ln_PA_Desc.LineColor = Color.Gray;
        }

        private void Txt_PA_QNum_Leave(object sender, EventArgs e)
        {
            ln_PA_QNum.LineColor = Color.Gray;
        }

        private void Txt_PA_QNum_MouseEnter(object sender, EventArgs e)
        {
            ln_PA_QNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_PA_QNum_MouseLeave(object sender, EventArgs e)
        {
            if (txt_PA_QNum.Focused)
                return;
            ln_PA_QNum.LineColor = Color.Gray;
        }

        private void Btn_PA_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_PA_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_PA_Close.Image = Resources.close_white;
        }

        private void Btn_PA_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_PA_Close.Image = Resources.close_black;
        }

        private void Btn_PA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_PA_Done.ForeColor = Color.White;
        }

        private void Btn_PA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_PA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_PA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_PA_Cancel.ForeColor = Color.White;
        }

        private void Btn_PA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_PA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Ddb_PA_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_PA_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Proj_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Proj_Add_MouseMove(object sender, MouseEventArgs e)
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

        private void Proj_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
