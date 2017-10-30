using dna.core.data.Entities;
using dna.core.data.Infrastructure;
using dna.core.data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class TreeMenuRepository : EntityReadWriteBaseRepository<TreeMenu>, ITreeMenuRepository
    {
        public IDnaCoreContext DnaCoreContext
        {
            get { return _context as IDnaCoreContext; }
        }
        public TreeMenuRepository(IDnaCoreContext context) : base(context)
        {
        }

        
    }






    #region TreeMenuRepository
    public interface ITreeMenuRepository : IWriteBaseRepository<TreeMenu>, IReadBaseRepository<TreeMenu>
    {
       
    }
    #endregion
}
