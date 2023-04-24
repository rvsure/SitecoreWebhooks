namespace WebServices.Models
{
    public class VersionedField
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public int Version { get; set; }
        public string Language { get; set; }
    }
}
