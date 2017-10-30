using dna.core.data.Abstract;
using dna.core.libs.Validation;
using dna.core.service.Infrastructure;
using dna.core.service.Models.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dna.core.service.Services.Abstract
{
    public interface IReadWriteService<TModel, TEntity> : IReadService<TModel>
        where TModel : IModelBase, new()
        where TEntity : IEntityBase, new()
    {
        /// <summary>
        /// Initiliaze Model State
        /// </summary>
        /// <param name="modelState"></param>
        void Initialize(IValidationDictionary modelState);

        /// <summary>
        /// Create model to database
        /// </summary>
        /// <param name="modelToCreate">T variable</param>
        /// <returns></returns>
        Response<TModel> Create(TModel modelToCreate);
        
        /// <summary>
        /// Edit model to database
        /// </summary>
        /// <param name="modelToEdit">T variable</param>
        /// <returns></returns>
        Response<TModel> Edit(TModel modelToEdit);
        /// <summary>
        /// Delete model to database
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns></returns>
        Task<Response<TModel>> Delete(int id);        

        /// <summary>
        /// Validate bussiness logic of model
        /// </summary>
        /// <remarks>
        /// This validation not to replace ModelState validation
        /// but adding validation which is not handle by DataAnnotation
        /// </remarks>
        /// <param name="modelToValidate"></param>
        /// <returns></returns>
        bool Validate(TModel modelToValidate);

        /// <summary>
        /// Prevent child insertion
        /// with remove the child manualy
        /// </summary>
        /// <param name="entity"></param>
        TEntity RemoveChildEntity(TEntity entity);

    }
}
