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
        public IActionResult CheckEmail([FromQuery]string email)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                   var result = LoginRepo.CheckEmail(email);
                    if (result.Password == "" || result.Password == null)
                    {
                        return Ok(new { hasLogin = false });
                    }
                    else return Ok(new { hasLogin = true });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "This email does not have an account" });
                
            }
        }

        [HttpPut("create-password")]
        public IActionResult CreatePassword([FromQuery]string email, string password, string repeatedPassword)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (password == repeatedPassword || password.Length > 6 || repeatedPassword.Length > 6)
                    {
                        LoginRepo.CreatePassword(email, Hashing.HashString(password));
                        return Ok(new { success = "password created" });
                    }
                    else
                    {
                        return Ok(new { error = "password invalid" });
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpGet("login")]
        public IActionResult Login([FromQuery] string email, string password)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var validateLogin = LoginRepo.CheckPassword(email, Hashing.HashString(password));

                    if (!validateLogin) { return Ok(new { error = "Password incorrect" }); }
                    
                    var userInfo = LoginRepo.GetUserInfo(email);
             
                    var token = Hashing.HashString(email, DateTime.Now.ToString());
                    userInfo.Token = token;

                    LoginRepo.SetToken(userInfo.Id, token);
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(userInfo));
                    var encryptCookie = Convert.ToBase64String(plainTextBytes);
                    return Ok(new { cookie = encryptCookie });

                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete("logout")]
        public IActionResult Logout([FromQuery]int id, string token)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(id, token)) { return Ok(new { error = "Not logged in" }); }
                    LoginRepo.RemoveToken(token);
                    return Ok(new { success = "Logged out" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
    }
}
