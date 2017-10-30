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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace dna.core.service.Services
{
    public class ImageService : ReadWriteServiceBase<ImageModel, Image>, IImageService
    {
        private readonly IDNAUnitOfWork _unitOfWork;
        private readonly IFileServices _fileService;
        public ImageService(IAuthenticationService authService, IFileServices fileService, IDNAUnitOfWork unitOfWork) : base(authService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public Response<ImageModel> Create(ImageModel modelToCreate)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToCreate) )
            {
                var userId = this.GetUserId();
                var en = GetEntityFromModel(modelToCreate);
                en.CreatedById = en.UpdatedById = userId;
                en.CreatedDate = en.UpdatedDate = DateTime.Now;

                _unitOfWork.ImageRepository.Add(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Create);
            }else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
            
        }

        public async Task<Response<ImageModel>> Delete(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.ImageRepository.GetSingleAsync(id);
            if ( en != null)
            {
                _unitOfWork.ImageRepository.Delete(en);
                _unitOfWork.Commit();

                _fileService.Delete(en.ImageUrl);
                response = InitSuccessResponse(MessageConstant.Delete);
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

        public Response<ImageModel> Edit(ImageModel modelToEdit)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToEdit) )
            {
                var userId = this.GetUserId();
                var en = GetEntityFromModel(modelToEdit);
                en.UpdatedById = userId;
                en.UpdatedDate = DateTime.Now;

                _unitOfWork.ImageRepository.Edit(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Update);
            }
            else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
        }

        public async Task<Response<PaginationSet<ImageModel>>> GetAllAsync(int pageIndex, int pageSize = 20)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.ImageRepository.GetAllAsync(pageIndex, pageSize);
            response.Item = GetModelFromEntity(result);
            return response;
        }

        public async Task<Response<ImageModel>> GetSingleAsync(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.ImageRepository.GetSingleAsync(id);
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

        public bool Validate(ImageModel modelToValidate)
        {
            return _validationDictionary.IsValid;
        }

        public Response<ImageModel> UploadImage(IFormFile file)
        {
            var response = InitErrorResponse();
            var fileResponse = _fileService.Upload(file);
            if ( fileResponse.Success )
            {
                var userId = this.GetUserId();
                var item = fileResponse.Item;
                Image imageToCreate = ImageToCreate(userId, item);
                _unitOfWork.ImageRepository.Add(imageToCreate);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Create);
                response.Item = GetModelFromEntity(imageToCreate);
            }
            else
            {
                response.Message = fileResponse.Message;
            }
            return response;

        }

        public Response<IList<ImageModel>> UploadImage(IList<IFormFile> files)
        {
            var response = InitErrorListResponse();
            var fileResponse = _fileService.Upload(files);
            if ( fileResponse.Success )
            {
                var userId = this.GetUserId();
                var items = fileResponse.Item;
                IList<Image> images = new List<Image>();
                
                foreach(var item in items )
                {
                    Image imageToCreate = ImageToCreate(userId, item);
                    images.Add(imageToCreate);
                }
                
                _unitOfWork.ImageRepository.AddRange(images);
                _unitOfWork.Commit();
                response = InitSuccessListResponse(MessageConstant.Create);
                response.Item = GetModelFromEntity(images);
            }
            else
            {
                response.Message = fileResponse.Message;
            }
            return response;
        }

        private Image ImageToCreate(int userId, JsonModel.FileModel item)
        {
            return new Image
            {
                FileName = Path.GetFileName(item.FileName),
                FileExtension = Path.GetExtension(item.FileName),
                Description = String.Empty,
                ImageUrl = item.Path,
                IsPrimary = item.IsCover,
                CreatedById = userId,
                UpdatedById = userId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
        }

        public Image RemoveChildEntity(Image entity)
        {
            return entity;
        }
    }
}
