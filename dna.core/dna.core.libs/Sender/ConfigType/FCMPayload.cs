using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Sender.ConfigType
{

    public class FCMPayload
    {
       
        public string NotificationType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
