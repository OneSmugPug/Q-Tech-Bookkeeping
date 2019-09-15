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
        private DataTable dt;
        private DataTable projDT;
        private Point lastLocation;

        public Proj_Add()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // PROJECT ADD FORM LOAD                                                                                                                      //
        //================================================================================================================================================//
        private void Proj_Add_Load(object sender, EventArgs e)
        {
            dtp_PA_Date.Value = DateTime.Now;
            LoadClients();
        }


        //================================================================================================================================================//
        // LOAD CLIENT DETAILS                                                                                                                            //
        //================================================================================================================================================//
        private void LoadClients()
        {
            dt = new DataTable();

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter lClientDA = new SqlDataAdapter("SELECT * FROM Clients", conn);
                SqlDataAdapter iClientDA = new SqlDataAdapter("SELECT * FROM Int_Clients", conn);

                lClientDA.Fill(dt);
                iClientDA.Fill(dt);
            }

            foreach (DataRow dr in dt.Rows)
                ddb_PA_CCode.AddItem(dr["Code"].ToString().Trim());

            ddb_PA_CCode.selectedIndex = 0;
        }


        //================================================================================================================================================//
        // CLIENT CODE SELECT                                                                                                                             //
        //================================================================================================================================================//
        private void Ddb_PA_CCode_onItemSelected(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Code"].ToString().Trim().Equals(ddb_PA_CCode.selectedValue))
                    txt_PA_CName.Text = dr["Name"].ToString().Trim();
            }

            projDT = ((Projects)((Home)this.Owner).GetCurForm()).GetProjects();

            int CCode = 0;
            foreach (DataRow row in projDT.Rows)
            {
                string[] strArray = row["Project_ID"].ToString().Trim().Split('_');

                int x = 0;

                if (strArray[1].Equals(ddb_PA_CCode.selectedValue))
                    x = Convert.ToInt32(strArray[0].Remove(0, 1));

                if (x > CCode)
                    CCode = x;
            }

            txt_PA_ProjCode.Text = "P" + (CCode + 1).ToString("000") + "_" + ddb_PA_CCode.selectedValue;

            DataTable dataTable;
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Quotes_Send WHERE Client = '" + txt_PA_CName.Text.Trim() + "'", conn);
                dataTable = new DataTable();
                da.Fill(dataTable);
            }

            int QNum = 1;
            foreach (DataRow dr in dataTable.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    string str = dr["Quote_Number", DataRowVersion.Original].ToString().Trim();
                    int pos = str.IndexOf("_");
                    int x = Convert.ToInt32(str.Remove(0, pos + 2));
                    if (x > QNum)
                        QNum = x;
                }
                else
                {
                    string str = dr["Quote_Number"].ToString().Trim();
                    int pos = str.IndexOf("_");
                    int x = Convert.ToInt32(str.Remove(0, pos + 2));
                    if (x > QNum)
                        QNum = x;
                }
            }
            txt_PA_QNum.Text = ddb_PA_CCode.selectedValue + "_Q" + (QNum + 1).ToString("000");
        }


        //================================================================================================================================================//
        // DONE CLICKED                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_PA_Done_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder().Append("Are you sure you want to add project with project code: ").Append(txt_PA_ProjCode.Text).Append("?");

            if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Projects VALUES (@ProjID, @Date, @ClientCode, @ClientName, @Desc, @QNum)", conn))
                        {
                            cmd.Parameters.AddWithValue("@ProjID", txt_PA_ProjCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@Date", dtp_PA_Date.Value);
                            cmd.Parameters.AddWithValue("@ClientCode", ddb_PA_CCode.selectedValue.Trim());
                            cmd.Parameters.AddWithValue("@ClientName", txt_PA_CName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Desc", txt_PA_Desc.Text.Trim());
                            cmd.Parameters.AddWithValue("@QNum", txt_PA_QNum.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Quotes_Send(Quote_Number, Client) VALUES (@QNum, @Client)", conn))
                        {
                            cmd.Parameters.AddWithValue("@QNum", txt_PA_QNum.Text.Trim());
                            cmd.Parameters.AddWithValue("@Client", txt_PA_CName.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("New project successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }           
        }


        //================================================================================================================================================//
        // CANCEL CLICKED                                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_PA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // PROJECT CODE                                                                                                                                   //
        //================================================================================================================================================//
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
            if (!txt_PA_ProjCode.Focused)
                ln_PA_ProjCode.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // DESCRIPTION                                                                                                                                    //
        //================================================================================================================================================//
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
            if (!txt_PA_Desc.Focused)
                ln_PA_Desc.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // QUOTE NUMBER                                                                                                                                   //
        //================================================================================================================================================//
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
            if (!txt_PA_QNum.Focused)
                ln_PA_QNum.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                                   //
        //================================================================================================================================================//
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


        //================================================================================================================================================//
        // DONE BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_PA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_PA_Done.ForeColor = Color.White;
        }

        private void Btn_PA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_PA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_PA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_PA_Cancel.ForeColor = Color.White;
        }

        private void Btn_PA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_PA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ENFORCE READONLY                                                                                                                               //
        //================================================================================================================================================//
        private void Ddb_PA_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_PA_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }


        //================================================================================================================================================//
        // PROJECT ADD                                                                                                                                    //
        //================================================================================================================================================//
        private void Proj_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Proj_Add_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Proj_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
