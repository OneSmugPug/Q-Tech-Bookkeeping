// Decompiled with JetBrains decompiler
// Type: Q_Tech_Bookkeeping.Proj_Dialog
// Assembly: Q-Tech Bookkeeping, Version=1.0.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 23C9EC80-8A12-46C0-87B7-19563BD5EB8E
// Assembly location: D:\Program Files\Q-Tech Industrial Solutions\Q-Tech Bookkeeping\Q-Tech Bookkeeping.exe

using Bunifu.Framework.UI;
using Q_Tech_Bookkeeping.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Q_Tech_Bookkeeping
{
  public class Proj_Dialog : Form
  {
    private IContainer components = (IContainer) null;
    private bool mouseDown;
    private Point lastLocation;
    private Button btn_PD_ManProject;
    private Button btn_PD_EditProj;
    private Button btn_PD_Close;
    private BunifuCustomLabel bunifuCustomLabel1;

    public Proj_Dialog()
    {
      this.InitializeComponent();
    }

    private void btn_PD_Close_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_PD_EditProj_Click(object sender, EventArgs e)
    {
      new Proj_Edit_Del().Show((IWin32Window) this.Owner);
      this.Close();
    }

    private void btn_PD_ManProject_Click(object sender, EventArgs e)
    {
      HomeOld owner = (HomeOld) this.Owner;
      Manage_Proj frmMP = new Manage_Proj();
      owner.setManageProjects(frmMP, owner);
      this.Close();
    }

    private void btn_PD_Close_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PD_Close.Image = (Image) Resources.close_white;
    }

    private void btn_PD_Close_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PD_Close.Image = (Image) Resources.close_black;
    }

    private void btn_PD_ManProject_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PD_ManProject.ForeColor = Color.White;
    }

    private void btn_PD_ManProject_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PD_ManProject.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void btn_PD_EditProj_MouseEnter(object sender, EventArgs e)
    {
      this.btn_PD_EditProj.ForeColor = Color.White;
    }

    private void btn_PD_EditProj_MouseLeave(object sender, EventArgs e)
    {
      this.btn_PD_EditProj.ForeColor = Color.FromArgb(64, 64, 64);
    }

    private void Proj_Dialog_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseDown = true;
      this.lastLocation = e.Location;
    }

    private void Proj_Dialog_MouseMove(object sender, MouseEventArgs e)
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

    private void Proj_Dialog_MouseUp(object sender, MouseEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Proj_Dialog));
      this.btn_PD_ManProject = new Button();
      this.btn_PD_EditProj = new Button();
      this.btn_PD_Close = new Button();
      this.bunifuCustomLabel1 = new BunifuCustomLabel();
      this.SuspendLayout();
      this.btn_PD_ManProject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_PD_ManProject.FlatAppearance.BorderSize = 0;
      this.btn_PD_ManProject.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_PD_ManProject.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_PD_ManProject.FlatStyle = FlatStyle.Flat;
      this.btn_PD_ManProject.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_PD_ManProject.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_PD_ManProject.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_PD_ManProject.Location = new Point(12, 51);
      this.btn_PD_ManProject.Name = "btn_PD_ManProject";
      this.btn_PD_ManProject.Size = new Size(122, 40);
      this.btn_PD_ManProject.TabIndex = 103;
      this.btn_PD_ManProject.Text = "Manage Project";
      this.btn_PD_ManProject.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_PD_ManProject.UseVisualStyleBackColor = true;
      this.btn_PD_ManProject.Click += new EventHandler(this.btn_PD_ManProject_Click);
      this.btn_PD_ManProject.MouseEnter += new EventHandler(this.btn_PD_ManProject_MouseEnter);
      this.btn_PD_ManProject.MouseLeave += new EventHandler(this.btn_PD_ManProject_MouseLeave);
      this.btn_PD_EditProj.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_PD_EditProj.FlatAppearance.BorderSize = 0;
      this.btn_PD_EditProj.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 91, 142);
      this.btn_PD_EditProj.FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 118, 188);
      this.btn_PD_EditProj.FlatStyle = FlatStyle.Flat;
      this.btn_PD_EditProj.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btn_PD_EditProj.ForeColor = Color.FromArgb(64, 64, 64);
      this.btn_PD_EditProj.ImageAlign = ContentAlignment.MiddleRight;
      this.btn_PD_EditProj.Location = new Point(149, 51);
      this.btn_PD_EditProj.Name = "btn_PD_EditProj";
      this.btn_PD_EditProj.Size = new Size(122, 40);
      this.btn_PD_EditProj.TabIndex = 104;
      this.btn_PD_EditProj.Text = "Edit Project";
      this.btn_PD_EditProj.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btn_PD_EditProj.UseVisualStyleBackColor = true;
      this.btn_PD_EditProj.Click += new EventHandler(this.btn_PD_EditProj_Click);
      this.btn_PD_EditProj.MouseEnter += new EventHandler(this.btn_PD_EditProj_MouseEnter);
      this.btn_PD_EditProj.MouseLeave += new EventHandler(this.btn_PD_EditProj_MouseLeave);
      this.btn_PD_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btn_PD_Close.BackColor = Color.Silver;
      this.btn_PD_Close.FlatAppearance.BorderSize = 0;
      this.btn_PD_Close.FlatAppearance.MouseDownBackColor = Color.FromArgb(183, 0, 0);
      this.btn_PD_Close.FlatAppearance.MouseOverBackColor = Color.FromArgb(216, 0, 0);
      this.btn_PD_Close.FlatStyle = FlatStyle.Flat;
      this.btn_PD_Close.Image = (Image) componentResourceManager.GetObject("btn_PD_Close.Image");
      this.btn_PD_Close.Location = new Point(252, 0);
      this.btn_PD_Close.Name = "btn_PD_Close";
      this.btn_PD_Close.Padding = new Padding(0, 0, 1, 0);
      this.btn_PD_Close.Size = new Size(31, 29);
      this.btn_PD_Close.TabIndex = 105;
      this.btn_PD_Close.UseVisualStyleBackColor = false;
      this.btn_PD_Close.Click += new EventHandler(this.btn_PD_Close_Click);
      this.btn_PD_Close.MouseEnter += new EventHandler(this.btn_PD_Close_MouseEnter);
      this.btn_PD_Close.MouseLeave += new EventHandler(this.btn_PD_Close_MouseLeave);
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bunifuCustomLabel1.ForeColor = Color.FromArgb(64, 64, 64);
      this.bunifuCustomLabel1.Location = new Point(12, 9);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new Size(94, 17);
      this.bunifuCustomLabel1.TabIndex = 106;
      this.bunifuCustomLabel1.Text = "Select option:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Silver;
      this.ClientSize = new Size(283, 107);
      this.Controls.Add((Control) this.bunifuCustomLabel1);
      this.Controls.Add((Control) this.btn_PD_Close);
      this.Controls.Add((Control) this.btn_PD_EditProj);
      this.Controls.Add((Control) this.btn_PD_ManProject);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (Proj_Dialog);
      this.SizeGripStyle = SizeGripStyle.Show;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (Proj_Dialog);
      this.MouseDown += new MouseEventHandler(this.Proj_Dialog_MouseDown);
      this.MouseMove += new MouseEventHandler(this.Proj_Dialog_MouseMove);
      this.MouseUp += new MouseEventHandler(this.Proj_Dialog_MouseUp);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
