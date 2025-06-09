using System;
using System.Collections.Generic;

namespace IGlassAPI.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string RawJson { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class LogFieldDefinition
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
    }

    public class LogMasterDefinition
    {
        public string ClientId { get; set; }
        public string Schema { get; set; }
        public List<LogFieldDefinition> Fields { get; set; }
    }
}