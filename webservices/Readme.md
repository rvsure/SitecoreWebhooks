Before building the webservices project you need to perform the following steps.

# Configure environment variables.
The repository uses Mailjet email service to send email notifications. You need your public keys and secrets on Mailjet api developer portal (https://dev.mailjet.com/email/guides/) and set them as environment variables on your hosting environment. The following environment variables needs to be set up.
* **MJ_APIKEY_PUBLIC**: The Mailjet public api key
* **MJ_APIKEY_PRIVATE**: The Mailjet private secret

# Update the valies in Constants.cs file
Replace the values of the following constants in Constants.cs file with your own values.
* **SlackServiceEndpoint**: The Slack incoming webhooks endpoint. You can create one by following the Slack documentation https://api.slack.com/messaging/webhooks.
* **FromEmail**: The from email address to be appeared on your email notifications. The from email address needs to be configured as a valida sender on Mailjet portal before sending emails. Otherwise, the email sending will fail.
* **FromEmailAlias** : The from email alias to be appeared on your email notifications.
* **ToEmail**: The email address you want the email notifications to be sent to.
* **ContentEditorUrl**: Your Sitecore instance's content editor URL.
* **WorkboxUrl**: Your Sitecore instance's Workbox URL.