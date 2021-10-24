using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RH.Autenticacao.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RH API de Autenticação",
                    Version = "v1",
                    Description = "Api para autenticar e cadastrar usuários no sistema de RH.",
                    Contact = new OpenApiContact { Email = "flavio.r.teixeira@outlook.com", Name = "Flávio Roberto Teixeira" }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RH.Autenticacao.API v1"));
            return app;
        }
    }
}
