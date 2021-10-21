
using FluentValidation;
using Validation.Application.InputModels;
using Validation.Enums;

namespace Validation.Application.Validators
{
    public class UsuarioInputModelValidator : AbstractValidator<UsuarioInputModel>
    {
        public UsuarioInputModelValidator()
        {
            RuleFor(cp => cp.IdUsuario)
                .GreaterThan(0)
                .WithMessage("IdUsuario não foi informado");

 
            RuleFor(cp => cp.IdUsuario)
                .NotNull()
                .NotEmpty()
                .WithMessage("IdUsuário não pode ser null ou vazio.");

      
        }
    }
}
