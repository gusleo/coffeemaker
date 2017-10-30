using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.JsonModel
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string GuidName { get; set; }
        public bool IsCover { get; set; }
        public int Order { get; set; }
    }
}
