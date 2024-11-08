using BusinessLogic.Abstractions;
using BusinessLogic.Implementation.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Implementation;

public static class Registration
{
    public static IServiceCollection RegisterBusinessLogic(this IServiceCollection services)
    {
        services.AddAutoMapper(configuration => configuration.AddProfile(new BusinessLogicMapperProfile()));
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ICardService, CardService>();
        return services;
    }
}