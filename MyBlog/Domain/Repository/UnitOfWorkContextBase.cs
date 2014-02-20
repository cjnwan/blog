using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Domain.Repository
{
    public abstract class UnitOfWorkContextBase:IUnitOfWorkContext
    {
        protected  abstract DbContext Context { get; }

        public System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State=EntityState.Added;
            }
            IsCommitted = false;
        }

        public void RegisterNew<TEntity>(System.Collections.Generic.IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterNew(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void RegisterModified<TEntity>(TEntity entity) where TEntity : class
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            IsCommitted = false;
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;
        }

        public void RegisterDeleted<TEntity>(System.Collections.Generic.IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public bool IsCommitted { get; private set; }

        public int Commit()
        {
           if (IsCommitted)
           {
               return 0;
           }
            try
            {
                int result = Context.SaveChanges();
                IsCommitted = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                 {
                     SqlException sqlEx = e.InnerException.InnerException as SqlException;
                     throw;
                 }
                 throw;
            }
        }

        public void Rollback()
        {
            IsCommitted = false;
        }

        public void Dispose()
        {
           if (!IsCommitted)
           {
               Commit();
           }
            Context.Dispose();
        }
    }
}