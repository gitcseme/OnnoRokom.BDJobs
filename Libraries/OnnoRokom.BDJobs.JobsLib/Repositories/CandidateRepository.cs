using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnnoRokom.BDJobs.JobsLib.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate, Guid>, ICandidateRepository
    {
        public CandidateRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public Candidate GetCandidate(Guid userId)
        {
            return _dataContext._session.Query<Candidate>()
                .FirstOrDefault(c => c.UserId == userId);
        }

        public List<Job> GetCandidateAppliedJobs(string userId)
        {
            var candidate = _dataContext._session.Query<Candidate>()
                .FirstOrDefault(c => c.UserId.ToString() == userId);

            if (candidate == null)
                return new List<Job>();

            return candidate.Jobs.ToList();
        }
    }
}
