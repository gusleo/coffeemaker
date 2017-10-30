using dna.core.data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories.Abstract
{
    public interface IWriteBaseRepository<T> where T : class, IEntityBase, new()
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entitites);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entitites);
        void Edit(T entity);       
    }
}
