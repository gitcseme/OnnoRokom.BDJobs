using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnnoRokom.BDJobs.Web.Models.JobModels
{
    public class JobPostViewModel
    {
        public string JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public string EmployerName { get; set; }
        public bool IsAlreadyApplied { get; set; }
    }
}