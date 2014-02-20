using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Domain.Repository
{
    public interface IUnitOfWorkContext:IUnitOfWork,IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void RegisterNew<TEntity>(TEntity entity) where TEntity : class;

        void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void RegisterModified<TEntity>(TEntity entity) where TEntity : class;

        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;

        void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}