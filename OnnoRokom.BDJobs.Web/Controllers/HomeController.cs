using OnnoRokom.BDJobs.JobsLib.Entities;
using OnnoRokom.BDJobs.JobsLib.Services;
using OnnoRokom.BDJobs.Web.SerilogHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnnoRokom.BDJobs.Web.Controllers
{
    public class HomeController : Controller
    {
        private IJobService _jobService { get; }
        private ILogger _logger { get; }

        public HomeController(IJobService jobService, Logger logger)
        {
            _jobService = jobService;
            _logger = logger.GetLogger;
        }

        public ActionResult Index()
        {
            _logger.Information("Index page is loading...");

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