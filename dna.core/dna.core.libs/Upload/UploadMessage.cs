using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Upload
{
    public class UploadMessage
    {
        public const string ERROR = "Error upload image";
        public const string SUCCESS = "Success upload image";
        public const string DELETE = "Success delete image";

        public string FileName { get; set; }
        public string GuidFileName { get; set; }
        public string FullPath { get; set; }
        public string Message { get; set; }
    }
}
