using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RH.Autenticacao.API.Models;
using System.Threading.Tasks;

namespace RH.Autenticacao.API.Controllers
{
    [Route("api/autenticacao")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AutenticacaoController(SignInManager<IdentityUser> signInManager,
                                      UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(usuario, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(usuario, false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

    }
}
