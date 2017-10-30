using dna.core.data.Abstract;
using dna.core.data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class EntityWriteBaseRepository<T> : IDisposable, IWriteBaseRepository<T> where T : class, IEntityBase, new()
    {
        private bool disposed = false;
        protected readonly DnaCoreContext _context;
        public virtual void Add(T entity)
        {

            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            if(dbEntityEntry.State != EntityState.Detached )
            {
                dbEntityEntry.State = EntityState.Added;
            }else
            {
                _context.Set<T>().Add(entity);
            }
            
        }
        public virtual void AddRange(IEnumerable<T> entities) {
           
            _context.Set<T>().AddRange(entities);
        }

        public virtual void Edit(T entity)
        {
            /*EntityEntry dbEntityEntry = _context.Entry<T>(entity);  
            if(dbEntityEntry.State == EntityState.Detached )
            {
                _context.Set<T>().Attach(entity);
            }         
            dbEntityEntry.State = EntityState.Modified;*/
            _context.Updated<T>(entity);

        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            if(dbEntityEntry.State != EntityState.Deleted )
            {
                dbEntityEntry.State = EntityState.Deleted;
            }else
            {
                _context.Set<T>().Remove(entity);
            }
           
        }
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }


        protected virtual void Dispose(bool disposing)
        {
            if ( !this.disposed )
            {
                if ( disposing )
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
