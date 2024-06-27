using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.BLL {
    public static class ContextExtension {
        public static void AddContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MainContext>(options => options.UseSqlServer(connection));
        }
    }
    public static class SaveUnitExtension {
        public static void AddSaveUnitService(this IServiceCollection services)
        {
            services.AddScoped<ISaveUnit, SaveUnit>();
        }
    }
    public class ValidationException : Exception 
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}