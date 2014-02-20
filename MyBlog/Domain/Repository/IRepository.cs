using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public interface IRepository<TEntity>where TEntity:class 
    {
        IQueryable<TEntity> Entities { get; }

        int Insert(TEntity entity, bool isSave = true);
        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

        int Delete(object id, bool isSave = true);
        int Delete(TEntity entity, bool isSave = true);
        int Delete(IEnumerable<TEntity> entities, bool isSave = true);
        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true);

        int Update(TEntity entity, bool isSave = true);
        TEntity GetByKey(object key);
    }
}