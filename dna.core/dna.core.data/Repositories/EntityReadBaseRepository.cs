using dna.core.data.Abstract;
using dna.core.data.Infrastructure;
using dna.core.data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class EntityReadBaseRepository<T> : IDisposable, IReadBaseRepository<T> where T : class, IEntityBase, new()
    {
        private bool disposed = false;
        //public DnaCoreContext _context;
        protected readonly IDnaCoreContext _context;


        public EntityReadBaseRepository(IDnaCoreContext context)
        {
            _context = context;
        }        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual async Task<PaginationEntity<T>> GetAllAsync(int pageIndex, int pageSize)
        {
            IQueryable<T> query = _context.Set<T>();
            query = query.OrderByDescending(x => x.Id);
            return new PaginationEntity<T>()
            {
                Page = pageIndex,
                PageSize = pageSize,
                TotalCount = await query.CountAsync(),
                Items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
        public virtual async Task<PaginationEntity<T>> GetAllAsync(int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach ( var includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }

            query = query.OrderByDescending(x => x.Id);
            return new PaginationEntity<T>()
            {
                Page = pageIndex,
                PageSize = pageSize,
                TotalCount = await query.CountAsync(),
                Items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
        
        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach ( var includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }
            query = query.OrderByDescending(x => x.Id);
            return await query.ToListAsync();
        }
       
        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach ( var includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).OrderByDescending(x => x.Id).ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach ( var includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }
            query = query.OrderByDescending(x => x.Id);
            return await query.Where(predicate).ToListAsync();
        }
        /*public virtual IQueryable<T> GenereateQuery(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach ( var includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate);
        }*/

       
        public virtual async Task<PaginationEntity<T>> FindByAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach ( var includeProperty in includeProperties )
            {
                query = query.Include(includeProperty);
            }

            //set predicate 
            query = query.Where(predicate);
            query = query.OrderByDescending(x => x.Id);
            return new PaginationEntity<T>()
            {
                Page = pageIndex,
                PageSize = pageSize,
                TotalCount = await query.CountAsync(),
                Items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync()
            };
           
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
