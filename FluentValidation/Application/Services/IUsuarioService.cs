using Validation.Application.ViewModels;
using System.Threading.Tasks;
using Validation.Application.InputModels;

namespace Validation.Application.Services
{
    public interface IUsuarioService
    {
        public Task<UsuarioViewModel> ObterNomeUsuario(int idUsuario);
        public void ExcessaoSemTratamento();
        public void ExcecaoComTratamentoTryCatch(UsuarioInputModel usuarioInputModel);
        public void ExcecaoComTratamentoThrowNew();

    }
}
