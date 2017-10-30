using dna.core.data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class ArticleImage : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int ImageId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

    }
}
