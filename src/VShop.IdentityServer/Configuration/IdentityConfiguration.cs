using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShopIdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope> {
            new("vshop","VShop Server"),
            new("read","Read data."),
            new("write","Write data."),
            new("delete","Delete data."),
        };

    public static IEnumerable<Client> Clients => new List<Client>
    {
        new() {
            ClientId = "client",
            ClientSecrets = {new Secret("keysecretstrongone".Sha256())},
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = {"read", "write","profile"}
        },
        new() {
            ClientId = "vshop",
            ClientSecrets = {new Secret("keysecretstrongone".Sha256())},
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = {"https://localhost:7109/signin-oidc"},
            PostLogoutRedirectUris = { "https://localhost:7109/signout-callback-oidc" },
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "vshop"
            }
        }
    };
}
