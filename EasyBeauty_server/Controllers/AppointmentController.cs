using System.Linq;

namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAppointments()
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = AppointmentRepo.GetAppointments();
        
                    var list = (from a in result
                    let employee = new Employee {ID = a.EmployeeID, FullName = a.EmployeeName}
                    let service = new Service {ID = a.ServiceID, Name = a.ServiceName, Price = a.ServicePrice, Duration = a.ServiceDuration}
                    let customer = new Customer {FullName = a.CustomerName, PhoneNumber = a.PhoneNr, Email = a.CustomerEmail}
                    select new Appointment
                    {
                        ID = a.ID,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime,
                        Notes = a.Notes,
                        Employee = employee,
                        Service = service,
                        Customer = customer,
                        IsAccepted = a.IsAccepted
                    }).ToList();
        
                    return Ok(list);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpGet("/appointment/{employeeId}")]
        public IActionResult GetAppointmentsByEmployee(int employeeId)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = AppointmentRepo.GetAppointmentsByEmployee(employeeId);

                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeTimeSchedule(int employeeId)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = AppointmentRepo.GetEmployeeTimeSchedule(employeeId);

                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
        [HttpPost]
        public IActionResult CreateAppointment([FromBody] Appointment appointment)
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
                        return Ok(new { error = "Customer has an existing appointment" });
                    }
                    else
                    {
                        AppointmentRepo.CreateAppointment(appointment);
                        return Ok(new { error = "Appointment has been requested" });
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult EditAppointment(int id, [FromBody] AppointmentDB appointmentDB)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    AppointmentRepo.EditAppointment(id, appointmentDB);
                    return Ok(new {success = "Appointment saved" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    AppointmentRepo.DeleteAppointment(id);
                    return Ok(new { success = "Appointment deleted" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
    }
}
