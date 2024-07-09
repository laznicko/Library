using Library.BusinessLayer.Interfaces;
using Library.BusinessLayer.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BusinessLayer
{
    public static class ServicesConfiguration
    {
        public static void ConfigureBusinessLayerServices(this IServiceCollection services)
        {
            // Register AutoMapper
            services.AddAutoMapper(typeof(Mapper.MapperProfile));

            // Register managers
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<IBorrowManager, BorrowManager>();
            services.AddScoped<IUserManager, UserManager>();
        }
    }
}
