using OnnoRokom.BDJobs.JobsLib.Services;
using OnnoRokom.BDJobs.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnnoRokom.BDJobs.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private IJobService _jobService { get; }
        private readonly ApplicationUserManager _userManager;

        public DashboardController(IJobService jobService, ApplicationUserManager userManager = null)
        {
            _jobService = jobService;
            _userManager = userManager;
        }

        // GET: Admin/Dashboard
        public ActionResult Index()
        {

            var model = new AdminDashboardViewModel
            {
                TotalUsers = _userManager.Users.Count(),
                TotalJobPosts = _jobService.GetAll().Count()
            };

            return View(model);
        }
    }
}