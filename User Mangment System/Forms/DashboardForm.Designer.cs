using System.Windows.Forms;

namespace User_Mangment_System
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAddUser = new System.Windows.Forms.TabPage();
            this.tabListUsers = new System.Windows.Forms.TabPage();
            this.tabRoleMng = new System.Windows.Forms.TabPage();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.tabControl.Controls.Add(this.tabAddUser);
            this.tabControl.Controls.Add(this.tabListUsers);
            this.tabControl.Controls.Add(this.tabRoleMng);
            this.tabControl.Controls.Add(this.tabEvents);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1085, 80);
            this.tabControl.TabIndex = 0;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.tabControl.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.Normal;
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.DrawItem += (s, e) => {
    var tab = this.tabControl.TabPages[e.Index];
    var rect = e.Bounds;
    using (var brush = new System.Drawing.SolidBrush(e.State.HasFlag(System.Windows.Forms.DrawItemState.Selected) ? System.Drawing.Color.FromArgb(0, 122, 204) : System.Drawing.Color.FromArgb(60, 63, 65)))
        e.Graphics.FillRectangle(brush, rect);
    var textColor = e.State.HasFlag(System.Windows.Forms.DrawItemState.Selected) ? System.Drawing.Color.White : System.Drawing.Color.LightGray;
    TextRenderer.DrawText(e.Graphics, tab.Text, this.tabControl.Font, rect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
};
            this.tabAddUser.Name = "tabAddUser";
            this.tabAddUser.Text = "Add User";
            this.tabAddUser.UseVisualStyleBackColor = true;
            this.tabListUsers.Name = "tabListUsers";
            this.tabListUsers.Text = "List Users";
            this.tabListUsers.UseVisualStyleBackColor = true;
            this.tabRoleMng.Name = "tabRoleMng";
            this.tabRoleMng.Text = "Info";
            this.tabRoleMng.UseVisualStyleBackColor = true;
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Text = "About";
            this.tabEvents.UseVisualStyleBackColor = true;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 80);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1085, 534);
            this.mainPanel.TabIndex = 5;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(240, 243, 250);
            this.button5.BackColor = System.Drawing.Color.Crimson;
            this.button5.Font = new System.Drawing.Font("Calibri", 16.2F);
            this.button5.Location = new System.Drawing.Point(12, 515);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(178, 78);
            this.button5.TabIndex = 4;
            this.button5.Text = "Return";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(230, 233, 240);
            this.ClientSize = new System.Drawing.Size(1085, 614);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.button5);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Name = "DashboardForm";
            this.Text = "Dashboard";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAddUser;
        private System.Windows.Forms.TabPage tabListUsers;
        private System.Windows.Forms.TabPage tabRoleMng;
        private System.Windows.Forms.TabPage tabEvents;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button button5;
    }
}