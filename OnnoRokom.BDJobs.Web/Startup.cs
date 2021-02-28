using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnnoRokom.BDJobs.Web.Startup))]
namespace OnnoRokom.BDJobs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
