using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IGlassAPI.Middleware;
using IGlassAPI.Queue;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;
var queueProvider = config["QueueProvider"];

if (queueProvider == "Azure")
    builder.Services.AddSingleton<IQueueProvider, AzureServiceBusQueueProvider>();
else
    builder.Services.AddSingleton<IQueueProvider, RabbitMqQueueProvider>();

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();