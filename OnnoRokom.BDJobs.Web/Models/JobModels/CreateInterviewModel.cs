using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnnoRokom.BDJobs.Web.Models.JobModels
{
    public class CreateInterviewModel
    {
        public string EmployerId { get; set; }
        public string ApplicantId { get; set; }
        public string JobId { get; set; }

        [Required]
        [Display(Name = "Schedule Time")]
        public DateTime Time { get; set; }
    }
}