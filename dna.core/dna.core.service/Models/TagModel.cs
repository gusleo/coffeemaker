using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class TagModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string TagName { get; set; }

        public virtual IEnumerable<ArticleTagMapModel> TagMaps { get; set; }
    }
}
