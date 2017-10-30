using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Sender
{
    public class Response
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
    }
}
