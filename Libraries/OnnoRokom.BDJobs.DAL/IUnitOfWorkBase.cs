using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.DAL
{
    public interface IUnitOfWorkBase : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}
