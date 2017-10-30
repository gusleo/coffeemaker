using dna.core.data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Entities
{
    public class ErrorLog : IEntityBase
    {
        public int Id { get; set; }
        public string LoggedUser { get; set; }
        [Required]
        public string UrlPath { get; set; }
        [Required]
        public string Controller { get; set; }
        [Required]
        public string Method { get; set; }
        public string Input { get; set; }
        [Required]
        public string ErrorMessage { get; set; }
        [Required]
        public DateTime LogDate { get; set; }
    }
}
