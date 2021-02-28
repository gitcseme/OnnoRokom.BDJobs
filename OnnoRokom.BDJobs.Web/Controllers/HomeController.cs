using OnnoRokom.BDJobs.JobsLib.Entities;
using OnnoRokom.BDJobs.JobsLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnnoRokom.BDJobs.Web.Controllers
{
    public class HomeController : Controller
    {
        public IJobService _jobService { get; }

        public HomeController(IJobService jobService)
        {
            _jobService = jobService;
        }

        public ActionResult Index()
        {
            var job = new Job
            {
                Title = "Test Job",
                Description = "This is testing job post",
                EmployerId = new Guid("b1d9a435-688b-49ea-a555-56013ef065fd")
            };

            _jobService.Create(job);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}