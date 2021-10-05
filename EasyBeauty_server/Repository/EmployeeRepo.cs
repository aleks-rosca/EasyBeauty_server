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
            try
            {
                DBConnection.DatabaseConnection.Execute("INSERT INTO Employee (fullName, phoneNr, email, password, role) Values (@fullName, @phoneNr, @email, @password, @role)",
                new { fullName = employee.FullName, phoneNr = employee.PhoneNr, email = employee.Email, password = employee.Password, role = employee.Role });
            }
            catch (SqlException ex)
            {
                string errorMessages = "";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages =("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n"); 
                }
                Console.WriteLine(errorMessages.ToString());

            }
        }
        public static void DeleteEmployee(Employee employee)
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
