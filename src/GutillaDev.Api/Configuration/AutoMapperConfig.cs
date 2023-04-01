using AutoMapper;
using GutillaDev.Api.Controllers;
using GutillaDev.Api.ViewModels;
using GutillDev.Business.Models;

namespace GutillaDev.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
