using dna.core.libs.Sender.ConfigType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dna.core.libs.Sender
{
    public interface ISender
    {
        void SetConfig(SenderConfiguration senderConfiguration);
        Task<Response> SendAsync(string receiver, string subject, string message, object data = null);
        Task<Response> SendMultipleAsync(List<string> receivers, string subject, string message, object data = null);
       
    }
}
