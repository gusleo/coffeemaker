using dna.core.data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using dna.core.data.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using dna.core.data.Infrastructure;

namespace dna.core.data.Repositories
{
    public class EntityReadWriteBaseRepository<T> : EntityReadBaseRepository<T>, IDisposable, IReadBaseRepository<T>, IWriteBaseRepository<T> where T : class, IEntityBase, new()
    {
        
        public EntityReadWriteBaseRepository(IDnaCoreContext context) : base(context)
        {
                       
        }        
        public virtual void Add(T entity)
        {
            
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            if ( dbEntityEntry.State != EntityState.Detached )
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                _context.Set<T>().Add(entity);
            }

        }
        public virtual void AddRange(IEnumerable<T> entities)
        {

            _context.Set<T>().AddRange(entities);
        }

        public virtual void Edit(T entity)
        {
            /*EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            if ( dbEntityEntry.State == EntityState.Detached )
            {
                _context.Set<T>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;*/
            _context.Updated<T>(entity);

        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            if ( dbEntityEntry.State != EntityState.Deleted )
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }

        }
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

    }
}
