using Autofac;
using Autofac.Integration.Mvc;
using GroceryStoreInventory.Models;
using GroceryStoreInventory.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(GroceryStoreInventory.Startup))]
namespace GroceryStoreInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Identity Dependencies
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);
            
            // Web Application Dependencies
            builder.RegisterType<StoreContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<StoreItemService>().AsSelf();
            builder.RegisterType<ReceivingItemService>().AsSelf();

            // Autofac MVC Boilerplate
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterFilterProvider();
            builder.RegisterControllers(typeof(MvcApplication).Assembly); 

            //Autofac WebAPI2 Boilerplate
            //builder.RegisterApiControllers(typeof(MvcApplication).Assembly); 

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            ConfigureAuth(app);
        }
    }
}
