// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.New_Proj_Line
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class New_Proj_Line : Form
  {
    private IContainer components = (IContainer) null;
    private string Proj_ID;
    private bool mouseDown;
    private Point lastLocation;
    private Manage_Proj parent;
    private Button btn_NPL_Close;
    private BunifuCustomLabel bunifuCustomLabel1;
    private BunifuDropdown ddb_NPL_Column;
    private BunifuCustomLabel bunifuCustomLabel4;
    private Panel panel2;
    private TextBox txt_NPL_Desc;
    private BunifuSeparator ln_NPL_Desc;
    private BunifuCustomLabel bunifuCustomLabel5;
    private Panel panel6;
    private TextBox txt_NPL_Val;
    private BunifuSeparator ln_NPL_Val;
    private BunifuCustomLabel bunifuCustomLabel8;
    private Button btn_NPL_Cancel;
    private Button btn_NPL_Done;
    private Panel panel1;
    private TextBox txt_NPL_User;
    private BunifuSeparator ln_NPL_User;
    private BunifuCustomLabel bunifuCustomLabel2;

    public New_Proj_Line()
    {
      this.InitializeComponent();
    }

    public void setParent(Manage_Proj parent)
    {
      this.parent = parent;
    }

    private void New_Proj_Line_Load(object sender, EventArgs e)
    {
      this.Proj_ID = this.parent.getProjectID();
      foreach (DataGridViewColumn column in (BaseCollection) this.parent.getLines().Columns)
      {
        if (!column.Name.Equals("ID") && !column.Name.Equals("Project_ID") && (!column.Name.Equals("Description") && !column.Name.Equals("Date")) && !column.Name.Equals("User_Log"))
          this.ddb_NPL_Column.AddItem(column.Name);
      }
      this.ddb_NPL_Column.selectedIndex = 0;
    }

    private void btn_NPL_Done_Click(object sender, EventArgs e)
    {
      if (this.ddb_NPL_Column.selectedValue.Equals("Travel") || this.ddb_NPL_Column.selectedValue.Equals("Accomodation") || this.ddb_NPL_Column.selectedValue.Equals("Subsistence") || this.ddb_NPL_Column.selectedValue.Equals("Tools"))
      {
        if (this.txt_NPL_Val.Text.Contains("R") || this.txt_NPL_Val.Text.Contains("$"))
        {
          this.doAdd();
        }
        else
        {
          int num = (int) MessageBox.Show("Value field requires a 'R'/'$' symbol", "error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      else
        this.doAdd();
    }

    private void doAdd()
    {
      if (!this.txt_NPL_User.Text.Equals(string.Empty))
      {
        if (MessageBox.Show(new StringBuilder().Append("Are you sure you want to add new line?").ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        using (SqlConnection dbConnection = DBUtils.GetDBConnection())
        {
          dbConnection.Open();
          try
          {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Project_Expenses VALUES (@ProjID, @Desc, @Travel, @Acc, @Sub, @Tools, @ProgHrs, @InstHrs, @Date, @User)", dbConnection))
            {
              sqlCommand.Parameters.AddWithValue("@ProjID", (object) this.Proj_ID);
              sqlCommand.Parameters.AddWithValue("@Desc", (object) this.txt_NPL_Desc.Text.Trim());
              if (this.ddb_NPL_Column.selectedValue.Equals("Travel"))
              {
                sqlCommand.Parameters.AddWithValue("@Travel", (object) this.txt_NPL_Val.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Acc", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Sub", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Tools", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ProgHrs", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@InstHrs", (object) DBNull.Value);
              }
              else if (this.ddb_NPL_Column.selectedValue.Equals("Accomodation"))
              {
                sqlCommand.Parameters.AddWithValue("@Travel", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Acc", (object) this.txt_NPL_Val.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Sub", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Tools", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ProgHrs", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@InstHrs", (object) DBNull.Value);
              }
              else if (this.ddb_NPL_Column.selectedValue.Equals("Subsistence"))
              {
                sqlCommand.Parameters.AddWithValue("@Travel", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Acc", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Sub", (object) this.txt_NPL_Val.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Tools", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ProgHrs", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@InstHrs", (object) DBNull.Value);
              }
              else if (this.ddb_NPL_Column.selectedValue.Equals("Tools"))
              {
                sqlCommand.Parameters.AddWithValue("@Travel", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Acc", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Sub", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Tools", (object) this.txt_NPL_Val.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@ProgHrs", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@InstHrs", (object) DBNull.Value);
              }
              else if (this.ddb_NPL_Column.selectedValue.Equals("Programming_Hours"))
              {
                sqlCommand.Parameters.AddWithValue("@Travel", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Acc", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Sub", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Tools", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ProgHrs", (object) this.txt_NPL_Val.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@InstHrs", (object) DBNull.Value);
              }
              else if (this.ddb_NPL_Column.selectedValue.Equals("Install_Hours"))
              {
                sqlCommand.Parameters.AddWithValue("@Travel", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Acc", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Sub", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Tools", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ProgHrs", (object) DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@InstHrs", (object) this.txt_NPL_Val.Text.Trim());
              }
              sqlCommand.Parameters.AddWithValue("@Date", (object) DateTime.Now.Date);
              sqlCommand.Parameters.AddWithValue("@User", (object) this.txt_NPL_User.Text.Trim());
              sqlCommand.ExecuteNonQuery();
              int num = (int) MessageBox.Show("New line successfully added.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              this.Close();
            }
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show("Please enter name in user field.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btn_NPL_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_NPL_Desc_MouseEnter(object sender, EventArgs e)
    {
      this.ln_NPL_Desc.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_NPL_Desc_Leave(object sender, EventArgs e)
    {
      this.ln_NPL_Desc.LineColor = Color.Gray;
    }

    private void txt_NPL_Desc_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_NPL_Desc.Focused)
        return;
      this.ln_NPL_Desc.LineColor = Color.Gray;
    }

    private void txt_NPL_Val_MouseEnter(object sender, EventArgs e)
    {
      this.ln_NPL_Val.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_NPL_Val_Leave(object sender, EventArgs e)
    {
      this.ln_NPL_Val.LineColor = Color.Gray;
    }

    private void txt_NPL_Val_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_NPL_Val.Focused)
        return;
      this.ln_NPL_Val.LineColor = Color.Gray;
    }

    private void txt_NPL_User_MouseEnter(object sender, EventArgs e)
    {
      this.ln_NPL_User.LineColor = Color.FromArgb(19, 118, 188);
    }

    private void txt_NPL_User_Leave(object sender, EventArgs e)
    {
      this.ln_NPL_User.LineColor = Color.Gray;
    }

    private void txt_NPL_User_MouseLeave(object sender, EventArgs e)
    {
      if (this.txt_NPL_User.Focused)
        return;
      this.ln_NPL_User.LineColor = Color.Gray;
    }

    private void btn_NPL_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_NPL_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_NPL_Close.Image = (Image) Resources.close_white;
    }

    private void btn_NPL_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_NPL_Close.Image = (Image) Resources.close_black;
    }

    private void btn_NPL_Done_MouseEnter(object sender, EventArgs e)
    {
      this.btn_NPL_Done.ForeColor = Color.White;
    }

    private void btn_NPL_Done_MouseLeave(object sender, EventArgs e)
    {
      this.btn_NPL_Done.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_NPL_Cancel_MouseEnter(object sender, EventArgs e)
    {
      this.btn_NPL_Cancel.ForeColor = Color.White;
    }

    private void btn_NPL_Cancel_MouseLeave(object sender, EventArgs e)
    {
      this.btn_NPL_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void New_Proj_Line_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void New_Proj_Line_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.mouseDown)
        return;
      Point location = this.Location;
      int x = location.X - this.lastLocation.X + e.X;
      location = this.Location;
      int y = location.Y - this.lastLocation.Y + e.Y;
      this.Location = new Point(x, y);
      this.Update();
    }

    private void New_Proj_Line_MouseUp(object sender, MouseEventArgs e)
    {
      this.mouseDown = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (New_Proj_Line));
      this.btn_NPL_Close = new Button();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.ddb_NPL_Column = new BunifuDropdown();
      this.bunifuCustomLabel4 = new BunifuCustomLabel();
      this.panel2 = new Panel();
      this.txt_NPL_Desc = new TextBox();
      this.ln_NPL_Desc = new BunifuSeparator();
      this.bunifuCustomLabel5 = new BunifuCustomLabel();
      this.panel6 = new Panel();
      this.txt_NPL_Val = new TextBox();
      this.ln_NPL_Val = new BunifuSeparator();
      this.bunifuCustomLabel8 = new BunifuCustomLabel();
      this.btn_NPL_Cancel = new Button();
      this.btn_NPL_Done = new Button();
      this.panel1 = new Panel();
      this.txt_NPL_User = new TextBox();
      this.ln_NPL_User = new BunifuSeparator();
      this.bunifuCustomLabel2 = new BunifuCustomLabel();
      this.panel2.SuspendLayout();
      this.panel6.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.btn_NPL_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_NPL_Close.BackColor = Color.Silver;
      this.btn_NPL_Close.FlatAppearance.BorderSize = 0;
      this.btn_NPL_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_NPL_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_NPL_Close.FlatStyle = FlatStyle.Flat;
      this.btn_NPL_Close.Image = (Image) componentResourceManager.GetObject("btn_NPL_Close.Image");
      this.btn_NPL_Close.Location = new Point(373, 0);
      this.btn_NPL_Close.Name = "btn_NPL_Close";
      this.btn_NPL_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_NPL_Close.Size = new Size(31, 29);
      this.btn_NPL_Close.TabIndex = 106;
      this.btn_NPL_Close.UseVisualStyleBackColor = false;
      this.btn_NPL_Close.Click += new EventHandler(this.btn_NPL_Close_Click);
      this.btn_NPL_Close.MouseEnter += new EventHandler(this.btn_NPL_Close_MouseEnter);
      this.btn_NPL_Close.MouseLeave += new EventHandler(this.btn_NPL_Close_MouseLeave);
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(12, 9);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(112, 17);
      this.bunifuCustomLabel1.TabIndex = 107;
      this.bunifuCustomLabel1.Text = "New project line:";
      this.ddb_NPL_Column.BackColor = Color.Transparent;
      this.ddb_NPL_Column.BorderRadius = 2;
      this.ddb_NPL_Column.DisabledColor = Color.Gray;
      this.ddb_NPL_Column.ForeColor = Color.FromArgb(15, 91, 142);
      this.ddb_NPL_Column.Items = new string[0];
      this.ddb_NPL_Column.Location = new Point(119, 49);
      this.ddb_NPL_Column.Name = "ddb_NPL_Column";
      this.ddb_NPL_Column.NomalColor = Color.Silver;
      this.ddb_NPL_Column.onHoverColor = Color.DarkGray;
      this.ddb_NPL_Column.selectedIndex = -1;
      this.ddb_NPL_Column.Size = new Size(228, 35);
      this.ddb_NPL_Column.TabIndex = 108;
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel4.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel4.Location = new Point(12, 57);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new Size(102, 17);
      this.bunifuCustomLabel4.TabIndex = 109;
      this.bunifuCustomLabel4.Text = "Select Column:";
      this.panel2.Controls.Add((Control) this.txt_NPL_Desc);
      this.panel2.Controls.Add((Control) this.ln_NPL_Desc);
      this.panel2.Location = new Point(120, 90);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(271, 27);
      this.panel2.TabIndex = 111;
      this.txt_NPL_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_NPL_Desc.BackColor = Color.Silver;
      this.txt_NPL_Desc.BorderStyle = BorderStyle.None;
      this.txt_NPL_Desc.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_NPL_Desc.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_NPL_Desc.Location = new Point(2, 7);
      this.txt_NPL_Desc.Name = "txt_NPL_Desc";
      this.txt_NPL_Desc.Size = new Size(268, 16);
      this.txt_NPL_Desc.TabIndex = 4;
      this.txt_NPL_Desc.Leave += new EventHandler(this.txt_NPL_Desc_Leave);
      this.txt_NPL_Desc.MouseEnter += new EventHandler(this.txt_NPL_Desc_MouseEnter);
      this.txt_NPL_Desc.MouseLeave += new EventHandler(this.txt_NPL_Desc_MouseLeave);
      this.ln_NPL_Desc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_NPL_Desc.BackColor = Color.Transparent;
      this.ln_NPL_Desc.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_NPL_Desc.LineThickness = 1;
      this.ln_NPL_Desc.Location = new Point(-1, 18);
      this.ln_NPL_Desc.Name = "ln_NPL_Desc";
      this.ln_NPL_Desc.Size = new Size(273, 10);
      this.ln_NPL_Desc.TabIndex = 0;
      this.ln_NPL_Desc.TabStop = false;
      this.ln_NPL_Desc.Transparency = (int) byte.MaxValue;
      this.ln_NPL_Desc.Vertical = false;
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel5.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel5.Location = new Point(31, 95);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new Size(83, 17);
      this.bunifuCustomLabel5.TabIndex = 110;
      this.bunifuCustomLabel5.Text = "Description:";
      this.panel6.Controls.Add((Control) this.txt_NPL_Val);
      this.panel6.Controls.Add((Control) this.ln_NPL_Val);
      this.panel6.Location = new Point(120, 123);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(156, 26);
      this.panel6.TabIndex = 113;
      this.txt_NPL_Val.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_NPL_Val.BackColor = Color.Silver;
      this.txt_NPL_Val.BorderStyle = BorderStyle.None;
      this.txt_NPL_Val.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_NPL_Val.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_NPL_Val.Location = new Point(2, 6);
      this.txt_NPL_Val.Name = "txt_NPL_Val";
      this.txt_NPL_Val.Size = new Size(153, 16);
      this.txt_NPL_Val.TabIndex = 5;
      this.txt_NPL_Val.Leave += new EventHandler(this.txt_NPL_Val_Leave);
      this.txt_NPL_Val.MouseEnter += new EventHandler(this.txt_NPL_Val_MouseEnter);
      this.txt_NPL_Val.MouseLeave += new EventHandler(this.txt_NPL_Val_MouseLeave);
      this.ln_NPL_Val.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_NPL_Val.BackColor = Color.Transparent;
      this.ln_NPL_Val.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_NPL_Val.LineThickness = 1;
      this.ln_NPL_Val.Location = new Point(-1, 18);
      this.ln_NPL_Val.Name = "ln_NPL_Val";
      this.ln_NPL_Val.Size = new Size(158, 10);
      this.ln_NPL_Val.TabIndex = 52;
      this.ln_NPL_Val.TabStop = false;
      this.ln_NPL_Val.Transparency = (int) byte.MaxValue;
      this.ln_NPL_Val.Vertical = false;
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel8.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel8.Location = new Point(66, 129);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new Size(48, 17);
      this.bunifuCustomLabel8.TabIndex = 112;
      this.bunifuCustomLabel8.Text = "Value:";
      this.btn_NPL_Cancel.FlatAppearance.BorderSize = 0;
      this.btn_NPL_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_NPL_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_NPL_Cancel.FlatStyle = FlatStyle.Flat;
      this.btn_NPL_Cancel.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_NPL_Cancel.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_NPL_Cancel.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_NPL_Cancel.Location = new Point(277, 191);
      this.btn_NPL_Cancel.Name = "btn_NPL_Cancel";
      this.btn_NPL_Cancel.Size = new Size(114, 40);
      this.btn_NPL_Cancel.TabIndex = 115;
      this.btn_NPL_Cancel.Text = "Cancel";
      this.btn_NPL_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_NPL_Cancel.UseVisualStyleBackColor = true;
      this.btn_NPL_Cancel.Click += new EventHandler(this.btn_NPL_Cancel_Click);
      this.btn_NPL_Cancel.MouseEnter += new EventHandler(this.btn_NPL_Cancel_MouseEnter);
      this.btn_NPL_Cancel.MouseLeave += new EventHandler(this.btn_NPL_Cancel_MouseLeave);
      this.btn_NPL_Done.FlatAppearance.BorderSize = 0;
      this.btn_NPL_Done.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_NPL_Done.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_NPL_Done.FlatStyle = FlatStyle.Flat;
      this.btn_NPL_Done.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_NPL_Done.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_NPL_Done.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_NPL_Done.Location = new Point(157, 191);
      this.btn_NPL_Done.Name = "btn_NPL_Done";
      this.btn_NPL_Done.Size = new Size(114, 40);
      this.btn_NPL_Done.TabIndex = 114;
      this.btn_NPL_Done.Text = "Done";
      this.btn_NPL_Done.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_NPL_Done.UseVisualStyleBackColor = true;
      this.btn_NPL_Done.Click += new EventHandler(this.btn_NPL_Done_Click);
      this.btn_NPL_Done.MouseEnter += new EventHandler(this.btn_NPL_Done_MouseEnter);
      this.btn_NPL_Done.MouseLeave += new EventHandler(this.btn_NPL_Done_MouseLeave);
      this.panel1.Controls.Add((Control) this.txt_NPL_User);
      this.panel1.Controls.Add((Control) this.ln_NPL_User);
      this.panel1.Location = new Point(120, 157);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(156, 26);
      this.panel1.TabIndex = 117;
      this.txt_NPL_User.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txt_NPL_User.BackColor = Color.Silver;
      this.txt_NPL_User.BorderStyle = BorderStyle.None;
      this.txt_NPL_User.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txt_NPL_User.ForeColor = Color.FromArgb(15, 91, 142);
      this.txt_NPL_User.Location = new Point(2, 6);
      this.txt_NPL_User.Name = "txt_NPL_User";
      this.txt_NPL_User.Size = new Size(153, 16);
      this.txt_NPL_User.TabIndex = 5;
      this.txt_NPL_User.Leave += new EventHandler(this.txt_NPL_User_Leave);
      this.txt_NPL_User.MouseEnter += new EventHandler(this.txt_NPL_User_MouseEnter);
      this.txt_NPL_User.MouseLeave += new EventHandler(this.txt_NPL_User_MouseLeave);
      this.ln_NPL_User.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.ln_NPL_User.BackColor = Color.Transparent;
      this.ln_NPL_User.LineColor = Color.FromArgb(128, 128, 128);
      this.ln_NPL_User.LineThickness = 1;
      this.ln_NPL_User.Location = new Point(-1, 18);
      this.ln_NPL_User.Name = "ln_NPL_User";
      this.ln_NPL_User.Size = new Size(158, 10);
      this.ln_NPL_User.TabIndex = 52;
      this.ln_NPL_User.TabStop = false;
      this.ln_NPL_User.Transparency = (int) byte.MaxValue;
      this.ln_NPL_User.Vertical = false;
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel2.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel2.Location = new Point(72, 162);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new Size(42, 17);
      this.bunifuCustomLabel2.TabIndex = 116;
      this.bunifuCustomLabel2.Text = "User:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(404, 243);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.bunifuCustomLabel2);
      this.Controls.Add((Control) this.btn_NPL_Cancel);
      this.Controls.Add((Control) this.btn_NPL_Done);
      this.Controls.Add((Control) this.panel6);
      this.Controls.Add((Control) this.bunifuCustomLabel8);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.bunifuCustomLabel5);
      this.Controls.Add((Control) this.bunifuCustomLabel4);
      this.Controls.Add((Control) this.ddb_NPL_Column);
      this.Controls.Add((Control) this.bunifuCustomLabel1);
      this.Controls.Add((Control) this.btn_NPL_Close);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (New_Proj_Line);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (New_Proj_Line);
      this.Load += new EventHandler(this.New_Proj_Line_Load);
      this.MouseDown += new MouseEventHandler(this.New_Proj_Line_MouseDown);
      this.MouseMove += new MouseEventHandler(this.New_Proj_Line_MouseMove);
      this.MouseUp += new MouseEventHandler(this.New_Proj_Line_MouseUp);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel6.ResumeLayout(false);
      this.panel6.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
