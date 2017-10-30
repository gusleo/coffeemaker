using dna.core.data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using dna.core.auth.Entity;
using System.ComponentModel.DataAnnotations;
using dna.core.data.Infrastructure;

namespace dna.core.data.Entities
{
    

    [Table("Article")]
    public class Article : WriteHistoryBase, IEntityBase
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
       

        [ForeignKey("AcceptedById")]
        public virtual ApplicationUser AcceptedByUser { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ArticleCategory Category { get; set; }

        public virtual ICollection<ArticleTagMap> TagMaps { get; set; }
        public virtual ICollection<ArticleImage> ImageMaps { get; set; }
    }
}
