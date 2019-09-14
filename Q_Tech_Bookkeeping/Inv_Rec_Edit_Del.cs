using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class Inv_Rec_Edit_Del : Form
    {
        private bool mouseDown = false;
        private DataTable dt;
        private int SELECTED_INVOICE;
        private string oldINum;
        private Point lastLocation;

        public Inv_Rec_Edit_Del()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // LOAD EDIT/DELETE FORM                                                                                                                          //
        //================================================================================================================================================//
        private void Inv_Rec_Edit_Del_Load(object sender, EventArgs e)
        {
            txt_IRED_SuppName.Focus();

            Inv_Rec curForm = (Inv_Rec)((Home)this.Owner).GetCurForm();
            dt = curForm.GetInvRec();
            SELECTED_INVOICE = curForm.GetSelectedInv();

            LoadInvRec();

            oldINum = txt_IRED_InvNum.Text.Trim();
        }

        private void LoadInvRec()
        {
            txt_IRED_SuppName.Text = dt.Rows[SELECTED_INVOICE]["Supplier"].ToString().Trim();
            txt_IRED_InvNum.Text = dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim();

            dtp_IRED_Date.Value = (dt.Rows[SELECTED_INVOICE]["Date"].ToString() == string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[SELECTED_INVOICE]["Date"].ToString());

            txt_IRED_Desc.Text = dt.Rows[SELECTED_INVOICE]["Description"].ToString().Trim();

            if (dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString() != string.Empty)
                txt_IRED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString().Trim()).ToString("C");
            else txt_IRED_Amt.Text = "R0.00";

            if (dt.Rows[SELECTED_INVOICE]["VAT"].ToString() != string.Empty)
                txt_IRED_VAT.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["VAT"].ToString().Trim()).ToString("C");
            else txt_IRED_VAT.Text = "R0.00";

            if (dt.Rows[SELECTED_INVOICE]["Paid"].ToString() == "Yes")
                cb_IRED_Paid.Checked = true;
            else cb_IRED_Paid.Checked = false;
        }


        //================================================================================================================================================//
        // FORMAT MONEY TEXTBOX                                                                                                                           //
        //================================================================================================================================================//
        private void Txt_IRED_Amt_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_IRED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= 100;

                txt_IRED_Amt.TextChanged -= Txt_IRED_Amt_TextChanged;
                txt_IRED_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                txt_IRED_Amt.TextChanged += Txt_IRED_Amt_TextChanged;
                txt_IRED_Amt.Select(txt_IRED_Amt.Text.Length, 0);
            }

            if (!TextisValid(txt_IRED_Amt.Text))
            {
                txt_IRED_Amt.Text = "R0.00";
                txt_IRED_Amt.Select(txt_IRED_Amt.Text.Length, 0);
            }
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }

        private void Txt_IRED_VAT_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_IRED_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= 100;

                txt_IRED_VAT.TextChanged -= Txt_IRED_VAT_TextChanged;
                txt_IRED_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                txt_IRED_VAT.TextChanged += Txt_IRED_VAT_TextChanged;
                txt_IRED_VAT.Select(txt_IRED_VAT.Text.Length, 0);
            }

            if (!TextisValid(txt_IRED_VAT.Text))
            {
                txt_IRED_VAT.Text = "R0.00";
                txt_IRED_VAT.Select(txt_IRED_VAT.Text.Length, 0);
            }
        }


        //================================================================================================================================================//
        // EDIT INVOICE DETAILS                                                                                                                           //
        //================================================================================================================================================//
        private void Btn_IRED_Done_Click(object sender, EventArgs e)
        {
            if (txt_IRED_InvNum.Text != string.Empty)
            {
                if (MessageBox.Show("Are you sure you want to update invoice?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (txt_IRED_InvNum.Text == oldINum)
                    {
                        using (SqlConnection conn = DBUtils.GetDBConnection())
                        {
                            conn.Open();
                            try
                            {
                                using (SqlCommand cmd = new SqlCommand("UPDATE Invoices_Received SET Date = @Date, Supplier = @Supp, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid WHERE Invoice_Number = @INum", conn))
                                {
                                    decimal amt;
                                    if (txt_IRED_Amt.Text.Contains("R"))
                                    {
                                        if (txt_IRED_Amt.Text.Replace("R", string.Empty) == "0.00")
                                            amt = 0.00m;
                                        else amt = Decimal.Parse(txt_IRED_Amt.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                    } else amt = 0.00m;

                                    decimal VAT;
                                    if (txt_IRED_VAT.Text.Contains("R"))
                                    {
                                        if (txt_IRED_VAT.Text.Replace("R", string.Empty) == "0.00")
                                            VAT = 0.00m;
                                        else VAT = Decimal.Parse(txt_IRED_VAT.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                    } else VAT = 0.00m;

                                    cmd.Parameters.AddWithValue("@Date", dtp_IRED_Date.Value);
                                    cmd.Parameters.AddWithValue("@Supp", txt_IRED_SuppName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Desc", txt_IRED_Desc.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Amt", amt);
                                    cmd.Parameters.AddWithValue("@VAT", VAT);

                                    if (cb_IRED_Paid.Checked)
                                        cmd.Parameters.AddWithValue("@Paid", "Yes");
                                    else cmd.Parameters.AddWithValue("@Paid", "No");

                                    cmd.Parameters.AddWithValue("@INum", txt_IRED_InvNum.Text.Trim());

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Invoice successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (txt_IRED_InvNum.Text != oldINum)
                    {
                        using (SqlConnection conn = DBUtils.GetDBConnection())
                        {
                            conn.Open();
                            try
                            {
                                using (SqlCommand cmd = new SqlCommand("UPDATE Invoices_Received SET Date = @Date, Invoice_Number = @oldINum, Supplier = @Supp, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid WHERE Invoice_Number = @INum", conn))
                                {
                                    decimal amt;
                                    if (txt_IRED_Amt.Text.Contains("R"))
                                    {
                                        if (txt_IRED_Amt.Text.Replace("R", string.Empty) == "0.00")
                                            amt = 0.00m;
                                        else amt = Decimal.Parse(txt_IRED_Amt.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                    } else amt = 0.00m;

                                    decimal VAT;
                                    if (txt_IRED_VAT.Text.Contains("R"))
                                    {
                                        if (txt_IRED_VAT.Text.Replace("R", string.Empty) == "0.00")
                                        {
                                            VAT = 0.00m;
                                        } else VAT = Decimal.Parse(txt_IRED_VAT.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                    } else VAT = 0.00m;

                                    cmd.Parameters.AddWithValue("@Date", dtp_IRED_Date.Value);
                                    cmd.Parameters.AddWithValue("@oldINum", txt_IRED_InvNum.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Supp", txt_IRED_SuppName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Desc", txt_IRED_Desc.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Amt", amt);
                                    cmd.Parameters.AddWithValue("@VAT", VAT);

                                    if (cb_IRED_Paid.Checked)
                                        cmd.Parameters.AddWithValue("@Paid", "Yes");
                                    else cmd.Parameters.AddWithValue("@Paid", "No");

                                    cmd.Parameters.AddWithValue("@INum", oldINum);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Invoice successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            }
            else
            {
                MessageBox.Show("Please enter an Invoice Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Btn_IRED_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // CALCULATE VAT                                                                                                                                  //
        //================================================================================================================================================//
        private void Txt_IRED_Amt_Leave(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_IRED_Amt.Text.Replace("R", string.Empty), out decimal result))
            {
                result *= 0.15m;
                txt_IRED_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
            }
        }


        //================================================================================================================================================//
        // INVOICE NUMBER                                                                                                                         //
        //================================================================================================================================================//
        private void Txt_IRED_InvNum_MouseEnter(object sender, EventArgs e)
        {
            ln_IRED_InvNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_InvNum_Leave(object sender, EventArgs e)
        {
            ln_IRED_InvNum.LineColor = Color.Gray;
        }

        private void Txt_IRA_InvNum_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRED_InvNum.Focused)
                ln_IRED_InvNum.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // SUPPLIER NAME                                                                                                                       //
        //================================================================================================================================================//
        private void Txt_IRED_SuppName_MouseEnter(object sender, EventArgs e)
        {
            ln_IRED_SuppName.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_SuppName_Leave(object sender, EventArgs e)
        {
            ln_IRED_SuppName.LineColor = Color.Gray;
        }

        private void Txt_IRED_SuppName_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRED_SuppName.Focused)
                ln_IRED_SuppName.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // DESCRIPTION                                                                                                                       //
        //================================================================================================================================================//
        private void Txt_IRED_Desc_Leave(object sender, EventArgs e)
        {
            ln_IRED_Desc.LineColor = Color.Gray;
        }

        private void Txt_IRED_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_IRED_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRED_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRED_Desc.Focused)
                ln_IRED_Desc.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // AMOUNT                                                                                                                         //
        //================================================================================================================================================//
        private void Txt_IRED_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_IRED_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRED_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRED_Amt.Focused)
                ln_IRED_Amt.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // VAT                                                                                                                        //
        //================================================================================================================================================//
        private void Txt_IRED_VAT_Leave(object sender, EventArgs e)
        {
            ln_IRED_VAT.LineColor = Color.Gray;
        }

        private void Txt_IRED_VAT_MouseEnter(object sender, EventArgs e)
        {
            ln_IRED_VAT.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRED_VAT_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_IRED_VAT.Focused)
                ln_IRED_VAT.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                       //
        //================================================================================================================================================//
        private void Btn_IRED_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_IRED_Close.Image = Resources.close_white;
        }

        private void Btn_IRED_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_IRED_Close.Image = Resources.close_black;
        }


        //================================================================================================================================================//
        // DONE BUTTON                                                                                                                        //
        //================================================================================================================================================//
        private void Btn_IRED_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_IRED_Done.ForeColor = Color.White;
        }

        private void Btn_IRED_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_IRED_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                       //
        //================================================================================================================================================//
        private void Btn_IRED_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_IRED_Cancel.ForeColor = Color.White;
        }

        private void Btn_IRED_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_IRED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // INVOICE RECEIVE EDIT DELETE                                                                                                                        //
        //================================================================================================================================================//
        private void Inv_Rec_Edit_Del_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Inv_Rec_Edit_Del_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(this.Location.X - (lastLocation.X + e.X), this.Location.Y - (lastLocation.Y + e.Y));
                this.Update();
            }
        }

        private void Inv_Rec_Edit_Del_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }


        //================================================================================================================================================//
        // INVOICE RECEIVE EDIT DELETE                                                                                                                        //
        //================================================================================================================================================//
        private void Btn_IRED_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
