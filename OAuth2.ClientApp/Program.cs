using System;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace OAuth2.ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // discover endpoints from metadata
            var discoveryClient = new DiscoveryClient("http://localhost:8080");
            discoveryClient.Policy.ValidateIssuerName = false;
            var disco = discoveryClient.GetAsync().Result;
            if (disco.IsError)
            {
                Console.WriteLine("Discovery error: " + disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "john.doe", "jdoe1234");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine("Token error: " + tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var apiResponse = httpClient.GetAsync("http://localhost:8081/api/values").Result;
            if (!apiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiResponse.StatusCode);
                Console.WriteLine(apiResponse.Headers.WwwAuthenticate);
                return;
            }

            var content = apiResponse.Content.ReadAsStringAsync().Result;
            Console.WriteLine(JArray.Parse(content));
        }
    }
}
