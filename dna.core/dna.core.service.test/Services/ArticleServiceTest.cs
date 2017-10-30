using AutoMapper;
using dna.core.data;
using dna.core.data.Entities;
using dna.core.data.UnitOfWork;
using dna.core.libs.Validation;
using dna.core.service.Infrastructure;
using dna.core.service.Models;
using dna.core.service.Services;
using dna.core.service.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace dna.core.service.test.Services
{
    public class ArticleServiceTest
    {           
        private List<Article> articles;

        public ArticleServiceTest()
        {
            DnaAutoMapperConfiguration.Configure();
            articles = MockData.Articles;            

        }

        private ArticleService InitializeService(DnaCoreContext context, bool isMockData = true)
        {
            var val = new Mock<IValidationDictionary>();
            val.Setup(aa => aa.IsValid).Returns(true);
            if(isMockData == true)
                context.AddRange<Article>(articles);

            var unitOfWork = new DNAUnitOfWork(context);
            var service = new ArticleService(unitOfWork);
            service.Initialize(val.Object);
            return service;
        }

        [Fact]
        public void Create_ReturnSuccess()
        {
            using (var context = new DnaCoreContext(Helper.GetDbContextOption()) )
            {
                var en = articles.FirstOrDefault();
                var model = Mapper.Map<ArticleModel>(en);
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
                var testArticle = context.Article.FirstOrDefault();
                var article = Mapper.Map<ArticleModel>(testArticle);
                article.Title = TITLE;
                var response = service.Edit(article);

                Assert.NotNull(response.Item);
                Assert.Equal(TITLE, response.Item.Title);          
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
        public void Archive_ReturnSuccess()
        {
            using ( var context = new DnaCoreContext(Helper.GetDbContextOption()) )
            {
                
                var service = InitializeService(context);
                var response = service.Archive(2);
                var archived = service.GetSingle(2);

                Assert.Equal(response.Success, true);
                Assert.Equal(response.Message, MessageConstant.Update);
                Assert.Equal(Models.ArticleStatus.Archive, archived.Item.Status);
            }


        }

        
    }
}
