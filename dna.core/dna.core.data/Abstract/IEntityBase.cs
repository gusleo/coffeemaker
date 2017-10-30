using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Abstract
{
    public interface IEntityBase
    {
        [Key]
        int Id { get; set; }
    }
}
