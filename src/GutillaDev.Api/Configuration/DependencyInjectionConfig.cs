using GutillDev.Business.Intefaces;
using GutillDev.Data.Repository;

namespace GutillaDev.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            return services;
        }
    }
}
