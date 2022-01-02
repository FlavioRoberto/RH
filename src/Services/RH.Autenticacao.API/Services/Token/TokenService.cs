using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RH.Autenticacao.API.Extensions;
using RH.Autenticacao.API.Models;
using RH.WebApi.Core.Identidade;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RH.Autenticacao.API.Services.Token
{
    public class TokenService : ITokenService
    {
        private Appsettings _appSettings;

        public TokenService(IOptions<Appsettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<UsuarioRespostaLogin> GerarJwt(IdentityUser usuario, IList<Claim> claims, IList<string> roles)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpachDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpachDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(_appSettings.Secret);

            SecurityToken token = null;

            token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpireIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpachDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
