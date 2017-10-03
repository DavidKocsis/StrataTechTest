using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using BasketApi.IOC;
using Microsoft.Practices.Unity;
using StrataBasket;
using StrataServices;
using StrataServices.JsonRepos;

namespace BasketApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            string jsonDataFolder = HttpContext.Current.Server.MapPath("~/bin");
            var container = new UnityContainer();
            container.RegisterType<IBasket, Basket>();
            container.RegisterType<ICustomerDetailsRepository, CustomerDetailsJsonRepository>(
                new InjectionConstructor($"{jsonDataFolder}/Data/Customers.json"));
            container.RegisterType<IBasketRepository, BasketJsonRepository>(
                new InjectionConstructor($"{jsonDataFolder}/Data/Basket.json"));
            container.RegisterType<IPaymentService, PaymentService>();
            container.RegisterType<IMessager, Messager>();
            config.DependencyResolver = new UnityResolver(container);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
