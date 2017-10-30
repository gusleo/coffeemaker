using dna.core.auth.Entity;
using dna.core.data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class UserDetail : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}
