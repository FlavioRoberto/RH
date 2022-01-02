using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RG.Gateway.API.Extensions;
using RG.Gateway.API.Services;
using RG.Gateway.API.Services.Handlers;
using System;

namespace RG.Gateway.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            #region Refit
            services.AddHttpClient("Refit", opt => {
                opt.BaseAddress = new Uri(configuration.GetSection("AutenticacaoUrl").Value);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddTypedClient(Refit.RestService.For<IAutenticacaoService>);

            #endregion
        }
    }
}