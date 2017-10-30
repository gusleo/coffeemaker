using dna.core.data.Entities;
using dna.core.data.Infrastructure;
using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dna.core.service.Models
{

    public class ArticleModel : WriteHistoryBaseModel, IModelBase
    {
        public int Id { get; set; }
        [Required, MaxLength(100), MinLength(3)]
        public string Slug { get; set; }

        [Required, MaxLength(100), MinLength(3)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(255)]
        public string ShortDescription { get; set; }

        [MaxLength(255)]
        public string Metadata { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<int> AcceptedById { get; set; }

        public DateTime? AcceptedDate { get; set; }
        public ArticleStatus Status { get; set; }
        
        public virtual UserModel AcceptedByUser { get; set; }
        
        public virtual ArticleCategoryModel Category { get; set; }

        public virtual IEnumerable<ArticleTagMapModel> TagMaps { get; set; }
        public virtual IEnumerable<ArticleImageModel> ImageMaps { get; set; }
    }
}
