using Dapper;
using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBeauty_server.Repository
{
    public class AppointmentRepo
    {
        public static List<AppointmentDB> GetAppointments()
        {
            return DBConnection.DatabaseConnection.Query<AppointmentDB>("Select * from Appointment").ToList();
           
        }
        public static void CreateAppointment(Appointment appointment)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Appointment (employeeid, phonenr, serviceid, starttime, endtime, notes, isAccepted) VALUES (@employeeID, @phonenr, @serviceid, @starttime, @endtime, @notes, 0)",
            new { employeeid = appointment.EmployeeID, phonenr = appointment.Customer.PhoneNumber, serviceid = appointment.ServiceID, starttime = appointment.StartTime, endtime = appointment.EndTime, notes = appointment.Notes });
        }
        public static void CreateCustomer(Customer customer)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Customer(phonenr,name, email) VALUES (@phonenr, @name, @email)", new { phonenr = customer.PhoneNumber, name = customer.FullName, email = customer.Email});
        }
        public static bool CheckCustomer(int phoneNr)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Customer WHERE phonenr = @phoneNr", new { phoneNr });
        }
        public static bool CheckAppointment(int phoneNr)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Appointment WHERE phonenr = @phoneNr", new { phoneNr });
        }
        public static void DeleteAppointment(int id)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Appointment WHERE ID = @id;", new { id});
        }

        public static void EditAppointment(int id, AppointmentDB appointmentDB)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Appointment SET employeeid = @employeeid, serviceid= @serviceid, starttime= @starttime, endtime= @endtime, isAccepted= @isAccepted WHERE ID = @id",
                new { id = id, employeeid = appointmentDB.EmployeeID, serviceid = appointmentDB.ServiceID, starttime = appointmentDB.StartTime, endtime = appointmentDB.EndTime, isAccepted= appointmentDB.IsAccepted });
        }
        public static List<AppointmentDB> GetAppointmentsByEmployee(int employeeId)
        {
            return DBConnection.DatabaseConnection.Query<AppointmentDB>("Select * FROM Appointment WHERE employeeId = @employeeId", new { employeeId }).ToList();
        }
        public static List<EmployeeSchedule> GetEmployeeTimeSchedule(int employeeId)
        {
            return DBConnection.DatabaseConnection.Query<EmployeeSchedule>("Select startTime, endTime FROM Appointment WHERE employeeId = @employeeId AND startTime > GETDATE()", new { employeeId }).ToList();
        }
    }
}
