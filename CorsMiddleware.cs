using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IngresoSML2
{

    public static class CorsMiddleware
    {

        private static string _corsAnyOrigin = "_corsCrossPort";

        public static void ConfigurerCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_corsAnyOrigin, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        public static void UseCorsMiddleware(this IApplicationBuilder app)
        {
            app.UseCors(_corsAnyOrigin);
        }
    }
}
