using Microsoft.AspNet.Identity.EntityFramework;
using OnnoRokom.BDJobs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnnoRokom.BDJobs.Web.AutofacIdentityConfiguration
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context)
        : base(context)
        {
        }
    }
}