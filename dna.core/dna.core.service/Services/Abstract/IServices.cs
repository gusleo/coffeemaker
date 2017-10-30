using dna.core.data.Entities;
using dna.core.data.Infrastructure;
using dna.core.libs.TreeMenu;
using dna.core.service.Infrastructure;
using dna.core.service.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dna.core.service.Services.Abstract
{
    public interface IArticleService : IReadWriteService<ArticleModel, Article>
    {
        Task<Response<ArticleModel>> GetSingleDetailAsync(int id);
        Task<Response<ArticleModel>> ChangeStatusAsync(int id, ArticleStatus status);
        Task<Response<PaginationSet<ArticleModel>>> GetNewestArticleAsync(List<ArticleStatus> status, int pageIndex, int pageSize);
        Response<IList<ArticleImageModel>> UploadArticleImage(int id, IList<IFormFile> files);
        Task<Response<ArticleImageModel>> UploadArticleImageCoverAsync(int id, IFormFile file);
        Task<Response<PaginationSet<ArticleModel>>> FindByStatus(int pageIndex, int pageSize, ArticleStatus[] status);
        Task<Response<PaginationSet<ArticleModel>>> GetArticleByStaff(int staffId, int pageIndex, int pageSize, ArticleStatus[] status);
    }

    public interface ITreeMenuService : IReadWriteService<TreeMenuModel, TreeMenu>
    {
        Task<Response<IList<MenuItem>>> GetMenuByTypeAsync(MenuType type);
        Task<Response<IList<TreeMenuModel>>> GetAllParentMenuAsync(MenuType type);
        Task<Response<PaginationSet<TreeMenuModel>>> GetMenuByTypeAsync(MenuType type, int pageIndex, int pageSize);

    }
    public interface IArticleCategoryService : IReadWriteService<ArticleCategoryModel, ArticleCategory>
    {
        Task<Response<ArticleCategoryModel>> GetArticleCountAsync(int id);
    }
    public interface IImageService : IReadWriteService<ImageModel, Image>
    {
        Response<ImageModel> UploadImage(IFormFile file);
        Response<IList<ImageModel>> UploadImage(IList<IFormFile> files);
    }

    public interface IErrorLogService : IReadWriteService<ErrorLogModel, ErrorLog> { }

    public interface IAdvertisingService : IReadWriteService<AdvertisingModel, Advertising>
    {
        Task<Response<IList<AdvertisingModel>>> GetAdvertisingByTypeAndStatusAsync(AdvertisingType[] types, Status[] status);
    }
    public interface IFirebaseUserMapUserService : IReadWriteService<FirebaseUserMapModel, FirebaseUserMap>
    {
      
    }
}
