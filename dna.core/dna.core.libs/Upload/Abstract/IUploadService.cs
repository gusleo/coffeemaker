using dna.core.libs.Upload.Config;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace dna.core.libs.Upload.Abstract
{
    public interface IUploadService
    {
        /// <summary>
        /// Upload file to server
        /// </summary>
        /// <param name="file">Form Input (file)</param>
        /// <param name="rootFolder">Root folder (for local server)</param>
        /// <returns></returns>
        UploadMessage Upload(IFormFile file, string rootFolder = "");
        UploadMessage Delete(string path);
    }
}
