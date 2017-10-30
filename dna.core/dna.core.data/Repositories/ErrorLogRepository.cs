using dna.core.data.Entities;
using dna.core.data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class ErrorLogRepository : EntityReadWriteBaseRepository<ErrorLog>, IErrorLogRepository
    {
        public IDnaCoreContext DnaCoreContext
        {
            get { return _context as IDnaCoreContext; }
        }
        public ErrorLogRepository(IDnaCoreContext context) : base(context)
        {
        }
    }

    public interface IErrorLogRepository : IWriteBaseRepository<ErrorLog>, IReadBaseRepository<ErrorLog> { }
}
