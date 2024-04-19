using Microsoft.AspNetCore.Authentication;
using VShopWeb.Interfaces;
using VShopWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("MicroserviceProduct", e => e.BaseAddress = new
                                    Uri(builder.Configuration["MicroserviceProduct:UrlApi"] ?? string.Empty));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IServiceCategory, ServiceCategory>();

builder.Services.AddScoped<IServiceProduct, ServiceProduct>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", e => e.ExpireTimeSpan = TimeSpan.FromMinutes(10)).AddOpenIdConnect("oidc", opt =>
{
    opt.Authority = builder.Configuration["MicroserviceProduct:IdentityServer"];
    opt.GetClaimsFromUserInfoEndpoint = true;
    opt.ClientId = "vshop";
    opt.ClientSecret = builder.Configuration["Client:Secret"];
    opt.ResponseType = "code";
    opt.ClaimActions.MapJsonKey("role", "role", "role");
    opt.ClaimActions.MapJsonKey("sub", "sub", "sub");
    opt.TokenValidationParameters.NameClaimType = "name";
    opt.TokenValidationParameters.RoleClaimType = "role";
    opt.Scope.Add("vshop");
    opt.SaveTokens = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
