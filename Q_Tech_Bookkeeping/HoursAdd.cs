using Bunifu.Framework.UI;
using Microsoft.Office.Interop.Word;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class HoursAdd : Form
    {
        private DataTable dt = new DataTable();
        private Microsoft.Office.Interop.Word.Application app = null;
        private object missing = null;
        private string error = "";
        private bool isError = false;
        private bool mouseDown = false;
        private object send;
        private int SELECTED_WW;
        private Document doc;
        private Point lastLocation;

        public HoursAdd()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // LOAD FORM                                                                                                                                      //
        //================================================================================================================================================//
        private void HoursAdd_Load(object sender, EventArgs e)
        {
            Contractors curForm = (Contractors)((Home)this.Owner).GetCurForm();
            txt_HA_CCode.Text = curForm.GetCCode();
            txt_HA_Name.Text = curForm.GetCName();
            txt_HA_Surname.Text = curForm.GetCSurname();
            txt_HA_EName.Text = curForm.GetEName();
            send = curForm.GetSender();

            dt = curForm.GetHours();

            if (send is Button)
            {
                btn_HA_CreateRem.Visible = false;

                txt_HA_ExcRate.Text = "0.00000";
                txt_HA_ExcRate.SelectionStart = txt_HA_ExcRate.Text.Length;

                txt_HA_DolPH.Text = "$0.00";
                txt_HA_DolPH.SelectionStart = txt_HA_DolPH.Text.Length;

                txt_HA_TotBE.Text = "$0.00";
                txt_HA_TotBE.SelectionStart = txt_HA_TotBE.Text.Length;

                txt_HA_QTCut.Text = "R0.00";
                txt_HA_QTCut.SelectionStart = txt_HA_QTCut.Text.Length;

                txt_HA_TotAE.Text = "R0.00";
                txt_HA_TotAE.SelectionStart = txt_HA_TotAE.Text.Length;

                txt_HA_FTotal.Text = "R0.00";
                txt_HA_FTotal.SelectionStart = txt_HA_FTotal.Text.Length;

                dtp_HA_From.Value = DateTime.Now;
                dtp_HA_To.Value = dtp_HA_From.Value.AddDays(6);
                dtp_HA_DatePaid.Value = DateTime.Now;

                int newCode = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        string str = dr["Code", DataRowVersion.Original].ToString().Trim();
                        int pos = str.IndexOf("_");
                        int x = Convert.ToInt32(str.Remove(0, pos + 2));
                        if (x > newCode)
                            newCode = x;
                    }
                    else
                    {
                        string str = dr["Code"].ToString().Trim();
                        int pos = str.IndexOf("_");
                        int x = Convert.ToInt32(str.Remove(0, pos + 2));
                        if (x > newCode)
                            newCode = x;
                    }
                }

                txt_HA_Code.Text = txt_HA_CCode.Text.Split('_')[1] + "_" + (newCode++).ToString("0000");
            }
            else
            {
                Text = "Edit Work Week";
                SELECTED_WW = curForm.GetSelectedHour();
                LoadHours();
                btn_HA_CreateRem.Visible = true;
            }
        }


        //================================================================================================================================================//
        // LOAD WORK WEEK DETAILS                                                                                                                                    //
        //================================================================================================================================================//
        private void LoadHours()
        {
            txt_HA_Code.Text = dt.Rows[SELECTED_WW]["Code"].ToString().Trim();

            dtp_HA_From.Value = (dt.Rows[SELECTED_WW]["Date_Start"].ToString() == string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[SELECTED_WW]["Date_Start"].ToString());
            dtp_HA_To.Value = (dt.Rows[SELECTED_WW]["Date_End"].ToString() == string.Empty) ? dtp_HA_From.Value.AddDays(6.0) : Convert.ToDateTime(dt.Rows[SELECTED_WW]["Date_End"].ToString());

            txt_HA_HW.Text = dt.Rows[SELECTED_WW]["Hours"].ToString().Trim();

            if (dt.Rows[SELECTED_WW]["Rate_Per_Hour"].ToString() != string.Empty)
                txt_HA_DolPH.Text = Convert.ToDecimal(dt.Rows[SELECTED_WW]["Rate_Per_Hour"].ToString().Trim()).ToString("c", CultureInfo.GetCultureInfo("en-US"));
            else
                txt_HA_DolPH.Text = "$0.00";

            if (dt.Rows[SELECTED_WW]["Total_$"].ToString() != string.Empty)
                txt_HA_TotBE.Text = Convert.ToDecimal(dt.Rows[SELECTED_WW]["Total_$"].ToString().Trim()).ToString("c", CultureInfo.GetCultureInfo("en-US"));
            else
                txt_HA_TotBE.Text = "$0.00";

            if (dt.Rows[SELECTED_WW]["Exchange_Rate"].ToString() != string.Empty)
                txt_HA_ExcRate.Text = Convert.ToDecimal(dt.Rows[SELECTED_WW]["Exchange_Rate"].ToString().Trim()).ToString();
            else
                txt_HA_ExcRate.Text = "0.00000";

            if (dt.Rows[SELECTED_WW]["Total_R"].ToString() != string.Empty)
                txt_HA_TotAE.Text = Convert.ToDecimal(dt.Rows[SELECTED_WW]["Total_R"].ToString().Trim()).ToString("c");
            else
                txt_HA_TotAE.Text = "R0.00";

            if (dt.Rows[SELECTED_WW]["QTech_Cut"].ToString() != string.Empty)
                txt_HA_QTCut.Text = Convert.ToDecimal(dt.Rows[SELECTED_WW]["QTech_Cut"].ToString().Trim()).ToString("c");
            else
                txt_HA_QTCut.Text = "R0.00";

            if (dt.Rows[SELECTED_WW]["Final_Total"].ToString() != string.Empty)
                txt_HA_FTotal.Text = Convert.ToDecimal(dt.Rows[SELECTED_WW]["Final_Total"].ToString().Trim()).ToString("c");
            else
                txt_HA_FTotal.Text = "R0.00";

            if (dt.Rows[SELECTED_WW]["Remittance"].ToString() == "Yes")
                btn_HA_CreateRem.Enabled = false;

            if (dt.Rows[SELECTED_WW]["Paid"].ToString() == "Yes")
            {
                cb_HA_Paid.Checked = true;
                dtp_HA_DatePaid.Enabled = true;
                dtp_HA_DatePaid.Value = !(dt.Rows[SELECTED_WW]["Date_Paid"].ToString() != string.Empty) ? DateTime.Now : Convert.ToDateTime(dt.Rows[SELECTED_WW]["Date_Paid"].ToString());
            }
        }


        //================================================================================================================================================//
        // BUTTON DONE CLICKED                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_HA_Done_Click(object sender, EventArgs e)
        {
            string code = txt_HA_Code.Text;

            if (send is Button)
            {
                StringBuilder sb = new StringBuilder().Append("Are you sure you want to add work week with Code: ").Append(code).Append("?");

                if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection conn = DBUtils.GetDBConnection())
                    {
                        conn.Open();
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Contractor_Hours VALUES (@Code, @Date_Start, @Date_End, @Hours, @RPHour, @TotBE, @ERate, @TotAE, @QTCut, @FTot, @Rem, @Inv, @Paid, @DPaid, @CCode)", conn))
                            {
                                decimal excRate;

                                if (txt_HA_ExcRate.Text == "0.00000")
                                    excRate = 0.00000m;
                                else excRate = Decimal.Parse(txt_HA_ExcRate.Text);

                                decimal perHour;
                                if (txt_HA_DolPH.Text.Contains("$"))
                                {
                                    if (txt_HA_DolPH.Text.Replace("$", string.Empty) == "0.00")
                                        perHour = 0.00m;
                                    else perHour = Decimal.Parse(txt_HA_DolPH.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                                }
                                else perHour = 0.00m;

                                decimal totBE;
                                if (txt_HA_TotBE.Text.Replace("$", string.Empty) == "0.00")
                                    totBE = 0.00m;
                                else totBE = Decimal.Parse(txt_HA_TotBE.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));

                                decimal qtCut;
                                if (txt_HA_QTCut.Text.Contains("R"))
                                {
                                    if (txt_HA_QTCut.Text.Replace("R", string.Empty) == "0.00")
                                        qtCut = 0.00m;
                                    else qtCut = Decimal.Parse(txt_HA_QTCut.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                                }
                                else qtCut = 0.00m;

                                decimal totAE;
                                if (txt_HA_TotAE.Text.Replace("R", string.Empty) == "0.00")
                                    totAE = 0.00m;
                                else totAE = Decimal.Parse(txt_HA_TotAE.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));

                                decimal fTot;
                                if (txt_HA_FTotal.Text.Replace("R", string.Empty) == "0.00")
                                    fTot = 0.00m;
                                else fTot = Decimal.Parse(txt_HA_FTotal.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));

                                decimal hours;
                                if (txt_HA_HW.Text == string.Empty)
                                    hours = 0.00m;
                                else if (txt_HA_HW.Text.Contains("."))
                                    hours = Decimal.Parse(txt_HA_HW.Text.Replace(".", ","));
                                else hours = Decimal.Parse(txt_HA_HW.Text);

                                cmd.Parameters.AddWithValue("@Code", txt_HA_Code.Text.Trim());
                                cmd.Parameters.AddWithValue("@Date_Start", dtp_HA_From.Value);
                                cmd.Parameters.AddWithValue("@Date_End", dtp_HA_To.Value);
                                cmd.Parameters.AddWithValue("@Hours", hours);
                                cmd.Parameters.AddWithValue("@RPHour", perHour);
                                cmd.Parameters.AddWithValue("@TotBE", totBE);
                                cmd.Parameters.AddWithValue("@ERate", excRate);
                                cmd.Parameters.AddWithValue("@TotAE", totAE);
                                cmd.Parameters.AddWithValue("@QTCut", qtCut);
                                cmd.Parameters.AddWithValue("@FTot", fTot);
                                cmd.Parameters.AddWithValue("@Rem", "No");
                                cmd.Parameters.AddWithValue("@Inv", "No");

                                if (cb_HA_Paid.Checked)
                                {
                                    cmd.Parameters.AddWithValue("@Paid", "Yes");
                                    cmd.Parameters.AddWithValue("@DPaid", dtp_HA_DatePaid.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Paid", "No");
                                    cmd.Parameters.AddWithValue("@DPaid", DBNull.Value);
                                }

                                cmd.Parameters.AddWithValue("@CCode", txt_HA_CCode.Text.Trim());
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("New work week successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                StringBuilder sb = new StringBuilder().Append("Are you sure you want to update work week with Code: ").Append(code).Append("?");

                if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    DoUpdate();
            }
        }

        private void DoUpdate()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Contractor_Hours SET Date_Start = @DateS, Date_End = @DateE, Hours = @Hours, Rate_Per_Hour = @RPH, Total_$ = @TotBE, Exchange_Rate = @ER, Total_R = @TotAE, QTech_Cut = @QTC, Final_Total = @FTot, Paid = @P, Date_Paid = @DP WHERE Code = @Code", conn))
                    {
                        decimal excRate;

                        if (txt_HA_ExcRate.Text == "0,00000" || txt_HA_ExcRate.Text == "0.00000")
                            excRate = 0.00000m;
                        else excRate = Decimal.Parse(txt_HA_ExcRate.Text.Replace(".", ","), CultureInfo.GetCultureInfo("en-ZA"));

                        decimal perHour;
                        if (txt_HA_DolPH.Text.Contains("$"))
                        {
                            if (txt_HA_DolPH.Text.Replace("$", string.Empty) == "0.00")
                                perHour = 0.00m;
                            else perHour = Decimal.Parse(txt_HA_DolPH.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                        }
                        else perHour = 0.00m;

                        decimal totBE;
                        if (txt_HA_TotBE.Text.Replace("$", string.Empty) == "0.00")
                            totBE = 0.00m;
                        else totBE = Decimal.Parse(txt_HA_TotBE.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));

                        decimal qtCut;
                        if (txt_HA_QTCut.Text.Contains("R"))
                        {
                            if (txt_HA_QTCut.Text.Replace("R", string.Empty) == "0,00" || txt_HA_QTCut.Text.Replace("R", string.Empty) == "0.00")
                                qtCut = 0.00m;
                            else qtCut = Decimal.Parse(txt_HA_QTCut.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                        }
                        else qtCut = 0.00m;

                        decimal totAE;
                        if (txt_HA_TotAE.Text.Replace("R", string.Empty) == "0,00" || txt_HA_TotAE.Text.Replace("R", string.Empty) == "0.00")
                            totAE = 0.00m;
                        else totAE = Decimal.Parse(txt_HA_TotAE.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));

                        decimal fTot;
                        if (txt_HA_FTotal.Text.Replace("R", string.Empty) == "0,00" || txt_HA_FTotal.Text.Replace("R", string.Empty) == "0.00")
                            fTot = 0.00m;
                        else fTot = Decimal.Parse(txt_HA_FTotal.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));

                        decimal hours;
                        if (txt_HA_HW.Text == string.Empty)
                            hours = 0.00m;
                        else if (txt_HA_HW.Text.Contains("."))
                            hours = Decimal.Parse(txt_HA_HW.Text.Replace(".", ","));
                        else hours = Decimal.Parse(txt_HA_HW.Text);

                        cmd.Parameters.AddWithValue("@Code", txt_HA_Code.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateS", dtp_HA_From.Value);
                        cmd.Parameters.AddWithValue("@DateE", dtp_HA_To.Value);
                        cmd.Parameters.AddWithValue("@Hours", hours);
                        cmd.Parameters.AddWithValue("@RPH", perHour);
                        cmd.Parameters.AddWithValue("@TotBE", totBE);
                        cmd.Parameters.AddWithValue("@ER", excRate);
                        cmd.Parameters.AddWithValue("@TotAE", totAE);
                        cmd.Parameters.AddWithValue("@QTC", qtCut);
                        cmd.Parameters.AddWithValue("@FTot", fTot);

                        if (cb_HA_Paid.Checked)
                        {
                            cmd.Parameters.AddWithValue("@P", "Yes");
                            cmd.Parameters.AddWithValue("@DP", dtp_HA_DatePaid.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@P", "No");
                            cmd.Parameters.AddWithValue("@DP", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Work week successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Btn_HA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_HA_PerHour_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_HA_DolPH.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= new Decimal(100);

                txt_HA_DolPH.TextChanged -= Txt_HA_PerHour_TextChanged;
                txt_HA_DolPH.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", result);
                txt_HA_DolPH.TextChanged += Txt_HA_PerHour_TextChanged;
                txt_HA_DolPH.Select(txt_HA_DolPH.Text.Length, 0);
            }

            if (!TextisValid(txt_HA_DolPH.Text))
            {
                txt_HA_DolPH.Text = "$0.00";
                txt_HA_DolPH.Select(txt_HA_DolPH.Text.Length, 0);
            }
        }

        private void Txt_HA_TotalBE_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_HA_TotBE.Text.Replace(",", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= new Decimal(100);

                txt_HA_TotBE.TextChanged -= Txt_HA_TotalBE_TextChanged;
                txt_HA_TotBE.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", result);
                txt_HA_TotBE.TextChanged += Txt_HA_TotalBE_TextChanged;
                txt_HA_TotBE.Select(txt_HA_TotBE.Text.Length, 0);
            }


            if (!TextisValid(txt_HA_TotBE.Text))
            {
                txt_HA_TotBE.Text = "$0.00";
                txt_HA_TotBE.Select(txt_HA_TotBE.Text.Length, 0);
            }
        }

        private void Txt_HA_QTCut_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_HA_QTCut.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /=new Decimal(100);
                txt_HA_QTCut.TextChanged -= Txt_HA_QTCut_TextChanged;
                txt_HA_QTCut.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                txt_HA_QTCut.TextChanged += Txt_HA_QTCut_TextChanged;
                txt_HA_QTCut.Select(txt_HA_QTCut.Text.Length, 0);
            }

            if (!TextisValid(txt_HA_QTCut.Text))
            {
                txt_HA_QTCut.Text = "R0.00";
                txt_HA_QTCut.Select(txt_HA_QTCut.Text.Length, 0);
            }            
        }

        private void Txt_HA_TotalAE_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_HA_TotAE.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= new Decimal(100);
                txt_HA_TotAE.TextChanged -= Txt_HA_TotalAE_TextChanged;
                txt_HA_TotAE.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                txt_HA_TotAE.TextChanged += Txt_HA_TotalAE_TextChanged;
                txt_HA_TotAE.Select(txt_HA_TotAE.Text.Length, 0);
            }

            if (!TextisValid(txt_HA_TotAE.Text))
            {
                txt_HA_TotAE.Text = "R0.00";
                txt_HA_TotAE.Select(txt_HA_TotAE.Text.Length, 0);
            }
        }

        private void Txt_HA_FTotal_TextChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txt_HA_FTotal.Text.Replace(",", string.Empty).Replace("R", string.Empty).Replace(".", string.Empty).TrimStart('0'), out decimal result))
            {
                result /= new Decimal(100);
                txt_HA_FTotal.TextChanged -= Txt_HA_FTotal_TextChanged;
                txt_HA_FTotal.Text = string.Format(CultureInfo.CreateSpecificCulture("en-ZA"), "{0:C2}", result);
                txt_HA_FTotal.TextChanged += Txt_HA_FTotal_TextChanged;
                txt_HA_FTotal.Select(txt_HA_FTotal.Text.Length, 0);
            }

            if (!TextisValid(txt_HA_FTotal.Text))
            {
                txt_HA_FTotal.Text = "R0.00";
                txt_HA_FTotal.Select(txt_HA_FTotal.Text.Length, 0);
            }
        }

        private bool TextisValid(string text)
        {
            return new Regex("[^0-9]").IsMatch(text);
        }


        //================================================================================================================================================//
        // CALCULATE TOTALS                                                                                                                               //
        //================================================================================================================================================//
        private void Txt_HA_HW_Leave(object sender, EventArgs e)
        {
            ln_HA_HW.LineColor = Color.Gray;
            CalculateTotBE();
        }

        private void Txt_HA_DolPH_Leave(object sender, EventArgs e)
        {
            ln_HA_DolPH.LineColor = Color.Gray;
            CalculateTotBE();
        }

        private void Txt_HA_ExcRate_Leave(object sender, EventArgs e)
        {
            ln_HA_ExcRate.LineColor = Color.Gray;
            CalculateTotAE();
        }

        private void Txt_HA_QTCut_Leave(object sender, EventArgs e)
        {
            ln_HA_TotAE.LineColor = Color.Gray;
            CalculateFinalTot();
        }

        private void CalculateTotBE()
        {
            if (txt_HA_HW.Text != string.Empty)
            {
                decimal hours;

                if (txt_HA_HW.Text.Contains("."))
                    hours = Decimal.Parse(txt_HA_HW.Text.Replace(".", ","), CultureInfo.GetCultureInfo("en-ZA"));
                else hours = Decimal.Parse(txt_HA_HW.Text);

                decimal perHour;
                if (txt_HA_DolPH.Text.Contains("$"))
                    perHour = Decimal.Parse(txt_HA_DolPH.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));
                else perHour = 0.00m;

                decimal tot = hours * perHour;

                txt_HA_TotBE.Text = tot.ToString("c", CultureInfo.GetCultureInfo("en-US"));
            }
        }

        private void CalculateTotAE()
        {
            decimal excRate;

            if (txt_HA_ExcRate.Text.Contains("."))
                excRate = Decimal.Parse(txt_HA_ExcRate.Text.Replace(".", ","), CultureInfo.GetCultureInfo("en-ZA"));
            else
                excRate = Decimal.Parse(txt_HA_ExcRate.Text);

            decimal totBE = Decimal.Parse(txt_HA_TotBE.Text.Replace("$", string.Empty), CultureInfo.GetCultureInfo("en-US"));

            decimal tot = totBE * excRate;

            txt_HA_TotAE.Text = tot.ToString("c");
        }

        private void CalculateFinalTot()
        {
            decimal totAE = Decimal.Parse(txt_HA_TotAE.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));

            decimal qtCut;

            if (txt_HA_QTCut.Text.Contains("."))
            {
                if (txt_HA_QTCut.Text.Contains("R"))
                    qtCut = Decimal.Parse(txt_HA_QTCut.Text.Replace("R", string.Empty).Replace(".", ","), CultureInfo.GetCultureInfo("en-ZA"));
                else qtCut = 0.00m;
            }
            else
            {
                if (txt_HA_QTCut.Text.Contains("R"))
                    qtCut = Decimal.Parse(txt_HA_QTCut.Text.Replace("R", string.Empty), CultureInfo.GetCultureInfo("en-ZA"));
                else qtCut = 0.00m;
            }

            decimal tot = totAE - qtCut;

            txt_HA_FTotal.Text = tot.ToString("c");
        }


        //================================================================================================================================================//
        // PAID CHECKBOX CHANGE                                                                                                                           //
        //================================================================================================================================================//
        private void Cb_HA_Paid_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_HA_Paid.Checked)
                dtp_HA_DatePaid.Enabled = true;
            else
                dtp_HA_DatePaid.Enabled = false;
        }


        //================================================================================================================================================//
        // GENERATE REMITTANCE DOCUMENT                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_HA_CRem_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                StringBuilder sb = new StringBuilder().Append("Are you sure you want to create remittance document for work week: ")
                    .Append(txt_HA_Code.Text.Trim()).Append("?");

                if (MessageBox.Show(sb.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btn_HA_Done.Enabled = false;
                    btn_HA_Cancel.Enabled = false;
                    btn_HA_CreateRem.Enabled = false;

                    pb_CreateRem.Visible = true;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                app = new Microsoft.Office.Interop.Word.Application();
                doc = null;

                backgroundWorker1.ReportProgress(10);

                object fileName = @"\\192.168.8.121\Contractors\Remittances\Remittance_Template.docx";
                missing = Type.Missing;

                backgroundWorker1.ReportProgress(20);

                doc = app.Documents.Open(fileName, missing, missing);
                app.Selection.Find.ClearFormatting();
                app.Selection.Find.Replacement.ClearFormatting();

                backgroundWorker1.ReportProgress(30);

                app.Selection.Find.Execute("<code>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_Code.Text.Trim(), 2);
                app.Selection.Find.Execute("<name>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_Name.Text.Trim(), 2);
                app.Selection.Find.Execute("<surname>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_Surname.Text.Trim(), 2);

                backgroundWorker1.ReportProgress(40);

                app.Selection.Find.Execute("<date>", missing, missing, missing, missing, missing, missing, missing, missing, DateTime.Now.ToShortDateString(), 2);
                app.Selection.Find.Execute("<desc>", missing, missing, missing, missing, missing, missing, missing, missing, "Week ending " + dtp_HA_To.Value.ToLongDateString(), 2);
                app.Selection.Find.Execute("<dolvalue>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_TotBE.Text.Trim(), 2);
                app.Selection.Find.Execute("<excrate>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_ExcRate.Text.Trim(), 2);

                backgroundWorker1.ReportProgress(50);

                app.Selection.Find.Execute("<total>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_TotAE.Text.Trim(), 2);
                app.Selection.Find.Execute("<subtotal>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_TotAE.Text.Trim(), 2);
                app.Selection.Find.Execute("<grandtotal>", missing, missing, missing, missing, missing, missing, missing, missing, txt_HA_TotAE.Text.Trim(), 2);

                backgroundWorker1.ReportProgress(60);

                object SaveAsFile = (object)@"\\192.168.8.121\Contractors\Remittances\Remittance_" + txt_HA_Code.Text.Trim() + ".docx";
                doc.SaveAs2(SaveAsFile, missing, missing, missing);

                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    backgroundWorker1.ReportProgress(70);

                    conn.Open();

                    string sql = "UPDATE Contractor_Hours SET Remittance=@Rem WHERE Code=@Code AND Contractor_Code=@CCode";

                    backgroundWorker1.ReportProgress(80);

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rem", "Yes");
                        cmd.Parameters.AddWithValue("@Code", txt_HA_Code.Text.Trim());

                        backgroundWorker1.ReportProgress(90);

                        cmd.Parameters.AddWithValue("@CCode", txt_HA_CCode.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }

                backgroundWorker1.ReportProgress(100);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                isError = true;
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pb_CreateRem.Value = e.ProgressPercentage;
            pb_CreateRem.Update();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!isError)
            {
                MessageBox.Show("Remittance document successfully created", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            doc.Close(false, missing, missing);
            app.Quit(false, false, false);
            Marshal.ReleaseComObject(app);

            DoUpdate();
        }

        private void Txt_HA_Code_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_Code.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_Code_Leave(object sender, EventArgs e)
        {
            ln_HA_Code.LineColor = Color.Gray;
        }

        private void Txt_HA_Code_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_HA_Code.Focused)
                ln_HA_Code.LineColor = Color.Gray;
        }

        private void Txt_HA_HW_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_HW.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_HW_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_HA_HW.Focused)
                ln_HA_HW.LineColor = Color.Gray;
        }

        private void Txt_HA_DolPH_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_DolPH.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_DolPH_MouseLeave(object sender, EventArgs e)
        {
            if (txt_HA_DolPH.Focused)
                ln_HA_DolPH.LineColor = Color.Gray;
        }

        private void Txt_HA_TotBE_Leave(object sender, EventArgs e)
        {
            ln_HA_TotBE.LineColor = Color.Gray;
        }

        private void Txt_HA_TotBE_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_TotBE.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_TotBE_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_HA_ExcRate.Focused)
                ln_HA_ExcRate.LineColor = Color.Gray;
        }

        private void Txt_HA_ExcRate_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_ExcRate.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_ExcRate_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_HA_ExcRate.Focused)
                ln_HA_ExcRate.LineColor = Color.Gray;
        }   

        private void Txt_HA_TotAE_Leave(object sender, EventArgs e)
        {
            ln_HA_TotAE.LineColor = Color.Gray;
        }

        private void Txt_HA_TotAE_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_TotAE.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_TotAE_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_HA_TotAE.Focused)
                ln_HA_TotAE.LineColor = Color.Gray;
        }

        private void Txt_HA_QTCut_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_QTCut.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_QTCut_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_HA_QTCut.Focused)
                ln_HA_QTCut.LineColor = Color.Gray;
        }

        private void Txt_HA_FTotal_Leave(object sender, EventArgs e)
        {
            ln_HA_FTotal.LineColor = Color.Gray;
        }

        private void Txt_HA_FTotal_MouseEnter(object sender, EventArgs e)
        {
            ln_HA_FTotal.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_HA_FTotal_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_HA_FTotal.Focused)
                ln_HA_FTotal.LineColor = Color.Gray;
        }

        private void Btn_HA_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_HA_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_HA_Close.Image = (Image)Resources.close_white;
        }

        private void Btn_HA_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_HA_Close.Image = (Image)Resources.close_black;
        }

        private void Btn_HA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_HA_Done.ForeColor = Color.White;
        }

        private void Btn_HA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_HA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_HA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_HA_Cancel.ForeColor = Color.White;
        }

        private void Btn_HA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_HA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_HA_CreateRem_MouseEnter(object sender, EventArgs e)
        {
            btn_HA_CreateRem.ForeColor = Color.White;
        }

        private void Btn_HA_CreateRem_MouseLeave(object sender, EventArgs e)
        {
            btn_HA_CreateRem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void O_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void O_Add_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(this.Location.X - (lastLocation.X + e.X), this.Location.Y - (lastLocation.Y + e.Y));
                this.Update();
            }
        }

        private void O_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
