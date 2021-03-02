using Microsoft.AspNet.Identity;
using OnnoRokom.BDJobs.JobsLib.Entities;
using OnnoRokom.BDJobs.JobsLib.Services;
using OnnoRokom.BDJobs.JobsLib.Utilities;
using OnnoRokom.BDJobs.Web.Models.JobModels;
using System;
using System.Collections.Generic;
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
                         JobId = j.Id.ToString(),
                         Title = j.Title,
                         Description = j.Description.Length > 30 ? j.Description.Substring(0, 30) + "..." : j.Description,
                         CreationDate = GeneralUtilityMethods.GetFormattedDate(j.CreationDate),
                         EmployerName = GetEmployerName(j.EmployerId)
                     }
                ).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ViewDetails(string jobId)
        {
            var loggedinUserId = User.Identity.GetUserId();

            var job = _jobService.Get(new Guid(jobId));
            var model = new JobPostViewModel
            {
                JobId = job.Id.ToString(),
                Title = job.Title,
                Description = job.Description,
                CreationDate = GeneralUtilityMethods.GetFormattedDate(job.CreationDate),
                EmployerName = GetEmployerName(job.EmployerId),
                IsAlreadyApplied = job.Candidates.Where(c => c.UserId.ToString() == loggedinUserId).Any()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ApplyToJob(JobPostViewModel model)
        {
            var loggedinUserId = new Guid(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                var job = _jobService.Get(new Guid(model.JobId));
                var candidate = _jobService.GetCandidate(loggedinUserId);
                if (candidate == null)
                {
                    candidate = new Candidate
                    {
                        UserId = loggedinUserId
                    };
                }

                _jobService.AddJobCandidate(job, candidate);

                return RedirectToAction("ViewDetails", new { jobId = model.JobId });
            }

            return View(model);
        }

        private string GetEmployerName(Guid employerId)
        {
            var user = _userManager.FindById(employerId.ToString());
            return user.FullName;
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult CreateJobPost()
        {
            var model = new CreateJobPostModel();

            return View(model);
        }

        [Authorize(Roles = "Employer")]
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

        [Authorize]
        public ActionResult ViewAppliedJobs()
        {
            var loggedinUserId = User.Identity.GetUserId();

            var model = _jobService.GetCandidateAppliedJobs(loggedinUserId).Select(job => new JobPostViewModel
            {
                JobId = job.Id.ToString(),
                Title = job.Title,
                Description = job.Description,
                CreationDate = GeneralUtilityMethods.GetFormattedDate(job.CreationDate),
                EmployerName = GetEmployerName(job.EmployerId),
            }).ToList();

            return View(model);
        }

        public ActionResult ViewAppliedCandidates()
        {
            var loggedinUserId = User.Identity.GetUserId();

            var model = _jobService.GetEmployerJobsAndCandidates(loggedinUserId).Select(job => new JobPostViewModel
            {
                JobId = job.Id.ToString(),
                Title = job.Title,
                Description = job.Description.Length > 30 ? job.Description.Substring(0, 30) + "..." : job.Description,
                CreationDate = GeneralUtilityMethods.GetFormattedDate(job.CreationDate),
                EmployerName = GetEmployerName(job.EmployerId),
                Applicants = GetApplicants(job.Candidates.ToList())
            }).ToList();

            return View(model);
        }

        private List<Applicant> GetApplicants(List<Candidate> Candidates)
        {
            List<Applicant> applicants = new List<Applicant>();
            foreach (var candidate in Candidates)
            {
                var user = _userManager.FindById(candidate.UserId.ToString());
                var applicant = new Applicant
                {
                    UserId = user.Id.ToString(),
                    Name = user.FullName
                };

                applicants.Add(applicant);
            }

            return applicants;
        }
    }
}