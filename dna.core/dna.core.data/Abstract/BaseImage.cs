using dna.core.auth.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Abstract
{
    public abstract class BaseImage : WriteHistoryBase, IEntityBase
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string ImagePath { get; set; }
        [Required, MaxLength(100)]
        public string ImageName { get; set; }
        
    }
}
