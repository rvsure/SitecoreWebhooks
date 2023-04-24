namespace WebServices.Models
{
    public class DataItem
    {
        public string Language { get; set; }
        public int Version { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string TemplateId { get; set; }
        public string MasterId { get; set; }
        public List<SharedField> SharedFields { get; set; }
        public List<UnversionedField> UnversionedFields { get; set; }
        public List<VersionedField> VersionedFields { get; set; }
    }
}
