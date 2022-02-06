using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RH.Clientes.API.Infra.Data;
using RH.WebApi.Core.Identidade;

namespace RH.Clientes.API.Configurations
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClientesContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(option =>
            {
                option.AddPolicy("Total",
                    builder =>
                      builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                    );
            });

            return services;
        }
        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseCors("Total");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
