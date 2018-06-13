using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Services;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;
using BlackJackDAL.Interfaces;
using BlackJackDAL.Repositories;
using Type = System.Type;

namespace BlackJack.WebApiNew
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.
            ConnectionStrings["ContextDB"].ConnectionString;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(DpGenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerDependency();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterType<CreateGameService>().As<ICreateGameService>();
            builder.RegisterType<RoundService>().As<IRoundService>();
            builder.RegisterType<HistoryService>().As<IHistoryService>();
            //builder.RegisterType<DpGenericRepository<UserCard>>().As<IGenericRepository<UserCard>>();
            //builder.RegisterType<DpGenericRepository<User>>().As<IGenericRepository<User>>();
            //builder.RegisterType<DpGenericRepository<Round>>().As<IGenericRepository<RoundRepository>>();
            //builder.RegisterType<DpGenericRepository<History>>().As<IGenericRepository<History>>();
            //builder.RegisterType<DpGenericRepository<Game>>().As<IGenericRepository<Game>>();
            //builder.RegisterType<DpGenericRepository<Card>>().As<IGenericRepository<Card>>();
            builder.RegisterType<DpGenericRepository<Type>>().As<IGenericRepository<Type>>();

            var conteiner = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(conteiner);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(conteiner));
            AreaRegistration.RegisterAllAreas();
           
        }
    }
}
