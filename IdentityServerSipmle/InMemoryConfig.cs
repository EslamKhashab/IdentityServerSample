using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerSipmle
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
                      new List<IdentityResource>
                          {
                            new IdentityResources.OpenId(),
                            new IdentityResources.Profile(),
                            new IdentityResource("roles", "User role(s)", new List<string> { "role" })

                          };
       
        public static IEnumerable<Client> GetClients() =>
    new List<Client>
    {
       new Client
       {
            ClientId = "company-employee",
            ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "ApiTest", "roles" }

        },
       new Client
        {
            ClientName = "MVC Client",
            ClientId = "mvc-client",
            AllowedGrantTypes = GrantTypes.Hybrid,
            RedirectUris = new List<string>{ "https://localhost:5010/signin-oidc" },
            RequirePkce = false,
            AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile },
            ClientSecrets = { new Secret("MVCSecret".Sha512()) }
        }
    };
        public static IEnumerable<ApiResource> GetApiResources() =>
    new List<ApiResource>
    {
        new ApiResource("companyApi", "CompanyEmployee API")
        {
            Scopes = { "ApiTest" }
        }
    };
        public static IEnumerable<ApiScope> GetApiScopes() =>
    new List<ApiScope> { new ApiScope("ApiTest", "CompanyEmployee API") };
    }
}
