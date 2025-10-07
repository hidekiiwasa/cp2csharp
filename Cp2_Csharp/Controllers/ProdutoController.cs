using Cp2_CsharpBusiness;
using Cp2_CsharpModel;
using Microsoft.AspNetCore.Mvc;

namespace Cp2_CsharpApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // Endpoint para criar um novo produto
        [HttpPost]
        public async Task<ActionResult> CriarProduto([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto inválido.");
            }

            bool sucesso = await _produtoService.CriarProdutoAsync(produto);
            if (sucesso)
            {
                return CreatedAtAction(nameof(ObterProduto), new { id = produto.Id }, produto);
            }

            return Conflict("Produto com o mesmo código já existe.");
        }

        // Endpoint para listar todos os produtos
        [HttpGet]
        public async Task<ActionResult<List<Produto>>> ObterTodosProdutos()
        {
            var produtos = await _produtoService.ObterTodosAsync();
            if (produtos == null || produtos.Count == 0)
            {
                return NotFound("Nenhum produto encontrado.");
            }

            return Ok(produtos);
        }

        // Endpoint para obter produto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ObterProduto(int id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }

            return Ok(produto);
        }

        // Endpoint para atualizar um produto
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarProduto(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest("ID do produto não confere.");
            }

            bool sucesso = await _produtoService.AtualizarProdutoAsync(produto);
            if (sucesso)
            {
                return NoContent(); // Atualização bem-sucedida
            }

            return NotFound($"Produto com ID {id} não encontrado.");
        }

        // Endpoint para excluir um produto
        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirProduto(int id)
        {
            bool sucesso = await _produtoService.ExcluirProdutoAsync(id);
            if (sucesso)
            {
                return NoContent(); // Exclusão bem-sucedida
            }

            return NotFound($"Produto com ID {id} não encontrado.");
        }

        // Endpoint para ajustar o estoque de um produto
        [HttpPatch("{id}/estoque")]
        public async Task<ActionResult> AjustarEstoque(int id, [FromQuery] int quantidade)
        {
            bool sucesso = await _produtoService.AjustarEstoqueAsync(id, quantidade);
            if (sucesso)
            {
                return Ok("Estoque ajustado com sucesso.");
            }

            return BadRequest("Não foi possível ajustar o estoque. Verifique se a quantidade é válida.");
        }
    }
}
