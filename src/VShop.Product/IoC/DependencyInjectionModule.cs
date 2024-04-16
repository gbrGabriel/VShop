using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VShopProduct.Context;
using VShopProduct.Interfaces;
using VShopProduct.Repositories;
using VShopProduct.Services;

namespace IoC;

public static class DependencyInjectionModule
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IRepositoryCategory, RepositoryCategory>();
        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IServiceCategory, ServiceCategory>();
        services.AddScoped<IServiceProduct, ServiceProduct>();

        services.AddAuthentication("Bearer").AddJwtBearer("Bearer", opt =>
        {
            opt.Authority = configuration["VShop.IdentityServer:ApplicationUrl"] ?? string.Empty;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "vshop");
            });
        });
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            e => e.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );
    }
}
