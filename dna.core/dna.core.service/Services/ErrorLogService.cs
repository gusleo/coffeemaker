using dna.core.data.Entities;
using dna.core.service.Models;
using dna.core.service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dna.core.service.Infrastructure;
using dna.core.data.UnitOfWork;
using dna.core.auth;

namespace dna.core.service.Services
{
    public class ErrorLogService : ReadWriteServiceBase<ErrorLogModel, ErrorLog>, IErrorLogService
    {
        private readonly IDNAUnitOfWork _unitOfWork;

        public ErrorLogService(IAuthenticationService authService, IDNAUnitOfWork unitOfWork) : base(authService)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<ErrorLogModel> Create(ErrorLogModel modelToCreate)
        {
            var response = InitErrorResponse();

            var en = GetEntityFromModel(modelToCreate);
            _unitOfWork.ErrorLogRepository.Add(en);
            _unitOfWork.Commit();

            response = InitSuccessResponse(MessageConstant.Create);
            response.Item = GetModelFromEntity(en);
            return response;
        }

        public async Task<Response<ErrorLogModel>> Delete(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.ErrorLogRepository.GetSingleAsync(id);
            if(en != null )
            {
                _unitOfWork.ErrorLogRepository.Delete(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Delete);
                response.Item = GetModelFromEntity(en);
            }
            else
            {
                response.Message = MessageConstant.NotFound;
            }
            return response;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public Response<ErrorLogModel> Edit(ErrorLogModel modelToEdit)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToEdit) )
            {
                var en = GetEntityFromModel(modelToEdit);
                _unitOfWork.ErrorLogRepository.Edit(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Update);
                response.Item = GetModelFromEntity(en);
            }else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
        }

        public async Task<Response<PaginationSet<ErrorLogModel>>> GetAllAsync(int pageIndex, int pageSize = 20)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.ErrorLogRepository.GetAllAsync(pageIndex, pageSize);
            response.Item = GetModelFromEntity(result);
            return response;
        }

        public async Task<Response<ErrorLogModel>> GetSingleAsync(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.ErrorLogRepository.GetSingleAsync(id);
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

        public ErrorLog RemoveChildEntity(ErrorLog entity)
        {
            return entity;
        }

        public bool Validate(ErrorLogModel modelToValidate)
        {
            return _validationDictionary.IsValid;
        }
    }
}
