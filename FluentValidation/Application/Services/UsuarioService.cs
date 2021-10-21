using Validation.Application.ViewModels;
using System.Threading.Tasks;
using Validation.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Validation.Infrastructure.Domain;
using Validation.Application.InputModels;

namespace Validation.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IEnumerable<Usuario> _usuarios;

        public UsuarioService()
        {
            _usuarios = new List<Usuario>()
            {
                new Usuario(){ Id=1, Nome="João Silva"},
                new Usuario(){ Id=1, Nome="Maria Silva"},
                new Usuario(){ Id=1, Nome="Cristiane Silva"},
            };
        }



        public Task<UsuarioViewModel> ObterNomeUsuario(int idUsuario)
        {
            var usuario = _usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);

            return Task.FromResult(new UsuarioViewModel(usuario));
        }

        public void ExcessaoSemTratamento()
        {
            Usuario usuario = null;

            usuario.Nome = "";
        }

        public void ExcecaoComTratamentoTryCatch(UsuarioInputModel usuarioInputModel)
        {
            try
            {
                usuarioInputModel.Nome = usuarioInputModel.Nome.Concat("Teste").ToString();
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, Enums.ErrorCode.InternalInputView);
            }
        }

        public void ExcecaoComTratamentoThrowNew()
        {
            Usuario usuario = null;

            if (usuario is null || usuario?.Id == null)
            {
                throw new CustomException("Usário não foi carregado corretamente no servidor", Enums.ErrorCode.Internal);

            }
        }



    }
}
