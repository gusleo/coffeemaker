using dna.core.auth.Entity;
using dna.core.auth.Infrastructure;
using dna.core.data.Entities;
using dna.core.data.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dna.core.data
{
   
    public static class DbInitializerExtension
    {
         private static DnaCoreContext _context;

        public static async void InitializeDatabase(this IApplicationBuilder app)
        {
            using ( var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope() )
            {
                _context = serviceScope.ServiceProvider.GetRequiredService<DnaCoreContext>();

                try
                {
                    await _context.Database.MigrateAsync();
                }
                catch ( Exception ex )
                {
                    Console.Write("Database already migrate. Detail: " + ex.Message);
                }

                await Task.Run(() => InitData());
            }
        }


        public static async Task  InitData()
        {
            await SeedUser();

            if ( !_context.ArticleCategory.Any() )
            {
                var categories = new List<ArticleCategory>()
                {
                    new ArticleCategory{IsVisible = true, Name = "ASP.Net Core", Slug = "aspnetcore"},
                    new ArticleCategory{IsVisible = true, Name = "MS SQL Server", Slug = "mssql"},
                    new ArticleCategory{IsVisible = true, Name = "Angular 2", Slug = "angular2"}
                };
                _context.ArticleCategory.AddRange(categories);
                _context.SaveChanges();
            }

            if ( !_context.Tag.Any() )
            {
                var tags = new List<Tag>()
                {
                    new Tag { TagName = "aspnet"},
                    new Tag { TagName = "sqlserver"},
                    new Tag { TagName = "programming"},
                    new Tag { TagName = "angular2"},
                    new Tag { TagName = "javascript"},
                    new Tag { TagName = "c#"},
                    new Tag { TagName = "unittesting"}
                };
                _context.Tag.AddRange(tags);
                _context.SaveChanges();
            }

            if ( !_context.Article.Any() )
            {
                string dummyDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                                                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                                                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                var articles = new List<Article>()
                {
                    new Article{ CategoryId = 1, CreatedById = 1, UpdatedById = 1, CreatedDate = DateTime.Now, IsFeatured = false,
                                    Status = ArticleStatus.Confirmed, Title = "How to mastering asp.net core", Slug = "howto-mastering-aspnetcore", Description = dummyDescription},
                    new Article{CategoryId = 2, CreatedById = 1, UpdatedById = 1, CreatedDate = DateTime.Now, IsFeatured = false,
                                    Status = ArticleStatus.Confirmed, Title = "How to mastering Sql Server", Slug = "howto-mastering-sqlserver", Description = dummyDescription},
                    new Article{ CategoryId = 3, CreatedById = 1, UpdatedById = 1, CreatedDate = DateTime.Now, IsFeatured = false,
                                    Status = ArticleStatus.Confirmed, Title = "How to mastering Angular", Slug = "howto-mastering-angular", Description = dummyDescription},
                };

                _context.Article.AddRange(articles);
                _context.SaveChanges();
            }

            if ( !_context.ArticleTag.Any() )
            {
                var articeltags = new List<ArticleTagMap>()
                {
                    new ArticleTagMap { ArticleId = 1, TagId = 1},
                    new ArticleTagMap { ArticleId = 1, TagId = 2},
                    new ArticleTagMap { ArticleId = 1, TagId = 6},
                    new ArticleTagMap { ArticleId = 1, TagId = 7},
                    new ArticleTagMap { ArticleId = 2, TagId = 2},
                    new ArticleTagMap { ArticleId = 2, TagId = 3}
                   
                };
                _context.ArticleTag.AddRange(articeltags);
                _context.SaveChanges();
            }

           
           
        }

        public static async Task SeedUser(){

            var user = new ApplicationUser
            {
                UserName = "admin@yourdomain.com",
                NormalizedUserName = "admin@yourdomain.com",
                Email = "admin@yourdomain.com",
                NormalizedEmail = "admin@yourdomain.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new CustomRoleStore(_context);

            if ( !_context.Roles.Any(r => r.Name == MembershipConstant.SuperAdmin) )
            {
                await roleStore.CreateAsync(new ApplicationRole { Name = MembershipConstant.SuperAdmin, NormalizedName = MembershipConstant.SuperAdmin });
            }

            if ( !_context.Users.Any(u => u.UserName == user.UserName) )
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "yourpassword123!");
                user.PasswordHash = hashed;
                var userStore = new CustomUserStore(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, MembershipConstant.SuperAdmin);
            }
        }
        
    }
}
