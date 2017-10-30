using dna.core.auth;
using dna.core.data.Entities;
using dna.core.data.UnitOfWork;
using dna.core.service.Models;
using dna.core.service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dna.core.service.Infrastructure;

namespace dna.core.service.Services
{
    public class FirebaseUserMapService : ReadWriteServiceBase<FirebaseUserMapModel, FirebaseUserMap>, IFirebaseUserMapUserService
    {
        private readonly IDNAUnitOfWork _unitOfWork;
        
        public FirebaseUserMapService(IAuthenticationService authService, IDNAUnitOfWork unitOfWork) : base(authService)
        {
            _unitOfWork = unitOfWork;          
        }

        public Response<FirebaseUserMapModel> Create(FirebaseUserMapModel modelToCreate)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToCreate) )
            {
                var en = GetEntityFromModel(modelToCreate);
                en.UserId = this.GetUserId();
                var existing = _unitOfWork.FirebaseUserMapRepository.GetSingle(x => x.UserId == en.UserId 
                                && x.DeviceId == modelToCreate.DeviceId);
                if(existing == null )
                {
                    _unitOfWork.FirebaseUserMapRepository.Add(this.RemoveChildEntity(en));
                    _unitOfWork.Commit();
                    response = InitSuccessResponse(MessageConstant.Create);
                   
                }else
                {
                    en = existing;
                    en.DeviceToken = modelToCreate.DeviceToken;
                    _unitOfWork.FirebaseUserMapRepository.Edit(this.RemoveChildEntity(en));
                    _unitOfWork.Commit();
                    response = InitSuccessResponse(MessageConstant.Update);
                }

                response.Item = GetModelFromEntity(en);

            }
            else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
        }

        public async Task<Response<FirebaseUserMapModel>> Delete(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.FirebaseUserMapRepository.GetSingleAsync(id);
            if(en != null )
            {
                _unitOfWork.FirebaseUserMapRepository.Delete(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Delete);
                response.Item = GetModelFromEntity(en);
            }else
            {
                response.Message = MessageConstant.NotFound;
            }
            return response;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public Response<FirebaseUserMapModel> Edit(FirebaseUserMapModel modelToEdit)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToEdit) )
            {
                var en = GetEntityFromModel(modelToEdit);
                en.UserId = this.GetUserId();
                _unitOfWork.FirebaseUserMapRepository.Edit(en);
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

        public async Task<Response<PaginationSet<FirebaseUserMapModel>>> GetAllAsync(int pageIndex, int pageSize = 20)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.FirebaseUserMapRepository.GetAllAsync(pageIndex, pageSize);
            response.Item = GetModelFromEntity(result);
            return response;
        }

        public async Task<Response<FirebaseUserMapModel>> GetSingleAsync(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.FirebaseUserMapRepository.GetSingleAsync(id);
            if(en != null )
            {
                response = InitSuccessResponse(MessageConstant.Load);
                response.Item = GetModelFromEntity(en);
            }else
            {
                response.Message = MessageConstant.NotFound;
            }
            return response;
        }

      

        public FirebaseUserMap RemoveChildEntity(FirebaseUserMap entity)
        {
            entity.User = null;
            return entity;
        }

        public bool Validate(FirebaseUserMapModel modelToValidate)
        {
            return _validationDictionary.IsValid;
        }
    }
}
