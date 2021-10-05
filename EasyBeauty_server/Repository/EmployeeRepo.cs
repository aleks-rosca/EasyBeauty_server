using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System;

namespace EasyBeauty_server.Repository
{
    public class EmployeeRepo
    {
        public static void CreateEmployee(Employee employee)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Employee (fullName, phoneNr, email, password, role) Values (@fullName, @phoneNr, @email, @password, @role)",
            new { fullName = employee.FullName, phoneNr = employee.PhoneNr, email = employee.Email, password = employee.Password, role = employee.Role });
        }
        public static void DeleteEmployee(int id)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Employee WHERE ID =" + id + ";");
        }
        public static void EditEmployee(int id, Employee employee)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Employee SET fullName = @fullName, phoneNr = @phoneNr, email = @email, password = @password, role = @role WHERE ID = @id",
                new { id = id, fullName = employee.FullName, phoneNr = employee.PhoneNr, email = employee.Email, password = employee.Password, role = employee.Role });

        }
        public static List<Employee> GetEmployees()
        {
            return DBConnection.DatabaseConnection.Query<Employee>("Select * from Employee").ToList();
        }
    }
}
