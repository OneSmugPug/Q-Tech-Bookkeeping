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
        private IContainer components = (IContainer)null;
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
            Home owner = (Home)Owner;
            if (owner.GetCurPanel() == "pnl_L_InvSent")
            {
                Invoices_Send curForm = (Invoices_Send)owner.GetCurForm();
                dt = curForm.GetInvoices();
                txt_ISED_CCode.Text = curForm.GetCCode();
                txt_ISED_CName.Text = curForm.GetCName();
                SELECTED_INVOICE = curForm.GetSelectedInvSend();
            }
            else
            {
                isInter = true;
                Int_Invoices_Send curForm = (Int_Invoices_Send)owner.GetCurForm();
                dt = curForm.GetInvoices();
                txt_ISED_CCode.Text = curForm.GetCCode();
                txt_ISED_CName.Text = curForm.GetCName();
                SELECTED_INVOICE = curForm.GetSelectedInvSend();
            }
            LoadInvSend();
            if (txt_ISED_INInst.Text.Trim() != string.Empty)
                oldINum = txt_ISED_InvNum.Text.Trim() + "." + txt_ISED_INInst.Text.Trim();
            else
                oldINum = txt_ISED_InvNum.Text.Trim();
        }

        private void LoadInvSend()
        {
            if (dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim().Contains("."))
            {
                string[] strArray = dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim().Split('.');
                txt_ISED_InvNum.Text = strArray[0];
                txt_ISED_INInst.Text = strArray[1];
            }
            else
                txt_ISED_InvNum.Text = dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim();
            dtp_ISED_Date.Value = !(dt.Rows[SELECTED_INVOICE]["Date"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[SELECTED_INVOICE]["Date"].ToString());
            txt_ISED_Desc.Text = dt.Rows[SELECTED_INVOICE]["Description"].ToString().Trim();
            if (!isInter)
            {
                if (dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString() != string.Empty)
                    txt_ISED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString().Trim()).ToString("C");
                else
                    txt_ISED_Amt.Text = "R0.00";
                if (dt.Rows[SELECTED_INVOICE]["VAT"].ToString() != string.Empty)
                    txt_ISED_VAT.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["VAT"].ToString().Trim()).ToString("C");
                else
                    txt_ISED_VAT.Text = "R0.00";
            }
            else
            {
                if (dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString() != string.Empty)
                    txt_ISED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString().Trim()).ToString("C", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                else
                    txt_ISED_Amt.Text = "$0.00";
                if (dt.Rows[SELECTED_INVOICE]["VAT"].ToString() != string.Empty)
                    txt_ISED_VAT.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["VAT"].ToString().Trim()).ToString("C", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                else
                    txt_ISED_VAT.Text = "$0.00";
            }
            if (dt.Rows[SELECTED_INVOICE]["Paid"].ToString() == "Yes")
            {
                cb_ISED_Paid.Checked = true;
                dtp_ISED_DatePaid.Enabled = true;
            }
            else
                cb_ISED_Paid.Checked = false;
            if (dt.Rows[SELECTED_INVOICE]["Date_Paid"].ToString() != string.Empty)
                dtp_ISED_DatePaid.Value = Convert.ToDateTime(dt.Rows[SELECTED_INVOICE]["Date_Paid"].ToString());
            else
                dtp_ISED_DatePaid.Value = DateTime.Now;
        }

        private void Txt_ISED_Amt_TextChanged(object sender, EventArgs e)
        {
            if (!isInter)
            {
                Decimal result;
                if (Decimal.TryParse(txt_ISED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
                {
                    result /= new Decimal(100);
                    txt_ISED_Amt.TextChanged -= new EventHandler(Txt_ISED_Amt_TextChanged);
                    txt_ISED_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                    txt_ISED_Amt.TextChanged += new EventHandler(Txt_ISED_Amt_TextChanged);
                    txt_ISED_Amt.Select(txt_ISED_Amt.Text.Length, 0);
                }
                if (TextisValid(txt_ISED_Amt.Text))
                    return;
                txt_ISED_Amt.Text = "R0.00";
                txt_ISED_Amt.Select(txt_ISED_Amt.Text.Length, 0);
            }
            else
            {
                Decimal result;
                if (Decimal.TryParse(txt_ISED_Amt.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
                {
                    result /= new Decimal(100);
                    txt_ISED_Amt.TextChanged -= new EventHandler(Txt_ISED_Amt_TextChanged);
                    txt_ISED_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", result);
                    txt_ISED_Amt.TextChanged += new EventHandler(Txt_ISED_Amt_TextChanged);
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
                Decimal result;
                if (Decimal.TryParse(txt_ISED_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
                {
                    result /= new Decimal(100);
                    txt_ISED_VAT.TextChanged -= new EventHandler(Txt_ISED_VAT_TextChanged);
                    txt_ISED_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                    txt_ISED_VAT.TextChanged += new EventHandler(Txt_ISED_VAT_TextChanged);
                    txt_ISED_VAT.Select(txt_ISED_VAT.Text.Length, 0);
                }
                if (TextisValid(txt_ISED_VAT.Text))
                    return;
                txt_ISED_VAT.Text = "R0.00";
                txt_ISED_VAT.Select(txt_ISED_VAT.Text.Length, 0);
            }
            else
            {
                Decimal result;
                if (Decimal.TryParse(txt_ISED_VAT.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
                {
                    result /= new Decimal(100);
                    txt_ISED_VAT.TextChanged -= new EventHandler(Txt_ISED_VAT_TextChanged);
                    txt_ISED_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", result);
                    txt_ISED_VAT.TextChanged += new EventHandler(Txt_ISED_VAT_TextChanged);
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
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UPDATE Invoices_Send SET Invoice_Number = @INum, Date = @Date, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid, Date_Paid = @DPaid WHERE Invoice_Number = @oldINum", dbConnection))
                    {
                        Decimal num1;
                        Decimal num2;
                        if (!isInter)
                        {
                            num1 = !txt_ISED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_ISED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_ISED_Amt.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                            num2 = !txt_ISED_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_ISED_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_ISED_VAT.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                        }
                        else
                        {
                            num1 = !txt_ISED_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_ISED_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(txt_ISED_Amt.Text.Replace("$", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte)2));
                            num2 = !txt_ISED_VAT.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_ISED_VAT.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(txt_ISED_VAT.Text.Replace("$", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte)2));
                        }
                        sqlCommand.Parameters.AddWithValue("@Date", dtp_ISED_Date.Value);
                        sqlCommand.Parameters.AddWithValue("@Desc", txt_ISED_Desc.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Amt", num1);
                        sqlCommand.Parameters.AddWithValue("@VAT", num2);
                        if (cb_ISED_Paid.Checked)
                        {
                            sqlCommand.Parameters.AddWithValue("@Paid", "Yes");
                            sqlCommand.Parameters.AddWithValue("@DPaid", dtp_ISED_DatePaid.Value);
                        }
                        else
                        {
                            sqlCommand.Parameters.AddWithValue("@Paid", "No");
                            sqlCommand.Parameters.AddWithValue("@DPaid", DBNull.Value);
                        }
                        if (txt_ISED_INInst.Text == string.Empty)
                            sqlCommand.Parameters.AddWithValue("@INum", txt_ISED_InvNum.Text.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@INum", (txt_ISED_InvNum.Text.Trim() + "." + txt_ISED_INInst.Text.Trim()));
                        sqlCommand.Parameters.AddWithValue("@oldINum", oldINum);
                        sqlCommand.ExecuteNonQuery();
                        int num3 = (int)MessageBox.Show("Invoice successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                Decimal result;
                if (!Decimal.TryParse(txt_ISED_Amt.Text.Replace("R", string.Empty), out result))
                    return;
                result -= result / new Decimal(115, 0, 0, false, (byte)2);
                txt_ISED_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
            }
            else
            {
                Decimal num = Decimal.Parse(txt_ISED_Amt.Text.Replace("$", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                txt_ISED_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", (num - num / new Decimal(115, 0, 0, false, (byte)2)));
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
            if (txt_ISED_InvNum.Focused)
                return;
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
            if (txt_ISED_INInst.Focused)
                return;
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
            if (txt_ISED_Desc.Focused)
                return;
            ln_ISED_Desc.LineColor = Color.Gray;
        }

        private void Txt_ISED_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_ISED_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISED_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (txt_ISED_Amt.Focused)
                return;
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
            if (txt_ISED_VAT.Focused)
                return;
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
            if (!mouseDown)
                return;
            Point location = Location;
            int x = location.X - lastLocation.X + e.X;
            location = Location;
            int y = location.Y - lastLocation.Y + e.Y;
            Location = new Point(x, y);
            this.Update();
        }

        private void Inv_Sent_Edit_Del_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Cb_ISED_Paid_OnChange(object sender, EventArgs e)
        {
            if (cb_ISED_Paid.Checked)
                dtp_ISED_DatePaid.Enabled = true;
            else
                dtp_ISED_DatePaid.Enabled = false;
        }
    }
}
