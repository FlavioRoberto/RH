using Microsoft.AspNetCore.Mvc;
using RH.Clientes.API.Application.Commands.Registrar;
using RH.Core.Mediator;
using RH.WebApi.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace RH.Clientes.API.Controllers
{
    [Route("[controller]")]
    public class WeatherForecastController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public WeatherForecastController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Teste()
        {
            var resultado = await _mediatorHandler.EnviarComando(new RegistrarClienteCommand(Guid.NewGuid(), "Teste", "teste@edu.com"));
            return CustomResponse(resultado);
        }
    }
}
