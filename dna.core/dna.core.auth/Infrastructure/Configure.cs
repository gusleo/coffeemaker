using dna.core.libs.Sender;
using Microsoft.Extensions.DependencyInjection;

namespace dna.core.auth.Infrastructure
{
    public static class Configure
    {

        public static void AddDnaAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Remeber to add config on the appsetting.json        
            services.AddTransient<ISenderFactory, SenderFactory>();
           
        }

    }
}
