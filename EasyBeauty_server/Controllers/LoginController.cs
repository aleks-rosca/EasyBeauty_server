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
                    Console.WriteLine(result.Password);
                    if (result.Password == "" || result.Password == null)
                    {
                        return new { hasLogin = false };
                    }
                    else return new { hasLogin = true };
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new { error = "This email doesn't have an account" };
            }
        }

        [HttpPut("create-password")]
        public object CreatePassword([FromQuery]string email, string password, string repeatedPassword)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (password == repeatedPassword || password.Length > 6 || repeatedPassword.Length > 6)
                    {
                    LoginRepo.CreatePassword(email, Hashing.HashString(password));
                        return new { success = "password created" };
                    }
                    else
                    {
                        return new { error = "password invalid" };
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new { error = e.Message };
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
        public object Logout([FromQuery]int id, string token)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(id, token)) { return new { error = "Not logged in" }; };
                    LoginRepo.RemoveToken(token);
                    return new { success = "Logged out" };
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new { error = "Something interrupted" };
            }
        }
    }
}
