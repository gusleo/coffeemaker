using dna.core.data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data
{
    public interface IDnaCoreContext
    {
        DbSet<Article> Article { get; set; }
        DbSet<ArticleCategory> ArticleCategory { get; set; }
        DbSet<ArticleImage> ArticleImage { get; set; }
        DbSet<Tag> Tag { get; set; }
        DbSet<ArticleTagMap> ArticleTag { get; set; }

        DbSet<UserDetail> UserDetail { get; set; }

        DbSet<TreeMenu> TreeMenu { get; set; }
        DbSet<Image> Image { get; set; }
        DbSet<Advertising> Advertising { get; set; }
        DbSet<FirebaseUserMap> FirebaseUserMap { get; set; }

        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Updated<TEntity>(TEntity entity) where TEntity : class;
  
        void Dispose();
    }
}
