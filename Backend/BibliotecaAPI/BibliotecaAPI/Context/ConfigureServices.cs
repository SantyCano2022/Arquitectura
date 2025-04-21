using BibliotecaAPI.Interfaces;
using BibliotecaAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Context
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DBbiblioteca"))
            .LogTo(Console.WriteLine, LogLevel.Information)
            );

            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddScoped<TipoMaterialService>();

            services.AddScoped<UsuarioService>();

            services.AddScoped<DevolucionService>();

            services.AddScoped<PrestamoService>();

            services.AddScoped<MaterialService>();

            services.AddScoped<RolService>();



            services.AddHttpClient();

            return services;
        }
    }
}
