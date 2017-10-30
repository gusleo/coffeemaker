using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Stream.Option
{
    public class StreamAdvanceOption : StreamOption
    {
        public bool FirstRowAsColumnName { get; set; }
        public StreamAdvanceOption() { }
        public StreamAdvanceOption(bool firstRowAsColumnName)
        {
            FirstRowAsColumnName = firstRowAsColumnName;
        }
        
    }
}
