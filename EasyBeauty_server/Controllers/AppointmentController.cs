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
        public List<Appointment> GetAppointments()
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    
                    var list = new List<Appointment>();
                    var result = AppointmentRepo.GetAppointments();

                    foreach (var a in result)
                    {

                        var employee = new Employee
                        {
                            ID = a.EmployeeID,
                            FullName = a.EmployeeName
                        };
                        var service = new Service
                        {
                            ID = a.ServiceID,
                            Name = a.ServiceName
                        };
                        var customer = new Customer
                        {
                            FullName = a.CustomerName,
                            PhoneNumber = a.PhoneNr
                        };

                        var appointment = new Appointment
                        {
                            ID = a.ID,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Notes = a.Notes,
                            Employee = employee,
                            Service = service,
                            Customer = customer,
                            IsAccepted = a.IsAccepted
                        };
                        list.Add(appointment);
                    }

                    return list;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<Appointment>();
            }
        }

        [HttpGet("/appointment/{employeeId}")]
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
