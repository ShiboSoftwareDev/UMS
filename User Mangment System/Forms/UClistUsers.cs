using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User_Mangment_System.DAL;
using User_Mangment_System.Models;

namespace User_Mangment_System.Forms
{
    public partial class UClistUsers : UserControl
    {
        public UClistUsers()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                UserDAL dal = new UserDAL();
                List<User> users = dal.GetAllUsers();

                dgvUsers.DataSource = null;
                dgvUsers.DataSource = users;

                dgvUsers.Columns["PasswordHash"].Visible = false;
                dgvUsers.Columns["FailedAttempts"].HeaderText = "Failed Attempts";
                dgvUsers.Columns["LastAttempt"].HeaderText = "Last Login Attempt";
                dgvUsers.Columns["IsLocked"].HeaderText = "Locked?";
                dgvUsers.Columns["Role"].HeaderText = "Role";
                dgvUsers.Columns["Username"].HeaderText = "Username";

                dgvUsers.AutoResizeColumns();
                dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading users:\n" + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}