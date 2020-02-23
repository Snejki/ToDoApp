using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ToDoApp.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void UseSswaggerExt(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ToDoAppOpenApiSpecification/swagger.json", "ToDoApp API");
                setupAction.RoutePrefix = "";
            });
        }

        public static void AddSwaggerExt(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction => {
                setupAction.SwaggerDoc(
                    "ToDoAppOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "ToDoApp",
                        Version = "1"
                    });

                var xmlDocumentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                setupAction.IncludeXmlComments(xmlDocumentFile);
            });
        }
    }
}
