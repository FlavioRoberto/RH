using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RH.Autenticacao.API.Models;
using RH.Autenticacao.API.Services.Token;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Autenticacao.API.Controllers
{
    [ApiController]
    [Route("api/autenticacao")]
    public class AutenticacaoController : ControllerBase
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
                return Ok(await GerarJwt(usuario.Email));
            }

            return BadRequest(result.Errors.Select(x => new { codigo = x.Code, erro = x.Description }));
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
                return Ok(await GerarJwt(usuarioLogin.Email));

            return BadRequest();
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
