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
    public partial class Q_Edit_Del : Form
    {
        private bool mouseDown = false;
        private DataTable dt;
        private int SELECTED_QUOTE;
        private Point lastLocation;

        public Q_Edit_Del()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // QUOTE EDIT DELETE FORM                                                                                                                         //
        //================================================================================================================================================//
        private void Q_Edit_Del_Load(object sender, EventArgs e)
        {
            Home frmHome = (Home)this.Owner;

            if (frmHome.GetCurPanel() == "pnl_L_Quotes")
            {
                Quotes curForm = (Quotes)frmHome.GetCurForm();
                dt = curForm.GetQuotes();
                SELECTED_QUOTE = curForm.GetSelectedQuote();
                txt_QED_CCode.Text = curForm.GetCCode();
                txt_QED_CName.Text = curForm.GetCName();
            }
            else
            {
                Int_Quotes curForm = (Int_Quotes)frmHome.GetCurForm();
                dt = curForm.GetQuotes();
                SELECTED_QUOTE = curForm.GetSelectedQuote();
                txt_QED_CCode.Text = curForm.GetCCode();
                txt_QED_CName.Text = curForm.GetCName();
            }
            LoadQuote();
        }


        //================================================================================================================================================//
        // LOAD QUOTE DETAILS                                                                                                                             //
        //================================================================================================================================================//
        private void LoadQuote()
        {
            txt_QED_QNum.Text = dt.Rows[SELECTED_QUOTE]["Quote_Number"].ToString().Trim();

            dtp_QED_Date.Value = (dt.Rows[SELECTED_QUOTE]["Date_Send"].ToString() == string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[SELECTED_QUOTE]["Date_Send"].ToString());

            txt_QED_Desc.Text = dt.Rows[SELECTED_QUOTE]["Quote_Description"].ToString().Trim();

            if (dt.Rows[SELECTED_QUOTE]["Order_Placed"].ToString() == "Yes")
                cb_QED_OrderPlaced.Checked = true;
            else cb_QED_OrderPlaced.Checked = false;
        }


        //================================================================================================================================================//
        // DONE CLICKED                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_QED_Done_Click(object sender, EventArgs e)
        {
            string qNum = txt_QED_QNum.Text;

            if (MessageBox.Show("Are you sure you want to update quote?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("UPDATE Quotes_Send SET Date_Send = @Date, Quote_Description = @Desc, Order_Placed = @OPlaced WHERE Quote_Number = @QNum", conn))
                        {
                            sqlCommand.Parameters.AddWithValue("@Date", dtp_QED_Date.Value);
                            sqlCommand.Parameters.AddWithValue("@Desc", txt_QED_Desc.Text.Trim());

                            if (cb_QED_OrderPlaced.Checked)
                                sqlCommand.Parameters.AddWithValue("@OPlaced", "Yes");
                            else sqlCommand.Parameters.AddWithValue("@OPlaced", "No");

                            sqlCommand.Parameters.AddWithValue("@QNum", qNum);
                            sqlCommand.ExecuteNonQuery();

                            MessageBox.Show("Quote successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        // CLOSE CLICKED                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_QED_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // CANCEL CLICKED                                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_QED_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // ORDER NUMBER                                                                                                                                   //
        //================================================================================================================================================//
        private void Txt_QED_ONum_MouseEnter(object sender, EventArgs e)
        {
            ln_QED_CONum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_QED_ONum_Leave(object sender, EventArgs e)
        {
            ln_QED_CONum.LineColor = Color.Gray;
        }

        private void Txt_QED_ONum_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_QED_QNum.Focused)
                ln_QED_CONum.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // DESCRIPTION                                                                                                                                    //
        //================================================================================================================================================//
        private void Txt_QED_Desc_Leave(object sender, EventArgs e)
        {
            ln_QED_Desc.LineColor = Color.Gray;
        }

        private void Txt_QED_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_QED_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_QED_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_QED_Desc.Focused)
                ln_QED_Desc.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_QED_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_QED_Close.Image = Resources.close_white;
        }

        private void Btn_QED_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_QED_Close.Image = Resources.close_black;
        }


        //================================================================================================================================================//
        // DONE BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_QED_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_QED_Done.ForeColor = Color.White;
        }

        private void Btn_QED_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_QED_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_QED_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_QED_Cancel.ForeColor = Color.White;
        }

        private void Btn_QED_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_QED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ENFORCE READ ONLY                                                                                                                              //
        //================================================================================================================================================//
        private void Txt_QED_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_QED_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }


        //================================================================================================================================================//
        // QUOTE EDIT DELETE                                                                                                                              //
        //================================================================================================================================================//
        private void Q_Edit_Del_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Q_Edit_Del_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Q_Edit_Del_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
