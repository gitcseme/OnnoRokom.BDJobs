using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;

namespace OnnoRokom.BDJobs.JobsLib.Repositories
{
    public interface IInterviewRepository : IRepositoryBase<Interview, Guid>
    {
        (bool, string) IsInterviewFixedAlready(Guid jobId, Guid userId);
        List<Interview> GetUserInterviewNotification(string userId);
    }
}
