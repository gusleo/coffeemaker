using dna.core.service.Infrastructure;
using dna.core.service.Models.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dna.core.service.Services.Abstract
{
    public interface IReadService<TModel> : IDisposable 
        where TModel : IModelBase, new()
    {
        /// <summary>
        /// Get single model by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>boolean</returns>
        Task<Response<TModel>> GetSingleAsync(int id);
        /// <summary>
        /// Get all list
        /// </summary>
        /// <returns></returns>
        Task<Response<PaginationSet<TModel>>> GetAllAsync(int pageIndex, int pageSize = Constant.PageSize);
        

    }
}
