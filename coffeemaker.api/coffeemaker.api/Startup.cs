using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using dna.core.data;
using Microsoft.EntityFrameworkCore;
using dna.core.service.Services.Abstract;
using dna.core.service.Services;
using dna.core.service;
using dna.core.auth.Infrastructure;
using Microsoft.AspNetCore.Http;
using dna.core.auth.Entity;
using Microsoft.AspNetCore.Identity;

namespace coffeemaker.api
{
    public partial class Startup
    {
        private IHostingEnvironment CurrentEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            services.AddMvc();
           

            services.AddDbContext<DnaCoreContext>(options => {
                options.UseNpgsql(connection,
                b => b.MigrationsAssembly(assemblyName));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {               
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            })
           .AddEntityFrameworkStores<DnaCoreContext>()
           .AddDefaultTokenProviders();


            services.AddDnaCoreDependency();           
            SwaggerConfig(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            DnaAutoMapperConfiguration.Configure();

            //configure HttpContext so it can use by AuthenticationService
            HTTPHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            //Seed Database
            app.InitializeDatabase();


            UseSwagger(app);
            app.UseMvc();
        }
    }
}
