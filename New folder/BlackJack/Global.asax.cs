﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Services;


namespace BlackJack
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        { 
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CreateGameService>().As<ICreateGameService>();

            var conteiner = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(conteiner));
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}