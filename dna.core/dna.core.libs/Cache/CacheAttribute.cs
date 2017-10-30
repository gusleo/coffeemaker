using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using dna.core.libs.Extension;

namespace dna.core.libs.Cache
{
    
   
    public class CacheAttribute : ActionFilterAttribute
    {
        private const string SuperAdmin = "SuperAdmin";
        protected ICacheService CacheService { set; get; }

        public int Duration { set; get; }
        public bool IgnoreSuperAdmin { get; set; }

        public CacheAttribute()
        {
            Duration = 60;
            IgnoreSuperAdmin = true;
        }

       


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //By default, we will not caching SuperAdmin
            bool isSuperAdmin = false;
            if(IgnoreSuperAdmin == false )
            {
                isSuperAdmin = context.HttpContext.User.IsInRole(SuperAdmin);
            }
            

            if ( context.HttpContext.Request.Method == "GET"  && !isSuperAdmin)
            {
                GetServices(context);
                var requestUrl = context.HttpContext.Request.GetEncodedUrl();
                var cacheKey = requestUrl.ToMd5();
                var cachedResult = CacheService.Get<IActionResult>(cacheKey);

                if ( cachedResult != null )
                {
                    //cache hit                   
                    context.Result = cachedResult;
                    return;

                }
                else
                {
                    //cache miss
                }
            }

               
            base.OnActionExecuting(context);

        }

       
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            
           
            if(context.HttpContext.Request.Method == "GET" )
            {
                GetServices(context);
                var cacheKey = context.HttpContext.Request.GetEncodedUrl().ToMd5();
                Task.Factory.StartNew(() =>
                {
                    CacheService.Store(cacheKey, context.Result, Duration);
                });
            }
                      
            
            base.OnResultExecuted(context);
        }
        protected void GetServices(FilterContext context)
        {
            CacheService = context.HttpContext.RequestServices.GetService(typeof(ICacheService)) as ICacheService;
        }
    }
}
