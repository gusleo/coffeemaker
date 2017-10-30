using dna.core.libs.Stream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace dna.core.libs.test
{
    public class SampleEntity: IStreamEntity
    {
        public int Id { get; set; }
    }
    public class StreamTest
    {
        private AdvanceStreamBuilder<SampleEntity> _builder;
        private IStreamAdvanceReader<SampleEntity> _reader;
        public StreamTest()
        {
            _builder = new AdvanceStreamBuilder<SampleEntity>();
        }


        [Fact]
        public void ReadExcelFile_Return_List()
        {
            
            _reader = _builder.CreateReader(".xlsx");
            Assert.IsType<ExcelStreamReader<SampleEntity>>(_reader);

            string filePath = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            filePath = String.Format("{0}\\{1}\\{2}", filePath, "dna.core.libs.test", "TestFile.xlsx");
            FileStream file = new FileStream(filePath, 
                    FileMode.Open, FileAccess.Read);
            var result = _reader.Read(file);
            Assert.Equal(result.Count() > 0, true);

        }
        [Theory]
        [InlineData(".xxx")]
        public void CreateStreamReader_NotImplement(string extension)
        {            
            Assert.Throws<NotImplementedException>(() => _builder.CreateReader(extension));
        }
    }
}