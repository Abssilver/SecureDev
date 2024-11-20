using Authentication.Abstractions.Dto;
using Microsoft.Extensions.DependencyInjection;
using Validation.Abstractions;
using Validation.Implementation.Authentication;
using Validation.Implementation.BusinessLogic;

namespace Validation.Implementation;

public static class Registration
{
    public static IServiceCollection RegisterValidation(this IServiceCollection services)
    {
        services.AddSingleton<IBusinessLogicOperationFailureFactory, BusinessLogicOperationFailureFactory>();
        services.AddSingleton<IAuthOperationFailureFactory, AuthOperationFailureFactory>();
        
        services.AddTransient<IValidationService<AuthRequestDto>, AuthRequestValidation>();
        services.AddTransient<IValidationService<CreateAccountDto>, CreateAccountValidation>();
        return services;
    }
}