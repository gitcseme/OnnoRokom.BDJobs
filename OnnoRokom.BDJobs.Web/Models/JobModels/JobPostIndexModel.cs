using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnnoRokom.BDJobs.Web.Models.JobModels
{
    public class JobPostIndexModel
    {
        public List<JobPostViewModel> JobPosts { get; set; } = new List<JobPostViewModel>();
    }
}