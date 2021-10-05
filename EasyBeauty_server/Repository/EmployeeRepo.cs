using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace EasyBeauty_server.Repository
{
    public class EmployeeRepo
    {
        public static void CreateEmployee(Employee employee)
        {

        }
        public static void deleteEmployee(Employee employee)
        {

        }
        public static void EditEmployee(Employee employee)
        {

        }
        public static List<Employee> GetEmployees()
        {
            return DBConnection.DatabaseConnection.Query<Employee>("Select * from Employee").ToList();
        }
    }
}
