using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Sender.ConfigType
{
    public class SendGridConfig
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
