using System;
using System.Text.Json;
using EasyBeauty_server.Models;

namespace EasyBeauty_server.Helpers
{
    public static class CookieEncDec
    {
        public static UserInfo DecryptCookie(string cookie)
        {
            if (cookie == null)
            {
                return new UserInfo();
            }
            var modCookie = cookie.Replace('-', '+').Replace('_', '/').PadRight(4*((cookie.Length+3)/4), '=');
            var decryptCookie = Convert.FromBase64String(modCookie);
            var user = JsonSerializer.Deserialize<UserInfo>(decryptCookie);
            return user;
        }

        public static string EncryptCookie(UserInfo user)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));
            var encryptCookie = Convert.ToBase64String(plainTextBytes);
            return encryptCookie;
        }
    }
}