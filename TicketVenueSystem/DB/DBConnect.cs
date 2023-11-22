using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVenueSystem.DB
{
    internal class DBConnect
    {
        private static String dbName = "ticketVenue";
        private static String serverAddress = "127.0.0.1";
        private static int serverPort = 1433;
        private static String userName = "sa";
        private static String password = "secret";

        private SqlConnection con;
        private static DBConnect? DBC;

        String conString = $"data source = {serverAddress},{serverPort};" +
                           $"database = {dbName};" +
                           $"user = {userName};" +
                           $"password = {password}";


        private DBConnect()
        {
            con = new SqlConnection(conString);
            con.Open();
        }

        public static DBConnect getInstance()
        {
            if (DBC == null)
            {
                DBC = new DBConnect();
            }
            return DBC;
        }


        public bool tryConnect()
        {
            bool conStatus = false;

            con = new SqlConnection(conString);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                conStatus = true;
            }

            return conStatus;
        }


        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
