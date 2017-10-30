using dna.core.libs.Stream;
using dna.core.libs.Stream.Option;
using dna.core.service.Infrastructure;
using dna.core.service.JsonModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace dna.core.service.Services
{
    public interface IFileServices
    {
        List<T> ImportData<T>(IFormFile file, StreamAdvanceOption option = null) where T : class, IStreamEntity, new();
        Response<FileModel> Upload(IFormFile file);
        Response<IList<FileModel>> Upload(IList<IFormFile> files);
        Response<FileModel> Delete(string path);
    }
}
