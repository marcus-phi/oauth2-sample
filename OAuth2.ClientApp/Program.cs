using System;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace OAuth2.ClientApp
{
    class Program
    {
        static int Main(string[] args)
        {
            // request token
            var tokenClient = new TokenClient("http://id-server/connect/token", "john.doe", "jdoe1234");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine("Token error: " + tokenResponse.Error);
                return 1;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var apiResponse = httpClient.GetAsync("http://web-api/api/values").Result;
            if (!apiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiResponse.StatusCode);
                Console.WriteLine(apiResponse.Headers.WwwAuthenticate);
                return 1;
            }

            var content = apiResponse.Content.ReadAsStringAsync().Result;
            Console.WriteLine(JArray.Parse(content));

            return 0;
        }
    }
}
