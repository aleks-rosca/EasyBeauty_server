
using Newtonsoft.Json;

namespace EasyBeauty_server.Models
{
    public class UserInfo
    {
        [JsonProperty("Id")]public int Id { get; set; }
        [JsonProperty("FullName")]public string FullName { get; set; }
        [JsonProperty("Role")]public string Role { get; set; }
        [JsonProperty("Token")]public string Token { get; set; }
    }
}
