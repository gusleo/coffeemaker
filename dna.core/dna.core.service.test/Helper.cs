using dna.core.data;
using dna.core.data.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.test
{
    public static class Helper
    {
        public static void AddRange<T>(this DnaCoreContext context, List<T> collection) where T : class
        {
            context.Set<T>().AddRange(collection);
            context.SaveChanges();
        }
        public static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet;
        }
        public static DbContextOptions<DnaCoreContext> GetDbContextOption()
        {
            
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            DbContextOptions<DnaCoreContext> options;
            var builder = new DbContextOptionsBuilder<DnaCoreContext>();
            builder.UseInMemoryDatabase().UseInternalServiceProvider(serviceProvider);
            options = builder.Options;
            return options;
        }
    }
}
