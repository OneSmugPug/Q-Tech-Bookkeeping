namespace Q_Tech_Bookkeeping
{
    partial class Int_Orders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Int_Orders));
            this.btn_IO_ClearFilter = new System.Windows.Forms.Button();
            this.dtp_IO_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dgv_IOrders = new ADGV.AdvancedDataGridView();
            this.btn_IO_NewOrder = new System.Windows.Forms.Button();
            this.btn_IO_Filter = new System.Windows.Forms.Button();
            this.dtp_IO_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_IO_SelCli = new System.Windows.Forms.Button();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.txt_IO_CName = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_IO_CCode = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.btn_IO_Next = new System.Windows.Forms.Button();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_IO_Prev = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_IO_ClearFilter
            // 
            this.btn_IO_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_IO_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IO_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IO_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IO_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IO_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IO_ClearFilter.Location = new System.Drawing.Point(555, 230);
            this.btn_IO_ClearFilter.Name = "btn_IO_ClearFilter";
            this.btn_IO_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_IO_ClearFilter.TabIndex = 0;
            this.btn_IO_ClearFilter.Text = "Clear Filter";
            this.btn_IO_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_IO_ClearFilter.Visible = false;
            this.btn_IO_ClearFilter.Click += new System.EventHandler(this.Btn_IO_ClearFilter_Click);
            this.btn_IO_ClearFilter.MouseEnter += new System.EventHandler(this.Btn_IO_ClearFilter_MouseEnter);
            this.btn_IO_ClearFilter.MouseLeave += new System.EventHandler(this.Btn_IO_ClearFilter_MouseLeave);
            // 
            // dtp_IO_From
            // 
            this.dtp_IO_From.BackColor = System.Drawing.Color.Silver;
            this.dtp_IO_From.BorderRadius = 0;
            this.dtp_IO_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_IO_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_IO_From.FormatCustom = "null";
            this.dtp_IO_From.Location = new System.Drawing.Point(69, 233);
            this.dtp_IO_From.Name = "dtp_IO_From";
            this.dtp_IO_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_IO_From.TabIndex = 1;
            this.dtp_IO_From.Value = new System.DateTime(2019, 9, 11, 13, 36, 23, 661);
            // 
            // dgv_IOrders
            // 
            this.dgv_IOrders.AllowUserToAddRows = false;
            this.dgv_IOrders.AllowUserToDeleteRows = false;
            this.dgv_IOrders.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.dgv_IOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_IOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_IOrders.AutoGenerateContextFilters = true;
            this.dgv_IOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_IOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_IOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_IOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_IOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_IOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_IOrders.DateWithTime = false;
            this.dgv_IOrders.EnableHeadersVisualStyles = false;
            this.dgv_IOrders.Location = new System.Drawing.Point(0, 276);
            this.dgv_IOrders.Name = "dgv_IOrders";
            this.dgv_IOrders.ReadOnly = true;
            this.dgv_IOrders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_IOrders.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_IOrders.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_IOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_IOrders.Size = new System.Drawing.Size(963, 342);
            this.dgv_IOrders.TabIndex = 2;
            this.dgv_IOrders.TimeFilter = false;
            this.dgv_IOrders.SortStringChanged += new System.EventHandler(this.Dgv_IOrders_SortStringChanged);
            this.dgv_IOrders.FilterStringChanged += new System.EventHandler(this.Dgv_IOrders_FilterStringChanged);
            this.dgv_IOrders.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_IOrders_CellDoubleClick);
            // 
            // btn_IO_NewOrder
            // 
            this.btn_IO_NewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_IO_NewOrder.FlatAppearance.BorderSize = 0;
            this.btn_IO_NewOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IO_NewOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IO_NewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IO_NewOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IO_NewOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IO_NewOrder.Image = global::Properties.Resources.add_grey;
            this.btn_IO_NewOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IO_NewOrder.Location = new System.Drawing.Point(831, 230);
            this.btn_IO_NewOrder.Name = "btn_IO_NewOrder";
            this.btn_IO_NewOrder.Size = new System.Drawing.Size(114, 40);
            this.btn_IO_NewOrder.TabIndex = 3;
            this.btn_IO_NewOrder.Text = "New Order";
            this.btn_IO_NewOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IO_NewOrder.UseVisualStyleBackColor = true;
            this.btn_IO_NewOrder.Click += new System.EventHandler(this.Btn_IO_NewOrder_Click);
            this.btn_IO_NewOrder.MouseEnter += new System.EventHandler(this.Btn_IO_NewOrder_MouseEnter);
            this.btn_IO_NewOrder.MouseLeave += new System.EventHandler(this.Btn_IO_NewOrder_MouseLeave);
            // 
            // btn_IO_Filter
            // 
            this.btn_IO_Filter.FlatAppearance.BorderSize = 0;
            this.btn_IO_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IO_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IO_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IO_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IO_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IO_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_IO_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IO_Filter.Location = new System.Drawing.Point(555, 230);
            this.btn_IO_Filter.Name = "btn_IO_Filter";
            this.btn_IO_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_IO_Filter.TabIndex = 4;
            this.btn_IO_Filter.Text = "Filter";
            this.btn_IO_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IO_Filter.UseVisualStyleBackColor = true;
            this.btn_IO_Filter.Click += new System.EventHandler(this.Btn_IO_Filter_Click);
            this.btn_IO_Filter.MouseEnter += new System.EventHandler(this.Btn_IO_Filter_MouseEnter);
            this.btn_IO_Filter.MouseLeave += new System.EventHandler(this.Btn_IO_Filter_MouseLeave);
            // 
            // dtp_IO_To
            // 
            this.dtp_IO_To.BackColor = System.Drawing.Color.Silver;
            this.dtp_IO_To.BorderRadius = 0;
            this.dtp_IO_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_IO_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_IO_To.FormatCustom = "null";
            this.dtp_IO_To.Location = new System.Drawing.Point(323, 233);
            this.dtp_IO_To.Name = "dtp_IO_To";
            this.dtp_IO_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_IO_To.TabIndex = 5;
            this.dtp_IO_To.Value = new System.DateTime(2019, 9, 11, 14, 6, 17, 479);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(283, 240);
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
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(11, 240);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(50, 20);
            this.bunifuCustomLabel2.TabIndex = 7;
            this.bunifuCustomLabel2.Text = "From:";
            // 
            // btn_IO_SelCli
            // 
            this.btn_IO_SelCli.FlatAppearance.BorderSize = 0;
            this.btn_IO_SelCli.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IO_SelCli.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IO_SelCli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IO_SelCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IO_SelCli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IO_SelCli.Image = global::Properties.Resources.user_list;
            this.btn_IO_SelCli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IO_SelCli.Location = new System.Drawing.Point(517, 159);
            this.btn_IO_SelCli.Name = "btn_IO_SelCli";
            this.btn_IO_SelCli.Size = new System.Drawing.Size(114, 40);
            this.btn_IO_SelCli.TabIndex = 8;
            this.btn_IO_SelCli.Text = "Client List";
            this.btn_IO_SelCli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IO_SelCli.UseVisualStyleBackColor = true;
            this.btn_IO_SelCli.Click += new System.EventHandler(this.Btn_IO_SelCli_Click);
            this.btn_IO_SelCli.MouseEnter += new System.EventHandler(this.Btn_IO_SelCli_MouseEnter);
            this.btn_IO_SelCli.MouseLeave += new System.EventHandler(this.Btn_IO_SelCli_MouseLeave);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.Gray;
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(16, 205);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(929, 35);
            this.bunifuSeparator1.TabIndex = 9;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // txt_IO_CName
            // 
            this.txt_IO_CName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_IO_CName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_IO_CName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IO_CName.HintForeColor = System.Drawing.Color.Empty;
            this.txt_IO_CName.HintText = "";
            this.txt_IO_CName.isPassword = false;
            this.txt_IO_CName.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IO_CName.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_IO_CName.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IO_CName.LineThickness = 1;
            this.txt_IO_CName.Location = new System.Drawing.Point(253, 113);
            this.txt_IO_CName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_IO_CName.Name = "txt_IO_CName";
            this.txt_IO_CName.Size = new System.Drawing.Size(379, 33);
            this.txt_IO_CName.TabIndex = 10;
            this.txt_IO_CName.Text = "bunifuMaterialTextbox1";
            this.txt_IO_CName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_IO_CName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_IO_CName_KeyDown);
            // 
            // txt_IO_CCode
            // 
            this.txt_IO_CCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_IO_CCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_IO_CCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IO_CCode.HintForeColor = System.Drawing.Color.Empty;
            this.txt_IO_CCode.HintText = "";
            this.txt_IO_CCode.isPassword = false;
            this.txt_IO_CCode.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IO_CCode.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_IO_CCode.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IO_CCode.LineThickness = 1;
            this.txt_IO_CCode.Location = new System.Drawing.Point(252, 51);
            this.txt_IO_CCode.Margin = new System.Windows.Forms.Padding(4);
            this.txt_IO_CCode.Name = "txt_IO_CCode";
            this.txt_IO_CCode.Size = new System.Drawing.Size(379, 33);
            this.txt_IO_CCode.TabIndex = 11;
            this.txt_IO_CCode.Text = "bunifuMaterialTextbox1";
            this.txt_IO_CCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_IO_CCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_IO_CCode_KeyDown);
            // 
            // btn_IO_Next
            // 
            this.btn_IO_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_IO_Next.FlatAppearance.BorderSize = 0;
            this.btn_IO_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IO_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IO_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IO_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IO_Next.ForeColor = System.Drawing.Color.White;
            this.btn_IO_Next.Image = global::Properties.Resources.forawrd_black;
            this.btn_IO_Next.Location = new System.Drawing.Point(896, 19);
            this.btn_IO_Next.Name = "btn_IO_Next";
            this.btn_IO_Next.Size = new System.Drawing.Size(49, 149);
            this.btn_IO_Next.TabIndex = 12;
            this.btn_IO_Next.UseVisualStyleBackColor = true;
            this.btn_IO_Next.Click += new System.EventHandler(this.Btn_IO_Next_Click);
            this.btn_IO_Next.MouseEnter += new System.EventHandler(this.Btn_IO_Next_MouseEnter);
            this.btn_IO_Next.MouseLeave += new System.EventHandler(this.Btn_IO_Next_MouseLeave);
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(126, 115);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(118, 24);
            this.bunifuCustomLabel3.TabIndex = 13;
            this.bunifuCustomLabel3.Text = "Client Name:";
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(131, 54);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(113, 24);
            this.bunifuCustomLabel4.TabIndex = 14;
            this.bunifuCustomLabel4.Text = "Client Code:";
            // 
            // btn_IO_Prev
            // 
            this.btn_IO_Prev.Enabled = false;
            this.btn_IO_Prev.FlatAppearance.BorderSize = 0;
            this.btn_IO_Prev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IO_Prev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IO_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IO_Prev.ForeColor = System.Drawing.Color.White;
            this.btn_IO_Prev.Image = global::Properties.Resources.back_black;
            this.btn_IO_Prev.Location = new System.Drawing.Point(16, 19);
            this.btn_IO_Prev.Name = "btn_IO_Prev";
            this.btn_IO_Prev.Size = new System.Drawing.Size(49, 149);
            this.btn_IO_Prev.TabIndex = 15;
            this.btn_IO_Prev.UseVisualStyleBackColor = true;
            this.btn_IO_Prev.Click += new System.EventHandler(this.Btn_IO_Prev_Click);
            this.btn_IO_Prev.MouseEnter += new System.EventHandler(this.Btn_IO_Prev_MouseEnter);
            this.btn_IO_Prev.MouseLeave += new System.EventHandler(this.Btn_IO_Prev_MouseLeave);
            // 
            // Int_Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.btn_IO_Prev);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.btn_IO_Next);
            this.Controls.Add(this.txt_IO_CCode);
            this.Controls.Add(this.txt_IO_CName);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.btn_IO_SelCli);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.dtp_IO_To);
            this.Controls.Add(this.btn_IO_Filter);
            this.Controls.Add(this.btn_IO_NewOrder);
            this.Controls.Add(this.dgv_IOrders);
            this.Controls.Add(this.dtp_IO_From);
            this.Controls.Add(this.btn_IO_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(625, 510);
            this.Name = "Int_Orders";
            this.Text = "International Orders";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Orders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_IO_ClearFilter;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_IO_From;
        private ADGV.AdvancedDataGridView dgv_IOrders;
        private System.Windows.Forms.Button btn_IO_NewOrder;
        private System.Windows.Forms.Button btn_IO_Filter;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_IO_To;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private System.Windows.Forms.Button btn_IO_SelCli;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_IO_CName;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_IO_CCode;
        private System.Windows.Forms.Button btn_IO_Next;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private System.Windows.Forms.Button btn_IO_Prev;
    }
}