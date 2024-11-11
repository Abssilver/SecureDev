using Authentication.Implementation;
using BusinessLogic.Implementation;
using DAL.Implementation;
using Microsoft.AspNetCore.HttpLogging;
using NLog.Web;
using Validation.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureLogging(builder);

builder.Services.RegisterBusinessLogic();
builder.Services.RegisterDataLayer(builder.Configuration);
builder.Services.RegisterValidation();
builder.Services.RegisterAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureLogging(WebApplicationBuilder appBuilder)
{
    appBuilder.Logging.ClearProviders();
    appBuilder.Logging.AddConsole();
    appBuilder.Host.UseNLog(new NLogAspNetCoreOptions { RemoveLoggerFactoryFilter = true });
    
    appBuilder.Services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
        logging.RequestBodyLogLimit = 4096;
        logging.ResponseBodyLogLimit = 4096;
        logging.RequestHeaders.Add("Authorization");
        logging.RequestHeaders.Add("X-Real-IP");
        logging.RequestHeaders.Add("X-Forwarded-For");
    });
}