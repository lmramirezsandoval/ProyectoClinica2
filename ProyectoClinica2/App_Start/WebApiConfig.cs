using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using ProyectoClinica2.Business;
using ProyectoClinica2.DependencyInjection;
using ProyectoClinica2.Interfaces;
using ProyectoClinica2.Repositories;
using Unity;

namespace ProyectoClinica2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Dependency Injection
            var container = new UnityContainer();
            container.RegisterType<ICitasRepository, CitasRepository>();
            container.RegisterType<IPacientesRepository, PacientesRepository>();
            config.DependencyResolver = new DependencyInjectionConfiguration(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
