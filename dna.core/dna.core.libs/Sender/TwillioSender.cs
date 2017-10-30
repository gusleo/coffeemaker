using dna.core.libs.Sender.ConfigType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace dna.core.libs.Sender
{
    public class TwillioSender : ISender
    {
        private TwillioConfig _option;
        public TwillioSender()
        {
        }
        public void SetConfig(SenderConfiguration senderConfiguration)
        {
            _option = senderConfiguration.Twillio;
        }

        public async Task<Response> SendAsync(string receiver, string subject, string message, object data = null)
        {

            var response = new Response() { Successful = false };
            try
            {
                TwilioClient.Init(_option.AccountSID, _option.Token);
                var res = await MessageResource.CreateAsync(
                    to: new PhoneNumber(receiver),
                    from: new PhoneNumber(_option.SendNumber),
                    body: message);
                response = new Response() { Successful = true, Message = res.Sid };
            }
            catch(Exception ex )
            {
                response.Message = ex.Message;
                response.Error = ex;
            }

            return response;
        }

        public Task<Response> SendMultipleAsync(List<string> receivers, string subject, string message, object data = null)
        {
            throw new NotImplementedException();
        }

        
    }
}
