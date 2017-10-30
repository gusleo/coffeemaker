using dna.core.auth.Entity;
using dna.core.data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data
{
    public class DnaCoreContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IDnaCoreContext
    {
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        public virtual DbSet<ArticleImage> ArticleImage { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<ArticleTagMap> ArticleTag { get; set; }

        public virtual DbSet<UserDetail> UserDetail { get; set; }

        public virtual DbSet<TreeMenu> TreeMenu { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Advertising> Advertising { get; set; }
        public virtual DbSet<FirebaseUserMap> FirebaseUserMap { get; set; }


        public DnaCoreContext(DbContextOptions<DnaCoreContext> options) : base(options)
        {
        }
        

      
        /// <summary>
        /// By default table behaviour is cascade delete, but we need some table not set as auto delete
        /// Set delete behaviour from cascade delete to cascade restrict
        /// Define manualy which table will cascade delete
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Remove Cascade Delate Bahaviour
            foreach ( var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()) )
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //delete behaviour

            modelBuilder.Entity<ArticleTagMap>().HasOne(x => x.Tag)
                .WithMany(x => x.TagMaps).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArticleTagMap>().HasOne(x => x.Article)
                .WithMany(x => x.TagMaps).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArticleImage>().HasOne(x => x.Article)
                .WithMany(x => x.ImageMaps).OnDelete(DeleteBehavior.Cascade);

            //TODO: Implement cascade delete for Image -> ArticleImage. When on the parent not have collection #NETCoreIssue
            modelBuilder.Entity<ArticleImage>()                
                .HasOne(x => x.Image).WithMany().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Advertising>().HasOne(x => x.Image)
                .WithOne().OnDelete(DeleteBehavior.Cascade);


        }

        public void Updated<TEntity>(TEntity entity) where TEntity : class
        {
            EntityEntry dbEntityEntry = this.Entry<TEntity>(entity);
            if ( dbEntityEntry.State == EntityState.Detached )
            {
                this.Set<TEntity>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
