using System.Net;
using GRPCServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GRPCServices;

public static class Registration
{
    public static IServiceCollection RegisterGrpc(this IServiceCollection services, IWebHostBuilder builder)
    {
        services.AddGrpc();
        builder.ConfigureKestrel((context, serverOptions) =>
        {
            var config = context.Configuration.Get<HttpsConfig>();
            serverOptions.Listen(IPAddress.Any, config.HttpsPort, options =>
            {
                options.Protocols = HttpProtocols.Http2;
                options.UseHttps(config.CertificatePath, config.CertificatePassword);
            });
        });
        return services;
    }

    public static void MapGrpcServices(this WebApplication application)
    {
        application.MapGrpcService<GrpcClientService>();
    }
}