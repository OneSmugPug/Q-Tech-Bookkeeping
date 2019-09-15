namespace Q_Tech_Bookkeeping
{
    partial class PettyCash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_PettyCash = new ADGV.AdvancedDataGridView();
            this.txt_PC_Tot = new System.Windows.Forms.TextBox();
            this.lblSubtotal = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_PC_Export = new System.Windows.Forms.Button();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PersonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PettyCash)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_PettyCash
            // 
            this.dgv_PettyCash.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_PettyCash.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_PettyCash.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_PettyCash.AutoGenerateContextFilters = true;
            this.dgv_PettyCash.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_PettyCash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_PettyCash.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_PettyCash.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_PettyCash.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_PettyCash.ColumnHeadersHeight = 25;
            this.dgv_PettyCash.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.PersonName,
            this.Desc,
            this.Credit,
            this.Debit});
            this.dgv_PettyCash.DateWithTime = false;
            this.dgv_PettyCash.EnableHeadersVisualStyles = false;
            this.dgv_PettyCash.Location = new System.Drawing.Point(0, 62);
            this.dgv_PettyCash.Name = "dgv_PettyCash";
            this.dgv_PettyCash.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_PettyCash.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_PettyCash.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_PettyCash.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_PettyCash.Size = new System.Drawing.Size(963, 513);
            this.dgv_PettyCash.TabIndex = 0;
            this.dgv_PettyCash.TimeFilter = false;
            this.dgv_PettyCash.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_PettyCash_CellValueChanged);
            this.dgv_PettyCash.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_PettyCash_RowEnter);
            // 
            // txt_PC_Tot
            // 
            this.txt_PC_Tot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_PC_Tot.Location = new System.Drawing.Point(822, 586);
            this.txt_PC_Tot.Name = "txt_PC_Tot";
            this.txt_PC_Tot.ReadOnly = true;
            this.txt_PC_Tot.Size = new System.Drawing.Size(129, 20);
            this.txt_PC_Tot.TabIndex = 1;
            this.txt_PC_Tot.Text = "0.00";
            this.txt_PC_Tot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSubtotal.Location = new System.Drawing.Point(752, 587);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(64, 17);
            this.lblSubtotal.TabIndex = 2;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // btn_PC_Export
            // 
            this.btn_PC_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PC_Export.FlatAppearance.BorderSize = 0;
            this.btn_PC_Export.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_PC_Export.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_PC_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PC_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PC_Export.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_PC_Export.Image = global::Q_Tech_Bookkeeping.Properties.Resources.doc_grey;
            this.btn_PC_Export.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_PC_Export.Location = new System.Drawing.Point(792, 12);
            this.btn_PC_Export.Name = "btn_PC_Export";
            this.btn_PC_Export.Size = new System.Drawing.Size(159, 40);
            this.btn_PC_Export.TabIndex = 3;
            this.btn_PC_Export.Text = "Export";
            this.btn_PC_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_PC_Export.UseVisualStyleBackColor = true;
            this.btn_PC_Export.Click += new System.EventHandler(this.Btn_PC_Export_Click);
            this.btn_PC_Export.MouseEnter += new System.EventHandler(this.Btn_PC_Export_MouseEnter);
            this.btn_PC_Export.MouseLeave += new System.EventHandler(this.Btn_PC_Export_MouseLeave);
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 22;
            this.Date.Name = "Date";
            this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PersonName
            // 
            this.PersonName.HeaderText = "Person Name";
            this.PersonName.MinimumWidth = 22;
            this.PersonName.Name = "PersonName";
            this.PersonName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Desc
            // 
            this.Desc.HeaderText = "Description";
            this.Desc.MinimumWidth = 22;
            this.Desc.Name = "Desc";
            this.Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Credit
            // 
            this.Credit.HeaderText = "Credit";
            this.Credit.MinimumWidth = 22;
            this.Credit.Name = "Credit";
            this.Credit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Debit
            // 
            this.Debit.HeaderText = "Debit";
            this.Debit.MinimumWidth = 22;
            this.Debit.Name = "Debit";
            this.Debit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PettyCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.btn_PC_Export);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.txt_PC_Tot);
            this.Controls.Add(this.dgv_PettyCash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PettyCash";
            this.Text = "PettyCash";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PettyCash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PettyCash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ADGV.AdvancedDataGridView dgv_PettyCash;
        private System.Windows.Forms.TextBox txt_PC_Tot;
        private Bunifu.Framework.UI.BunifuCustomLabel lblSubtotal;
        private System.Windows.Forms.Button btn_PC_Export;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
    }
}