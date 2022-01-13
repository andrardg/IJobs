using IJobs.Utilities.JWTUtils;
using IJobs.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Utilities
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        public async Task invoke(HttpContext httpContext, IUserService userService, IJWTUtils ijwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
            var userId = ijwtUtils.ValidateJWTToken(token);

            if(userId != Guid.Empty)
            {
                httpContext.Items["User"] = userService.GetById(userId);
            }
            await _next(httpContext);
        }
    }
}
