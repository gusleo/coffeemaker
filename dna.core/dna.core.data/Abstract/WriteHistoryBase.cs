using dna.core.auth.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Abstract
{
    public abstract class WriteHistoryBase
    {
        [Required]
        public int CreatedById { get; set; }

        [Required]
        public int UpdatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        [ForeignKey("UpdatedById")]
        public virtual ApplicationUser UpdatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
