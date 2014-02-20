using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace Domain.Repository
{
    public abstract class Repository<TEntity>:IRepository<TEntity> where TEntity:class 
    {
        public  IUnitOfWork UnitOfWork { get; set; }

        
        protected  IUnitOfWorkContext EfContext
        {
            get
            {
                if (UnitOfWork is IUnitOfWorkContext)
                {
                    return UnitOfWork as IUnitOfWorkContext;
                }
                //throw;
                    //new DataAccessException(string.Format("数据仓储上下文对象类型不正确，应为IUnitOfWorkContext，实际为 {0}", UnitOfWork.GetType().Name));
                return null;
            }
        }

        //public Repository(IUnitOfWork unitOfWork)
        //{
        //    UnitOfWork = unitOfWork;
        //}

        public System.Linq.IQueryable<TEntity> Entities
        {
            get { return EfContext.Set<TEntity>(); }
        }

        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            EfContext.RegisterNew(entity);
            return isSave ? EfContext.Commit() : 0;
        }

        public virtual int Insert(System.Collections.Generic.IEnumerable<TEntity> entities, bool isSave = true)
        {
            EfContext.RegisterNew(entities);
            return isSave ? EfContext.Commit() : 0;
        }

        public virtual int Delete(object id, bool isSave = true)
        {
            TEntity entity = EfContext.Set<TEntity>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            EfContext.RegisterDeleted(entity);
            return isSave ? EfContext.Commit() : 0;
        }


        public virtual int Delete(System.Collections.Generic.IEnumerable<TEntity> entities, bool isSave = true)
        {
            EfContext.RegisterDeleted(entities);
            return isSave ? EfContext.Commit() : 0;
        }

        public virtual int Delete(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate, bool isSave = true)
        {
            List<TEntity> entities = EfContext.Set<TEntity>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }

        public virtual int Update(TEntity entity, bool isSave = true)
        {
            EfContext.RegisterModified(entity);
            return isSave ? EfContext.Commit() : 0;
        }

        public virtual TEntity GetByKey(object key)
        {
            return EfContext.Set<TEntity>().Find(key);
        }
    }
}