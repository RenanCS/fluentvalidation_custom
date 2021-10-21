using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Validation.Application.InputModels;
using Validation.Application.Services;
using Validation.Application.Validators;
using Validation.Infrastructure.ProblemDetail;

namespace Validation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UsuarioInputModel>, UsuarioInputModelValidator>();

            services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();

            return services;
        }
    }
}
