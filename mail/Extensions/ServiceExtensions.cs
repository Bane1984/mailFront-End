using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mail.Models;
using Microsoft.Extensions.DependencyInjection;

namespace mail.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, CORSConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors",
                    builder =>
                    {
                        builder.WithOrigins(config.WithOrigin);
                        builder.WithMethods(config.WithMethod);
                    });
            });
        }
    }
}
