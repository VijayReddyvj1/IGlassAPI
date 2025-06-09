using Microsoft.Extensions.Configuration;
using IGlassAPI.Models;
using System.Collections.Generic;

namespace IGlassAPI.Data
{
    public class MasterRepository
    {
        private readonly IConfiguration _config;

        public MasterRepository(IConfiguration config)
        {
            _config = config;
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
    }
}