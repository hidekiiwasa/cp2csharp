using Cp2_CsharpBusiness;
using Cp2_CsharpModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cp2_CsharpUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IClienteService _clienteService;

        public LoginController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // Exibe a página de login
        public IActionResult Index()
        {
            return View();
        }

        // Processa o login do cliente
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            var cliente = await _clienteService.ValidarLoginAsync(login, password);

            if (cliente)
            {
                // Se o login for válido, redireciona para a página principal
                return RedirectToAction("Index", "Home");
            }

            // Se o login falhar, retorna à página de login com mensagem de erro
            ViewBag.ErrorMessage = "Login ou senha inválidos!";
            return View("Index");
        }
    }
}
