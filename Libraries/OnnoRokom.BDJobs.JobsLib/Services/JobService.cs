using OnnoRokom.BDJobs.JobsLib.Entities;
using OnnoRokom.BDJobs.JobsLib.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Services
{
    public class JobService : IJobService
    {
        private IJobUnitOfWork _jobUnitOfWork { get; }

        public JobService(IJobUnitOfWork jobUnitOfWork)
        {
            _jobUnitOfWork = jobUnitOfWork;
        }

        public void Create(Job entity)
        {
            _jobUnitOfWork.JobRepository.Create(entity);
            _jobUnitOfWork.Save();
        }

        public void Delete(Job entity)
        {
            _jobUnitOfWork.JobRepository.Delete(entity);
            _jobUnitOfWork.Save();
        }

        public void Dispose()
        {
            _jobUnitOfWork.Dispose();
        }

        public void Edit(Job entity)
        {
            _jobUnitOfWork.JobRepository.Edit(entity);
            _jobUnitOfWork.Save();
        }

        public Job Get(Guid Id)
        {
            return _jobUnitOfWork.JobRepository.Get(Id);
        }

        public IQueryable<Job> GetAll()
        {
            return _jobUnitOfWork.JobRepository.GetAll();
        }

        public void AddJobCandidate(Job job, Candidate candidate)
        {
            job.AddCandidate(candidate);
            _jobUnitOfWork.Save();
        }

        public List<Job> GetCandidateAppliedJobs(string userId)
        {
            return _jobUnitOfWork.CandidateRepository.GetCandidateAppliedJobs(userId);
        }

        public Candidate GetCandidate(Guid userId)
        {
            return _jobUnitOfWork.CandidateRepository.GetCandidate(userId);
        }
    }
}
