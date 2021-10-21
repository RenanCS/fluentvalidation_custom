using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Validation.Application.InputModels;
using Validation.Application.Services;

namespace Validation.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> ObterNomeUsuario(UsuarioInputModel usuario)
        {
            var usuarioViewModel = await _usuarioService.ObterNomeUsuario(usuario.IdUsuario);

            return Ok(usuarioViewModel);
        }

        [HttpPost("ExcessaoSemTratamento")]
        public IActionResult ExcessaoSemTratamento()
        {
            _usuarioService.ExcessaoSemTratamento();

            return Ok();
        }

        [HttpPost("ExcecaoComTratamentoTryCatch")]
        public IActionResult ExcecaoComTratamentoTryCatch(UsuarioInputModel usuarioInputModel)
        {            
            _usuarioService.ExcecaoComTratamentoTryCatch(usuarioInputModel);

            return Ok();
        }

        [HttpGet("ExcecaoComTratamentoThrowNew")]
        public IActionResult ExcecaoComTratamentoThrowNew()
        {
            _usuarioService.ExcecaoComTratamentoThrowNew();

            return Ok();
        }
    }
}
