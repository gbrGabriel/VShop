using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

namespace IoC;

public static class DependencyInjectionModule
{
    public static void Register(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IRepositoryCategory, RepositoryCategory>();
        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IServiceCategory, ServiceCategory>();
        services.AddScoped<IServiceProduct, ServiceProduct>();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            e => e.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );
    }
}
