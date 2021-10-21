using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Validation.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using Validation.Extensions;

namespace Validation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddApplication();
            services.AddValidators();
            services.AddControllers();

            services
                .AddMvc(options => options.Filters.Add(typeof(ModelStateValidatorFilter)))
                .AddFluentValidation(fv => {
                    // Para de executar as demais validações quando ocorrer a primeira excessão
                    fv.ValidatorOptions.CascadeMode = FluentValidation.CascadeMode.Stop;
                    // Define a linguagem do validador
                    fv.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("pt-Br");
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Validation", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Validation v1"));
            }

            app.UseApiExceptionHandling();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
