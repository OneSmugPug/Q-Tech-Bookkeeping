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
    public partial class Inv_Send_Edit_Del : Form
    {
        private bool isInter = false;
        private bool mouseDown = false;
        private DataTable dt;
        private int SELECTED_INVOICE;
        private string oldINum;
        private Point lastLocation;
        

        public Inv_Send_Edit_Del()
        {
            InitializeComponent();
        }

        private void Inv_Send_Edit_Del_Load(object sender, EventArgs e)
        {
            Home frmHome = (Home)this.Owner;

            if (frmHome.GetCurPanel() == "pnl_L_InvSent")
            {
                Invoices_Send curForm = (Invoices_Send)frmHome.GetCurForm();

                dt = curForm.GetInvoices();

                txt_ISED_CCode.Text = curForm.GetCCode();
                txt_ISED_CName.Text = curForm.GetCName();

                SELECTED_INVOICE = curForm.GetSelectedInvSend();
            }
            else
            {
                isInter = true;
                Int_Invoices_Send curForm = (Int_Invoices_Send)frmHome.GetCurForm();

                dt = curForm.GetInvoices();

                txt_ISED_CCode.Text = curForm.GetCCode();
                txt_ISED_CName.Text = curForm.GetCName();

                SELECTED_INVOICE = curForm.GetSelectedInvSend();
            }

            LoadInvSend();

            if (txt_ISED_INInst.Text.Trim() != string.Empty)
                oldINum = txt_ISED_InvNum.Text.Trim() + "." + txt_ISED_INInst.Text.Trim();
            else oldINum = txt_ISED_InvNum.Text.Trim();
        }

        private void LoadInvSend()
        {
            if (dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim().Contains("."))
            {
                string[] strArray = dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim().Split('.');

                txt_ISED_InvNum.Text = strArray[0];
                txt_ISED_INInst.Text = strArray[1];
            }
            else txt_ISED_InvNum.Text = dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim();

            if (dt.Rows[SELECTED_INVOICE]["Date"].ToString() != string.Empty)
                dtp_ISED_Date.Value = Convert.ToDateTime(dt.Rows[SELECTED_INVOICE]["Date"].ToString());
            else dtp_ISED_Date.Value = DateTime.Now;

            txt_ISED_Desc.Text = dt.Rows[SELECTED_INVOICE]["Description"].ToString().Trim();

            if (!isInter)
            {
                if (dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString() != string.Empty)
                    txt_ISED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString().Trim()).ToString("C");
                else txt_ISED_Amt.Text = "R0.00";

                if (dt.Rows[SELECTED_INVOICE]["VAT"].ToString() != string.Empty)
                    txt_ISED_VAT.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["VAT"].ToString().Trim()).ToString("C");
                else txt_ISED_VAT.Text = "R0.00";
            }
            else
            {
                if (dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString() != string.Empty)
                    txt_ISED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString().Trim()).ToString("C", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                else txt_ISED_Amt.Text = "$0.00";

                if (dt.Rows[SELECTED_INVOICE]["VAT"].ToString() != string.Empty)
                    txt_ISED_VAT.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["VAT"].ToString().Trim()).ToString("C", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                else txt_ISED_VAT.Text = "$0.00";
            }

            if (dt.Rows[SELECTED_INVOICE]["Paid"].ToString() == "Yes")
            {
                cb_ISED_Paid.Checked = true;
                dtp_ISED_DatePaid.Enabled = true;
            }
            else cb_ISED_Paid.Checked = false;

            if (dt.Rows[SELECTED_INVOICE]["Date_Paid"].ToString() != string.Empty)
                dtp_ISED_DatePaid.Value = Convert.ToDateTime(dt.Rows[SELECTED_INVOICE]["Date_Paid"].ToString());
            else dtp_ISED_DatePaid.Value = DateTime.Now;
        }


        //================================================================================================================================================//
        // FORMAT MONEY TEXTBOX                                                                                                                           //
        //================================================================================================================================================//
        private void Txt_ISED_Amt_TextChanged(object sender, EventArgs e)
        {
            if (!isInter)
            {
                if (Decimal.TryParse(txt_ISED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISED_Amt.TextChanged -= Txt_ISED_Amt_TextChanged;

                    txt_ISED_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", ul);
                    txt_ISED_Amt.TextChanged += Txt_ISED_Amt_TextChanged;
                    txt_ISED_Amt.Select(txt_ISED_Amt.Text.Length, 0);
                }

                if (!TextisValid(txt_ISED_Amt.Text))
                {
                    txt_ISED_Amt.Text = "R0.00";
                    txt_ISED_Amt.Select(txt_ISED_Amt.Text.Length, 0);
                }
            }
            else
            {
                if (Decimal.TryParse(txt_ISED_Amt.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISED_Amt.TextChanged -= Txt_ISED_Amt_TextChanged;

                    txt_ISED_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                    txt_ISED_Amt.TextChanged += Txt_ISED_Amt_TextChanged;
                    txt_ISED_Amt.Select(txt_ISED_Amt.Text.Length, 0);
                }

                if (!TextisValid(txt_ISED_Amt.Text))
                {
                    txt_ISED_Amt.Text = "$0.00";
                    txt_ISED_Amt.Select(txt_ISED_Amt.Text.Length, 0);
                }
            }
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }

        private void Txt_ISED_VAT_TextChanged(object sender, EventArgs e)
        {
            if (!isInter)
            {
                if (Decimal.TryParse(txt_ISED_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISED_VAT.TextChanged -= Txt_ISED_VAT_TextChanged;

                    txt_ISED_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", ul);
                    txt_ISED_VAT.TextChanged += Txt_ISED_VAT_TextChanged;
                    txt_ISED_VAT.Select(txt_ISED_VAT.Text.Length, 0);
                }

                if (TextisValid(txt_ISED_VAT.Text))
                {
                    txt_ISED_VAT.Text = "R0.00";
                    txt_ISED_VAT.Select(txt_ISED_VAT.Text.Length, 0);
                }
            }
            else
            {
                if (Decimal.TryParse(txt_ISED_VAT.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISED_VAT.TextChanged -= Txt_ISED_VAT_TextChanged;

                    txt_ISED_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                    txt_ISED_VAT.TextChanged += Txt_ISED_VAT_TextChanged;
                    txt_ISED_VAT.Select(txt_ISED_VAT.Text.Length, 0);
                }

                if (!TextisValid(txt_ISED_VAT.Text))
                {
                    txt_ISED_VAT.Text = "$0.00";
                    txt_ISED_VAT.Select(txt_ISED_VAT.Text.Length, 0);
                }
            }
        }

        private void Btn_ISED_Done_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to update invoice?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Invoices_Send SET Invoice_Number = @INum, Date = @Date, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid, Date_Paid = @DPaid WHERE Invoice_Number = @oldINum", conn))
                    {
                        decimal amt;
                        decimal VAT;

                        if (!isInter)
                        {
                            if (txt_ISED_Amt.Text.Contains("R"))
                            {
                                if (txt_ISED_Amt.Text.Replace("R", string.Empty) == "0.00")
                                {
                                    amt = 0.00m;
                                }
                                else amt = Decimal.Parse(txt_ISED_Amt.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                            }
                            else amt = 0.00m;

                            if (txt_ISED_VAT.Text.Contains("R"))
                            {
                                if (txt_ISED_VAT.Text.Replace("R", string.Empty) == "0.00")
                                {
                                    VAT = 0.00m;
                                }
                                else VAT = Decimal.Parse(txt_ISED_VAT.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                            }
                            else VAT = 0.00m;
                        }
                        else
                        {
                            if (txt_ISED_Amt.Text.Contains("$"))
                            {
                                if (txt_ISED_Amt.Text.Replace("$", string.Empty) == "0.00")
                                {
                                    amt = 0.00m;
                                }
                                else amt = Decimal.Parse(txt_ISED_Amt.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                            }
                            else amt = 0.00m;

                            if (txt_ISED_VAT.Text.Contains("$"))
                            {
                                if (txt_ISED_VAT.Text.Replace("$", string.Empty) == "0.00")
                                {
                                    VAT = 0.00m;
                                }
                                else VAT = Decimal.Parse(txt_ISED_VAT.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                            }
                            else VAT = 0.00m;
                        }

                        cmd.Parameters.AddWithValue("@Date", dtp_ISED_Date.Value);
                        cmd.Parameters.AddWithValue("@Desc", txt_ISED_Desc.Text.Trim());
                        cmd.Parameters.AddWithValue("@Amt", amt);
                        cmd.Parameters.AddWithValue("@VAT", VAT);

                        if (cb_ISED_Paid.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Paid", "Yes");
                            cmd.Parameters.AddWithValue("@DPaid", dtp_ISED_DatePaid.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Paid", "No");
                            cmd.Parameters.AddWithValue("@DPaid", DBNull.Value);
                        }

                        if (txt_ISED_INInst.Text == string.Empty)
                            cmd.Parameters.AddWithValue("@INum", txt_ISED_InvNum.Text.Trim());
                        else cmd.Parameters.AddWithValue("@INum", txt_ISED_InvNum.Text.Trim() + "." + txt_ISED_INInst.Text.Trim());

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

        private void Btn_ISED_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_ISED_Amt_Leave(object sender, EventArgs e)
        {
            if (!isInter)
            {
                string value = txt_ISED_Amt.Text.Replace("R", string.Empty);

                decimal ul;

                if (decimal.TryParse(value, out ul))
                {
                    ul *= 0.15m;

                    txt_ISED_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", ul);
                }
            }
            else
            {
                string value = txt_ISED_Amt.Text.Replace("$", string.Empty);

                decimal ul = decimal.Parse(value, CultureInfo.GetCultureInfo("en-US"));

                ul *= 0.15m;

                txt_ISED_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
            }
        }

        private void Btn_ISED_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_ISED_InvNum_MouseEnter(object sender, EventArgs e)
        {
            ln_ISED_InvNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISED_InvNum_Leave(object sender, EventArgs e)
        {
            ln_ISED_InvNum.LineColor = Color.Gray;
        }

        private void Txt_ISED_InvNum_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISED_InvNum.Focused)
                ln_ISED_InvNum.LineColor = Color.Gray;
        }

        private void Txt_ISED_INInst_MouseEnter(object sender, EventArgs e)
        {
            ln_ISED_INInst.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISED_INInst_Leave(object sender, EventArgs e)
        {
            ln_ISED_INInst.LineColor = Color.Gray;
        }

        private void Txt_ISED_INInst_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISED_INInst.Focused)
                ln_ISED_INInst.LineColor = Color.Gray;
        }

        private void Txt_ISED_Desc_Leave(object sender, EventArgs e)
        {
            ln_ISED_Desc.LineColor = Color.Gray;
        }

        private void Txt_ISED_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_ISED_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISED_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISED_Desc.Focused)
                ln_ISED_Desc.LineColor = Color.Gray;
        }

        private void Txt_ISED_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_ISED_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISED_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISED_Amt.Focused)
                ln_ISED_Amt.LineColor = Color.Gray;
        }

        private void Txt_ISED_VAT_Leave(object sender, EventArgs e)
        {
            ln_ISED_VAT.LineColor = Color.Gray;
        }

        private void Txt_ISED_VAT_MouseEnter(object sender, EventArgs e)
        {
            ln_ISED_VAT.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISED_VAT_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISED_VAT.Focused)
                ln_ISED_VAT.LineColor = Color.Gray;
        }

        private void Btn_ISED_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_ISED_Close.Image = Resources.close_white;
        }

        private void Btn_ISED_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_ISED_Close.Image = Resources.close_black;
        }

        private void Btn_ISED_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_ISED_Done.ForeColor = Color.White;
        }

        private void Btn_ISED_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_ISED_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_ISED_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_ISED_Cancel.ForeColor = Color.White;
        }

        private void Btn_ISED_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_ISED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Txt_ISED_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_ISED_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Inv_Send_Edit_Del_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Inv_Send_Edit_Del_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Inv_Sent_Edit_Del_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Cb_ISED_Paid_OnChange(object sender, EventArgs e)
        {
            if (cb_ISED_Paid.Checked)
                dtp_ISED_DatePaid.Enabled = true;
            else dtp_ISED_DatePaid.Enabled = false;
        }
    }
}
