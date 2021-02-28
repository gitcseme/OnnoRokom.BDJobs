using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnnoRokom.BDJobs.DAL
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected IDataContext _dataContext;

        public RepositoryBase(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(TEntity entity)
        {
            _dataContext._session.Save(entity);
        }

        public void Delete(TEntity entity)
        {
            _dataContext._session.Delete(entity);
        }

        public void Edit(TEntity entity)
        {
            _dataContext._session.Update(entity);
        }

        public TEntity Get(TKey Id)
        {
            return _dataContext._session.Get<TEntity>(Id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dataContext._session.Query<TEntity>();
        }
    }
}
