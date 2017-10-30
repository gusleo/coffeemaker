using dna.core.data.Entities;
using dna.core.service.Models;
using dna.core.service.Services;
using dna.core.service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dna.core.service.Infrastructure;
using dna.core.data.UnitOfWork;
using AutoMapper;
using dna.core.auth;

namespace dna.core.service.Services
{
    public class ArticleCategoryService : ReadWriteServiceBase<ArticleCategoryModel, ArticleCategory>, IArticleCategoryService
    {
        private readonly IDNAUnitOfWork _unitOfWork;

        public ArticleCategoryService(IAuthenticationService authService, IDNAUnitOfWork unitOfWork) : base (authService)
        {
            _unitOfWork = unitOfWork;
        }
        public Response<ArticleCategoryModel> Create(ArticleCategoryModel modelToCreate)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToCreate) )
            {
                try
                {
                    var en = GetEntityFromModel(modelToCreate);
                    _unitOfWork.ArticleCategoryRepository.Add(en);
                    _unitOfWork.Commit();
                    response = InitSuccessResponse(MessageConstant.Create);
                    response.Item = GetModelFromEntity(en);
                }
                catch(Exception ex)
                {
                    response.Message = ex.Message;
                }
                
            }else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
        }

        public async Task<Response<ArticleCategoryModel>> Delete(int id)
        {
            var response = InitErrorResponse();
            try
            {
                var en = await _unitOfWork.ArticleCategoryRepository.GetSingleAsync(id);
                _unitOfWork.ArticleCategoryRepository.Delete(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Delete);
            }catch(Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public Response<ArticleCategoryModel> Edit(ArticleCategoryModel modelToEdit)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToEdit) )
            {
                                  
                var en = GetEntityFromModel(modelToEdit);
                _unitOfWork.ArticleCategoryRepository.Edit(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Update);
                response.Item = GetModelFromEntity(en);
                
                
            }
            else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
        }

        public async Task<Response<PaginationSet<ArticleCategoryModel>>> GetAllAsync(int pageIndex, int pageSize = 20)
        {
            var response = InitErrorResponse(pageIndex, pageSize);
            try
            {
                var categories = await _unitOfWork.ArticleCategoryRepository.GetAllAsync(pageIndex, pageSize, true);
                response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
                response.Item = Mapper.Map<PaginationSet<ArticleCategoryModel>>(categories);
            }catch(Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<ArticleCategoryModel>> GetArticleCountAsync(int id)
        {
            var response = InitErrorResponse();
            try
            {
                var en = await _unitOfWork.ArticleCategoryRepository.GetSingleAsync(id);
                int count = _unitOfWork.ArticleCategoryRepository.GetArticleCount(id);
                var model = GetModelFromEntity(en);
                model.ArticleCount = count;
                response = InitSuccessResponse(MessageConstant.Load);
                response.Item = model;
            }catch(Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
            

        }

        public async Task<Response<ArticleCategoryModel>> GetSingleAsync(int id)
        {
            var response = InitErrorResponse();
            try
            {
                var en = await _unitOfWork.ArticleCategoryRepository.GetSingleAsync(id);
                response = InitSuccessResponse(MessageConstant.Load);
                response.Item = GetModelFromEntity(en); 
            }catch(Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ArticleCategory RemoveChildEntity(ArticleCategory entity)
        {
            throw new NotImplementedException();
        }

        public bool Validate(ArticleCategoryModel modelToValidate)
        {
            return _validationDictionary.IsValid;
        }
    }
}
