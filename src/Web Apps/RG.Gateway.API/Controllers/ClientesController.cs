using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RG.Gateway.API.Services;
using RH.WebApi.Core.Controllers;
using System.Threading.Tasks;
using static RG.Gateway.API.ViewModels.UsuarioAutenticadoViewModel;

namespace RG.Gateway.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : MainController
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IAutenticacaoService _autenticacaoService;

        public ClientesController(ILogger<ClientesController> logger,
                                  IAutenticacaoService autenticacaoService)
        {
            _logger = logger;
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar(UsuarioLogin usuarioLogin)
        {
            var respostaAutenticacao = await _autenticacaoService.Autenticar(usuarioLogin);
            return Ok(respostaAutenticacao);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            var respostaRegistro = await _autenticacaoService.Registrar(usuarioRegistro);
            return Ok(respostaRegistro);
        }
    }
}
