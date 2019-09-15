using Q_Tech_Bookkeeping.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Data;

namespace Q_Tech_Bookkeeping
{
    public partial class PettyCash : Form
    {
        private Decimal subtotal = 0.00m;

        public PettyCash()
        {
            InitializeComponent();
        }

        //================================================================================================================================================//
        // PETTYCASH FORM LOAD                                                                                                                            //
        //================================================================================================================================================//
        private void PettyCash_Load(object sender, EventArgs e)
        {
            txt_PC_Tot.Text = 0.00m.ToString("c2");

            dgv_PettyCash.Columns[0].ValueType = Type.GetType("System.DateTime");
            dgv_PettyCash.Columns[0].DefaultCellStyle.Format = "d";

            dgv_PettyCash.Columns[3].ValueType = Type.GetType("System.Decimal");
            dgv_PettyCash.Columns[3].DefaultCellStyle.Format = "c2";

            dgv_PettyCash.Columns[4].ValueType = Type.GetType("System.Decimal");
            dgv_PettyCash.Columns[4].DefaultCellStyle.Format = "c2";
        }


        //================================================================================================================================================//
        // ON CELL VALUE CHANGED                                                                                                                          //
        //================================================================================================================================================//
        private void Dgv_PettyCash_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_PettyCash.Rows[e.RowIndex];

                if (row.Cells[3].FormattedValue.ToString() != string.Empty && e.ColumnIndex == 3 && (Decimal.TryParse(row.Cells[3].Value.ToString(), out decimal debit) && Decimal.TryParse(txt_PC_Tot.Text.Replace(",", string.Empty).Replace(".", string.Empty).Replace("R", string.Empty), out subtotal)))
                {
                    subtotal /= 100;
                    subtotal += debit;
                    txt_PC_Tot.Text = subtotal.ToString("c2");
                }

                if (row.Cells[4].FormattedValue.ToString() != string.Empty && e.ColumnIndex == 4 && (Decimal.TryParse(row.Cells[4].Value.ToString(), out decimal credit) && Decimal.TryParse(txt_PC_Tot.Text.Replace(",", string.Empty).Replace(".", string.Empty).Replace("R", string.Empty), out subtotal)))
                {
                    subtotal /= new Decimal(100);
                    subtotal -= credit;
                    txt_PC_Tot.Text = subtotal.ToString("c2");
                }
            }
        }


        //================================================================================================================================================//
        // EXPORT CLICKED                                                                                                                                 //
        //================================================================================================================================================//
        private void Btn_PC_Export_Click(object sender, EventArgs e)
        {
            string path = "c:\\Petty Cash";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = string.Format("PettyCashLeger_{0}.xls", string.Format("{0:dd-MM-yy_hh-mm-ss}", DateTime.Now));
            path = Path.Combine(path, fileName);

            XLWorkbook wb = new XLWorkbook();

            try
            {
                wb.Worksheets.Add((DataTable)dgv_PettyCash.DataSource, fileName);
                wb.SaveAs(path);

                MessageBox.Show("Data successfully exported to " + path, "Export Complete", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //================================================================================================================================================//
        // NEW ROW ENTERED                                                                                                                                //
        //================================================================================================================================================//
        private void Dgv_PettyCash_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dgv_PettyCash.Rows[e.RowIndex];

                if (!row.Cells[3].FormattedValue.ToString().Equals("0.00"))
                    row.Cells[3].Value = 0.00m;

                if (!row.Cells[4].FormattedValue.ToString().Equals("0.00"))
                    row.Cells[4].Value = 0.00m;
            }
        }


        //================================================================================================================================================//
        // EXPORT BUTTON                                                                                                                                  //
        //================================================================================================================================================//
        private void Btn_PC_Export_MouseEnter(object sender, EventArgs e)
        {
            btn_PC_Export.Image = Resources.export_white;
            btn_PC_Export.ForeColor = Color.White;
        }

        private void Btn_PC_Export_MouseLeave(object sender, EventArgs e)
        {
            btn_PC_Export.Image = Resources.export_grey;
            btn_PC_Export.ForeColor = Color.FromArgb(64, 64, 64);
        }
    }
}
