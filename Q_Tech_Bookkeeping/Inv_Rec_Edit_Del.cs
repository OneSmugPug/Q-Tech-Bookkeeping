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
        private IContainer components = (IContainer)null;
        private DataTable dt;
        private int SELECTED_INVOICE;
        private string oldINum;
        private Point lastLocation;

        public Inv_Rec_Edit_Del()
        {
            InitializeComponent();
        }

        private void Inv_Rec_Edit_Del_Load(object sender, EventArgs e)
        {
            txt_IRED_SuppName.Focus();
            Inv_RecOld curForm = (Inv_RecOld)((Home)Owner).GetCurForm();
            dt = curForm.getInvRec();
            SELECTED_INVOICE = curForm.getSelectedInv();
            LoadInvRec();
            oldINum = txt_IRED_InvNum.Text.Trim();
        }

        private void LoadInvRec()
        {
            txt_IRED_SuppName.Text = dt.Rows[SELECTED_INVOICE]["Supplier"].ToString().Trim();
            txt_IRED_InvNum.Text = dt.Rows[SELECTED_INVOICE]["Invoice_Number"].ToString().Trim();
            dtp_IRED_Date.Value = !(dt.Rows[SELECTED_INVOICE]["Date"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[SELECTED_INVOICE]["Date"].ToString());
            txt_IRED_Desc.Text = dt.Rows[SELECTED_INVOICE]["Description"].ToString().Trim();
            if (dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString() != string.Empty)
                txt_IRED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["Total_Amount"].ToString().Trim()).ToString("C");
            else
                txt_IRED_Amt.Text = "R0.00";
            if (dt.Rows[SELECTED_INVOICE]["VAT"].ToString() != string.Empty)
                txt_IRED_VAT.Text = Convert.ToDecimal(dt.Rows[SELECTED_INVOICE]["VAT"].ToString().Trim()).ToString("C");
            else
                txt_IRED_VAT.Text = "R0.00";
            if (dt.Rows[SELECTED_INVOICE]["Paid"].ToString() == "Yes")
                cb_IRED_Paid.Checked = true;
            else
                cb_IRED_Paid.Checked = false;
        }

        private void Txt_IRED_Amt_TextChanged(object sender, EventArgs e)
        {
            Decimal result;
            if (Decimal.TryParse(txt_IRED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
            {
                Decimal num = result / new Decimal(100);
                txt_IRED_Amt.TextChanged -= new EventHandler(Txt_IRED_Amt_TextChanged);
                txt_IRED_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", num);
                txt_IRED_Amt.TextChanged += new EventHandler(Txt_IRED_Amt_TextChanged);
                txt_IRED_Amt.Select(txt_IRED_Amt.Text.Length, 0);
            }
            if (TextisValid(txt_IRED_Amt.Text))
                return;
            txt_IRED_Amt.Text = "R0.00";
            txt_IRED_Amt.Select(txt_IRED_Amt.Text.Length, 0);
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }

        private void Txt_IRED_VAT_TextChanged(object sender, EventArgs e)
        {
            Decimal result;
            if (Decimal.TryParse(txt_IRED_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
            {
                Decimal num = result / new Decimal(100);
                txt_IRED_VAT.TextChanged -= new EventHandler(Txt_IRED_VAT_TextChanged);
                txt_IRED_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", num);
                txt_IRED_VAT.TextChanged += new EventHandler(Txt_IRED_VAT_TextChanged);
                txt_IRED_VAT.Select(txt_IRED_VAT.Text.Length, 0);
            }
            if (TextisValid(txt_IRED_VAT.Text))
                return;
            txt_IRED_VAT.Text = "R0.00";
            txt_IRED_VAT.Select(txt_IRED_VAT.Text.Length, 0);
        }

        private void Btn_IRED_Done_Click(object sender, EventArgs e)
        {
            if (txt_IRED_InvNum.Text != string.Empty)
            {
                if (MessageBox.Show("Are you sure you want to update invoice?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                if (txt_IRED_InvNum.Text == oldINum)
                {
                    using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                    {
                        dbConnection.Open();
                        try
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Invoices_Received SET Date = @Date, Supplier = @Supp, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid WHERE Invoice_Number = @INum", dbConnection))
                            {
                                Decimal num1 = !txt_IRED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_IRED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_IRED_Amt.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                                Decimal num2 = !txt_IRED_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_IRED_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_IRED_VAT.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                                sqlCommand.Parameters.AddWithValue("@Date", dtp_IRED_Date.Value);
                                sqlCommand.Parameters.AddWithValue("@Supp", txt_IRED_SuppName.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Desc", txt_IRED_Desc.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Amt", num1);
                                sqlCommand.Parameters.AddWithValue("@VAT", num2);
                                if (cb_IRED_Paid.Checked)
                                    sqlCommand.Parameters.AddWithValue("@Paid", "Yes");
                                else
                                    sqlCommand.Parameters.AddWithValue("@Paid", "No");
                                sqlCommand.Parameters.AddWithValue("@INum", txt_IRED_InvNum.Text.Trim());
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
                else if (txt_IRED_InvNum.Text != oldINum)
                {
                    using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                    {
                        dbConnection.Open();
                        try
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Invoices_Received SET Date = @Date, Invoice_Number = @oldINum, Supplier = @Supp, Description = @Desc, Total_Amount = @Amt, VAT = @VAT, Paid = @Paid WHERE Invoice_Number = @INum", dbConnection))
                            {
                                Decimal num1 = !txt_IRED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_IRED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_IRED_Amt.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                                Decimal num2 = !txt_IRED_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_IRED_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_IRED_VAT.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                                sqlCommand.Parameters.AddWithValue("@Date", dtp_IRED_Date.Value);
                                sqlCommand.Parameters.AddWithValue("@oldINum", txt_IRED_InvNum.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Supp", txt_IRED_SuppName.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Desc", txt_IRED_Desc.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Amt", num1);
                                sqlCommand.Parameters.AddWithValue("@VAT", num2);
                                if (cb_IRED_Paid.Checked)
                                    sqlCommand.Parameters.AddWithValue("@Paid", "Yes");
                                else
                                    sqlCommand.Parameters.AddWithValue("@Paid", "No");
                                sqlCommand.Parameters.AddWithValue("@INum", oldINum);
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
            }
            else
            {
                int num4 = (int)MessageBox.Show("Please enter an Invoice Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_IRED_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_IRED_Amt_Leave(object sender, EventArgs e)
        {
            Decimal result;
            if (!Decimal.TryParse(txt_IRED_Amt.Text.Replace("R", string.Empty), out result))
                return;
            txt_IRED_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (result - result / new Decimal(115, 0, 0, false, (byte)2)));
        }

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
            if (txt_IRED_InvNum.Focused)
                return;
            ln_IRED_InvNum.LineColor = Color.Gray;
        }

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
            if (txt_IRED_SuppName.Focused)
                return;
            ln_IRED_SuppName.LineColor = Color.Gray;
        }

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
            if (txt_IRED_Desc.Focused)
                return;
            ln_IRED_Desc.LineColor = Color.Gray;
        }

        private void Txt_IRED_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_IRED_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRED_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (txt_IRED_Amt.Focused)
                return;
            ln_IRED_Amt.LineColor = Color.Gray;
        }

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
            if (txt_IRED_VAT.Focused)
                return;
            ln_IRED_VAT.LineColor = Color.Gray;
        }

        private void Btn_IRED_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_IRED_Close.Image = Resources.close_white;
        }

        private void Btn_IRED_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_IRED_Close.Image = Resources.close_black;
        }

        private void Btn_IRED_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_IRED_Done.ForeColor = Color.White;
        }

        private void Btn_IRED_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_IRED_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IRED_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_IRED_Cancel.ForeColor = Color.White;
        }

        private void Btn_IRED_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_IRED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Inv_Rec_Edit_Del_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Inv_Rec_Edit_Del_MouseMove(object sender, MouseEventArgs e)
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

        private void Inv_Rec_Edit_Del_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Btn_IRED_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
