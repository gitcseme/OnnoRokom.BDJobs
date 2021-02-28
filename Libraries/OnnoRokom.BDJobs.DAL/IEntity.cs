using System;
using System.Collections.Generic;
using System.Text;

namespace OnnoRokom.BDJobs.DAL
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
