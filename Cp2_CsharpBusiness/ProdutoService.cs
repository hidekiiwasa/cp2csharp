using Cp2_CsharpData;
using Cp2_CsharpModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cp2_CsharpBusiness
{
    public class ProdutoService : IProdutoService
    {
        private readonly ApplicationDbContext _context;

        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> ObterPorIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<Produto> ObterPorCodigoAsync(string codigo)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<bool> CriarProdutoAsync(Produto produto)
        {
            var produtoExistente = await ObterPorCodigoAsync(produto.Codigo);
            if (produtoExistente != null)
            {
                return false; 
            }

            _context.Produtos.Add(produto);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AtualizarProdutoAsync(Produto produto)
        {
            var produtoExistente = await ObterPorIdAsync(produto.Id);
            if (produtoExistente == null)
            {
                return false;
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Estoque = produto.Estoque;

            _context.Produtos.Update(produtoExistente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExcluirProdutoAsync(int id)
        {
            var produtoExistente = await ObterPorIdAsync(id);
            if (produtoExistente == null)
            {
                return false;
            }

            _context.Produtos.Remove(produtoExistente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AjustarEstoqueAsync(int id, int quantidade)
        {
            var produto = await ObterPorIdAsync(id);
            if (produto == null)
            {
                return false;
            }

            try
            {
                produto.AjustarEstoque(quantidade);
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}