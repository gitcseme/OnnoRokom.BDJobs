using FluentNHibernate.Mapping;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Mappings
{
    public class JobMap : ClassMap<Job>
    {
        public JobMap()
        {
            Table("Jobs");

            Id(j => j.Id).GeneratedBy.Guid();
            Map(j => j.EmployerId).Not.Nullable();
            Map(j => j.Title).Length(50).Not.Nullable();
            Map(j => j.Description).Length(2000).Not.Nullable();
            Map(j => j.CreationDate).Not.Nullable();

            HasManyToMany(j => j.Candidates)
                .Cascade.All()
                .Table("JobCandidate");
        }
    }
}
