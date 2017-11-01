using dna.core.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace coffeemaker.api
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DnaCoreContext>
    {
        public DnaCoreContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DnaCoreContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(assemblyName));

            return new DnaCoreContext(builder.Options);
        }
    }
}
