
using Newtonsoft.Json;

namespace EasyBeauty_server.Models
{
    public class UserInfo
    {
        [JsonProperty("Id")]public int Id { get; set; }
        [JsonProperty("Name")]public string Name { get; set; }
        [JsonProperty("Role")]public string Role { get; set; }
        [JsonProperty("Token")]public string Token { get; set; }
    }
}
