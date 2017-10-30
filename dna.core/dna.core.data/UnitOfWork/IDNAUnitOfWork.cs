using dna.core.data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.UnitOfWork
{
    public interface IDNAUnitOfWork : IDisposable
    {
        IArticleCategoryRepository ArticleCategoryRepository { get; }
        IArticleRepository ArticleRepository { get; }

        IUserDetailRepository UserDetailRepository { get; }
        ITreeMenuRepository TreeMenuRepository { get; }
        IErrorLogRepository ErrorLogRepository { get; }
        IImageRepository ImageRepository { get; }
        IAdvertisingRepository AdvertisingRepository { get; }
        IArticleImageRepository ArticleImageRepository { get; }
        IFirebaseUserMapRepository FirebaseUserMapRepository { get; }
        int Commit();
    }
}
