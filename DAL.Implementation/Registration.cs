using DAL.Abstractions;
using DAL.Implementation.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Implementation;

public static class Registration
{
    public static IServiceCollection RegisterDataLayer(this IServiceCollection services, ConfigurationManager configuration)
    {
        configuration.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database.settings.json"));
        services.AddDbContext<CardStorageServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
        });
        
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        return services;
    }
}