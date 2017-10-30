using dna.core.data.Abstract;
using dna.core.data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dna.core.data.Repositories.Abstract
{
    public interface IReadBaseRepository<T> where T : class, IEntityBase, new()
    {
        
        /// <summary>
        /// Method to get all async of data with full join with other entities
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
               
        /// <summary>
        /// Method to get all of data async
        /// </summary>       
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Method to get all of data with paging
        /// </summary>
        /// <param name="pageIndex">int pageIndex</param>
        /// <param name="pageSize">int pageSize</param>
        /// <returns><see cref="PaginationEntity{T}"/> pagination</returns>
        Task<PaginationEntity<T>> GetAllAsync(int pageIndex, int pageSize);

        /// <summary>
        /// Method to get all of data with paging
        /// </summary>
        /// <param name="pageIndex">int pageIndex</param>
        /// <param name="pageSize">int pageSize</param>
        /// <returns><see cref="PaginationEntity{T}"/> pagination</returns>
        Task<PaginationEntity<T>> GetAllAsync(int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Method to get data by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(int id);
        /// <summary>
        /// Method to get data by predicate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Method to get data by predicate with full join
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// Method to get data async by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Method to get all data by predicate with full join
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// Method to get all data async by predicate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        Task<PaginationEntity<T>> FindByAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties);

        T GetSingle(Expression<Func<T, bool>> predicate);
    }
}
