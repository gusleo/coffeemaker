using dna.core.auth.Entity;
using dna.core.data.Abstract;
using dna.core.data.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dna.core.data.Entities
{
    public class FirebaseUserMap : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public Infrastructure.OperatingSystem OperatingSystem { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
