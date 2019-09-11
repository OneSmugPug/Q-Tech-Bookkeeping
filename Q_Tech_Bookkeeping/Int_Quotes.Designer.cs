namespace Q_Tech_Bookkeeping
{
    partial class Int_Quotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Int_Quotes));
            this.btn_IQ_ClearFilter = new System.Windows.Forms.Button();
            this.btn_IQ_NewQuote = new System.Windows.Forms.Button();
            this.btn_IQ_Filter = new System.Windows.Forms.Button();
            this.btn_IQ_SelCli = new System.Windows.Forms.Button();
            this.btn_IQ_Next = new System.Windows.Forms.Button();
            this.btn_IQ_Prev = new System.Windows.Forms.Button();
            this.dtp_IQ_To = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtp_IQ_From = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dgv_IQuotes = new ADGV.AdvancedDataGridView();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txt_IQ_CName = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_IQ_CCode = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IQuotes)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_IQ_ClearFilter
            // 
            this.btn_IQ_ClearFilter.FlatAppearance.BorderSize = 0;
            this.btn_IQ_ClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IQ_ClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IQ_ClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IQ_ClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IQ_ClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IQ_ClearFilter.Location = new System.Drawing.Point(554, 233);
            this.btn_IQ_ClearFilter.Name = "btn_IQ_ClearFilter";
            this.btn_IQ_ClearFilter.Size = new System.Drawing.Size(114, 40);
            this.btn_IQ_ClearFilter.TabIndex = 0;
            this.btn_IQ_ClearFilter.Text = "Clear Filter";
            this.btn_IQ_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_IQ_ClearFilter.Visible = false;
            this.btn_IQ_ClearFilter.Click += new System.EventHandler(this.Btn_IQ_ClearFilter_Click);
            this.btn_IQ_ClearFilter.MouseEnter += new System.EventHandler(this.Btn_IQ_ClearFilter_MouseEnter);
            this.btn_IQ_ClearFilter.MouseLeave += new System.EventHandler(this.Btn_IO_ClearFilter_MouseLeave);
            // 
            // btn_IQ_NewQuote
            // 
            this.btn_IQ_NewQuote.FlatAppearance.BorderSize = 0;
            this.btn_IQ_NewQuote.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IQ_NewQuote.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IQ_NewQuote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IQ_NewQuote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IQ_NewQuote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IQ_NewQuote.Image = global::Properties.Resources.add_grey;
            this.btn_IQ_NewQuote.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IQ_NewQuote.Location = new System.Drawing.Point(827, 233);
            this.btn_IQ_NewQuote.Name = "btn_IQ_NewQuote";
            this.btn_IQ_NewQuote.Size = new System.Drawing.Size(122, 40);
            this.btn_IQ_NewQuote.TabIndex = 1;
            this.btn_IQ_NewQuote.Text = "New Quote";
            this.btn_IQ_NewQuote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IQ_NewQuote.UseVisualStyleBackColor = true;
            this.btn_IQ_NewQuote.Click += new System.EventHandler(this.Btn_IQ_NewQuote_Click);
            this.btn_IQ_NewQuote.MouseEnter += new System.EventHandler(this.Btn_IQ_NewQuote_MouseEnter);
            this.btn_IQ_NewQuote.MouseLeave += new System.EventHandler(this.Btn_IQ_NewQuote_MouseLeave);
            // 
            // btn_IQ_Filter
            // 
            this.btn_IQ_Filter.FlatAppearance.BorderSize = 0;
            this.btn_IQ_Filter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IQ_Filter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IQ_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IQ_Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IQ_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IQ_Filter.Image = global::Properties.Resources.filter_grey;
            this.btn_IQ_Filter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IQ_Filter.Location = new System.Drawing.Point(554, 233);
            this.btn_IQ_Filter.Name = "btn_IQ_Filter";
            this.btn_IQ_Filter.Size = new System.Drawing.Size(114, 40);
            this.btn_IQ_Filter.TabIndex = 2;
            this.btn_IQ_Filter.Text = "Filter";
            this.btn_IQ_Filter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IQ_Filter.UseVisualStyleBackColor = true;
            this.btn_IQ_Filter.Click += new System.EventHandler(this.Btn_IQ_Filter_Click);
            this.btn_IQ_Filter.MouseEnter += new System.EventHandler(this.Btn_IQ_Filter_MouseEnter);
            this.btn_IQ_Filter.MouseLeave += new System.EventHandler(this.Btn_IQ_Filter_MouseLeave);
            // 
            // btn_IQ_SelCli
            // 
            this.btn_IQ_SelCli.FlatAppearance.BorderSize = 0;
            this.btn_IQ_SelCli.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IQ_SelCli.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IQ_SelCli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IQ_SelCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IQ_SelCli.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_IQ_SelCli.Image = global::Properties.Resources.user_list;
            this.btn_IQ_SelCli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_IQ_SelCli.Location = new System.Drawing.Point(519, 162);
            this.btn_IQ_SelCli.Name = "btn_IQ_SelCli";
            this.btn_IQ_SelCli.Size = new System.Drawing.Size(114, 40);
            this.btn_IQ_SelCli.TabIndex = 3;
            this.btn_IQ_SelCli.Text = "Client List";
            this.btn_IQ_SelCli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_IQ_SelCli.UseVisualStyleBackColor = true;
            this.btn_IQ_SelCli.Click += new System.EventHandler(this.Btn_IQ_SelCli_Click);
            this.btn_IQ_SelCli.MouseEnter += new System.EventHandler(this.Btn_IQ_SelCli_MouseEnter);
            this.btn_IQ_SelCli.MouseLeave += new System.EventHandler(this.Btn_IQ_SelCli_MouseLeave);
            // 
            // btn_IQ_Next
            // 
            this.btn_IQ_Next.FlatAppearance.BorderSize = 0;
            this.btn_IQ_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IQ_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IQ_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IQ_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_IQ_Next.Image = global::Properties.Resources.forawrd_black;
            this.btn_IQ_Next.Location = new System.Drawing.Point(898, 22);
            this.btn_IQ_Next.Name = "btn_IQ_Next";
            this.btn_IQ_Next.Size = new System.Drawing.Size(49, 149);
            this.btn_IQ_Next.TabIndex = 4;
            this.btn_IQ_Next.UseVisualStyleBackColor = true;
            this.btn_IQ_Next.Click += new System.EventHandler(this.Btn_IQ_Next_Click);
            this.btn_IQ_Next.MouseEnter += new System.EventHandler(this.Btn_IQ_Next_MouseEnter);
            this.btn_IQ_Next.MouseLeave += new System.EventHandler(this.Btn_IQ_Next_MouseLeave);
            // 
            // btn_IQ_Prev
            // 
            this.btn_IQ_Prev.FlatAppearance.BorderSize = 0;
            this.btn_IQ_Prev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.btn_IQ_Prev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.btn_IQ_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IQ_Prev.Image = global::Properties.Resources.back_black;
            this.btn_IQ_Prev.Location = new System.Drawing.Point(18, 22);
            this.btn_IQ_Prev.Name = "btn_IQ_Prev";
            this.btn_IQ_Prev.Size = new System.Drawing.Size(49, 149);
            this.btn_IQ_Prev.TabIndex = 5;
            this.btn_IQ_Prev.UseVisualStyleBackColor = true;
            this.btn_IQ_Prev.Click += new System.EventHandler(this.Btn_IQ_Prev_Click);
            this.btn_IQ_Prev.MouseEnter += new System.EventHandler(this.Btn_IQ_Prev_MouseEnter);
            this.btn_IQ_Prev.MouseLeave += new System.EventHandler(this.Btn_IQ_Prev_MouseLeave);
            // 
            // dtp_IQ_To
            // 
            this.dtp_IQ_To.BackColor = System.Drawing.Color.Silver;
            this.dtp_IQ_To.BorderRadius = 0;
            this.dtp_IQ_To.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_IQ_To.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_IQ_To.FormatCustom = null;
            this.dtp_IQ_To.Location = new System.Drawing.Point(325, 236);
            this.dtp_IQ_To.Name = "dtp_IQ_To";
            this.dtp_IQ_To.Size = new System.Drawing.Size(208, 36);
            this.dtp_IQ_To.TabIndex = 6;
            this.dtp_IQ_To.Value = new System.DateTime(2019, 9, 11, 15, 56, 2, 657);
            // 
            // dtp_IQ_From
            // 
            this.dtp_IQ_From.BackColor = System.Drawing.Color.Silver;
            this.dtp_IQ_From.BorderRadius = 0;
            this.dtp_IQ_From.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.dtp_IQ_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_IQ_From.FormatCustom = null;
            this.dtp_IQ_From.Location = new System.Drawing.Point(71, 236);
            this.dtp_IQ_From.Name = "dtp_IQ_From";
            this.dtp_IQ_From.Size = new System.Drawing.Size(208, 36);
            this.dtp_IQ_From.TabIndex = 7;
            this.dtp_IQ_From.Value = new System.DateTime(2019, 9, 11, 15, 56, 10, 120);
            // 
            // dgv_IQuotes
            // 
            this.dgv_IQuotes.AllowUserToAddRows = false;
            this.dgv_IQuotes.AllowUserToDeleteRows = false;
            this.dgv_IQuotes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_IQuotes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_IQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_IQuotes.AutoGenerateContextFilters = true;
            this.dgv_IQuotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_IQuotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_IQuotes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv_IQuotes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_IQuotes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_IQuotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_IQuotes.DateWithTime = false;
            this.dgv_IQuotes.EnableHeadersVisualStyles = false;
            this.dgv_IQuotes.Location = new System.Drawing.Point(0, 279);
            this.dgv_IQuotes.Name = "dgv_IQuotes";
            this.dgv_IQuotes.ReadOnly = true;
            this.dgv_IQuotes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(91)))), ((int)(((byte)(142)))));
            this.dgv_IQuotes.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_IQuotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_IQuotes.Size = new System.Drawing.Size(963, 340);
            this.dgv_IQuotes.TabIndex = 8;
            this.dgv_IQuotes.TimeFilter = false;
            this.dgv_IQuotes.SortStringChanged += new System.EventHandler(this.Dgv_IQuotes_SortStringChanged);
            this.dgv_IQuotes.FilterStringChanged += new System.EventHandler(this.Dgv_IQuotes_FilterStringChanged);
            this.dgv_IQuotes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_IQuotes_CellDoubleClick);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(285, 243);
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
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(13, 243);
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
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(128, 118);
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
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(133, 57);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(113, 24);
            this.bunifuCustomLabel4.TabIndex = 12;
            this.bunifuCustomLabel4.Text = "Client Code:";
            // 
            // txt_IQ_CName
            // 
            this.txt_IQ_CName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_IQ_CName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_IQ_CName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IQ_CName.HintForeColor = System.Drawing.Color.Empty;
            this.txt_IQ_CName.HintText = "";
            this.txt_IQ_CName.isPassword = false;
            this.txt_IQ_CName.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IQ_CName.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_IQ_CName.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IQ_CName.LineThickness = 1;
            this.txt_IQ_CName.Location = new System.Drawing.Point(254, 116);
            this.txt_IQ_CName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_IQ_CName.Name = "txt_IQ_CName";
            this.txt_IQ_CName.Size = new System.Drawing.Size(379, 33);
            this.txt_IQ_CName.TabIndex = 13;
            this.txt_IQ_CName.Text = "bunifuMaterialTextbox1";
            this.txt_IQ_CName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_IQ_CName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_IQ_CName_KeyDown);
            // 
            // txt_IQ_CCode
            // 
            this.txt_IQ_CCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_IQ_CCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_IQ_CCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IQ_CCode.HintForeColor = System.Drawing.Color.Empty;
            this.txt_IQ_CCode.HintText = "";
            this.txt_IQ_CCode.isPassword = false;
            this.txt_IQ_CCode.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IQ_CCode.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_IQ_CCode.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(118)))), ((int)(((byte)(188)))));
            this.txt_IQ_CCode.LineThickness = 1;
            this.txt_IQ_CCode.Location = new System.Drawing.Point(254, 54);
            this.txt_IQ_CCode.Margin = new System.Windows.Forms.Padding(4);
            this.txt_IQ_CCode.Name = "txt_IQ_CCode";
            this.txt_IQ_CCode.Size = new System.Drawing.Size(379, 33);
            this.txt_IQ_CCode.TabIndex = 14;
            this.txt_IQ_CCode.Text = "bunifuMaterialTextbox2";
            this.txt_IQ_CCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_IQ_CCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_IQ_CCode_KeyDown);
            // 
            // Int_Quotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(963, 618);
            this.Controls.Add(this.txt_IQ_CCode);
            this.Controls.Add(this.txt_IQ_CName);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.dgv_IQuotes);
            this.Controls.Add(this.dtp_IQ_From);
            this.Controls.Add(this.dtp_IQ_To);
            this.Controls.Add(this.btn_IQ_Prev);
            this.Controls.Add(this.btn_IQ_Next);
            this.Controls.Add(this.btn_IQ_SelCli);
            this.Controls.Add(this.btn_IQ_Filter);
            this.Controls.Add(this.btn_IQ_NewQuote);
            this.Controls.Add(this.btn_IQ_ClearFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(963, 618);
            this.Name = "Int_Quotes";
            this.Text = "International Quotes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Quotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IQuotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_IQ_ClearFilter;
        private System.Windows.Forms.Button btn_IQ_NewQuote;
        private System.Windows.Forms.Button btn_IQ_Filter;
        private System.Windows.Forms.Button btn_IQ_SelCli;
        private System.Windows.Forms.Button btn_IQ_Next;
        private System.Windows.Forms.Button btn_IQ_Prev;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_IQ_To;
        private Bunifu.Framework.UI.BunifuDatepicker dtp_IQ_From;
        private ADGV.AdvancedDataGridView dgv_IQuotes;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_IQ_CName;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_IQ_CCode;
    }
}