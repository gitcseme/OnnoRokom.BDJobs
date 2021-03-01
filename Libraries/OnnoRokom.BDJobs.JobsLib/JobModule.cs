using Autofac;
using OnnoRokom.BDJobs.DAL;
using OnnoRokom.BDJobs.JobsLib.Repositories;
using OnnoRokom.BDJobs.JobsLib.Services;
using OnnoRokom.BDJobs.JobsLib.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib
{
    public class JobModule : Module
    {
        private readonly string _connectionString;
        public JobModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<JobService>()
                .As<IJobService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<JobUnitOfWork>()
                .As<IJobUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<JobRepository>()
                .As<IJobRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CandidateRepository>()
                .As<ICandidateRepository>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
