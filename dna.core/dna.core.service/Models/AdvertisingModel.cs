using dna.core.data.Infrastructure;
using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class AdvertisingModel : WriteHistoryBaseModel, IModelBase
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

       
        public virtual ImageModel Image { get; set; }
    }
}
