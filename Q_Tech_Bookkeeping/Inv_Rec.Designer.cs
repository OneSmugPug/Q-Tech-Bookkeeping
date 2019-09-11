namespace Q_Tech_Bookkeeping
{
    partial class Inv_Rec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inv_Rec));
            this.btn_LIR_ClearFilter = new System.Windows.Forms.Button();
            this.btn_LIR_NewIR = new System.Windows.Forms.Button();
            this.btn_LIR_Filter = new System.Windows.Forms.Button();
            this.dtp_LIR_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtp_LIR_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dgv_LInvRec = new ADGV.AdvancedDataGridView();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LInvRec)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_LIR_ClearFilter
            // 
            this.btn_LIR_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_LIR_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIR_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIR_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIR_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIR_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LIR_ClearFilter.Location = new System.Drawing.Point(553, 9);
            this.btn_LIR_ClearFilter.Name = "btn_LIR_ClearFilter";
            this.btn_LIR_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_LIR_ClearFilter.TabIndex = 0;
            this.btn_LIR_ClearFilter.Text = "Clear Filter";
            this.btn_LIR_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_LIR_ClearFilter.Visible = false;
            this.btn_LIR_ClearFilter.Click += new System.EventHandler(this.Btn_LIR_ClearFilter_Click);
            this.btn_LIR_ClearFilter.MouseEnter += new System.EventHandler(this.Btn_LIR_ClearFilter_MouseEnter);
            this.btn_LIR_ClearFilter.MouseLeave += new System.EventHandler(this.Btn_LIR_ClearFilter_MouseLeave);
            // 
            // btn_LIR_NewIR
            // 
            this.btn_LIR_NewIR.FlatAppearance.BorderSize = 0;
            this.btn_LIR_NewIR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIR_NewIR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIR_NewIR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIR_NewIR.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIR_NewIR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LIR_NewIR.Image = global::Properties.Resources.add_grey;
            this.btn_LIR_NewIR.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LIR_NewIR.Location = new System.Drawing.Point(829, 9);
            this.btn_LIR_NewIR.Name = "btn_LIR_NewIR";
            this.btn_LIR_NewIR.Size = new System.Drawing.Size(122, 40);
            this.btn_LIR_NewIR.TabIndex = 2;
            this.btn_LIR_NewIR.Text = "New Invoice";
            this.btn_LIR_NewIR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LIR_NewIR.UseVisualStyleBackColor = true;
            this.btn_LIR_NewIR.Click += new System.EventHandler(this.Btn_LIR_NewIR_Click);
            this.btn_LIR_NewIR.MouseEnter += new System.EventHandler(this.Btn_LIR_NewIR_MouseEnter);
            this.btn_LIR_NewIR.MouseLeave += new System.EventHandler(this.Btn_LIR_NewIR_MouseLeave);
            // 
            // btn_LIR_Filter
            // 
            this.btn_LIR_Filter.FlatAppearance.BorderSize = 0;
            this.btn_LIR_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIR_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIR_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIR_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIR_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LIR_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_LIR_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LIR_Filter.Location = new System.Drawing.Point(553, 9);
            this.btn_LIR_Filter.Name = "btn_LIR_Filter";
            this.btn_LIR_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_LIR_Filter.TabIndex = 1;
            this.btn_LIR_Filter.Text = "Filter";
            this.btn_LIR_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LIR_Filter.UseVisualStyleBackColor = true;
            this.btn_LIR_Filter.Click += new System.EventHandler(this.Btn_LIR_Filter_Click);
            this.btn_LIR_Filter.MouseEnter += new System.EventHandler(this.Btn_LIR_Filter_MouseEnter);
            this.btn_LIR_Filter.MouseLeave += new System.EventHandler(this.Btn_LIR_Filter_MouseLeave);
            // 
            // dtp_LIR_To
            // 
            this.dtp_LIR_To.BackColor = System.Drawing.Color.Silver;
            this.dtp_LIR_To.BorderRadius = 0;
            this.dtp_LIR_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LIR_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LIR_To.FormatCustom = null;
            this.dtp_LIR_To.Location = new System.Drawing.Point(324, 12);
            this.dtp_LIR_To.Name = "dtp_LIR_To";
            this.dtp_LIR_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_LIR_To.TabIndex = 3;
            this.dtp_LIR_To.Value = new System.DateTime(2019, 9, 11, 17, 23, 59, 369);
            // 
            // dtp_LIR_From
            // 
            this.dtp_LIR_From.BackColor = System.Drawing.Color.Silver;
            this.dtp_LIR_From.BorderRadius = 0;
            this.dtp_LIR_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LIR_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LIR_From.FormatCustom = null;
            this.dtp_LIR_From.Location = new System.Drawing.Point(70, 12);
            this.dtp_LIR_From.Name = "dtp_LIR_From";
            this.dtp_LIR_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_LIR_From.TabIndex = 4;
            this.dtp_LIR_From.Value = new System.DateTime(2019, 9, 11, 17, 24, 4, 632);
            // 
            // dgv_LInvRec
            // 
            this.dgv_LInvRec.AllowUserToAddRows = false;
            this.dgv_LInvRec.AllowUserToDeleteRows = false;
            this.dgv_LInvRec.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_LInvRec.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_LInvRec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_LInvRec.AutoGenerateContextFilters = true;
            this.dgv_LInvRec.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_LInvRec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_LInvRec.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_LInvRec.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_LInvRec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_LInvRec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_LInvRec.DateWithTime = false;
            this.dgv_LInvRec.EnableHeadersVisualStyles = false;
            this.dgv_LInvRec.Location = new System.Drawing.Point(0, 56);
            this.dgv_LInvRec.Name = "dgv_LInvRec";
            this.dgv_LInvRec.ReadOnly = true;
            this.dgv_LInvRec.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_LInvRec.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_LInvRec.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_LInvRec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_LInvRec.Size = new System.Drawing.Size(963, 562);
            this.dgv_LInvRec.TabIndex = 5;
            this.dgv_LInvRec.TimeFilter = false;
            this.dgv_LInvRec.SortStringChanged += new System.EventHandler(this.Dgv_LInvRec_SortStringChanged);
            this.dgv_LInvRec.FilterStringChanged += new System.EventHandler(this.Dgv_LInvRec_FilterStringChanged);
            this.dgv_LInvRec.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_LInvRec_CellDoubleClick);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(284, 19);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(31, 20);
            this.bunifuCustomLabel1.TabIndex = 6;
            this.bunifuCustomLabel1.Text = "To:";
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(12, 19);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(50, 20);
            this.bunifuCustomLabel2.TabIndex = 7;
            this.bunifuCustomLabel2.Text = "From:";
            // 
            // Inv_Rec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.dgv_LInvRec);
            this.Controls.Add(this.dtp_LIR_From);
            this.Controls.Add(this.dtp_LIR_To);
            this.Controls.Add(this.btn_LIR_NewIR);
            this.Controls.Add(this.btn_LIR_Filter);
            this.Controls.Add(this.btn_LIR_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(963, 618);
            this.Name = "Inv_Rec";
            this.Text = "Invoices Received";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Inv_Rec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LInvRec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_LIR_ClearFilter;
        private System.Windows.Forms.Button btn_LIR_Filter;
        private System.Windows.Forms.Button btn_LIR_NewIR;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LIR_To;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LIR_From;
        private ADGV.AdvancedDataGridView dgv_LInvRec;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
    }
}