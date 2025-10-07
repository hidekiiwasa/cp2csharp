using Cp2_CsharpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cp2_CsharpBusiness
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ObterTodosAsync();
        Task<Pedido> ObterPorIdAsync(int id);
        Task<bool> CriarPedidoAsync(Pedido pedido);
        Task<bool> AtualizarPedidoAsync(Pedido pedido);
        Task<bool> ExcluirPedidoAsync(int id);
    }
}
