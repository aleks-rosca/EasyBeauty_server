using System;

namespace EasyBeauty_server.Helpers
{
    public abstract class Notify
    {
        public static void SendSMS(string text, int number)
        {
            var client = new RestSharp.RestClient("https://gatewayapi.com/rest/");
            const string apiToken = "Dd50JkMySlOQ9fJ6szmfiIqYNrbjPGBergmTmF5GGUziR_KE1_qexPDPT2X4AVlB";

            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(apiToken, "");
            var request = new RestSharp.RestRequest("mtsms", RestSharp.Method.POST);
            request.AddJsonBody(new
            {
                sender = "EasyBeauty",
                message = text,
                recipients = new[] { new { msisdn = number } }
            });
            var response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                var res = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                foreach (var i in res["usage"])
                {
                    Console.WriteLine(i);
                }
            }
            else if (response.ResponseStatus == RestSharp.ResponseStatus.Completed)
            {
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
            }

        }
        
    }

}