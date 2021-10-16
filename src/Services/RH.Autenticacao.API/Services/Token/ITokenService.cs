using Microsoft.AspNetCore.Identity;
using RH.Autenticacao.API.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RH.Autenticacao.API.Services.Token
{
    public interface ITokenService
    {
        Task<UsuarioRespostaLogin> GerarJwt(IdentityUser usuario, IList<Claim> claims, IList<string> roles);
    }
}
