using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShopIdentityServer.Configuration;
using VShopIdentityServer.Interfaces;

namespace VShopIdentityServer.Identity;

public class DatabaseIdentityServerInitializer : IDatabaseIdentityInitialize
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeRoles()
    {
        if (!await _roleManager.RoleExistsAsync(IdentityConfiguration.Admin))
        {
            _roleManager.CreateAsync(new IdentityRole
            {
                Name = IdentityConfiguration.Admin,
                NormalizedName = IdentityConfiguration.Admin.ToUpper(),
            }).Wait();
        }

        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            _roleManager.CreateAsync(new IdentityRole
            {
                Name = IdentityConfiguration.Client,
                NormalizedName = IdentityConfiguration.Client.ToUpper(),
            }).Wait();
        }

    }

    public async Task InitializeUser()
    {
        if (await _userManager.FindByEmailAsync("admin1@com.br") == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin1",
                NormalizedUserName = IdentityConfiguration.Admin.ToUpper(),
                Email = "admin1@com.br",
                NormalizedEmail = "admin1@com.br".ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (16) 99740-1926",
                FirstName = "ADM",
                LastName = "Gabriel",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(admin, "Keysecretgenericone#2024!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin);

                await _userManager.AddClaimsAsync(admin, new Claim[]
               {
                    new(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new(JwtClaimTypes.GivenName, admin.FirstName),
                    new (JwtClaimTypes.FamilyName, admin.LastName),
                    new (JwtClaimTypes.Role, IdentityConfiguration.Admin)
               });
            }
        }

        if (await _userManager.FindByEmailAsync("client1@com.br") == null)
        {
            var client = new ApplicationUser
            {
                UserName = "client1",
                NormalizedUserName = IdentityConfiguration.Client.ToUpper(),
                Email = "client1@com.br",
                NormalizedEmail = "client1@com.br".ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (16) 99641-3112",
                FirstName = "User",
                LastName = "Juliana",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(client, "Keysecretgenericone#2024!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(client, IdentityConfiguration.Client);

                await _userManager.AddClaimsAsync(client, new Claim[]
               {
                    new(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new(JwtClaimTypes.GivenName, client.FirstName),
                    new (JwtClaimTypes.FamilyName, client.LastName),
                    new (JwtClaimTypes.Role, IdentityConfiguration.Admin)
               });
            }
        }
    }
}
