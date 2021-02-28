using NHibernate;
using System;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.DAL
{
    public interface IDataContext : IDisposable
    {
        ISession _session { get; set; }
        ITransaction _transaction { get; set; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}