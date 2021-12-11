
namespace EasyBeauty_server.Repository
{
    using Dapper;
    using DataAccess;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class EmployeeRepo
    {
        public static void CreateEmployee(Employee employee)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Employee (name, phoneNr, email, role) Values (@name, @phoneNr, @email, @role)",
            new { name = employee.Name, phoneNr = employee.PhoneNr, email = employee.Email, role = employee.Role });
        }

        public static bool CheckEmployeeEmail(string email)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Employee Where email = @email", new { email });
        }

        public static void DeleteEmployee(int id)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Employee WHERE ID = @id;", new { id });
        }

        public static void EditEmployee(int id, Employee employee)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Employee SET name = @name, phoneNr = @phoneNr, email = @email, role = @role WHERE ID = @id",
                new { id, name = employee.Name, phoneNr = employee.PhoneNr, email = employee.Email, role = employee.Role });
        }

        public static List<Employee> GetEmployees()
        {
            return DBConnection.DatabaseConnection.Query<Employee>("Select id, name, phoneNr, email, role from Employee").ToList();
        }
        
        public static string GetRole(int id)
        {
            return DBConnection.DatabaseConnection.QueryFirstOrDefault<string>("Select role FROM Employee WHERE ID = @id",new {id});
        }
    }
}
