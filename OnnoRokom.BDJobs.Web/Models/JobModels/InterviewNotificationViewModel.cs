using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnnoRokom.BDJobs.Web.Models.JobModels
{
    public class InterviewNotificationViewModel
    {
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string InterviewTime { get; set; }
    }
}