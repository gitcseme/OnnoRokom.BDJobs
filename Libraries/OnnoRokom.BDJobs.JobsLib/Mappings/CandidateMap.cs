using FluentNHibernate.Mapping;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Mappings
{
    public class CandidateMap : ClassMap<Candidate>
    {
        public CandidateMap()
        {
            Table("Candidates");

            Id(c => c.Id).GeneratedBy.Guid();
            Map(c => c.UserId).Not.Nullable();

            HasManyToMany(c => c.Jobs)
                .Cascade.All()
                .Inverse()
                .Table("JobCandidate");
        }
    }
}
