using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using User_Mangment_System.Models;
using UserManagementSystem.Utilities;

namespace User_Mangment_System.DAL
{
    public class UserDAL : DbHelper, IDisposable
    {
        public bool AddUser(User user)
        {
            string hashedPassword = user.PasswordHash;

            string query = @"INSERT INTO Owners (username, password_hash, role, failed_attempts, last_attempt, is_locked) 
                     VALUES (@Username, @PasswordHash, @Role, @FailedAttempts, @LastAttempt, @IsLocked)";
            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword); // هنا تخزن القيمة المشفرة
                    cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
                    cmd.Parameters.AddWithValue("@FailedAttempts", user.FailedAttempts);
                    cmd.Parameters.AddWithValue("@LastAttempt", user.LastAttempt ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsLocked", user.IsLocked);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) // Primary key violation
            {
                Debug.WriteLine($"User already exists: {ex.Message}");
                throw new Exception("Username already exists. Please choose a different username.", ex);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Database error in AddUser: {ex.Message}");
                throw new Exception("Failed to create user due to a database error.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public User GetUserByUsername(string username)
        {
            string query = "SELECT username, password_hash, role, failed_attempts, last_attempt, is_locked FROM Owners WHERE username = @Username";
            User user = null;

            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Username = reader["username"].ToString(),
                                PasswordHash = reader["password_hash"].ToString(),
                                Role = (UserRole)Enum.Parse(typeof(UserRole), reader["role"].ToString(), true),
                                FailedAttempts = Convert.ToInt32(reader["failed_attempts"]),
                                LastAttempt = reader["last_attempt"] != DBNull.Value ?
                                Convert.ToDateTime(reader["last_attempt"]) : (DateTime?)null,
                                IsLocked = Convert.ToBoolean(reader["is_locked"])
                            };

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Database error in GetUserByUsername: {ex.Message}");
                Console.WriteLine("Actual error: " + ex.Message);

                throw new Exception("Failed to retrieve user information due to a database error.", ex);
            }
            finally
            {
                CloseConnection();
            }

            return user;
        }

        public bool UpdateUserLoginAttempts(User user)
        {
            string query = @"UPDATE Owners SET 
                 failed_attempts = @FailedAttempts, 
                 last_attempt = @LastAttempt,
                 is_locked = @IsLocked
                 WHERE username = @Username";


            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@FailedAttempts", user.FailedAttempts);
                    cmd.Parameters.AddWithValue("@LastAttempt", user.LastAttempt ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsLocked", user.IsLocked);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Database error in UpdateUserLoginAttempts: {ex.Message}");
                throw new Exception("Failed to update login attempts due to a database error.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string query = "SELECT username, password_hash, role, failed_attempts, last_attempt, is_locked FROM Owners";

            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Username = reader["username"].ToString(),
                                PasswordHash = reader["password_hash"].ToString(),
                                Role = (UserRole)Enum.Parse(typeof(UserRole), reader["role"].ToString(), true),
                                FailedAttempts = Convert.ToInt32(reader["failed_attempts"]),
                                LastAttempt = reader["last_attempt"] != DBNull.Value ?
                                             Convert.ToDateTime(reader["last_attempt"]) : (DateTime?)null,
                                IsLocked = Convert.ToBoolean(reader["is_locked"])
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Database error in GetAllUsers: {ex.Message}");
                throw new Exception("Failed to retrieve user list due to a database error.", ex);
            }
            finally
            {
                CloseConnection();
            }

            return users;
        }
    }
}