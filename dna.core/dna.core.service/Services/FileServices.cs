using dna.core.libs.Stream;
using dna.core.libs.Stream.Option;
using dna.core.libs.Upload.Abstract;
using dna.core.service.Infrastructure;
using dna.core.service.JsonModel;
using dna.core.service.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace dna.core.service.Services
{
    public class FileServices : IFileServices
    {
        private readonly IUploadService _upload;
        private readonly IHostingEnvironment _env;
       
        public FileServices(IUploadService upload, IHostingEnvironment env)
        {
            _upload = upload;
            _env = env;
        }
        public List<T> ImportData<T>(IFormFile file, StreamAdvanceOption option = null) where T : class, IStreamEntity, new()
        {
            if ( file != null )
            {
                Stream stream = file.OpenReadStream();

                var builder = new AdvanceStreamBuilder<T>(option);
                return builder.CreateReader(Path.GetExtension(file.FileName)).Read(stream);
            }
            else
            {
                throw new FieldAccessException("Failed accessed file");
            }

        }
        public Response<IList<FileModel>> Upload(IList<IFormFile> files)
        {
            var response = new Response<IList<FileModel>>(false, MessageConstant.Error);


            try
            {
                string rootFolder = _env.ContentRootPath;
                var photos = new List<FileModel>();
                int index = 0;
                foreach ( var file in files )
                {
                    var message = _upload.Upload(file, rootFolder);
                    photos.Add(new FileModel
                    {
                        Order = index,
                        FileName = message.FileName,
                        Path = message.FullPath,
                        IsCover = false

                    });
                    index++;

                }
                response.Item = photos;
                response.Message = MessageConstant.Create;
                response.Success = true;

            }
            catch ( Exception ex )
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<FileModel> Delete(string path)
        {
            var response = _upload.Delete(path);
            return new Response<FileModel>
            {
                Success = true,
                Message = MessageConstant.Delete
            };

        }
        public Response<FileModel> Upload(IFormFile file)
        {
            var response = new Response<FileModel>(false, MessageConstant.Error);
            try
            {
                string rootFolder = _env.ContentRootPath;
                var message = _upload.Upload(file, rootFolder);
                var photo = new FileModel
                {
                    Order = 0,
                    FileName = message.FileName,
                    Path = message.FullPath,
                    IsCover = false

                };
                response.Success = true;
                response.Message = MessageConstant.Create;
                response.Item = photo;
            }catch(Exception ex )
            {
                response.Message = ex.Message;
            }
            

            return response;
        }
    }
}
