// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.PettyCash
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using ADGV;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class PettyCash : Form
  {
    private Decimal subtotal = new Decimal(0, 0, 0, false, (byte) 2);
    private IContainer components = (IContainer) null;
    private AdvancedDataGridView dgv_PettyCash;
    private Label label5;
    private TextBox txt_PC_Tot;
    private Button btn_PC_Export;
    private DataGridViewTextBoxColumn Date;
    private DataGridViewTextBoxColumn PersonName;
    private DataGridViewTextBoxColumn Desc;
    private DataGridViewTextBoxColumn Credit;
    private DataGridViewTextBoxColumn Debit;

    public PettyCash()
    {
      this.InitializeComponent();
    }

    private void PettyCash_Load(object sender, EventArgs e)
    {
      this.txt_PC_Tot.Text = new Decimal(0, 0, 0, false, (byte) 2).ToString("c2");
      this.dgv_PettyCash.Columns[0].ValueType = System.Type.GetType("System.DateTime");
      this.dgv_PettyCash.Columns[0].DefaultCellStyle.Format = "d";
      this.dgv_PettyCash.Columns[3].ValueType = System.Type.GetType("System.Decimal");
      this.dgv_PettyCash.Columns[3].DefaultCellStyle.Format = "c2";
      this.dgv_PettyCash.Columns[4].ValueType = System.Type.GetType("System.Decimal");
      this.dgv_PettyCash.Columns[4].DefaultCellStyle.Format = "c2";
    }

    private void dgv_PettyCash_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex <= -1)
        return;
      DataGridViewRow row = this.dgv_PettyCash.Rows[e.RowIndex];
      Decimal result1;
      if (row.Cells[3].FormattedValue.ToString() != string.Empty && e.ColumnIndex == 3 && (Decimal.TryParse(row.Cells[3].Value.ToString(), out result1) && Decimal.TryParse(this.txt_PC_Tot.Text.Replace(",", string.Empty).Replace(".", string.Empty).Replace("R", string.Empty), out this.subtotal)))
      {
        this.subtotal /= new Decimal(100);
        this.subtotal += result1;
        this.txt_PC_Tot.Text = this.subtotal.ToString("c2");
      }
      Decimal result2;
      if (row.Cells[4].FormattedValue.ToString() != string.Empty && e.ColumnIndex == 4 && (Decimal.TryParse(row.Cells[4].Value.ToString(), out result2) && Decimal.TryParse(this.txt_PC_Tot.Text.Replace(",", string.Empty).Replace(".", string.Empty).Replace("R", string.Empty), out this.subtotal)))
      {
        this.subtotal /= new Decimal(100);
        this.subtotal -= result2;
        this.txt_PC_Tot.Text = this.subtotal.ToString("c2");
      }
    }

    private void btn_PC_Export_Click(object sender, EventArgs e)
    {
      object obj = (object) Missing.Value;
      string str1 = "c:\\Petty Cash";
      if (!Directory.Exists(str1))
        Directory.CreateDirectory(str1);
      string path2 = string.Format("PettyCashLeger_{0}.xls", (object) string.Format("{0:dd-MM-yy_hh-mm-ss}", (object) DateTime.Now));
      string str2 = Path.Combine(str1, path2);
      // ISSUE: variable of a compiler-generated type
      Microsoft.Office.Interop.Excel.Application instance = (Microsoft.Office.Interop.Excel.Application) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
      if (instance == null)
      {
        int num1 = (int) MessageBox.Show("Excel is not properly installed on this computer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: variable of a compiler-generated type
        Workbook workbook = instance.Workbooks.Add(obj);
        // ISSUE: reference to a compiler-generated field
        if (PettyCash.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PettyCash.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Worksheet>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Worksheet), typeof (PettyCash)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        // ISSUE: variable of a compiler-generated type
        Worksheet worksheet = PettyCash.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) PettyCash.\u003C\u003Eo__4.\u003C\u003Ep__0, workbook.Worksheets.get_Item((object) 1));
        worksheet.Cells[(object) 1, (object) 1] = (object) "Date";
        worksheet.Cells[(object) 1, (object) 2] = (object) "Name";
        worksheet.Cells[(object) 1, (object) 3] = (object) "Description";
        worksheet.Cells[(object) 1, (object) 4] = (object) "Credit";
        worksheet.Cells[(object) 1, (object) 5] = (object) "Debit";
        int num2 = 0;
        for (int index1 = 0; index1 <= this.dgv_PettyCash.RowCount - 2; ++index1)
        {
          for (int index2 = 0; index2 < 5; ++index2)
          {
            DataGridViewRow row = this.dgv_PettyCash.Rows[index1];
            worksheet.Cells[(object) (index1 + 2), (object) (index2 + 1)] = (object) row.Cells[index2].FormattedValue.ToString();
          }
          num2 = index1;
        }
        worksheet.Cells[(object) (num2 + 4), (object) 4] = (object) "Subtotal";
        worksheet.Cells[(object) (num2 + 4), (object) 5] = (object) this.txt_PC_Tot.Text;
        // ISSUE: reference to a compiler-generated method
        workbook.SaveAs((object) str2, (object) XlFileFormat.xlWorkbookNormal, obj, obj, obj, obj, XlSaveAsAccessMode.xlExclusive, obj, obj, obj, obj, obj);
        // ISSUE: reference to a compiler-generated method
        workbook.Close((object) true, obj, obj);
        // ISSUE: reference to a compiler-generated method
        instance.Quit();
        this.releaseObject((object) worksheet);
        this.releaseObject((object) workbook);
        this.releaseObject((object) instance);
        int num3 = (int) MessageBox.Show("Excel file created , you can find the file in " + str2, "File Exported", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void releaseObject(object obj)
    {
      try
      {
        Marshal.ReleaseComObject(obj);
        obj = (object) null;
      }
      catch (Exception ex)
      {
        obj = (object) null;
        int num = (int) MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
      }
      finally
      {
        GC.Collect();
      }
    }

    private void dgv_PettyCash_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex <= -1)
        return;
      DataGridViewRow row = this.dgv_PettyCash.Rows[e.RowIndex];
      if (row.Cells[3].FormattedValue.ToString().Equals("0.00"))
        row.Cells[3].Value = (object) new Decimal(0, 0, 0, false, (byte) 2);
      if (row.Cells[4].FormattedValue.ToString().Equals("0.00"))
        row.Cells[4].Value = (object) new Decimal(0, 0, 0, false, (byte) 2);
    }

    private void btn_PC_Export_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PC_Export.Image = (Image) Resources.export_white;
      this.btn_PC_Export.ForeColor = Color.White;
    }

    private void btn_PC_Export_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PC_Export.Image = (Image) Resources.export_grey;
      this.btn_PC_Export.ForeColor = Color.FromArgb(64, 64, 64);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
      this.dgv_PettyCash = new AdvancedDataGridView();
      this.Date = new DataGridViewTextBoxColumn();
      this.PersonName = new DataGridViewTextBoxColumn();
      this.Desc = new DataGridViewTextBoxColumn();
      this.Credit = new DataGridViewTextBoxColumn();
      this.Debit = new DataGridViewTextBoxColumn();
      this.label5 = new Label();
      this.txt_PC_Tot = new TextBox();
      this.btn_PC_Export = new Button();
      ((ISupportInitialize) this.dgv_PettyCash).BeginInit();
      this.SuspendLayout();
      this.dgv_PettyCash.AllowUserToResizeColumns = false;
      this.dgv_PettyCash.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.LightGray;
      this.dgv_PettyCash.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgv_PettyCash.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgv_PettyCash.AutoGenerateContextFilters = true;
      this.dgv_PettyCash.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgv_PettyCash.BorderStyle = BorderStyle.None;
      this.dgv_PettyCash.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
      this.dgv_PettyCash.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.FromArgb(19, 118, 188);
      gridViewCellStyle2.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.White;
      gridViewCellStyle2.SelectionBackColor = Color.Gray;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgv_PettyCash.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgv_PettyCash.ColumnHeadersHeight = 25;
      this.dgv_PettyCash.Columns.AddRange((DataGridViewColumn) this.Date, (DataGridViewColumn) this.PersonName, (DataGridViewColumn) this.Desc, (DataGridViewColumn) this.Credit, (DataGridViewColumn) this.Debit);
      this.dgv_PettyCash.DateWithTime = false;
      this.dgv_PettyCash.EnableHeadersVisualStyles = false;
      this.dgv_PettyCash.Location = new Point(0, 62);
      this.dgv_PettyCash.Name = "dgv_PettyCash";
      this.dgv_PettyCash.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dgv_PettyCash.RowHeadersVisible = false;
      gridViewCellStyle3.SelectionBackColor = Color.FromArgb(15, 91, 142);
      this.dgv_PettyCash.RowsDefaultCellStyle = gridViewCellStyle3;
      this.dgv_PettyCash.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.dgv_PettyCash.Size = new Size(963, 513);
      this.dgv_PettyCash.TabIndex = 97;
      this.dgv_PettyCash.TimeFilter = false;
      this.dgv_PettyCash.CellValueChanged += new DataGridViewCellEventHandler(this.dgv_PettyCash_CellValueChanged);
      this.dgv_PettyCash.RowEnter += new DataGridViewCellEventHandler(this.dgv_PettyCash_RowEnter);
      gridViewCellStyle4.Format = "dd/mm/yyyy";
      gridViewCellStyle4.NullValue = (object) null;
      this.Date.DefaultCellStyle = gridViewCellStyle4;
      this.Date.HeaderText = "Date";
      this.Date.MinimumWidth = 22;
      this.Date.Name = "Date";
      this.Date.SortMode = DataGridViewColumnSortMode.Programmatic;
      this.PersonName.HeaderText = "Name";
      this.PersonName.MinimumWidth = 22;
      this.PersonName.Name = "PersonName";
      this.PersonName.SortMode = DataGridViewColumnSortMode.Programmatic;
      this.Desc.HeaderText = "Description";
      this.Desc.MinimumWidth = 22;
      this.Desc.Name = "Desc";
      this.Desc.SortMode = DataGridViewColumnSortMode.Programmatic;
      gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
      gridViewCellStyle5.Format = "C2";
      gridViewCellStyle5.NullValue = (object) "0.00";
      this.Credit.DefaultCellStyle = gridViewCellStyle5;
      this.Credit.HeaderText = "Credit";
      this.Credit.MinimumWidth = 22;
      this.Credit.Name = "Credit";
      this.Credit.SortMode = DataGridViewColumnSortMode.Programmatic;
      gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
      gridViewCellStyle6.Format = "C2";
      gridViewCellStyle6.NullValue = (object) "0.00";
      this.Debit.DefaultCellStyle = gridViewCellStyle6;
      this.Debit.HeaderText = "Debit";
      this.Debit.MinimumWidth = 22;
      this.Debit.Name = "Debit";
      this.Debit.SortMode = DataGridViewColumnSortMode.Programmatic;
      this.label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.ForeColor = Color.FromArgb(64, 64, 64);
      this.label5.Location = new Point(752, 587);
      this.label5.Name = "label5";
      this.label5.Size = new Size(64, 17);
      this.label5.TabIndex = 98;
      this.label5.Text = "Subtotal:";
      this.txt_PC_Tot.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.txt_PC_Tot.Location = new Point(822, 586);
      this.txt_PC_Tot.Name = "txt_PC_Tot";
      this.txt_PC_Tot.ReadOnly = true;
      this.txt_PC_Tot.Size = new Size(129, 20);
      this.txt_PC_Tot.TabIndex = 99;
      this.txt_PC_Tot.TabStop = false;
      this.txt_PC_Tot.Text = "0.00";
      this.txt_PC_Tot.TextAlign = HorizontalAlignment.Right;
      this.btn_PC_Export.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_PC_Export.FlatAppearance.BorderSize = 0;
      this.btn_PC_Export.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_PC_Export.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_PC_Export.FlatStyle = FlatStyle.Flat;
      this.btn_PC_Export.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_PC_Export.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_PC_Export.Image = (Image) Resources.export_grey;
      this.btn_PC_Export.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_PC_Export.Location = new Point(792, 12);
      this.btn_PC_Export.Name = "btn_PC_Export";
      this.btn_PC_Export.Size = new Size(159, 40);
      this.btn_PC_Export.TabIndex = 100;
      this.btn_PC_Export.Text = "Export";
      this.btn_PC_Export.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_PC_Export.UseVisualStyleBackColor = true;
      this.btn_PC_Export.Click += new EventHandler(this.btn_PC_Export_Click);
      this.btn_PC_Export.MouseEnter += new EventHandler(this.btn_PC_Export_MouseEnter);
      this.btn_PC_Export.MouseLeave += new EventHandler(this.btn_PC_Export_MouseLeave);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.LightGray;
      this.ClientSize = new Size(963, 618);
      this.Controls.Add((Control) this.btn_PC_Export);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.txt_PC_Tot);
      this.Controls.Add((Control) this.dgv_PettyCash);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (PettyCash);
      this.Text = nameof (PettyCash);
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.PettyCash_Load);
      ((ISupportInitialize) this.dgv_PettyCash).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
