using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using OnnoRokom.BDJobs.DAL.Helpers;
using OnnoRokom.BDJobs.JobsLib;
using OnnoRokom.BDJobs.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnnoRokom.BDJobs.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //using (var _context = new ApplicationDbContext())
            //{
            //    _context.Database.Initialize(true);
            //}

            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //using (var tableCreatioinHelper = FNhibernateHelper.OpenSession(connectionString)) { }


            //// Autofac configuration
            //var builder = new ContainerBuilder();
            //builder.RegisterModule(new JobModule(connectionString));
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
