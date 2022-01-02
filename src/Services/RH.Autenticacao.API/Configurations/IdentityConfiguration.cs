using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RH.Autenticacao.API.Data;
using RH.Autenticacao.API.Extensions;
using RH.WebApi.Core.Identidade;

namespace RH.Autenticacao.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagensPtBr>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtConfiguration(Configuration);

            return services;
        }
    }
}