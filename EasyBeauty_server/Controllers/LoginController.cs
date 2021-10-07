using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Helpers;
using EasyBeauty_server.Models;
using EasyBeauty_server.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyBeauty_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        // GET api/<LoginController>/5
        [HttpGet("{email}")]
        public EmailResponse CheckEmail(string email)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = LoginRepo.CheckEmail(email);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }

        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}, {password}")]
        public void CreatePassword(int id,string password)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    LoginRepo.CreatePassword(id, Hashing.HashString(password));
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        // POST api/<LoginController>
        
        [HttpGet("{id},{email},{password}")]
        public string CheckPassword(int id, string email, string password)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                   var validateLogin = LoginRepo.CheckPassword(id, Hashing.HashString(password));
                    if (LoginRepo.CheckLogin(id)){ throw new ArgumentException("Already Loggen In"); }

                    if (!validateLogin) { throw new ArgumentException("Not Autenticated"); }

                    var token = Hashing.HashString(email, DateTime.Now.ToString());
                    LoginRepo.SetToken(id, token);
                    return token;

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return e.Message;
            }
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}, {token}")]
        public void Logout(int id, string token)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(id, token)){ throw new ArgumentException("Not Logged In"); }
                    LoginRepo.RemoveToken(token);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

        }
    }
}
