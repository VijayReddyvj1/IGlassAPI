using IGlassAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGlassAPI.Data
{
    public class DynamicLogService
    {
        private readonly DbContextOptions<ClientLogDbContext> _baseOptions;

        public DynamicLogService(DbContextOptions<ClientLogDbContext> baseOptions)
        {
            _baseOptions = baseOptions;
        }

        public async Task SaveLogAsync(LogMasterDefinition config, string json)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientLogDbContext>(_baseOptions);
            using var db = new ClientLogDbContext(optionsBuilder.Options, config.Schema);

            var dynamicData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var log = new DynamicLog
            {
                RawData = json,
                Timestamp = DateTime.UtcNow,
                Fields = new Dictionary<string, string>()
            };

            foreach (var field in config.Fields)
            {
                log.Fields[field.Name] = dynamicData.TryGetValue(field.Name, out var value) ? value?.ToString() : null;
            }

            db.Logs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}


