
using System.Globalization;

namespace EasyBeauty_server.Controllers
{
    using DataAccess;
    using Models;
    using Repository;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAppointmentsByEmployee([FromQuery]int employeeId)
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
                return StatusCode(500, new{error = e});
            }
        }
        [HttpGet("GetEmployeeSchedule")]
        public IActionResult GetEmployeeTimeSchedule([FromQuery]int employeeId)
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
                return StatusCode(500, new{error = e});
            }
        }

        [HttpGet("CheckCustomer")]
        public IActionResult CheckCustomer([FromQuery]int phoneNr)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    return CustomerRepo.CheckCustomer(phoneNr) ? Ok(CustomerRepo.GetCustomerByPhoneNumber(phoneNr)) : Ok(new {isCustomer = false, phoneNr});
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new {error = e});
            }
            
        }
        
        [HttpPost]
        public IActionResult CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!CustomerRepo.CheckCustomer(appointment.PhoneNr))
                    {
                        var customer = new Customer
                        {
                            PhoneNr = appointment.PhoneNr,
                            Name = appointment.CustomerName,
                            Email = appointment.CustomerEmail
                        };
                        CustomerRepo.CreateCustomer(customer);
                        
                    }
                    else
                    {
                        var customer = CustomerRepo.GetCustomerByPhoneNumber(appointment.PhoneNr);
                        if (!(appointment.CustomerName == customer.Name && appointment.CustomerEmail == customer.Email))
                        {
                            var newCustomer = new Customer
                            {
                                Email = appointment.CustomerEmail,
                                Name = appointment.CustomerName,
                                PhoneNr = appointment.PhoneNr
                            };
                            CustomerRepo.EditCustomer(appointment.PhoneNr, newCustomer);
                        }
                    }
                    
                    if(AppointmentRepo.CheckAppointment(appointment.PhoneNr))
                    {
                        return Ok(new { error = "Customer has an existing appointment" });
                    }
                    else
                    {
                        AppointmentRepo.CreateAppointment(appointment);
                        return Ok(new { success = "Appointment has been requested on " + appointment.StartTime.ToString("dddd, dd MMMM", CultureInfo.InvariantCulture) + " at "+ appointment.StartTime.ToString("HH:mm")});
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }

        [HttpPut]
        public IActionResult EditAppointment([FromQuery]int id, [FromBody] Appointment appointment, [FromQuery]string cookie)
        {
            if (string.IsNullOrEmpty(cookie)) return BadRequest(new {error = "internal error"});
            
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(user.Id, user.Token))
                    {
                        LoginRepo.RemoveToken(user.Id);
                        return Ok(new { error = "Not logged in" });
                    }
                    AppointmentRepo.EditAppointment(id, appointment);
                    //Notify.SendSMS(appointment.CustomerName+","+"Appointment has been approved on " + appointment.StartTime.ToString("dddd, dd MMMM", CultureInfo.InvariantCulture) + " at "+ appointment.StartTime.ToString("HH:mm"), appointment.PhoneNr);
                    return Ok(new {success = "Appointment saved" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int id, [FromQuery]string cookie)
        {
            if (string.IsNullOrEmpty(cookie)) return BadRequest(new {error = "internal error"});
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(user.Id, user.Token))
                    {
                        LoginRepo.RemoveToken(user.Id);
                        return Ok(new { error = "Not logged in" });
                    }
                    AppointmentRepo.DeleteAppointment(id);
                    return Ok(new { success = "Appointment deleted" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }
    }
}
