using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class ImageModel : WriteHistoryBaseModel, IModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "StringLength")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(4, ErrorMessage = "StringLength")]
        public string FileExtension { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool IsPrimary { get; set; }

        public string Description { get; set; }
    }
}
