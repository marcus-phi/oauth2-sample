using System.Collections.Generic;
using IdentityServer4.Models;

namespace OAuth2.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("api1", "My Api"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client()
                {
                    ClientId = "john.doe",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("jdoe1234".Sha256())
                    },

                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}