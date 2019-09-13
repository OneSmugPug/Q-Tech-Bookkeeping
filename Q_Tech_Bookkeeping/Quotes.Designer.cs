namespace Q_Tech_Bookkeeping
{
    partial class Quotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Quotes));
            this.btn_LQ_ClearFilter = new System.Windows.Forms.Button();
            this.btn_C_NewWW = new System.Windows.Forms.Button();
            this.btn_LQ_Filter = new System.Windows.Forms.Button();
            this.btn_LQ_SelCli = new System.Windows.Forms.Button();
            this.btn_LQ_Next = new System.Windows.Forms.Button();
            this.btn_LQ_Prev = new System.Windows.Forms.Button();
            this.dtp_LQ_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtp_LQ_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dgv_Contractors = new ADGV.AdvancedDataGridView();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txt_LQ_CName = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_LQ_CCode = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Contractors)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_LQ_ClearFilter
            // 
            this.btn_LQ_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_LQ_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LQ_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LQ_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LQ_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LQ_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LQ_ClearFilter.Location = new System.Drawing.Point(553, 232);
            this.btn_LQ_ClearFilter.Name = "btn_LQ_ClearFilter";
            this.btn_LQ_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_LQ_ClearFilter.TabIndex = 0;
            this.btn_LQ_ClearFilter.Text = "Clear Filter";
            this.btn_LQ_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_LQ_ClearFilter.Visible = false;
            this.btn_LQ_ClearFilter.Click += new System.EventHandler(this.Btn_LQ_ClearFilter_Click);
            this.btn_LQ_ClearFilter.MouseEnter += new System.EventHandler(this.Btn_LQ_ClearFilter_MouseEnter);
            this.btn_LQ_ClearFilter.MouseLeave += new System.EventHandler(this.Btn_LO_ClearFilter_MouseLeave);
            // 
            // btn_C_NewWW
            // 
            this.btn_C_NewWW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_C_NewWW.FlatAppearance.BorderSize = 0;
            this.btn_C_NewWW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_C_NewWW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_C_NewWW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_C_NewWW.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_C_NewWW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_C_NewWW.Image = global::Properties.Resources.add_grey;
            this.btn_C_NewWW.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_C_NewWW.Location = new System.Drawing.Point(826, 232);
            this.btn_C_NewWW.Name = "btn_C_NewWW";
            this.btn_C_NewWW.Size = new System.Drawing.Size(122, 40);
            this.btn_C_NewWW.TabIndex = 1;
            this.btn_C_NewWW.Text = "New Quote";
            this.btn_C_NewWW.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_C_NewWW.UseVisualStyleBackColor = true;
            this.btn_C_NewWW.Click += new System.EventHandler(this.Btn_LQ_NewQuote_Click);
            this.btn_C_NewWW.MouseEnter += new System.EventHandler(this.Btn_LQ_NewQuote_MouseEnter);
            this.btn_C_NewWW.MouseLeave += new System.EventHandler(this.Btn_LQ_NewQuote_MouseLeave);
            // 
            // btn_LQ_Filter
            // 
            this.btn_LQ_Filter.FlatAppearance.BorderSize = 0;
            this.btn_LQ_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LQ_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LQ_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LQ_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LQ_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LQ_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_LQ_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LQ_Filter.Location = new System.Drawing.Point(553, 232);
            this.btn_LQ_Filter.Name = "btn_LQ_Filter";
            this.btn_LQ_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_LQ_Filter.TabIndex = 2;
            this.btn_LQ_Filter.Text = "Filter";
            this.btn_LQ_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LQ_Filter.UseVisualStyleBackColor = true;
            this.btn_LQ_Filter.Click += new System.EventHandler(this.Btn_LQ_Filter_Click);
            this.btn_LQ_Filter.MouseEnter += new System.EventHandler(this.Btn_LQ_Filter_MouseEnter);
            this.btn_LQ_Filter.MouseLeave += new System.EventHandler(this.Btn_LQ_Filter_MouseLeave);
            // 
            // btn_LQ_SelCli
            // 
            this.btn_LQ_SelCli.FlatAppearance.BorderSize = 0;
            this.btn_LQ_SelCli.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LQ_SelCli.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LQ_SelCli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LQ_SelCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LQ_SelCli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LQ_SelCli.Image = global::Properties.Resources.user_list;
            this.btn_LQ_SelCli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LQ_SelCli.Location = new System.Drawing.Point(518, 161);
            this.btn_LQ_SelCli.Name = "btn_LQ_SelCli";
            this.btn_LQ_SelCli.Size = new System.Drawing.Size(114, 40);
            this.btn_LQ_SelCli.TabIndex = 3;
            this.btn_LQ_SelCli.Text = "Client List";
            this.btn_LQ_SelCli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LQ_SelCli.UseVisualStyleBackColor = true;
            this.btn_LQ_SelCli.Click += new System.EventHandler(this.Btn_LQ_SelCli_Click);
            this.btn_LQ_SelCli.MouseEnter += new System.EventHandler(this.Btn_LQ_SelCli_MouseEnter);
            this.btn_LQ_SelCli.MouseLeave += new System.EventHandler(this.Btn_LQ_SelCli_MouseLeave);
            // 
            // btn_LQ_Next
            // 
            this.btn_LQ_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LQ_Next.FlatAppearance.BorderSize = 0;
            this.btn_LQ_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LQ_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LQ_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LQ_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LQ_Next.Image = global::Properties.Resources.forawrd_black;
            this.btn_LQ_Next.Location = new System.Drawing.Point(897, 21);
            this.btn_LQ_Next.Name = "btn_LQ_Next";
            this.btn_LQ_Next.Size = new System.Drawing.Size(49, 149);
            this.btn_LQ_Next.TabIndex = 4;
            this.btn_LQ_Next.UseVisualStyleBackColor = true;
            this.btn_LQ_Next.Click += new System.EventHandler(this.Btn_LQ_Next_Click);
            this.btn_LQ_Next.MouseEnter += new System.EventHandler(this.Btn_LQ_Next_MouseEnter);
            this.btn_LQ_Next.MouseLeave += new System.EventHandler(this.Btn_LQ_Next_MouseLeave);
            // 
            // btn_LQ_Prev
            // 
            this.btn_LQ_Prev.Enabled = false;
            this.btn_LQ_Prev.FlatAppearance.BorderSize = 0;
            this.btn_LQ_Prev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LQ_Prev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LQ_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LQ_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LQ_Prev.Image = global::Properties.Resources.back_black;
            this.btn_LQ_Prev.Location = new System.Drawing.Point(17, 21);
            this.btn_LQ_Prev.Name = "btn_LQ_Prev";
            this.btn_LQ_Prev.Size = new System.Drawing.Size(49, 149);
            this.btn_LQ_Prev.TabIndex = 5;
            this.btn_LQ_Prev.UseVisualStyleBackColor = true;
            this.btn_LQ_Prev.Click += new System.EventHandler(this.Btn_LQ_Prev_Click);
            this.btn_LQ_Prev.MouseEnter += new System.EventHandler(this.Btn_LQ_Prev_MouseEnter);
            this.btn_LQ_Prev.MouseLeave += new System.EventHandler(this.Btn_LQ_Prev_MouseLeave);
            // 
            // dtp_LQ_From
            // 
            this.dtp_LQ_From.BackColor = System.Drawing.Color.LightGray;
            this.dtp_LQ_From.BorderRadius = 0;
            this.dtp_LQ_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LQ_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LQ_From.FormatCustom = null;
            this.dtp_LQ_From.Location = new System.Drawing.Point(70, 235);
            this.dtp_LQ_From.Name = "dtp_LQ_From";
            this.dtp_LQ_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_LQ_From.TabIndex = 6;
            this.dtp_LQ_From.Value = new System.DateTime(2019, 9, 13, 13, 43, 50, 212);
            // 
            // dtp_LQ_To
            // 
            this.dtp_LQ_To.BackColor = System.Drawing.Color.LightGray;
            this.dtp_LQ_To.BorderRadius = 0;
            this.dtp_LQ_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LQ_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LQ_To.FormatCustom = null;
            this.dtp_LQ_To.Location = new System.Drawing.Point(324, 235);
            this.dtp_LQ_To.Name = "dtp_LQ_To";
            this.dtp_LQ_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_LQ_To.TabIndex = 7;
            this.dtp_LQ_To.Value = new System.DateTime(2019, 9, 13, 13, 43, 53, 408);
            // 
            // dgv_Contractors
            // 
            this.dgv_Contractors.AllowUserToAddRows = false;
            this.dgv_Contractors.AllowUserToDeleteRows = false;
            this.dgv_Contractors.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_Contractors.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Contractors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Contractors.AutoGenerateContextFilters = true;
            this.dgv_Contractors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Contractors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Contractors.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_Contractors.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Contractors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Contractors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Contractors.DateWithTime = false;
            this.dgv_Contractors.EnableHeadersVisualStyles = false;
            this.dgv_Contractors.Location = new System.Drawing.Point(-1, 278);
            this.dgv_Contractors.Name = "dgv_Contractors";
            this.dgv_Contractors.ReadOnly = true;
            this.dgv_Contractors.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_Contractors.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_Contractors.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Contractors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Contractors.Size = new System.Drawing.Size(963, 340);
            this.dgv_Contractors.TabIndex = 8;
            this.dgv_Contractors.TimeFilter = false;
            this.dgv_Contractors.SortStringChanged += new System.EventHandler(this.Dgv_LQuotes_SortStringChanged);
            this.dgv_Contractors.FilterStringChanged += new System.EventHandler(this.Dgv_LQuotes_FilterStringChanged);
            this.dgv_Contractors.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_LQuotes_CellDoubleClick);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.DimGray;
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(17, 207);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(934, 35);
            this.bunifuSeparator1.TabIndex = 9;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(284, 242);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(31, 20);
            this.bunifuCustomLabel1.TabIndex = 10;
            this.bunifuCustomLabel1.Text = "To:";
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(12, 242);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(50, 20);
            this.bunifuCustomLabel2.TabIndex = 11;
            this.bunifuCustomLabel2.Text = "From:";
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(127, 117);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(118, 24);
            this.bunifuCustomLabel3.TabIndex = 12;
            this.bunifuCustomLabel3.Text = "Client Name:";
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(132, 56);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(113, 24);
            this.bunifuCustomLabel4.TabIndex = 13;
            this.bunifuCustomLabel4.Text = "Client Code:";
            // 
            // txt_LQ_CName
            // 
            this.txt_LQ_CName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_LQ_CName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_LQ_CName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LQ_CName.HintForeColor = System.Drawing.Color.Empty;
            this.txt_LQ_CName.HintText = "";
            this.txt_LQ_CName.isPassword = false;
            this.txt_LQ_CName.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LQ_CName.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_LQ_CName.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LQ_CName.LineThickness = 1;
            this.txt_LQ_CName.Location = new System.Drawing.Point(254, 115);
            this.txt_LQ_CName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_LQ_CName.Name = "txt_LQ_CName";
            this.txt_LQ_CName.Size = new System.Drawing.Size(379, 33);
            this.txt_LQ_CName.TabIndex = 14;
            this.txt_LQ_CName.Text = "bunifuMaterialTextbox1";
            this.txt_LQ_CName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_LQ_CName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_LQ_CName_KeyDown);
            // 
            // txt_LQ_CCode
            // 
            this.txt_LQ_CCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_LQ_CCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_LQ_CCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LQ_CCode.HintForeColor = System.Drawing.Color.Empty;
            this.txt_LQ_CCode.HintText = "";
            this.txt_LQ_CCode.isPassword = false;
            this.txt_LQ_CCode.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LQ_CCode.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_LQ_CCode.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LQ_CCode.LineThickness = 1;
            this.txt_LQ_CCode.Location = new System.Drawing.Point(253, 50);
            this.txt_LQ_CCode.Margin = new System.Windows.Forms.Padding(4);
            this.txt_LQ_CCode.Name = "txt_LQ_CCode";
            this.txt_LQ_CCode.Size = new System.Drawing.Size(379, 33);
            this.txt_LQ_CCode.TabIndex = 15;
            this.txt_LQ_CCode.Text = "bunifuMaterialTextbox2";
            this.txt_LQ_CCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_LQ_CCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_LQ_CCode_KeyDown);
            // 
            // Quotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.txt_LQ_CCode);
            this.Controls.Add(this.txt_LQ_CName);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.dgv_Contractors);
            this.Controls.Add(this.dtp_LQ_To);
            this.Controls.Add(this.dtp_LQ_From);
            this.Controls.Add(this.btn_LQ_Prev);
            this.Controls.Add(this.btn_LQ_Next);
            this.Controls.Add(this.btn_LQ_SelCli);
            this.Controls.Add(this.btn_LQ_Filter);
            this.Controls.Add(this.btn_C_NewWW);
            this.Controls.Add(this.btn_LQ_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 510);
            this.Name = "Quotes";
            this.Text = "Quotes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Quotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Contractors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_LQ_ClearFilter;
        private System.Windows.Forms.Button btn_C_NewWW;
        private System.Windows.Forms.Button btn_LQ_Filter;
        private System.Windows.Forms.Button btn_LQ_SelCli;
        private System.Windows.Forms.Button btn_LQ_Next;
        private System.Windows.Forms.Button btn_LQ_Prev;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LQ_From;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LQ_To;
        private ADGV.AdvancedDataGridView dgv_Contractors;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_LQ_CName;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_LQ_CCode;
    }
}