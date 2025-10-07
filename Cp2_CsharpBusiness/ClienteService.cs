using Cp2_CsharpModel;
using Cp2_CsharpData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cp2_CsharpBusiness
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AdicionarClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        public async Task<List<Cliente>> BuscarTodosClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> BuscarClientePorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<bool> AtualizarClienteAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        public async Task<bool> ExcluirClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                var resultado = await _context.SaveChangesAsync();
                return resultado > 0;
            }
            return false;
        }

        public async Task<bool> ValidarLoginAsync(string login, string senha)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Login == login && c.Password == senha);
            return cliente != null;
        }
    }
}
