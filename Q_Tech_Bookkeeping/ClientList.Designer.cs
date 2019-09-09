namespace Q_Tech_Bookkeeping
{
    partial class ClientList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientList));
            this.dgv_CL = new ADGV.AdvancedDataGridView();
            this.btn_CL_Close = new System.Windows.Forms.Button();
            this.lbl_Header = new Bunifu.Framework.UI.BunifuCustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CL)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_CL
            // 
            this.dgv_CL.AllowUserToAddRows = false;
            this.dgv_CL.AllowUserToDeleteRows = false;
            this.dgv_CL.AllowUserToResizeColumns = false;
            this.dgv_CL.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_CL.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_CL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_CL.AutoGenerateContextFilters = true;
            this.dgv_CL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_CL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_CL.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_CL.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CL.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_CL.ColumnHeadersHeight = 25;
            this.dgv_CL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_CL.DateWithTime = false;
            this.dgv_CL.EnableHeadersVisualStyles = false;
            this.dgv_CL.Location = new System.Drawing.Point(12, 52);
            this.dgv_CL.Name = "dgv_CL";
            this.dgv_CL.ReadOnly = true;
            this.dgv_CL.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_CL.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_CL.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_CL.Size = new System.Drawing.Size(366, 616);
            this.dgv_CL.TabIndex = 0;
            this.dgv_CL.TimeFilter = false;
            this.dgv_CL.UseWaitCursor = true;
            this.dgv_CL.SortStringChanged += new System.EventHandler(this.DGV_CList_SortStringChanged);
            this.dgv_CL.FilterStringChanged += new System.EventHandler(this.DGV_CList_FilterStringChanged);
            this.dgv_CL.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CL_CellDoubleClick);
            // 
            // btn_CL_Close
            // 
            this.btn_CL_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CL_Close.FlatAppearance.BorderSize = 0;
            this.btn_CL_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_CL_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_CL_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CL_Close.Image = global::Q_Tech_Bookkeeping.Properties.Resources.close_black;
            this.btn_CL_Close.Location = new System.Drawing.Point(353, 6);
            this.btn_CL_Close.Name = "btn_CL_Close";
            this.btn_CL_Close.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.btn_CL_Close.Size = new System.Drawing.Size(31, 29);
            this.btn_CL_Close.TabIndex = 1;
            this.btn_CL_Close.UseVisualStyleBackColor = false;
            this.btn_CL_Close.UseWaitCursor = true;
            this.btn_CL_Close.Click += new System.EventHandler(this.Btn_CL_Close_Click);
            this.btn_CL_Close.MouseEnter += new System.EventHandler(this.Btn_CL_Close_MouseEnter);
            this.btn_CL_Close.MouseLeave += new System.EventHandler(this.Btn_CL_Close_MouseLeave);
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.lbl_Header.Location = new System.Drawing.Point(12, 14);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(102, 20);
            this.lbl_Header.TabIndex = 2;
            this.lbl_Header.Text = "Select Client:";
            // 
            // ClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(390, 680);
            this.Controls.Add(this.lbl_Header);
            this.Controls.Add(this.btn_CL_Close);
            this.Controls.Add(this.dgv_CL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(390, 680);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 680);
            this.Name = "ClientList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Client List";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.ClientList_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CL_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CL_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CL_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ADGV.AdvancedDataGridView dgv_CL;
        private System.Windows.Forms.Button btn_CL_Close;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_Header;
    }
}