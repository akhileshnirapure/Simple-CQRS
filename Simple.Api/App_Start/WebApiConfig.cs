using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Simple.Api.Filters;

namespace Simple.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.DependencyResolver = new AutofacWebApiDependencyResolver(GetContainer());
            
            config.Filters.Add(new ValidationExceptionFilter());
        }

        private static ILifetimeScope GetContainer()
        {
            var config = new AutofacConfig();

            return config.GetContainer();

        }
    }
}
