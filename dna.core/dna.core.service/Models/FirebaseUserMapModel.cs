using dna.core.data.Infrastructure;
using dna.core.service.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models
{
    public class FirebaseUserMapModel : IModelBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public data.Infrastructure.OperatingSystem OperatingSystem { get; set; }

        public virtual UserModel User { get; set; }
    }
}
