using System;
using System.Collections.Generic;
using dna.core.libs.Stream.Option;
using OfficeOpenXml;
using dna.core.libs.Stream.Extension;
using System.IO;

namespace dna.core.libs.Stream
{
    public class ExcelStreamReader<T> : IStreamAdvanceReader<T> where T : class, IStreamEntity, new ()
    {
        private StreamAdvanceOption options;
        private string type;
        public ExcelStreamReader(string type, StreamAdvanceOption options)
        {
            this.type = type;
            this.options = options;
        }
        
        public List<T> Read(System.IO.Stream file)
        {
            return Read(file, 1);         
        }
        public List<T> Read(System.IO.Stream file, int worksheetIndex)
        {
            ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets[worksheetIndex];
            return workSheet.ToList<T>(options.FirstRowAsColumnName);
        }




    }
}
