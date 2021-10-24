using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RH.Autenticacao.API.Models;
using RH.Autenticacao.API.Services.Token;
using System.Threading.Tasks;

namespace RH.Autenticacao.API.Controllers
{
    [Route("api/autenticacao")]
    public class AutenticacaoController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AutenticacaoController(SignInManager<IdentityUser> signInManager,
                                      UserManager<IdentityUser> userManager,
                                      ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var usuario = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(usuario, usuarioRegistro.Senha);

            if (result.Succeeded)
                return CustomResponse(await GerarJwt(usuario.Email));

            foreach (var erro in result.Errors)
                AdicionarErroProcessamento(erro.Description);

            return CustomResponse();
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
                return CustomResponse(await GerarJwt(usuarioLogin.Email));

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporiamente bloqueado por tentativas inválidas.");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou senha incorretos.");
            return CustomResponse();

        }

        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);
            return await _tokenService.GerarJwt(usuario, claims, roles);
        }
    }
}
