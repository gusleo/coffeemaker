using dna.core.data.Infrastructure;
using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class TreeMenuModel : IModelBase
    {
        public int Id { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        
        [MaxLength(100)]
        public string Link { get; set; }

        [MaxLength(100)]
        public string DisplayIcon { get; set; }

        [MaxLength(100)]
        public string Roles { get; set; }       
        

        public MenuType Type { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
