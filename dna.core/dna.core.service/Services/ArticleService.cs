using dna.core.service.Models;
using dna.core.service.Services.Abstract;
using System;
using dna.core.service.Infrastructure;
using dna.core.data.UnitOfWork;
using AutoMapper;
using dna.core.data.Entities;
using System.Collections.Generic;
using System.Linq;
using dna.core.auth;
using dna.core.libs.Extension;
using Microsoft.AspNetCore.Http;
using dna.core.libs.Upload.Config;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using dna.core.data.Infrastructure;

namespace dna.core.service.Services
{
    public class ArticleService :  ReadWriteServiceBase<ArticleModel, Article>, IArticleService
    {

        private readonly IDNAUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private ServerConfig _config;
        public ArticleService(IOptions<ServerConfig> config, IImageService imageService, IAuthenticationService authService, IDNAUnitOfWork unitOfWork) : base(authService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _config = config.Value;
        }
        public bool AllowUserToEdit(ArticleModel modelToEdit)
        {
            int userId = this.GetUserId();
            if ( this.IsSuperAdmin() )
            {
                return true;
            }else
            {
                return modelToEdit.CreatedById == userId;
            }

        }

        
        public Response<ArticleModel> Create(ArticleModel modelToCreate)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToCreate) )
            {
                var en = GetEntityFromModel(modelToCreate);
                var userId = this.GetUserId();

                en.ShortDescription = CreateShortDescription(en);
                en.CreatedById = en.UpdatedById = userId;
                en.CreatedDate = en.UpdatedDate = DateTime.Now;
                _unitOfWork.ArticleRepository.Add(this.RemoveChildEntity(en));
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Create);
                response.Item = GetModelFromEntity(en);
            }

            return response;
            
        }

        private string CreateShortDescription(Article en)
        {
            if ( String.IsNullOrEmpty(en.ShortDescription) )
            {
                string planText = en.Description.RemoveHtmlTag();
                planText = planText.Length < 150 ? planText : planText.Substring(0, 150);
                return planText;

            }else
            {
                return en.ShortDescription;
            }
        }

        public bool Validate(ArticleModel modelToValidate)
        {
            return _validationDictionary.IsValid;
        }

        public void CreateRange(List<ArticleModel> listOfData)
        {
            List<Article> data = Mapper.Map<List<Article>>(listOfData);
            _unitOfWork.ArticleRepository.AddRange(data);
            _unitOfWork.Commit();
        }

        public async Task<Response<ArticleModel>> Delete(int id)
        {
            var response = InitErrorResponse();    
            var en = await _unitOfWork.ArticleRepository.GetSingleAsync(id);
            if(en != null )
            {
               
                en.UpdatedById = this.GetUserId();
                en.UpdatedDate = DateTime.Now;
                en.Status = ArticleStatus.Archive;
                _unitOfWork.ArticleRepository.Edit(this.RemoveChildEntity(en));
                _unitOfWork.Commit();

                response = InitSuccessResponse(MessageConstant.Delete);
                response.Item = GetModelFromEntity(en);
            }else
            {
                response.Message = MessageConstant.NotFound;
            }
            return response;            

        }
       
        public Response<ArticleModel> Edit(ArticleModel modelToEdit)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToEdit)  && AllowUserToEdit(modelToEdit))
            {
                var en = GetEntityFromModel(modelToEdit);
                en.ShortDescription = CreateShortDescription(en);
                en.UpdatedById = this.GetUserId();
                en.UpdatedDate = DateTime.Now;

                _unitOfWork.ArticleRepository.Edit(this.RemoveChildEntity(en));
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

        
        public async Task<Response<ArticleModel>> GetSingleAsync(int id)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.ArticleRepository.GetSingleAsync(x => x.Id == id, x => x.Category, x => x.CreatedByUser);
            if(en != null )
            {
                var imgMap = await _unitOfWork.ArticleImageRepository.FindByAsync(x => x.ArticleId == en.Id && x.Image.IsPrimary == true, 
                    includeProperties => includeProperties.Image);
                imgMap = imgMap.Select(x => { x.Image.ImageUrl = String.Format("{0}/{1}", _config.Endpoint, x.Image.ImageUrl); return x; }).ToList();
                en.ImageMaps = imgMap.ToList();
                response = InitSuccessResponse(MessageConstant.Load);
                response.Item = GetModelFromEntity(en);
            }else
            {
                response.Message = MessageConstant.NotFound;
            }
            return response;
        }
        public async Task<Response<IList<ArticleModel>>> GetAllAsyncAll()
        {
            var response = InitSuccessListResponse(MessageConstant.Load);
            var result = await _unitOfWork.ArticleRepository.GetAllAsync();       
            response.Item = GetModelFromEntity(result.ToList());            
            return response;      
            
        }
        public async Task<Response<ArticleModel>> GetSingleDetailAsync(int id)
        {
            var res = InitErrorResponse();
            var result = await _unitOfWork.ArticleRepository.GetSingleAsync(x => x.Id == id, incl => incl.CreatedByUser);
            
            if(result != null )
            {
                res = InitSuccessResponse(MessageConstant.Load);
                res.Item = GetModelFromEntity(result);
            }else
            {
                res.Message = MessageConstant.NotFound;
            }            
            return res;
        }


        

        public async Task<Response<PaginationSet<ArticleModel>>> GetAllAsync(int pageIndex, int pageSize)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.ArticleRepository.GetAllAsync(pageIndex, pageSize);
            response.Item = GetModelFromEntity(result);            
            return response;
        }

        public async Task<Response<ArticleModel>> ChangeStatusAsync(int id, ArticleStatus status)
        {
            var response = InitErrorResponse();
            var en = await _unitOfWork.ArticleRepository.GetSingleAsync(id);
            if ( en != null)
            {
                en.Status = status;
                en.UpdatedById = this.GetUserId();
                en.UpdatedDate = DateTime.Now;
                _unitOfWork.ArticleRepository.Edit(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Update);
                response.Item = GetModelFromEntity(en);

            }else
            {
                response.Message = MessageConstant.NotFound;
            }
            
            return response;
        }

        public Response<IList<ArticleImageModel>> UploadArticleImage(int id, IList<IFormFile> files)
        {
            var response = InitErrorListResponse<ArticleImageModel>();
            

            var result = _imageService.UploadImage(files);
            if ( result.Success )
            {
                var articleImages = new List<ArticleImage>();
                foreach ( var item in result.Item )
                {
                    articleImages.Add(new ArticleImage
                    {
                        ArticleId = id,
                        ImageId = item.Id
                    });
                }
                _unitOfWork.ArticleImageRepository.AddRange(articleImages);
                _unitOfWork.Commit();

                response = InitSuccessListResponse<ArticleImageModel>(MessageConstant.Create);
                response.Item = GetModelFromEntity<ArticleImage, ArticleImageModel>(articleImages);

            }
            else
            {
                response.Message = result.Message;
            }

            return response;
        }

        public async Task<Response<ArticleImageModel>> UploadArticleImageCoverAsync(int id, IFormFile file)
        {

            var image = await _unitOfWork.ArticleImageRepository.GetSingleAsync(x => x.ArticleId == id 
                        && x.Image.IsPrimary == true, img => img.Image);

            if ( image != null )
            {
                //auto delete articleImage, sinces it uses cascade.delete
                await _imageService.Delete(image.Image.Id);

            }
            var response = UploadArticleImage(id, new List<IFormFile>() { file });
            if ( response.Success )
            {
                int imgId = response.Item[0].Id;
                var en = await _unitOfWork.ImageRepository.GetSingleAsync(imgId);
                en.IsPrimary = true;
                _unitOfWork.ImageRepository.Edit(en);
                _unitOfWork.Commit();
            }

            return new Response<ArticleImageModel>
            {
                Success = response.Success,
                Message = response.Message,
                Item = response.Item.FirstOrDefault()
            };

        }

        public Article RemoveChildEntity(Article entity)
        {
            entity.AcceptedByUser = null;
            entity.CreatedByUser = null;
            entity.UpdatedByUser = null;
            entity.TagMaps = null;
            entity.Category = null;
            entity.ImageMaps = null;

            return entity;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<Response<PaginationSet<ArticleModel>>> GetNewestArticleAsync(List<ArticleStatus> status, int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.ArticleRepository.GetNewestArticleAsync(pageIndex, pageSize, status.ToArray());
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            response.Item = GetModelFromEntity(result);
            return response;

        }

        public async Task<Response<PaginationSet<ArticleModel>>> FindByStatus(int pageIndex, int pageSize, ArticleStatus[] status)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.ArticleRepository.FindByAsync(x => status.Contains(x.Status), pageIndex, pageSize);
            response.Item = GetModelFromEntity(result);
            return response;
        }

        public async Task<Response<PaginationSet<ArticleModel>>> GetArticleByStaff(int staffId, int pageIndex, int pageSize, ArticleStatus[] status)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.ArticleRepository.GetNewestArticleAsync(pageIndex, pageSize, status, staffId);
            response.Item = GetModelFromEntity(result);
            return response;
        }
    }

    
}
