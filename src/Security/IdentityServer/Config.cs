using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        private const string CATALOG_SCOPE = "Catalog";
        private static readonly string SECRET = "cmA1Z5oHWB";

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                   new Client
                   {
                        ClientId = "CatalogClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret(SECRET.Sha256())
                        },
                        AllowedScopes = { CATALOG_SCOPE }
                   }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope(CATALOG_SCOPE, "Catalog API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               //new ApiResource("eShop", "eShop API")
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource("roles", "Your role(s)", new List<string>() { "role" })
          };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "daniel",
                    Password = "abc123",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "daniel"),
                        new Claim(JwtClaimTypes.FamilyName, "souza")
                    }
                }
            };
    }
}
