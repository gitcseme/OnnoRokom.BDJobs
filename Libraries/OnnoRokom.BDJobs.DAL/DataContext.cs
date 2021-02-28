using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnnoRokom.BDJobs.DAL.Helpers;

namespace OnnoRokom.BDJobs.DAL
{
    public class DataContext : IDataContext
    {
        public DataContext(string connectionString)
        {
            _session = FNhibernateHelper.OpenSession(connectionString);
            _transaction = _session.BeginTransaction();
        }

        public ITransaction _transaction { get; set; }
        public ISession _session { get; set; }

        public void SaveChanges()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Rollback();
                }
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
        }
    }
}
