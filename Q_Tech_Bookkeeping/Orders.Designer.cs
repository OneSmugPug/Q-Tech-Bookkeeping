namespace Q_Tech_Bookkeeping
{
    partial class Orders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Orders));
            this.btn_LO_ClearFilter = new System.Windows.Forms.Button();
            this.btn_LO_Filter = new System.Windows.Forms.Button();
            this.btn_LO_SelCli = new System.Windows.Forms.Button();
            this.btn_LO_Next = new System.Windows.Forms.Button();
            this.btn_LO_Prev = new System.Windows.Forms.Button();
            this.btn_LO_NewOrder = new System.Windows.Forms.Button();
            this.dtp_LO_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtp_LO_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dgv_LOrders = new ADGV.AdvancedDataGridView();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txt_LO_CName = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_LO_CCode = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_LO_ClearFilter
            // 
            this.btn_LO_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_LO_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LO_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LO_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LO_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LO_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LO_ClearFilter.Location = new System.Drawing.Point(556, 232);
            this.btn_LO_ClearFilter.Name = "btn_LO_ClearFilter";
            this.btn_LO_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_LO_ClearFilter.TabIndex = 0;
            this.btn_LO_ClearFilter.Text = "Clear Filter";
            this.btn_LO_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_LO_ClearFilter.Visible = false;
            this.btn_LO_ClearFilter.Click += new System.EventHandler(this.Btn_O_ClearF_Click);
            this.btn_LO_ClearFilter.MouseEnter += new System.EventHandler(this.Btn_LO_ClearFilter_MouseEnter);
            this.btn_LO_ClearFilter.MouseLeave += new System.EventHandler(this.Btn_LO_ClearFilter_MouseLeave);
            // 
            // btn_LO_Filter
            // 
            this.btn_LO_Filter.FlatAppearance.BorderSize = 0;
            this.btn_LO_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LO_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LO_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LO_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LO_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LO_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_LO_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LO_Filter.Location = new System.Drawing.Point(556, 232);
            this.btn_LO_Filter.Name = "btn_LO_Filter";
            this.btn_LO_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_LO_Filter.TabIndex = 2;
            this.btn_LO_Filter.Text = "Filter";
            this.btn_LO_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LO_Filter.UseVisualStyleBackColor = true;
            this.btn_LO_Filter.Click += new System.EventHandler(this.Btn_O_FilterD_Click);
            this.btn_LO_Filter.MouseEnter += new System.EventHandler(this.Btn_LO_Filter_MouseEnter);
            this.btn_LO_Filter.MouseLeave += new System.EventHandler(this.Btn_LO_Filter_MouseLeave);
            // 
            // btn_LO_SelCli
            // 
            this.btn_LO_SelCli.FlatAppearance.BorderSize = 0;
            this.btn_LO_SelCli.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LO_SelCli.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LO_SelCli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LO_SelCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LO_SelCli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LO_SelCli.Image = global::Properties.Resources.user_list;
            this.btn_LO_SelCli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LO_SelCli.Location = new System.Drawing.Point(518, 161);
            this.btn_LO_SelCli.Name = "btn_LO_SelCli";
            this.btn_LO_SelCli.Size = new System.Drawing.Size(114, 40);
            this.btn_LO_SelCli.TabIndex = 3;
            this.btn_LO_SelCli.Text = "Client List";
            this.btn_LO_SelCli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LO_SelCli.UseVisualStyleBackColor = true;
            this.btn_LO_SelCli.Click += new System.EventHandler(this.Btn_Order_CBrowse_Click);
            this.btn_LO_SelCli.MouseEnter += new System.EventHandler(this.Btn_LO_SelCli_MouseEnter);
            this.btn_LO_SelCli.MouseLeave += new System.EventHandler(this.Btn_LO_SelCli_MouseLeave);
            // 
            // btn_LO_Next
            // 
            this.btn_LO_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LO_Next.FlatAppearance.BorderSize = 0;
            this.btn_LO_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LO_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LO_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LO_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LO_Next.Image = global::Properties.Resources.forawrd_black;
            this.btn_LO_Next.Location = new System.Drawing.Point(897, 21);
            this.btn_LO_Next.Name = "btn_LO_Next";
            this.btn_LO_Next.Size = new System.Drawing.Size(49, 149);
            this.btn_LO_Next.TabIndex = 4;
            this.btn_LO_Next.UseVisualStyleBackColor = true;
            this.btn_LO_Next.Click += new System.EventHandler(this.Btn_Order_CNext_Click);
            this.btn_LO_Next.MouseEnter += new System.EventHandler(this.Btn_LO_Next_MouseEnter);
            this.btn_LO_Next.MouseLeave += new System.EventHandler(this.Btn_LO_Next_MouseLeave);
            // 
            // btn_LO_Prev
            // 
            this.btn_LO_Prev.Enabled = false;
            this.btn_LO_Prev.FlatAppearance.BorderSize = 0;
            this.btn_LO_Prev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LO_Prev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LO_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LO_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LO_Prev.Image = global::Properties.Resources.back_black;
            this.btn_LO_Prev.Location = new System.Drawing.Point(17, 21);
            this.btn_LO_Prev.Name = "btn_LO_Prev";
            this.btn_LO_Prev.Size = new System.Drawing.Size(49, 149);
            this.btn_LO_Prev.TabIndex = 5;
            this.btn_LO_Prev.UseVisualStyleBackColor = true;
            this.btn_LO_Prev.Click += new System.EventHandler(this.Btn_Order_CPrev_Click);
            this.btn_LO_Prev.MouseEnter += new System.EventHandler(this.Btn_LO_Prev_MouseEnter);
            this.btn_LO_Prev.MouseLeave += new System.EventHandler(this.Btn_LO_Prev_MouseLeave);
            // 
            // btn_LO_NewOrder
            // 
            this.btn_LO_NewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LO_NewOrder.FlatAppearance.BorderSize = 0;
            this.btn_LO_NewOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_LO_NewOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_LO_NewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LO_NewOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LO_NewOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_LO_NewOrder.Image = global::Properties.Resources.add_grey;
            this.btn_LO_NewOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LO_NewOrder.Location = new System.Drawing.Point(832, 232);
            this.btn_LO_NewOrder.Name = "btn_LO_NewOrder";
            this.btn_LO_NewOrder.Size = new System.Drawing.Size(114, 40);
            this.btn_LO_NewOrder.TabIndex = 1;
            this.btn_LO_NewOrder.Text = "New Order";
            this.btn_LO_NewOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_LO_NewOrder.UseVisualStyleBackColor = true;
            this.btn_LO_NewOrder.Click += new System.EventHandler(this.Btn_AddOrder_Click);
            this.btn_LO_NewOrder.MouseEnter += new System.EventHandler(this.Btn_LO_NewOrder_MouseEnter);
            this.btn_LO_NewOrder.MouseLeave += new System.EventHandler(this.Btn_LO_NewOrder_MouseLeave);
            // 
            // dtp_LO_From
            // 
            this.dtp_LO_From.BackColor = System.Drawing.Color.LightGray;
            this.dtp_LO_From.BorderRadius = 0;
            this.dtp_LO_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LO_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LO_From.FormatCustom = null;
            this.dtp_LO_From.Location = new System.Drawing.Point(70, 235);
            this.dtp_LO_From.Name = "dtp_LO_From";
            this.dtp_LO_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_LO_From.TabIndex = 6;
            this.dtp_LO_From.Value = new System.DateTime(2019, 9, 14, 15, 59, 32, 212);
            // 
            // dtp_LO_To
            // 
            this.dtp_LO_To.BackColor = System.Drawing.Color.LightGray;
            this.dtp_LO_To.BorderRadius = 0;
            this.dtp_LO_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_LO_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_LO_To.FormatCustom = null;
            this.dtp_LO_To.Location = new System.Drawing.Point(324, 235);
            this.dtp_LO_To.Name = "dtp_LO_To";
            this.dtp_LO_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_LO_To.TabIndex = 7;
            this.dtp_LO_To.Value = new System.DateTime(2019, 9, 14, 15, 59, 37, 458);
            // 
            // dgv_LOrders
            // 
            this.dgv_LOrders.AllowUserToAddRows = false;
            this.dgv_LOrders.AllowUserToDeleteRows = false;
            this.dgv_LOrders.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_LOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_LOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_LOrders.AutoGenerateContextFilters = true;
            this.dgv_LOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_LOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_LOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_LOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_LOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_LOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_LOrders.DateWithTime = false;
            this.dgv_LOrders.EnableHeadersVisualStyles = false;
            this.dgv_LOrders.Location = new System.Drawing.Point(1, 278);
            this.dgv_LOrders.Name = "dgv_LOrders";
            this.dgv_LOrders.ReadOnly = true;
            this.dgv_LOrders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_LOrders.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_LOrders.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_LOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_LOrders.Size = new System.Drawing.Size(963, 342);
            this.dgv_LOrders.TabIndex = 8;
            this.dgv_LOrders.TimeFilter = false;
            this.dgv_LOrders.SortStringChanged += new System.EventHandler(this.Dgv_Order_SortStringChanged);
            this.dgv_LOrders.FilterStringChanged += new System.EventHandler(this.Dgv_Order_FilterStringChanged);
            this.dgv_LOrders.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Order_CellDoubleClick);
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
            // txt_LO_CName
            // 
            this.txt_LO_CName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_LO_CName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_LO_CName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LO_CName.HintForeColor = System.Drawing.Color.Empty;
            this.txt_LO_CName.HintText = "";
            this.txt_LO_CName.isPassword = false;
            this.txt_LO_CName.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LO_CName.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_LO_CName.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LO_CName.LineThickness = 1;
            this.txt_LO_CName.Location = new System.Drawing.Point(254, 115);
            this.txt_LO_CName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_LO_CName.Name = "txt_LO_CName";
            this.txt_LO_CName.Size = new System.Drawing.Size(379, 33);
            this.txt_LO_CName.TabIndex = 13;
            this.txt_LO_CName.Text = "bunifuMaterialTextbox1";
            this.txt_LO_CName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_LO_CName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_LO_CName_KeyDown);
            // 
            // txt_LO_CCode
            // 
            this.txt_LO_CCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_LO_CCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_LO_CCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LO_CCode.HintForeColor = System.Drawing.Color.Empty;
            this.txt_LO_CCode.HintText = "";
            this.txt_LO_CCode.isPassword = false;
            this.txt_LO_CCode.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LO_CCode.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_LO_CCode.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_LO_CCode.LineThickness = 1;
            this.txt_LO_CCode.Location = new System.Drawing.Point(253, 53);
            this.txt_LO_CCode.Margin = new System.Windows.Forms.Padding(4);
            this.txt_LO_CCode.Name = "txt_LO_CCode";
            this.txt_LO_CCode.Size = new System.Drawing.Size(379, 33);
            this.txt_LO_CCode.TabIndex = 14;
            this.txt_LO_CCode.Text = "bunifuMaterialTextbox1";
            this.txt_LO_CCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_LO_CCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_LO_CCode_KeyDown);
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
            this.bunifuSeparator1.Size = new System.Drawing.Size(929, 35);
            this.bunifuSeparator1.TabIndex = 15;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.txt_LO_CCode);
            this.Controls.Add(this.txt_LO_CName);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.dgv_LOrders);
            this.Controls.Add(this.dtp_LO_To);
            this.Controls.Add(this.dtp_LO_From);
            this.Controls.Add(this.btn_LO_Prev);
            this.Controls.Add(this.btn_LO_Next);
            this.Controls.Add(this.btn_LO_SelCli);
            this.Controls.Add(this.btn_LO_Filter);
            this.Controls.Add(this.btn_LO_NewOrder);
            this.Controls.Add(this.btn_LO_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(963, 618);
            this.Name = "Orders";
            this.Text = "Orders";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Orders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_LOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_LO_ClearFilter;
        private System.Windows.Forms.Button btn_LO_NewOrder;
        private System.Windows.Forms.Button btn_LO_Filter;
        private System.Windows.Forms.Button btn_LO_SelCli;
        private System.Windows.Forms.Button btn_LO_Next;
        private System.Windows.Forms.Button btn_LO_Prev;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LO_From;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_LO_To;
        private ADGV.AdvancedDataGridView dgv_LOrders;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_LO_CName;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_LO_CCode;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
    }
}