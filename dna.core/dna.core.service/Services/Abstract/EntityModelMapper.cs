using AutoMapper;
using dna.core.data.Abstract;
using dna.core.data.Infrastructure;
using dna.core.service.Infrastructure;
using dna.core.service.Models.Abstract;
using System.Collections.Generic;

namespace dna.core.service.Services.Abstract
{
    public class EntityModelMapper<TModel, TEntity>
        where TModel : IModelBase, new()
        where TEntity : IEntityBase, new()
    {
        /// <summary>
        /// Convert entity to model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected TModel GetModelFromEntity(TEntity entity)
        {
            return Mapper.Map<TModel>(entity);
        }

        /// <summary>
        /// Convert list entities to list model
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected IList<TModel> GetModelFromEntity(IList<TEntity> entities)
        {
            return Mapper.Map<IList<TModel>>(entities);
        }

        protected IList<TM> GetModelFromEntity<T, TM>(IList<T> entities) 
            where TM : IModelBase, new()
            where T : IEntityBase, new()
        {
            return Mapper.Map<IList<TM>>(entities);
        }
        protected PaginationSet<TM> GetModelFromEntity<T, TM>(PaginationEntity<T> entities)
            where TM : IModelBase, new()
            where T : IEntityBase, new()
        {
            return Mapper.Map<PaginationSet<TM>>(entities);
        }

        protected TM GetModelFromEntity<T, TM>(T entity)
            where TM : IModelBase, new()
            where T : IEntityBase, new()
        {
            return Mapper.Map<TM>(entity);
        }


        protected PaginationSet<TModel> GetModelFromEntity(PaginationEntity<TEntity> entities)
        {
            return Mapper.Map<PaginationSet<TModel>>(entities);
        }

        /// <summary>
        /// Convert model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected TEntity GetEntityFromModel(TModel model)
        {
            return Mapper.Map<TEntity>(model);
        }
        protected T GetEntityFromModel<TM, T>(TM model)
            where TM : IModelBase, new()
            where T : IEntityBase, new()
        {
            return Mapper.Map<T>(model);
        }

        /// <summary>
        /// Convert list model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected IList<TEntity> GetEntityFromModel(IList<TModel> model)
        {
            return Mapper.Map<IList<TEntity>>(model);
        }

        /// <summary>
        /// Get list of model from list entities
        /// </summary>
        /// <typeparam name="T">destination</typeparam>
        /// <typeparam name="TM">source</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        protected IList<T> GetEntityFromModel<TM, T>(IList<TM> model)
            where TM : IModelBase, new()
            where T : IEntityBase, new()
        {
            return Mapper.Map<IList<T>>(model);
        }

        /// <summary>
        /// Convert list model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected PaginationEntity<TEntity> GetEntityFromModel(PaginationSet<TModel> model)
        {
            return Mapper.Map<PaginationEntity<TEntity>>(model);
        }

        /// <summary>
        /// Convert model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected TEntity GetEntityFromModel(TModel model, TEntity entity)
        {
            return Mapper.Map<TModel, TEntity>(model, entity);
        }

       
    }
}
