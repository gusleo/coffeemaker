using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using dna.core.libs.Upload.Config;

namespace dna.core.libs.Upload.Server
{
    public class AzureServer : IServerType
    {
        private ServerConfig _config;
        public AzureServer(ServerConfig config)
        {
            _config = config;
        }
        public UploadMessage Delete(string containerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public MemoryStream Read(string containerName, string fileName, bool crop = false, int w = 0, int h = 0)
        {
            throw new NotImplementedException();
        }

        public UploadMessage UploadAsync(IFormFile file, string rootFolder)
        {
            throw new NotImplementedException();
        }
        public UploadMessage Delete(string path)
        {
            throw new NotImplementedException();
        }
    }
}
