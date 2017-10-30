using dna.core.libs.Upload.Abstract;
using Microsoft.AspNetCore.Http;
using dna.core.libs.Upload.Config;
using Microsoft.Extensions.Options;
using System;

namespace dna.core.libs.Upload
{
    public class UploadService : IUploadService
    {
        private readonly ServerConfig _config;
        private IServerType _serverType;
        
        public UploadService(IOptions<ServerConfig> config)
        {
            _config = config.Value;

            _serverType = new ServerTypeFactory().Create(_config);      
        }

        public UploadMessage Delete(string path)
        {
            return _serverType.Delete(path);
        }

        public UploadMessage Upload(IFormFile file, string rootFolder = "")
        {
            return _serverType.UploadAsync(file, rootFolder);
        }
    }
}
