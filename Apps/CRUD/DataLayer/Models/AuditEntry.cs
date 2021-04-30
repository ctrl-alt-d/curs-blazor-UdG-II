namespace Datalayer
{

    public class AuditEntry
    {
        public int AuditEntryId { get; set; } 
        public string Username { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }

}
