using OnnoRokom.BDJobs.DAL;
using System;
using System.Collections.Generic;

namespace OnnoRokom.BDJobs.JobsLib.Entities
{
    public class Candidate : EntityBase<Guid>
    {
        public virtual Guid UserId { get; set; }
        public virtual IList<Job> Jobs { get; set; } = new List<Job>();
    }
}