using dna.core.data.Entities;
using dna.core.data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class UserDetailRepository : EntityReadWriteBaseRepository<UserDetail>, IUserDetailRepository
    {
        public IDnaCoreContext IDnaCoreContext
        {
            get { return _context as IDnaCoreContext; }
        }
        public UserDetailRepository(IDnaCoreContext context) : base(context)
        {
        }
    }

    public interface IUserDetailRepository : IWriteBaseRepository<UserDetail>, IReadBaseRepository<UserDetail>
    {

    }
}
