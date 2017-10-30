using dna.core.libs.Upload;
using dna.core.libs.Upload.Config;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.IO;
using Xunit;

namespace dna.core.libs.test.Upload
{

    public class UploadServiceTest
    {      
        

        [Fact]
        public void Upload_LocalServer_Success()
        {
            var opt = LocalServerOption();
            var file = new Helper().CreateMockFormFile();
            string path = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            path = String.Format("{0}\\{1}", path, "dna.core.libs.test");
            var service = new UploadService(opt);
            UploadMessage msg = service.Upload(file, path);

            Assert.Equal(UploadMessage.SUCCESS, msg.Message);           

        }

        private IOptions<ServerConfig> LocalServerOption()
        {
            ServerConfig config = new ServerConfig()
            {
                ServerType = "Local",
                ContainerDefault = "Content/Upload"
            };
            var mock = new Mock<IOptions<ServerConfig>>();
            mock.Setup(ap => ap.Value).Returns(config);
            return mock.Object;
        }
        
    }
}
