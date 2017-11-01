using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace coffeemaker.api
{
    public partial class Startup
    {
        public void SwaggerConfig(IServiceCollection services)
        {
            //Swagger config
            string pathToDoc = string.Empty;

            
                pathToDoc = $@"{CurrentEnvironment.ContentRootPath}\MediCore.Api.xml";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

        }

        /// <summary>
        /// Using swagger
        /// </summary>
        /// <param name="app">Application builder <see cref="IApplicationBuilder"/> </param>
        public void UseSwagger(IApplicationBuilder app)
        {
            //use swagger (always after app.UseMvc();)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // keeping swagger UI URL consistent with my previous settings
                c.RoutePrefix = "swagger/ui";
                // adding endpoint to JSON file containing API endpoints for UI
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                // disabling the Swagger validator -- passing null as validator URL.
                // Alternatively, you can specify your own internal validator
                c.EnabledValidator(null);
            });
        }
    }
}
