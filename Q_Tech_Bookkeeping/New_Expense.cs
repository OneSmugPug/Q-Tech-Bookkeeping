using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
    public partial class New_Expense : Form
    {
        private string Proj_ID;
        private bool mouseDown;
        private Point lastLocation;
        private Proj_AddExp parent;

        public New_Expense()
        {
            InitializeComponent();
        }


        //================================================================================================================================================//
        // SET PARENT FORM                                                                                                                                //
        //================================================================================================================================================//
        public void SetParent(Proj_AddExp parent)
        {
            this.parent = parent;
        }


        //================================================================================================================================================//
        // NEW EXPENSE FORM LOAD                                                                                                                          //
        //================================================================================================================================================//
        private void New_Expense_Load(object sender, EventArgs e)
        {
            Proj_ID = parent.GetProjectID();

            foreach (DataGridViewColumn column in parent.GetLines().Columns)
            {
                if (!column.Name.Equals("ID") && !column.Name.Equals("Project_ID") && !column.Name.Equals("Description") && !column.Name.Equals("Date") && !column.Name.Equals("User_Log"))
                    ddb_NE_Column.AddItem(column.Name);
            }

            ddb_NE_Column.selectedIndex = 0;
        }


        //================================================================================================================================================//
        // DONE CLICKED                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_NE_Done_Click(object sender, EventArgs e)
        {
            if (ddb_NE_Column.selectedValue.Equals("Travel") || ddb_NE_Column.selectedValue.Equals("Accomodation") || ddb_NE_Column.selectedValue.Equals("Subsistence") || ddb_NE_Column.selectedValue.Equals("Tools"))
            {
                if (txt_NE_Val.Text.Contains("R") || txt_NE_Val.Text.Contains("$"))
                    DoAdd();
                else MessageBox.Show("Value field requires a 'R'/'$' symbol", "error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else DoAdd();
        }

        private void DoAdd()
        {
            if (!txt_NE_User.Text.Equals(string.Empty))
            {
                if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add new line?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection conn = DBUtils.GetDBConnection())
                    {
                        conn.Open();
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Project_Expenses VALUES (@ProjID, @Desc, @Travel, @Acc, @Sub, @Tools, @ProgHrs, @InstHrs, @Date, @User)", conn))
                            {
                                cmd.Parameters.AddWithValue("@ProjID", Proj_ID);
                                cmd.Parameters.AddWithValue("@Desc", txt_NE_Desc.Text.Trim());

                                if (ddb_NE_Column.selectedValue.Equals("Travel"))
                                {
                                    cmd.Parameters.AddWithValue("@Travel", txt_NE_Val.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Acc", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Sub", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Tools", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@ProgHrs", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@InstHrs", DBNull.Value);
                                }
                                else if (ddb_NE_Column.selectedValue.Equals("Accomodation"))
                                {
                                    cmd.Parameters.AddWithValue("@Travel", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Acc", txt_NE_Val.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Sub", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Tools", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@ProgHrs", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@InstHrs", DBNull.Value);
                                }
                                else if (ddb_NE_Column.selectedValue.Equals("Subsistence"))
                                {
                                    cmd.Parameters.AddWithValue("@Travel", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Acc", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Sub", txt_NE_Val.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Tools", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@ProgHrs", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@InstHrs", DBNull.Value);
                                }
                                else if (ddb_NE_Column.selectedValue.Equals("Tools"))
                                {
                                    cmd.Parameters.AddWithValue("@Travel", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Acc", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Sub", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Tools", txt_NE_Val.Text.Trim());
                                    cmd.Parameters.AddWithValue("@ProgHrs", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@InstHrs", DBNull.Value);
                                }
                                else if (ddb_NE_Column.selectedValue.Equals("Programming_Hours"))
                                {
                                    cmd.Parameters.AddWithValue("@Travel", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Acc", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Sub", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Tools", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@ProgHrs", txt_NE_Val.Text.Trim());
                                    cmd.Parameters.AddWithValue("@InstHrs", DBNull.Value);
                                }
                                else if (ddb_NE_Column.selectedValue.Equals("Install_Hours"))
                                {
                                    cmd.Parameters.AddWithValue("@Travel", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Acc", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Sub", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Tools", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@ProgHrs", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@InstHrs", txt_NE_Val.Text.Trim());
                                }

                                cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                                cmd.Parameters.AddWithValue("@User", txt_NE_User.Text.Trim());
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("New line successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Please enter name in user field.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //================================================================================================================================================//
        // CANCEL CLICKED                                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_NE_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // DESCRIPTION                                                                                                                                    //
        //================================================================================================================================================//
        private void txt_NE_Desc_MouseEnter(object sender, EventArgs e)
        {
            ln_NE_Desc.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void txt_NE_Desc_Leave(object sender, EventArgs e)
        {
            ln_NE_Desc.LineColor = Color.Gray;
        }

        private void txt_NE_Desc_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_NE_Desc.Focused)
                ln_NE_Desc.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // VALUE                                                                                                                                          //
        //================================================================================================================================================//
        private void txt_NE_Val_MouseEnter(object sender, EventArgs e)
        {
            ln_NE_Val.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void txt_NE_Val_Leave(object sender, EventArgs e)
        {
            ln_NE_Val.LineColor = Color.Gray;
        }

        private void txt_NE_Val_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_NE_Val.Focused)
                ln_NE_Val.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // USER                                                                                                                                           //
        //================================================================================================================================================//
        private void txt_NE_User_MouseEnter(object sender, EventArgs e)
        {
            ln_NE_User.LineColor = Color.FromArgb(19, 118, 188);
        }

        private void txt_NE_User_Leave(object sender, EventArgs e)
        {
            ln_NE_User.LineColor = Color.Gray;
        }

        private void txt_NE_User_MouseLeave(object sender, EventArgs e)
        {
            if (!txt_NE_User.Focused)
                ln_NE_User.LineColor = Color.Gray;
        }


        //================================================================================================================================================//
        // CLOSE CLICKED                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_NE_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //================================================================================================================================================//
        // CLOSE BUTTON                                                                                                                                   //
        //================================================================================================================================================//
        private void Btn_NE_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_NE_Close.Image = Resources.close_white;
        }

        private void Btn_NE_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_NE_Close.Image = Resources.close_black;
        }


        //================================================================================================================================================//
        // DONE BUTTON                                                                                                                                    //
        //================================================================================================================================================//
        private void Btn_NE_Done_MouseEnter(object sender, EventArgs e)
        {
            btn_NE_Done.ForeColor = Color.White;
        }

        private void Btn_NE_Done_MouseLeave(object sender, EventArgs e)
        {
            btn_NE_Done.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // CANCEL BUTTON                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_NE_Cancel_MouseEnter(object sender, EventArgs e)
        {
            btn_NE_Cancel.ForeColor = Color.White;
        }

        private void Btn_NE_Cancel_MouseLeave(object sender, EventArgs e)
        {
            btn_NE_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
        }


        //================================================================================================================================================//
        // NEW EXPENSE                                                                                                                                    //
        //================================================================================================================================================//
        private void New_Expense_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void New_Expense_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
            {
                Location = new Point(this.Location.X - (lastLocation.X + e.X), this.Location.Y - (lastLocation.Y + e.Y));
                this.Update();
            }
        }

        private void New_Expense_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
