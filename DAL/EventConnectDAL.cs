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
    public class EventConnectDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public EventConnectDAL()
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

        public List<EventConnect> GetAllEvents()
        {

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM EventConnect ORDER BY EventID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<EventConnect> eventList = new List<EventConnect>();
            while (reader.Read())
            {
                eventList.Add(
                new EventConnect
                {
                    EventID = reader.GetInt32(0), //0: 1st column
                    EventName = reader.GetString(1),
                    EventLocation = reader.GetString(2),
                    StartDate = !reader.IsDBNull(3) ?
                    reader.GetDateTime(3) : (DateTime?)null,
                    EndDate = !reader.IsDBNull(4) ?
                    reader.GetDateTime(4) : (DateTime?)null,
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return eventList;
        }

        public int Add(EventConnect events)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will

            cmd.CommandText = @"INSERT INTO EventConnect (EventName)
                                OUTPUT INSERTED.EventID
                                VALUES(@EventName)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@EventName", events.EventName);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated

            events.EventID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return events.EventID;
        }

        public bool IsEventNameExist(string name, int eventConnectID)
        {
            bool nameFound = false;
            //Create a SqlCommand object and specify the SQL statement

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT EventID FROM EventConnect
                                WHERE EventName=@eventName";
            cmd.Parameters.AddWithValue("@eventName", name);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != eventConnectID)
                        //The name is used
                        nameFound = true;
                }
            }
            else
            { //No record
                nameFound = false; // The name given does not exist
            }
            reader.Close();
            conn.Close();

            return nameFound;
        }

        public EventConnect GetDetails(int eventID)
        {
            EventConnect events = new EventConnect();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that

            cmd.CommandText = @"SELECT * FROM EventConnect
                    WHERE EventID = @selectedeventID";
            //Define the parameter used in SQL statement, value for the

            cmd.Parameters.AddWithValue("@selectedeventID", eventID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {

                    events.EventID = eventID;
                    events.EventName = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    // (char) 0 - ASCII Code 0 - null value
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return events;
        }

        public int DeleteEvent(int eventID)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement

            SqlCommand cmd = conn.CreateCommand();


            cmd.CommandText = @"DELETE FROM EventConnect
            WHERE eventID = @selecteventID";

            cmd.Parameters.AddWithValue("@selecteventID", eventID);

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
