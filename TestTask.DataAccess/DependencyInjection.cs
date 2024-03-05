using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Common.Interfaces;
using TestTask.DataAccess.Repositories;

namespace TestTask.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDbAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TestTaskDbContext>(options =>
            options.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString(TestTaskDbContext.ConnectionStringName)));

        services.AddScoped<ITreeRepository, TreeRepository>();
        services.AddScoped<IJournalRepository, JournalRepository>();

        return services;
    }
}