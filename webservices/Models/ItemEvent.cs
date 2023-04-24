namespace WebServices.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Item
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

    public class ItemEvent
    {
        public string EventName { get; set; }
        public Item Item { get; set; }
        public string WebhookItemId { get; set; }
        public string WebhookItemName { get; set; }
    }
}