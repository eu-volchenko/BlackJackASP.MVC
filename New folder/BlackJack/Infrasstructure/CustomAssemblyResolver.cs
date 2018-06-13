using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dispatcher;

namespace BlackJack.Infrasstructure
{
    public class CustomAssemblyResolver:IAssembliesResolver
    {
        public ICollection<Assembly> GetAssemblies()
        {
            List<Assembly> baseAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var controllersAssembly =
                Assembly.LoadFrom(
                    @"C:\Users\Anuitex-84\source\repos\BlackJack\New folder\BlackJack.WebAPI\obj\Debug\BlackJack.WebAPI.dll");
            baseAssemblies.Add(controllersAssembly);
            return baseAssemblies;  
        }
    }
}