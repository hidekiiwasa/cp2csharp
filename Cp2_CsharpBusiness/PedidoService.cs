using Cp2_CsharpData;
using Cp2_CsharpModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cp2_CsharpBusiness
{

    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<bool> CriarPedidoAsync(Pedido pedido)
        {
            var pedidoExistente = await _context.Pedidos
                .FirstOrDefaultAsync(p => p.Codigo == pedido.Codigo);

            if (pedidoExistente != null)
            {
                return false;
            }

            _context.Pedidos.Add(pedido);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AtualizarPedidoAsync(Pedido pedido)
        {
            var pedidoExistente = await ObterPorIdAsync(pedido.Id);
            if (pedidoExistente == null)
            {
                return false;
            }

            if (pedidoExistente.EstaEntregue())
            {
                return false;
            }

            pedidoExistente.Status = pedido.Status;
            pedidoExistente.ValorTotal = pedido.ValorTotal;
            pedidoExistente.Data = pedido.Data;

            _context.Pedidos.Update(pedidoExistente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExcluirPedidoAsync(int id)
        {
            var pedidoExistente = await ObterPorIdAsync(id);
            if (pedidoExistente == null)
            {
                return false;
            }

            if (pedidoExistente.EstaEntregue())
            {
                return false;
            }

            _context.Pedidos.Remove(pedidoExistente);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
