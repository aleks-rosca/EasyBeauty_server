namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        public List<AppointmentDB> GetAppointments()
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = AppointmentRepo.GetAppointments();
                    
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<AppointmentDB>();
            }
        }

        [HttpGet("/pizdira/{employeeId}")]
        public List<AppointmentDB> GetAppointmentsByEmployee(int employeeId)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = AppointmentRepo.GetAppointmentsByEmployee(employeeId);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<AppointmentDB>();
            }
        }
        [HttpGet("{employeeId}")]
        public List<EmployeeSchedule> GetEmployeeTimeSchedule(int employeeId)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = AppointmentRepo.GetEmployeeTimeSchedule(employeeId);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<EmployeeSchedule>();
            }
        }
        [HttpPost]
        public string CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!AppointmentRepo.CheckCustomer(appointment.Customer.PhoneNumber))
                    {
                        AppointmentRepo.CreateCustomer(appointment.Customer);
                    }
                    if(AppointmentRepo.CheckAppointment(appointment.Customer.PhoneNumber))
                    {
                        return "Customer has an existing appointment";
                    }
                    else
                    {
                        AppointmentRepo.CreateAppointment(appointment);
                        return "Appointment has been requested";
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return "";
        }

        [HttpPut("{id}")]
        public void EditAppointment(int id, [FromBody] AppointmentDB appointmentDB)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    AppointmentRepo.EditAppointment(id, appointmentDB);

                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    AppointmentRepo.DeleteAppointment(id);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
