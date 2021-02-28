using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnnoRokom.BDJobs.DAL
{
    public abstract class UnitOfWorkBase : IUnitOfWorkBase
    {
        protected readonly IDataContext _dataContext;

        public UnitOfWorkBase(string connectionString)
        {
            _dataContext = new DataContext(connectionString);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
             await _dataContext.SaveChangesAsync();
        }
    }
}
