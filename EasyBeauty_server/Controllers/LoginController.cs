namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Helpers;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using System.Text.Json;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("check-email")]
        public object CheckEmail([FromQuery]string email)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                   var result = LoginRepo.CheckEmail(email);
                    return new { hasLogin = result };
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new { error = "This email doesn't have an account" };
            }
        }

        [HttpPut("create-password")]
        public void CreatePassword([FromQuery]string email, string password)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    LoginRepo.CreatePassword(email, Hashing.HashString(password));
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        [HttpGet("login")]
        public object Login([FromQuery] string email, string password)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var validateLogin = LoginRepo.CheckPassword(email, Hashing.HashString(password));

                    if (!validateLogin) { return new { error = "Password incorrect" }; }
                    
                    var userInfo = LoginRepo.GetUserInfo(email);
             
                    var token = Hashing.HashString(email, DateTime.Now.ToString());
                    userInfo.Token = token;

                    LoginRepo.SetToken(userInfo.Id, token);
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(userInfo));
                    var encryptCookie = Convert.ToBase64String(plainTextBytes);
                    return new { cookie = encryptCookie };

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return e.Message;
            }
        }

        [HttpDelete("logout")]
        public void Logout([FromQuery]int id, string token)
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
