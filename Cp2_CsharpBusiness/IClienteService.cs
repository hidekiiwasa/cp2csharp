using Cp2_CsharpModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cp2_CsharpBusiness
{
    public interface IClienteService
    {
        Task<bool> AdicionarClienteAsync(Cliente cliente);
        Task<List<Cliente>> BuscarTodosClientesAsync();
        Task<Cliente> BuscarClientePorIdAsync(int id);
        Task<bool> AtualizarClienteAsync(Cliente cliente);
        Task<bool> ExcluirClienteAsync(int id);
        Task<bool> ValidarLoginAsync(string login, string senha);
    }
}
