using System.Collections.Generic;

namespace RG.Gateway.API.ViewModels
{
    public class UsuarioAutenticadoViewModel
    {
        public class UsuarioRespostaLogin
        {
            public string AccessToken { get; set; }
            public double ExpiresIn { get; set; }
            public UsuarioToken UsuarioToken { get; set; }
            public ResponseResult ResponseResult { get; set; }
        }

        public class UsuarioLogin
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        public class UsuarioToken
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public IEnumerable<UsuarioClaim> Claims { get; set; }
        }

        public class UsuarioClaim
        {
            public string Value { get; set; }
            public string Type { get; set; }
        }
    }
}
