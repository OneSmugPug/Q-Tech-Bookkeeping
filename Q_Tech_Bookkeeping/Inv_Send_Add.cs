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
    public partial class Inv_Send_Add : Form
    {
        private DataTable dt = new DataTable();
        private bool isInter = false, mouseDown = false;
        private Invoices_Send parent = null;
        private Int_Invoices_Send intParent = null;
        private Point lastLocation;

        public Inv_Send_Add()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // INVOICES SENT ADD FORM LOAD                                                                                                                    //
        //================================================================================================================================================//
        private void Inv_Send_Add_Load(object sender, EventArgs e)
        {
            Home frmHome = (Home)this.Owner;

            if (frmHome.GetCurPanel() == "pnl_L_InvSent")
            {
                parent = (Invoices_Send)frmHome.GetCurForm();

                txt_ISA_CCode.Text = parent.GetCCode();
                txt_ISA_CName.Text = parent.GetCName();

                txt_ISA_Amt.Text = "R0.00";
                txt_ISA_Amt.SelectionStart = txt_ISA_Amt.Text.Length;

                txt_ISA_VAT.Text = "R0.00";
                txt_ISA_VAT.SelectionStart = txt_ISA_VAT.Text.Length;

                dt = parent.GetInvoices();
            }
            else
            {
                isInter = true;
                intParent = (Int_Invoices_Send)frmHome.GetCurForm();

                txt_ISA_CCode.Text = intParent.GetCCode();
                txt_ISA_CName.Text = intParent.GetCName();

                txt_ISA_Amt.Text = "$0.00";
                txt_ISA_Amt.SelectionStart = txt_ISA_Amt.Text.Length;

                txt_ISA_VAT.Text = "$0.00";
                txt_ISA_VAT.SelectionStart = txt_ISA_VAT.Text.Length;

                dt = intParent.GetInvoices();
            }

            int newInvNum = 0;
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    string curInvNum = row["Invoice_Number", DataRowVersion.Original].ToString().Trim();

                    if (!curInvNum.Contains("."))
                    {
                        int pos = curInvNum.IndexOf("_");
                        int int32 = Convert.ToInt32(curInvNum.Remove(0, pos + 2));
                        if (int32 > newInvNum)
                            newInvNum = int32;
                    }
                    else
                    {
                        int pos = curInvNum.IndexOf("_");
                        curInvNum = curInvNum.Remove(0, pos + 2);

                        pos = curInvNum.IndexOf(".");
                        int int32 = Convert.ToInt32(curInvNum.Remove(pos, curInvNum.Length - 3));
                        if (int32 > newInvNum)
                            newInvNum = int32;
                    }
                }
                else
                {
                    string curInvNum = row["Invoice_Number"].ToString().Trim();

                    if (!curInvNum.Contains("."))
                    {
                        int pos = curInvNum.IndexOf("_");
                        int int32 = Convert.ToInt32(curInvNum.Remove(0, pos + 2));
                        if (int32 > newInvNum)
                            newInvNum = int32;
                    }
                    else
                    {
                        int pos = curInvNum.IndexOf("_");
                        curInvNum = curInvNum.Remove(0, pos + 2);

                        pos = curInvNum.IndexOf(".");
                        int int32 = Convert.ToInt32(curInvNum.Remove(pos, curInvNum.Length - 3));
                        if (int32 > newInvNum)
                            newInvNum = int32;
                    }
                }
            }

            txt_ISA_InvNum.Text = txt_ISA_CCode.Text + "_I" + (newInvNum + 1).ToString("000");

            dtp_ISA_Date.Value = DateTime.Now;
            dtp_ISA_DatePaid.Value = DateTime.Now;
        }


        //================================================================================================================================================//
        // AMOUNT TEXT CHANGED                                                                                                                            //
        //================================================================================================================================================//
        private void Txt_ISA_Amt_TextChanged(object sender, EventArgs e)
        {
            if (!isInter)
            {
                if (Decimal.TryParse(txt_ISA_Amt.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISA_Amt.TextChanged -= Txt_ISA_Amt_TextChanged;

                    txt_ISA_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", ul);
                    txt_ISA_Amt.TextChanged += Txt_ISA_Amt_TextChanged;
                    txt_ISA_Amt.Select(txt_ISA_Amt.Text.Length, 0);
                }

                if (!TextisValid(txt_ISA_Amt.Text))
                {
                    txt_ISA_Amt.Text = "R0.00";
                    txt_ISA_Amt.Select(txt_ISA_Amt.Text.Length, 0);
                }
            }
            else
            {
                if (Decimal.TryParse(txt_ISA_Amt.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISA_Amt.TextChanged -= Txt_ISA_Amt_TextChanged;

                    txt_ISA_Amt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                    txt_ISA_Amt.TextChanged += Txt_ISA_Amt_TextChanged;
                    txt_ISA_Amt.Select(txt_ISA_Amt.Text.Length, 0);
                }

                if (!TextisValid(txt_ISA_Amt.Text))
                {
                    txt_ISA_Amt.Text = "$0.00";
                    txt_ISA_Amt.Select(txt_ISA_Amt.Text.Length, 0);
                }
            }
        }


        //================================================================================================================================================//
        // IS TEXT VALID                                                                                                                                  //
        //================================================================================================================================================//
        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }


        //================================================================================================================================================//
        // VAT TEXT CHANGED                                                                                                                               //
        //================================================================================================================================================//
        private void Txt_ISA_VAT_TextChanged(object sender, EventArgs e)
        {
            if (!isInter)
            {
                if (Decimal.TryParse(txt_ISA_VAT.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISA_VAT.TextChanged -= Txt_ISA_VAT_TextChanged;

                    txt_ISA_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", ul);
                    txt_ISA_VAT.TextChanged += Txt_ISA_VAT_TextChanged;
                    txt_ISA_VAT.Select(txt_ISA_VAT.Text.Length, 0);
                }

                if (!TextisValid(txt_ISA_VAT.Text))
                {
                    txt_ISA_VAT.Text = "R0.00";
                    txt_ISA_VAT.Select(txt_ISA_VAT.Text.Length, 0);
                }
            }
            else
            {
                if (Decimal.TryParse(txt_ISA_VAT.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal ul))
                {
                    ul /= 100;

                    txt_ISA_VAT.TextChanged -= Txt_ISA_VAT_TextChanged;

                    txt_ISA_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                    txt_ISA_VAT.TextChanged += Txt_ISA_VAT_TextChanged;
                    txt_ISA_VAT.Select(txt_ISA_VAT.Text.Length, 0);
                }
                if (!TextisValid(txt_ISA_VAT.Text))
                {
                    txt_ISA_VAT.Text = "$0.00";
                    txt_ISA_VAT.Select(txt_ISA_VAT.Text.Length, 0);
                }
            }
        }


        //================================================================================================================================================//
        // AMOUNT TEXTBOX LEAVE                                                                                                                           //
        //================================================================================================================================================//
        private void Txt_ISA_Amt_Leave(object sender, EventArgs e)
        {
            ln_ISA_Amt.LineColor = Color.Gray;

            if (!isInter)
            {
                string value = txt_ISA_Amt.Text.Replace("R", string.Empty);

                decimal ul;

                if (decimal.TryParse(value, out ul))
                {
                    ul *= 0.15m;

                    txt_ISA_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", ul);
                }
            }
            else
            {
                string value = txt_ISA_Amt.Text.Replace("$", string.Empty);

                decimal ul = decimal.Parse(value, CultureInfo.GetCultureInfo("en-US"));

                ul *= 0.15m;

                txt_ISA_VAT.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
            }
        }


        //================================================================================================================================================//
        // DONE CLICKED                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_ISA_Done_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add invoice with Invoice Number: " + txt_ISA_InvNum.Text + "?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Invoices_Send VALUES (@Date, @InvNum, @Client, @Desc, @Amt, @VAT, @Paid, @DatePaid)", conn))
                        {
                            decimal amt;
                            decimal VAT;

                            if (!isInter)
                            {
                                if (txt_ISA_Amt.Text.Contains("R"))
                                {
                                    if (txt_ISA_Amt.Text.Replace("R", string.Empty) == "0.00")
                                    {
                                        amt = 0.00m;
                                    }
                                    else amt = Decimal.Parse(txt_ISA_Amt.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                }
                                else amt = 0.00m;


                                if (txt_ISA_VAT.Text.Contains("R"))
                                {
                                    if (txt_ISA_VAT.Text.Replace("R", string.Empty) == "0.00")
                                    {
                                        VAT = 0.00m;
                                    }
                                    else VAT = Decimal.Parse(txt_ISA_VAT.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                }
                                else VAT = 0.00m;
                            }
                            else
                            {
                                if (txt_ISA_Amt.Text.Contains("$"))
                                {
                                    if (txt_ISA_Amt.Text.Replace("$", string.Empty) == "0.00")
                                    {
                                        amt = 0.00m;
                                    }
                                    else amt = Decimal.Parse(txt_ISA_Amt.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                                }
                                else amt = 0.00m;


                                if (txt_ISA_VAT.Text.Contains("$"))
                                {
                                    if (txt_ISA_VAT.Text.Replace("$", string.Empty) == "0.00")
                                    {
                                        VAT = 0.00m;
                                    }
                                    else VAT = Decimal.Parse(txt_ISA_VAT.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                                }
                                else VAT = 0.00m;
                            }

                            cmd.Parameters.AddWithValue("@Date", dtp_ISA_Date.Value);
                            cmd.Parameters.AddWithValue("@InvNum", txt_ISA_InvNum.Text.Trim());
                            cmd.Parameters.AddWithValue("@Client", txt_ISA_CName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Desc", txt_ISA_Desc.Text.Trim());
                            cmd.Parameters.AddWithValue("@Amt", amt);
                            cmd.Parameters.AddWithValue("@VAT", VAT);

                            if (cb_ISA_Paid.Checked)
                            {
                                cmd.Parameters.AddWithValue("@Paid", "Yes");
                                cmd.Parameters.AddWithValue("@DatePaid", dtp_ISA_DatePaid.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Paid", "No");
                                cmd.Parameters.AddWithValue("@DatePaid", DBNull.Value);
                            }

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("New invoice successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (!isInter)
                                parent.setNewInvoice(txt_ISA_InvNum.Text);
                            else intParent.SetNewInvoice(txt_ISA_InvNum.Text);

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
        // CANCEL CLICKED                                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_ISA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // INVOICE NUMBER                                                                                                                                 //
        //================================================================================================================================================//
        private void Txt_ISA_InvNum_MouseEnter(object sender, EventArgs e)
        {
            ln_ISA_InvNum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISA_InvNum_Leave(object sender, EventArgs e)
        {
            ln_ISA_InvNum.LineColor = Color.Gray;
        }

        private void Txt_ISA_InvNum_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISA_InvNum.Focused)
                ln_ISA_InvNum.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // INVOICE NUMBER INSTANCE                                                                                                                        //
        //================================================================================================================================================//
        private void Txt_ISA_INInst_MouseEnter(object sender, EventArgs e)
        {
            ln_ISA_INInst.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISA_INInst_Leave(object sender, EventArgs e)
        {
            ln_ISA_INInst.LineColor = Color.Gray;
        }

        private void Txt_ISA_INInst_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISA_INInst.Focused)
                ln_ISA_INInst.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // DESCRIPTION                                                                                                                                    //
        //================================================================================================================================================//
        private void Txt_ISA_Desc_Leave(object sender, EventArgs e)
        {
            ln_ISA_Desc.LineColor = Color.Gray;
        }

        private void Txt_ISA_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_ISA_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISA_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISA_Desc.Focused)
                ln_ISA_Desc.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // AMOUNT                                                                                                                                         //
        //================================================================================================================================================//
        private void Txt_ISA_Amt_MouseEnter(object sender, EventArgs e)
        {
            ln_ISA_Amt.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISA_Amt_MouseLeave(object sender, EventArgs e)
        {

        }


        //================================================================================================================================================//
        // VAT                                                                                                                                            //
        //================================================================================================================================================//
        private void Txt_ISA_VAT_Leave(object sender, EventArgs e)
        {
            ln_ISA_VAT.LineColor = Color.Gray;
        }

        private void Txt_ISA_VAT_MouseEnter(object sender, EventArgs e)
        {
            ln_ISA_VAT.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_ISA_VAT_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_ISA_VAT.Focused)
                ln_ISA_VAT.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // CLOSE CLICKED                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_ISA_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_ISA_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_ISA_Close.Image = (Image)Resources.close_white;
        }

        private void Btn_ISA_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_ISA_Close.Image = (Image)Resources.close_black;
        }


        //================================================================================================================================================//
        // DONE BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_ISA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_ISA_Done.ForeColor = Color.White;
        }

        private void Btn_ISA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_ISA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_ISA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_ISA_Cancel.ForeColor = Color.White;
        }

        private void Btn_ISA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_ISA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // ENFORCE READONLY                                                                                                                               //
        //================================================================================================================================================//
        private void Txt_ISA_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_ISA_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }


        //================================================================================================================================================//
        // INVOICES SENT ADD                                                                                                                              //
        //================================================================================================================================================//
        private void Inv_Send_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Inv_Send_Add_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(this.Location.X - (lastLocation.X + e.X), this.Location.Y - (lastLocation.Y + e.Y));
                this.Update();
            }
        }

        private void Inv_Sent_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }


        //================================================================================================================================================//
        // PAID CHECKBOX CHANGE                                                                                                                           //
        //================================================================================================================================================//
        private void Bb_ISA_Paid_OnChange(object sender, EventArgs e)
        {
            if (cb_ISA_Paid.Checked)
                dtp_ISA_DatePaid.Enabled = true;
            else
                dtp_ISA_DatePaid.Enabled = false;
        }
    }
}
