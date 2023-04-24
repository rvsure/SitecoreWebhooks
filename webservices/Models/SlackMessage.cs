using Newtonsoft.Json;

namespace WebServices.Models
{
    public class SlackMessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("mrkdwn")]
        public bool Markdown { get; set; }
    }
}
