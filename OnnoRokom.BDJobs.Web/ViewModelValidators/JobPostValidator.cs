using FluentValidation;
using OnnoRokom.BDJobs.Web.Models.JobModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnnoRokom.BDJobs.Web.ViewModelValidators
{
    public class JobPostValidator : AbstractValidator<CreateJobPostModel>
    {
        public JobPostValidator()
        {
            RuleFor(jp => jp.Title)
                .NotNull()
                .Length(5, 50).WithMessage("5 <= Title Length <= 50");

            RuleFor(jp => jp.Description)
                .NotNull()
                .Length(50, 2000).WithMessage("50 <= Description Length <= 2000");
        }
    }
}