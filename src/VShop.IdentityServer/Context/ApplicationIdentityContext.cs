using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VShopIdentityServer.Identity;

namespace VShopIdentityServer.Context;

public class ApplicationIdentityContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options) : base(options) { }
}
