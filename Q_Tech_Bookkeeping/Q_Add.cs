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
    public partial class Q_Add : Form
    {
        private DataTable dt = (DataTable)null;
        private bool mouseDown = false;
        private Point lastLocation;

        public Q_Add()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // QUOTE ADD LOAD FORM                                                                                                                            //
        //================================================================================================================================================//
        private void Q_Add_Load(object sender, EventArgs e)
        {
            Home frmHome = (Home)this.Owner;

            if (frmHome.GetCurPanel() == "pnl_L_Quotes")
            {
                Quotes curForm = (Quotes)frmHome.GetCurForm();
                txt_QA_CCode.Text = curForm.GetCCode();
                txt_QA_CName.Text = curForm.GetCName();
                dt = curForm.GetQuotes();
            }
            else
            {
                Int_Quotes curForm = (Int_Quotes)frmHome.GetCurForm();
                txt_QA_CCode.Text = curForm.GetCCode();
                txt_QA_CName.Text = curForm.GetCName();
                dt = curForm.GetQuotes();
            }

            int qNum = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    string str = dr["Quote_Number", DataRowVersion.Original].ToString().Trim();
                    int pos = str.IndexOf("_");
                    int x = Convert.ToInt32(str.Remove(0, pos + 2));

                    if (x > qNum)
                        qNum = x;
                }
                else
                {
                    string str = dr["Quote_Number"].ToString().Trim();
                    int pos = str.IndexOf("_");
                    int x = Convert.ToInt32(str.Remove(0, pos + 2));
                    if (x > qNum)
                        qNum = x;
                }
            }
            txt_QA_QNum.Text = txt_QA_CCode.Text + "_Q" + (qNum + 1).ToString("000");
            txt_QA_Desc.Focus();
        }


        //================================================================================================================================================//
        // DONE CLICKED                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_QA_Done_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder().Append("Are you sure you want to add quote with Quote Number: ").Append(txt_QA_QNum.Text).Append("?");

            if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Quotes_Send VALUES (@QNum, @Date, @Client, @Desc, @OPlaced)", conn))
                        {
                            cmd.Parameters.AddWithValue("@QNum", txt_QA_QNum.Text.Trim());
                            cmd.Parameters.AddWithValue("@Date", dtp_QA_Date.Value);
                            cmd.Parameters.AddWithValue("@Client", txt_QA_CName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Desc", txt_QA_Desc.Text.Trim());

                            if (cb_QA_OrderPlaced.Checked)
                                cmd.Parameters.AddWithValue("@OPlaced", "Yes");
                            else cmd.Parameters.AddWithValue("@OPlaced", "No");

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("New quote successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
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
        private void Btn_QA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // ORDER NUMBER                                                                                                                                   //
        //================================================================================================================================================//
        private void Txt_QA_ONum_MouseEnter(object sender, EventArgs e)
        {
            ln_QA_CONum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_QA_ONum_Leave(object sender, EventArgs e)
        {
            ln_QA_CONum.LineColor = Color.Gray;
        }

        private void Txt_QA_ONum_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_QA_QNum.Focused)
                ln_QA_CONum.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // DESCRIPTION                                                                                                                                    //
        //================================================================================================================================================//
        private void Txt_QA_Desc_Leave(object sender, EventArgs e)
        {
            ln_QA_Desc.LineColor = Color.Gray;
        }

        private void Txt_QA_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_QA_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_QA_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_QA_Desc.Focused)
                ln_QA_Desc.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // CLOSE CLICKED                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_QA_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_QA_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_QA_Close.Image = Resources.close_white;
        }

        private void Btn_QA_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_QA_Close.Image = Resources.close_black;
        }


        //================================================================================================================================================//
        // DONE BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_QA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_QA_Done.ForeColor = Color.White;
        }

        private void Btn_QA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_QA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_QA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_QA_Cancel.ForeColor = Color.White;
        }

        private void Btn_QA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_QA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ENFORCE READONLY                                                                                                                               //
        //================================================================================================================================================//
        private void Txt_QA_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_QA_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }


        //================================================================================================================================================//
        // QUOTE ADD                                                                                                                                      //
        //================================================================================================================================================//
        private void Q_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Q_Add_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(this.Location.X - (lastLocation.X + e.X), this.Location.Y - (lastLocation.Y + e.Y));
                this.Update();
            }
        }

        private void Q_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
