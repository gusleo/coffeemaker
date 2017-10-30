using dna.core.data.Entities;
using dna.core.data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class FirebaseUserMapRepository : EntityReadWriteBaseRepository<FirebaseUserMap>, IFirebaseUserMapRepository
    {
        public IDnaCoreContext DnaCoreContext
        {
            get { return _context as IDnaCoreContext; }
        }
        public FirebaseUserMapRepository(IDnaCoreContext context) : base(context)
        {
        }
    }

    public interface IFirebaseUserMapRepository : IWriteBaseRepository<FirebaseUserMap>, IReadBaseRepository<FirebaseUserMap>
    {
    }
}
