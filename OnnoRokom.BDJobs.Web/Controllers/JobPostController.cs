﻿using Microsoft.AspNet.Identity;
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

        [HttpPost]
        public ActionResult ApplyToJob(JobPostViewModel model)
        {
            var loggedinUserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var job = _jobService.Get(new Guid(model.JobId));
                var candidate = new Candidate
                {
                    UserId = new Guid(loggedinUserId)
                };

                _jobService.AddJobCandidate(job, candidate);

                return RedirectToAction("ViewDetails", new { jobId = model.JobId });
            }

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