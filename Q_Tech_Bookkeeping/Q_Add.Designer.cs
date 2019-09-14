namespace Q_Tech_Bookkeeping
{
    partial class Q_Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Q_Add));
            this.btn_QA_Cancel = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_QA_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_QA_Cancel
            // 
            this.btn_QA_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_QA_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_QA_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_QA_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_QA_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QA_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_QA_Cancel.Location = new System.Drawing.Point(632, 251);
            this.btn_QA_Cancel.Name = "btn_QA_Cancel";
            this.btn_QA_Cancel.Size = new System.Drawing.Size(114, 40);
            this.btn_QA_Cancel.TabIndex = 1;
            this.btn_QA_Cancel.Text = "Cancel";
            this.btn_QA_Cancel.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(300, 166);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btn_QA_Close
            // 
            this.btn_QA_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_QA_Close.FlatAppearance.BorderSize = 0;
            this.btn_QA_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_QA_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_QA_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_QA_Close.Image = global::Properties.Resources.close_black;
            this.btn_QA_Close.Location = new System.Drawing.Point(723, 5);
            this.btn_QA_Close.Name = "btn_QA_Close";
            this.btn_QA_Close.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.btn_QA_Close.Size = new System.Drawing.Size(31, 29);
            this.btn_QA_Close.TabIndex = 0;
            this.btn_QA_Close.TabStop = false;
            this.btn_QA_Close.UseVisualStyleBackColor = false;
            this.btn_QA_Close.Click += new System.EventHandler(this.Btn_QA_Close_Click);
            this.btn_QA_Close.MouseEnter += new System.EventHandler(this.Btn_QA_Close_MouseEnter);
            this.btn_QA_Close.MouseLeave += new System.EventHandler(this.Btn_QA_Close_MouseLeave);
            // 
            // Q_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(758, 303);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_QA_Cancel);
            this.Controls.Add(this.btn_QA_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(758, 343);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(758, 303);
            this.Name = "Q_Add";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Quote Sent";
            this.Load += new System.EventHandler(this.Q_Add_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Q_Add_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Q_Add_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Q_Add_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_QA_Close;
        private System.Windows.Forms.Button btn_QA_Cancel;
        private System.Windows.Forms.Button button3;
    }
}