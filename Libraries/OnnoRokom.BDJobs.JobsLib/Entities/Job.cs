using OnnoRokom.BDJobs.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Entities
{
    public class Job : EntityBase<Guid>
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreationDate { get; set; }

        public virtual Guid EmployerId { get; set; }
        public virtual IList<Candidate> Candidates { get; set; } = new List<Candidate>();

        public virtual void AddCandidate(Candidate candidate)
        {
            candidate.Jobs.Add(this);
            Candidates.Add(candidate);
        }
    }
}
