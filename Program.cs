using IGlassAPI.Data;
using IGlassAPI.Middleware;
using IGlassAPI.Queue;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitQueueWorkerHybrid.RefitClients;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MasterRepository>();

if (builder.Configuration["QueueProvider"] == "Azure")
    builder.Services.AddSingleton<IQueueProvider, AzureServiceBusQueueProvider>();
else
    builder.Services.AddSingleton<IQueueProvider, RabbitMqQueueProvider>();

builder.Services.AddDbContext<IGlassAPI.Data.ClientLogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddDbContext<MasterDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("Postgres");
    options.UseNpgsql(connStr); 
});
builder.Services.AddRefitClient<IApiClient>()
    .ConfigureHttpClient((sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        var baseUrl = config["RefitClients:PayloadApi:BaseUrl"];
        client.BaseAddress = new Uri(baseUrl);
    });

var app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
IGlassAPI.Data.MigrationRunner.RunMigrations(app);
