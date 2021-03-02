using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;

namespace OnnoRokom.BDJobs.JobsLib.Repositories
{
    public interface IInterviewRepository : IRepositoryBase<Interview, Guid>
    {
        (bool, string) IsInterviewFixedAlready(Guid jobId, Guid userId);
    }
}
