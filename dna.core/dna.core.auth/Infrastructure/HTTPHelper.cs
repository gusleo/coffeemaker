using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.auth.Infrastructure
{
    /// <summary>
    /// Put the HTTPHelper on Configure method at Startup.cs
    /// </summary>

    //syntaks: HttpHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
    public static class HTTPHelper
    {
        private static IHttpContextAccessor HttpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext
        {
            get
            {
                return HttpContextAccessor.HttpContext;
            }
        }
    }
}
