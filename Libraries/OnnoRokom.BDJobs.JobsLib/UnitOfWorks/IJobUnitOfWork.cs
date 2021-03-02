using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.UnitOfWorks
{
    public interface IJobUnitOfWork : IUnitOfWorkBase
    {
        IJobRepository JobRepository { get; }
        ICandidateRepository CandidateRepository { get; }
        IInterviewRepository InterviewRepository { get; }
    }
}
