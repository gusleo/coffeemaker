using System.Collections.Generic;
using System.Threading.Tasks;
using dna.core.libs.Sender.ConfigType;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace dna.core.libs.Sender
{
    public class SendGridSender : ISender
    {
        private  SendGridConfig _option;
        public SendGridSender()
        {
        }

        public void SetConfig(SenderConfiguration senderConfiguration)
        {
            _option = senderConfiguration.SendGrid;
        }
        
        public async Task<Response> SendAsync(string receiver, string subject, string message, object data = null)
        {
            
            var response = new Response() { Successful = false };

            // Plug in your email service here to send an email.
            var myMessage = new SendGridMessage();
            myMessage.AddTo(receiver);
            myMessage.From = new EmailAddress(_option.SenderEmail, _option.SenderName);
            myMessage.Subject = subject;
            myMessage.PlainTextContent = message;
            myMessage.HtmlContent = message;

            var client = new SendGridClient(_option.ApiKey);
            var res = await client.SendEmailAsync(myMessage);
            // Send the email.
            if ( res != null )
            {
                response = new Response() { Successful = true, Message = "Success" };
            }
            else
            {
                response.Message = "Error";
            }
            return response;
        }
        public async Task<Response> SendMultipleAsync(List<string> receivers, string subject, string message, object data = null)
        {
            var response = new Response() { Successful = false };
           
            var myMessage = new SendGridMessage()
            {
                From = new EmailAddress(_option.SenderEmail),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            var client = new SendGridClient(_option.ApiKey);
            var res = await client.SendEmailAsync(myMessage);
            // Send the email.
            if ( res != null )
            {               
                response = new Response() { Successful = true, Message = "Success" };
            }
            else
            {
                response.Message = "Error";
            }
            return response;
        }
    }
       
}
