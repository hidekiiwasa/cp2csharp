using Cp2_CsharpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cp2_CsharpBusiness
{
    public interface IProdutoService
    {
        Task<List<Produto>> ObterTodosAsync();
        Task<Produto> ObterPorIdAsync(int id);
        Task<Produto> ObterPorCodigoAsync(string codigo);
        Task<bool> CriarProdutoAsync(Produto produto);
        Task<bool> AtualizarProdutoAsync(Produto produto);
        Task<bool> ExcluirProdutoAsync(int id);
        Task<bool> AjustarEstoqueAsync(int id, int quantidade);
    }
}
