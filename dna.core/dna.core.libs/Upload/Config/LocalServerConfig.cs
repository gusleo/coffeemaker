using dna.core.libs.Upload.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Upload.Config
{
    public class LocalServerConfig : IServerConfig
    {
        
        public string ContainerDefault { get; set; }

        public string Endpoint { get; set; }

        public string ServerType { get; set; }
    }
}
