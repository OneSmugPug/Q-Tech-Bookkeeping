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
        private bool isInter = false, mouseDown = false;
        private DataTable dt;
        private int SELECTED_ORDER;
        private string oldCONum;
        private Decimal pInv, pRec;
        private Point lastLocation;
        

        public O_Edit_Del()
        {
            InitializeComponent();
        }

        //================================================================================================================================================//
        // LOAD EDIT/DELETE FORM                                                                                                                          //
        //================================================================================================================================================//
        private void O_Edit_Del_Load(object sender, EventArgs e)
        {
            Home frmHome = (Home)this.Owner;

            if (frmHome.GetCurPanel() == "pnl_L_Orders")
            {
                Orders parent = (Orders)frmHome.GetCurForm();

                dt = parent.GetOrders();
                SELECTED_ORDER = parent.GetSelectedOrder();

                txt_OED_CCode.Text = parent.GetCCode();
                txt_OED_CName.Text = parent.GetCName();
            }
            else
            {
                isInter = true;

                Int_Orders parent = (Int_Orders)frmHome.GetCurForm();

                dt = parent.GetOrders();
                SELECTED_ORDER = parent.GetSelectedOrder();

                txt_OED_CCode.Text = parent.GetCCode();
                txt_OED_CName.Text = parent.GetCName();
            }

            LoadOrder();

            oldCONum = txt_OED_CONum.Text.Trim();
        }

        private void LoadOrder()
        {
            double percInv, percRec;

            txt_OED_CONum.Text = dt.Rows[SELECTED_ORDER]["Client_Order_Number"].ToString().Trim();

            if (dt.Rows[SELECTED_ORDER]["Date"].ToString() != string.Empty)
                dtp_OED_Date.Value = Convert.ToDateTime(dt.Rows[SELECTED_ORDER]["Date"].ToString());
            else dtp_OED_Date.Value = DateTime.Now;

            if (isInter)
            {
                if (dt.Rows[SELECTED_ORDER]["Amount"].ToString() != string.Empty)
                    txt_OED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_ORDER]["Amount"].ToString().Trim()).ToString("c", CultureInfo.GetCultureInfo("en-US"));
                else txt_OED_Amt.Text = "$0.00";
            }
            else if (dt.Rows[SELECTED_ORDER]["Amount"].ToString() != string.Empty)
                txt_OED_Amt.Text = Convert.ToDecimal(dt.Rows[SELECTED_ORDER]["Amount"].ToString().Trim()).ToString("c");
            else txt_OED_Amt.Text = "R0.00";

            txt_OED_Amt.SelectionStart = txt_OED_Amt.Text.Length;
            txt_OED_Desc.Text = dt.Rows[SELECTED_ORDER]["Description"].ToString().Trim();

            if (dt.Rows[SELECTED_ORDER]["Percentage_Invoiced"].ToString() != string.Empty)
                percInv = Convert.ToDouble(dt.Rows[SELECTED_ORDER]["Percentage_Invoiced"].ToString().Trim());
            else percInv = 0;
            txt_OED_PercInv.Text = percInv.ToString("p0");

            if (dt.Rows[SELECTED_ORDER]["Percentage_Received"].ToString() != string.Empty)
                percRec = Convert.ToDouble(dt.Rows[SELECTED_ORDER]["Percentage_Received"].ToString().Trim());
            else percRec = 0;
            txt_OED_PercRec.Text = percRec.ToString("p0");

            txt_OED_QNum.Text = dt.Rows[SELECTED_ORDER]["Quote_Number"].ToString().Trim();
        }


        //================================================================================================================================================//
        // FORMAT AMOUNT TEXTBOX                                                                                                                          //
        //================================================================================================================================================//
        private void Txt_OED_Amt_TextChanged(object sender, EventArgs e)
        {
            if (isInter)
            {
                if (Decimal.TryParse(txt_OED_Amt.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_OED_Amt.TextChanged -= Txt_OED_Amt_TextChanged;

                    txt_OED_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                    txt_OED_Amt.TextChanged += Txt_OED_Amt_TextChanged;
                    txt_OED_Amt.Select(txt_OED_Amt.Text.Length, 0);
                }

                if (!TextisValid(txt_OED_Amt.Text))
                {
                    txt_OED_Amt.Text = "$0.00";
                    txt_OED_Amt.Select(txt_OED_Amt.Text.Length, 0);
                }
            }
            else
            {
                if (Decimal.TryParse(txt_OED_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_OED_Amt.TextChanged -= Txt_OED_Amt_TextChanged;

                    txt_OED_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", ul);
                    txt_OED_Amt.TextChanged += Txt_OED_Amt_TextChanged;
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

        //================================================================================================================================================//
        // FORMAT PERCENTAGE RECIEVED TEXTBOX                                                                                                             //
        //================================================================================================================================================//
        private void Txt_OED_Perc_Rec_Validating(object sender, CancelEventArgs e)
        {
            double temp;

            if (Double.TryParse(txt_OED_PercRec.Text, out temp) && Convert.ToDouble(txt_OED_PercRec.Text) >= 0 && Convert.ToDouble(txt_OED_PercRec.Text) <= 1)
            {
                pRec = Convert.ToDecimal(txt_OED_PercRec.Text.ToString());
                txt_OED_PercRec.Text = temp.ToString("p0");
            }
            else if (txt_OED_PercRec.Text == String.Empty)
                txt_OED_PercRec.Text = "0%";
            else
            {
                e.Cancel = true;
                MessageBox.Show("Invalid value entered. Please enter a value between 0 and 1. Make use of \",\" instead of \".\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //================================================================================================================================================//
        // FORMAT PERCENTAGE INVOICED TEXTBOX                                                                                                             //
        //================================================================================================================================================//
        private void Txt_OED_Perc_Inv_Validating(object sender, CancelEventArgs e)
        {
            double temp;

            if (Double.TryParse(txt_OED_PercInv.Text, out temp) && Convert.ToDouble(txt_OED_PercInv.Text) >= 0 && Convert.ToDouble(txt_OED_PercInv.Text) <= 1)
            {
                pInv = Convert.ToDecimal(txt_OED_PercInv.Text.ToString());
                txt_OED_PercInv.Text = temp.ToString("p0");
            }

            else if (txt_OED_PercInv.Text == String.Empty)
                txt_OED_PercInv.Text = "0%";
            else
            {
                e.Cancel = true;
                MessageBox.Show("Invalid value entered. Please enter a value between 0 and 1. Make use of \",\" instead of \".\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //================================================================================================================================================//
        // EDIT ORDER DETAILS                                                                                                                             //
        //================================================================================================================================================//
        private void Btn_OED_Done_Click(object sender, EventArgs e)
        {
            if (txt_OED_CONum.Text != string.Empty)
            {
                if (MessageBox.Show("Are you sure you want to update order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (txt_OED_CONum.Text.Trim() == oldCONum)
                    {
                        using (SqlConnection conn = DBUtils.GetDBConnection())
                        {
                            conn.Open();

                            try
                            {
                                string sql = "UPDATE Orders_Received SET Date = @Date, Description = @Desc, Amount = @Amt, Percentage_Invoiced = @PercInv, " +
                                    "Percentage_Received = @PercRec, Quote_Number = @QNum WHERE Client_Order_Number = @CONum";

                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    decimal amt;

                                    if (isInter)
                                    {
                                        if (txt_OED_Amt.Text.Contains("$"))
                                        {
                                            if (txt_OED_Amt.Text.Replace("$", string.Empty) == "0.00")
                                            {
                                                amt = 0.00m;
                                            }
                                            else amt = Decimal.Parse(txt_OED_Amt.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                                        }
                                        else amt = 0.00m;
                                    }
                                    else
                                    {
                                        if (txt_OED_Amt.Text.Contains("R"))
                                        {
                                            if (txt_OED_Amt.Text.Replace("R", string.Empty) == "0.00")
                                            {
                                                amt = 0.00m;
                                            }
                                            else amt = Decimal.Parse(txt_OED_Amt.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                        }
                                        else amt = 0.00m;
                                    }

                                    cmd.Parameters.AddWithValue("@Date", dtp_OED_Date.Value);
                                    cmd.Parameters.AddWithValue("@Desc", txt_OED_Desc.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Amt", amt);
                                    cmd.Parameters.AddWithValue("@PercInv", pInv);
                                    cmd.Parameters.AddWithValue("@PercRec", pRec);
                                    cmd.Parameters.AddWithValue("@QNum", txt_OED_QNum.Text.Trim());
                                    cmd.Parameters.AddWithValue("@CONum", oldCONum);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Order successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (txt_OED_CONum.Text.Trim() != oldCONum)
                    {
                        using (SqlConnection conn = DBUtils.GetDBConnection())
                        {
                            conn.Open();

                            try
                            {
                                string sql = "UPDATE Orders_Received SET Client_Order_Number = @CONum, Date = @Date, Description = @Desc, Amount = @Amt, Percentage_Invoiced = @PercInv, " +
                                    "Percentage_Received = @PercRec, Quote_Number = @QNum WHERE Client_Order_Number = @oldCONum";

                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    decimal amt;

                                    if (isInter)
                                    {
                                        if (txt_OED_Amt.Text.Contains("$"))
                                        {
                                            if (txt_OED_Amt.Text.Replace("$", string.Empty) == "0.00")
                                            {
                                                amt = 0.00m;
                                            }
                                            else amt = Decimal.Parse(txt_OED_Amt.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                                        }
                                        else amt = 0.00m;
                                    }
                                    else
                                    {
                                        if (txt_OED_Amt.Text.Contains("R"))
                                        {
                                            if (txt_OED_Amt.Text.Replace("R", string.Empty) == "0.00")
                                            {
                                                amt = 0.00m;
                                            }
                                            else amt = Decimal.Parse(txt_OED_Amt.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                        }
                                        else amt = 0.00m;
                                    }

                                    cmd.Parameters.AddWithValue("@CONum", txt_OED_CONum.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Date", dtp_OED_Date.Value);
                                    cmd.Parameters.AddWithValue("@Desc", txt_OED_Desc.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Amt", amt);
                                    cmd.Parameters.AddWithValue("@PercInv", pInv);
                                    cmd.Parameters.AddWithValue("@PercRec", pRec);
                                    cmd.Parameters.AddWithValue("@QNum", txt_OED_QNum.Text.Trim());
                                    cmd.Parameters.AddWithValue("@oldCONum", oldCONum);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Order successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            else MessageBox.Show("Please enter a Client Order Number to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (!txt_OED_CONum.Focused)
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
            if (!txt_OED_Desc.Focused)
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
            if (!txt_OED_Amt.Focused)
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
            if (!txt_OED_PercInv.Focused)
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
            if (!txt_OED_PercRec.Focused)
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
            if (!txt_OED_QNum.Focused)
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
            mouseDown = true; //If user clicks on main form mouseDown is set to true
            lastLocation = e.Location; //The last location of the mouse click is captured
        }

        private void O_Edit_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                //Moves the form to a new location as long as user has mouse click down
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                //Updates the main form with the new position
                this.Update();
            }
        }

        private void O_Edit_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false; //Sets mouseDown to false when user stops clicking
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
