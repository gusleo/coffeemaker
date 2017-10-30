using dna.core.data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class Image : WriteHistoryBase, IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(100)]
        public string FileName { get; set; }
        [Required]
        [MaxLength(4)]
        public string FileExtension { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
        public string Description { get; set; }
    }
}
