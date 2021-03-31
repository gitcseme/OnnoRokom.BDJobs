using Microsoft.AspNet.Identity;
using OnnoRokom.BDJobs.JobsLib.Entities;
using OnnoRokom.BDJobs.JobsLib.Services;
using OnnoRokom.BDJobs.JobsLib.Utilities;
using OnnoRokom.BDJobs.Web.Models.JobModels;
using OnnoRokom.BDJobs.Web.SerilogHelper;
using OnnoRokom.BDJobs.Web.ViewModelValidators;
using Serilog;
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
        private ILogger _logger { get; }

        public JobPostController(IJobService jobService, ApplicationUserManager userManager, Logger logger)
        {
            _jobService = jobService;
            _userManager = userManager;
            _logger = logger.GetLogger;
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
                         Description = j.Description.Length > 100 ? j.Description.Substring(0, 100) + "..." : j.Description,
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
        [HttpPost, ValidateInput(false)]
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
            JobPostValidator validator = new JobPostValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }

            var loggedinUserId = new Guid(User.Identity.GetUserId());

            var job = new Job
            {
                Title = model.Title,
                Description = model.Description,
                EmployerId = loggedinUserId,
                CreationDate = DateTime.Now
            };

            if (job.Description.Length > 15)
                job.Description = "<b>" + job.Description.Substring(0, 15) + "</b>" + job.Description.Substring(15);

            _jobService.Create(job);

            _logger.Information($"A new job post is created at {DateTime.Now} with title {job.Title}");

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

        [Authorize(Roles = "Employer")]
        public ActionResult ViewAppliedCandidates()
        {
            var loggedinUserId = User.Identity.GetUserId();

            var model = _jobService.GetEmployerJobsAndCandidates(loggedinUserId).Select(job => new JobPostViewModel
            {
                JobId = job.Id.ToString(),
                Title = job.Title,
                Description = job.Description.Length > 100 ? job.Description.Substring(0, 100) + "..." : job.Description,
                CreationDate = GeneralUtilityMethods.GetFormattedDate(job.CreationDate),
                EmployerName = GetEmployerName(job.EmployerId),
                Applicants = GetApplicants(job.Candidates.ToList(), job)
            }).ToList();

            return View(model);
        }

        private List<Applicant> GetApplicants(List<Candidate> Candidates, Job job)
        {
            List<Applicant> applicants = new List<Applicant>();
            foreach (var candidate in Candidates)
            {
                var user = _userManager.FindById(candidate.UserId.ToString());
                var interviewInfo = _jobService.IsInterviewFixedAlready(job.Id, candidate.UserId);

                var applicant = new Applicant
                {
                    UserId = user.Id.ToString(),
                    Name = user.FullName,
                    IsInterviewFixed = interviewInfo.Item1,
                    InterviewTime = interviewInfo.Item2
                };

                applicants.Add(applicant);
            }

            return applicants;
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult FixInterview(string jobId, string applicantId)
        {
            var loggedinUserId = User.Identity.GetUserId();

            var model = new CreateInterviewModel
            {
                JobId = jobId,
                ApplicantId = applicantId,
                EmployerId = loggedinUserId
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public ActionResult FixInterview(CreateInterviewModel model)
        {
            if (ModelState.IsValid)
            {
                var interview = new Interview
                {
                    JobId = new Guid(model.JobId),
                    ApplicantId = new Guid(model.ApplicantId),
                    EmployerId = new Guid(model.EmployerId),
                    Time = model.Time
                };

                _jobService.CreateInterView(interview);
                _logger.Information($"A interview is fixed at {interview.Time}");

                return RedirectToAction("ViewAppliedCandidates");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult InterviewNotification()
        {
            var loggedinUserId = User.Identity.GetUserId();

            var interviews = _jobService.GetUserInterviewNotification(loggedinUserId);
            List<InterviewNotificationViewModel> model = new List<InterviewNotificationViewModel>();

            foreach (var interview in interviews)
            {
                var job = _jobService.Get(interview.JobId);
                var employer = _userManager.FindById(interview.EmployerId.ToString());

                var interviewNotificationModel = new InterviewNotificationViewModel
                {
                    InterviewTime = interview.Time.ToString(),
                    JobTitle = job.Title,
                    Description = job.Description,
                    EmployerName = employer?.FullName
                };

                model.Add(interviewNotificationModel);
            }

            return View(model);
        }
    }
}