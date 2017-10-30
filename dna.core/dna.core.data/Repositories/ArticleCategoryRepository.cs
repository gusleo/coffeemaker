using dna.core.data.Entities;
using dna.core.data.Infrastructure;
using dna.core.data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
    public class ArticleCategoryRepository : EntityReadWriteBaseRepository<ArticleCategory>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(IDnaCoreContext context) : base(context)
        {

        }

        public int GetArticleCount(int id)
        {
            int count = _context.Article.Where(x => x.CategoryId == id).Count();
            return count;
        }
        public async Task<PaginationEntity<ArticleCategory>> GetAllAsync(int pageIndex, int pageSize, bool onlyVisibleCategory)
        {
            var query = (from c in _context.ArticleCategory                        
                            select c);

            if ( onlyVisibleCategory )
                query.Where(x => x.IsVisible == true);

            return new PaginationEntity<ArticleCategory>()
            {
                Items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(),
                Page = pageIndex,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };
        }
    }

    public interface IArticleCategoryRepository: IWriteBaseRepository<ArticleCategory>, IReadBaseRepository<ArticleCategory>
    {
        int GetArticleCount(int id);
        Task<PaginationEntity<ArticleCategory>> GetAllAsync(int pageIndex, int pageSize, bool onlyVisibleCategory);
    }
}
