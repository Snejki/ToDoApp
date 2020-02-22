using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Api.Interfaces;
using ToDoApp.Api.Repositories;
using ToDoApp.Api.Services;

namespace ToDoApp.Api.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.InjectRepositories();
            services.InjectServices();
        }

        private static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
        }

        private static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IEncrypter, Encrypter>();
            services.AddTransient<IJwthandler, JwtHandler>();
        }
    }
}
