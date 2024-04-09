using Context;
using Microsoft.EntityFrameworkCore;

namespace IoC;

public static class DependencyInjectionModule
{
    public static void Register(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            e => e.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );
    }
}
