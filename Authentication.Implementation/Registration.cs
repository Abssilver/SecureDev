using System.Text;
using Authentication.Abstractions.Configs;
using Authentication.Abstractions.Services;
using Authentication.Implementation.Profiles;
using Authentication.Implementation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        
        services.AddCors();
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new
                    TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSecretKey"] 
                            ?? throw new Exception("Jwt secrets was not found"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
            });

        return services;
    }
    
    public static void AuthenticationOptions(this SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "CardStorageService", Version = "v1" });
        options.CustomOperationIds(GetControllersShortMethodNames);
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer token_text_value')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }
    
    private static string GetControllersShortMethodNames(ApiDescription description)
    {
        var controllerName = description.ActionDescriptor.RouteValues["controller"];
        var actionName = description.ActionDescriptor.Id;

        if (!description.TryGetMethodInfo(out var methodInfo))
            return $"{controllerName}_{actionName}";
        
        actionName = methodInfo.Name;
        if (actionName.EndsWith("Async"))
            actionName = actionName.Substring(0, actionName.Length - 5);

        return $"{controllerName}_{actionName}";
    }
}