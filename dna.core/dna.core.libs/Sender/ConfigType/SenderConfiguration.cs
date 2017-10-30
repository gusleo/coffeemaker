using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Sender.ConfigType
{
    public class SenderConfiguration
    {
        public SendGridConfig SendGrid { get; set; }
        public TwillioConfig Twillio { get; set; }
        public FCMConfig FirebaseMessaging { get; set; }

    }
}
