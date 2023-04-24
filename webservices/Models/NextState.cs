namespace WebServices.Models
{
    public class NextState
    {
        public string DisplayName { get; set; }
        public bool FinalState { get; set; }
        public string Icon { get; set; }
        public string StateID { get; set; }
        public List<object> PreviewPublishingTargets { get; set; }
    }
}
