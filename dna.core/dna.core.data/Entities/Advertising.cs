using dna.core.data.Abstract;
using dna.core.data.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class Advertising : WriteHistoryBase, IEntityBase
    {
        public int Id { get; set; }
        public Nullable<int> ImageId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public AdvertisingType Type { get; set; }

        [Required]
        public Status Status { get; set; }

        [StringLength(100)]
        public string ButtonName { get; set; }
        public string ButtonActionUrl { get; set; }
        [StringLength(100)]
        public string ButtonSecondaryName { get; set; }
        public string ButtonSecondaryAction { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
    }
}
