using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using OnnoRokom.BDJobs.JobsLib;
using OnnoRokom.BDJobs.Web.AutofacIdentityConfiguration;
using OnnoRokom.BDJobs.Web.Models;
using OnnoRokom.BDJobs.Web.Seed;
using OnnoRokom.BDJobs.Web.SerilogHelper;
using Owin;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(OnnoRokom.BDJobs.Web.Startup))]
namespace OnnoRokom.BDJobs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // REGISTER DEPENDENCIES
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerLifetimeScope();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerLifetimeScope();

            // REGISTER DATABASE & TABLE & SEED PROVIDER
            builder.RegisterType<Initializer>().AsSelf().InstancePerLifetimeScope();

            // REGISTER SERILOG
            builder.RegisterType<Logger>().SingleInstance();

            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // REGISTER CUSTOM MODULE
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            builder.RegisterModule(new JobModule(connectionString));

            // BUILD THE CONTAINER SET DEPENDENCY RESOLVER AS AUTOFAC
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            // SEED & DATABASE CREATION
            using (var scope = container.BeginLifetimeScope())
            {
                var initializer = scope.Resolve<Initializer>();
                var _logger = scope.Resolve<Logger>().GetLogger;

                //Task.Run(async () => await initializer.SeedAsync()).Wait();
                initializer.SeedAsync().Wait();

                _logger.Information("Application is Seeding ...");
            }

            // REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);
        }
    }
}
