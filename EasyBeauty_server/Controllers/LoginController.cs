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
                   return Ok(string.IsNullOrEmpty(result.Password) ? new { hasLogin = false } : new { hasLogin = true });
                }
            }
            catch (Exception)
            {
                return StatusCode(401, new { error = "This email does not have an account" });
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

                    return Ok(new { error = "password invalid" });
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
                    var encryptCookie = CookieEncDec.EncryptCookie(userInfo);
                    return Ok(new { cookie = encryptCookie });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }

        [HttpDelete("logout")]
        public IActionResult Logout([FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(user.Id, user.Token)) { return Ok(new { error = "Not logged in" }); }
                    LoginRepo.RemoveToken(user.Token);
                    return Ok(new { success = "Logged out" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }
    }
}
