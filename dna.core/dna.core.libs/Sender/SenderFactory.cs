using dna.core.libs.Sender.ConfigType;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dna.core.libs.Sender
{
    public class SenderFactory : ISenderFactory
    {
        public const string SMS = "sms";
        public const string EMAIL = "email";
        public const string FCM = "fcm";

        private static Dictionary<string, ISender> _map;
        private readonly SenderConfiguration _options;

        public SenderFactory(IOptions<SenderConfiguration> options)
        {
            _options = options.Value;

            _map = new Dictionary<string, ISender>()
            {
                {"email", new SendGridSender() },
                {"sms", new TwillioSender() },
                {"fcm", new FCMSender() }
            };
        }

        public ISender Create(string type)
        {
            ISender _sender = _map.Where(x => x.Key.Equals(type)).FirstOrDefault().Value;
            if ( _sender != null )
            {
                _sender.SetConfig(_options);
                return _sender;
            }
            else
                throw new NotImplementedException();
            
        }

        public void RegisterSender<T1>(string type) where T1: class, ISender, new()
        {
            if(_map.ContainsKey(type) == false )
            {
                _map.Add(type, (ISender)Activator.CreateInstance(typeof(T1)));
            }
        }        
    }


    public interface ISenderFactory
    {
        ISender Create(string type);
        void RegisterSender<T1>(string type) where T1 : class, ISender, new();
    }
}
