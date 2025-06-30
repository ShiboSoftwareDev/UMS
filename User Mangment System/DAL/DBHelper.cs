using System;
using System.Data;
using System.Data.SqlClient;

namespace User_Mangment_System.DAL
{
    public class DbHelper : IDisposable
    {
        protected string connectionString = "Server=Shibo;Database=management;Trusted_Connection=True;";
        protected SqlConnection connection;

        public DbHelper()
        {
            connection = new SqlConnection(connectionString);
        }

        protected void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        protected void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        public void Dispose()
        {
            CloseConnection();  
            connection?.Dispose();  
        }
    }
}