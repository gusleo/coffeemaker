using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.test
{
    public class Helper
    {
        public IFormFile CreateMockFormFile()
        {
            //Arrange
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock.Object;
        }
    }
}
