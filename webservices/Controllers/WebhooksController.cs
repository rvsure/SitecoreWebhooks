using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebServices.Models;
using WebServices.Services;

namespace WebServices.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class WebhooksController : ControllerBase
    {
        private readonly ILogger<WebhooksController> _logger;
        private readonly EmailService _emailService;
        private readonly SlackService _slackService;

        public WebhooksController(ILogger<WebhooksController> logger, EmailService emailService, SlackService slackService)
        {
            _logger = logger;
            _emailService = emailService;
            _slackService = slackService;
        }

        [HttpPost(Name = "ItemEvent")]
        public void LogItemEvent(ItemEvent itemEvent)
        {
            _logger.LogInformation("LogItemEvent request received.");
            if (itemEvent is null)
            {
                throw new ArgumentNullException(nameof(itemEvent));
            }

            _slackService.PosttoSlack(itemEvent);
            _emailService.SendEmail(itemEvent);
        }

        [HttpPost(Name = "Validate")]
        public JsonResult ValidateWokflowState(WebhookValidateRequest request)
        {
            _logger.LogInformation("ValidateWokflowState request received.");
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new JsonResult(GetValidationResponse());
        }

        [HttpPost(Name = "Submit")]
        public void WokflowSubmit(WorkflowSubmitRequest request)
        {
            _logger.LogInformation("WokflowSubmit request received.");
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _slackService.PosttoSlack(request);
            _emailService.SendEmail(request);
        }

        #region Private stuff, nothing to see here

        private WebhookValidateResponse GetValidationResponse()
        {
            return new WebhookValidateResponse()
            {
                IsValid = DateTime.Now.Minute % 2 == 0,
                Message = DateTime.Now.Minute % 2 == 0 ? "You have submitted on an even minute. You are good to go."
                : "You have submitted on an odd minute. Please try submitting again on an even minute."
            };
        }

        #endregion
    }
}