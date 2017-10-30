using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Upload
{
    public interface IServerType
    {
        UploadMessage UploadAsync(IFormFile file, string rootFolder);
        MemoryStream Read(string containerName, string fileName, bool crop = false, int w = 0, int h = 0);
        UploadMessage Delete(string path);
    }
}
