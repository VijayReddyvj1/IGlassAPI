using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IGlassAPI.Data;
using IGlassAPI.Middleware;
using IGlassAPI.Queue;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MasterRepository>();

builder.Services.AddSingleton<LogsDbContextFactory>();

if (builder.Configuration["QueueProvider"] == "Azure")
    builder.Services.AddSingleton<IQueueProvider, AzureServiceBusQueueProvider>();
else
    builder.Services.AddSingleton<IQueueProvider, RabbitMqQueueProvider>();

builder.Services.AddDbContext<IGlassAPI.Data.ClientLogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));


var app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
IGlassAPI.Data.MigrationRunner.RunMigrations(app);
