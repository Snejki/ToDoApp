using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Api.Settings;

namespace ToDoApp.Api.Extensions
{
    public static class SettingsConfiguration
    {
        public static void AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
        }
    }
}
