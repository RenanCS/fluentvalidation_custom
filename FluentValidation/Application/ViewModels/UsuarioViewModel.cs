
using Validation.Core.Entities;

namespace Validation.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }

        public UsuarioViewModel(Usuario usuario)
        {
            Nome = usuario.Nome;
        }
    }
}
