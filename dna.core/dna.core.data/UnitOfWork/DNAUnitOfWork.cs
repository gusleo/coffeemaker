using dna.core.data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.UnitOfWork
{
    public class DNAUnitOfWork : IDNAUnitOfWork
    {
        private readonly IDnaCoreContext _context;
        protected IArticleRepository _articleRepo;
        protected IUserDetailRepository _userDetailRepo;
        protected ITreeMenuRepository _treeMenuRepo;
        protected IArticleCategoryRepository _articleCategoryRepo;
        protected IErrorLogRepository _errorLogRepository;
        protected IImageRepository _imageRespository;
        private IAdvertisingRepository _advertisingRepository;
        protected IArticleImageRepository _articleImageRepository;
        protected IFirebaseUserMapRepository _firebaseUserMapRepository;

        public DNAUnitOfWork(IDnaCoreContext context)
        {
            _context = context;
        }

        public IErrorLogRepository ErrorLogRepository
        {
            get
            {
                if(_errorLogRepository == null )
                {
                    _errorLogRepository = new ErrorLogRepository(_context);
                }
                return _errorLogRepository;
            }
        }
        public IArticleRepository ArticleRepository
        {
            get
            {
                if ( _articleRepo == null )
                {
                    _articleRepo = new ArticleRepository(_context);
                }
                return _articleRepo;
            }
            
        }

        public virtual IUserDetailRepository UserDetailRepository
        {
            get
            {
                if ( _userDetailRepo == null )
                {
                    _userDetailRepo = new UserDetailRepository(_context);
                }
                return _userDetailRepo;
            }

        }



        public ITreeMenuRepository TreeMenuRepository
        {
            get
            {
                if ( _treeMenuRepo == null )
                {
                    _treeMenuRepo = new TreeMenuRepository(_context);
                }
                return _treeMenuRepo;
            }

        }

        public IArticleCategoryRepository ArticleCategoryRepository
        {
            get
            {
                if(_articleCategoryRepo == null )
                {
                    _articleCategoryRepo = new ArticleCategoryRepository(_context);
                }
                return _articleCategoryRepo;
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if ( _imageRespository == null )
                {
                    _imageRespository = new ImageRepository(_context);
                }
                return _imageRespository;
            }
        }

        public IAdvertisingRepository AdvertisingRepository
        {
            get
            {
                if(_advertisingRepository == null )
                {
                    _advertisingRepository = new AdvertisingRepository(_context);
                }
                return _advertisingRepository;
            }
        }

        public IArticleImageRepository ArticleImageRepository
        {
            get
            {
                if(_articleImageRepository == null )
                {
                    _articleImageRepository = new ArticleImageRepository(_context);
                }
                return _articleImageRepository;
            }
        }

        public IFirebaseUserMapRepository FirebaseUserMapRepository
        {
            get
            {
                if(_firebaseUserMapRepository == null )
                {
                    _firebaseUserMapRepository = new FirebaseUserMapRepository(_context);
                }
                return _firebaseUserMapRepository;
            }
        }


        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            // Save changes with the default options
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if ( !this.disposed )
            {
                if ( disposing )
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
