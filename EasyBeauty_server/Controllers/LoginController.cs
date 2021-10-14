﻿namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Helpers;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Text.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("check-email")]
        public EmailResponse CheckEmail([FromQuery]string email)
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

        [HttpPut("create-password")]
        public void CreatePassword([FromQuery]int id, string password)
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

        [HttpGet("login")]
        public string Login([FromQuery]int id, string email, string password)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var validateLogin = LoginRepo.CheckPassword(id, Hashing.HashString(password));
                    if (LoginRepo.CheckLogin(id)) { throw new ArgumentException("Already Loggen In"); }

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

        [HttpDelete("logout")]
        public void Logout([FromBody]int id, string token)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(id, token)) { throw new ArgumentException("Not Logged In"); }
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
