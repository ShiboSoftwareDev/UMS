using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using User_Mangment_System.DAL;
using User_Mangment_System.Models;
using UserManagementSystem.Utilities;

namespace User_Mangment_System.Forms
{
    public partial class UCaddUser : UserControl
    {
        private readonly User currentUser;


        public UCaddUser(User user)
        {
            InitializeComponent();
            currentUser = user;

            if (currentUser.Role == UserRole.Viewer)
            {
                addUserBtn.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string roleStr = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(roleStr))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (username.Length < 5 || !Regex.IsMatch(username, @"^[a-zA-Z].*"))
            {
                MessageBox.Show("Username must be at least 5 characters and start with a letter.");
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters and contain letters, numbers, and symbols.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (!Enum.TryParse<UserRole>(roleStr, out UserRole role))
            {
                MessageBox.Show("Invalid role selected.");
                return;
            }

            string hashedPassword = PasswordHasher.HashPassword(password);

            User newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Role = role,
                FailedAttempts = 0,
                LastAttempt = null,
                IsLocked = false
            };

            try
            {
                UserDAL dal = new UserDAL();
                bool success = dal.AddUser(newUser);

                if (success)
                {
                    MessageBox.Show("User added successfully.");
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Failed to add user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

            private bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;

            bool hasLetter = false, hasDigit = false, hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsWhiteSpace(c)) hasSpecial = true;
            }

            return hasLetter && hasDigit && hasSpecial;
        }

        private void ClearInputs()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cmbRole.SelectedIndex = -1;
        }
    }
}