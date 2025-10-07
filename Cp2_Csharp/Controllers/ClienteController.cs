using Cp2_CsharpBusiness;
using Cp2_CsharpModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cp2_CsharpApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        // Injeção de dependência
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // Endpoint para criar um novo cliente
        [HttpPost]
        public async Task<ActionResult> CriarCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente inválido.");
            }

            bool sucesso = await _clienteService.AdicionarClienteAsync(cliente);
            if (sucesso)
            {
                return CreatedAtAction(nameof(BuscarCliente), new { id = cliente.Id }, cliente);
            }

            return StatusCode(500, "Erro ao criar cliente.");
        }

        // Endpoint para listar todos os clientes
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> BuscarTodosClientes()
        {
            var clientes = await _clienteService.BuscarTodosClientesAsync();
            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("Nenhum cliente encontrado.");
            }

            return Ok(clientes);
        }

        // Endpoint para buscar cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> BuscarCliente(int id)
        {
            var cliente = await _clienteService.BuscarClientePorIdAsync(id);
            if (cliente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }

            return Ok(cliente);
        }

        // Endpoint para atualizar um cliente existente
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null || id != cliente.Id)
            {
                return BadRequest("Dados inválidos.");
            }

            var clienteExistente = await _clienteService.BuscarClientePorIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }

            bool sucesso = await _clienteService.AtualizarClienteAsync(cliente);
            if (sucesso)
            {
                return NoContent(); // Atualização bem-sucedida
            }

            return StatusCode(500, "Erro ao atualizar cliente.");
        }

        // Endpoint para excluir um cliente
        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirCliente(int id)
        {
            var clienteExistente = await _clienteService.BuscarClientePorIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }

            bool sucesso = await _clienteService.ExcluirClienteAsync(id);
            if (sucesso)
            {
                return NoContent(); // Excluído com sucesso
            }

            return StatusCode(500, "Erro ao excluir cliente.");
        }

        // Endpoint para validação de login
        [HttpPost("login")]
        public async Task<ActionResult> ValidarLogin([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Dados de login inválidos.");
            }

            bool autenticado = await _clienteService.ValidarLoginAsync(cliente.Login, cliente.Password);
            if (autenticado)
            {
                return Ok("Login bem-sucedido.");
            }

            return Unauthorized("Login ou senha inválidos.");
        }
    }
}
