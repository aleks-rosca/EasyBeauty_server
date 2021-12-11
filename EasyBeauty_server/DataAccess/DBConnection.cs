namespace EasyBeauty_server.DataAccess
{
    using Dapper;
    using System;
    using System.Data.SqlClient;

    public static class DBConnection
    {
        private static SqlConnection databaseConnection;

        public static SqlConnection DatabaseConnection
        {
            get
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                if (databaseConnection == null)
                {
                    throw new Exception($"Database Connection error");
                }
                if (databaseConnection.State != System.Data.ConnectionState.Open)
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
            conn = "Server=easybeauty.mssql.somee.com;user=AleksMD_SQLLogin_1;password=m3x6lm4ode;Database=easybeauty";
            //conn = "Server=db1.easyways.dk;user=easybeauty;password=EasyBeauty1337;Database=easybeauty;";
            var connection = new SqlConnection(conn);
            connection.Open();
            DatabaseConnection = connection;
            return connection;
        }
    }
}
