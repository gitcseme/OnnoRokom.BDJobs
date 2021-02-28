using System;
using System.Collections.Generic;
using System.Text;

namespace OnnoRokom.BDJobs.DAL
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
