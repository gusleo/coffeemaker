using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class UserDetailModel : IModelBase
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

       
        public virtual UserModel User { get; set; }
    }
}
