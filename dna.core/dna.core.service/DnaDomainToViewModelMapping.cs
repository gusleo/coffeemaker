using AutoMapper;
using dna.core.data.Entities;
using dna.core.service.Mapping;
using dna.core.service.Models;
using System;

namespace dna.core.service
{
    public class DnaDomainToViewModelMapping : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<Article, ArticleModel>().IgnoreAllNonExisting(this);
            CreateMap<ArticleCategory, ArticleCategoryModel>().IgnoreAllNonExisting(this);
            CreateMap<ArticleImage, ArticleImageModel>().IgnoreAllNonExisting(this);
            CreateMap<ArticleTagMap, ArticleTagMapModel>().IgnoreAllNonExisting(this);
            CreateMap<UserDetail, UserDetailModel>().IgnoreAllNonExisting(this);
            CreateMap<TreeMenu, TreeMenuModel>().IgnoreAllNonExisting(this);
            CreateMap<Advertising, AdvertisingModel>().IgnoreAllNonExisting(this);
            CreateMap<FirebaseUserMap, FirebaseUserMapModel>().IgnoreAllNonExisting(this);
        }
    }
}
