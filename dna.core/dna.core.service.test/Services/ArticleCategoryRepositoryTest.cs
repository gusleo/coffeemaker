using AutoMapper;
using dna.core.data;
using dna.core.data.Entities;
using dna.core.data.UnitOfWork;
using dna.core.libs.Validation;
using dna.core.service.Infrastructure;
using dna.core.service.Models;
using dna.core.service.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace dna.core.service.test.Services
{
    public class ArticleCategoryRepositoryTest
    {
        private List<ArticleCategory> categories;

        public ArticleCategoryRepositoryTest()
        {
            DnaAutoMapperConfiguration.Configure();
            categories = MockData.Categories;

        }

        private ArticleCategoryService InitializeService(DnaCoreContext context, bool isMockData = true)
        {
            var val = new Mock<IValidationDictionary>();
            val.Setup(aa => aa.IsValid).Returns(true);

            if ( isMockData == true )
            {
                context.AddRange<ArticleCategory>(categories);
                context.AddRange<Article>(MockData.Articles);
            }
               

            var unitOfWork = new DNAUnitOfWork(context);
            var service = new ArticleCategoryService(unitOfWork);
            service.Initialize(val.Object);
            return service;
        }

        [Fact]
        public void Create_ReturnSuccess()
        {
            using ( var context = new DnaCoreContext(Helper.GetDbContextOption()) )
            {
                var en = categories.FirstOrDefault();
                var model = Mapper.Map<ArticleCategoryModel>(en);
                model.Id = 0;
                var service = InitializeService(context, false);
                var response = service.Create(model);


                Assert.Equal(1, response.Item.Id);
                Assert.Equal(true, response.Success);
                Assert.Equal(MessageConstant.Create, response.Message);
            }



        }

        [Fact]
        public void Edit_ReturnSuccess()
        {
            using ( var context = new DnaCoreContext(Helper.GetDbContextOption()) )
            {

                const string TITLE = "Another test";

                var service = InitializeService(context);
                var testArticle = context.ArticleCategory.FirstOrDefault();
                var category = Mapper.Map<ArticleCategoryModel>(testArticle);
                category.CategoryName = TITLE;
                var response = service.Edit(category);

                Assert.NotNull(response.Item);
                Assert.Equal(TITLE, response.Item.CategoryName);
                Assert.Equal(true, response.Success);
                Assert.Equal(MessageConstant.Update, response.Message);
            }


        }

        [Fact]
        public void Delete_ReturnSuccess()
        {
            using ( var context = new DnaCoreContext(Helper.GetDbContextOption()) )
            {

                var service = InitializeService(context);
                var response = service.Delete(2);
                var deleted = service.GetSingle(2);

                Assert.Equal(response.Success, true);
                Assert.Equal(response.Message, MessageConstant.Delete);
                Assert.Null(deleted.Item);
            }


        }

        [Fact]
        public void GetAll_ReturnSuccess()
        {
            using ( var context = new DnaCoreContext(Helper.GetDbContextOption()) )
            {

                var service = InitializeService(context);
                var response = service.GetAll(1, 10);               

                Assert.Equal(response.Success, true);
                Assert.Equal(response.Message, MessageConstant.Load);
                Assert.NotEqual(0, response.Item.Count);
            }


        }
    }
}
