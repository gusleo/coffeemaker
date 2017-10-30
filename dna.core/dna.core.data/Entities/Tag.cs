using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string TagName { get; set; }

        public virtual ICollection<ArticleTagMap> TagMaps { get; set; }
    }
}
