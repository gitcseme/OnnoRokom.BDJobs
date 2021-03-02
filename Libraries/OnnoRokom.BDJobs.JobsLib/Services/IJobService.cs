using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Services
{
    public interface IJobService : IService<Job, Guid>
    {
        void AddJobCandidate(Job job, Candidate candidate);
        List<Job> GetCandidateAppliedJobs(string userId);

        Candidate GetCandidate(Guid userId);
        List<Job> GetEmployerJobsAndCandidates(string employerId);
    }
}
