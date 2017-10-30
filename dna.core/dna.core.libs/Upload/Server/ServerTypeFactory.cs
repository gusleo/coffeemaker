using dna.core.libs.Upload.Abstract;
using dna.core.libs.Upload.Config;
using dna.core.libs.Upload.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Upload
{
    public class ServerTypeFactory
    {
        const string AZURE = "Azure";
        const string LOCAL = "Local";
        public IServerType Create(ServerConfig config)
        {
            IServerType server;
            switch ( config.ServerType )
            {
                case AZURE:
                    server = new AzureServer(config);
                    break;
                default:
                    server = new LocalServer(config);
                    break;
            }
            return server;
        }
    }
}
