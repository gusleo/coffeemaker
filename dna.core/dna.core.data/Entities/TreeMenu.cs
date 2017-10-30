using dna.core.data.Abstract;
using dna.core.data.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class TreeMenu : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ParentId { get; set; }
        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }
       
        [StringLength(100)]
        public string Link { get; set; }
        [StringLength(100)]
        public string Roles { get; set; }
        [StringLength(100)]
        public string DisplayIcon { get; set; }
        [Required]
        public int Order { get; set; }

        public MenuType Type { get; set; }
    }
}
