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
using dna.core.data.Infrastructure;
using Microsoft.Extensions.Options;
using dna.core.libs.Upload.Config;

namespace dna.core.service.Services
{
    public class AdvertisingService : ReadWriteServiceBase<AdvertisingModel, Advertising>, IAdvertisingService
    {
        private readonly IDNAUnitOfWork _unitOfWork;
        private readonly ServerConfig _config;
        public AdvertisingService(IOptions<ServerConfig> config, IAuthenticationService authService, IDNAUnitOfWork unitOfWork) : base(authService)
        {
            _unitOfWork = unitOfWork;
            _config = config.Value;
        }

        public Response<AdvertisingModel> Create(AdvertisingModel modelToCreate)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToCreate) )
            {
                var userId = this.GetUserId();
                var en = GetEntityFromModel(modelToCreate);
                en.CreatedById = en.UpdatedById = userId;
                en.CreatedDate = en.UpdatedDate = DateTime.Now;
                _unitOfWork.AdvertisingRepository.Add(this.RemoveChildEntity(en));
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Create);
                response.Item = GetModelFromEntity(en);
            }
            else
            {
                response.Message = MessageConstant.ValidationError;
            }

            return response;


        }

        public async Task<Response<AdvertisingModel>> Delete(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.AdvertisingRepository.GetSingleAsync(id);
            if(en != null )
            {
                _unitOfWork.AdvertisingRepository.Delete(en);
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

        public Response<AdvertisingModel> Edit(AdvertisingModel modelToEdit)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToEdit) )
            {
                var userId = this.GetUserId();
                var en = GetEntityFromModel(modelToEdit);
                en.UpdatedById = userId;
                en.UpdatedDate = DateTime.Now;
                _unitOfWork.AdvertisingRepository.Edit(this.RemoveChildEntity(en));
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

        public async Task<Response<IList<AdvertisingModel>>> GetAdvertisingByTypeAndStatusAsync(AdvertisingType[] types, Status[] status)
        {
            var response = InitSuccessListResponse(MessageConstant.Load);
            var result =await _unitOfWork.AdvertisingRepository.FindByAsync(x => types.Contains(x.Type) && status.Contains(x.Status), x => x.Image);
            result = result.Select(x => { x.Image.ImageUrl = _config.Endpoint + "/" + x.Image.ImageUrl; return x; });
            response.Item = GetModelFromEntity(result.ToList());
            return response;

        }

        public async Task<Response<PaginationSet<AdvertisingModel>>> GetAllAsync(int pageIndex, int pageSize = 20)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.AdvertisingRepository.GetAllAsync(pageIndex, pageSize);
            response.Item = GetModelFromEntity(result);
            return response;
        }

        public async Task<Response<AdvertisingModel>> GetSingleAsync(int id)
        {
            var response = InitErrorResponse();
            var result = await _unitOfWork.AdvertisingRepository.GetSingleAsync(x => x.Id == id, x => x.Image);
            result.Image.ImageUrl = _config + "/" + result.Image.ImageUrl;
            if (result != null )
            {
                response = InitSuccessResponse(MessageConstant.Load);
                response.Item = GetModelFromEntity(result);

            }
            else
            {
                response.Message = MessageConstant.NotFound;
            }
            return response;
        }

        public Advertising RemoveChildEntity(Advertising entity)
        {
            entity.Image = null;
            entity.CreatedByUser = null;
            entity.UpdatedByUser = null;
            return entity;
        }

        public bool Validate(AdvertisingModel modelToValidate)
        {
            return _validationDictionary.IsValid;
        }
    }
}
