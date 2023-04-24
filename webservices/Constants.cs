namespace WebServices
{
    public static class Constants
    {
        public const string SlackServiceEndpoint = "{Your Slack incoming webhook URL}";
        public const string FromEmail = "{Your from email address}";
        public const string ToEmail = "{Your to email address}";
        public const string FromEmailAlias = "{Your from email alias}";
        public const string ContentEditorUrl = "{YOur Sitecore instance content editor URL}";
        public const string WorkboxUrl = "{YOur Sitecore instance workbox URL}";
        public static readonly string MailjetAPIKey = Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC")?? "Key not set";
        public static readonly string MailjetAPISecret = Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE") ?? "Secret not set";
    }
}
