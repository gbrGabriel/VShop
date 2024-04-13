using VShopWeb.Interfaces;
using VShopWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("MicroserviceProduct", e => e.BaseAddress = new
                                    Uri(builder.Configuration["MicroserviceProduct:UrlApi"] ?? string.Empty));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IServiceCategory, ServiceCategory>();

builder.Services.AddScoped<IServiceProduct, ServiceProduct>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
