// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string>{"role"}),
                new IdentityResource("country", "Your country", new List<string> { "country" })
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("employeemanagementapi", "EmployeeManagement API")
                {
                    Scopes = { "employeemanagementapi.scope" },
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("employeemanagementapi.scope", "EmployeeManagement API Scope")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            { 
                new Client
                {
                    ClientName = "EmployeeManagementClient",
                    ClientId = "employeemanagementclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> { "https://localhost:5010/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:5010/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile, 
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "employeemanagementapi.scope",
                        "country"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha512())
                    },
                    RequirePkce = true,
                    RequireConsent = true,
                    ClientUri = "https://localhost:5010"
                }
            };
    }
}