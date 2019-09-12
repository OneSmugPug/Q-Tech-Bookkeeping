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
        private IContainer components = (IContainer)null;
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

        private void Txt_IRA_Amt_TextChanged(object sender, EventArgs e)
        {
            Decimal result;
            if (Decimal.TryParse(txt_IRA_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
            {
                Decimal num = result / new Decimal(100);
                txt_IRA_Amt.TextChanged -= new EventHandler(Txt_IRA_Amt_TextChanged);
                txt_IRA_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", num);
                txt_IRA_Amt.TextChanged += new EventHandler(Txt_IRA_Amt_TextChanged);
                txt_IRA_Amt.Select(txt_IRA_Amt.Text.Length, 0);
            }
            if (TextisValid(txt_IRA_Amt.Text))
                return;
            txt_IRA_Amt.Text = "R0.00";
            txt_IRA_Amt.Select(txt_IRA_Amt.Text.Length, 0);
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }

        private void Txt_IRA_VAT_TextChanged(object sender, EventArgs e)
        {
            Decimal result;
            if (Decimal.TryParse(txt_IRA_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
            {
                Decimal num = result / new Decimal(100);
                txt_IRA_VAT.TextChanged -= new EventHandler(Txt_IRA_VAT_TextChanged);
                txt_IRA_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", num);
                txt_IRA_VAT.TextChanged += new EventHandler(Txt_IRA_VAT_TextChanged);
                txt_IRA_VAT.Select(txt_IRA_VAT.Text.Length, 0);
            }
            if (TextisValid(txt_IRA_VAT.Text))
                return;
            txt_IRA_VAT.Text = "R0.00";
            txt_IRA_VAT.Select(txt_IRA_VAT.Text.Length, 0);
        }

        private void Txt_IRA_Amt_Leave(object sender, EventArgs e)
        {
            Decimal result;
            if (!Decimal.TryParse(txt_IRA_Amt.Text.Replace("R", string.Empty), out result))
                return;
            txt_IRA_VAT.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", (result - result / new Decimal(115, 0, 0, false, (byte)2)));
        }

        private void Btn_IRA_Done_Click(object sender, EventArgs e)
        {
            string text = txt_IRA_InvNum.Text;
            sb = new StringBuilder().Append("Are you sure you want to add invoice with Invoice Number: ").Append(text).Append("?");
            if (text != string.Empty)
            {
                if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                {
                    dbConnection.Open();
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Invoices_Received VALUES (@Date, @InvNum, @Supp, @Desc, @Amt, @VAT, @Paid)", dbConnection))
                        {
                            Decimal num1 = !txt_IRA_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_IRA_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_IRA_Amt.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                            Decimal num2 = !txt_IRA_VAT.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_IRA_VAT.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_IRA_VAT.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2));
                            sqlCommand.Parameters.AddWithValue("@Date", dtp_IRA_Date.Value);
                            sqlCommand.Parameters.AddWithValue("@InvNum", txt_IRA_InvNum.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Supp", txt_IRA_SuppName.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Desc", txt_IRA_Desc.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Amt", num1);
                            sqlCommand.Parameters.AddWithValue("@VAT", num2);
                            if (cb_IRA_Paid.Checked)
                                sqlCommand.Parameters.AddWithValue("@Paid", "Yes");
                            else
                                sqlCommand.Parameters.AddWithValue("@Paid", "No");
                            sqlCommand.ExecuteNonQuery();
                            int num3 = (int)MessageBox.Show("New invoice successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
            else
            {
                int num4 = (int)MessageBox.Show("Please enter an Invoice Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_IRA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
            if (txt_IRA_InvNum.Focused)
                return;
            ln_IRA_InvNum.LineColor = Color.Gray;
        }

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
            if (txt_IRA_SuppName.Focused)
                return;
            ln_IRA_SuppName.LineColor = Color.Gray;
        }

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
            if (txt_IRA_Desc.Focused)
                return;
            ln_IRA_Desc.LineColor = Color.Gray;
        }

        private void Txt_IRA_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_IRA_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_IRA_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (txt_IRA_Amt.Focused)
                return;
            ln_IRA_Amt.LineColor = Color.Gray;
        }

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
            if (txt_IRA_VAT.Focused)
                return;
            ln_IRA_VAT.LineColor = Color.Gray;
        }

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

        private void Btn_IRA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_IRA_Done.ForeColor = Color.White;
        }

        private void Btn_IRA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_IRA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_IRA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_IRA_Cancel.ForeColor = Color.White;
        }

        private void Btn_IRA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_IRA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Inv_Rec_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Inv_Rec_Add_MouseMove(object sender, MouseEventArgs e)
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

        private void Inv_REc_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
