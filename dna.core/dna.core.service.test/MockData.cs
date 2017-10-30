using dna.core.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.test
{
    public static class MockData
    {
        public static List<ArticleCategory> Categories
        {
            get
            {
                return new List<ArticleCategory>()
                {
                    new ArticleCategory {Id = 1, CategoryName = "Unknown", IsVisible = true, ParentId = 0, Slug = "unknown" },
                    new ArticleCategory {Id = 2, CategoryName = "Kesehatan", IsVisible = true, ParentId = 0, Slug = "kesehatan" },
                    new ArticleCategory {Id = 3, CategoryName = "Kandungan", IsVisible = true, ParentId = 1, Slug = "kandungan" }
                };
            }
        }
        public static List<Article> Articles {
            get
            {
                return new List<Article>()
                {
                    new Article {
                        Id = 1, Title = "Article Number One", Slug = "article-number-one", CategoryId = 1,
                        Status = ArticleStatus.Confirmed, AcceptedById = 1, AcceptedDate = DateTime.Now,
                        CreatedById = 2, CreatedDate = DateTime.Now, UpdatedById = 3, UpdatedDate = DateTime.Now,
                        IsFeatured = false, Metadata = "",
                        ShortDescription = "Article number one", 
                        Description = "Lorem ipsum dolor sit ame"                        
                    },
                    new Article {
                        Id = 2, Title = "Article Number two", Slug = "article-number-two", CategoryId = 1,
                        Status = ArticleStatus.Confirmed, AcceptedById = 1, AcceptedDate = DateTime.Now,
                        CreatedById = 2, CreatedDate = DateTime.Now, UpdatedById = 3, UpdatedDate = DateTime.Now,
                        IsFeatured = false, Metadata = "",
                        ShortDescription = "Article number one",
                        Description = "Lorem ipsum dolor sit ame"
                    }
                };
            }
        }
        
    }
}
