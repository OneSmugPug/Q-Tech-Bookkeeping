namespace Q_Tech_Bookkeeping
{
    partial class O_Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(O_Add));
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_OA_Cancel = new System.Windows.Forms.Button();
            this.btn_OA_Done = new System.Windows.Forms.Button();
            this.btn_OA_Close = new System.Windows.Forms.Button();
            this.gb_OA_ODetails = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_OA_QNum = new System.Windows.Forms.TextBox();
            this.ln_OA_QNum = new Bunifu.Framework.UI.BunifuSeparator();
            this.gb_OA_ODetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(307, 10);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(134, 22);
            this.bunifuCustomLabel1.TabIndex = 0;
            this.bunifuCustomLabel1.Text = "Add New Order";
            // 
            // btn_OA_Cancel
            // 
            this.btn_OA_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_OA_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_OA_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_OA_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OA_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OA_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_OA_Cancel.Location = new System.Drawing.Point(633, 290);
            this.btn_OA_Cancel.Name = "btn_OA_Cancel";
            this.btn_OA_Cancel.Size = new System.Drawing.Size(114, 40);
            this.btn_OA_Cancel.TabIndex = 2;
            this.btn_OA_Cancel.Text = "Cancel";
            this.btn_OA_Cancel.UseVisualStyleBackColor = true;
            this.btn_OA_Cancel.Click += new System.EventHandler(this.Btn_OA_Cancel_Click);
            this.btn_OA_Cancel.MouseEnter += new System.EventHandler(this.Btn_OA_Cancel_MouseEnter);
            this.btn_OA_Cancel.MouseLeave += new System.EventHandler(this.Btn_OA_Cancel_MouseLeave);
            // 
            // btn_OA_Done
            // 
            this.btn_OA_Done.FlatAppearance.BorderSize = 0;
            this.btn_OA_Done.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_OA_Done.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_OA_Done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OA_Done.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OA_Done.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_OA_Done.Location = new System.Drawing.Point(513, 290);
            this.btn_OA_Done.Name = "btn_OA_Done";
            this.btn_OA_Done.Size = new System.Drawing.Size(114, 40);
            this.btn_OA_Done.TabIndex = 3;
            this.btn_OA_Done.Text = "Done";
            this.btn_OA_Done.UseVisualStyleBackColor = true;
            this.btn_OA_Done.Click += new System.EventHandler(this.Btn_OA_Done_Click);
            this.btn_OA_Done.MouseEnter += new System.EventHandler(this.Btn_OA_Done_MouseEnter);
            this.btn_OA_Done.MouseLeave += new System.EventHandler(this.Btn_OA_Done_MouseLeave);
            // 
            // btn_OA_Close
            // 
            this.btn_OA_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OA_Close.FlatAppearance.BorderSize = 0;
            this.btn_OA_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_OA_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_OA_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OA_Close.Image = global::Properties.Resources.close_black;
            this.btn_OA_Close.Location = new System.Drawing.Point(724, 5);
            this.btn_OA_Close.Name = "btn_OA_Close";
            this.btn_OA_Close.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.btn_OA_Close.Size = new System.Drawing.Size(31, 29);
            this.btn_OA_Close.TabIndex = 1;
            this.btn_OA_Close.TabStop = false;
            this.btn_OA_Close.UseVisualStyleBackColor = false;
            this.btn_OA_Close.Click += new System.EventHandler(this.Btn_OA_Close_Click);
            this.btn_OA_Close.MouseEnter += new System.EventHandler(this.Btn_OA_Close_MouseEnter);
            this.btn_OA_Close.MouseLeave += new System.EventHandler(this.Btn_OA_Close_MouseLeave);
            // 
            // gb_OA_ODetails
            // 
            this.gb_OA_ODetails.Controls.Add(this.panel1);
            this.gb_OA_ODetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.gb_OA_ODetails.Location = new System.Drawing.Point(12, 119);
            this.gb_OA_ODetails.Name = "gb_OA_ODetails";
            this.gb_OA_ODetails.Size = new System.Drawing.Size(735, 165);
            this.gb_OA_ODetails.TabIndex = 4;
            this.gb_OA_ODetails.TabStop = false;
            this.gb_OA_ODetails.Text = "Order Details";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ln_OA_QNum);
            this.panel1.Controls.Add(this.txt_OA_QNum);
            this.panel1.Location = new System.Drawing.Point(51, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // txt_OA_QNum
            // 
            this.txt_OA_QNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_OA_QNum.BackColor = System.Drawing.Color.Silver;
            this.txt_OA_QNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_OA_QNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_OA_QNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.txt_OA_QNum.Location = new System.Drawing.Point(2, 6);
            this.txt_OA_QNum.Name = "txt_OA_QNum";
            this.txt_OA_QNum.Size = new System.Drawing.Size(153, 16);
            this.txt_OA_QNum.TabIndex = 0;
            // 
            // ln_OA_QNum
            // 
            this.ln_OA_QNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ln_OA_QNum.BackColor = System.Drawing.Color.Transparent;
            this.ln_OA_QNum.LineColor = System.Drawing.Color.Gray;
            this.ln_OA_QNum.LineThickness = 1;
            this.ln_OA_QNum.Location = new System.Drawing.Point(-1, 18);
            this.ln_OA_QNum.Name = "ln_OA_QNum";
            this.ln_OA_QNum.Size = new System.Drawing.Size(158, 10);
            this.ln_OA_QNum.TabIndex = 1;
            this.ln_OA_QNum.TabStop = false;
            this.ln_OA_QNum.Transparency = 255;
            this.ln_OA_QNum.Vertical = false;
            // 
            // O_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(758, 343);
            this.Controls.Add(this.gb_OA_ODetails);
            this.Controls.Add(this.btn_OA_Done);
            this.Controls.Add(this.btn_OA_Cancel);
            this.Controls.Add(this.btn_OA_Close);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(758, 343);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(758, 343);
            this.Name = "O_Add";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Date";
            this.Load += new System.EventHandler(this.O_Add_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.O_Add_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.O_Add_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.O_Add_MouseUp);
            this.gb_OA_ODetails.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.Button btn_OA_Close;
        private System.Windows.Forms.Button btn_OA_Cancel;
        private System.Windows.Forms.Button btn_OA_Done;
        private System.Windows.Forms.GroupBox gb_OA_ODetails;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuSeparator ln_OA_QNum;
        private System.Windows.Forms.TextBox txt_OA_QNum;
    }
}