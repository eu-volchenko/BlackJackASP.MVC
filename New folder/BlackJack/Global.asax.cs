using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BlackJack.api;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Services;
using Autofac.Integration.WebApi;


namespace BlackJack
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<CreateGameService>().As<ICreateGameService>();
            builder.RegisterType<RoundService>().As<IRoundService>();
            builder.RegisterType<HistoryService>().As<IHistoryService>();

            var conteiner = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(conteiner);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(conteiner));
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
