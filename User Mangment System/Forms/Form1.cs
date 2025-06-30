using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using User_Mangment_System.DAL;
using User_Mangment_System.Forms;
using User_Mangment_System.Models;
using UserManagementSystem.Utilities;

namespace User_Mangment_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int MaxAttemptsLevel1 = 3;
        private const int MaxAttemptsLevel2 = 4;
        private const int MaxAttemptsLevel3 = 5;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputUsername = txtUsername.Text.Trim();
            string inputPassword = txtPassword.Text;

            UserDAL userDal = new UserDAL();
            User user = userDal.GetUserByUsername(inputUsername);

            if (user == null)
            {
                MessageBox.Show("User not found.");
            }
            else if (user.IsLocked)
            {
                MessageBox.Show("Your account is locked.");
            }
            else if (!PasswordHasher.VerifyPassword(inputPassword, user.PasswordHash))
            {
                user.FailedAttempts++;
                user.LastAttempt = DateTime.Now;

                if (user.FailedAttempts >= 3)
                {
                    user.IsLocked = true;
                    MessageBox.Show("Account locked after 3 failed attempts.");
                }
                else
                {
                    MessageBox.Show("Incorrect password.");
                }
                userDal.UpdateUserLoginAttempts(user);
            }
            else
            {
                user.FailedAttempts = 0;
                user.IsLocked = false;
                user.LastAttempt = DateTime.Now;
                userDal.UpdateUserLoginAttempts(user);

                DashboardForm dashboard = new DashboardForm(user);
                this.Hide();
                dashboard.Show();
            }
        }

        private bool ShouldLock(int attempts)
        {
            return attempts >= MaxAttemptsLevel1;
        }

        private string GetLockMessage(int attempts, TimeSpan timeSinceLast)
        {
            if (attempts >= MaxAttemptsLevel3 && timeSinceLast.TotalHours < 24)
                return "Account is locked for 24 hours. Please try again later.";
            else if (attempts >= MaxAttemptsLevel2 && timeSinceLast.TotalMinutes < 5)
                return "Account is locked for 5 minutes. Please try again later.";
            else if (attempts >= MaxAttemptsLevel1 && timeSinceLast.TotalMinutes < 1)
                return "Account is locked for 1 minute. Please try again later.";
            else
                return "Account is locked. Please try again later.";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}