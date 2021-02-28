using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.UnitOfWorks
{
    public class JobUnitOfWork : UnitOfWorkBase, IJobUnitOfWork
    {
        public JobUnitOfWork(string connectionString) : base(connectionString)
        {
            JobRepository = new JobRepository(_dataContext);
        }

        public IJobRepository JobRepository { get; protected set; }
    }
}
