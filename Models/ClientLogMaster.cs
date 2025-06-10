namespace IGlassAPI.Models
{
    public class ClientLogMaster
    {
        public string ClientId { get; set; }
        public string SchemaName { get; set; }
        public string LogTableStructure { get; set; }  // JSON string
    }
}
