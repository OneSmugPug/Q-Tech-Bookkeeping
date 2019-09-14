namespace Q_Tech_Bookkeeping
{
    partial class Int_Invoices_Send
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Int_Invoices_Send));
            this.btn_IIS_ClearFilter = new System.Windows.Forms.Button();
            this.btn_IIS_Filter = new System.Windows.Forms.Button();
            this.btn_IIS_SelCli = new System.Windows.Forms.Button();
            this.btn_IIS_Next = new System.Windows.Forms.Button();
            this.btn_IIS_Prev = new System.Windows.Forms.Button();
            this.btn_IIS_NewIS = new System.Windows.Forms.Button();
            this.dgv_IInvSent = new ADGV.AdvancedDataGridView();
            this.dtp_IIS_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtp_IIS_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.txt_IIS_CName = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_IIS_CCode = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IInvSent)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_IIS_ClearFilter
            // 
            this.btn_IIS_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_IIS_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IIS_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IIS_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IIS_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IIS_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IIS_ClearFilter.Location = new System.Drawing.Point(553, 232);
            this.btn_IIS_ClearFilter.Name = "btn_IIS_ClearFilter";
            this.btn_IIS_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_IIS_ClearFilter.TabIndex = 0;
            this.btn_IIS_ClearFilter.Text = "Clear Filter";
            this.btn_IIS_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_IIS_ClearFilter.Visible = false;
            this.btn_IIS_ClearFilter.Click += new System.EventHandler(this.Btn_IIS_ClearFilter_Click);
            this.btn_IIS_ClearFilter.MouseEnter += new System.EventHandler(this.Btn_IIS_ClearFilter_MouseEnter);
            this.btn_IIS_ClearFilter.MouseLeave += new System.EventHandler(this.Btn_IIS_ClearFilter_MouseLeave);
            // 
            // btn_IIS_Filter
            // 
            this.btn_IIS_Filter.FlatAppearance.BorderSize = 0;
            this.btn_IIS_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IIS_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IIS_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IIS_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IIS_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IIS_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_IIS_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IIS_Filter.Location = new System.Drawing.Point(553, 232);
            this.btn_IIS_Filter.Name = "btn_IIS_Filter";
            this.btn_IIS_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_IIS_Filter.TabIndex = 2;
            this.btn_IIS_Filter.Text = "Filter";
            this.btn_IIS_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IIS_Filter.UseVisualStyleBackColor = true;
            this.btn_IIS_Filter.Click += new System.EventHandler(this.Btn_IIS_Filter_Click);
            this.btn_IIS_Filter.MouseEnter += new System.EventHandler(this.Btn_IIS_Filter_MouseEnter);
            this.btn_IIS_Filter.MouseLeave += new System.EventHandler(this.Btn_IIS_Filter_MouseLeave);
            // 
            // btn_IIS_SelCli
            // 
            this.btn_IIS_SelCli.FlatAppearance.BorderSize = 0;
            this.btn_IIS_SelCli.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IIS_SelCli.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IIS_SelCli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IIS_SelCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IIS_SelCli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IIS_SelCli.Image = global::Properties.Resources.user_list;
            this.btn_IIS_SelCli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IIS_SelCli.Location = new System.Drawing.Point(518, 161);
            this.btn_IIS_SelCli.Name = "btn_IIS_SelCli";
            this.btn_IIS_SelCli.Size = new System.Drawing.Size(114, 40);
            this.btn_IIS_SelCli.TabIndex = 3;
            this.btn_IIS_SelCli.Text = "Client List";
            this.btn_IIS_SelCli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IIS_SelCli.UseVisualStyleBackColor = true;
            this.btn_IIS_SelCli.Click += new System.EventHandler(this.Btn_IIS_SelCli_Click);
            this.btn_IIS_SelCli.MouseEnter += new System.EventHandler(this.Btn_IIS_SelCli_MouseEnter);
            this.btn_IIS_SelCli.MouseLeave += new System.EventHandler(this.Btn_IIS_NewIS_MouseLeave);
            // 
            // btn_IIS_Next
            // 
            this.btn_IIS_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_IIS_Next.FlatAppearance.BorderSize = 0;
            this.btn_IIS_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IIS_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IIS_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IIS_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IIS_Next.Image = global::Properties.Resources.forawrd_black;
            this.btn_IIS_Next.Location = new System.Drawing.Point(897, 21);
            this.btn_IIS_Next.Name = "btn_IIS_Next";
            this.btn_IIS_Next.Size = new System.Drawing.Size(49, 149);
            this.btn_IIS_Next.TabIndex = 4;
            this.btn_IIS_Next.UseVisualStyleBackColor = true;
            this.btn_IIS_Next.Click += new System.EventHandler(this.Btn_IIS_Next_Click);
            this.btn_IIS_Next.MouseEnter += new System.EventHandler(this.Btn_IIS_Next_MouseEnter);
            this.btn_IIS_Next.MouseLeave += new System.EventHandler(this.Btn_IIS_Next_MouseLeave);
            // 
            // btn_IIS_Prev
            // 
            this.btn_IIS_Prev.Enabled = false;
            this.btn_IIS_Prev.FlatAppearance.BorderSize = 0;
            this.btn_IIS_Prev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IIS_Prev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IIS_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IIS_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IIS_Prev.Image = global::Properties.Resources.back_black;
            this.btn_IIS_Prev.Location = new System.Drawing.Point(17, 21);
            this.btn_IIS_Prev.Name = "btn_IIS_Prev";
            this.btn_IIS_Prev.Size = new System.Drawing.Size(49, 149);
            this.btn_IIS_Prev.TabIndex = 5;
            this.btn_IIS_Prev.UseVisualStyleBackColor = true;
            this.btn_IIS_Prev.Click += new System.EventHandler(this.Btn_IIS_Prev_Click);
            this.btn_IIS_Prev.MouseEnter += new System.EventHandler(this.Btn_IIS_Prev_MouseEnter);
            this.btn_IIS_Prev.MouseLeave += new System.EventHandler(this.Btn_IIS_Prev_MouseLeave);
            // 
            // btn_IIS_NewIS
            // 
            this.btn_IIS_NewIS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_IIS_NewIS.FlatAppearance.BorderSize = 0;
            this.btn_IIS_NewIS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IIS_NewIS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IIS_NewIS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IIS_NewIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IIS_NewIS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IIS_NewIS.Image = global::Properties.Resources.add_grey;
            this.btn_IIS_NewIS.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IIS_NewIS.Location = new System.Drawing.Point(825, 232);
            this.btn_IIS_NewIS.Name = "btn_IIS_NewIS";
            this.btn_IIS_NewIS.Size = new System.Drawing.Size(122, 40);
            this.btn_IIS_NewIS.TabIndex = 1;
            this.btn_IIS_NewIS.Text = "New Invoice";
            this.btn_IIS_NewIS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IIS_NewIS.UseVisualStyleBackColor = true;
            this.btn_IIS_NewIS.Click += new System.EventHandler(this.Btn_IIS_NewIS_Click);
            this.btn_IIS_NewIS.MouseEnter += new System.EventHandler(this.Btn_IIS_NewIS_MouseEnter);
            this.btn_IIS_NewIS.MouseLeave += new System.EventHandler(this.Btn_IIS_NewIS_MouseLeave);
            // 
            // dgv_IInvSent
            // 
            this.dgv_IInvSent.AllowUserToAddRows = false;
            this.dgv_IInvSent.AllowUserToDeleteRows = false;
            this.dgv_IInvSent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_IInvSent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_IInvSent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_IInvSent.AutoGenerateContextFilters = true;
            this.dgv_IInvSent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_IInvSent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_IInvSent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_IInvSent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_IInvSent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_IInvSent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_IInvSent.DateWithTime = false;
            this.dgv_IInvSent.EnableHeadersVisualStyles = false;
            this.dgv_IInvSent.Location = new System.Drawing.Point(0, 279);
            this.dgv_IInvSent.Name = "dgv_IInvSent";
            this.dgv_IInvSent.ReadOnly = true;
            this.dgv_IInvSent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_IInvSent.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_IInvSent.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_IInvSent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_IInvSent.Size = new System.Drawing.Size(963, 340);
            this.dgv_IInvSent.TabIndex = 6;
            this.dgv_IInvSent.TimeFilter = false;
            this.dgv_IInvSent.SortStringChanged += new System.EventHandler(this.Dgv_IInvSent_SortStringChanged);
            this.dgv_IInvSent.FilterStringChanged += new System.EventHandler(this.Dgv_IInvSent_FilterStringChanged);
            this.dgv_IInvSent.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_IInvSent_CellDoubleClick);
            // 
            // dtp_IIS_From
            // 
            this.dtp_IIS_From.BackColor = System.Drawing.Color.LightGray;
            this.dtp_IIS_From.BorderRadius = 0;
            this.dtp_IIS_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_IIS_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_IIS_From.FormatCustom = null;
            this.dtp_IIS_From.Location = new System.Drawing.Point(70, 235);
            this.dtp_IIS_From.Name = "dtp_IIS_From";
            this.dtp_IIS_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_IIS_From.TabIndex = 7;
            this.dtp_IIS_From.Value = new System.DateTime(2019, 9, 14, 15, 1, 10, 886);
            // 
            // dtp_IIS_To
            // 
            this.dtp_IIS_To.BackColor = System.Drawing.Color.LightGray;
            this.dtp_IIS_To.BorderRadius = 0;
            this.dtp_IIS_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_IIS_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_IIS_To.FormatCustom = null;
            this.dtp_IIS_To.Location = new System.Drawing.Point(324, 235);
            this.dtp_IIS_To.Name = "dtp_IIS_To";
            this.dtp_IIS_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_IIS_To.TabIndex = 8;
            this.dtp_IIS_To.Value = new System.DateTime(2019, 9, 14, 15, 1, 14, 467);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(284, 242);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(31, 20);
            this.bunifuCustomLabel1.TabIndex = 9;
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
            this.bunifuCustomLabel2.TabIndex = 10;
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
            this.bunifuCustomLabel3.TabIndex = 11;
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
            this.bunifuCustomLabel4.TabIndex = 12;
            this.bunifuCustomLabel4.Text = "Client Code:";
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
            this.bunifuSeparator1.TabIndex = 13;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // txt_IIS_CName
            // 
            this.txt_IIS_CName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_IIS_CName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_IIS_CName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IIS_CName.HintForeColor = System.Drawing.Color.Empty;
            this.txt_IIS_CName.HintText = "";
            this.txt_IIS_CName.isPassword = false;
            this.txt_IIS_CName.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IIS_CName.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_IIS_CName.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IIS_CName.LineThickness = 1;
            this.txt_IIS_CName.Location = new System.Drawing.Point(254, 115);
            this.txt_IIS_CName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_IIS_CName.Name = "txt_IIS_CName";
            this.txt_IIS_CName.Size = new System.Drawing.Size(379, 33);
            this.txt_IIS_CName.TabIndex = 14;
            this.txt_IIS_CName.Text = "bunifuMaterialTextbox1";
            this.txt_IIS_CName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_IIS_CName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_IIS_CName_KeyDown);
            // 
            // txt_IIS_CCode
            // 
            this.txt_IIS_CCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_IIS_CCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_IIS_CCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IIS_CCode.HintForeColor = System.Drawing.Color.Empty;
            this.txt_IIS_CCode.HintText = "";
            this.txt_IIS_CCode.isPassword = false;
            this.txt_IIS_CCode.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IIS_CCode.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_IIS_CCode.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IIS_CCode.LineThickness = 1;
            this.txt_IIS_CCode.Location = new System.Drawing.Point(253, 53);
            this.txt_IIS_CCode.Margin = new System.Windows.Forms.Padding(4);
            this.txt_IIS_CCode.Name = "txt_IIS_CCode";
            this.txt_IIS_CCode.Size = new System.Drawing.Size(379, 31);
            this.txt_IIS_CCode.TabIndex = 15;
            this.txt_IIS_CCode.Text = "bunifuMaterialTextbox1";
            this.txt_IIS_CCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_IIS_CCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_IIS_CCode_KeyDown);
            // 
            // Int_Invoices_Send
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.txt_IIS_CCode);
            this.Controls.Add(this.txt_IIS_CName);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.dtp_IIS_To);
            this.Controls.Add(this.dtp_IIS_From);
            this.Controls.Add(this.dgv_IInvSent);
            this.Controls.Add(this.btn_IIS_Prev);
            this.Controls.Add(this.btn_IIS_Next);
            this.Controls.Add(this.btn_IIS_SelCli);
            this.Controls.Add(this.btn_IIS_Filter);
            this.Controls.Add(this.btn_IIS_NewIS);
            this.Controls.Add(this.btn_IIS_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(963, 618);
            this.Name = "Int_Invoices_Send";
            this.Text = "International Invoices Send";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Invoices_Send_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IInvSent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_IIS_ClearFilter;
        private System.Windows.Forms.Button btn_IIS_NewIS;
        private System.Windows.Forms.Button btn_IIS_Filter;
        private System.Windows.Forms.Button btn_IIS_SelCli;
        private System.Windows.Forms.Button btn_IIS_Next;
        private System.Windows.Forms.Button btn_IIS_Prev;
        private ADGV.AdvancedDataGridView dgv_IInvSent;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_IIS_From;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_IIS_To;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_IIS_CName;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_IIS_CCode;
    }
}