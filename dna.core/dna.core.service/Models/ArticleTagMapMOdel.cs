using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class ArticleTagMapModel : IModelBase
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int TagId { get; set; }

        public virtual TagModel Tag { get; set; }

        public virtual ArticleModel Article { get; set; }
    }
}
