namespace EasyBeauty_server.Repository
{
    using Dapper;
    using DataAccess;
    using Models;

    public static class LoginRepo
    {
        public static EmailDatabase CheckEmail(string email)
        {
            return DBConnection.DatabaseConnection.QuerySingle<EmailDatabase>(@"SELECT email, password FROM Employee Where email = @email", new { email });
        }

        public static void CreatePassword(string email, string password)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Employee SET password=@password WHERE email=@email", new { email,  password });
        }

        public static bool CheckPassword(string email, string password)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Employee WHERE email = @email AND password = @password", new { email, password });
        }

        public static UserInfo GetUserInfo(string email)
        {
            return DBConnection.DatabaseConnection.QuerySingle<UserInfo>(@"SELECT name, id, role FROM Employee WHERE email = @email", new { email });
        }
        
        public static void SetToken(int id, string token)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Token(token, employeeId, expiresOn) VALUES (@token, @id, DATEADD(MINUTE, +30, GETDATE()))", new { token,  id });
        }

        public static void RemoveToken(int id)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Token WHERE employeeId = @id", new { id });
        }

        public static bool CheckToken(int id, string token)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Token WHERE employeeId = @id AND token = @token AND expiresOn > GetDate()", new { id, token });
        }
        public static bool CheckLogin(int id)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Token WHERE employeeId = @id", new { id});
        }

        public static int GetIdByEmail(string email)
        {
            return DBConnection.DatabaseConnection.QuerySingleOrDefault<int>(
                "SELECT id FROM Employee WHERE email = @email", new {email});
        }
        
    }
}
