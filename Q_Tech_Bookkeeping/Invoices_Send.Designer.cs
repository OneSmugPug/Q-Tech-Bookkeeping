namespace Q_Tech_Bookkeeping
{
    partial class Invoices_Send
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoices_Send));
            this.txt_LIS_CCode = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_LIS_CName = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.dgv_LInvSent = new ADGV.AdvancedDataGridView();
            this.dtp_LIS_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtp_LIS_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.btn_LIS_Prev = new System.Windows.Forms.Button();
            this.btn_LIS_Next = new System.Windows.Forms.Button();
            this.btn_LIS_SelCli = new System.Windows.Forms.Button();
            this.btn_LIS_Filter = new System.Windows.Forms.Button();
            this.btn_LIS_NewIS = new System.Windows.Forms.Button();
            this.btn_LIS_ClearFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LInvSent)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_LIS_CCode
            // 
            this.txt_LIS_CCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_LIS_CCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_LIS_CCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LIS_CCode.HintForeColor = System.Drawing.Color.Empty;
            this.txt_LIS_CCode.HintText = "";
            this.txt_LIS_CCode.isPassword = false;
            this.txt_LIS_CCode.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LIS_CCode.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_LIS_CCode.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LIS_CCode.LineThickness = 1;
            this.txt_LIS_CCode.Location = new System.Drawing.Point(254, 49);
            this.txt_LIS_CCode.Margin = new System.Windows.Forms.Padding(4);
            this.txt_LIS_CCode.Name = "txt_LIS_CCode";
            this.txt_LIS_CCode.Size = new System.Drawing.Size(379, 33);
            this.txt_LIS_CCode.TabIndex = 31;
            this.txt_LIS_CCode.Text = "bunifuMaterialTextbox2";
            this.txt_LIS_CCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_LIS_CCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_LIS_CCode_KeyDown);
            // 
            // txt_LIS_CName
            // 
            this.txt_LIS_CName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_LIS_CName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_LIS_CName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LIS_CName.HintForeColor = System.Drawing.Color.Empty;
            this.txt_LIS_CName.HintText = "";
            this.txt_LIS_CName.isPassword = false;
            this.txt_LIS_CName.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LIS_CName.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_LIS_CName.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LIS_CName.LineThickness = 1;
            this.txt_LIS_CName.Location = new System.Drawing.Point(255, 114);
            this.txt_LIS_CName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_LIS_CName.Name = "txt_LIS_CName";
            this.txt_LIS_CName.Size = new System.Drawing.Size(379, 33);
            this.txt_LIS_CName.TabIndex = 30;
            this.txt_LIS_CName.Text = "bunifuMaterialTextbox1";
            this.txt_LIS_CName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_LIS_CName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_LIS_CName_KeyDown);
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(133, 55);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(113, 24);
            this.bunifuCustomLabel4.TabIndex = 29;
            this.bunifuCustomLabel4.Text = "Client Code:";
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(128, 116);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(118, 24);
            this.bunifuCustomLabel3.TabIndex = 28;
            this.bunifuCustomLabel3.Text = "Client Name:";
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(13, 241);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(50, 20);
            this.bunifuCustomLabel2.TabIndex = 27;
            this.bunifuCustomLabel2.Text = "From:";
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(285, 241);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(31, 20);
            this.bunifuCustomLabel1.TabIndex = 26;
            this.bunifuCustomLabel1.Text = "To:";
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(18, 206);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(934, 35);
            this.bunifuSeparator1.TabIndex = 25;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // dgv_LInvSent
            // 
            this.dgv_LInvSent.AllowUserToAddRows = false;
            this.dgv_LInvSent.AllowUserToDeleteRows = false;
            this.dgv_LInvSent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_LInvSent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_LInvSent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_LInvSent.AutoGenerateContextFilters = true;
            this.dgv_LInvSent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_LInvSent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_LInvSent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_LInvSent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_LInvSent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_LInvSent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_LInvSent.DateWithTime = false;
            this.dgv_LInvSent.EnableHeadersVisualStyles = false;
            this.dgv_LInvSent.Location = new System.Drawing.Point(0, 277);
            this.dgv_LInvSent.Name = "dgv_LInvSent";
            this.dgv_LInvSent.ReadOnly = true;
            this.dgv_LInvSent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_LInvSent.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_LInvSent.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_LInvSent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_LInvSent.Size = new System.Drawing.Size(963, 340);
            this.dgv_LInvSent.TabIndex = 24;
            this.dgv_LInvSent.TimeFilter = false;
            this.dgv_LInvSent.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_LInvSent_CellDoubleClick);
            // 
            // dtp_LIS_To
            // 
            this.dtp_LIS_To.BackColor = System.Drawing.Color.LightGray;
            this.dtp_LIS_To.BorderRadius = 0;
            this.dtp_LIS_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LIS_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LIS_To.FormatCustom = null;
            this.dtp_LIS_To.Location = new System.Drawing.Point(325, 234);
            this.dtp_LIS_To.Name = "dtp_LIS_To";
            this.dtp_LIS_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_LIS_To.TabIndex = 23;
            this.dtp_LIS_To.Value = new System.DateTime(2019, 9, 13, 13, 43, 53, 408);
            // 
            // dtp_LIS_From
            // 
            this.dtp_LIS_From.BackColor = System.Drawing.Color.LightGray;
            this.dtp_LIS_From.BorderRadius = 0;
            this.dtp_LIS_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LIS_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LIS_From.FormatCustom = null;
            this.dtp_LIS_From.Location = new System.Drawing.Point(71, 234);
            this.dtp_LIS_From.Name = "dtp_LIS_From";
            this.dtp_LIS_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_LIS_From.TabIndex = 22;
            this.dtp_LIS_From.Value = new System.DateTime(2019, 9, 13, 13, 43, 50, 212);
            // 
            // btn_LIS_Prev
            // 
            this.btn_LIS_Prev.Enabled = false;
            this.btn_LIS_Prev.FlatAppearance.BorderSize = 0;
            this.btn_LIS_Prev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIS_Prev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIS_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIS_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIS_Prev.Image = global::Properties.Resources.back_black;
            this.btn_LIS_Prev.Location = new System.Drawing.Point(18, 20);
            this.btn_LIS_Prev.Name = "btn_LIS_Prev";
            this.btn_LIS_Prev.Size = new System.Drawing.Size(49, 149);
            this.btn_LIS_Prev.TabIndex = 21;
            this.btn_LIS_Prev.UseVisualStyleBackColor = true;
            this.btn_LIS_Prev.Click += new System.EventHandler(this.btn_LIS_Prev_Click);
            this.btn_LIS_Prev.MouseEnter += new System.EventHandler(this.btn_LIS_Prev_MouseEnter);
            this.btn_LIS_Prev.MouseLeave += new System.EventHandler(this.btn_LIS_Prev_MouseLeave);
            // 
            // btn_LIS_Next
            // 
            this.btn_LIS_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LIS_Next.FlatAppearance.BorderSize = 0;
            this.btn_LIS_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIS_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIS_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIS_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIS_Next.Image = global::Properties.Resources.forawrd_black;
            this.btn_LIS_Next.Location = new System.Drawing.Point(898, 20);
            this.btn_LIS_Next.Name = "btn_LIS_Next";
            this.btn_LIS_Next.Size = new System.Drawing.Size(49, 149);
            this.btn_LIS_Next.TabIndex = 20;
            this.btn_LIS_Next.UseVisualStyleBackColor = true;
            this.btn_LIS_Next.Click += new System.EventHandler(this.btn_LIS_Next_Click);
            this.btn_LIS_Next.MouseEnter += new System.EventHandler(this.btn_LIS_Next_MouseEnter);
            this.btn_LIS_Next.MouseLeave += new System.EventHandler(this.btn_LIS_Next_MouseLeave);
            // 
            // btn_LIS_SelCli
            // 
            this.btn_LIS_SelCli.FlatAppearance.BorderSize = 0;
            this.btn_LIS_SelCli.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIS_SelCli.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIS_SelCli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIS_SelCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIS_SelCli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LIS_SelCli.Image = global::Properties.Resources.user_list;
            this.btn_LIS_SelCli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LIS_SelCli.Location = new System.Drawing.Point(519, 160);
            this.btn_LIS_SelCli.Name = "btn_LIS_SelCli";
            this.btn_LIS_SelCli.Size = new System.Drawing.Size(114, 40);
            this.btn_LIS_SelCli.TabIndex = 19;
            this.btn_LIS_SelCli.Text = "Client List";
            this.btn_LIS_SelCli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LIS_SelCli.UseVisualStyleBackColor = true;
            this.btn_LIS_SelCli.Click += new System.EventHandler(this.btn_LIS_SelCli_Click);
            this.btn_LIS_SelCli.MouseEnter += new System.EventHandler(this.btn_LIS_SelCli_MouseEnter);
            this.btn_LIS_SelCli.MouseLeave += new System.EventHandler(this.btn_LIS_SelCli_MouseLeave);
            // 
            // btn_LIS_Filter
            // 
            this.btn_LIS_Filter.FlatAppearance.BorderSize = 0;
            this.btn_LIS_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIS_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIS_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIS_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIS_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LIS_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_LIS_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LIS_Filter.Location = new System.Drawing.Point(554, 231);
            this.btn_LIS_Filter.Name = "btn_LIS_Filter";
            this.btn_LIS_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_LIS_Filter.TabIndex = 18;
            this.btn_LIS_Filter.Text = "Filter";
            this.btn_LIS_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LIS_Filter.UseVisualStyleBackColor = true;
            this.btn_LIS_Filter.Click += new System.EventHandler(this.btn_LIS_Filter_Click);
            this.btn_LIS_Filter.MouseEnter += new System.EventHandler(this.btn_LIS_Filter_MouseEnter);
            this.btn_LIS_Filter.MouseLeave += new System.EventHandler(this.btn_LIS_Filter_MouseLeave);
            // 
            // btn_LIS_NewIS
            // 
            this.btn_LIS_NewIS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LIS_NewIS.FlatAppearance.BorderSize = 0;
            this.btn_LIS_NewIS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIS_NewIS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIS_NewIS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIS_NewIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIS_NewIS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LIS_NewIS.Image = global::Properties.Resources.add_grey;
            this.btn_LIS_NewIS.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LIS_NewIS.Location = new System.Drawing.Point(822, 231);
            this.btn_LIS_NewIS.Name = "btn_LIS_NewIS";
            this.btn_LIS_NewIS.Size = new System.Drawing.Size(127, 40);
            this.btn_LIS_NewIS.TabIndex = 17;
            this.btn_LIS_NewIS.Text = "New Invoice";
            this.btn_LIS_NewIS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LIS_NewIS.UseVisualStyleBackColor = true;
            this.btn_LIS_NewIS.Click += new System.EventHandler(this.btn_LIS_NewIS_Click);
            this.btn_LIS_NewIS.MouseEnter += new System.EventHandler(this.btn_LIS_NewIS_MouseEnter);
            this.btn_LIS_NewIS.MouseLeave += new System.EventHandler(this.btn_LIS_NewIS_MouseLeave);
            // 
            // btn_LIS_ClearFilter
            // 
            this.btn_LIS_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_LIS_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LIS_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LIS_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LIS_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LIS_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LIS_ClearFilter.Location = new System.Drawing.Point(554, 231);
            this.btn_LIS_ClearFilter.Name = "btn_LIS_ClearFilter";
            this.btn_LIS_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_LIS_ClearFilter.TabIndex = 16;
            this.btn_LIS_ClearFilter.Text = "Clear Filter";
            this.btn_LIS_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_LIS_ClearFilter.Visible = false;
            this.btn_LIS_ClearFilter.Click += new System.EventHandler(this.btn_LIS_ClearFilter_Click);
            this.btn_LIS_ClearFilter.MouseEnter += new System.EventHandler(this.btn_LIS_ClearFilter_MouseEnter);
            this.btn_LIS_ClearFilter.MouseLeave += new System.EventHandler(this.btn_LIS_ClearFilter_MouseLeave);
            // 
            // Invoices_Send
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.txt_LIS_CCode);
            this.Controls.Add(this.txt_LIS_CName);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.dgv_LInvSent);
            this.Controls.Add(this.dtp_LIS_To);
            this.Controls.Add(this.dtp_LIS_From);
            this.Controls.Add(this.btn_LIS_Prev);
            this.Controls.Add(this.btn_LIS_Next);
            this.Controls.Add(this.btn_LIS_SelCli);
            this.Controls.Add(this.btn_LIS_Filter);
            this.Controls.Add(this.btn_LIS_NewIS);
            this.Controls.Add(this.btn_LIS_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(850, 510);
            this.Name = "Invoices_Send";
            this.Text = "Invoices_Send";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Invoices_Send_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LInvSent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_LIS_CCode;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_LIS_CName;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private ADGV.AdvancedDataGridView dgv_LInvSent;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LIS_To;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LIS_From;
        private System.Windows.Forms.Button btn_LIS_Prev;
        private System.Windows.Forms.Button btn_LIS_Next;
        private System.Windows.Forms.Button btn_LIS_SelCli;
        private System.Windows.Forms.Button btn_LIS_Filter;
        private System.Windows.Forms.Button btn_LIS_NewIS;
        private System.Windows.Forms.Button btn_LIS_ClearFilter;
    }
}