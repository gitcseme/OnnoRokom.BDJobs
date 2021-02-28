﻿using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Repositories
{
    public class JobRepository : RepositoryBase<Job, Guid>, IJobRepository
    {
        public JobRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}