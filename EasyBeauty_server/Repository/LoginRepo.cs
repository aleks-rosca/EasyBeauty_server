namespace EasyBeauty_server.Repository
{
    using Dapper;
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;

    public class LoginRepo
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
            return DBConnection.DatabaseConnection.QuerySingle<UserInfo>(@"SELECT fullName, id, role FROM Employee WHERE email = @email", new { email });
        }
        
        public static void SetToken(int id, string token)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Token(token, employeeid, expiresOn) VALUES (@token, @id, DATEADD(MINUTE, +30, GETDATE()))", new { token,  id });
        }

        public static void RemoveToken(string token)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Token WHERE token = @token", new { token });
        }

        public static bool CheckToken(int id, string token)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Token WHERE employeeid = @id AND token = @token", new { id, token });
        }

        public static bool CheckLogin(int id)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Token WHERE employeeid = @id AND expiresOn > GETDATE()", new { id });
        }
    }
}
