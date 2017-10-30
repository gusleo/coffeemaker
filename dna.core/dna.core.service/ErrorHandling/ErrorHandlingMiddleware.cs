using dna.core.data.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace dna.core.service.ErrorHandling
{
    //TODO: This class not integration testing yet
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;        
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception ex )
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if ( ex is NotImplementedException )
                code = HttpStatusCode.NotImplemented;
            else if ( ex is UnauthorizedAccessException )
                code = HttpStatusCode.Unauthorized;
            

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private void SaveErrorLog(HttpContext context, Exception ex)
        {
            /*var request = context.Request;
            var currentUser = context.User.Identity.Name;
            string controller = request.
            string action = request.RequestContext.RouteData.Values["action"].ToString();*/
        }
    }
}
