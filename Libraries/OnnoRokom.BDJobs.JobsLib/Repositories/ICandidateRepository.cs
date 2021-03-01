using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;

namespace OnnoRokom.BDJobs.JobsLib.Repositories
{
    public interface ICandidateRepository : IRepositoryBase<Candidate, Guid>
    {
        List<Job> GetCandidateAppliedJobs(string userId);

        Candidate GetCandidate(Guid userId);
    }
}
