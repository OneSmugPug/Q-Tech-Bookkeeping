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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inv_Rec));
            this.btn_LIR_ClearFilter = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_LIR_Filter = new System.Windows.Forms.Button();
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(480, 236);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
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
            // Inv_Rec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_LIR_Filter);
            this.Controls.Add(this.btn_LIR_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(963, 618);
            this.Name = "Inv_Rec";
            this.Text = "Invoices Received";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Inv_Rec_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_LIR_ClearFilter;
        private System.Windows.Forms.Button btn_LIR_Filter;
        private System.Windows.Forms.Button button3;
    }
}