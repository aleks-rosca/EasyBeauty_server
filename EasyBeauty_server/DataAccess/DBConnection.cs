using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
namespace EasyBeauty_server.DataAccess
    
{
    public static class DBConnection
    {
        private static SqlConnection databaseConnection;
        public static SqlConnection DatabaseConnection
        {
            get
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                if(databaseConnection ==null)
                {
                    throw new Exception($"Database Connection error");
                }
                if(databaseConnection.State != System.Data.ConnectionState.Open)
                {
                    throw new Exception($"Connection state: { databaseConnection.State}");
                }
                return databaseConnection;
            }
            set
            {
                databaseConnection = value;
            }
        }

        public static SqlConnection GetConnection(string conn = null)
        {
            conn = "Server=db1.easyways.dk;user=easybeauty;password=EasyBeauty1337;Database=easybeauty;";
            var connection = new SqlConnection(conn);
            connection.Open();
            DatabaseConnection = connection;
            return connection;
        }
        public static void CloseConnection()
        {
            databaseConnection.Close();
        }

    }

}
