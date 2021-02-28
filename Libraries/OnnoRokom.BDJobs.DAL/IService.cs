using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnnoRokom.BDJobs.DAL
{
    public interface IService<TEntity, TKey> : IDisposable 
        where TEntity : class, IEntity<TKey>
    {
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(TKey Id);
        IQueryable<TEntity> GetAll();
    }
}
