
using System.Collections.Generic;
using Domain.Repository;

namespace Service
{
    public abstract class ServiceBase<TEntity> where TEntity:class
    {
        protected abstract  IRepository<TEntity> CurrRepository { get; }

        public System.Linq.IQueryable<TEntity> Entities
        {
            get { return CurrRepository.Entities; }
        }

        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            return CurrRepository.Insert(entity, isSave);
        }

        public virtual int Insert(System.Collections.Generic.IEnumerable<TEntity> entities, bool isSave = true)
        {
            return CurrRepository.Insert(entities, isSave);
        }

        public virtual int Delete(object id, bool isSave = true)
        {
            return CurrRepository.Delete(id, isSave);
        }

        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            return CurrRepository.Delete(entity, isSave);
        }


        public virtual int Delete(System.Collections.Generic.IEnumerable<TEntity> entities, bool isSave = true)
        {
            return CurrRepository.Delete(entities, isSave);
        }

        public virtual int Delete(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate, bool isSave = true)
        {
            return   CurrRepository.Delete(predicate, isSave);
        }

        public virtual int Update(TEntity entity, bool isSave = true)
        {
            return CurrRepository.Update(entity, isSave);
        }

        public virtual TEntity GetByKey(object key)
        {
            return CurrRepository.GetByKey(key);
        }

    }
}