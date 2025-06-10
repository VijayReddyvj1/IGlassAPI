using IGlassAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGlassAPI.Data
{
    public class MasterRepository
    {
        private readonly IConfiguration _config;
        private readonly MasterDbContext _context;


        public MasterRepository(IConfiguration config, MasterDbContext context)
        {
            _config = config;
            _context = context;

        }

        public LogMasterDefinition GetClientDefinition(string clientId)
        {
            var section = _config.GetSection($"Clients:{clientId}");
            var schema = section["Schema"];
            var fields = section.GetSection("Fields").Get<List<LogFieldDefinition>>();
            return new LogMasterDefinition
            {
                ClientId = clientId,
                Schema = schema,
                Fields = fields
            };
        }
     public async Task<LogMasterDefinition> GetClientDefinitionAsync(string clientId)
        {
            var clientMaster = await _context.ClientLogMasters
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (clientMaster == null) return null;

            var fields = JsonSerializer.Deserialize<List<LogFieldDefinition>>(clientMaster.LogTableStructure);

            return new LogMasterDefinition
            {
                ClientId = clientId,
                Schema = clientMaster.SchemaName,
                Fields = fields
            };
        }
    }

}