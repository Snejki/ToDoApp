using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToDoApp.Api.Extensions;
using ToDoApp.Db;

namespace ToDoApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => {
                    options.EnableEndpointRouting = false;
                    });

            services.AddDbContext<ToDoAppContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerExt();

            services.AddDependencyInjection();
            services.AddSettingsConfiguration(Configuration);
            services.AddJwtAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.ConfigureExceptionHandler(logger);
            }

            app.UseSswaggerExt();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
