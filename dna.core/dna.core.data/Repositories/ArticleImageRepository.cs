using dna.core.data.Entities;
using dna.core.data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Repositories
{
   
    public class ArticleImageRepository : EntityReadWriteBaseRepository<ArticleImage>, IArticleImageRepository
    {
        public ArticleImageRepository(IDnaCoreContext context) : base(context)
        {

        }
    }
    public interface IArticleImageRepository : IWriteBaseRepository<ArticleImage>, IReadBaseRepository<ArticleImage>
    {

    }
}
