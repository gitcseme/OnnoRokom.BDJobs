using FluentNHibernate.Mapping;
using OnnoRokom.BDJobs.JobsLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Mappings
{
    public class InterviewMap : ClassMap<Interview>
    {
        public InterviewMap()
        {
            Table("Interviews");

            Id(i => i.Id).GeneratedBy.Guid();
            Map(i => i.EmployerId).Not.Nullable();
            Map(i => i.ApplicantId).Not.Nullable();
            Map(i => i.JobId).Not.Nullable();
            Map(i => i.Time).Not.Nullable();
        }
    }
}
