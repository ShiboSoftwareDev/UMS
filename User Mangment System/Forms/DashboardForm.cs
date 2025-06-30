using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User_Mangment_System.Forms;
using User_Mangment_System.Models;

namespace User_Mangment_System
{
    public partial class DashboardForm : Form
    {
        private readonly User currentUser;

        public DashboardForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            this.tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            LoadTabContent();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTabContent();
        }

        private void LoadTabContent()
        {
            mainPanel.Controls.Clear();
            Control control = null;
            switch (tabControl.SelectedTab.Name)
            {
                case "tabAddUser":
                    control = new UCaddUser(currentUser) { Dock = DockStyle.Fill };
                    break;
                case "tabListUsers":
                    control = new UClistUsers() { Dock = DockStyle.Fill };
                    break;
                case "tabRoleMng": 
                    var infoPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 247, 252) };
                    var welcomeLabelInfo = new Label
                    {
                        Text = $"Welcome, {currentUser.Username}!",
                        Dock = DockStyle.Top,
                        Height = 60,
                        Font = new Font("Segoe UI", 24, FontStyle.Bold),
                        ForeColor = Color.FromArgb(0, 122, 204),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    var statsPanelInfo = new TableLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        ColumnCount = 2,
                        RowCount = 2,
                        Padding = new Padding(40, 20, 40, 20),
                        BackColor = Color.Transparent
                    };
                    statsPanelInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    statsPanelInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    statsPanelInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                    statsPanelInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                    var dal = new User_Mangment_System.DAL.UserDAL();
                    var users = dal.GetAllUsers();
                    int totalUsers = users.Count;
                    int lockedAccounts = users.Count(u => u.IsLocked);
                    int totalRoles = users.Select(u => u.Role).Distinct().Count();
                    var lastLogin = users.FirstOrDefault(u => u.Username == currentUser.Username)?.LastAttempt;
                    var usersStatInfo = new Label
                    {
                        Text = $"👤\nTotal Users\n{totalUsers}",
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 18, FontStyle.Bold),
                        ForeColor = Color.FromArgb(60, 63, 65),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    var rolesStatInfo = new Label
                    {
                        Text = $"🔑\nRoles\n{totalRoles}",
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 18, FontStyle.Bold),
                        ForeColor = Color.FromArgb(60, 63, 65),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    var lockedStatInfo = new Label
                    {
                        Text = $"🔒\nLocked Accounts\n{lockedAccounts}",
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 18, FontStyle.Bold),
                        ForeColor = Color.FromArgb(60, 63, 65),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    var lastLoginStatInfo = new Label
                    {
                        Text = $"⏰\nLast Login\n{(lastLogin.HasValue ? lastLogin.Value.ToString("dd MMM yyyy") : "Never")}",
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 18, FontStyle.Bold),
                        ForeColor = Color.FromArgb(60, 63, 65),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    statsPanelInfo.Controls.Add(usersStatInfo, 0, 0);
                    statsPanelInfo.Controls.Add(rolesStatInfo, 1, 0);
                    statsPanelInfo.Controls.Add(lockedStatInfo, 0, 1);
                    statsPanelInfo.Controls.Add(lastLoginStatInfo, 1, 1);
                    infoPanel.Controls.Add(statsPanelInfo);
                    infoPanel.Controls.Add(welcomeLabelInfo);
                    control = infoPanel;
                    break;
                case "tabEvents": 
                    var aboutPanel = new Panel { Dock = DockStyle.Fill };
                    var aboutLabel = new Label
                    {
                        Text = "User Management System\n\nVersion 1.0\n\nDeveloped by Ahmed Alshaybani\n\nThis application allows you to manage users, roles, and more.",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Calibri", 16, FontStyle.Bold)
                    };
                    aboutPanel.Controls.Add(aboutLabel);
                    control = aboutPanel;
                    break;
            }
            if (control != null)
                mainPanel.Controls.Add(control);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
        }
    }
}
