using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class ArticleImageModel : IModelBase
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int ImageId { get; set; }

     
        public virtual ArticleModel Article { get; set; }
        public virtual ImageModel  Image { get; set; }
    }
}
