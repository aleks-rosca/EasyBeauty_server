using Dapper;
using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Models;

namespace EasyBeauty_server.Repository
{
    public static class CustomerRepo
    {
        public static void CreateCustomer(Customer customer)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Customer(phonenr,name, email) VALUES (@phonenr, @name, @email)", new { phonenr = customer.PhoneNr, name = customer.Name, email = customer.Email});
        }
        public static Customer GetCustomerByPhoneNumber(int phoneNr)
        {
            return DBConnection.DatabaseConnection.QuerySingle<Customer>("SELECT * FROM Customer WHERE phoneNr = @phoneNr", new { phoneNr });
        }

        public static void EditCustomer(int phoneNr, Customer customer)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Customer SET name = @name, email = @email WHERE phoneNr = @phoneNr", 
                new {name = customer.Name, email = customer.Email, phoneNr});
        }
        public static bool CheckCustomer(int phoneNr)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Customer WHERE phoneNr = @phoneNr", new { phoneNr });
        }
    }
}