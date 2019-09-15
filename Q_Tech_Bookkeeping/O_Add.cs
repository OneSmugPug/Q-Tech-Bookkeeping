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
    public partial class O_Add : Form
    {
        private bool isInter = false;
        private bool mouseDown = false;
        private IContainer components = (IContainer)null;
        private Decimal pInv;
        private Decimal pRec;
        private StringBuilder sb;
        private Point lastLocation;

        public O_Add()
        {
            InitializeComponent();
        }

        private void Txt_OA_Amt_TextChanged(object sender, EventArgs e)
        {
            if (isInter)
            {
                Decimal result;
                if (Decimal.TryParse(txt_OA_Amt.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0'), out result))
                {
                    Decimal num = result / new Decimal(100);
                    txt_OA_Amt.TextChanged -= new EventHandler(Txt_OA_Amt_TextChanged);
                    txt_OA_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", num);
                    txt_OA_Amt.TextChanged += new EventHandler(Txt_OA_Amt_TextChanged);
                    txt_OA_Amt.Select(txt_OA_Amt.Text.Length, 0);
                }
                if (TextisValid(txt_OA_Amt.Text))
                    return;
                txt_OA_Amt.Text = "$0.00";
                txt_OA_Amt.Select(txt_OA_Amt.Text.Length, 0);
            }
            else
            {
                Decimal result;
                if (Decimal.TryParse(txt_OA_Amt.Text.Replace(",", "").Replace("R", "").Replace(".", "").TrimStart('0'), out result))
                {
                    Decimal num = result / new Decimal(100);
                    txt_OA_Amt.TextChanged -= new EventHandler(Txt_OA_Amt_TextChanged);
                    txt_OA_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", num);
                    txt_OA_Amt.TextChanged += new EventHandler(Txt_OA_Amt_TextChanged);
                    txt_OA_Amt.Select(txt_OA_Amt.Text.Length, 0);
                }
                if (!TextisValid(txt_OA_Amt.Text))
                {
                    txt_OA_Amt.Text = "R0.00";
                    txt_OA_Amt.Select(txt_OA_Amt.Text.Length, 0);
                }
            }
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }

        private void O_Add_Load(object sender, EventArgs e)
        {
            Home owner = (Home)Owner;
            txt_OA_PercInv.Text = "0%";
            txt_OA_PercRec.Text = "0%";
            if (owner.GetCurPanel() == "pnl_L_Orders")
            {
                Orders curForm = (Orders)owner.GetCurForm();
                txt_OA_CCode.Text = curForm.GetCCode();
                txt_OA_CName.Text = curForm.GetCName();
                txt_OA_Amt.Text = "R0.00";
            }
            else
            {
                isInter = true;
                Int_Orders curForm = (Int_Orders)owner.GetCurForm();
                txt_OA_CCode.Text = curForm.GetCCode();
                txt_OA_CName.Text = curForm.GetCName();
                txt_OA_Amt.Text = "$0.00";
            }
            txt_OA_Amt.SelectionStart = txt_OA_Amt.Text.Length;
        }

        private void Txt_OA_Perc_Inv_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (double.TryParse(txt_OA_PercInv.Text, out result) && Convert.ToDouble(txt_OA_PercInv.Text) >= 0.0 && Convert.ToDouble(txt_OA_PercInv.Text) <= 100.0)
            {
                pInv = Convert.ToDecimal(txt_OA_PercInv.Text.ToString());
                txt_OA_PercInv.Text = result.ToString() + "%";
            }
            else if (txt_OA_PercInv.Text == string.Empty)
            {
                txt_OA_PercInv.Text = "0%";
            }
            else
            {
                e.Cancel = true;
                int num = (int)MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Txt_OA_Perc_Rec_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (double.TryParse(txt_OA_PercRec.Text, out result) && Convert.ToDouble(txt_OA_PercRec.Text) >= 0.0 && Convert.ToDouble(txt_OA_PercRec.Text) <= 100.0)
            {
                pRec = Convert.ToDecimal(txt_OA_PercRec.Text.ToString());
                txt_OA_PercRec.Text = result.ToString() + "%";
            }
            else if (txt_OA_PercRec.Text == string.Empty)
            {
                txt_OA_PercRec.Text = "0%";
            }
            else
            {
                e.Cancel = true;
                int num = (int)MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Txt_OA_Perc_Inv_Enter(object sender, EventArgs e)
        {
            txt_OA_PercInv.Clear();
        }

        private void Txt_OA_Perc_Rec_Enter(object sender, EventArgs e)
        {
            txt_OA_PercRec.Clear();
        }

        private void Btn_OA_Done_Click(object sender, EventArgs e)
        {
            string text = txt_OA_CONum.Text;
            sb = new StringBuilder().Append("Are you sure you want to add order with Client Order Number: ").Append(text).Append("?");
            if (text != string.Empty)
            {
                if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                {
                    dbConnection.Open();
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Orders_Received VALUES (@Date, @Client, @CONum, @Desc, @Amt, @PercInv, @PercRec, @QNum)", dbConnection))
                        {
                            Decimal num1 = !isInter ? (!txt_OA_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_OA_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_OA_Amt.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2))) : (!txt_OA_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_OA_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(txt_OA_Amt.Text.Replace("$", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte)2)));
                            sqlCommand.Parameters.AddWithValue("@Date", dtp_OA_Date.Value);
                            sqlCommand.Parameters.AddWithValue("@Client", txt_OA_CName.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@CONum", txt_OA_CONum.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Desc", txt_OA_Desc.Text.Trim());
                            sqlCommand.Parameters.AddWithValue("@Amt", num1);
                            sqlCommand.Parameters.AddWithValue("@PercInv", pInv);
                            sqlCommand.Parameters.AddWithValue("@PercRec", pRec);
                            sqlCommand.Parameters.AddWithValue("@QNum", txt_OA_QNum.Text.Trim());
                            sqlCommand.ExecuteNonQuery();
                            int num2 = (int)MessageBox.Show("New order successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                int num3 = (int)MessageBox.Show("Please enter a Client Order Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_OA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_OA_CONum_MouseEnter(object sender, EventArgs e)
        {
            ln_OA_CONum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OA_CONum_Leave(object sender, EventArgs e)
        {
            ln_OA_CONum.LineColor = Color.Gray;
        }

        private void Txt_OA_CONum_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OA_CONum.Focused)
                return;
            ln_OA_CONum.LineColor = Color.Gray;
        }

        private void Txt_OA_Desc_Leave(object sender, EventArgs e)
        {
            ln_OA_Desc.LineColor = Color.Gray;
        }

        private void Txt_OA_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_OA_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OA_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OA_Desc.Focused)
                return;
            ln_OA_Desc.LineColor = Color.Gray;
        }

        private void Txt_OA_Amt_Leave(object sender, EventArgs e)
        {
            ln_OA_Amt.LineColor = Color.Gray;
        }

        private void Txt_OA_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_OA_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OA_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OA_Amt.Focused)
                return;
            ln_OA_Amt.LineColor = Color.Gray;
        }

        private void Txt_OA_PercInv_Leave(object sender, EventArgs e)
        {
            ln_OA_PercInv.LineColor = Color.Gray;
        }

        private void Txt_OA_PercInv_MouseEnter(object sender, EventArgs e)
        {
            ln_OA_PercInv.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OA_PercInv_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OA_PercInv.Focused)
                return;
            ln_OA_PercInv.LineColor = Color.Gray;
        }

        private void Txt_OA_PercRec_Leave(object sender, EventArgs e)
        {
            ln_OA_PercRec.LineColor = Color.Gray;
        }

        private void Txt_OA_PercRec_MouseEnter(object sender, EventArgs e)
        {
            ln_OA_PercRec.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OA_PercRec_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OA_PercRec.Focused)
                return;
            ln_OA_PercRec.LineColor = Color.Gray;
        }

        private void Txt_OA_QNum_Leave(object sender, EventArgs e)
        {
            ln_OA_QNum.LineColor = Color.Gray;
        }

        private void Txt_OA_QNum_MouseEnter(object sender, EventArgs e)
        {
            ln_OA_QNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OA_QNum_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OA_QNum.Focused)
                return;
            ln_OA_QNum.LineColor = Color.Gray;
        }

        private void Btn_OA_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_OA_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_OA_Close.Image = Resources.close_white;
        }

        private void Btn_OA_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_OA_Close.Image = Resources.close_black;
        }

        private void Btn_OA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_OA_Done.ForeColor = Color.White;
        }

        private void Btn_OA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_OA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_OA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_OA_Cancel.ForeColor = Color.White;
        }

        private void Btn_OA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_OA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Txt_OA_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_OA_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void O_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void O_Add_MouseMove(object sender, MouseEventArgs e)
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

        private void O_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
