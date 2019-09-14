using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class Q_Add : Form
    {
        private DataTable dt = (DataTable)null;
        private bool mouseDown = false;
        private Point lastLocation;

        public Q_Add()
        {
            InitializeComponent();
        }

        private void Q_Add_Load(object sender, EventArgs e)
        {
            Home owner = (Home)Owner;
            if (owner.GetCurPanel() == "pnl_L_Quotes")
            {
                Quotes curForm = (Quotes)owner.GetCurForm();
                txt_QA_CCode.Text = curForm.GetCCode();
                txt_QA_CName.Text = curForm.GetCName();
                dt = curForm.GetQuotes();
            }
            else
            {
                Int_Quotes curForm = (Int_Quotes)owner.GetCurForm();
                txt_QA_CCode.Text = curForm.GetCCode();
                txt_QA_CName.Text = curForm.GetCName();
                dt = curForm.GetQuotes();
            }
            int num1 = 0;
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    string str = row["Quote_Number", DataRowVersion.Original].ToString().Trim();
                    int num2 = str.IndexOf("_");
                    int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
                    if (int32 > num1)
                        num1 = int32;
                }
                else
                {
                    string str = row["Quote_Number"].ToString().Trim();
                    int num2 = str.IndexOf("_");
                    int int32 = Convert.ToInt32(str.Remove(0, num2 + 2));
                    if (int32 > num1)
                        num1 = int32;
                }
            }
            txt_QA_QNum.Text = txt_QA_CCode.Text + "_Q" + (num1 + 1).ToString("000");
            txt_QA_Desc.Focus();
        }

        private void Btn_QA_Done_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add quote with Quote Number: ").Append(txt_QA_QNum.Text).Append("?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            using (SqlConnection dbConnection = DBUtils.GetDBConnection())
            {
                dbConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Quotes_Send VALUES (@QNum, @Date, @Client, @Desc, @OPlaced)", dbConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@QNum", txt_QA_QNum.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Date", dtp_QA_Date.Value);
                        sqlCommand.Parameters.AddWithValue("@Client", txt_QA_CName.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Desc", txt_QA_Desc.Text.Trim());
                        if (cb_QA_OrderPlaced.Checked)
                            sqlCommand.Parameters.AddWithValue("@OPlaced", "Yes");
                        else
                            sqlCommand.Parameters.AddWithValue("@OPlaced", "No");
                        sqlCommand.ExecuteNonQuery();
                        int num = (int)MessageBox.Show("New quote successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Btn_QA_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Txt_QA_ONum_MouseEnter(object sender, EventArgs e)
        {
            ln_QA_CONum.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_QA_ONum_Leave(object sender, EventArgs e)
        {
            ln_QA_CONum.LineColor = Color.Gray;
        }

        private void Txt_QA_ONum_MouseLeave(object sender, EventArgs e)
        {
            if (txt_QA_QNum.Focused)
                return;
            ln_QA_CONum.LineColor = Color.Gray;
        }

        private void Txt_QA_Desc_Leave(object sender, EventArgs e)
        {
            ln_QA_Desc.LineColor = Color.Gray;
        }

        private void Txt_QA_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_QA_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void Txt_QA_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (txt_QA_Desc.Focused)
                return;
            ln_QA_Desc.LineColor = Color.Gray;
        }

        private void Btn_QA_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_QA_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_QA_Close.Image = Resources.close_white;
        }

        private void Btn_QA_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_QA_Close.Image = Resources.close_black;
        }

        private void Btn_QA_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_QA_Done.ForeColor = Color.White;
        }

        private void Btn_QA_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_QA_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Btn_QA_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_QA_Cancel.ForeColor = Color.White;
        }

        private void Btn_QA_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_QA_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void Txt_QA_CCode_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Txt_QA_CName_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void Q_Add_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Q_Add_MouseMove(object sender, MouseEventArgs e)
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

        private void Q_Add_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
