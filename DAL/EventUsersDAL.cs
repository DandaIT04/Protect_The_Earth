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
    public class EventUsersDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        //Constructor
        public EventUsersDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("PolyEarthConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }

        public List<EventUsers> GetAllEventUsers()
        {

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM EventUsers ORDER BY EventID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<EventUsers> eventUsersList = new List<EventUsers>();
            while (reader.Read())
            {
                eventUsersList.Add(
                new EventUsers
                {
                    EventID = reader.GetInt32(0),  //0: 1st column and + 1 per column 
                    UserID = reader.GetInt32(1),  //0: 1st column and + 1 per column 
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return eventUsersList;
        }

        public int AddUsers(EventUsers addUsers)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will

            cmd.CommandText = @"INSERT INTO EventUsers (EventID,UserID)
                                OUTPUT INSERTED.EventID
                                VALUES(@eventID,@userID)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@eventID", addUsers.EventID);
            cmd.Parameters.AddWithValue("@userID", addUsers.UserID);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated

            addUsers.EventID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return addUsers.EventID;
        }

        public int RemoveEvents(EventUsers toDelete)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement

            SqlCommand cmd = conn.CreateCommand();


            cmd.CommandText = @"DELETE FROM EventUsers
            WHERE EventID = @selectEventID
            AND UserID = @selectUserID";

            cmd.Parameters.AddWithValue("@selectEventID", toDelete.EventID);
            cmd.Parameters.AddWithValue("@selectUserID", toDelete.UserID);

            //Open a database connection
            conn.Open();
            int rowAffected = 0;

            rowAffected += cmd.ExecuteNonQuery();

            //Close database connection
            conn.Close();

            return rowAffected;
        }

    }
}
