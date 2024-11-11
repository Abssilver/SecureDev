using Microsoft.Extensions.DependencyInjection;
using Validation.Abstractions;
using Validation.Implementation.Authentication;
using Validation.Implementation.BusinessLogic;

namespace Validation.Implementation;

public static class Registration
{
    public static IServiceCollection RegisterValidation(this IServiceCollection services)
    {
        services.AddScoped<IBusinessLogicOperationFailureFactory, BusinessLogicOperationFailureFactory>();
        services.AddScoped<IAuthOperationFailureFactory, AuthOperationFailureFactory>();
        return services;
    }
}