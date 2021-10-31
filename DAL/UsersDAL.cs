using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using PFD_SaveTheEnvironment.Models;

namespace PFD_SaveTheEnvironment.DAL
{
    public class UsersDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public UsersDAL()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "PolyEarthConnectionString");
            conn = new SqlConnection(strConn);
        }
        public bool IsUserExist(string email, int userId) //Create new validation (True means email already used)
        {
            bool emailFound = false;

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT UserID FROM Users WHERE EmailAddr=@selectedEmail";
            cmd.Parameters.AddWithValue("@selectedEmail", email);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) //Records Found
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != userId)
                    {
                        emailFound = true;
                    }
                }
            }
            else
            {
                emailFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();

            return emailFound;
        }
        public bool ValidUserLogin(string email, string pass)
        {
            bool validLogin = false;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT UserID FROM Users WHERE EmailAddr=@email AND Password=@pass";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pass", pass);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                validLogin = true;
            }
            else
            {
                validLogin = false;
            }
            reader.Close();
            conn.Close();

            return validLogin;
        }
        public int Add(Users user)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO Users (UserName, Salutation, EmailAddr, Password, Score)
                                OUTPUT INSERTED.UserID
                                VALUES(@name, @salutation, @email, @password, '0')";

            cmd.Parameters.AddWithValue("@name", user.UserName);
            cmd.Parameters.AddWithValue("@salutation", user.Salutation);
            cmd.Parameters.AddWithValue("@email", user.EmailAddr);
            cmd.Parameters.AddWithValue("@password", user.Password);

            conn.Open();
            user.UserID = (int)cmd.ExecuteScalar();
            conn.Close();

            return user.UserID;
        }
    }
}
