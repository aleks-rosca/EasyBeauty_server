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
            return DBConnection.DatabaseConnection.Query<AppointmentDB>(@"
            SELECT id, startTime, endTime, notes, employeeID,
            (SELECT fullName FROM Employee WHERE ID = A.employeeId) AS EmployeeName, serviceID,
            (SELECT name FROM Service WHERE ID = A.serviceId) AS ServiceName, 
            (SELECT price FROM Service WHERE ID = A.serviceId) as ServicePrice,
            (SELECT duration FROM Service WHERE ID = A.serviceId) as ServiceDuration,
            (SELECT name FROM Customer WHERE phoneNr = A.phoneNr) AS CustomerName,
            (SELECT email FROM Customer WHERE phoneNr = A.phoneNr) AS CustomerEmail, phoneNr, isAccepted FROM Appointment AS A").ToList();
           
        }
        public static void CreateAppointment(Appointment appointment)
        {
            DBConnection.DatabaseConnection.Execute(@"INSERT INTO Appointment (employeeid, phonenr, serviceid, starttime, endtime, notes, isAccepted) VALUES (@employeeID, @phonenr, @serviceid, @starttime, @endtime, @notes, 0)",
            new { employeeid = appointment.EmployeeId, phonenr = appointment.PhoneNr, serviceid = appointment.ServiceId, starttime = appointment.StartTime, endtime = appointment.EndTime, notes = appointment.Notes });
        }

        public static bool CheckAppointment(int phoneNr)
        {
            return DBConnection.DatabaseConnection.ExecuteScalar<bool>(@"SELECT 1 FROM Appointment WHERE phonenr = @phoneNr AND starttime > GETDATE()", new { phoneNr });
        }
        public static void DeleteAppointment(int id)
        {
            DBConnection.DatabaseConnection.Execute(@"DELETE FROM Appointment WHERE ID = @id;", new { id});
        }

        public static void EditAppointment(int id, Appointment appointment)
        {
            DBConnection.DatabaseConnection.Execute(@"UPDATE Appointment SET employeeid = @employeeid, serviceid= @serviceid, starttime= @starttime, endtime= @endtime, isAccepted= @isAccepted WHERE ID = @id",
                new { id, employeeid = appointment.EmployeeId, serviceid = appointment.ServiceId, starttime = appointment.StartTime, endtime = appointment.EndTime, isAccepted= appointment.IsAccepted });
        }
        public static List<Appointment> GetAppointmentsByEmployee(int employeeId)
        {
            return DBConnection.DatabaseConnection.Query<Appointment>(@"
            SELECT id, startTime, endTime, serviceId, notes, employeeID,
            (SELECT name FROM Customer WHERE phoneNr = A.phoneNr) AS CustomerName,
            (SELECT email FROM Customer WHERE phoneNr = A.phoneNr) AS CustomerEmail, phoneNr, isAccepted 
            FROM Appointment AS A WHERE employeeID = @employeeId;", new { employeeId }).ToList();
        }
        public static List<EmployeeSchedule> GetEmployeeTimeSchedule(int employeeId)
        {
            return DBConnection.DatabaseConnection.Query<EmployeeSchedule>("Select startTime, endTime FROM Appointment WHERE employeeId = @employeeId AND startTime > GETDATE()", new { employeeId }).ToList();
        }
    }
}
