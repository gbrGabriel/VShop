using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VShopIdentityServer.Configuration;
using VShopIdentityServer.Context;
using VShopIdentityServer.Identity;
using VShopIdentityServer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationIdentityContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationIdentityContext>(e =>
                e.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var builderIdentityServer = builder.Services.AddIdentityServer(opt =>
{
    opt.Events.RaiseErrorEvents = true;
    opt.Events.RaiseInformationEvents = true;
    opt.Events.RaiseFailureEvents = true;
    opt.Events.RaiseSuccessEvents = true;
    opt.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources).
        AddInMemoryApiScopes(IdentityConfiguration.ApiScopes).
        AddInMemoryClients(IdentityConfiguration.Clients).
        AddAspNetIdentity<ApplicationUser>();

builderIdentityServer.AddDeveloperSigningCredential();

builder.Services.AddScoped<IDatabaseIdentityInitialize, DatabaseIdentityServerInitializer>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection()
    ;
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

DatabaseIdentityServer(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void DatabaseIdentityServer(IApplicationBuilder builder)
{
    using (var scope = builder.ApplicationServices.CreateScope())
    {
        var initRoleUsers = scope.ServiceProvider.GetService<IDatabaseIdentityInitialize>();

        initRoleUsers.InitializeRoles().Wait();
        initRoleUsers.InitializeUser().Wait();
    }
}
