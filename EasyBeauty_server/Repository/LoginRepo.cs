using EasyBeauty_server.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyBeauty_server.Models;

namespace EasyBeauty_server.Repository
{
    public class LoginRepo
    {
        public static EmailResponse CheckEmail(string email)
        {
             return DBConnection.DatabaseConnection.QuerySingle<EmailResponse>(@"SELECT id , (SELECT 1 FROM Employee Where email = @email AND password != '')AS hasPassword FROM Employee WHERE email = @email", new { email = email });

        }
        public static void CreatePassword(int id, string password)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Employee SET password=@password WHERE id=@id", new {id=id, password=password });
        }
        public static bool CheckPassword(int id, string password)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Employee Where id = @id AND password = @password", new {id=id, password=password});
        }
        public static void SetToken(int id, string token)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Token(token, employeeid) VALUES (@token, @id)", new { token=token, id=id });
        }
        public static void RemoveToken(string token)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Token where token = @token", new { token = token});
        }
        public static bool CheckToken(int id, string token)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Token Where employeeid = @id AND token = @token", new { id = id, token = token });
        }
        public static bool CheckLogin(int id)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Token Where employeeid = @id", new { id = id });
        }
    }
}
