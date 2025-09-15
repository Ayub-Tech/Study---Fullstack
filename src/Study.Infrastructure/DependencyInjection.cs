using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Study.Application.Common.Interfaces;
using Study.Infrastructure.Persistence;
using Study.Infrastructure.Repositories;

namespace Study.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlite(connectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
