using DevIO.Data.Mappings;
using DevIO.Data.Repository;
using GutillDev.Business.Intefaces;
using GutillDev.Business.Notificacoes;
using GutillDev.Business.Services;
using GutillDev.Data.Repository;

namespace GutillaDev.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
