using dna.core.libs.Stream.Option;
using System.Collections.Generic;
using System.IO;

namespace dna.core.libs.Stream
{
    public interface IStreamAdvanceReader<T> where T : class, IStreamEntity, new()
    {       
        List<T> Read(System.IO.Stream file);
        List<T> Read(System.IO.Stream file, int worksheetIndex);
    }

    public interface IStreamReader<T>
    {      
        T Read(System.IO.Stream file);
    }
}
