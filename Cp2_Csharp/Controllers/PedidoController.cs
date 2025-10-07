using Cp2_CsharpBusiness;
using Cp2_CsharpModel;
using Microsoft.AspNetCore.Mvc;

namespace Cp2_CsharpApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // Endpoint para criar um novo pedido
        [HttpPost]
        public async Task<ActionResult> CriarPedido([FromBody] Pedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest("Pedido inválido.");
            }

            bool sucesso = await _pedidoService.CriarPedidoAsync(pedido);
            if (sucesso)
            {
                return CreatedAtAction(nameof(ObterPedido), new { id = pedido.Id }, pedido);
            }

            return Conflict("Pedido com o mesmo código já existe.");
        }

        // Endpoint para listar todos os pedidos
        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> ObterTodosPedidos()
        {
            var pedidos = await _pedidoService.ObterTodosAsync();
            if (pedidos == null || pedidos.Count == 0)
            {
                return NotFound("Nenhum pedido encontrado.");
            }

            return Ok(pedidos);
        }

        // Endpoint para obter pedido por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> ObterPedido(int id)
        {
            var pedido = await _pedidoService.ObterPorIdAsync(id);
            if (pedido == null)
            {
                return NotFound($"Pedido com ID {id} não encontrado.");
            }

            return Ok(pedido);
        }

        // Endpoint para atualizar um pedido
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarPedido(int id, [FromBody] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest("ID do pedido não confere.");
            }

            bool sucesso = await _pedidoService.AtualizarPedidoAsync(pedido);
            if (sucesso)
            {
                return NoContent(); // Atualização bem-sucedida
            }

            return BadRequest("Não é permitido editar um pedido entregue.");
        }

        // Endpoint para excluir um pedido
        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirPedido(int id)
        {
            bool sucesso = await _pedidoService.ExcluirPedidoAsync(id);
            if (sucesso)
            {
                return NoContent(); // Exclusão bem-sucedida
            }

            return BadRequest("Não é permitido excluir um pedido entregue.");
        }
    }
}
