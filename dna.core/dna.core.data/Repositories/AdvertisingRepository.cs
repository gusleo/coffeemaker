using dna.core.data.Entities;
using dna.core.data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class AdvertisingRepository : EntityReadWriteBaseRepository<Advertising>, IAdvertisingRepository
    {
        public AdvertisingRepository(IDnaCoreContext context) : base(context)
        {

        }
    }
    public interface IAdvertisingRepository : IWriteBaseRepository<Advertising>, IReadBaseRepository<Advertising>
    {

    }
}
