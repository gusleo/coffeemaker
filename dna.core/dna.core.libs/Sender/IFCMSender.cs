using dna.core.libs.Sender.ConfigType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Sender
{
    public interface IFCMSender : ISender
    {
        Task<Response> SendTopicAsync(string topic, string title, string message, object data = null);
        Task<Response> SendTopicAsync(List<string> topics, string title, string message, object data = null);        
    }
}
