using Microsoft.AspNet.Identity;
using OnnoRokom.BDJobs.JobsLib.Entities;
using OnnoRokom.BDJobs.JobsLib.Services;
using OnnoRokom.BDJobs.JobsLib.Utilities;
using OnnoRokom.BDJobs.Web.Models.JobModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnnoRokom.BDJobs.Web.Controllers
{
    public class JobPostController : Controller
    {
        private IJobService _jobService { get; }
        private readonly ApplicationUserManager _userManager;

        public JobPostController(IJobService jobService, ApplicationUserManager userManager)
        {
            _jobService = jobService;
            _userManager = userManager;
        }

        // GET: JobPost
        public ActionResult Index()
        {
            var model = new JobPostIndexModel
            {
                JobPosts = _jobService.GetAll().Select(j =>
                     new JobPostViewModel {
                        Title = j.Title,
                        Description = j.Description.Length > 30 ? j.Description.Substring(0, 30) + "..." : j.Description,
                        CreationDate = GeneralUtilityMethods.GetFormattedDate(j.CreationDate),
                        EmployerName = GetEmployerName(j.EmployerId)
                     }
                ).ToList()
            };

            return View(model);
        }

        private string GetEmployerName(Guid employerId)
        {
            var user = _userManager.FindById(employerId.ToString());
            return user.Email; // FullName
        }

        [HttpGet]
        public ActionResult CreateJobPost()
        {
            var model = new CreateJobPostModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateJobPost(CreateJobPostModel model)
        {
            var loggedinUserId = new Guid(User.Identity.GetUserId());

            var job = new Job
            {
                Title = model.Title,
                Description = model.Description,
                EmployerId = loggedinUserId,
                CreationDate = DateTime.Now
            };

            _jobService.Create(job);

            return RedirectToAction("Index");
        }
    }
}