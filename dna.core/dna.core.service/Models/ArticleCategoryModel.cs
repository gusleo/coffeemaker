using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class ArticleCategoryModel : IModelBase
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Slug { get; set; }

        public bool IsVisible { get; set; }
        public int ArticleCount { get; set; }
    }
}
