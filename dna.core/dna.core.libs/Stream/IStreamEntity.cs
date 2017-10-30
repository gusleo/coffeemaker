using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Stream
{
    public interface IStreamEntity
    {
        [NotMapped]
        int Id { get; set; }
    }
}
