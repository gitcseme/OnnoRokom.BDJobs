using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnnoRokom.BDJobs.JobsLib.Repositories
{
    public class InterviewRepository : RepositoryBase<Interview, Guid>, IInterviewRepository
    {
        public InterviewRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public List<Interview> GetUserInterviewNotification(string userId)
        {
            return _dataContext._session.Query<Interview>()
                .Where(i => i.ApplicantId.ToString() == userId)
                .ToList();
        }

        public (bool, string) IsInterviewFixedAlready(Guid jobId, Guid userId)
        {
            var interview = _dataContext._session.Query<Interview>()
                .FirstOrDefault(i => i.JobId == jobId && i.ApplicantId == userId);

            bool isInterviewAlreadyFixed = interview != null;
            string time = isInterviewAlreadyFixed ? interview.Time : "Not set yet";

            return (isInterviewAlreadyFixed, time);
        }
    }
}
