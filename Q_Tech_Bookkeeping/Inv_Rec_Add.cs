using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class Inv_Rec_Add : Form
    {
        private bool mouseDown = false;
        private StringBuilder sb;
        private Point lastLocation;

        public Inv_Rec_Add()
        {
            InitializeComponent();
        }

        private void Inv_Rec_Add_Load(object sender, EventArgs e)
        {
            txt_IRA_Amt.Text = "R0.00";
            txt_IRA_Amt.SelectionStart = txt_IRA_Amt.Text.Length;

            txt_IRA_VAT.Text = "R0.00";
            txt_IRA_VAT.SelectionStart = txt_IRA_VAT.Text.Length;

            dtp_IRA_Date.Value = DateTime.Now;
        }


        //================================================================================================================================================//
        // FORMAT MONEY TEXTBOX                                                                                                                           //
        //================================================================================================================================================//
        private void Txt_IRA_Amt_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_IRA_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= 100;

                txt_IRA_Amt.TextChanged -= Txt_IRA_Amt_TextChanged;

                txt_IRA_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                txt_IRA_Amt.TextChanged += Txt_IRA_Amt_TextChanged;
                txt_IRA_Amt.Select(txt_IRA_Amt.Text.Length, 0);
            }

            if (!TextisValid(txt_IRA_Amt.Text))
            {
                txt_IRA_Amt.Text = "R0.00";
                txt_IRA_Amt.Select(txt_IRA_Amt.Text.Length, 0);
            }
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }

        private void Txt_IRA_VAT_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_IRA_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= 100;

                txt_IRA_VAT.TextChanged -= Txt_IRA_VAT_TextChanged;
                txt_IRA_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                txt_IRA_VAT.TextChanged += Txt_IRA_VAT_TextChanged;
                txt_IRA_VAT.Select(txt_IRA_VAT.Text.Length, 0);
            }

            if (!TextisValid(txt_IRA_VAT.Text))
            {
                txt_IRA_VAT.Text = "R0.00";
                txt_IRA_VAT.Select(txt_IRA_VAT.Text.Length, 0);
            }
        }

        private void Txt_IRA_Amt_Leave(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_IRA_Amt.Text.Replace("R", string.Empty), out decimal result))
            {
                result *= 0.15m;
                txt_IRA_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
            }
        }


        //================================================================================================================================================//
        // BUTTON DONE CLICKED                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_IRA_Done_Click(object sender, EventArgs e)
        {
            string newINum = txt_IRA_InvNum.Text;

            sb = new StringBuilder().Append("Are you sure you want to add invoice with Invoice Number: ").Append(newINum).Append("?");

            if (newINum != string.Empty)
            {
                if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = DBUtils.GetDBConnection())
                    {
                        conn.Open();

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Invoices_Received VALUES (@Date, @InvNum, @Supp, @Desc, @Amt, @VAT, @Paid)", conn))
                            {
                                decimal amt;
                                if (txt_IRA_Amt.Text.Contains("R"))
                                {
                                    if (txt_IRA_Amt.Text.Replace("R", string.Empty) == "0.00")
                                        amt = 0.00m;
                                    else amt = Decimal.Parse(txt_IRA_Amt.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                }
                                else amt = 0.00m;

                                decimal VAT;
                                if (txt_IRA_VAT.Text.Contains("R"))
                                {
                                    if (txt_IRA_VAT.Text.Replace("R", string.Empty) == "0.00")
                                        VAT = 0.00m;
                                    else VAT = Decimal.Parse(txt_IRA_VAT.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                }
                                else VAT = 0.00m;

                                cmd.Parameters.AddWithValue("@Date", dtp_IRA_Date.Value);
                                cmd.Parameters.AddWithValue("@InvNum", txt_IRA_InvNum.Text.Trim());
                                cmd.Parameters.AddWithValue("@Supp", txt_IRA_SuppName.Text.Trim());
                                cmd.Parameters.AddWithValue("@Desc", txt_IRA_Desc.Text.Trim());
                                cmd.Parameters.AddWithValue("@Amt", amt);
                                cmd.Parameters.AddWithValue("@VAT", VAT);

                                if (cb_IRA_Paid.Checked)
                                    cmd.Parameters.AddWithValue("@Paid", "Yes");
                                else
                                    cmd.Parameters.AddWithValue("@Paid", "No");

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("New invoice successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            else MessageBox.Show("Please enter an Invoice Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        //================================================================================================================================================//
        // CANCEL CLICKED                                                                                                                            //
        //================================================================================================================================================//
        private void Btn_IRA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // INVOICE NUMBER                                                                                                                           //
        //================================================================================================================================================//
        private void Txt_IRA_InvNum_MouseEnter(object sender, EventArgs e)
        {
            ln_IRA_InvNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_InvNum_Leave(object sender, EventArgs e)
        {
            ln_IRA_InvNum.LineColor = Color.Gray;
        }

        private void Txt_IRA_InvNum_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRA_InvNum.Focused)
                ln_IRA_InvNum.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // SUPPLIER NAME                                                                                                                            //
        //================================================================================================================================================//
        private void Txt_IRA_SuppName_MouseEnter(object sender, EventArgs e)
        {
            ln_IRA_SuppName.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_SuppName_Leave(object sender, EventArgs e)
        {
            ln_IRA_SuppName.LineColor = Color.Gray;
        }

        private void Txt_IRA_SuppName_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRA_SuppName.Focused)
                ln_IRA_SuppName.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // DESCRIPTION                                                                                                                        //
        //================================================================================================================================================//
        private void Txt_IRA_Desc_Leave(object sender, EventArgs e)
        {
            ln_IRA_Desc.LineColor = Color.Gray;
        }

        private void Txt_IRA_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_IRA_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRA_Desc.Focused)
                ln_IRA_Desc.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // AMOUNT                                                                                                                            //
        //================================================================================================================================================//
        private void Txt_IRA_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_IRA_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRA_Amt.Focused)
                ln_IRA_Amt.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // VAT                                                                                                                           //
        //================================================================================================================================================//
        private void Txt_IRA_VAT_Leave(object sender, EventArgs e)
        {
            ln_IRA_VAT.LineColor = Color.Gray;
        }

        private void Txt_IRA_VAT_MouseEnter(object sender, EventArgs e)
        {
            ln_IRA_VAT.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_VAT_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRA_VAT.Focused)
                ln_IRA_VAT.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                        //
        //================================================================================================================================================//
        private void Btn_IRA_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_IRA_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_IRA_Close.Image = Resources.close_white;
        }

        private void Btn_IRA_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_IRA_Close.Image = Resources.close_black;
        }


        //================================================================================================================================================//
        // DONE BUTTON                                                                                                                          //
        //================================================================================================================================================//
        private void Btn_IRA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_IRA_Done.ForeColor = Color.White;
        }

        private void Btn_IRA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_IRA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                           //
        //================================================================================================================================================//
        private void Btn_IRA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_IRA_Cancel.ForeColor = Color.White;
        }

        private void Btn_IRA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_IRA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // INVOICE RECEIVE ADD                                                                                                                           //
        //================================================================================================================================================//
        private void Inv_Rec_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Inv_Rec_Add_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Inv_REc_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
