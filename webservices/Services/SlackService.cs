using Newtonsoft.Json;
using System.Text;
using WebServices.Models;

namespace WebServices.Services
{
    public class SlackService
    {
        private readonly ILogger<SlackService> _logger;
        public SlackService(ILogger<SlackService> logger) 
        {
            _logger = logger;
        }
        

        public void PosttoSlack(WorkflowSubmitRequest request)
        {
            _logger.LogInformation("Posting the workflow submit request info to slack");
            var slackPayload = ConvertToSlackPayload(request);
            SendSlackMessage(slackPayload);
        }

        public void PosttoSlack(ItemEvent itemEvent)
        {
            _logger.LogInformation("Posting the item event info to slack");
            var slackPayload = ConvertToSlackPayload(itemEvent);
            SendSlackMessage(slackPayload);
        }

        private SlackMessage ConvertToSlackPayload(WorkflowSubmitRequest request)
        {
            return new SlackMessage()
            {
                Username = "Sitecore Workflow",
                Text = $"*An item has been submitted for approval by {request.UserName}.*\n*Item Name:* {request.DataItem.Name}" +
                $"\n*Item ID:* {request.DataItem.Id}\n*Next Workflow State:* {request.NextState.DisplayName}\n" +
                $"*Comments:* {request.Comments.LastOrDefault()?.Value}\n" +
                $"Review and approve/reject the item on <{Constants.WorkboxUrl}|Sitecore Workbox>",
                Markdown = true
            };
        }

        

        private SlackMessage ConvertToSlackPayload(ItemEvent itemEvent)
        {
            return new SlackMessage()
            {
                Username = "Sitecore",
                Text = GetItemEventSlackMessage(itemEvent),
                Markdown = true
            };
        }

        private string GetItemEventSlackMessage(ItemEvent itemEvent)
        {
            switch (itemEvent.EventName)
            {
                case "item:added":
                    return $"*A new item has been added.*\n*Item Name:* {itemEvent.Item.Name}\n*Item ID:* {itemEvent.Item.Id}\nReview the item on <{Constants.ContentEditorUrl}|Sitecore Content Editor>";
                case "item:deleted":
                    return $"*An item has been deleted.*\n*Item Name:* {itemEvent.Item.Name}\n*Item ID:* {itemEvent.Item.Id}\nReview the item on <{Constants.ContentEditorUrl}|Sitecore Content Editor>";
                default: return $"*An item event has been triggered.*\n*Item Name:* {itemEvent.EventName}\n*Item ID: {itemEvent.Item.Id}\nReview the item on <{Constants.ContentEditorUrl}|Sitecore Content Editor>";
            }
        }

        
        private void SendSlackMessage(SlackMessage message)
        {
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(message);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = client.PostAsync(Constants.SlackServiceEndpoint, httpContent);
            _logger.LogInformation($"Posted Slack message with response:{Environment.NewLine} {JsonConvert.SerializeObject(httpResponse.Result)}");
        }
    }
}
