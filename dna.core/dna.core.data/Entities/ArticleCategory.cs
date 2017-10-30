using dna.core.data.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dna.core.data.Entities
{
    [Table("ArticleCategory")]
    public class ArticleCategory : IEntityBase
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Slug { get; set; }

        public bool IsVisible { get; set; }
    }
}
