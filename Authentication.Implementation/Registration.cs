using Authentication.Abstractions.Configs;
using Authentication.Abstractions.Services;
using Authentication.Implementation.Profiles;
using Authentication.Implementation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Implementation;

public static class Registration
{
    public static IServiceCollection RegisterAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        configuration.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "authentication.settings.json"));
        services.Configure<SecurityConfig>(configuration);
        services.Configure<JwtConfig>(configuration);
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        services.AddScoped<IPasswordSecurityService, PasswordSecurityService>();

        services.AddMemoryCache();
        services.AddAutoMapper(conf => conf.AddProfile(new AuthenticationMapperProfile()));
        return services;
    }
}