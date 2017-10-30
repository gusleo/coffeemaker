using dna.core.data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class ArticleTagMap : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
