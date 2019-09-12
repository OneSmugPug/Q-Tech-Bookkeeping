namespace Q_Tech_Bookkeeping
{
    partial class Projects
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_P_NewProject = new System.Windows.Forms.Button();
            this.btn_P_ClearFilter = new System.Windows.Forms.Button();
            this.btn_P_Filter = new System.Windows.Forms.Button();
            this.dtp_P_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtp_P_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dgv_Projects = new ADGV.AdvancedDataGridView();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Projects)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_P_NewProject
            // 
            this.btn_P_NewProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_P_NewProject.FlatAppearance.BorderSize = 0;
            this.btn_P_NewProject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_P_NewProject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_P_NewProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_P_NewProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_P_NewProject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_P_NewProject.Image = global::Properties.Resources.add_grey;
            this.btn_P_NewProject.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_P_NewProject.Location = new System.Drawing.Point(810, 10);
            this.btn_P_NewProject.Name = "btn_P_NewProject";
            this.btn_P_NewProject.Size = new System.Drawing.Size(140, 40);
            this.btn_P_NewProject.TabIndex = 0;
            this.btn_P_NewProject.Text = "New Project";
            this.btn_P_NewProject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_P_NewProject.UseVisualStyleBackColor = true;
            this.btn_P_NewProject.Click += new System.EventHandler(this.Btn_P_NewProject_Click);
            this.btn_P_NewProject.MouseEnter += new System.EventHandler(this.Btn_P_NewProject_MouseEnter);
            this.btn_P_NewProject.MouseLeave += new System.EventHandler(this.Btn_P_NewProject_MouseLeave);
            // 
            // btn_P_ClearFilter
            // 
            this.btn_P_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_P_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_P_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_P_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_P_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_P_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_P_ClearFilter.Location = new System.Drawing.Point(553, 9);
            this.btn_P_ClearFilter.Name = "btn_P_ClearFilter";
            this.btn_P_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_P_ClearFilter.TabIndex = 1;
            this.btn_P_ClearFilter.Text = "Clear Filter";
            this.btn_P_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_P_ClearFilter.Visible = false;
            this.btn_P_ClearFilter.Click += new System.EventHandler(this.Btn_P_ClearFilter_Click);
            this.btn_P_ClearFilter.MouseEnter += new System.EventHandler(this.Btn_P_ClearFilter_MouseEnter);
            this.btn_P_ClearFilter.MouseLeave += new System.EventHandler(this.Btn_P_ClearFilter_MouseLeave);
            // 
            // btn_P_Filter
            // 
            this.btn_P_Filter.FlatAppearance.BorderSize = 0;
            this.btn_P_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_P_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_P_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_P_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_P_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_P_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_P_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_P_Filter.Location = new System.Drawing.Point(553, 10);
            this.btn_P_Filter.Name = "btn_P_Filter";
            this.btn_P_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_P_Filter.TabIndex = 2;
            this.btn_P_Filter.Text = "Filter";
            this.btn_P_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_P_Filter.UseVisualStyleBackColor = true;
            this.btn_P_Filter.Click += new System.EventHandler(this.Btn_P_Filter_Click);
            this.btn_P_Filter.MouseEnter += new System.EventHandler(this.Btn_P_Filter_MouseEnter);
            this.btn_P_Filter.MouseLeave += new System.EventHandler(this.Btn_P_Filter_MouseLeave);
            // 
            // dtp_P_From
            // 
            this.dtp_P_From.BackColor = System.Drawing.Color.LightGray;
            this.dtp_P_From.BorderRadius = 0;
            this.dtp_P_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_P_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_P_From.FormatCustom = null;
            this.dtp_P_From.Location = new System.Drawing.Point(70, 13);
            this.dtp_P_From.Name = "dtp_P_From";
            this.dtp_P_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_P_From.TabIndex = 3;
            this.dtp_P_From.Value = new System.DateTime(2019, 9, 12, 15, 2, 11, 609);
            // 
            // dtp_P_To
            // 
            this.dtp_P_To.BackColor = System.Drawing.Color.LightGray;
            this.dtp_P_To.BorderRadius = 0;
            this.dtp_P_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_P_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_P_To.FormatCustom = null;
            this.dtp_P_To.Location = new System.Drawing.Point(324, 13);
            this.dtp_P_To.Name = "dtp_P_To";
            this.dtp_P_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_P_To.TabIndex = 4;
            this.dtp_P_To.Value = new System.DateTime(2019, 9, 12, 15, 2, 16, 356);
            // 
            // dgv_Projects
            // 
            this.dgv_Projects.AllowUserToAddRows = false;
            this.dgv_Projects.AllowUserToDeleteRows = false;
            this.dgv_Projects.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            this.dgv_Projects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_Projects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Projects.AutoGenerateContextFilters = true;
            this.dgv_Projects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Projects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Projects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_Projects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Projects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_Projects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Projects.DateWithTime = false;
            this.dgv_Projects.Location = new System.Drawing.Point(0, 57);
            this.dgv_Projects.Name = "dgv_Projects";
            this.dgv_Projects.ReadOnly = true;
            this.dgv_Projects.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_Projects.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_Projects.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_Projects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Projects.Size = new System.Drawing.Size(963, 562);
            this.dgv_Projects.TabIndex = 5;
            this.dgv_Projects.TimeFilter = false;
            this.dgv_Projects.SortStringChanged += new System.EventHandler(this.Dgv_Projects_SortStringChanged);
            this.dgv_Projects.FilterStringChanged += new System.EventHandler(this.Dgv_Projects_FilterStringChanged);
            this.dgv_Projects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Projects_CellDoubleClick);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(284, 20);
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
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(12, 20);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(50, 20);
            this.bunifuCustomLabel2.TabIndex = 7;
            this.bunifuCustomLabel2.Text = "From:";
            // 
            // Projects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.dgv_Projects);
            this.Controls.Add(this.dtp_P_To);
            this.Controls.Add(this.dtp_P_From);
            this.Controls.Add(this.btn_P_Filter);
            this.Controls.Add(this.btn_P_ClearFilter);
            this.Controls.Add(this.btn_P_NewProject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Projects";
            this.Text = "Projects";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Projects_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Projects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_P_NewProject;
        private System.Windows.Forms.Button btn_P_ClearFilter;
        private System.Windows.Forms.Button btn_P_Filter;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_P_From;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_P_To;
        private ADGV.AdvancedDataGridView dgv_Projects;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
    }
}