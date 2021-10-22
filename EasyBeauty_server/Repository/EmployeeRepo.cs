namespace EasyBeauty_server.Repository
{
    using Dapper;
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeRepo
    {
        public static void CreateEmployee(Employee employee)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Employee (fullName, phoneNr, email, role) Values (@fullName, @phoneNr, @email, @role)",
            new { fullName = employee.FullName, phoneNr = employee.PhoneNr, email = employee.Email, role = employee.Role });
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
            DBConnection.DatabaseConnection.Execute(@"UPDATE Employee SET fullName = @fullName, phoneNr = @phoneNr, email = @email, role = @role WHERE ID = @id",
                new { id, fullName = employee.FullName, phoneNr = employee.PhoneNr, email = employee.Email, role = employee.Role });
        }

        public static List<Employee> GetEmployees()
        {
            return DBConnection.DatabaseConnection.Query<Employee>("Select id, fullName, phoneNr, email, role from Employee").ToList();
        }
    }
}
