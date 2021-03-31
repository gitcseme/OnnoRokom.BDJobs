using OnnoRokom.BDJobs.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.JobsLib.Entities
{
    public class Interview : EntityBase<Guid>
    {
        public virtual Guid EmployerId { get; set; }
        public virtual Guid ApplicantId { get; set; }
        public virtual Guid JobId { get; set; }
        public virtual DateTime Time { get; set; }
    }
}
