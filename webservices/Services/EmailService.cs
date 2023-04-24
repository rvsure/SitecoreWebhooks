using Mailjet.Client.TransactionalEmails;
using Mailjet.Client;
using Newtonsoft.Json;
using WebServices.Models;
using Mailjet.Client.Resources;

namespace WebServices.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public void SendEmail(WorkflowSubmitRequest workflowRequest)
        {
            _logger.LogInformation("Emailing the workflow submit request info");
            SendEmail($"An item has been submitted for approval by {workflowRequest.UserName}",
                $"The item details are<br/> <strong>Item ID:</strong> {workflowRequest.DataItem.Id}<br/><strong>Item Name:</strong> {workflowRequest.DataItem.Name}<br/>" +
                        $"<strong>NextState Workflow State</strong> {workflowRequest.NextState.DisplayName}<br/>" +
                        $"<strong>Comments:</strong> {workflowRequest.Comments.LastOrDefault()?.Value}<br/>" +
                        $"Review and approve/reject the item on <a href=\"{Constants.WorkboxUrl}\">Sitecore Workbox</a>");
        }

        public void SendEmail(ItemEvent itemEvent)
        {
            _logger.LogInformation("Emailing the item event info");
            SendEmail($"Sitecore item event type {itemEvent.EventName} has been triggered"
                , $"The item details are<br/> <strong>Item ID:</strong> {itemEvent.Item.Id}<br/><strong>Item Name:</strong> {itemEvent.Item.Name}<br/>" +
                $"Review item on <ahref=\"{Constants.ContentEditorUrl}\">Sitecore Content Editor</a>");
        }

        private void SendEmail(string subject, string body)
        {
            MailjetClient client = new MailjetClient(Constants.MailjetAPIKey, Constants.MailjetAPISecret);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            // construct your email with builder
            var email = new TransactionalEmailBuilder()
                   .WithFrom(new SendContact(Constants.FromEmail, Constants.FromEmailAlias))
                   .WithSubject(subject)
                   .WithHtmlPart(body)
                   .WithTo(new List<SendContact>() { new SendContact(Constants.ToEmail) })
                   .Build();
            try
            {
                // invoke API to send email
                var response = client.SendTransactionalEmailAsync(email);
                _logger.LogInformation($"Email api response: {Environment.NewLine}{JsonConvert.SerializeObject(response.Result)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while sending email");
            }
        }
    }
}
