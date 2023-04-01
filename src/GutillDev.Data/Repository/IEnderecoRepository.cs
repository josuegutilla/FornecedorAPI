using GutillDev.Business.Models;

namespace GutillDev.Data.Repository
{
    public interface IEnderecoRepository
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}