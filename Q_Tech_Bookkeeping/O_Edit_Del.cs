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
    public partial class O_Edit_Del : Form
    {
        private bool isInter = false;
        private bool mouseDown = false;
        private IContainer components = (IContainer)null;
        private DataTable dt;
        private int SELECTED_ORDER;
        private string oldCONum;
        private Decimal pInv;
        private Decimal pRec;
        private Point lastLocation;
        

        public O_Edit_Del()
        {
            InitializeComponent();
        }

        private void O_Edit_Del_Load(object sender, EventArgs e)
        {
            Home owner = (Home)Owner;
            if (owner.GetCurPanel() == "pnl_L_Orders")
            {
                Orders curForm = (Orders)owner.GetCurForm();
                dt = curForm.GetOrders();
                SELECTED_ORDER = curForm.GetSelectedOrder();
                txt_OED_CCode.Text = curForm.GetCCode();
                txt_OED_CName.Text = curForm.GetCName();
            }
            else
            {
                isInter = true;
                Int_Orders curForm = (Int_Orders)owner.GetCurForm();
                dt = curForm.GetOrders();
                SELECTED_ORDER = curForm.GetSelectedOrder();
                txt_OED_CCode.Text = curForm.GetCCode();
                txt_OED_CName.Text = curForm.GetCName();
            }
            LoadOrder();
            oldCONum = txt_OED_CONum.Text.Trim();
        }

        private void LoadOrder()
        {
            txt_OED_CONum.Text = dt.Rows[SELECTED_ORDER]["Client_Order_Number"].ToString().Trim();
            dtp_OED_Date.Value = !(dt.Rows[SELECTED_ORDER]["Date"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[SELECTED_ORDER]["Date"].ToString());
            if (isInter)
            {
                if (dt.Rows[SELECTED_ORDER]["Amount"].ToString() != string.Empty)
                    txt_OED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_ORDER]["Amount"].ToString().Trim()).ToString("c", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                else
                    txt_OED_Amt.Text = "$0.00";
            }
            else if (dt.Rows[SELECTED_ORDER]["Amount"].ToString() != string.Empty)
                txt_OED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_ORDER]["Amount"].ToString().Trim()).ToString("c");
            else
                txt_OED_Amt.Text = "R0.00";
            txt_OED_Amt.SelectionStart = txt_OED_Amt.Text.Length;
            txt_OED_Desc.Text = dt.Rows[SELECTED_ORDER]["Description"].ToString().Trim();
            txt_OED_PercInv.Text = (!(dt.Rows[SELECTED_ORDER]["Percentage_Invoiced"].ToString() != string.Empty) ? 0.0 : Convert.ToDouble(dt.Rows[SELECTED_ORDER]["Percentage_Invoiced"].ToString().Trim())).ToString("p0");
            txt_OED_PercRec.Text = (!(dt.Rows[SELECTED_ORDER]["Percentage_Received"].ToString() != string.Empty) ? 0.0 : Convert.ToDouble(dt.Rows[SELECTED_ORDER]["Percentage_Received"].ToString().Trim())).ToString("p0");
            txt_OED_QNum.Text = dt.Rows[SELECTED_ORDER]["Quote_Number"].ToString().Trim();
        }

        private void Txt_OED_Amt_TextChanged(object sender, EventArgs e)
        {
            if (isInter)
            {
                Decimal result;
                if (Decimal.TryParse(txt_OED_Amt.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
                {
                    Decimal num = result / new Decimal(100);
                    txt_OED_Amt.TextChanged -= new EventHandler(Txt_OED_Amt_TextChanged);
                    txt_OED_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", num);
                    txt_OED_Amt.TextChanged += new EventHandler(Txt_OED_Amt_TextChanged);
                    txt_OED_Amt.Select(txt_OED_Amt.Text.Length, 0);
                }
                if (TextisValid(txt_OED_Amt.Text))
                    return;
                txt_OED_Amt.Text = "$0.00";
                txt_OED_Amt.Select(txt_OED_Amt.Text.Length, 0);
            }
            else
            {
                Decimal result;
                if (Decimal.TryParse(txt_OED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out result))
                {
                    Decimal num = result / new Decimal(100);
                    txt_OED_Amt.TextChanged -= new EventHandler(Txt_OED_Amt_TextChanged);
                    txt_OED_Amt.Text = string.Format((IFormatProvider)CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", num);
                    txt_OED_Amt.TextChanged += new EventHandler(Txt_OED_Amt_TextChanged);
                    txt_OED_Amt.Select(txt_OED_Amt.Text.Length, 0);
                }
                if (!TextisValid(txt_OED_Amt.Text))
                {
                    txt_OED_Amt.Text = "R0.00";
                    txt_OED_Amt.Select(txt_OED_Amt.Text.Length, 0);
                }
            }
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }

        private void Txt_OED_Perc_Rec_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (double.TryParse(txt_OED_PercRec.Text, out result) && Convert.ToDouble(txt_OED_PercRec.Text) >= 0.0 && Convert.ToDouble(txt_OED_PercRec.Text) <= 100.0)
            {
                pRec = Convert.ToDecimal(txt_OED_PercRec.Text.ToString());
                txt_OED_PercRec.Text = result.ToString() + "%";
            }
            else if (txt_OED_PercRec.Text == string.Empty)
            {
                txt_OED_PercRec.Text = "0%";
            }
            else
            {
                e.Cancel = true;
                int num = (int)MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Txt_OED_Perc_Inv_Validating(object sender, CancelEventArgs e)
        {
            double result;
            if (double.TryParse(txt_OED_PercInv.Text, out result) && Convert.ToDouble(txt_OED_PercInv.Text) >= 0.0 && Convert.ToDouble(txt_OED_PercInv.Text) <= 100.0)
            {
                pInv = Convert.ToDecimal(txt_OED_PercInv.Text.ToString());
                txt_OED_PercInv.Text = result.ToString() + "%";
            }
            else if (txt_OED_PercInv.Text == string.Empty)
            {
                txt_OED_PercInv.Text = "0%";
            }
            else
            {
                e.Cancel = true;
                int num = (int)MessageBox.Show("Invalid value entered. Please enter a value between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_OED_Done_Click(object sender, EventArgs e)
        {
            if (txt_OED_CONum.Text != string.Empty)
            {
                if (MessageBox.Show("Are you sure you want to update order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                if (txt_OED_CONum.Text.Trim() == oldCONum)
                {
                    using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                    {
                        dbConnection.Open();
                        try
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Orders_Received SET Date = @Date, Description = @Desc, Amount = @Amt, Percentage_Invoiced = @PercInv, Percentage_Received = @PercRec, Quote_Number = @QNum WHERE Client_Order_Number = @CONum", dbConnection))
                            {
                                Decimal num1 = !isInter ? (!txt_OED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_OED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_OED_Amt.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2))) : (!txt_OED_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_OED_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(txt_OED_Amt.Text.Replace("$", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte)2)));
                                sqlCommand.Parameters.AddWithValue("@Date", dtp_OED_Date.Value);
                                sqlCommand.Parameters.AddWithValue("@Desc", txt_OED_Desc.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Amt", num1);
                                sqlCommand.Parameters.AddWithValue("@PercInv", pInv);
                                sqlCommand.Parameters.AddWithValue("@PercRec", pRec);
                                sqlCommand.Parameters.AddWithValue("@QNum", txt_OED_QNum.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@CONum", oldCONum);
                                sqlCommand.ExecuteNonQuery();
                                int num2 = (int)MessageBox.Show("Order successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                }
                else if (txt_OED_CONum.Text.Trim() != oldCONum)
                {
                    using (SqlConnection dbConnection = DBUtils.GetDBConnection())
                    {
                        dbConnection.Open();
                        try
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Orders_Received SET Client_Order_Number = @CONum, Date = @Date, Description = @Desc, Amount = @Amt, Percentage_Invoiced = @PercInv, Percentage_Received = @PercRec, Quote_Number = @QNum WHERE Client_Order_Number = @oldCONum", dbConnection))
                            {
                                Decimal num1 = !isInter ? (!txt_OED_Amt.Text.Contains("R") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_OED_Amt.Text.Replace("R", string.Empty) == "0.00") ? Decimal.Parse(txt_OED_Amt.Text.Replace("R", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-ZA")) : new Decimal(0, 0, 0, false, (byte)2))) : (!txt_OED_Amt.Text.Contains("$") ? new Decimal(0, 0, 0, false, (byte)2) : (!(txt_OED_Amt.Text.Replace("$", string.Empty) == "0.00") ? Decimal.Parse(txt_OED_Amt.Text.Replace("$", string.Empty), (IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : new Decimal(0, 0, 0, false, (byte)2)));
                                sqlCommand.Parameters.AddWithValue("@CONum", txt_OED_CONum.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Date", dtp_OED_Date.Value);
                                sqlCommand.Parameters.AddWithValue("@Desc", txt_OED_Desc.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@Amt", num1);
                                sqlCommand.Parameters.AddWithValue("@PercInv", pInv);
                                sqlCommand.Parameters.AddWithValue("@PercRec", pRec);
                                sqlCommand.Parameters.AddWithValue("@QNum", txt_OED_QNum.Text.Trim());
                                sqlCommand.Parameters.AddWithValue("@oldCONum", oldCONum);
                                sqlCommand.ExecuteNonQuery();
                                int num2 = (int)MessageBox.Show("Order successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                int num3 = (int)MessageBox.Show("Please enter a Client Order Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_OED_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_OED_Perc_Inv_Enter(object sender, EventArgs e)
        {
            txt_OED_PercInv.Clear();
        }

        private void Txt_OED_Perc_Rec_Enter(object sender, EventArgs e)
        {
            txt_OED_PercRec.Clear();
        }

        private void Btn_OED_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_OED_CONum_MouseEnter(object sender, EventArgs e)
        {
            ln_OED_CONum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OED_CONum_Leave(object sender, EventArgs e)
        {
            ln_OED_CONum.LineColor = Color.Gray;
        }

        private void Txt_OED_CONum_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OED_CONum.Focused)
                return;
            ln_OED_CONum.LineColor = Color.Gray;
        }

        private void Txt_OED_Desc_Leave(object sender, EventArgs e)
        {
            ln_OED_Desc.LineColor = Color.Gray;
        }

        private void Txt_OED_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_OED_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OED_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OED_Desc.Focused)
                return;
            ln_OED_Desc.LineColor = Color.Gray;
        }

        private void Txt_OED_Amt_Leave(object sender, EventArgs e)
        {
            ln_OED_Amt.LineColor = Color.Gray;
        }

        private void Txt_OED_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_OED_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OED_Amt_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OED_Amt.Focused)
                return;
            ln_OED_Amt.LineColor = Color.Gray;
        }

        private void Txt_OED_PercInv_Leave(object sender, EventArgs e)
        {
            ln_OED_PercInv.LineColor = Color.Gray;
        }

        private void Txt_OED_PercInv_MouseEnter(object sender, EventArgs e)
        {
            ln_OED_PercInv.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OED_PercInv_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OED_PercInv.Focused)
                return;
            ln_OED_PercInv.LineColor = Color.Gray;
        }

        private void Txt_OED_PercRec_Leave(object sender, EventArgs e)
        {
            ln_OED_PercRec.LineColor = Color.Gray;
        }

        private void Txt_OED_PercRec_MouseEnter(object sender, EventArgs e)
        {
            ln_OED_PercRec.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OED_PercRec_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OED_PercRec.Focused)
                return;
            ln_OED_PercRec.LineColor = Color.Gray;
        }

        private void Txt_OED_QNum_Leave(object sender, EventArgs e)
        {
            ln_OED_QNum.LineColor = Color.Gray;
        }

        private void Txt_OED_QNum_MouseEnter(object sender, EventArgs e)
        {
            ln_OED_QNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_OED_QNum_MouseLeave(object sender, EventArgs e)
        {
            if (txt_OED_QNum.Focused)
                return;
            ln_OED_QNum.LineColor = Color.Gray;
        }

        private void Btn_OED_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_OED_Close.Image = Resources.close_white;
        }

        private void Btn_OED_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_OED_Close.Image = Resources.close_black;
        }

        private void Btn_OED_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_OED_Done.ForeColor = Color.White;
        }

        private void Btn_OED_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_OED_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_OED_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_OED_Cancel.ForeColor = Color.White;
        }

        private void Btn_OED_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_OED_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Txt_OED_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_OED_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void O_Edit_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void O_Edit_MouseMove(object sender, MouseEventArgs e)
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

        private void O_Edit_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Txt_OED_PercInv_Enter(object sender, EventArgs e)
        {
            txt_OED_PercInv.Clear();
        }

        private void Txt_OED_PercRec_Enter(object sender, EventArgs e)
        {
            txt_OED_PercRec.Clear();
        }
    }
}
