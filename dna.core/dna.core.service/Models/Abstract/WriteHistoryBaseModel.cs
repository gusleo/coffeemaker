using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models.Abstract
{
    public abstract class WriteHistoryBaseModel
    {
        [Required]
        public int CreatedById { get; set; }

        [Required]
        public int UpdatedById { get; set; }
        public virtual UserModel CreatedByUser { get; set; }


        public virtual UserModel UpdatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
