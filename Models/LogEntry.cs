using System;
using System.Collections.Generic;

namespace IGlassAPI.Models
{
 

    public class DynamicLog
    {
        public int Id { get; set; }
        public string RawData { get; set; }
        public DateTime Timestamp { get; set; }
          public Dictionary<string, string> Fields { get; set; } = new();
    }

    public class LogMasterDefinition
    {
        public string ClientId { get; set; }
        public string Schema { get; set; }
        public List<LogFieldDefinition> Fields { get; set; }
    }

    public class LogFieldDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }  
    }
}