
using Refit;
using System.Threading.Tasks;
using static RG.Gateway.API.ViewModels.UsuarioAutenticadoViewModel;

namespace RG.Gateway.API.Services
{
    public interface IAutenticacaoService
    {
        [Post("/autenticar")]
        Task<UsuarioRespostaLogin> Autenticar(UsuarioLogin login);
    }
}
